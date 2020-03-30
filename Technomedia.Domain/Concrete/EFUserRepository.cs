using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technomedia.Domain.Abstract;
using Technomedia.Domain.Entities;

namespace Technomedia.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }
    }
}
