using System;
using System.Collections.Generic;
using System.Text;

namespace poc_database.Entities
{
    public class Users: BaseEntities
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
        public Roles Role { get; set; }
    }
}
