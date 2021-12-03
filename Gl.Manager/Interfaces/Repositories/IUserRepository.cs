using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.User;

namespace Gl.Manager.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<User> InsertAsync(User user);
        Task<User> UpdateAsync(User user, string id);
        Task DeleteAsync(string id);
    }
}