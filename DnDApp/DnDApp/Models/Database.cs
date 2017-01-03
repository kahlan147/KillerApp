using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DnDApp.Models
{
    public static class Database
    {
        static SqlConnection sqlConnection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(Connection);
                return sqlConnection;
            }
        }
        private static string Connection
        {
            get
            {
                string connection = "Server=KAHLAN;" + "Trusted_Connection=yes;" +
            "database=D&DApp; " + "connection timeout=15";
                return connection;
            }
        }

        public static int CharId { get; set; }
        public static string LoggedUser { get; set; }


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

        public static void UpdateCharacter()
        {

        }

        public static void SaveCharacter()
        {
           /* string commandString @"INSERT INTO GameCharacter VALUES
            ('" + character.CharName + "', '" + character.RaceName + "', '"  + character.ClassName+"', '"  + character.CharLevel + "', '"  + character.Speed + "', '"  
              + character.StrScore + "', '" + character.DexScore + "', '"  + character.ConScore + "', '" + character.IntScore + "', '"  + character.WisScore + "', '"  + 
              character.ChaScore + "', '"  + character.ArmorClass + "', '"  + character.Initiative + "', '"  + character.Inspiration + "', '"  + character.CurHealth + "', '"  
              + character.MaxHealth + "', '"  + character.TrainedSkills + "', '"  + character.TrainedSavingThrows + "', '"  + 0 + "', '"  + 0"')";
            */
            string commandString = "INSERT INTO GameCharacter VALUES ('Test', 'Halfling', 'Rogue',1, 6, 14, 18,10, 10, 12, 17, 13, 4, 0, 12,12, 23452,0, 0, 0)";
            DataTable empty= General(commandString);
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
            foreach (DataRow rol in result.Rows)
            {
                Character character = new Character();
                character.CharId = Convert.ToInt32(rol["CharId"]);
                character.CharName = rol["CharName"].ToString();
                character.ClassName = rol["CharClass"].ToString();
                character.RaceName = rol["CharRace"].ToString();
                character.CharLevel = Convert.ToInt32(rol["CharLevel"]);
                Characters.Add(character);
            }
            return Characters;
        }
    }
}