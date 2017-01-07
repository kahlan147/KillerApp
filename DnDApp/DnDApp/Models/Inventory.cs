using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DnDApp.Models
{
    public class Inventory
    {
        public List<Item> MyItems { get; set; }
        public List<string> SelectedItems { get; set; }
        public List<string> ToBeAddedItems { get; set; }

        public Inventory()
        {

        }
    }
}