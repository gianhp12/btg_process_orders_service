namespace btg_process_orders_service.Infra.Queue;

public interface IQueueConfiguration
{
    public IConfigurationSection Value { get; }
}
