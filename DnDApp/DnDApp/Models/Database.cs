using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DnDApp.Models
{
    public class Database
    {
        protected SqlConnection sqlConnection {get; set;}
        private string Connection {get; set;}

        public Database()
        {
            Connection = "Server=KAHLAN;" + "Trusted_Connection=yes;" +
            "database=D&DApp; " + "connection timeout=15";
            sqlConnection = new SqlConnection(Connection);
        }

        private DataTable General(string commandString)
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

        public DataRow GetCharacter(int CharId)
        {
            string commandString = @"SELECT * FROM GetCharacter(" + CharId + ")";

            DataTable Character = General(commandString);
            return Character.Rows[0];

        }

        public DataRow GetCharacterInfo(int CharId)
        {
            string commandString = @"SELECT * FROM GameCharInfo WHERE CharId = '" + CharId + "'";

            DataTable CharacterInfo = General(commandString);
            return CharacterInfo.Rows[0];
        }

        public DataTable getInventory(int CharId)
        {
            string commandString = @"SELECT * FROM Inventory WHERE CharId = '" + CharId + "'";
            DataTable Inventory = General(commandString);
            return Inventory;
        }

        public DataRow getItem(int ItemId)
        {
            string commandString = @"SELECT * FROM Item WHERE ItemId = '" + ItemId + "'";
            DataTable ItemTable = General(commandString);
            DataRow ItemRow = ItemTable.Rows[0];
            return ItemRow;
        }

        public void UpdateCharacter()
        {

        }

        public void SaveCharacter()
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
    }
}