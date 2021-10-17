using Microsoft.EntityFrameworkCore.Migrations;
using poc_common.Helper;
using poc_database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_database.Seed
{
    public static class UserDataInitialize
    {
        public static void Seed(MigrationBuilder builder)
        {
            builder.InsertData
            (
                nameof(Roles),
                new[] {"Id", "RoleName", "RoleNumber", "CreatedDate", "UpdatedDate" },
                new object[,]
                {
                    {Guid.Parse("58a7f97a-d0a0-4f67-bcfc-2251860b01f3"), "Admin", 1, DateTime.UtcNow, DateTime.UtcNow },
                    {Guid.Parse("3e9b21ad-01c5-4a85-bb14-290887c3bb63"), "Client", 2, DateTime.UtcNow, DateTime.UtcNow }
                }
            );

            builder.InsertData
            (
                nameof(Users),
                new[] {"Id", "UserName", "Password", "RoleId", "CreatedDate", "UpdatedDate" },
                new object[,]
                {
                    {Guid.Parse("e6f9c626-7f5d-4be7-b2e6-c992c817c5eb"), "admin", Encryption.EncryptPassword("admin@123", "poc_userservice"), Guid.Parse("58a7f97a-d0a0-4f67-bcfc-2251860b01f3"), DateTime.UtcNow, DateTime.UtcNow}
                }
            );
        }
    }
}
