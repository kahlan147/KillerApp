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

        }
    }
}