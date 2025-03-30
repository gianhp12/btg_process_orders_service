namespace btg_process_orders_service.Domain.VO;

public class CustomerCode
{
    private int Value { get; init; }

    public CustomerCode(int value)
    {
        if (value < 0)
        {
            throw new Exception("Customer code cannot be zero or less than zero");
        }
        Value = value;
    }
}
