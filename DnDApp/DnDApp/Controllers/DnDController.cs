using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace DnDApp.Controllers
{
    public class DnDController : Controller
    {
        //
        // GET: /DnD/
        public ActionResult CharacterPage(int CharId)
        {
            if (CharId != null)
            {
                Models.Database database = new Models.Database();
                Models.Character character = new Models.Character(database.GetCharacter(CharId));
                Models.CharInfo charInfo = new Models.CharInfo(database.GetCharacterInfo(CharId));
                ViewBag.Character = character;
                ViewBag.CharInfo = charInfo;
            }
            return View();
        }

        public ActionResult Inventory(int CharId)
        {
            Models.Database database = new Models.Database();
            DataTable InventoryTable = database.getInventory(CharId);
            List<Models.Item> InventoryList = new List<Models.Item>();
            foreach (DataRow InventoryRow in InventoryTable.Rows)
            {
                int ItemId = Convert.ToInt32(InventoryRow["ItemId"]);
                DataRow ItemRow = database.getItem(ItemId);
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
            Models.Database database = new Models.Database();
            database.SaveCharacter();
            return View();
        }
	}
}