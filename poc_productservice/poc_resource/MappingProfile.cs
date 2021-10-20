using AutoMapper;
using poc_common.DTO;
using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_resource
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductEntity, ProductDto>();
        }
    }
}
