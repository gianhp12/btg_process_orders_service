using btg_process_orders_service.Domain.Entity;
using btg_process_orders_service.Domain.VO;
using btg_process_orders_service.Infra.NoSql;
using Newtonsoft.Json.Linq;

namespace btg_process_orders_service.Services;
public interface IProcessOrderService
{
    void Execute(JToken json);
}
public class ProcessOrderService : IProcessOrderService
{
    private readonly INoSqlDatabase NoSqlDatabase;

    public ProcessOrderService(INoSqlDatabase noSqlDatabase)
    {
        NoSqlDatabase = noSqlDatabase;
    }
    public void Execute(JToken json)
    {
        var order = Order.Create(
            orderCode: (int)json["codigoPedido"]!,
            customerCode: (int)json["codigoCliente"]!,
           items: MapItems(json["itens"]!)
        );
        
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
