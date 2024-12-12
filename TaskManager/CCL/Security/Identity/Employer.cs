using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
    public class Employer : User
    {
        public Employer(int userId, string name, string email, string password, string department)
            : base(userId, name, email, password, department, nameof(Employer)) { }
    }
}
