using btg_process_orders_service.Infra.NoSql;
using btg_process_orders_service.Infra.NoSql.Adapters;
using btg_process_orders_service.Infra.NoSql.Configuration;
using btg_process_orders_service.Infra.Queue;
using btg_process_orders_service.Infra.Queue.Adapters;
using btg_process_orders_service.Infra.Queue.Configuration;
using btg_process_orders_service.Services;

namespace btg_process_orders_service.Infra.Di;

public class DependecyInjection
{
    public static void Inject(IServiceCollection services)
    {
        services.AddSingleton<INoSqlConfiguration, MongoDbConfiguration>();
        services.AddSingleton<IQueueConfiguration, RabbitMqConfiguration>();
        services.AddSingleton<INoSqlDatabase, MongoDbAdapter>();
        services.AddSingleton<IMessageQueue, RabbitMQAdapter>();
        services.AddTransient<IProcessOrderService, ProcessOrderService>();
    }
}
