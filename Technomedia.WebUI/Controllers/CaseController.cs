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
    public class CaseController : Controller
    {
        private ICaseRepository _caseRepository;
        private ITeamRepository _teamRepository;
        private IUserRepository _userRepository;
        private ITeamUserRepository _teamUserRepository;

        public CaseController(ICaseRepository caseRepository, ITeamRepository teamRepository, IUserRepository userRepository, ITeamUserRepository teamUserRepository)
        {
            _caseRepository = caseRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _teamUserRepository = teamUserRepository;
        }

        /// <summary>
        /// Страница заявок для секретаря
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(GetCaseViewModel());
        }

        /// <summary>
        /// Возвращает данные по заявкам для пагинации
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
            return this.Store(GetCaseViewModel());
        }

        /// <summary>
        /// Возвращает список заявок
        /// </summary>
        /// <param name="teamId">Id бригады</param>
        /// <returns></returns>
        public List<CaseViewModel> GetCaseViewModel(int teamId = 0)
        {
            IEnumerable<Case> cases;
            if(teamId == 0)
                cases = _caseRepository.GetCases().OrderByDescending(x => x.DateCreate);
            else
                cases = _caseRepository.GetCases().Where(t => t.TeamId == teamId).OrderByDescending(x => x.DateCreate);
            IEnumerable<Team> teamsList = _teamRepository.GetTeams();

            var viewCases = cases.Select(x => new CaseViewModel(x)
            {
                Status = CaseViewModel.CaseStatuses[(Enums.CaseStatus)x.StatusId],
                Team = teamsList.FirstOrDefault(t => t.TeamId == x.TeamId).Name
            }).ToList();

            return viewCases;
        }

        /// <summary>
        /// Открывает страницу создания заявки
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateCase()
        {

            try
            {
                ViewBag.GridName = "Создание заявки";
                ViewBag.Action = "create";
                List<Team> teamsList = _teamRepository.GetTeams().ToList();
                List<ListItem> listItems = new List<ListItem>();

                foreach(var team in teamsList)
                {
                    listItems.Add(new ListItem(team.Name, team.TeamId));
                }

                CreateEditCaseViewModel model = new CreateEditCaseViewModel()
                {
                    Case = new Case(),
                    ListItems = listItems,
                    ListItemValue = new ListItem()
                };


                return View("CreateEditCase", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Сохранияет данные заявки
        /// </summary>
        /// <param name="model"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCase(CreateEditCaseViewModel model, string team)
        {
            //_caseRepository.InsertCase(model.Case);
            if (!string.IsNullOrEmpty(team) && !string.IsNullOrEmpty(model.Case.Name) && !string.IsNullOrEmpty(model.Case.Description))
            {

                Team teamInstance = _teamRepository.GetTeamByName(team);
                model.Case.TeamId = teamInstance.TeamId;

                model.Case.StatusId = (int)Enums.CaseStatus.New;

                _caseRepository.InsertCase(model.Case);

                return RedirectToAction("Index");
            }
            else
                return null;
        }

        /// <summary>
        /// Отмена создания/редактирования
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            return RedirectToAction("Index", "MainPage");
        }

        /// <summary>
        /// Открывает страницу редактирования заявки для секретаря
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCase(string id)
        {
            ViewBag.GridName = "Редактирование заявки";
            ViewBag.Action = "edit";
            List<Team> teamsList = _teamRepository.GetTeams().ToList();
            List<ListItem> listItems = new List<ListItem>();

            foreach (var team in teamsList)
            {
                listItems.Add(new ListItem(team.Name, team.TeamId));
            }

            Case caseInstance = _caseRepository.GetCases().FirstOrDefault(x => x.Id == int.Parse(id));
            Team teamInstance = teamsList.FirstOrDefault(x => x.TeamId == caseInstance.TeamId);
            ListItem teamValue = new ListItem(teamInstance.Name, teamInstance.TeamId);

            CreateEditCaseViewModel model = new CreateEditCaseViewModel()
            {
                Case = _caseRepository.GetCases().FirstOrDefault(x => x.Id == int.Parse(id)),
                ListItems = listItems,
                ListItemValue = teamValue
            };

            return View("CreateEditCase", model);
        }

        /// <summary>
        /// Обновляет данные заявки
        /// </summary>
        /// <param name="model"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCase(CreateEditCaseViewModel model, string team)
        {
            Case caseInstance = _caseRepository.GetCases().FirstOrDefault(x => x.Id == model.Case.Id);

            if (!string.IsNullOrEmpty(team) && !string.IsNullOrEmpty(model.Case.Name) && !string.IsNullOrEmpty(model.Case.Description))
            {                
                Team teamInstance = _teamRepository.GetTeamByName(team);
                caseInstance.TeamId = teamInstance.TeamId;

                caseInstance.Name = model.Case.Name;
                caseInstance.Description = model.Case.Description;

                _caseRepository.UpdateCase(caseInstance);

                return RedirectToAction("Index");
            }
            else
                return null;
        }

        /// <summary>
        /// Удаление заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCase(string id)
        {
            _caseRepository.DeleteCase(int.Parse(id));

            return RedirectToAction("Index");
        }

        #region // Часть для бригад
        /// <summary>
        /// Страница заявок для юзера бригады
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowTeamCases()
        {
            string userToken = "";
            if (Request.Cookies["token"] != null)
            {
                userToken = Request.Cookies["token"].Value.ToString();
            }
            else
                return RedirectToAction("Login", "Registration");

            User user = _userRepository.GetUsers().FirstOrDefault(x => x.Token == userToken);
            if (user == null)   
                return RedirectToAction("Login", "Registration");

            TeamUser teamOfUser = _teamUserRepository.GetTeamByUserId(user.UserId);
            var cases = GetCaseViewModel(teamOfUser.TeamId);

            return View("ShowTeamCases", cases);
        }

        /// <summary>
        /// Возвращает данные заявок бригады для пагинации
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataForTeam()
        {
            string userToken = "";
            if (Request.Cookies["token"] != null)
            {
                userToken = Request.Cookies["token"].Value.ToString();
            }
            else
                return RedirectToAction("Login", "Registration");

            User user = _userRepository.GetUsers().FirstOrDefault(x => x.Token == userToken);
            if (user == null)
                return RedirectToAction("Login", "Registration");

            TeamUser teamOfUser = _teamUserRepository.GetTeamByUserId(user.UserId);
            var cases = GetCaseViewModel(teamOfUser.TeamId);

            return this.Store(cases);
        }

        /// <summary>
        /// Открывает страницу редактирования заявки для юзера бригады 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditTeamCase(string id)
        {
            string dateCloseCase = "";
            string timeCloseCase = "";

            Case caseInstance = _caseRepository.GetCases().FirstOrDefault(x => x.Id == int.Parse(id));

            List<ListItem> caseStatuses = CaseViewModel.CaseStatuses.Select(x => new ListItem(x.Value, x.Key)).ToList(); // Перегоняем статусы из словаря в перечислитель
            caseStatuses.RemoveAt(0); // Удаляем статус "Новая"

            var caseStatus = CaseViewModel.CaseStatuses.FirstOrDefault(x => x.Key == (Enums.CaseStatus)caseInstance.StatusId);
            ListItem listItemValue = caseInstance.StatusId == (int)Enums.CaseStatus.New ? new ListItem() : new ListItem(caseStatus.Value, caseStatus.Key);

            if(caseInstance.StatusId == (int)Enums.CaseStatus.Closed) // Если закрыта
            {
                dateCloseCase = caseInstance.DateTimeEnd.ToShortDateString();
                timeCloseCase = caseInstance.DateTimeEnd.ToShortTimeString();
            }

            CreateEditCaseViewModel model = new CreateEditCaseViewModel()
            {
                Case = caseInstance,
                ListItems = caseStatuses,
                ListItemValue = listItemValue,
                DateClosedCase = dateCloseCase,
                TimeClosedCase = timeCloseCase
            };

            return View(model);
        }

        /// <summary>
        /// Изменяет данные заявки
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTeamCase(CreateEditCaseViewModel model)
        {
            Case caseInstance = _caseRepository.GetCases().FirstOrDefault(x => x.Id == model.Case.Id);

            if (!string.IsNullOrEmpty(model.DateClosedCase) && !string.IsNullOrEmpty(model.TimeClosedCase))
            {
                caseInstance.DateTimeEnd = Convert.ToDateTime(model.DateClosedCase + " " + model.TimeClosedCase);  // Дата закрытие заявки
            }

            if (!string.IsNullOrEmpty(model.SelectedListItem))
            {
                caseInstance.StatusId = (int)CaseViewModel.CaseStatuses.FirstOrDefault(x => x.Value.Equals(model.SelectedListItem)).Key; // Получаем id статуса заявки
            }
            
            caseInstance.Note = model.Case.Note;

            _caseRepository.UpdateCase(caseInstance);

            return RedirectToAction("ShowTeamCases");
        }
        #endregion

    }
}