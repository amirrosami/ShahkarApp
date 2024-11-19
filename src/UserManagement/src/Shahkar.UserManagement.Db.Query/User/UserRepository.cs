using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shahkar.UserManagement.Db.Query.Common;
using Shahkar.UserManagement.Db.Query.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.Db.Query.User
{
    public class UserRepository:IUserRepository
    {
        private readonly UsersQueryContext _queryContext;

        public UserRepository(UsersQueryContext queryContext)
        {
            _queryContext = queryContext;
        }

        public async Task<Users> GetUserAsync(string PhoneNumber)
        {
            return await _queryContext
                .Users
                .Where(x => x.Phone_Number == PhoneNumber)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        
    }
}
