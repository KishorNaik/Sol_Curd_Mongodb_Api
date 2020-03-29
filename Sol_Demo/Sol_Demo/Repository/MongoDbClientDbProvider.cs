using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Repository
{
    public interface IMongoDbClientDbProvider
    {
        IMongoDatabase GetConnectionWithDatabase { get; }
    }


    public class MongoDbClientDbProvider : IMongoDbClientDbProvider
    {
      
        private readonly IMongoDatabase mongoDatabase = null;
        
        public MongoDbClientDbProvider(string connectionString,string database)
        {
            var client = new MongoClient(connectionString);
            if (client != null)
            {
               mongoDatabase= client.GetDatabase(database);
            }
        }


        IMongoDatabase IMongoDbClientDbProvider.GetConnectionWithDatabase => mongoDatabase;
    }
}
