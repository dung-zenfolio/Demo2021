using poc_search.Index;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace poc_search.Interfaces
{
    public interface ISearchService
    {
        Task CreateOrUpdateIndex(ProductIndex index);
        Task<List<ProductIndex>> GetAllProducts();
    }
}
