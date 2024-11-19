using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shahkar.UserManagement.Db.Query.Common;
using Shahkar.UserManagement.Db.Query.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.Db.Query.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddDbContext<UsersQueryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ShahkarDb"))
            ) ;
            services.AddScoped<IUserRepository , UserRepository>();
            return services ;
        }
    }
}
