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
        public int NumberOfDice { get; set; }
        public int DiceDamage { get; set; }
        public string Components { get; set; }
        public string Range { get; set; }
        public string CastTime { get; set; }
        public string Duration { get; set; }
        public bool Custom { get; set; }

        public Spell()
        {

        }

        public Spell(DataRow SpellData)
        {
            this.SpellName = SpellData["SpellName"].ToString();
            this.SpellType = SpellData["SpellType"].ToString();
            this.Level = SpellData["Lvl"].ToString();
            this.SpellDescription = SpellData["SpellDescription"].ToString();
            this.NumberOfDice = Convert.ToInt32(SpellData["NumberOfDice"].ToString());
            this.DiceDamage = Convert.ToInt32(SpellData["DiceDamage"].ToString());
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