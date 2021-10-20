using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using poc_common.DTO;
using poc_resource;
using poc_search.Index;

namespace poc_productservice
{
    public class MappingProfile : poc_resource.MappingProfile
    {
        public MappingProfile() : base()
        {
            CreateMap<ProductDto, ProductIndex>();
        }
    }
}
