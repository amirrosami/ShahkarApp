using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shahkar.UserManagement.AppService.Common.QueryHandlers;
using Shahkar.UserManagement.AppService.UserInfo.Queries;
using Shahkar.UserManagement.AppService.UserInfo.QueryHandlers;
using Shahkar.UserManagement.AppService.UserInfo.Views;
using Shahkar.UserManagement.Db.Query.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.AppService.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbServices(configuration);
            services.AddScoped<IQueryDispatcher, QueryDispathcher>();
            services.AddScoped<IQueryHandler<GetUserInfoByPhoneQuery, UserInfoView>, GetUserInfoQueryHandler>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
                {
                    AbortOnConnectFail = true,
                    EndPoints = { options.Configuration }
                    
                };
            });
            return services;
        }


    }
}
