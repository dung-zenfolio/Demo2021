using AutoMapper;
using Microsoft.EntityFrameworkCore;
using poc_common.DTO;
using poc_productdatabase.Context;
using poc_productdatabase.Entities;
using poc_resource.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace poc_resource.Repositories
{
    public class ProductRepository : Repository<ProductEntity, ProductContext>, IProductRepository
    {
        private ProductContext _productContext;
        private IMapper _mapper;

        public ProductRepository(ProductContext context, IMapper mapper): base(context)
        {
            _productContext = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductInfoById (Guid productId)
        {
            var product = await _productContext.Product
                .Include(x => x.ProductType)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == productId);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProductInfoByName(string productName)
        {
            var product = await _productContext.Product
                .Include(x => x.ProductType)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));

            return _mapper.Map<ProductDto>(product);
        }
    }
}
