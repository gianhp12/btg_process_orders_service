using btg_process_orders_service.Application.Controllers;
using btg_process_orders_service.Infra.Di;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
DependecyInjection.Inject(builder.Services);

var host = builder.Build();
host.Run();
