using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Technomedia.Domain.Entities;
using Technomedia.WebUI.Classes;

namespace Technomedia.WebUI.Models
{
    public class CaseViewModel : Case
    {
        public CaseViewModel() { }

        public CaseViewModel(Case caseInstance)
        {
            Id = caseInstance.Id;
            Name = caseInstance.Name;
            Description = caseInstance.Description;
            DateCreate = caseInstance.DateCreate;
            DateTimeEnd = caseInstance.DateTimeEnd;
            Note = caseInstance.Note;
            TeamId = caseInstance.TeamId;
            StatusId = caseInstance.StatusId;
            Deleted = caseInstance.Deleted;
        }

        public string Status { get; set; }
        public string Team { get; set; }

        /// <summary>
        /// Время потраченное на выполнение задания
        /// </summary>
        public string WorkingTime {get;set;}

        public static readonly IDictionary<Enums.CaseStatus, string> CaseStatuses = new Dictionary<Enums.CaseStatus, string>
        {
            [Enums.CaseStatus.New] = "Новая",
            [Enums.CaseStatus.Process] = "В работе",
            [Enums.CaseStatus.Closed] = "Закрыта"
        };
    }
}