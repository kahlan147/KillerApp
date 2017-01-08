using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDApp.Models
{
    interface Iinformation
    {
        string Name { get; set; }
        string Description { get; set; }
        int NrOfDice { get; set; }
        int DiceDamage { get; set; }
        bool Custom { get; set; }
        string Showable { get; }
        string Dice { get; }
    }
}
