using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDApp.Models
{
    public class CreateChar
    {
        public List<string> options { get; set; }
        public int step { get; set; }

        public CreateChar()
        {
            options = new List<string>();
            options.Add("test");
        }
    }
}