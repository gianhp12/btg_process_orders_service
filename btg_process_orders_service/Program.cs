using btg_process_orders_service.Application.Controllers;
using btg_process_orders_service.Infra.NoSql;
using btg_process_orders_service.Infra.NoSql.Adapters;
using btg_process_orders_service.Infra.NoSql.Configuration;
using btg_process_orders_service.Infra.Queue;
using btg_process_orders_service.Infra.Queue.Adapters;
using btg_process_orders_service.Infra.Queue.Configuration;
using btg_process_orders_service.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<INoSqlConfiguration, MongoDbConfiguration>();
builder.Services.AddSingleton<IQueueConfiguration, RabbitMqConfiguration>();
builder.Services.AddSingleton<INoSqlDatabase, MongoDbAdapter>();
builder.Services.AddSingleton<IMessageQueue, RabbitMQAdapter>();
builder.Services.AddTransient<IProcessOrderService, ProcessOrderService>();
var host = builder.Build();
host.Run();
