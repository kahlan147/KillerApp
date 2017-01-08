using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DnDApp.Models;
using System.Data;

namespace DnDApp.Controllers
{
    public class MainMenuController : Controller
    {
        //
        // GET: /MainMenu/
        public ActionResult CharacterSelect()
        {
            Database.MoreInfoItemId = 0;
            Database.MoreInfoSpell = null;
            Database.CharId = -1;
            if (Database.AllowedToBeHere("low"))
            {
                List<Character> MyCharacters = Database.getCharactersFrom();
                return View(MyCharacters);
            }
            else
            {
                return RedirectToAction("LogInScreen", "Login");
            }
        }

        // /MainMenu/CreateNewChar
        public ActionResult CreateNewChar()
        {
            if (Database.AllowedToBeHere("low"))
            {
                ViewBag.Classes = Database.getAllClasses();
                ViewBag.Races = Database.getAllRaces();
                return View();
            }
            else
            {
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }

        [HttpPost]
        public ActionResult CreateNewChar(Character character)
        {
            Database.SaveNewCharacter(character);
            return RedirectToAction("CharacterSelect", "MainMenu");
        }

        // /MainMenu/DeleteCharacter
        public ActionResult DeleteCharacter(int id)
        {
            Database.CharId = id;
            if (Database.AllowedToBeHere("high"))
            {
                DataRow result = Database.GetCharacter(id);
                ViewBag.Name = result["CharName"].ToString();
                ViewBag.Race = result["RaceName"].ToString();
                ViewBag.Class = result["ClassName"].ToString();
                ViewBag.Id = id;
                return View();
            }
            else
            {
                return RedirectToAction("LogInScreen", "Login");
            }
        }

        [HttpPost]
        public ActionResult DeleteCharacter(Character character)
        {
            Database.DeleteCharacter(character);
            return RedirectToAction("CharacterSelect", "MainMenu");
        }
	}
}