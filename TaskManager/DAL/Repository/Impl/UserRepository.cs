using DAL.EF;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Impl
{
    public class UserRepository
        : BaseRepository<User>, IUserRepository
    {
        internal UserRepository(TaskManagerContext context)
            : base(context)
        {
        }
    }
}
