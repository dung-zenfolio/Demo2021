using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using poc_database.Entities;
using poc_resource.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace poc_service.Validator
{
    public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        //repository to get user from db
        private readonly IUserRepository _userRepository;
        private ILogger<ResourceOwnerPasswordValidator> _logger;

        public ResourceOwnerPasswordValidator(
            IUserRepository userRepository,
            ILogger<ResourceOwnerPasswordValidator> logger)
        {
            _userRepository = userRepository; //DI
            _logger = logger;
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var user = await _userRepository.GetUsersByName(context.UserName);
                if (user != null)
                {
                    //check if password match - remember to hash password if stored as hash in db
                    if (user.Password == context.Password)
                    {
                        //set the result
                        context.Result = new GrantValidationResult(
                            subject: user.UserName,
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                _logger.LogError(ex.Message);
            }
        }

        private List<Claim> GetUserClaims(Users user)
        {
            return new List<Claim>
            {
                new Claim("user_id", user.UserName),
                new Claim(JwtClaimTypes.Role, user.Role?.RoleName)
            };
        }
    }
}
