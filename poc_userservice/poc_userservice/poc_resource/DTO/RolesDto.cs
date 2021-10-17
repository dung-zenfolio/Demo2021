using System.Collections.Generic;

namespace poc_resource.DTO
{
    public class RolesDto
    {
        public string RoleName { get; set; }
        public int RoleNumber { get; set; }
        public IList<UserDto> Users { get; set; }
    }
}
