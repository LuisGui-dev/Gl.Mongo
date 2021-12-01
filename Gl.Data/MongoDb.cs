using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Gl.Data
{
    public class MongoDb
    {
        public IMongoDatabase Db { get; set; }

        public MongoDb(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration["ConnectionString"]);
                Db = client.GetDatabase(configuration["DatabaseName"]);
            }
            catch (Exception ex)
            {
                throw new MongoException("Não foi possivel se conectar ao MongoDb", ex);
            }
        }
    }
}