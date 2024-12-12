using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
    public abstract class User
    {
        public User(int userId, string name, string email, string password, string department, string userType)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Password = password;
            Department = department;
            UserType = userType;
        }
        public int UserId { get; }
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public string Department { get; }
        protected string UserType { get; }
    }
}
