using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DnDApp.Models;

namespace DnDApp.Controllers
{
    public class MainMenuController : Controller
    {
        //
        // GET: /MainMenu/
        public ActionResult CharacterSelect()
        {
            if (Database.LoggedUser == null)
            {
                return RedirectToAction("LogInScreen", "Login");
            }
            List<Character> MyCharacters = Database.getCharactersFrom();
            return View(MyCharacters);
        }
	}
}