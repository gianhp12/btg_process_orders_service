using System.Text;
using btg_process_orders_service.Infra.Queue.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace btg_process_orders_service.Infra.Queue.Adapters;

public class RabbitMQAdapter : IMessageQueue
{
    private IConnection Connection { get; set; } = null!;
    private IChannel Channel { get; set; } = null!;
    private readonly IConfigurationSection QueueSettings;

    public RabbitMQAdapter(IQueueConfiguration configuration)
    {
        QueueSettings = configuration.Value;
    }
    public async Task CreateConnection()
    {
        var host = QueueSettings.GetValue<string>("Host")!;
        var port = QueueSettings.GetValue<int>("Port");
        var userName = QueueSettings.GetValue<string>("Username")!;
        var password = QueueSettings.GetValue<string>("Password")!;
        var connFactory = new ConnectionFactory { HostName = host, Port = port, UserName = userName, Password = password, AutomaticRecoveryEnabled = true };
        var connection = await connFactory.CreateConnectionAsync();
        Connection = connection;
    }
    public async Task CreateChannel() { Channel = await Connection.CreateChannelAsync(); }

    public async Task SendMessage(QueueMessageDto dto)
    {
        var message = dto.Message;
        if (message is not string) message = JsonConvert.SerializeObject(dto.Message);
        var body = Encoding.UTF8.GetBytes((string)message);
        var props = dto.Props ?? new BasicProperties();
        await Channel.BasicPublishAsync(exchange: dto.Exchange, routingKey: dto.RoutingKey, mandatory: true, basicProperties: props, body: body);
    }

    public async Task AddConsumerListener(string queueName, AsyncEventHandler<BasicDeliverEventArgs> onListener)
    {
        var consumerChannel = await Connection.CreateChannelAsync();
        await consumerChannel.QueueDeclarePassiveAsync(queue: queueName);
        var consumer = new AsyncEventingBasicConsumer(consumerChannel);
        consumer.ReceivedAsync += onListener;
        await consumerChannel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer);
    }
}