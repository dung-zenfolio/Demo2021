using Microsoft.EntityFrameworkCore;
using poc_productdatabase.Context;
using poc_productdatabase.Entities;
using poc_resource.Repositories.IRepository;
using System;
using System.Threading.Tasks;

namespace poc_resource.Repositories
{
    public class ProductRepository : Repository<ProductEntity, ProductContext>, IProductRepository
    {
        private ProductContext _productContext;
        public ProductRepository(ProductContext context): base(context)
        {
            _productContext = context;
        }

        public async Task<ProductEntity> GetProductInfoById (Guid productId)
        {
            return await _productContext.Product
                .Include(x => x.ProductType)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<ProductEntity> GetProductInfoByName(string productName)
        {
            return await _productContext.Product
                .Include(x => x.ProductType)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
