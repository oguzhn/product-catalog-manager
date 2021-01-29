using ProductCatalogManager.Models;
using System.Threading.Tasks;
using System.Linq;


namespace ProductCatalogManager.Business
{
    public interface IProductBusiness
    {
         Task<bool> AddProduct(Product product);
         Task<bool> UpdateProduct(Product product);
         Task<bool> ConfirmProduct(string id);
         Task<bool> DeleteProduct(string id);
         Task<Product> GetProductById(string id);
         IQueryable<Product> GetProducts();
         IQueryable<Product> SearchProductByName(string name);
    }
}