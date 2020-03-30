using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technomedia.Domain.Abstract;
using Technomedia.Domain.Entities;

namespace Technomedia.Domain.Concrete
{
    public class EFTeamRepository : ITeamRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Team> GetTeams()
        {
            return context.Teams;
        }

        public Team GetTeamByName(string teamName)
        {
            Team dbEntry = context.Teams.FirstOrDefault(x => x.Name.Equals(teamName));
            return dbEntry;
        }
    }
}
