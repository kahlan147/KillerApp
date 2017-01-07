using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace DnDApp.Models
{
    public static class Database
    {
        public static int CharId { get; set; }
        public static string LoggedUser { get; set; }
        public static Character currentCharacter { get; set; }
        public static int MoreInfoItemId { get; set; }
        public static string MoreInfoSpell { get; set; }

        private static string Connection
        {
            get
            {
                string connection = "Server=KAHLAN;" + "Trusted_Connection=yes;" +
            "database=D&DApp; " + "connection timeout=15";
                return connection;
            }
        }

        static SqlConnection sqlConnection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(Connection);
                return sqlConnection;
            }
        }

        private static DataTable General(string commandString)
        {
            DataTable dt = new DataTable();

            using (SqlCommand command = new SqlCommand(commandString, sqlConnection))
            {

                using (SqlDataAdapter Adapter = new SqlDataAdapter(commandString, sqlConnection))
                {

                    Adapter.Fill(dt);
                }
            }
            return dt;
        }

        public static bool AllowedToBeHere(string Permission)
        {
            if (LoggedUser == null)
            {
                return false;
            }
            else
            {

                if (Permission == "low")
                {
                    return true;
                }
                else if (Permission == "high")
                {
                    string commandString = @"SELECT count(*) as Result FROM UserCharacters WHERE UserName = '"+ LoggedUser +"' AND CharId = "+CharId+";";
                    DataTable result = General(commandString);
                    DataRow resultRow = result.Rows[0];
                    if (Convert.ToInt32(resultRow["Result"]) >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static DataRow GetCharacter(int CharId)
        {
            string commandString = @"SELECT * FROM GetCharacter(" + CharId + ")";

            DataTable Character = General(commandString);
            return Character.Rows[0];

        }

        public static DataRow GetCharacterInfo(int CharId)
        {
            string commandString = @"SELECT * FROM GameCharInfo WHERE CharId = '" + CharId + "'";

            DataTable CharacterInfo = General(commandString);
            return CharacterInfo.Rows[0];
        }

        public static Inventory getInventory(int CharId)
        {
            Inventory inventory = new Inventory();
            string commandString = @"SELECT * FROM Inventory WHERE CharId = '" + CharId + "';";
            DataTable InventoryTable = General(commandString);
            foreach (DataRow InventoryRow in InventoryTable.Rows)
            {
                int ItemId = Convert.ToInt32(InventoryRow["ItemId"]);
                int Amount = Convert.ToInt32(InventoryRow["Amount"]);
                DataRow ItemRow = Database.getItem(ItemId);
                Models.Item newItem = new Models.Item(ItemRow);
                newItem.Amount = Amount;
                inventory.MyItems.Add(newItem);
            }
            return inventory;
        }

        public static Spellbook getSpellbook(int CharId)
        {
            Spellbook spellbook = new Spellbook();
            string commandString = @"SELECT * FROM Spellbook WHERE CharId = '" + CharId + "';";
            DataTable spellbookTable = General(commandString);
            foreach (DataRow spellbookRow in spellbookTable.Rows)
            {
                string SpellName = spellbookRow["SpellName"].ToString();
                DataRow SpellRow = getSpell(SpellName);
                Spell newSpell = new Spell(SpellRow);
                if (spellbookRow["Prepared"].ToString() == "1")
                {
                    newSpell.Prepared = true;
                }
                else
                {
                    newSpell.Prepared = false;
                }
                spellbook.MySpells.Add(newSpell);
            }
            return spellbook;
        }

        public static DataRow getSpell(string SpellName)
        {
            string commandString = @"SELECT * FROM Spell WHERE SpellName = '" + SpellName + "';";
            DataTable SpellList = General(commandString);
            return SpellList.Rows[0];
        }

        public static DataRow getItem(int ItemId)
        {
            string commandString = @"SELECT * FROM Item WHERE ItemId = '" + ItemId + "'";
            DataTable ItemTable = General(commandString);
            DataRow ItemRow = ItemTable.Rows[0];
            return ItemRow;
        }

        public static void UpdateCharacter(Character newCharacter)
        {
            string commandString = @"UPDATE GameCharacter 
            SET CharName = '" + newCharacter.CharName + "', RaceName = '" + newCharacter.RaceName + "', ClassName = '" + newCharacter.ClassName + "', CharLevel = " + newCharacter.CharLevel +
            ", Speed = " + newCharacter.Speed + ", StrScore = " + newCharacter.StrScore + ", DexScore = " + newCharacter.DexScore + ", ConScore = " + newCharacter.ConScore + ", IntScore = " + newCharacter.IntScore +
            ", WisScore = " + newCharacter.WisScore + ", ChaScore = " + newCharacter.ChaScore + ", ArmorClass = " + newCharacter.ArmorClass + ", Initiative = " + newCharacter.Initiative +
            ", Inspiration = " + newCharacter.Inspiration + ", CurHealth = " + newCharacter.CurHealth + ", MaxHealth= " + newCharacter.MaxHealth +
            ", TrainedSkills = " + newCharacter.TrainedSkills + ", TrainedSavingThrows = " + newCharacter.TrainedSavingThrows +
            " WHERE CharId = " + newCharacter.CharId + ";";
            DataTable Empty = General(commandString);
            commandString = @"UPDATE GameCharInfo
            SET CharAge = " + newCharacter.CharAge + ", CharHeight= '" + newCharacter.CharHeight + "', CharWeight='" + newCharacter.CharWeight +
            "', CharEyes = '" + newCharacter.CharEyes + "', CharSkin= '" + newCharacter.CharSkin + "', CharHair='" + newCharacter.CharHair + 
            "' WHERE CharId=" + newCharacter.CharId + ";";
            Empty = General(commandString);
        }

        public static void SaveNewCharacter(Character character)
        {
            //Create a character in the GameCharacter table
            string commandString = @"INSERT INTO GameCharacter VALUES ('" + character.CharName + "', '" + character.RaceName + "', '"  + character.ClassName + "', " +
                character.CharLevel + ", "  + character.Speed + ", " + character.StrScore + ", " + character.DexScore + ", "  + character.ConScore + ", " + 
                character.IntScore + ", "  + character.WisScore + ", "  + character.ChaScore + ", "  + 0 + ", "  + 0 + ", "  + 0 + ", "  + 0 + ", " + 0 + 
                ", "  + 0 + ", "  + 0 + ", "  + 0 + ", "  + 0 + ")";
            
            //get the, by de database given, charid of the added character
            DataTable results= General(commandString);
            commandString = @"SELECT CharId FROM GameCharacter WHERE CharName = '" + character.CharName + "' AND RaceName = '" + character.RaceName + 
                "' AND ClassName = '" + character.ClassName + "'AND CharLevel = "+ character.CharLevel + " AND Speed = " + character.Speed + " AND StrScore = " +
                    character.StrScore + " AND DexScore = " + character.DexScore + " AND ConScore = " + character.ConScore + " AND IntScore = " + character.IntScore +
                    "AND WisScore = " + character.WisScore + " AND ChaScore = " + character.ChaScore + ";";
            results = General(commandString);
            //bind the character to the loggeduser
            DataRow resultRow = results.Rows[0];
            int charId = Convert.ToInt32(resultRow["CharId"]);
            commandString = @"INSERT INTO UserCharacters VALUES ('" + LoggedUser + "', " + charId + ");";
            results = General(commandString);
        }

        public static bool LogIn(AppUser appUser)
        {
            string commandString = "SELECT dbo.LoggingIn('"+ appUser.UserName + "', '"+ appUser.Password +"') as correctlogin;";
            DataTable result = General(commandString);
            DataRow resultRow = result.Rows[0];
            int resultint = Convert.ToInt32(resultRow["correctlogin"]);
            if (resultint == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Register(AppUser appUser)
        {
            string commandString = "SELECT dbo.AccountExists('" + appUser.UserName + "') as exist;";
            DataTable result = General(commandString);
            DataRow resultRow = result.Rows[0];
            if (Convert.ToInt32(resultRow["exist"]) == 0)
            {
                commandString = "execute Register '" + appUser.UserName + "', '" + appUser.Password + "', 1;";
                result = General(commandString);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Character> getCharactersFrom()
        {
            List<Character> Characters = new List<Character>();
            string commandString = "SELECT * FROM dbo.GetCharactersFrom('" + LoggedUser + "');";
            DataTable result = General(commandString);
            foreach (DataRow row in result.Rows)
            {
                Character character = new Character();
                character.CharId = Convert.ToInt32(row["CharId"]);
                character.CharName = row["CharName"].ToString();
                character.ClassName = row["CharClass"].ToString();
                character.RaceName = row["CharRace"].ToString();
                character.CharLevel = Convert.ToInt32(row["CharLevel"]);
                Characters.Add(character);
            }
            return Characters;
        }

        public static List<string> getAllClasses()
        {
            List<string> classes = new List<string>();
            string commandString = "SELECT ClassName FROM Class;";
            DataTable result = General(commandString);
            foreach (DataRow row in result.Rows)
            {
                classes.Add(row["ClassName"].ToString());
            }
            return classes;
        }

        public static List<string> getAllRaces()
        {
            List<string> Races = new List<string>();
            string commandString = "SELECT RaceName FROM Race;";
            DataTable result = General(commandString);
            foreach (DataRow row in result.Rows)
            {
                Races.Add(row["RaceName"].ToString());
            }
            return Races;
        }

        public static List<Item> getAllItems()
        {
            List<Item> AllItems = new List<Item>();
            string commandString = "SELECT * FROM Item;";
            DataTable result = General(commandString);
            foreach (DataRow row in result.Rows)
            {
                Item newItem = new Item(row);
                AllItems.Add(newItem);
            }

            return AllItems;
        }

        public static List<Spell> getAllSpells()
        {
            List<Spell> SpellList = new List<Spell>();
            string commandString = "SELECT * FROM Spell;";
            DataTable result = General(commandString);
            foreach(DataRow row in result.Rows){
                Spell newSpell = new Spell(row);
                SpellList.Add(newSpell);
            }
            return SpellList;
        }

        public static void DeleteCharacter(Character character)
        {
            string commandString = @"execute DeleteCharacter " + character.CharId + ";";
            DataTable empty = General(commandString);
        }

        public static void IncDecItemInv(Inventory inventory, bool IncDec, int amount)
        {
            if (inventory.SelectedItems.Count > 0)
            {
                string commandString;
                string IncreaseOrDecrease = "+";
                if (IncDec == true)
                {
                    IncreaseOrDecrease = "+";
                }
                else
                {
                    IncreaseOrDecrease = "-";
                }
                foreach (int item in inventory.SelectedItems)
                {
                    commandString = @"UPDATE Inventory
                                SET Amount = (SELECT Amount FROM Inventory WHERE CharId = " + CharId + " AND ItemId = " + item + ") " + IncreaseOrDecrease + amount +
                                    "WHERE CharId =  " + CharId + "  AND ItemId = " + item + ";";
                    General(commandString);
                }
            }
        }

        public static void AddItems(Inventory inventory)
        {
            if (inventory.ToBeAddedItems.Count > 0)
            {
                foreach (int itemId in inventory.ToBeAddedItems)
                {
                    string commandString = @"INSERT INTO Inventory VALUES (" + CharId + ", " + itemId + ",1 , 0);";
                    General(commandString);
                }
            }
        }

        public static void RemoveItems(Inventory inventory)
        {
            if (inventory.SelectedItems.Count > 0)
            {
                foreach (int itemId in inventory.SelectedItems)
                {
                    string commandString = @"DELETE FROM Inventory WHERE CharId = " + CharId + " AND ItemId = " + itemId + ";";
                    General(commandString);
                }
            }
        }

        public static void AddSpells(Spellbook spellbook)
        {
            if (spellbook.ToBeAddedSpells.Count > 0)
            {
                foreach (string SpellName in spellbook.ToBeAddedSpells)
                {
                    string commandString = @"INSERT INTO Spellbook VALUES (" + CharId + ", '" + SpellName + "', 0);";
                    General(commandString);
                }
            }
        }

        public static void RemoveSpells(Spellbook spellbook)
        {
            if (spellbook.SelectedSpells.Count > 0)
            {
                foreach (string SpellName in spellbook.SelectedSpells)
                {
                    string commandString = @"DELETE FROM Spellbook WHERE CharId = " + CharId + " AND SpellName = '" + SpellName + "';";
                    General(commandString);
                }
            }
        }

        public static void PrepareSpells(Spellbook spellbook, bool Prepared)
        {
            if (spellbook.SelectedSpells.Count > 0)
            {
                int prepared = 0;
                if (Prepared == true)
                {
                    prepared = 1;
                }
                foreach (string SpellName in spellbook.SelectedSpells)
                {
                    string commandString = @"UPDATE Spellbook SET Prepared = " + prepared + " WHERE CharId = " + CharId + " AND SpellName = '" + SpellName + "';";
                    General(commandString);
                }
            }
        }
    }
}