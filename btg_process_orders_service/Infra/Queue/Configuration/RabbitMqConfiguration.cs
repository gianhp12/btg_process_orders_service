using btg_process_orders_service.Infra.Extensions;

namespace btg_process_orders_service.Infra.Queue.Configuration;

public class RabbitMqConfiguration : IQueueConfiguration
{
 public IConfigurationSection Value { get; private set; }

  public RabbitMqConfiguration(IConfiguration conf) => Value = conf.GetEnvironmentSettings("MessageQueue");
}
