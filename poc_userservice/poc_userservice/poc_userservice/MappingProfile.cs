using poc_resource;
using poc_resource.DTO;
using poc_userservice.Models.Request;
using poc_userservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poc_userservice
{
    public class MappingProfile: MapperProfile
    {
        public MappingProfile() : base()
        {
            CreateMap<UserDto, UserResponseModel>();
            CreateMap<RolesDto, RoleRequestModel>();

            CreateMap<UserRequestModel, UserDto>();
            CreateMap<RoleRequestModel, RolesDto>();
        }
    }
}
