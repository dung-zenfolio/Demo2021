using poc_resource.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poc_userservice.Models.Response
{
    public class UserResponseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public RolesDto Role { get; set; }
    }
}
