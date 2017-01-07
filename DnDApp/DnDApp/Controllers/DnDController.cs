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
                List<Item> AllItems = Database.getAllItems();
                Inventory inventory = Database.getInventory();
                inventory.moneyPouch = Database.GetMoneyPouch();
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
            int CharId = Database.CharId;
            string messageSelected = "Please choose atleast one item from your inventory first";
            string messageAdded = "Please choose atleast one item from the all items box";
            if (command.Equals("Increase +1"))
            {
                if (newinventory.IsSelected())
                {
                    Database.IncDecItemInv(newinventory, true, 1);
                }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("Increase +10"))
            {
                if (newinventory.IsSelected())
                {
                Database.IncDecItemInv(newinventory, true, 10);
                    }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("Increase +20"))
            {
                if (newinventory.IsSelected())
                {
                Database.IncDecItemInv(newinventory, true, 20);
                    }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("Decrease -1"))
            {
                if (newinventory.IsSelected())
                {
                Database.IncDecItemInv(newinventory, false, 1);
                    }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("Decrease -10"))
            {
                if (newinventory.IsSelected())
                {
                Database.IncDecItemInv(newinventory, false, 10);
                    }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("Decrease -20"))
            {
                if (newinventory.IsSelected())
                {
                Database.IncDecItemInv(newinventory, false, 20);
                    }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("Remove"))
            {
                if (newinventory.IsSelected())
                {
                Database.RemoveItems(newinventory);
                    }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("Add"))
            {
                if (newinventory.IsAddedSelected())
                {
                    Database.AddItems(newinventory);
                }
                else
                {
                    ViewBag.ErrorMessage = messageAdded;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("More info"))
            {
                if (newinventory.IsAddedSelected())
                {
                    if (newinventory.ToBeAddedItems.Count > 0)
                    {
                        Database.MoreInfoItemId = newinventory.ToBeAddedItems[0];
                        return RedirectToAction("MoreInfo", "DnD");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = messageAdded;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if (command.Equals("More Info"))
            {
                if (newinventory.IsSelected())
                {
                    if(newinventory.SelectedItems.Count > 0){
                        Database.MoreInfoItemId = newinventory.SelectedItems[0];
                        return RedirectToAction("MoreInfo", "DnD");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllItems = Database.getAllItems();
                    Inventory inventory = Database.getInventory();
                    inventory.moneyPouch = Database.GetMoneyPouch();
                    ViewBag.Inventory = inventory;
                    return View();
                }
            }
            else if(command.Equals("Save")){
                Database.UpdateMoneyPouch(newinventory.moneyPouch);
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
            int CharId = Database.CharId;
            string messageSelected = "Please choose atleast one item from your inventory first";
            string messageAdded = "Please choose atleast one item from the all items box";
            

            if (command.Equals("Remove"))
            {
                if (newspellBook.IsSelected())
                {
                    Database.RemoveSpells(newspellBook);
                }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllSpells = Database.getAllSpells();
                    ViewBag.MySpellBook = Database.getSpellbook(CharId);
                    return View();
                }
            }
            else if (command.Equals("Add"))
            {
                if (newspellBook.IsAddedSelected())
                {
                    Database.AddSpells(newspellBook);
                }
                else
                {
                    ViewBag.ErrorMessage = messageAdded;
                    ViewBag.AllSpells = Database.getAllSpells();
                    ViewBag.MySpellBook = Database.getSpellbook(CharId);
                    return View();
                }
            }
            else if (command.Equals("Prepare"))
            {
                if (newspellBook.IsSelected())
                {
                    Database.PrepareSpells(newspellBook, true);
                }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllSpells = Database.getAllSpells();
                    ViewBag.MySpellBook = Database.getSpellbook(CharId);
                    return View();
                }
            }
            else if (command.Equals("Unprepare"))
            {
                if (newspellBook.IsSelected())
                {
                    Database.PrepareSpells(newspellBook, false);
                }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllSpells = Database.getAllSpells();
                    ViewBag.MySpellBook = Database.getSpellbook(CharId);
                    return View();
                }
            }
            else if (command.Equals("More info"))
            {
                if (newspellBook.IsAddedSelected())
                {
                    Database.MoreInfoSpell = newspellBook.ToBeAddedSpells[0];
                    return RedirectToAction("MoreInfo", "DnD");
                }
                else
                {
                    ViewBag.ErrorMessage = messageAdded;
                    ViewBag.AllSpells = Database.getAllSpells();
                    ViewBag.MySpellBook = Database.getSpellbook(CharId);
                    return View();
                }
            }
            else if (command.Equals("More Info"))
            {
                if (newspellBook.IsSelected())
                {
                    Database.MoreInfoSpell = newspellBook.SelectedSpells[0];
                    return RedirectToAction("MoreInfo", "DnD");
                }
                else
                {
                    ViewBag.ErrorMessage = messageSelected;
                    ViewBag.AllSpells = Database.getAllSpells();
                    ViewBag.MySpellBook = Database.getSpellbook(CharId);
                    return View();
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