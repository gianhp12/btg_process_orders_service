using MongoDB.Driver;

namespace btg_process_orders_service.Infra.NoSql.Dtos;

public class MongoDbQueryDto<T>
{
    public required string Database { get; init; }
    public required string CollectionName { get; init; }
    public FilterDefinition<T>? Filter { get; init; }
    public UpdateDefinition<T>? Update { get; init; }
    public T? Document { get; init; }
    public List<UpdateDefinition<T>>? Updates { get; init; }
}
