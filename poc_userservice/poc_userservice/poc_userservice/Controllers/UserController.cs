using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poc_resource.DTO;
using poc_service.services.Interfaces;
using poc_userservice.Models.Request;
using poc_userservice.Models.Response;

namespace poc_userservice.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        private ILogger _logger;

        public UserController(IMapper mapper, 
            IUserService userService,
            ILogger<UserController> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<List<UserResponseModel>> GetAllUser()
        {
            var users = await _userService.GetAllUser();
            var result = _mapper.Map<List<UserDto>, List<UserResponseModel>>(users);

            return result;
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<UserResponseModel> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);

            return _mapper.Map<UserDto, UserResponseModel>(user);
        }

        [HttpGet]
        [Route("getbyname/{username}")]
        public async Task<UserResponseModel> GetUserByUserName(string username)
        {
            var user = await _userService.GetUserById(username);

            return _mapper.Map<UserDto, UserResponseModel>(user);
        }

        [HttpPost]
        
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> AddNewUser(UserRequestModel user)
        {
            _logger.LogInformation("Start insert new User");
            _logger.LogInformation("User info: ", user);

            var userDto = _mapper.Map<UserRequestModel, UserDto>(user);

            await _userService.AddNewUserAsync(userDto);
            
            return Accepted();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation("Start delete user");
            _logger.LogInformation("User Id: ", id);

            try
            {
                await _userService.DeleteUserAsync(id);
                return Accepted();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
