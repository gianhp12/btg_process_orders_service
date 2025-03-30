using System.Text;
using btg_process_orders_service.Infra.Queue;
using btg_process_orders_service.Services;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace btg_process_orders_service.Application.Controllers;

public class Worker : BackgroundService
{
    private bool IsRunning { get; set; } = false;
    private readonly IMessageQueue MessageQueue;
    private readonly IProcessOrderService ProcessOrderService;

    public Worker(IMessageQueue messageQueue, IProcessOrderService processOrderService)
    {
        MessageQueue = messageQueue;
        ProcessOrderService = processOrderService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IsRunning = true;
        await MessageQueue.CreateConnection();
        await MessageQueue.AddConsumerListener(
                queueName: "btg-pactual-order-created",
                onListener: (sender, args) => Wrapper(sender, args, ProcessOrder)
            );
        while (IsRunning)
        {
            await Task.Delay(1000);
        }
    }

    private async Task Wrapper(object sender, BasicDeliverEventArgs args, Func<object, BasicDeliverEventArgs, string, Task> callback)
    {
        try
        {
            var channel = ((AsyncEventingBasicConsumer)sender).Channel;
            if (!IsRunning)
            {
                await channel.AbortAsync();
                return;
            }
            var traceId = $"{Guid.NewGuid()}-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(" ", "")}";
            await callback(sender, args, traceId);
            DateTime? conclusionDate = DateTime.Now;
        }
        catch (Exception)
        {
            IsRunning = false;
            Environment.Exit(1);
        }
    }

    private async Task ProcessOrder(object consumer, BasicDeliverEventArgs args, string traceId)
    {
        var channel = ((AsyncEventingBasicConsumer)consumer).Channel;
        var json = GetJsonFromMessageBytes(args.Body);
        await ProcessOrderService.Execute(json);
        await channel.BasicAckAsync(deliveryTag: args.DeliveryTag, multiple: false);
    }

    private static JToken GetJsonFromMessageBytes(ReadOnlyMemory<byte> message)
    {
        var bodyBytes = message.ToArray();
        var messageString = Encoding.UTF8.GetString(bodyBytes);
        var messageParsed = JToken.Parse(messageString);
        return messageParsed;
    }
}
