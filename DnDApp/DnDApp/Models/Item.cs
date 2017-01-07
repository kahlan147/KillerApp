using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DnDApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsMagical { get; set; }
        public bool Wearable { get; set; }
        public int NrOfDice { get; set; }
        public int DiceDamage { get; set; }
        public bool Custom { get; set; }

        public int Amount { get; set; }
        public string NameAmount { get { return Amount.ToString() + ", " + Name; } }

        public Item(int id, string name, string description, bool isMagical, bool wearable, int nrOfDice, int diceDamage, bool custom)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.IsMagical = isMagical;
            this.Wearable = wearable;
            this.NrOfDice = nrOfDice;
            this.DiceDamage = diceDamage;
            this.Custom = custom;
        }

        public Item(DataRow ItemRow)
        {
            this.Id = Convert.ToInt32(ItemRow["ItemId"]);
            this.Name = ItemRow["ItemName"].ToString();
            this.Description = ItemRow["ItemDescription"].ToString();
            if (ItemRow["NumberOfDice"] is DBNull || Convert.ToInt32(ItemRow["NumberOfDice"]) == 0)
            {
                this.NrOfDice = 0;
            }
            else
            {
                this.NrOfDice = Convert.ToInt32(ItemRow["NumberOfDice"]);
            }

            if (ItemRow["DiceDamage"] is DBNull || Convert.ToInt32(ItemRow["DiceDamage"]) == 0)
            {
                this.DiceDamage = 0;
            }
            else
            {
                this.DiceDamage = Convert.ToInt32(ItemRow["DiceDamage"]);
            }

            if (ItemRow["IsMagical"] is DBNull || Convert.ToInt32(ItemRow["IsMagical"]) == 0)
            {
            this.IsMagical = false;
            }
            else
            {
                this.IsMagical = true;
            }

            if (ItemRow["IsWearable"] is DBNull || Convert.ToInt32(ItemRow["IsWearable"]) == 0)
            {
                this.Wearable = false;
            }
            else
            {
                this.Wearable = true;
            }

            if (ItemRow["Custom"] is DBNull || Convert.ToInt32(ItemRow["Custom"]) == 0)
            {
                this.Custom = false;
            }
            else
            {
                this.Custom = true;
            }
        }
    }
}