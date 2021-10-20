using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Seed
{
    public static class ProductSeedDataInit
    {
        public static void Seed(MigrationBuilder builder)
        {
            builder.InsertData
                (
                    "ProductType",
                    new [] 
                    {
                        "Id",
                        "TypeName",
                        "Rank",
                        "CreatedDate",
                        "UpdatedDate"
                    },
                    new object[,]
                    {
                        {
                            Guid.Parse("d6b7fd17-76f5-4658-ba55-8b7790f43d63"),
                            "Type 1",
                            1,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        {
                            Guid.Parse("37b4db1b-33a3-44de-95af-055a01b8beb5"),
                            "Type 2",
                            1,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        }
                    }
                );

            builder.InsertData
                (
                    "Product",
                    new [] 
                    {
                        "Id", 
                        "ProductName", 
                        "Description", 
                        "ProductTypeId", 
                        "Price", 
                        "ProductQuantity",
                        "CreatedDate",
                        "UpdatedDate"
                    },
                    new object[,]
                    {
                        { 
                            Guid.Parse("11b195dd-fc26-4e97-9edb-f419f345ec81"),
                            "Product 1",
                            "Description 1",
                            Guid.Parse("d6b7fd17-76f5-4658-ba55-8b7790f43d63"),
                            12345,
                            22,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        },
                        {
                            Guid.Parse("8f71caf2-4e75-4973-ac84-7538dc1ad914"),
                            "Product 2",
                            "Description 2",
                            Guid.Parse("37b4db1b-33a3-44de-95af-055a01b8beb5"),
                            12345,
                            22,
                            DateTime.UtcNow,
                            DateTime.UtcNow
                        }
                    }
                );
        }
    }
}
