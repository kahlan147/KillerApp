using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DnDApp.Models
{
    public class Inventory : ILister
    {
        public List<Item> MyItems { get; set; }
        public List<int> SelectedItems { get; set; }
        public List<int> ToBeAddedItems { get; set; }
        public MoneyPouch moneyPouch { get; set; }

        public Inventory()
        {
            MyItems = new List<Item>();
            SelectedItems = new List<int>();
            ToBeAddedItems = new List<int>();
            moneyPouch = new MoneyPouch();
        }

        public bool IsSelected()
        {
            if (this.SelectedItems.Count <= 0)
            {
                return false;
            }
            else { return true; }
        }

        public bool IsAddedSelected()
        {
            if (this.ToBeAddedItems.Count <= 0)
            {
                return false;
            }
            else { return true; }
        }
    }
}