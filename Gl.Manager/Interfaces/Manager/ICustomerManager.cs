using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.Customer;

namespace Gl.Manager.Interfaces.Manager
{
    public interface ICustomerManager
    {
        Task<IEnumerable<CustomerView>> GetAsync();
        Task<CustomerView> GetAsync(string id);
        Task<CustomerView> InsertAsync(NewCustomer newCustomer);
        
        Task<CustomerView> UpdateAsync(EditCustomer customer, string id);
        Task DeleteAsync(string id);
    }
}