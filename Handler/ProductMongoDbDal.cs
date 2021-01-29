using ProductCatalogManager.Models;
using ProductCatalogManager.Utilities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;


namespace ProductCatalogManager.Handler
{
    public class ProductMongoDbDal : MongoDbRepoBase<Product> , IProductDal
    {
        public ProductMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
            
        }
        public Task<Product> GetByCodeAsync(string code)
        {
            return Collection.Find(x => x.Code == code).FirstOrDefaultAsync();
        }
    }
}