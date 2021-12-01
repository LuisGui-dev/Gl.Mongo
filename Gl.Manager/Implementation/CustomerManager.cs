using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.Customer;
using Gl.Manager.Interfaces.Manager;
using Gl.Manager.Interfaces.Repositories;

namespace Gl.Manager.Implementation
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerView>> GetAsync()
        {
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerView>>(await _repository.GetAsync());
        }

        public async Task<CustomerView> GetAsync(string id)
        {
            return _mapper.Map<CustomerView>(await _repository.GetAsync(id));
        }

        public async Task<CustomerView> InsertAsync(NewCustomer newCustomer)
        {
            var customer = _mapper.Map<Customer>(newCustomer);
            return _mapper.Map<CustomerView>(await _repository.InsertAsync(customer));
        }
        
        public async Task<CustomerView> UpdateAsync(EditCustomer editCustomer, string id)
        {
            var customer = _mapper.Map<Customer>(editCustomer);
            return _mapper.Map<CustomerView>(await _repository.UpdateAsync(customer, id));
        }
        
        public async Task DeleteAsync(string id)
        { 
            await _repository.DeleteAsync(id);
        }
    }
}