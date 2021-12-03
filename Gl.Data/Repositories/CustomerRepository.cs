using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;
using Gl.Manager.Interfaces.Repositories;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Gl.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerRepository(MongoDb mongoDb)
        {
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConvention, _ => true);
            _customerCollection = mongoDb.Db.GetCollection<Customer>("customers");
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await _customerCollection.Find(x => true).ToListAsync();
        }

        public async Task<Customer> GetAsync(string id)
        {
            return await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> InsertAsync(Customer customer)
        {
            await _customerCollection.InsertOneAsync(customer);
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer, string id)
        {
            var filter = Builders<Customer>.Filter.Eq(s => s.Id, id);
            var update = Builders<Customer>.Update
                .Set(s => s.Name, customer.Name)
                .Set(s => s.Email, customer.Email)
                .Set(s => s.Phone, customer.Phone)
                .Set(s => s.IsActive, customer.IsActive)
                .CurrentDate(s => s.UpdateOn);
            await _customerCollection.UpdateOneAsync(filter, update);
            return customer;
        }

        public async Task DeleteAsync(string id)
        {
            var customer = await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (customer == null) return;
            await _customerCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}