using System.Collections.Generic;
using System.Threading.Tasks;
using Gl.Core.Domain;
using Gl.Manager.Interfaces.Repositories;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Gl.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(MongoDb mongoDb)
        {
            var camelCaseConvention = new ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("CamelCase", camelCaseConvention, _ => true);
            _userCollection = mongoDb.Db.GetCollection<User>("users");
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _userCollection.Find(x => true).ToListAsync();
        }

        public async Task<User> GetAsync(string id)
        {
            return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> InsertAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateAsync(User user, string id)
        {
            var condition = Builders<User>.Filter.Eq(x => x.Id, id);
            await _userCollection.ReplaceOneAsync(condition, user);
            return user;
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if(user is null) return;
            await _userCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}