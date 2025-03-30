using btg_process_orders_service.Domain.Entity;
using btg_process_orders_service.Infra.NoSql;
using btg_process_orders_service.Infra.NoSql.Dtos;
using Newtonsoft.Json.Linq;

namespace btg_process_orders_service.Application.Services;
public interface IProcessOrderService
{
    Task Execute(JToken json);
}
public class ProcessOrderService : IProcessOrderService
{
    private readonly INoSqlDatabase NoSqlDatabase;

    public ProcessOrderService(INoSqlDatabase noSqlDatabase)
    {
        NoSqlDatabase = noSqlDatabase;
    }

    public async Task Execute(JToken json)
    {
        var order = Order.Create(
          orderCode: (int)json["codigoPedido"]!,
          customerCode: (int)json["codigoCliente"]!,
         items: MapItems(json["itens"]!)
      );
        var query = new MongoDbQueryDto<Order>
        {
            Database = "btg_pactual_orders",
            CollectionName = "orders",
            Document = order
        };
        await NoSqlDatabase.InsertDocumentAsync(query);
    }
    private List<OrderItem> MapItems(JToken itemsJson)
    {
        var items = new List<OrderItem>();
        foreach (var itemJson in itemsJson)
        {
            var product = (string)itemJson["produto"]!;
            var quantity = (int)itemJson["quantidade"]!;
            var price = (decimal)itemJson["preco"]!;
            var item = new OrderItem
            (
                product: product,
                quantity: quantity,
                price: price
            );
            items.Add(item);
        }
        return items;
    }
}
