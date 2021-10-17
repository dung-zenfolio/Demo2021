using System;
using System.Collections.Generic;
using System.Text;

namespace poc_database.Entities
{
    public class Roles: BaseEntities
    {
        public string RoleName { get; set; }
        public int RoleNumber { get; set; }
        public IList<Users> Users { get; set; }
    }
}
