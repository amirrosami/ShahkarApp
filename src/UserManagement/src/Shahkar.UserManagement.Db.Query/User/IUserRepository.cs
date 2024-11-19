using Shahkar.UserManagement.Db.Query.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.Db.Query.User
{
    public interface IUserRepository
    {
        Task<Users> GetUserAsync(string PhoneNumber);
        
    }
}
