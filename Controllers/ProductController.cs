using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClosedXML.Excel;
using ProductCatalogManager.Business;
using ProductCatalogManager.Models;

namespace ProductCatalogManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductBusiness productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            this.productBusiness = productBusiness;
        }

        [HttpGet("{id:length(24)}", Name = "GetProductById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await productBusiness.GetProductById(id);

            if(product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IQueryable<Product>>> GetProducts()
        {
            var products = productBusiness.GetProducts();
            return Ok(products);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            var isAdd = await productBusiness.AddProduct(product);
            if (!isAdd)
                return BadRequest();
            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product value)
        {
            var isUpdated = await productBusiness.UpdateProduct(value);
            if (!isUpdated)
                return BadRequest();
            return Ok(value);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            var isDeleted = await productBusiness.DeleteProduct(id);
            if (!isDeleted)
                return BadRequest();
            return Ok();
        }

        [Route("[action]/{id}")]
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmProductById(string id)
        {
            var isConfirmed = await productBusiness.ConfirmProduct(id);
            if (!isConfirmed)
                return BadRequest();
            return Ok();
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IQueryable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var products = productBusiness.SearchProductByName(name);
            if(products == null)
                return NotFound();
            return Ok(products);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(File), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Excel()
        {
            var products = productBusiness.GetProducts();
            //this part should have been in business layer
            using(var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Code";
                worksheet.Cell(currentRow, 3).Value = "Name";
                worksheet.Cell(currentRow, 4).Value = "Price";
                worksheet.Cell(currentRow, 5).Value = "CreatedAt";
                worksheet.Cell(currentRow, 6).Value = "LastUpdated";
                foreach (var product in products)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = product.Id;
                    worksheet.Cell(currentRow, 2).Value = product.Code;
                    worksheet.Cell(currentRow, 3).Value = product.Name;
                    worksheet.Cell(currentRow, 4).Value = product.Price;
                    worksheet.Cell(currentRow, 5).Value = product.CreatedAt;
                    worksheet.Cell(currentRow, 6).Value = product.LastUpdated;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return Ok(File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "products.xlsx"));
                }
            }

        }
    }
}