using poc_database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace poc_resource.Repositories.IRepository
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users> GetUsersByName(string userName);
        Task<Users> VerifyUser(string userName, string password);
    }
}
