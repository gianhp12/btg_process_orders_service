using btg_process_orders_service.Infra.Extensions;

namespace btg_process_orders_service.Infra.NoSql.Configuration;

public class MongoDbConfiguration : INoSqlConfiguration
{
    public IConfiguration Value { get; private set; }
    public string Environment { get; private set; }

    public MongoDbConfiguration(IConfiguration conf)
    {
        Value = conf;
        Environment = conf.GetEnvironmentString();
    }
}
