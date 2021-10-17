using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poc_userservice
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "userApi",
                    DisplayName = "User API",
                    Description = "Allow the application to access User Api",
                    Scopes = new List<string> { "userApi.read", "userApi.write"},
                    ApiSecrets = new List<Secret> {new Secret("userApi".Sha256())},
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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("aabc95ea-8de7-46d4-ac90-7043dcc6adbb".Sha256())
                    },

                    AllowOfflineAccess = true,
                    // scopes that client has access to
                    AllowedScopes = { "userApi.read" }
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
                new ApiScope("userApi.read", "Read Access to User API"),
                new ApiScope("userApi.write", "Write Access to User API")
            };
        }
    }
}
