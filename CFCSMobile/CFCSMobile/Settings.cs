using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CFCSMobile
{
    public static class Settings
    {

        static public string BASEURL = "http://192.168.225.128/CFCSMobileWebServices/api"; // VM running in Work Mac

        //static public string BASEURL = "http://192.168.193.128/CFCSMobileWebServices/api"; // VM running in Linux Host Machine
        
        //static public string BASEURL = "http://192.168.1.250/CFCSMobileWebServices/api"; // Machine At Home


        //static public string BASEURL = "http://192.168.1.9/CFCSMobileWebServices/api"; // VM In Scrantoin Office At Home

        static public string USERNAME = "";
        static public string FIRSTNAME = "";
        static public string LASTNAME = "";
        static public string DOB = "";
        static public string MEMBERID = "";
        static public string ADDRESS1 = "";
        static public string ADDRESS2 = "";
        static public string CITY = "";
        static public string STATE = "";
        static public string ZIP = "";
        static public string LOGTYPE = "";

        static public bool LOGGEDIN = false;

        static public string MOTD = "";

        static public Color EvenColor = Color.FromHex("#FFB0EEB0");
        static public Color OddColor = Color.LightYellow;

        static public Color ReferralBackgroundColor = Color.FromHex("#C8F1FC");
        static public Color StaffBackgroundColor = Color.FromHex("#C8FCDF");

        static public Color LabelColorForValueFields = Color.FromHex("#9090AO");

        static public Color EditFieldBackgroundColor = Color.White;

        static public Color ButtonBackgroundColor = Color.FromHex("#C0C000");

        static public TheLookups Lookups = null; // Will get Populated at Startup after login
    }
}
