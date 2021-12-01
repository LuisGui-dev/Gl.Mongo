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

        public async Task<User> GetAsync(string login)
        {
            return await _userCollection.Find(x => x.Login == login).FirstOrDefaultAsync();
        }

        public async Task<User> InsertAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateAsync(User user, string login)
        {
            var condition = Builders<User>.Filter.Eq(x => x.Login, login);
            await _userCollection.ReplaceOneAsync(condition, user);
            return user;
        }
    }
}