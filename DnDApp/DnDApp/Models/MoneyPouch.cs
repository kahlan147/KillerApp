using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DnDApp.Models
{
    public class MoneyPouch
    {
        public int Platinum { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Copper { get; set; }

        public MoneyPouch()
        {
            this.Platinum = 0;
            this.Gold = 0;
            this.Silver = 0;
            this.Copper = 0;
        }

        public MoneyPouch(DataRow dataRow)
        {
            this.Platinum = Convert.ToInt32(dataRow["Coin_Platinum"].ToString());
            this.Gold = Convert.ToInt32(dataRow["Coin_Gold"].ToString());
            this.Silver = Convert.ToInt32(dataRow["Coin_Silver"].ToString());
            this.Copper = Convert.ToInt32(dataRow["Coin_Copper"].ToString());
        }
    }
}