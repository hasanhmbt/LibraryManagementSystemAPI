using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool ChangePassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
    }
}
