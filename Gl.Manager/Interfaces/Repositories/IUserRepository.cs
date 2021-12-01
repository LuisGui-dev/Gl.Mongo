using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;

namespace Gl.Manager.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetAsync(string login);
        Task<User> InsertAsync(User user);
        Task<User> UpdateAsync(User user, string login);
    }
}