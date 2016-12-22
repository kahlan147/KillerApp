using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DnDApp.Models
{
    public class Character
    {
        public string CharName { get; set; }
        public string RaceName { get; set; }
        public string ClassName { get; set; }
        public int CharLevel { get; set; }
        public int Speed { get; set; }
        public int StrScore { get; set; }
        public int DexScore { get; set; }
        public int ConScore { get; set; }
        public int IntScore { get; set; }
        public int WisScore { get; set; }
        public int ChaScore { get; set; }
        public int ArmorClass { get; set; }
        public int Initiative { get; set; }
        public int Inspiration { get; set; }
        public int CurHealth { get; set; }
        public int MaxHealth { get; set; }
        public int TrainedSkills { get; set; }
        public int TrainedSavingThrows { get; set; }
        public bool PlayerOrNpc { get; set; }
        public bool FriendlyEnemy { get; set; }

        public Character(DataRow CharacterData)
        {
            this.CharName = CharacterData["CharName"].ToString();
            this.ClassName = CharacterData["ClassName"].ToString();
            this.RaceName = CharacterData["RaceName"].ToString();

            this.StrScore = Int32.Parse(CharacterData["StrScore"].ToString());
            this.DexScore = Int32.Parse(CharacterData["DexScore"].ToString());
            this.ConScore = Int32.Parse(CharacterData["ConScore"].ToString());
            this.IntScore = Int32.Parse(CharacterData["IntScore"].ToString());
            this.WisScore = Int32.Parse(CharacterData["WisScore"].ToString());
            this.ChaScore = Int32.Parse(CharacterData["ChaScore"].ToString());

            if (CharacterData["Speed"] is DBNull)
            {
                Speed = 0;
            }
            else
            {
                this.Speed = Int32.Parse(CharacterData["Speed"].ToString());
            }

            if (CharacterData["ArmorClass"] is DBNull)
            {
                ArmorClass = 0;
            }
            else
            {
                this.ArmorClass = Int32.Parse(CharacterData["ArmorClass"].ToString());
            }

            if (CharacterData["Initiative"] is DBNull)
            {
                Initiative = 0;
            }
            else
            {
                this.Initiative = Int32.Parse(CharacterData["Initiative"].ToString());
            }

            if (CharacterData["Inspiration"] is DBNull)
            {
                Inspiration = 0;
            }
            else
            {
                this.Inspiration = Int32.Parse(CharacterData["Inspiration"].ToString());
            }

            this.CurHealth = Int32.Parse(CharacterData["CurHealth"].ToString());
            this.MaxHealth = Int32.Parse(CharacterData["MaxHealth"].ToString());
            this.TrainedSkills = Int32.Parse(CharacterData["TrainedSkills"].ToString());
            this.TrainedSavingThrows = Int32.Parse(CharacterData["TrainedSavingThrows"].ToString());
            
        }
    }
}