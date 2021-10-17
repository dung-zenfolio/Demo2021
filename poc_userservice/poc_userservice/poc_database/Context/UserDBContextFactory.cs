using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_database.Context
{
    public class UserDBContextFactory: IDesignTimeDbContextFactory<UserDBContext>
    {
        public UserDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDBContext>();

            return new UserDBContext(optionsBuilder.Options);
        }
    }
}
