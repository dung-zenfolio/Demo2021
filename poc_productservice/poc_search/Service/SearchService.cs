using Microsoft.Extensions.Logging;
using poc_search.Index;
using poc_search.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace poc_search.Service
{
    public class SearchService : ISearchService
    {
        private IConnection _connection;
        private ILogger _logger;
        public SearchService(
            IConnection connection,
            ILogger<SearchService> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task CreateOrUpdateIndex(ProductIndex index)
        {
            try
            {
                _connection.OpenConnection();
                var result = await _connection._client.IndexDocumentAsync(index);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<List<ProductIndex>> GetAllProducts()
        {
            _connection.OpenConnection();
            var products = await _connection._client.SearchAsync<ProductIndex>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.ProductName)
                        .Query("Product 1")
                    )
                ));

            return (List<ProductIndex>)products.Documents;
        }
    }
}
