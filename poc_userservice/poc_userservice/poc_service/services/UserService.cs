using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using poc_common.Configurations;
using poc_common.Helper;
using poc_database.Entities;
using poc_resource.DTO;
using poc_resource.Repositories.IRepository;
using poc_service.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace poc_service.services
{
    public class UserService: IUserService
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        private Configurations.Encrypt encryptConfig;
        private ILogger logger;

        public UserService(IUserRepository _userRepository, 
            IMapper _mapper, 
            IOptions<Configurations.Encrypt> _encryptConfig,
            ILogger<UserService> _logger) 
        {
            userRepository = _userRepository;
            mapper = _mapper;
            encryptConfig = _encryptConfig.Value;
            logger = _logger;
        }

        #region Public Functions
        public async Task<int> AddNewUserAsync(UserDto user)
        {
            logger.LogInformation("Service start.");

            var userExists = await CheckIfUserExists(user.Id);

            if (userExists == null)
            {
                Users userEntity = mapper.Map<UserDto, Users>(user);
                userEntity.CreatedBy = "";
                userEntity.CreatedDate = DateTime.Now;
                userEntity.UpdatedBy = "";
                userEntity.UpdatedDate = DateTime.Now;
                userEntity.Password = Encryption.EncryptPassword(userEntity.Password, encryptConfig.Salt);

                userRepository.Add(userEntity);
                logger.LogInformation("Inserted successfully.");

                return await userRepository.SaveChangesAsync();
            }
            
            throw new Exception("Can't not insert this user");
        }

        public async Task<int> DeleteUserAsync(string id)
        {
            var userExists = await CheckIfUserExists(Guid.Parse(id));
            if (userExists != null)
            {
                var user = mapper.Map<UserDto, Users>(userExists);
                userRepository.Delete(user);
                return await userRepository.SaveChangesAsync();
            }

            throw new Exception("Can't not delete this user");
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await userRepository.GetById(userId);
            var result = mapper.Map<Users, UserDto>(user);

            return result;
        }

        public async Task<UserDto> GetUserByName(string userName)
        {
            var user = await userRepository.GetUsersByName(userName);
            var result = mapper.Map<Users, UserDto>(user);

            return result;
        }

        public async Task<int> ChangePassword (ChangePasswordDto changePassword)
        {
            var encryptCurrentPass = Encryption.EncryptPassword(changePassword.CurrentPassword, encryptConfig.Salt);
            var encryptNewPass = Encryption.EncryptPassword(changePassword.NewPassword, encryptConfig.Salt);
            var user = await userRepository.GetById(changePassword.UserId.ToString());

            if(user != null && user.Password == encryptCurrentPass)
            {
                user.Password = encryptNewPass;

                userRepository.Update(user);
                return await userRepository.SaveChangesAsync();
            }

            throw new Exception("Can not change password of this user");
        }

        public async Task<UserDto> VerifyUserAccount(string userName, string password)
        {
            var encryptCurrentPass = Encryption.EncryptPassword(password, encryptConfig.Salt);
            var user = await userRepository.VerifyUser(userName, encryptCurrentPass);
            return mapper.Map<Users, UserDto>(user); ;
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            var user = await userRepository.GetAll();

            return mapper.Map<List<Users>, List<UserDto>>(user);
        }
        #endregion

        #region Private Functions
        private async Task<UserDto> CheckIfUserExists(Guid userId)
        {
            var user = await userRepository.GetById(userId.ToString());

            return mapper.Map<Users, UserDto>(user);
        }
        #endregion

    }
}
