using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.Db.Query.Entities
{
    public class Users
    {
        public Guid User_Id { get; set; }
        public string Phone_Number { get; set; }
        public string National_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateOnly Birth_Date { get; set; }
        public string Address { get; set; }
    }
}
