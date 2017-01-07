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
            Database.MoreInfoSpell = null;
            Database.MoreInfoItemId = 0;
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
                Database.MoreInfoSpell = null;
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
            
            if (command.Equals("Increase +1"))
            {
                Database.IncDecItemInv(newinventory, true, 1);
            }
            else if (command.Equals("Increase +10"))
            {
                Database.IncDecItemInv(newinventory, true, 10);
            }
            else if (command.Equals("Increase +20"))
            {
                Database.IncDecItemInv(newinventory, true, 20);
            }
            else if (command.Equals("Decrease -1"))
            {
                Database.IncDecItemInv(newinventory, false, 1);
            }
            else if (command.Equals("Decrease -10"))
            {
                Database.IncDecItemInv(newinventory, false, 10);
            }
            else if (command.Equals("Decrease -20"))
            {
                Database.IncDecItemInv(newinventory, false, 20);
            }
            else if (command.Equals("Remove"))
            {
                Database.RemoveItems(newinventory);
            }
            else if (command.Equals("Add"))
            {
                Database.AddItems(newinventory);
            }
            else if (command.Equals("More info"))
            {
                if (newinventory.ToBeAddedItems.Count > 0)
                {
                    Database.MoreInfoItemId = newinventory.ToBeAddedItems[0];
                    return RedirectToAction("MoreInfo", "DnD");
                }
            }
            else if (command.Equals("More Info"))
            {
                if(newinventory.SelectedItems.Count > 0){
                Database.MoreInfoItemId = newinventory.SelectedItems[0];
                return RedirectToAction("MoreInfo", "DnD");
                }
            }
                return RedirectToAction("Inventory", "DnD");
        }

        // /DnD/Spellbook
        public ActionResult Spellbook()
        {
            if(Database.AllowedToBeHere("high"))
            {
                Database.MoreInfoItemId = 0;
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
            else if (command.Equals("More info"))
            {
                if (newspellBook.ToBeAddedSpells.Count > 0)
                {
                    Database.MoreInfoSpell = newspellBook.ToBeAddedSpells[0];
                    return RedirectToAction("MoreInfo", "DnD");
                }
            }
            else if (command.Equals("More Info"))
            {
                if (newspellBook.SelectedSpells.Count > 0)
                {
                    Database.MoreInfoSpell = newspellBook.SelectedSpells[0];
                    return RedirectToAction("MoreInfo", "DnD");
                }
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

        // /DnD/MoreInfo
        public ActionResult MoreInfo()
        {
            if (Database.MoreInfoSpell == null && Database.MoreInfoItemId != 0)
            {
                int id = Database.MoreInfoItemId;
                DataRow ItemRow = Database.getItem(id);
                Item newItem = new Item(ItemRow);
                ViewBag.Object = newItem;
                ViewBag.SpellOrItem = "Item";
                return View();
            }
            else if (Database.MoreInfoSpell != null && Database.MoreInfoItemId == 0)
            {
                string name = Database.MoreInfoSpell;
                DataRow SpellRow = Database.getSpell(name);
                Spell newSpell = new Spell(SpellRow);
                ViewBag.Object = newSpell;
                ViewBag.SpellOrItem = "Spell";
                return View();
            }
            else
            {
                return RedirectToAction("CharacterSelect", "MainMenu");
            }
        }
	}
}