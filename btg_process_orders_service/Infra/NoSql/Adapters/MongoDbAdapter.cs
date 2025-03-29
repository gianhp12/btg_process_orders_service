using MongoDB.Driver;
using btg_process_orders_service.Infra.NoSql.Dtos;

namespace btg_process_orders_service.Infra.NoSql.Adapters
{
    public class MongoDbAdapter : INoSqlDatabase
    {
        private readonly IConfiguration _conf;
        private readonly string _environment;
        private readonly IMongoClient _mongoClient;

        public MongoDbAdapter(INoSqlConfiguration configuration)
        {
            _conf = configuration.Value;
            _environment = configuration.Environment;

            var connectionString = GetConnectionString("MongoDB");
            _mongoClient = new MongoClient(connectionString);
        }

        private string GetConnectionString(string database)
        {
            if (_environment == "Development")
                return _conf.GetConnectionString($"{database}_DEV")!;
            if (_environment == "Staging")
                return _conf.GetConnectionString($"{database}_STAGING")!;
            if (_environment == "Production")
                return _conf.GetConnectionString($"{database}_PROD")!;
            throw new Exception("Unable to get MongoDB connection string");
        }

        public async Task InsertDocumentAsync<T>(MongoDbQueryDto<T> queryDto)
        {
            var database = _mongoClient.GetDatabase(queryDto.Database);
            var collection = database.GetCollection<T>(queryDto.CollectionName);
            if (queryDto.Document != null)
            {
                await collection.InsertOneAsync(queryDto.Document);
            }
        }

        public async Task<List<T>> ExecuteQueryAsync<T>(MongoDbQueryDto<T> queryDto)
        {
            var database = _mongoClient.GetDatabase(queryDto.Database);
            var collection = database.GetCollection<T>(queryDto.CollectionName);
            if (queryDto.Filter != null)
            {
                return await collection.Find(queryDto.Filter).ToListAsync();
            }
            return new List<T>();
        }

        public async Task UpdateDocumentAsync<T>(MongoDbQueryDto<T> queryDto)
        {
            var database = _mongoClient.GetDatabase(queryDto.Database);
            var collection = database.GetCollection<T>(queryDto.CollectionName);
            if (queryDto.Filter != null && queryDto.Update != null)
            {
                await collection.UpdateOneAsync(queryDto.Filter, queryDto.Update);
            }
        }

        public async Task DeleteDocumentAsync<T>(MongoDbQueryDto<T> queryDto)
        {
            var database = _mongoClient.GetDatabase(queryDto.Database);
            var collection = database.GetCollection<T>(queryDto.CollectionName);
            if (queryDto.Filter != null)
            {
                await collection.DeleteOneAsync(queryDto.Filter);
            }
        }
    }
}
