namespace btg_process_orders_service.Infra.Queue.Configuration;

public interface IQueueConfiguration
{
    public IConfigurationSection Value { get; }
}
