﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technomedia.Domain.Abstract;
using Technomedia.Domain.Entities;

namespace Technomedia.Domain.Concrete
{
    public class EFTeamUserRepository : ITeamUserRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<TeamUser> GetTeamUsers()
        {
            return context.TeamUsers;
        }

        public TeamUser GetTeamByUserId(int userId)
        {
            return context.TeamUsers.FirstOrDefault(x => x.UserId == userId);
        }

        

    }
}
