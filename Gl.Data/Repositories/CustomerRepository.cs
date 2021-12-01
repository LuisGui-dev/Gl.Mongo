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
            var condition = Builders<Customer>.Filter.Eq(x => x.Id, id);
            await _customerCollection.ReplaceOneAsync(condition, customer);
            return customer;
        }

        public async Task DeleteAsync(string id)
        {
            var customerObj = await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (customerObj == null) return;
            await _customerCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}