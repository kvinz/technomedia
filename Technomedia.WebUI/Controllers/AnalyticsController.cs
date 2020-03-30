using Ext.Net;
using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Technomedia.Domain.Abstract;
using Technomedia.Domain.Entities;
using Technomedia.WebUI.Classes;
using Technomedia.WebUI.Models;

namespace Technomedia.WebUI.Controllers
{
    public class AnalyticsController : Controller
    {
        ITeamRepository _teamRepository;
        ICaseRepository _caseRepository;

        public AnalyticsController(ITeamRepository teamRepository, ICaseRepository caseRepository)
        {
            _teamRepository = teamRepository;
            _caseRepository = caseRepository;
        }

        public ActionResult Index()
        {
            IEnumerable<Team> teamsList = _teamRepository.GetTeams();
            List<ListItem> listItems = new List<ListItem>();

            foreach (var team in teamsList)
            {
                listItems.Add(new ListItem(team.Name, team.TeamId));
            }

            AnalyticsViewModel model = new AnalyticsViewModel()
            {
                ListItems = listItems,
                ListItemValue = new ListItem(),
                CasesData = GetAnalyticsData()
            };

            return View(model);
        }

        public IEnumerable<CaseViewModel> GetAnalyticsData(string teamid = "", string month = "")
        {
            IEnumerable<Case> cases;

            if (string.IsNullOrEmpty(teamid) && string.IsNullOrEmpty(month))
            {
                cases = _caseRepository.GetCases().Where(x => x.StatusId == (int)Enums.CaseStatus.Closed);
            }
            else if (!string.IsNullOrEmpty(teamid) && string.IsNullOrEmpty(month))
            {
                cases = _caseRepository.GetCases().Where(x => x.StatusId == (int)Enums.CaseStatus.Closed && x.TeamId == int.Parse(teamid));
            }
            else if (string.IsNullOrEmpty(teamid) && !string.IsNullOrEmpty(month))
            {
                DateTime dateMonth = Convert.ToDateTime(month);
                cases = _caseRepository.GetCases().Where(x => x.StatusId == (int)Enums.CaseStatus.Closed && x.DateTimeEnd.Month == dateMonth.Month && x.DateTimeEnd.Year == dateMonth.Year);
            }
            else
            {
                DateTime dateMonth = Convert.ToDateTime(month);
                cases = _caseRepository.GetCases().Where(x => x.StatusId == (int)Enums.CaseStatus.Closed && x.TeamId == int.Parse(teamid) && x.DateTimeEnd.Month == dateMonth.Month && x.DateTimeEnd.Year == dateMonth.Year);
            }

                IEnumerable<Team> teamsList = _teamRepository.GetTeams();

            var viewCases = cases.Select(x => new CaseViewModel(x)
            {
                Status = CaseViewModel.CaseStatuses[(Enums.CaseStatus)x.StatusId],
                Team = teamsList.FirstOrDefault(t => t.TeamId == x.TeamId).Name,
                WorkingTime = (x.DateTimeEnd - x.DateCreate).ToString(@"hh\:mm")
            }).ToList();

            return viewCases;
        }

         public ActionResult GetData(string teamId, string month)
         {
             IEnumerable<Team> teamsList = _teamRepository.GetTeams();
             List<ListItem> listItems = new List<ListItem>();

             foreach (var team in teamsList)
             {
                 listItems.Add(new ListItem(team.Name, team.TeamId));
             }

             AnalyticsViewModel model = new AnalyticsViewModel()
             {
                 ListItems = listItems,
                 ListItemValue = new ListItem(),
                 CasesData = GetAnalyticsData(teamId, month)
             };

             return this.Store(model.CasesData);
         }
    }
}