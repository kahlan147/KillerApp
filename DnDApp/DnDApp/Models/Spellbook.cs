using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDApp.Models
{
    public class Spellbook
    {
        public List<Spell> MySpells { get; set; }
        public List<string> SelectedSpells { get; set; }
        public List<string> ToBeAddedSpells { get; set; }

        public Spellbook()
        {
            MySpells = new List<Spell>();
            SelectedSpells = new List<string>();
            ToBeAddedSpells = new List<string>();
        }

        public bool IsSelected()
        {
            if (this.SelectedSpells.Count <= 0)
            {
                return false;
            }
            else { return true; }
        }

        public bool IsAddedSelected()
        {
            if (this.ToBeAddedSpells.Count <= 0)
            {
                return false;
            }
            else { return true; }
        }
    }
}