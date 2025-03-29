using System.Text;
using btg_process_orders_service.Infra.Queue.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace btg_process_orders_service.Infra.Queue;

public interface IMessageQueue
{
    public Task CreateConnection();
    public Task CreateChannel();
    public Task SendMessage(QueueMessageDto dto);
    public Task AddConsumerListener(string queueName, AsyncEventHandler<BasicDeliverEventArgs> onListener);
}

