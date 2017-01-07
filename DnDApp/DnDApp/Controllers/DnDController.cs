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
            Database.CharId = 2;
            Database.LoggedUser = "Niels";
            if (Database.AllowedToBeHere("high"))
            {
                int CharId = Database.CharId;
                List<Item> MyItems = new List<Item>();
                Inventory inventory = new Inventory();
                DataTable InventoryTable = Database.getInventory(CharId);
                foreach (DataRow InventoryRow in InventoryTable.Rows)
                {
                    int ItemId = Convert.ToInt32(InventoryRow["ItemId"]);
                    int Amount = Convert.ToInt32(InventoryRow["Amount"]);
                    DataRow ItemRow = Database.getItem(ItemId);
                    Models.Item newItem = new Models.Item(ItemRow);
                    newItem.Amount = Amount;
                    MyItems.Add(newItem);
                }
                inventory.MyItems = MyItems;
                List<Item> AllItems = Database.getAllItems();
                ViewBag.Inventory = inventory;
                ViewBag.AllItems = AllItems;
                ViewBag.Test = "Yolo";
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