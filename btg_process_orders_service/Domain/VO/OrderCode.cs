namespace btg_process_orders_service.Domain.VO;

public class OrderCode
{
    private int Value { get; init; }

    public OrderCode(int value)
    {
        if (value < 0)
        {
            throw new Exception("Order code cannot be zero or less than zero");
        }
        Value = value;
    }

    public int GetValue()
    {
        return Value;
    }
}
