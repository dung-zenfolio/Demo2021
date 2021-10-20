using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poc_common.DTO;
using poc_search.Index;
using poc_search.Interfaces;
using poc_service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poc_productservice.Controllers
{
    [Route("api/product")]
    [ApiController]
    // [Authorize]
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ISearchService _searchService;
        private IMapper _mapper;
        private ILogger _logger;

        public ProductController(
            IProductService productService,
            ISearchService searchService,
            IMapper mapper,
            ILogger<ProductController> logger
            )
        {
            _productService = productService;
            _searchService = searchService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductDto>>> GetProductsAll()
        {
            var result = await _productService.GetAllProduct();

            return Ok(result);
        }

        /// <summary>
        /// Migrate product data to ElasticSearch
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("migrate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductDto>>> MigrateToElasticSearch()
        {
            try
            {
                var products = await _productService.GetAllProduct();

                var esProducts = _mapper.Map<List<ProductIndex>>(products);

                foreach (var esProduct in esProducts)
                {
                    await _searchService.CreateOrUpdateIndex(esProduct);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Get all products in Elastic Search.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getalles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductIndex>>> GetProductsAllInES()
        {
            var result = await _searchService.GetAllProducts();

            return Ok(result);
        }
    }
}
