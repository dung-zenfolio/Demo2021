using AutoMapper;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poc_service.services.Interfaces;
using poc_userservice.Models.Request;
using System;
using System.Threading.Tasks;

namespace poc_userservice.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        private ILogger _logger;

        public AccountController(IMapper mapper,
            IUserService userService,
            ILogger<UserController> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AccountLogin(LoginRequestModel request)
        {
            if (request == null || 
                string.IsNullOrEmpty(request.UserName) ||
                string.IsNullOrEmpty(request.Password))
            {
                return NotFound();
            }

            var result = await _userService.VerifyUserAccount(request.UserName, request.Password);
            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.Add(new TimeSpan(3, 0, 0, 0))
            };

            var isuser = new IdentityServerUser(result.UserName)
            {
                DisplayName = result.UserName
            };

            await HttpContext.SignInAsync(isuser, props);

            return NoContent();
        }
    }
}
