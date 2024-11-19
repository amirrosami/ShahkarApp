using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.AppService.Common.QueryHandlers
{
    public interface IQueryHandler<TQuery ,TData>
        where TQuery : class
        where TData : class
    {
        Task<TData> HandleAsync(TQuery query);
    }
}
