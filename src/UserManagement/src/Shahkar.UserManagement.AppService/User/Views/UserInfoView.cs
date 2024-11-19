using Shahkar.UserManagement.AppService.Common.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.AppService.UserInfo.Views
{
    public class UserInfoView: BaseView
    {
        public string National_Id { get; set; }
        public string   First_Name  { get; set; }
        public string   Last_Name  { get; set; }
        public string   Birth_Date  { get; set; }
        public string   Address  { get; set; }
        
    }
}
