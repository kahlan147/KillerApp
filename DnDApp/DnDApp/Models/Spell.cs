using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DnDApp.Models
{
    public class Spell
    {
        public string SpellName { get; set; }
        public string SpellType { get; set; }
        public string Level { get; set; }
        public string SpellDescription { get; set; }
        public int NrOfDice { get; set; }
        public int DiceDamage { get; set; }
        public string Components { get; set; }
        public string Range { get; set; }
        public string CastTime { get; set; }
        public string Duration { get; set; }
        public bool Custom { get; set; }

        public bool Prepared { get; set; }
        private string preparedString {get{if(Prepared == true){return "Prepared";}else{return "Unprepared";}}}
        public string Showable{get{return SpellName + ", " + preparedString;}}

        public Spell()
        {

        }

        public Spell(DataRow SpellData)
        {
            this.SpellName = SpellData["SpellName"].ToString();
            this.SpellType = SpellData["SpellType"].ToString();
            this.Level = SpellData["Lvl"].ToString();
            this.SpellDescription = SpellData["SpellDescription"].ToString();
            if (SpellData["NumberOfDice"] is DBNull) { }
            else
            {
                this.NrOfDice = Convert.ToInt32(SpellData["NumberOfDice"].ToString());
            }
            if (SpellData["DiceDamage"] is DBNull) { }
            else
            {
                this.DiceDamage = Convert.ToInt32(SpellData["DiceDamage"].ToString());
            }
            this.Components = SpellData["Components"].ToString();
            this.Range = SpellData["ASRange"].ToString();
            this.CastTime = SpellData["CastingTime"].ToString();
            this.Duration = SpellData["Duration"].ToString();
            if (SpellData["Custom"].ToString() == "1")
            {
                this.Custom = true;
            }
            else
            {
                this.Custom = false;
            }
        }
    }
}