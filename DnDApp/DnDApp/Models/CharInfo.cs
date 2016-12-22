using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DnDApp.Models
{
    public class CharInfo
    {
        public int CharId { get; set; }
        public int CharAge { get; set; }
        public string CharHeight { get; set; }
        public string CharWeight { get; set; }
        public string CharEyes { get; set; }
        public string CharSkin { get; set; }
        public string CharHair { get; set; }
        public string CharDescription { get; set; }
        public string CharBackground { get; set; }

        public CharInfo(DataRow CharacterInfo)
        {
            CharId = Convert.ToInt32(CharacterInfo["CharId"]);
            if (CharacterInfo["CharAge"] is DBNull)
            {
                this.CharAge = 0;
            }
            else
            {
                this.CharAge = Convert.ToInt32(CharacterInfo["CharAge"]);
            }
            this.CharHeight = CharacterInfo["CharHeight"].ToString();
            this.CharWeight = CharacterInfo["CharWeight"].ToString();
            this.CharEyes = CharacterInfo["CharEyes"].ToString();
            this.CharSkin = CharacterInfo["CharSkin"].ToString();
            this.CharHair = CharacterInfo["CharHair"].ToString();
            this.CharDescription = CharacterInfo["CharDescription"].ToString();
            this.CharBackground = CharacterInfo["CharBackground"].ToString();
            isAllNull();
        }

        private void isAllNull()
        {
            if (this.CharAge == null)
            {
                CharAge = 0;
            }
            if (this.CharHeight == null)
            {
                CharHeight = "";
            }
            if (this.CharWeight == null)
            {
                CharWeight = "";
            }
            if (this.CharEyes == null)
            {
                CharEyes = "";
            }
            if (this.CharSkin == null)
            {
                CharSkin = "";
            }
            if (this.CharHair == null)
            {
                CharHair = "";
            }
            if (this.CharDescription == null)
            {
                CharDescription = "";
            }
            if (this.CharBackground == null)
            {
                CharBackground = "";
            }
        }
    }
}