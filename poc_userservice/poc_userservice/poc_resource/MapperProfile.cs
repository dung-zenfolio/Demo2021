using AutoMapper;
using poc_database.Entities;
using poc_resource.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_resource
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Roles, RolesDto>().ReverseMap();
            CreateMap<Users, UserDto>().ReverseMap();
        }
    }
}
