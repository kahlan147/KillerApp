using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDApp.Models
{
    public class AppUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AppUser()
        {

        }

        public AppUser(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
    }
}