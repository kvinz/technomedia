using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technomedia.Domain.Entities;

namespace Technomedia.Domain.Abstract
{
    public interface ICaseRepository
    {
        IEnumerable<Case> GetCases();

        void InsertCase(Case model);

        void UpdateCase(Case model);

        void DeleteCase(int caseId);
    }
}
