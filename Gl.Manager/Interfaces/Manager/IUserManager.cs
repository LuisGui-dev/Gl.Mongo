using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.User;

namespace Gl.Manager.Interfaces.Manager
{
    public interface IUserManager
    {
        Task<IEnumerable<UserView>> GetAsync();
        Task<UserView> GetAsync(string login);
        Task<UserView> InsertAsync(NewUser newUser);
        Task<UserView> UpdateUserAsync(User user, string login);
        Task<UserLogin> ValidPasswordAndGenereteToken(User user);
    }
}