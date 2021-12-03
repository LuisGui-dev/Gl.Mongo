using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelInput.User;
using Gl.Core.Shared.ModelViews.User;

namespace Gl.Manager.Interfaces.Manager
{
    public interface IUserManager
    {
        Task<IEnumerable<UserView>> GetAsync();
        Task<UserView> GetAsync(string id);
        Task<UserLoginView> GetByEmailAsync(string email);
        Task<UserView> InsertAsync(NewUser newUser);
        Task<UserView> UpdateAsync(NewUser newUser, string id);
        Task DeleteAsync(string id);
        Task<UserLoginView> ValidPasswordAndGenereteToken(UserLogin user);
    }
}