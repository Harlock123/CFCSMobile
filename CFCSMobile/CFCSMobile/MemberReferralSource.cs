using System;
using System.Collections.Generic;
using System.Text;

namespace CFCSMobile
{
    public class MemberReferralSource
    {
        public long RSID { get; set; }
        public string RSFIRSTNAME { get; set; }
        public string RSLASTNAME { get; set; }
        public string RSROLE { get; set; }
        public string RSROLEDESCRIPTION { get; set; }
        public bool RSISAGENCY { get; set; }
        public long RSAGENCYID { get; set; }
        public string RSADDRESS1 { get; set; }
        public string RSADDRESS2 { get; set; }
        public string RSADDRESS3 { get; set; }
        public string RSCITY { get; set; }
        public string RSSTATE { get; set; }
        public string RSZIP { get; set; }
        public string RSCOUNTY { get; set; }
        public string RSEMAIL { get; set; }
        public string RSHOMEPHONE { get; set; }
        public string RSWORKPHONE { get; set; }
        public bool RSACTIVE { get; set; }
        public string RSCREATEDBY { get; set; }
        public DateTime RSCREATEDDATE { get; set; }
        public string SSN { get; set; }
        public long RSCASEMANAGER { get; set; }
        public string AGENCYDESC { get; set; }
        public DateTime RSSTARTDATE { get; set; }
        public DateTime RSENDDATE { get; set; }
        public string RSFULLNAME { get; set; }
        public string ROLEFULLNAME { get; set; }
        public string RSUSERNAME { get; set; }
        public string ISEDITPERSON { get; set; }
        public string strRSSTARTDATE { get; set; }
        public string strRSENDDATE { get; set; }

        public string RSEXTENSION { get; set; }

        public MemberReferralSource()
        {
            RSID = 0;
            RSCASEMANAGER = 0;
            RSFIRSTNAME = "";
            RSLASTNAME = "";
            RSROLE = "";
            RSROLEDESCRIPTION = "";
            RSISAGENCY = false;
            RSAGENCYID = 0;
            RSADDRESS1 = "";
            RSADDRESS2 = "";
            RSADDRESS3 = "";
            RSCITY = "";
            RSSTATE = "";
            RSZIP = "";
            RSCOUNTY = "";
            RSEMAIL = "";
            RSHOMEPHONE = "";
            RSWORKPHONE = "";
            RSACTIVE = false;
            RSCREATEDBY = "";
            RSCREATEDDATE = Convert.ToDateTime(null);
            SSN = "";
            AGENCYDESC = "";
            RSSTARTDATE = Convert.ToDateTime(null);
            RSENDDATE = Convert.ToDateTime(null);
            RSFULLNAME = "";
            ROLEFULLNAME = "";
            RSUSERNAME = "";
            ISEDITPERSON = "";
            strRSSTARTDATE = "";
            strRSENDDATE = "";
            RSEXTENSION = "";
        }
    }
}
