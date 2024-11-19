using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.AppService.Common.QueryHandlers
{
    public class BaseView
    {
        public string Message { get; set; }
        public Guid Request_Id { get; set; }
        
        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetRequest_Id(Guid  request_id)
        {
            Request_Id = request_id;
        }
    }
}
