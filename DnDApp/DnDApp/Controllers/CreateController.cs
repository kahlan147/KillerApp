using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DnDApp.Models;

namespace DnDApp.Controllers
{
    public class CreateController : Controller
    {
        //
        // GET: /Create/
        public ActionResult NewItem()
        {
            if(Database.AllowedToBeHere("high")){
                return View();
            }
            else{
                return RedirectToAction("CharacterSelect","MainMenu");
            }
        }

        [HttpPost]
        public ActionResult NewItem(Item item)
        {
            try
            {
                Database.AddNewItem(item);
                return RedirectToAction("Inventory", "DnD");
            }
            catch
            {
                ViewBag.ErrorMessage = "An Error Occured";
                return View();
            }
        }

        public ActionResult NewSpell()
        {
            if(Database.AllowedToBeHere("high")){
                return View();
            }
            else{
                return RedirectToAction("CharacterSelect","MainMenu");
            }
        }

        [HttpPost]
        public ActionResult NewSpell(Spell spell)
        {
            try
            {
                Database.AddNewSpell(spell);
                return RedirectToAction("Spellbook", "DnD");
            }
            catch
            {
                ViewBag.ErrorMEssage = "An error occured or a spell with this name already exists.";
                return View();
            }
        }
	}
}