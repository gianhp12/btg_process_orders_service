namespace btg_process_orders_service.Infra.NoSql;

public interface INoSqlConfiguration
{
    IConfiguration Value { get; }
    string Environment { get; }
}