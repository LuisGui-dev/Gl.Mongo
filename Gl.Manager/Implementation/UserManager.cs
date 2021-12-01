using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.User;
using Gl.Manager.Interfaces.Manager;
using Gl.Manager.Interfaces.Repositories;
using Gl.Manager.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Gl.Manager.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserManager(IUserRepository repository, IMapper mapper, IJwtService jwtService)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<UserView>> GetAsync()
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserView>>(await _repository.GetAsync());
        }

        public async Task<UserView> GetAsync(string login)
        {
            return _mapper.Map<UserView>(await _repository.GetAsync(login));
        }

        public async Task<UserView> InsertAsync(NewUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            ConvertPasswordIndHash(user);
            return _mapper.Map<UserView>(await _repository.InsertAsync(user));
        }

        private void ConvertPasswordIndHash(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
        }
        
        public async Task<UserView> UpdateUserAsync(User user, string login)
        {
            ConvertPasswordIndHash(user);
            return _mapper.Map<UserView>(await _repository.UpdateAsync(user, login));
        }

        public async Task<UserLogin> ValidPasswordAndGenereteToken(User user)
        {
            var userObj = await _repository.GetAsync(user.Login);
            if (userObj == null)
                return null;
            if (!await ValidHashAsync(user, userObj.Password)) return null;
            var userLogin = _mapper.Map<UserLogin>(userObj);
            userLogin.Token = _jwtService.GenerateToken(userObj);
            return userLogin;
        }

        private async Task<bool> ValidHashAsync(User user, string hash)
        {
            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(user, hash, user.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.Success:
                    return true;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdateUserAsync(user, hash);
                    return true;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}