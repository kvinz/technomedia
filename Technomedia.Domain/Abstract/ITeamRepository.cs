using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technomedia.Domain.Entities;

namespace Technomedia.Domain.Abstract
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetTeams();

        Team GetTeamByName(string teamName);

    }
}
