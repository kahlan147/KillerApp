using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using DnDApp.Models;

namespace DnDApp.Controllers
{
    public class LogInController : Controller
    {
        //
        // GET: /LogIn/
        public ActionResult LogInScreen()
        {
            if (Database.LoggedUser == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }

        [HttpPost]
        public ActionResult LogInScreen(AppUser appUser)
        {
            if (Database.LogIn(appUser))
            {
                Database.LoggedUser = appUser.UserName;
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AppUser appUser)
        {
            if (Database.Register(appUser) == true)
            {
                Database.LoggedUser = appUser.UserName;
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Database.LoggedUser = null;
            return RedirectToAction("LoginScreen", "Login");
        }
	}
}