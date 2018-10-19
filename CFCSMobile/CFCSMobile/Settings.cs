using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CFCSMobile
{
    public static class Settings
    {
        static public string BASEURL = "http://192.168.225.128/CFCSMobileWebServices/api"; // VM running in Work Mac

        //static public string BASEURL = "http://192.168.1.250/CFCSMobileWebServices/api"; // Machine At Home


        //static public string BASEURL = "http://192.168.1.9/CFCSMobileWebServices/api"; // VM In Scrantoin Office At Home

        static public string USERNAME = "";
        static public string FIRSTNAME = "";
        static public string LASTNAME = "";
        static public string DOB = "";
        static public bool LOGGEDIN = false;

        static public string MOTD = "";

        static public Color EvenColor = Color.FromHex("#FFB0EEB0");
        static public Color OddColor = Color.LightYellow;

        static public Color ReferralBackgroundColor = Color.FromHex("#C8F1FC");
        static public Color StaffBackgroundColor = Color.FromHex("#C8FCDF");

        static public Color LabelColorForValueFields = Color.FromHex("#9090AO");

        static public TheLookups Lookups = null;
    }
}
