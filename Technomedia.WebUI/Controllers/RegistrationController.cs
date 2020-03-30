using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Technomedia.Domain.Abstract;
using Technomedia.Domain.Entities;
using Technomedia.WebUI.Models;

namespace Technomedia.WebUI.Controllers
{
    public class RegistrationController : Controller
    {
        private IUserRepository _userRepository;

        //public RegistrationController() { }

        public RegistrationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult Login()
        {
            return View(new LoginModel() { Login = "L", Password = "P" });
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            User user = null;

            user = _userRepository.GetUsers().FirstOrDefault(x => x.Login.Equals(model.Login) && x.Password.Equals(model.Password));

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);
                HttpContext.Response.Cookies["token"].Value = user.Token;

                return RedirectToAction("Index", "MainPage");
            }
            else                    // Если пользователь не найден то оповещаем об этом
            {                
                return null;
            }
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        public ActionResult Logoff()
        {
            Session.Abandon();

            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1); // помечаем, что куки истек вчЁра
                Response.Cookies.Add(aCookie); // перезаписываем
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Registration");

        }
    }
}