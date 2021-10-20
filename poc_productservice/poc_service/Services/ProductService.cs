using AutoMapper;
using poc_common.DTO;
using poc_resource.Repositories.IRepository;
using poc_service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace poc_service.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProduct()
        {
            var products = await _productRepository.GetAll();

            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
