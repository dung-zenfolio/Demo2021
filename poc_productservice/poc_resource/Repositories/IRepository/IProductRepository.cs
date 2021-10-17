using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace poc_resource.Repositories.IRepository
{
    public interface IProductRepository:IRepository<ProductEntity>
    {
        Task<ProductEntity> GetProductInfoById(Guid productId);
        Task<ProductEntity> GetProductInfoByName(string productName);
    }
}
