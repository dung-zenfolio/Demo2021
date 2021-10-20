using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using poc_resource.DTO;
using poc_service.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace poc_userservice
{
    public class Config
    {
        private static IUserService _userService;

        public Config(
            IUserService userService)
        {
            _userService = userService;
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "productApi",
                    DisplayName = "Product API",
                    Description = "Allow the application to access Product Api",
                    Scopes = new List<string> { "productApi.read", "productApi.write"},
                    ApiSecrets = new List<Secret> {new Secret("productApi".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "f2c517cd-1cb3-483c-a06e-4f95854fa2f9",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("aabc95ea-8de7-46d4-ac90-7043dcc6adbb".Sha256())
                    },

                    AllowOfflineAccess = true,
                    // scopes that client has access to
                    AllowedScopes = { "productApi.read" }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
                {
                new ApiScope("productApi.read", "Read Access to User API"),
                new ApiScope("productApi.write", "Write Access to User API")
            };
        }

        public static List<TestUser> GetUsers()
        {
            var users = _userService.GetAllUser().Result;

            List<TestUser> result = new List<TestUser>();
            foreach(var user in users)
            {
                result.Add(new TestUser
                {
                     Username = user.UserName,
                     Password = user.Password,
                     Claims = new List<Claim>
                     {
                         new Claim(JwtClaimTypes.Id, user.UserName),
                         new Claim(JwtClaimTypes.Role, user.Role.RoleName)
                     }
                });
            }

            return result;
        }
    }
}
