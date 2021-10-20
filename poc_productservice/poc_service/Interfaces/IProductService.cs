using poc_common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace poc_service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProduct();
    }
}
