using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poc_userservice.Models.Request
{
    public class UserRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
