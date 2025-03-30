using btg_process_orders_service.Domain.VO;

namespace btg_process_orders_service.Domain.Entity;

public class OrderItem
{
    public string Product { get; init; }
    public Quantity Quantity { get; init; }
    public Price Price { get; init; }

    public OrderItem(string product, int quantity, decimal price)
    {
        Product = product;
        Quantity = new Quantity(quantity);
        Price = new Price(price);
    }
}
