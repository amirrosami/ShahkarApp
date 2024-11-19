using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shahkar.UserManagement.AppService.Common.QueryHandlers
{
    public interface IQueryDispatcher
        
    {
        Task<TData> ExexcuteAsync<TQuery, TData>(TQuery query)
            where TQuery : class
            where TData : class;           
       
        
    }
}
