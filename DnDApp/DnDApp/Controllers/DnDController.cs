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
            if (Database.LoggedUser == null)
            {
                return RedirectToAction("LogInScreen", "Login");
            }
            if (id >= 0)
            {

                Models.Character character = new Models.Character(Database.GetCharacter(id), Database.GetCharacterInfo(id));
                Models.CharInfo charInfo = new Models.CharInfo(Database.GetCharacterInfo(id));
                ViewBag.Character = character;
                ViewBag.CharInfo = charInfo;
                return View();
            }
            else
            {
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }

        public ActionResult Inventory(int CharId)
        {
            if (Database.LoggedUser == null)
            {
                return RedirectToAction("LogInScreen", "Login");
            }
            DataTable InventoryTable = Database.getInventory(CharId);
            List<Models.Item> InventoryList = new List<Models.Item>();
            foreach (DataRow InventoryRow in InventoryTable.Rows)
            {
                int ItemId = Convert.ToInt32(InventoryRow["ItemId"]);
                DataRow ItemRow = Database.getItem(ItemId);
                Models.Item newItem = new Models.Item(ItemRow);
                InventoryList.Add(newItem);
            }
            Models.Item[] Inventory = InventoryList.ToArray();
            ViewBag.Inventory = Inventory;
            return View();
        }

        public ActionResult Spellbook()
        {
            if (Database.LoggedUser == null)
            {
                return RedirectToAction("LogInScreen", "Login");
            }
            return View();
        }

        public ActionResult CreateNewChar()
        {
            if (Database.LoggedUser == null)
            {
                return RedirectToAction("LogInScreen", "Login");
            }
            ViewBag.Classes = Database.getAllClasses();
            ViewBag.Races = Database.getAllRaces();
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewChar(Character character)
        {
            Database.SaveNewCharacter(character);
            return RedirectToAction("CharacterSelect", "MainMenu");
        }

        public ActionResult DeleteCharacter(int id)
        {
            if (Database.LoggedUser == null)
            {
                return RedirectToAction("LogInScreen", "Login");
            }
            DataRow result = Database.GetCharacter(id);
            ViewBag.Name = result["CharName"].ToString();
            ViewBag.Race = result["RaceName"].ToString();
            ViewBag.Class = result["ClassName"].ToString();
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCharacter(Character character)
        {
            Database.DeleteCharacter(character);
            return RedirectToAction("CharacterSelect", "MainMenu");
        }
	}
}