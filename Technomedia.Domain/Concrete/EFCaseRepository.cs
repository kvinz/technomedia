using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technomedia.Domain.Abstract;
using Technomedia.Domain.Entities;

namespace Technomedia.Domain.Concrete
{
    public class EFCaseRepository : ICaseRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Case> GetCases()
        {
            return context.Cases.Where(x => x.Deleted == false);
        }

        public void InsertCase(Case model)
        {
            model.DateCreate = DateTime.Now;
            model.Deleted = false;
            context.Cases.Add(model);
            context.SaveChanges();
        }

        public void UpdateCase(Case model)
        {
            Case dbEntry = context.Cases.Find(model.Id);
            if(dbEntry != null)
            {
                dbEntry.DateTimeEnd = model.DateTimeEnd;
                dbEntry.Note = model.Note;
                dbEntry.StatusId = model.StatusId;
                dbEntry.Description = model.Description;
                dbEntry.Name = dbEntry.Name;
                dbEntry.TeamId = dbEntry.TeamId;
                context.SaveChanges();
            }
        }

        public void DeleteCase(int caseId)
        {
            Case dbEntry = context.Cases.Find(caseId);
            if(dbEntry != null)
            {
                dbEntry.Deleted = true;
                context.SaveChanges();
            }
        }
    }
}
