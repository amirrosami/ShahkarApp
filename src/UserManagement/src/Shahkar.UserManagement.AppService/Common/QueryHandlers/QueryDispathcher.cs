using Microsoft.Extensions.DependencyInjection;

namespace Shahkar.UserManagement.AppService.Common.QueryHandlers
{
    public class QueryDispathcher:IQueryDispatcher   
       
    {

        private readonly IServiceProvider _serviceProvider;

        public QueryDispathcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TData> ExexcuteAsync<TQuery,TData>(TQuery query)
            where TQuery : class
            where TData : class
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery,TData>>();
            if (handler == null)
            {
                throw new NotImplementedException();
            }
            try
            {
               return await handler.HandleAsync(query);
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
