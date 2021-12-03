using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelInput.User;
using Gl.Core.Shared.ModelViews.User;
using Gl.Manager.Interfaces.Manager;
using Gl.Manager.Interfaces.Repositories;
using Gl.Manager.Interfaces.Services;

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

        public async Task<UserView> GetAsync(string id)
        {
            return _mapper.Map<UserView>(await _repository.GetAsync(id));
        }

        public async Task<UserLoginView> GetByEmailAsync(string email)
        {
            return _mapper.Map<UserLoginView>(await _repository.GetByEmailAsync(email));
        }

        public async Task<UserView> InsertAsync(NewUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Password = EncryptPassword(user.Password);
            return _mapper.Map<UserView>(await _repository.InsertAsync(user));
        }
        
        public async Task<UserView> UpdateAsync(NewUser newUser, string id)
        {
            newUser.Password = EncryptPassword(newUser.Password);
            var user = _mapper.Map<User>(newUser);
            return _mapper.Map<UserView>(await _repository.UpdateAsync(user, id));
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<UserLoginView> ValidPasswordAndGenereteToken(UserLogin user)
        {
            var userObj = await _repository.GetByEmailAsync(user.Email);
            if (userObj == null)
                return null;
            user.Password = EncryptPassword(user.Password);
            if (user.Password != userObj.Password) return null;
            var userLogin = _mapper.Map<UserLoginView>(userObj);
            userLogin.Token = _jwtService.GenerateToken(userObj);
            return userLogin;
        }
        
        private string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass + "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}