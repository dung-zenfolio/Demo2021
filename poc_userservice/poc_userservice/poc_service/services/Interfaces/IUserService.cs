using poc_resource.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace poc_service.services.Interfaces
{
    public interface IUserService
    {
        Task<int> AddNewUserAsync(UserDto user);
        Task<int> DeleteUserAsync(string id);
        Task<UserDto> GetUserById(string userId);
        Task<UserDto> GetUserByName(string userName);
        Task<int> ChangePassword(ChangePasswordDto changePassword);
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> VerifyUserAccount(string userName, string password);

    }
}
