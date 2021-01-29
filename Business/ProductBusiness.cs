using ProductCatalogManager.Models;
using ProductCatalogManager.Handler;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;


namespace ProductCatalogManager.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductDal productHandler;

        public ProductBusiness(IProductDal productHandler)
        {
            this.productHandler = productHandler;
        }

        public async Task<bool> AddProduct(Product product)
        {
            if(product.Price < 1)
                return false;
            product.IsConfirmed = product.Price < 1000;
            
            var productByCode = await productHandler.GetByCodeAsync(product.Code);
            if(productByCode != null) {
                return false;
            }
            product.CreatedAt = DateTime.Now;
            await productHandler.AddAsync(product);

            return true;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var productById = await productHandler.GetByIdAsync(product.Id);
            if(productById == null)
                return false;
            if(productById.Code != product.Code)
                return false;
            product.IsConfirmed = product.Price < 1000;
            product.CreatedAt = productById.CreatedAt;
            product.LastUpdated = DateTime.Now;

            await productHandler.UpdateAsync(product.Id, product);
            return true;
        }

        public async Task<bool> ConfirmProduct(string id)
        {
            var productById = await productHandler.GetByIdAsync(id);
            if(productById == null)
                return false;
            if(productById.Price < 1000)
                return false;
            productById.IsConfirmed = true;
            productById.LastUpdated = DateTime.Now;

            await productHandler.UpdateAsync(id, productById);
            return true;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deletedProduct = await productHandler.DeleteAsync(id);
            if(deletedProduct == null)
                return false;
            return true;
        }
        
        public async Task<Product> GetProductById(string id)
        {
            var product = await productHandler.GetByIdAsync(id);

            return product;
        }

        public IQueryable<Product> GetProducts()
        {
            var products = productHandler.Get();
            return products;
        }

        public IQueryable<Product> SearchProductByName(string name)
        {
            var products = productHandler.Get(t => t.Name == name);

            return products;
        }
    }
}