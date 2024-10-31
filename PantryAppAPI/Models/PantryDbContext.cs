using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace PantryAppAPI.Models
{
    public class PantryDbContext
    {
        private readonly IMongoDatabase _database;

        public PantryDbContext(IConfiguration config)
        {
            // Retrieve MongoDB connection settings from appsettings.json
            var connectionString = config.GetSection("MongoDB:ConnectionString").Value;
            var databaseName = config.GetSection("MongoDB:DatabaseName").Value;
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Define a collection for PantryItems
        public IMongoCollection<PantryItem> PantryItems => _database.GetCollection<PantryItem>("PantryItems");
    }
}
