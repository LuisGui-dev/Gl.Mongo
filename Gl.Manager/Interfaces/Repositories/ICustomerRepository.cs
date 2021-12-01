using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;

namespace Gl.Manager.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAsync();
        Task<Customer> GetAsync(string id);
        Task<Customer> InsertAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer, string id);
        Task DeleteAsync(string id);
    }
}