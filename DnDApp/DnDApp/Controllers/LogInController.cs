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
            return View();
        }

        [HttpPost]
        public ActionResult LogInScreen(AppUser appUser)
        {
            if (Database.LogIn(appUser))
            {
                Database.LoggedUser = appUser.UserName;
                return RedirectToAction("CharacterPage", "DnD");
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
	}
}