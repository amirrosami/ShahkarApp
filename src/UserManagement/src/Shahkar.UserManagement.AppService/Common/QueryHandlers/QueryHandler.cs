namespace Shahkar.UserManagement.AppService.Common.QueryHandlers
{
    public abstract class QueryHandler<TQuery, TData>:IQueryHandler<TQuery,TData> 
        where TQuery : class
        where TData : class
    {
        public abstract Task<TData> HandleAsync(TQuery query); 
    }
}
