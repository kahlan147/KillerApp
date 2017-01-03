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
        public ActionResult CharacterPage()
        {
            int CharId = 1;
            if (CharId != null)
            {

                Models.Character character = new Models.Character(Database.GetCharacter(CharId), Database.GetCharacterInfo(CharId));
                Models.CharInfo charInfo = new Models.CharInfo(Database.GetCharacterInfo(CharId));
                ViewBag.Character = character;
                ViewBag.CharInfo = charInfo;
                return View();
            }
            else
            {
                return RedirectToAction("LogInScreen", "Login");
            }
        }

        public ActionResult Inventory(int CharId)
        {
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
            return View();
        }

        public ActionResult SaveCharData()
        {
            Database.SaveCharacter();
            return View();
        }
	}
}