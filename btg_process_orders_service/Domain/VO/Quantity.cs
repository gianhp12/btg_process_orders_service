namespace btg_process_orders_service.Domain.VO;

public class Quantity
{
    private int Value { get; init; }

    public Quantity(int value)
    {
        if (value < 0)
        {
            throw new Exception("Quantity can`t less than zero");
        }
        Value = value;
    }

    public int GetValue()
    {
        return Value;
    }
}
