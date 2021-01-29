using ProductCatalogManager.Models;
using System.Threading.Tasks;

namespace ProductCatalogManager.Handler
{
    public interface IProductDal : IRepository<Product, string>
    {
         Task<Product> GetByCodeAsync(string code);
    }
}