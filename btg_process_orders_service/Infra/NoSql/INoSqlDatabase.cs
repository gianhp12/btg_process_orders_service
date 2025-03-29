using btg_process_orders_service.Infra.NoSql.Dtos;

namespace btg_process_orders_service.Infra.NoSql
{
    public interface INoSqlDatabase
    {
        Task InsertDocumentAsync<T>(MongoDbQueryDto<T> queryDto);
        Task<List<T>> ExecuteQueryAsync<T>(MongoDbQueryDto<T> queryDto);
        Task UpdateDocumentAsync<T>(MongoDbQueryDto<T> queryDto);
        Task DeleteDocumentAsync<T>(MongoDbQueryDto<T> queryDto);
    }
}
