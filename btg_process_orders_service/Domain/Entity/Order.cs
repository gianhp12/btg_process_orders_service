using btg_process_orders_service.Domain.VO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace btg_process_orders_service.Domain.Entity;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; init; }
    public OrderCode OrderCode { get; init; }
    public CustomerCode CustomerCode { get; init; }
    public List<OrderItem> Items { get; init; }

    private Order(Guid id, int orderCode, int customerCode, List<OrderItem> items)
    {
        Id = id;
        OrderCode = new OrderCode(orderCode);
        CustomerCode = new CustomerCode(customerCode);
        Items = items;
    }

    public static Order Create(int orderCode, int customerCode, List<OrderItem> items)
    {
        var orderId = Guid.NewGuid();
        return new Order(
            id: orderId,
            orderCode: orderCode,
            customerCode: customerCode,
            items: items
        );
    }
}
