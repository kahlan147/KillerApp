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
                ViewBag.ErrorMessage = "Username or password is incorrect.";
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
            try
            {
                if (appUser.UserName == "" || appUser.Password == "" || appUser.UserName.Contains(" ") || appUser.Password.Contains(" ") 
                    || appUser.UserName == null || appUser.Password == null || appUser.UserName.Length <6 || appUser.Password.Length<6)
                {
                    ViewBag.ErrorMessage = "Invalid username or password, username and password must be atleast 6 characters long and may not contain spaces.";
                    return View();
                }
                if (Database.Register(appUser) == true)
                {
                    Database.LoggedUser = appUser.UserName;
                    return RedirectToAction("CharacterSelect", "MainMenu");
                }
                else
                {
                    ViewBag.ErrorMessage = "Username is already in use, try something else.";
                    return View();
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "An fatal error occured, please try again later. Our lab monkeys are working hard to fix this.";
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