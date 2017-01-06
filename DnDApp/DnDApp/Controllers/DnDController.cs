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

        public ActionResult Inventory()
        {
            if (Database.AllowedToBeHere("high"))
            {
                int CharId = Database.CharId;
                DataTable InventoryTable = Database.getInventory(CharId);
                List<Item> InventoryList = new List<Item>();
                foreach (DataRow InventoryRow in InventoryTable.Rows)
                {
                    int ItemId = Convert.ToInt32(InventoryRow["ItemId"]);
                    DataRow ItemRow = Database.getItem(ItemId);
                    Models.Item newItem = new Models.Item(ItemRow);
                    InventoryList.Add(newItem);
                }
                List<Item> AllItems = Database.getAllItems();
                ViewBag.AllItems = AllItems;
                ViewBag.Inventory = InventoryList;
                return View();
            }
            else
            {
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }

        public ActionResult Spellbook()
        {
            if(Database.AllowedToBeHere("high"))
            {
            return View();
            }
            else{
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }

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