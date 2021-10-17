using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using poc_database.Entities;
using poc_resource.Repositories;
using poc_resource.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace poc_service.services
{
    public class ProfileService : IProfileService
    {
        private IUserRepository _userRepository;
        private ILogger<ProfileService> _logger;

        public ProfileService(
            IUserRepository userRepository,
            ILogger<ProfileService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //get user from db (in my case this is by email)
                var user = await _userRepository.GetUsersByName(context.Subject.Identity.Name);

                if (user != null)
                {
                    var claims = GetUserClaims(user);

                    //set issued claims to return
                    context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                {
                    var user = await _userRepository.GetUsersByName(userId.Value);

                    if (user != null)
                    {
                        context.IsActive = true;
                    }
                }
            }
            catch (Exception ex)
            {
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
