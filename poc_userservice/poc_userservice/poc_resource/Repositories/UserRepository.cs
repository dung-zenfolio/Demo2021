using Microsoft.EntityFrameworkCore;
using poc_database.Context;
using poc_database.Entities;
using poc_resource.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_resource.Repositories
{
    public class UserRepository : Repository<Users, UserDBContext>, IUserRepository
    {
        private UserDBContext userContext;
        public UserRepository(UserDBContext context): base(context)
        {
            userContext = context;
        }

        public async Task<Users> GetUsersByName(string userName)
        {
            return await userContext.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<Users> VerifyUser(string userName, string password)
        {
            return await userContext.Users
                .FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
        }
    }
}
