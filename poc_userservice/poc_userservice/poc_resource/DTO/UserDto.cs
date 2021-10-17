using System;
using System.Collections.Generic;
using System.Text;

namespace poc_resource.DTO
{
    public class UserDto: BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public RolesDto Role { get; set; }
    }
}
