namespace btg_process_orders_service.Domain.VO;

public class Price
{
    private decimal Value { get; init; }

    public Price(decimal value)
    {
        if (value < 0)
        {
            throw new Exception("Price can`t less than zero");
        }
        Value = value;
    }

    public decimal GetPrice()
    {
        return Value;
    }
}
