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

        public static DataTable getInventory(int CharId)
        {
            string commandString = @"SELECT * FROM Inventory WHERE CharId = '" + CharId + "'";
            DataTable Inventory = General(commandString);
            return Inventory;
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
            string commandString = @"UPDATE GameCharInfo
            SET CharAge = " + newCharacter.CharAge + ", CharHeight= '" + newCharacter.CharHeight + "', CharWeight='" + newCharacter.CharWeight +
            "', CharEyes = '" + newCharacter.CharEyes + "', CharSkin= '" + newCharacter.CharSkin + "', CharHair='" + newCharacter.CharHair + 
            "' WHERE CharId=" + newCharacter.CharId + ";";
            DataTable Empty = General(commandString);
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

        public static void DeleteCharacter(Character character)
        {
            string commandString = @"execute DeleteCharacter " + character.CharId + ";";
            DataTable empty = General(commandString);
        }
    }
}