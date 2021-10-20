using poc_common.DTO;
using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace poc_resource.Repositories.IRepository
{
    public interface IProductRepository:IRepository<ProductEntity>
    {
        Task<ProductDto> GetProductInfoById(Guid productId);
        Task<ProductDto> GetProductInfoByName(string productName);
    }
}
