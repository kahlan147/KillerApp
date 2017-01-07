using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using DnDApp.Models;

namespace DnDApp.Controllers
{
    public class DnDController : Controller
    {
        //
        // GET: /DnD/

        // /DnD/CharacterPage
        public ActionResult CharacterPage(int id)
        {
            Database.CharId = id;
            if(Database.AllowedToBeHere("high")){
                Models.Character character = new Models.Character(Database.GetCharacter(id), Database.GetCharacterInfo(id));
                Database.currentCharacter = character;
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
        public ActionResult CharacterPage(Character newCharacter)
        {
            Database.UpdateCharacter(newCharacter);
            int id = newCharacter.CharId;
            Models.Character newcharacter = new Models.Character(Database.GetCharacter(id), Database.GetCharacterInfo(id));
            Database.currentCharacter = newcharacter;
            return View();
        }

        // /DnD/Inventory
        public ActionResult Inventory()
        {
            if (Database.AllowedToBeHere("high"))
            {
                int CharId = Database.CharId;
                Inventory inventory = new Inventory();
                inventory = Database.getInventory(CharId);
                List<Item> AllItems = Database.getAllItems();
                ViewBag.Inventory = inventory;
                ViewBag.AllItems = AllItems;
                return View();
            }
            else
            {
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }
        [HttpPost]
        public ActionResult Inventory(Inventory newinventory, string command)
        {
            
            if (command.Equals("Increase"))
            {
                Database.IncDecItemInv(newinventory, true);
            }
            else if (command.Equals("Decrease"))
            {
                Database.IncDecItemInv(newinventory, false);
            }
            else if (command.Equals("Remove"))
            {
                Database.RemoveItems(newinventory);
            }
            else if (command.Equals("Add"))
            {
                Database.AddItems(newinventory);
            }
                return RedirectToAction("Inventory", "DnD");
        }

        // /DnD/Spellbook
        public ActionResult Spellbook()
        {
            if(Database.AllowedToBeHere("high"))
            {
                int charId = Database.CharId;
                ViewBag.MySpellBook = Database.getSpellbook(charId);
                ViewBag.AllSpells = Database.getAllSpells();
                return View();
            }
            else{
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }

        [HttpPost]
        public ActionResult Spellbook(Spellbook newspellBook, string command)
        {
            if (command.Equals("Remove"))
            {
                Database.RemoveSpells(newspellBook);
            }
            else if (command.Equals("Add"))
            {
                Database.AddSpells(newspellBook);
            }
            else if (command.Equals("Prepare"))
            {
                Database.PrepareSpells(newspellBook, true);
            }
            else if (command.Equals("Unprepare"))
            {
                Database.PrepareSpells(newspellBook, false);
            }

            return RedirectToAction("Spellbook", "Dnd");
        }

        // /DnD/CreateNewChar
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

        // /DnD/DeleteCharacter
        public ActionResult DeleteCharacter(int id)
        {
            if (Database.AllowedToBeHere("low"))
            {
            DataRow result = Database.GetCharacter(id);
            ViewBag.Name = result["CharName"].ToString();
            ViewBag.Race = result["RaceName"].ToString();
            ViewBag.Class = result["ClassName"].ToString();
            ViewBag.Id = id;
            return View();
            }
            else{
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