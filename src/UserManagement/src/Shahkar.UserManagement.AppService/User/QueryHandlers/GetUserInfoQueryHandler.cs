using Microsoft.Extensions.Caching.Distributed;
using Shahkar.UserManagement.AppService.Common.QueryHandlers;
using Shahkar.UserManagement.AppService.Common.Redis;
using Shahkar.UserManagement.AppService.UserInfo.Queries;
using Shahkar.UserManagement.AppService.UserInfo.Views;
using Shahkar.UserManagement.Db.Query.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.AppService.UserInfo.QueryHandlers
{
    public class GetUserInfoQueryHandler : QueryHandler<GetUserInfoByPhoneQuery, UserInfoView>
    {
        private readonly IUserRepository _userRepo;
        private readonly IDistributedCache _cache;
        public GetUserInfoQueryHandler(IUserRepository userRepo,IDistributedCache cache)
        {
            _userRepo = userRepo;
           _cache = cache;
        }
        public override async Task<UserInfoView> HandleAsync(GetUserInfoByPhoneQuery query)
        {
            UserInfoView response = null;
            if (_cache.TryGetValue<UserInfoView>(query.PhoneNumber ,out response))
            {
              return response;
            }

            var user =await _userRepo.GetUserAsync(query.PhoneNumber);
            response = new UserInfoView();
            //response.SetRequest_Id(query.Request_Details.Request_Id);
            if (user == null)
            {
                response.SetMessage("fail");
            }
            else
            {
                response.SetMessage("success");
                response.National_Id = user.National_Id;
                response.Birth_Date = user.Birth_Date.ToString();
                response.First_Name = user.First_Name;
                response.Last_Name  = user.Last_Name;
                response.Address = user.Address;
                await _cache.SetAsync<UserInfoView>(query.PhoneNumber,response);     
            }

            return response;
        }
    }
}
