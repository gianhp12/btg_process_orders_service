namespace btg_process_orders_service.Domain.Entity;

public class Order
{
    public Guid Id { get; set; }
    public int OrderCode { get; init; }
    public int CustomerCode { get; init; }
    public List<OrderItem> Items { get; init; }

    private Order(Guid id, int orderCode, int customerCode, List<OrderItem> items)
    {
        Id = id;
        OrderCode = orderCode;
        CustomerCode = customerCode;
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
