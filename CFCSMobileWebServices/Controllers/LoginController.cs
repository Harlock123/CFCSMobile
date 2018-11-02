using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;

namespace CFCSMobileWebServices.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        [Route("api/Login/GetLoginDetails")]
        [HttpGet]
        public JsonResult<UserLogins>GetLoginDetails()
        {
            bool res = DBLocked();

            return Json(InternalGetLoginDetails("lwatson"));
        }

        [Route("api/Login/MOTD")]
        [HttpGet]
        public JsonResult<string> MOTD()
        {

            string uname = "UNSET";

            var req = Request;

            var rhead = req.Headers;

            if (rhead.Contains("LOGIN"))
            {
                uname = rhead.GetValues("LOGIN").First();
            }


            string result = "This Message of the day was retrieved directly from the server " +
                "and illustrates a convienient method for getting the word out to the Mobile " +
                "user community supported by the CFCS System. These may be targeted to specific " +
                "users or roles of users for user " + uname;

            return Json(result);

        }


        [Route("api/Login/DoLogin")]
        [HttpGet]
        public JsonResult<LoginResult>DoLogin()
        {
            bool res = DBLocked();

            string uname = "";
            string pw = "";

            var req = Request;

            var rhead = req.Headers;

            if (rhead.Contains("LOGIN"))
            {
                uname = rhead.GetValues("LOGIN").First();
            }

            if (rhead.Contains("PW"))
            {
                pw = rhead.GetValues("PW").First();
            }
                                 
            LoginResult result = new LoginResult();

            result.Success = false;

            if (USERAuthorized(uname, pw))
            {
                UserLogins log = InternalGetLoginDetails(uname);

                result.Address1 = log.Address1;
                result.Address2 = log.Address2;
                result.City = log.City;
                result.FirstName = log.FirstName;
                result.LastName = log.LastName;
                result.State = log.State;
                result.Success = true;
                result.UserName = uname;
                result.ZipCode = log.ZipCode;

            }

            return Json(result);
        }

        // GET: api/Login/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }

        private bool USERAuthorized(string uname, string encryptedpw)
        {
            bool result = false;

            try
            {
                string sql = "SELECT FIRSTNAME from TBLUSERLOGINS " +
                    "WHERE USERNAME = @UNAME AND USERPASSWORD = @PW AND (LOGINATTEMPTS <= 3 OR LOGINATTEMPTS IS NULL) " +
                    "AND DEACTIVE <> 'Y'"; 

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.Add("@UNAME", System.Data.SqlDbType.VarChar, 20, "USERNAME").Value = uname;
                cmd.Parameters.Add("@PW", System.Data.SqlDbType.VarChar, 100, "USERPASSWORD").Value = encryptedpw;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    result = true; // we got a login that passes password checks and is not deactivated or locked
                }

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                               
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }

            return result;
        }

        public UserLogins InternalGetLoginDetails(string Username)
        {
            UserLogins log = new UserLogins();

            try
            {
                //LogError("GetLoginDetails", "Made It Here");

                string sql = "SELECT USERNAME,FIRSTNAME,LASTNAME,ORGANIZATION," +
                    "PASSWORDCHANGEDATE,DEACTIVE,NOTE,ReadOnlyMemberDemo, " +
                    "ReadOnlyAuthorizations,ReadOnlyProgressNotes,GlobalGroupAccess, tblUserLoginID,LastGlobalMesssageDate,LastWhatsNewDate, " +
                    "Address1,Address2,City,State,ZipCode,Email2,Gender,Religion,Race,Email,ContactNum,CredentialType1,CredentialType2,ClinicalLicenseNumber,BCBANumber,IndividualNPINumber, " +
                    "PhysicalAbuse,SexualAbuse,ADHD,AdoptionFoster,AngerManagement,AppliedBehavior,ArtTherapy,Autism,BehaviorModification, " +
                    "CognitiveBehavior,DevDisabled,DomesticViolence,EatingDisorder,EMDR,EvidenceBased,FaithChrisitian,FaithJewish, " +
                    "FaithOther,FamilyTherapy,GangInvolvement,JuvenileJustice,LearningDisability,LGBTIssues,ParentingSkills, " +
                    "PlayTherapy,PTSD,SelfMutilatition,SexOffenders,SexualBoundaryIssues,SocialSkillsTraining,SubstanceAbuse, " +
                    "TraumaIssues,VocationalSkillsTraining,AcceptTexts,LATITUDE,LONGITUDE,MAPGROUP,TOOLTIP,GEO,OON,CP1,CP2,CP3,CP4,CP5,CP6,CP7,CP8,CP9,CP10 " +
                    "FROM TBLUSERLOGINS WHERE " +
                    "USERNAME = @UNAME";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = Username;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                bool found = false;

                while (r.Read())
                {
                    found = true;

                    log.Username = r[0] + "";

                    log.FirstName = r[1] + "";

                    log.LastName = r[2] + "";

                    log.Organization = r[3] + "";

                    log.GlobalOrganization = GetGlobalProviderID();

                    if (!r.IsDBNull(4))
                        log.PasswordChangeDate = r.GetDateTime(4);
                    else
                        log.PasswordChangeDate = DateTime.MinValue;

                    log.Deactive = r[5] + "";
                    log.Note = r[6] + "";

                    if (!r.IsDBNull(7))
                    {
                        if (r[7] + "" == "Y")
                        {
                            log.IsMemberDemoRO = true;
                        }
                        else
                        {
                            log.IsMemberDemoRO = false;
                        }
                    }
                    else
                    {
                        log.IsMemberDemoRO = false;
                    }

                    if (!r.IsDBNull(8))
                    {
                        if (r[8] + "" == "Y")
                        {
                            log.IsAuthRO = true;
                        }
                        else
                        {
                            log.IsAuthRO = false;
                        }
                    }
                    else
                    {
                        log.IsAuthRO = false;
                    }

                    if (!r.IsDBNull(9))
                    {
                        if (r[9] + "" == "Y")
                        {
                            log.IsProgressNotesRO = true;
                        }
                        else
                        {
                            log.IsProgressNotesRO = false;
                        }
                    }
                    else
                    {
                        log.IsProgressNotesRO = false;
                    }

                    if (!r.IsDBNull(10))
                    {
                        if (r[10] + "" == "Y")
                        {
                            log.IsGroupAccessGlobal = true;
                        }
                        else
                        {
                            log.IsGroupAccessGlobal = false;
                        }
                    }
                    else
                    {
                        log.IsGroupAccessGlobal = false;
                    }

                    log.UserID = Convert.ToInt32(r[11]);

                    if (!r.IsDBNull(12))
                        log.LastGlobalMesssageDate = r.GetDateTime(12);
                    else
                        log.LastGlobalMesssageDate = DateTime.MinValue;

                    if (!r.IsDBNull(13))
                        log.LastWhatsNewDate = r.GetDateTime(13);
                    else
                        log.LastWhatsNewDate = DateTime.MinValue;

                    log.Address1 = r[14] + "";
                    log.Address2 = r[15] + "";
                    log.City = r[16] + "";
                    log.State = r[17] + "";
                    log.ZipCode = r[18] + "";
                    log.Email2 = r[19] + "";
                    log.Gender = r[20] + "";
                    log.Religion = r[21] + "";
                    log.Race = r[22] + "";
                    log.Email = r[23] + "";
                    log.ContactNum = r[24] + "";

                    log.Hierarchy = GetListOfHierarchyFor(r[0] + "");
                    log.Roles = GetListOfRolesFor(r[0] + "");
                    log.Groups = GetListOfGroupsFor(r[0] + "");
                    log.Regions = GetListOfRegionsFor(r[0] + "");
                    log.CostCenters = GetListOfCostCentersFor(r[0] + "");
                    log.ServiceIDS = GetListOfServiceIDsFor(r[0] + "");
                    log.Competencies = GetListOfUserCompetencies(r[0] + "");
                    log.Languages = GetListOfLanguagesFor(r[0] + "");

                    log.CredentialType1 = r["CredentialType1"] + "";
                    log.CredentialType2 = r["CredentialType2"] + "";
                    log.ClinicalLicenseNumber = r["ClinicalLicenseNumber"] + "";
                    log.BCBANumber = r["BCBANumber"] + "";
                    log.IndividualNPINumber = r["IndividualNPINumber"] + "";
                    if (r["PhysicalAbuse"].ToString() != "")
                    { log.PhysicalAbuse = Convert.ToBoolean(r["PhysicalAbuse"]); }
                    if (r["SexualAbuse"].ToString() != "")
                    { log.SexualAbuse = Convert.ToBoolean(r["SexualAbuse"]); }
                    if (r["ADHD"].ToString() != "")
                    { log.ADHD = Convert.ToBoolean(r["ADHD"]); }
                    if (r["AdoptionFoster"].ToString() != "")
                    { log.AdoptionFoster = Convert.ToBoolean(r["AdoptionFoster"]); }
                    if (r["AngerManagement"].ToString() != "")
                    { log.AngerManagement = Convert.ToBoolean(r["AngerManagement"]); }
                    if (r["AppliedBehavior"].ToString() != "")
                    { log.AppliedBehavior = Convert.ToBoolean(r["AppliedBehavior"]); }
                    if (r["ArtTherapy"].ToString() != "")
                    { log.ArtTherapy = Convert.ToBoolean(r["ArtTherapy"]); }
                    if (r["Autism"].ToString() != "")
                    { log.Autism = Convert.ToBoolean(r["Autism"]); }
                    if (r["BehaviorModification"].ToString() != "")
                    { log.BehaviorModification = Convert.ToBoolean(r["BehaviorModification"]); }
                    if (r["CognitiveBehavior"].ToString() != "")
                    { log.CognitiveBehavior = Convert.ToBoolean(r["CognitiveBehavior"]); }
                    if (r["DevDisabled"].ToString() != "")
                    { log.DevDisabled = Convert.ToBoolean(r["DevDisabled"]); }
                    if (r["DomesticViolence"].ToString() != "")
                    { log.DomesticViolence = Convert.ToBoolean(r["DomesticViolence"]); }
                    if (r["EatingDisorder"].ToString() != "")
                    { log.EatingDisorder = Convert.ToBoolean(r["EatingDisorder"]); }
                    if (r["EMDR"].ToString() != "")
                    { log.EMDR = Convert.ToBoolean(r["EMDR"]); }
                    if (r["EvidenceBased"].ToString() != "")
                    { log.EvidenceBased = Convert.ToBoolean(r["EvidenceBased"]); }
                    if (r["FaithChrisitian"].ToString() != "")
                    { log.FaithChrisitian = Convert.ToBoolean(r["FaithChrisitian"]); }
                    if (r["FaithJewish"].ToString() != "")
                    { log.FaithJewish = Convert.ToBoolean(r["FaithJewish"]); }
                    if (r["FaithOther"].ToString() != "")
                    { log.FaithOther = Convert.ToBoolean(r["FaithOther"]); }
                    if (r["FamilyTherapy"].ToString() != "")
                    { log.FamilyTherapy = Convert.ToBoolean(r["FamilyTherapy"]); }
                    if (r["GangInvolvement"].ToString() != "")
                    { log.GangInvolvement = Convert.ToBoolean(r["GangInvolvement"]); }
                    if (r["JuvenileJustice"].ToString() != "")
                    { log.JuvenileJustice = Convert.ToBoolean(r["JuvenileJustice"]); }
                    if (r["LearningDisability"].ToString() != "")
                    { log.LearningDisability = Convert.ToBoolean(r["LearningDisability"]); }
                    if (r["LGBTIssues"].ToString() != "")
                    { log.LGBTIssues = Convert.ToBoolean(r["LGBTIssues"]); }
                    if (r["ParentingSkills"].ToString() != "")
                    { log.ParentingSkills = Convert.ToBoolean(r["ParentingSkills"]); }
                    if (r["PlayTherapy"].ToString() != "")
                    { log.PlayTherapy = Convert.ToBoolean(r["PlayTherapy"]); }
                    if (r["PTSD"].ToString() != "")
                    { log.PTSD = Convert.ToBoolean(r["PTSD"]); }
                    if (r["SelfMutilatition"].ToString() != "")
                    { log.SelfMutilatition = Convert.ToBoolean(r["SelfMutilatition"]); }
                    if (r["SexOffenders"].ToString() != "")
                    { log.SexOffenders = Convert.ToBoolean(r["SexOffenders"]); }
                    if (r["SexualBoundaryIssues"].ToString() != "")
                    { log.SexualBoundaryIssues = Convert.ToBoolean(r["SexualBoundaryIssues"]); }
                    if (r["SocialSkillsTraining"].ToString() != "")
                    { log.SocialSkillsTraining = Convert.ToBoolean(r["SocialSkillsTraining"]); }
                    if (r["SubstanceAbuse"].ToString() != "")
                    { log.SubstanceAbuse = Convert.ToBoolean(r["SubstanceAbuse"]); }
                    if (r["TraumaIssues"].ToString() != "")
                    { log.TraumaIssues = Convert.ToBoolean(r["TraumaIssues"]); }
                    if (r["VocationalSkillsTraining"].ToString() != "")
                    { log.VocationalSkillsTraining = Convert.ToBoolean(r["VocationalSkillsTraining"]); }
                    if (r["AcceptTexts"].ToString() != "")
                    { log.AcceptTexts = Convert.ToBoolean(r["AcceptTexts"]); }

                    if (r["LATITUDE"] != null)
                    { log.LATITUDE = Convert.ToDouble(r["LATITUDE"]); }
                    if (r["LONGITUDE"] != null)
                    { log.LONGITUDE = Convert.ToDouble(r["LONGITUDE"]); }
                    log.MAPGROUP = r["MAPGROUP"] + "";
                    log.TOOLTIP = r["TOOLTIP"] + "";
                    log.GEO = r["GEO"] + "";
                    log.OON = r["OON"] + "";
                    log.Note = r["NOTE"] + "";
                    //log.IpAddress = GetIPAddress();
                    log.IpAddress = "Mobile Phone";// HttpContext.Current.Request.UserHostAddress;

                    log.IsOrgAReferralTarget = true; //IsProviderOrganizationAReferralTarget(log.Organization);

                    log.CP1 = r["CP1"] + "";
                    log.CP2 = r["CP2"] + "";
                    log.CP3 = r["CP3"] + "";
                    log.CP4 = r["CP4"] + "";
                    log.CP5 = r["CP5"] + "";
                    log.CP6 = r["CP6"] + "";
                    log.CP7 = r["CP7"] + "";
                    log.CP8 = r["CP8"] + "";
                    log.CP9 = r["CP9"] + "";
                    log.CP10 = r["CP10"] + "";

                    //if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserName"] + "" != "" + log.Username)
                    //{
                    //    // only do this once per session
                    //    HttpContext.Current.Session["UserName"] = log.Username;
                    //    HttpContext.Current.Session["FirstName"] = log.FirstName;
                    //    HttpContext.Current.Session["LastName"] = log.LastName;
                    //    HttpContext.Current.Session["Organization"] = log.Organization;
                    //    HttpContext.Current.Session["UserID"] = log.UserID;

                    //    //log.IpAddress = HttpContext.Current.Request.UserHostAddress;
                    //    //HttpContext.Current.Session["userLogin"] = log;
                    //}



                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

                if (!found) // maybe we have a consumer/member login
                {
                    sql = "SELECT [Username],[CP1],[CP2],[CP3],[CP4],[CP5],[CP6],[CP7],[CP8],[CP9],[CP10],[MemberID] " +
                        "FROM [tblUserConsumer] where USERNAME = @UNAME";

                    SqlConnection cn2 = new SqlConnection(DBCON());
                    cn2.Open();

                    SqlCommand cmd2 = new SqlCommand(sql, cn2);
                    cmd2.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = Username;
                    cmd2.CommandTimeout = 500;

                    SqlDataReader r2 = cmd2.ExecuteReader();

                    while (r2.Read())
                    {
                        log.Username = r2["Username"] + "";

                        log.FirstName = r2["Username"] + "";

                        log.LastName = "";

                        CodedDescriptor rr = new CodedDescriptor();
                        rr.code = "CONS";
                        rr.description = "CONSUMER LOGIN";

                        log.Roles = new List<CodedDescriptor>();

                        log.Roles.Add(rr);

                        CodedDescriptor hh = new CodedDescriptor();
                        hh.code = "CONS";
                        hh.description = "CONSUMER LOGIN";

                        log.Hierarchy = new List<CodedDescriptor>();

                        log.Hierarchy.Add(hh);

                        log.IpAddress = "Mobile Phone";//HttpContext.Current.Request.UserHostAddress;

                        log.CP1 = r2["CP1"] + "";
                        log.CP2 = r2["CP2"] + "";
                        log.CP3 = r2["CP3"] + "";
                        log.CP4 = r2["CP4"] + "";
                        log.CP5 = r2["CP5"] + "";
                        log.CP6 = r2["CP6"] + "";
                        log.CP7 = r2["CP7"] + "";
                        log.CP8 = r2["CP8"] + "";
                        log.CP9 = r2["CP9"] + "";
                        log.CP10 = r2["CP10"] + "";
                        log.MEMBERID = r2["MemberID"] + "";
                    }
                    r2.Close();
                    cmd2.Dispose();
                    cn2.Close();
                    cn2.Dispose();

                }


                //LogError("GetLoginDetails", "Made It Here2");

                // Trigger for any auto processing stuff

                // If username is primary developer "LWATSON" then trigger auto processing stuff

                if (Username.ToUpper() == "LWATSON")
                {
                    // do the nasty

                    //TryAndSetAllALTsandLONGsinthePOITable();
                }
            }
            catch (Exception ex)
            {
                LogError("GetLoginDetails", ex.Message);

                log.FirstName = ex.Message;
                log.LastName = ex.Message;
                log.Organization = ex.Message;
            }

            //LogError("GetLoginDetails", "Made It Here3");

            return log;
        }

        public void LogSystemError(string errorUrl, string errorMessage, string stackTrace)
        {
            LogError(errorUrl, errorMessage + "\n" + stackTrace);
        }

        private void LogError(string source, string message)
        {
            try
            {
                var IPaddr = "Mobile";
                if (IPaddr == "::1")  //This is when running local Test IP address..BME 10/25/2017.
                { IPaddr = "01.01.01.01"; }

                string sql = "INSERT INTO tblSYSTEMERRORS (IPADDR,CREATEDATE,ERRORSOURCE,ERRORMSG) VALUES (@IP,@CD,@ES,@EM)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                cmd.Parameters.Add("@IP", SqlDbType.VarChar).Value = IPaddr;
                cmd.Parameters.Add("@CD", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@ES", SqlDbType.VarChar).Value = source;

                string userName = "Mobile Phone User";
                //if (HttpContext.Current.Session == null)
                //{
                //    userName = HttpContext.Current.User.Identity.Name;
                //}
                //else
                //{
                //    userName = HttpContext.Current.Session["UserName"].ToString();
                //}
                cmd.Parameters.Add("@EM", SqlDbType.VarChar).Value = message + "\nSystem User ATM: " + userName;

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                // here we should try an alternative to the database logging of the error

                Console.WriteLine(ex.Message);
            }
        }

        private string DBCON()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["CFCSData"].ConnectionString;
        }
               
        private bool DBLocked()
        {
            bool locked = false;

            try
            {
                SqlConnection cn = new SqlConnection(DBCON());
                string sql = "SELECT DBLOCKED FROM TBLSYSTEMSETTINGS";
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    if (r[0] + "" == "Y")
                        locked = true;
                    else
                        locked = false;

                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();


            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // fail silently
                locked = false;
            }

            return locked;
        }

        [Route("api/Login/GetCaseLoad")]
        [HttpGet]
        public JsonResult<List<MemberDetailsShort>> GetCurrentCaseLoad()
        {
            List<MemberDetailsShort> members = new List<MemberDetailsShort>();
            try
            {
                //string sql = "SELECT top 1000 MIN(MMID) as 'MMID',FIRSTNAME,LASTNAME,MIDDLENAME,DOB,GENDER,ETHNICITY,RACE,MM.SSN ";
                //sql += "FROM tblMemberMain MM ";
                //sql += "LEFT OUTER JOIN tblMemberAuthorizedServices AUS ON AUS.SSN = MM.SSN ";
                //sql += "LEFT OUTER JOIN tblMemberObservers OB ON OB.moSSN = MM.SSN ";
                //sql += "WHERE AUS.CASEMANAGER = @CM ";
                //sql += " AND OB.moCASEMANAGER = @CM ";
                //sql += " AND (STARTDATE <= GETDATE() AND ENDDATE >= DATEADD(DAY,-1,GETDATE())) ";
                //sql += " GROUP BY FIRSTNAME,LASTNAME,MIDDLENAME,DOB,GENDER,ETHNICITY,RACE,MM.SSN ";
                //sql += " ORDER BY LASTNAME,FIRSTNAME,MM.SSN ";

                //04292013 - per client show where staff is observer since staff is not linked to auths on import
                //will revert later
                string sql = "SELECT top 1000 MIN(MMID) as 'MMID',FIRSTNAME,LASTNAME,MIDDLENAME,DOB,GENDER,ETHNICITY,RACE,MM.SSN, PHONE1, PHONE2 ";
                sql += "FROM tblMemberMain MM ";
                sql += "LEFT OUTER JOIN tblMemberAuthorizedServices AUS ON AUS.SSN = MM.SSN ";
                sql += "LEFT OUTER JOIN tblMemberObservers OB ON OB.moSSN = MM.SSN ";
                sql += "WHERE OB.moCASEMANAGER = @CM AND (OB.moEDATE IS NULL OR OB.moEDATE >= GETDATE()) ";
                sql += " GROUP BY FIRSTNAME,LASTNAME,MIDDLENAME,DOB,GENDER,ETHNICITY,RACE,MM.SSN,PHONE1,PHONE2 ";
                sql += " ORDER BY LASTNAME,FIRSTNAME,MM.SSN ";

                string userName = "";

                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("LOGIN"))
                {
                    userName = rhead.GetValues("LOGIN").First();
                }
                
                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                //cmd.Parameters.Add("@CM", SqlDbType.VarChar).Value = HttpContext.Current.Session["UserName"].ToString();
                cmd.Parameters.Add("@CM", SqlDbType.VarChar).Value = userName;


                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    MemberDetailsShort mem = new MemberDetailsShort();
                    if (r["MMID"] != DBNull.Value)
                    {
                        mem.MMID = Convert.ToInt64(r["MMID"]);
                    }
                    mem.FirstName = r["FIRSTNAME"].ToString() + "";
                    mem.LastName = r["LASTNAME"].ToString() + "";
                    mem.MiddleName = r["MIDDLENAME"].ToString() + "";
                    if (r["DOB"] != DBNull.Value)
                    {
                        mem.DOB = r.GetDateTime(r.GetOrdinal("DOB")).ToShortDateString();
                    }
                    else
                    {
                        mem.DOB = Convert.ToDateTime(null).ToShortDateString(); ;
                    }

                    if ((r["GENDER"].ToString() + "") == "1" )
                    {
                        mem.Gender = "M";
                    }
                    else
                    {
                        if ((r["GENDER"].ToString() + "") == "2")
                        {
                            mem.Gender = "F";
                        }
                        else
                        {
                            mem.Gender = "U";
                        }
                    }

                   

                    //mem.Gender = r["GENDER"].ToString() + "";
                    mem.Ethnicity = r["ETHNICITY"].ToString() + "";
                    mem.Race = r["RACE"].ToString() + "";
                    mem.SSN = r["SSN"].ToString() + "";
                    mem.memberAddress = GetMemberAddress(mem.SSN);
                    mem.Phone1 = r["PHONE1"].ToString() + "";
                    mem.Phone2 = r["PHONE2"].ToString() + "";

                    members.Add(mem);
                }

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetCurrentCaseLoad", ex.Message);
            }
            return Json(members);

        }
        
        private List<CodedDescriptor> GetListOfHierarchyFor(string UserName)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT CODE,DESCRIPTION from tblLOOKUPHIERARCHY " +
                    "WHERE CODE in (SELECT DISTINCT HIERCODE FROM TBLUSERHIERARCHY WHERE USERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfHierarchyFor", ex.Message);

                result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetListOfLanguagesFor(string UserName)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT DISTINCT UL.ULCODE,LL.DESCRIPTION ";
                sql += "FROM TBLUSERLANGUAGES UL ";
                sql += "LEFT OUTER JOIN TBLLOOKUPLANGUAGES LL ON LL.CODE = UL.ULCODE ";
                sql += "WHERE LL.CODE IN (SELECT DISTINCT ULCODE FROM TBLUSERLANGUAGES WHERE ULUSERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfLanguagesFor", ex.Message);

                result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetListOfGroupsFor(string UserName)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT CODE,DESCRIPTION from [tblLOOKUPPROGRAMMEMBERSHIP] " +
                    "WHERE CODE in (SELECT DISTINCT GROUPCODE FROM TBLUSERGROUPS WHERE USERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfGroupsFor", ex.Message);

                result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetListOfCostCentersFor(string UserName)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT DISTINCT A.COSTCENTER," +
                    "(SELECT TOP 1 SVCDESCRIPTION FROM TBLLOOKUPSERVICES B WHERE A.COSTCENTER = B.COSTCENTER) as 'SVCDESCRIPTION' " +
                    "from [TBLUSERCOSTCENTERS] A " +
                    "WHERE COSTCENTER in (SELECT DISTINCT COSTCENTER FROM TBLUSERCOSTCENTERS WHERE USERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfCostCentersFor", ex.Message);

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptorWID> GetListOfServiceIDsFor(string UserName)
        {
            List<CodedDescriptorWID> result = new List<CodedDescriptorWID>();

            try
            {
                string sql = "SELECT DISTINCT A.COSTCENTER," +
                    "(SELECT TOP 1 SVCDESCRIPTION FROM TBLLOOKUPSERVICES B WHERE A.COSTCENTER = CAST(B.SVCID AS VARCHAR(10))) as 'SVCDESCRIPTION' " +
                    "from [TBLUSERCOSTCENTERS] A " +
                    "WHERE COSTCENTER in (SELECT DISTINCT COSTCENTER FROM TBLUSERCOSTCENTERS WHERE USERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWID res = new CodedDescriptorWID();

                    //if (r[0].ToString() != "")
                    //{ res.code = r[0].ToString(); }
                    if (r[0] != System.DBNull.Value)
                        res.id = Convert.ToInt64(r[0]);
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfServidIDsFor", ex.Message);

                CodedDescriptorWID cd = new CodedDescriptorWID();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetListOfRolesFor(string UserName)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT CODE,DESCRIPTION from tblLOOKUPROLES " +
                    "WHERE CODE in (SELECT DISTINCT ROLECODE FROM TBLUSERROLES WHERE USERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfRolesFor", ex.Message);

                result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetListOfRegionsFor(string UserName)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {

                string sql = "SELECT DISTINCT CODE,DESCRIPTION from tblLOOKUPCOUNTYNAME " +
                    "WHERE CODE in (SELECT DISTINCT COUNTYCODE FROM TBLUSERREGIONS WHERE USERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfRegionsFor", ex.Message);

                result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);

            }

            return result;
        }

        private List<CodedDescriptor> GetListOfUserCompetencies(string UserName)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT DISTINCT A.CODE, A.DESCRIPTION ";
                sql += "FROM [TBLLOOKUPUSERCOMPETENCY] A ";
                sql += "WHERE CODE IN (SELECT DISTINCT UCCODE FROM TBLUSERCOMPETENCY WHERE UCUSERNAME = @UNAME)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar, 20, "USERNAME").Value = UserName;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfUserCompetencies", ex.Message);

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private string GetGlobalProviderID()
        {
            string result = "";

            try
            {
                string sql = "SELECT GLOBALPROVIDERNUM from TBLSYSTEMSETTINGS";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    result = r[0] + "";
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetGlobalProviderID", ex.Message);
            }

            return result;
        }

        [Route("api/Login/GetAuths")]
        [HttpGet]
        public JsonResult<List<AuthorizedService>> GetAuthsAvailableForMember()
        {
            List<AuthorizedService> result = new List<AuthorizedService>();

            string IDNUM = "";

            try
            {
                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("IDNUM"))
                {
                    IDNUM = rhead.GetValues("IDNUM").First();
                }


                string sql = "SELECT DISTINCT B.mauthid,B.Providerid as 'PID'," +
                    "coalesce((SELECT TOP 1 SUBPROVIDERNAME FROM TBLPROVIDERSUBPROVIDER A WHERE A.SUBPROVIDERID = B.PROVIDERID)," +
                    "(SELECT TOP 1 PROVIDERNAME FROM TBLPROVIDERSUBPROVIDER A WHERE A.PROVIDERID = B.PROVIDERID)) as 'PNAME', " +
                    "B.STARTDATE,B.ENDDATE,B.COSTCENTER,C.SvcDescription as 'COSTCENTERDESC'," +
                    "B.UNITS,B.DOLLARS,B.ACCEPTED,B.REMAININGUNITS,B.CREATEUSER,D.[DESCRIPTION] as 'FUNDER', " +
                    "B.AUTHNUMBER, B.CONNECTEDNPI, B.HPW, B.CASEMANAGER, B.LINKEDAUTHNUMBER,B.FUNDER as 'FUNDERID', C.UNITTYPE,C.RelatedSplitCode, B.DSPRATE " +
                    "FROM TBLMEMBERAUTHORIZEDSERVICES B  " +
                    "LEFT OUTER JOIN tblLOOKUPSERVICES C ON LTRIM(RTRIM(B.COSTCENTER)) = CONVERT(varchar(5), C.SVCID) " +
                    "LEFT OUTER JOIN TBLLOOKUPFUNDERS D ON B.FUNDER = D.CODE " +
                    "WHERE SSN = @SSN " +
                    "AND B.PROVIDERID IN (SELECT PROVIDERID from tblProviderSubProvider UNION SELECT SUBPROVIDERID FROM tblProviderSubProvider) " +
                    "ORDER BY B.STARTDATE DESC ,B.ENDDATE DESC";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = IDNUM;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    AuthorizedService svc = new AuthorizedService();

                    svc.mauthID = r.GetInt64(0);
                    svc.PROVIDERID = r["PID"] + "";
                    svc.PROVIDERNAME = r["PNAME"] + "";

                    DateTime d = Convert.ToDateTime(null);

                    if (DateTime.TryParse(r["STARTDATE"] + "", out d))
                        svc.STARTDATE = d;

                    if (DateTime.TryParse(r["ENDDATE"] + "", out d))
                        svc.ENDDATE = d;

                    svc.COSTCENTER = r["COSTCENTER"] + "";
                    svc.COSTCENTERDESC = r["COSTCENTERDESC"] + "";

                    int i = 0;

                    if (int.TryParse(r["UNITS"] + "", out i))
                        svc.UNITS = i;

                    double dd = 0.0;
                    if (double.TryParse(r["DOLLARS"] + "", out dd))
                        svc.DOLLARS = dd;

                    svc.ACCEPTED = r["ACCEPTED"] + "";

                    if (int.TryParse(r["REMAININGUNITS"] + "", out i))
                        svc.REMAININGUNITS = i;

                    svc.CREATEUSER = r["CREATEUSER"] + "";
                    svc.FUNDER = r["FUNDER"] + "";
                    svc.FUNDERID = r["FUNDERID"] + "";

                    svc.AUTHNUMBER = r["AUTHNUMBER"] + "";
                    svc.CONNECTEDNPI = r["CONNECTEDNPI"] + "";

                    int hpw = 0;
                    if (int.TryParse(r["HPW"] + "", out hpw))
                        svc.HPW = hpw;

                    svc.CASEMANAGER = r["CASEMANAGER"] + "";
                    svc.LINKEDAUTHNUMBER = r["LINKEDAUTHNUMBER"] + "";

                    string utype = r["UNITTYPE"] + "";
                    int unittype = 0;
                    if (int.TryParse(utype, out unittype))
                    {
                        //if (utype == "01")
                        //{ unittype = Convert.ToInt32("15"); }
                        //else if (utype == "02")
                        //{ unittype = Convert.ToInt32("30"); }
                        //else if (utype == "03")
                        //{ unittype = Convert.ToInt32("45"); }
                        //else if (utype == "04")
                        //{ unittype = Convert.ToInt32("60"); }
                        //else if (utype == "05")
                        //{ unittype = Convert.ToInt32("1"); }

                        double ut = 0;
                        ut = GetUnitType(utype);
                        unittype = Convert.ToInt32(ut);

                        svc.UNITTYPE = unittype;
                    }

                    svc.RelatedSplitCode = r["RelatedSplitCode"] + "";

                    double rate = 0.0;
                    if (double.TryParse(r["DSPRATE"] + "", out rate))
                        svc.DSPRATE = rate;

                    result.Add(svc);

                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetAuthsAvailableForMember", ex.Message);

                AuthorizedService svc = new AuthorizedService();

                svc.PROVIDERNAME = ex.Message;

                svc.COSTCENTERDESC = ex.Message;

                result.Add(svc);
            }

            return Json(result);
        }
        
        public MemberAddress GetMemberAddress(string IDNUM)
        {
            MemberAddress mem = new MemberAddress();

            try
            {
                string sql = "SELECT maID,SSN,ADDRESS1,ADDRESS2,ADDRESS3,APARTMENT_SUITE,ADDRESSTYPE,CITY," +
                    "COALESCE((SELECT top 1 COUNTY FROM tblLOOKUPZIPCOUNTYCITY B WHERE B.ZIP = ZIPCODE),'Unknown') as 'COUNTY',ZIPCODE,[STATE], " +
                    "CREATEDATE,CREATEDBY,UPDATEDATE,UPDATEDBY FROM tblMemberAddress WHERE SSN=@SSN ";
                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = IDNUM;

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    if (r["maID"] != DBNull.Value)
                    {
                        mem.MAID = Convert.ToInt64(r["maID"]);
                    }
                    mem.SSN = r["SSN"].ToString() + "";
                    mem.Address1 = r["ADDRESS1"].ToString() + "";
                    mem.Address2 = r["ADDRESS2"].ToString() + "";
                    mem.Address3 = r["ADDRESS3"].ToString() + "";
                    mem.ApartmentSuite = r["APARTMENT_SUITE"].ToString() + "";
                    mem.AddressType = r["ADDRESSTYPE"].ToString() + "";
                    mem.City = r["CITY"].ToString() + "";
                    mem.County = r["COUNTY"].ToString() + "";
                    mem.ZipCode = r["ZIPCODE"].ToString() + "";
                    mem.State = r["STATE"].ToString() + "";
                    if (r["CREATEDATE"] != DBNull.Value)
                    {
                        mem.CreateDate = r.GetDateTime(r.GetOrdinal("CREATEDATE"));
                    }
                    mem.CreateBy = r["CreatedBy"].ToString() + "";
                    if (r["UPDATEDATE"] != DBNull.Value)
                    {
                        mem.UpdateDate = r.GetDateTime(r.GetOrdinal("UPDATEDATE"));
                    }
                    mem.UpdatedBy = r["UPDATEDBY"].ToString() + "";
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetMemberAddress", ex.Message);
            }
            return mem;
        }

        [Route("api/Login/MemberObservers")]
        [HttpGet]
        public JsonResult<List<MemberObservers>> GetListOfMemberObservers()
        {
            List<MemberObservers> ret = new List<MemberObservers>();

            string IDNUM = "";

            try
            {
                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("IDNUM"))
                {
                    IDNUM = rhead.GetValues("IDNUM").First();
                }

                //moID bigint NOT NULL IDENTITY (1, 1),
                //moSSN varchar(10) NULL,
                //moCASEMANAGER varchar(20) NULL,
                //moSDATE datetime NULL,
                //moEDATE datetime NULL,
                //moCREATEDATE datetime NULL,
                //moCREATEDBY varchar(20) NULL,
                //moOBSTYPE varchar(5) NULL

                //04292016 - changes in this grid result per client
                //string sql = "SELECT moID,moSSN,moCASEMANAGER,moSDATE,moEDATE,moCREATEDATE,moCREATEDBY,moOBSTYPE " +
                //    "FROM tblMemberObservers WHERE moSSN = @SSN  and  (moCASEMANAGER is NOT NUll and moCASEMANAGER <> '' ) ORDER BY moSDATE DESC,moEDATE DESC";

                string sql = "SELECT DISTINCT moID,moSSN,(ul.FirstName + ' ' + ul.LastName) as STAFF, ";
                //sql += "(SELECT FIRSTNAME + ' ' + LASTNAME FROM tblUserLogins WHERE sup.SupervisorName = Username) as SUPERVISOR, ";
                sql += "ul.ContactNum,ul.Email, ";
                sql += "(SELECT DESCRIPTION FROM tblLOOKUPUSERCREDENTIALS WHERE CODE = UL.CredentialType1) AS CREDENTIALONE, ";
                sql += "(SELECT DESCRIPTION FROM tblLOOKUPUSERCREDENTIALS WHERE CODE = UL.CredentialType2) AS CREDENTIALTWO, ";
                sql += "moSDATE,moEDATE,moCREATEDATE,moCREATEDBY,ul.username as USERNAME ";
                sql += "FROM tblMemberObservers obs ";
                sql += "INNER JOIN tblUserLogins ul on ul.Username = obs.moCASEMANAGER ";
                sql += "left outer JOIN tblUsersSupervisor sup ON SUP.UserName = UL.Username ";
                sql += "WHERE moSSN = @SSN ";
                sql += "and  (moCASEMANAGER is NOT NUll and moCASEMANAGER <> '' ) ";
                sql += "ORDER BY moSDATE DESC,moEDATE DESC ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = IDNUM;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    MemberObservers mo = new MemberObservers();

                    //mo.ID = r.GetInt64(0);
                    mo.ID = r.GetInt32(0);
                    mo.SSN = r["moSSN"] + "";
                    mo.OBSERVER = r["STAFF"] + "";
                    mo.EMAIL = r["Email"] + "";
                    mo.PHONE = r["ContactNum"] + "";
                    mo.CREDENTIALONE = r["CREDENTIALONE"] + "";
                    mo.CREDENTIALTWO = r["CREDENTIALTWO"] + "";
                    if (r["moSDATE"] != DBNull.Value)
                    {
                        mo.SDATE = Convert.ToDateTime(r["moSDATE"]);
                        mo.strSDATE = mo.SDATE.ToShortDateString();
                    }
                    else
                    {
                        mo.SDATE = Convert.ToDateTime(null);
                        mo.strSDATE = "";
                    }

                    if (r["moEDATE"] != DBNull.Value)
                    {
                        mo.EDATE = Convert.ToDateTime(r["moEDATE"]);
                        mo.strEDATE = mo.EDATE.ToShortDateString();
                    }
                    else
                    {
                        mo.EDATE = Convert.ToDateTime(null);
                        mo.strEDATE = "";
                    }

                    if (r["moCREATEDATE"] != DBNull.Value)
                        mo.CREATEDATE = Convert.ToDateTime(r["moCREATEDATE"]);
                    else
                        mo.CREATEDATE = Convert.ToDateTime(null);

                    mo.AUTHOR = r["moCREATEDBY"] + "";
                    mo.OBSTYPE = GetListOfAllSupervisorsForUser(r["USERNAME"] + "");
                    //mo.OBSTYPE = r["SUPERVISOR"] + "";

                    ret.Add(mo);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfMemberObservers", ex.Message);
            }

            return Json(ret);
        }

        public string GetListOfAllSupervisorsForUser(string UserName)
        {
            string result = "";
            UserName = UserName.TrimEnd(',', ' ');
            UserName = UserName.Replace(",", "','");

            try
            {
                string sql = "Select distinct SupervisorName  + ',' AS SupervisorName from tblUsersSupervisor ";
                sql += "where UserName = '" + UserName + "'";

                SqlConnection cn = new SqlConnection(DBCON());

                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    result += r["SupervisorName"] + " ";
                }

                result = result.TrimEnd(',');

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

                return result;

            }
            catch (Exception ex)
            {
                LogError("GetListOfAllSupervisorsForUser", ex.Message);

                result = "";
            }

            return result;
        }

        [Route("api/Login/MemberReferrals")]
        [HttpGet]
        public JsonResult<List<MemberReferralSource>> GetListOfMemberReferralSourceForMember()
        {
            List<MemberReferralSource> result = new List<MemberReferralSource>();

            string IDNUM = "";
            try
            {

                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("IDNUM"))
                {
                    IDNUM = rhead.GetValues("IDNUM").First();
                }

                string sql = "SELECT DISTINCT RS.*, BB.*, ";
                sql += "(SELECT DESCRIPTION FROM tblLOOKUPSUPPORTSRELATIONSHIP WHERE CODE = RS.RSROLE) AS RELATIONSHIP, ";
                sql += "(SELECT DESCRIPTION FROM tblLOOKUPREFERRALSOURCE WHERE CODE = rs.RSAGENCYID) AS AGENCY ";
                sql += "FROM tblREFERRALSOURCE RS ";
                sql += "LEFT OUTER JOIN tblMEMBERREFERRALSBLUEBOOK BB ON BB.RSID = RS.RSID ";
                sql += "LEFT OUTER JOIN tblMEMBERREFERRALS MR ON MR.SSN = BB.MRSSN ";
                sql += "WHERE (BB.MRSSN = @SSN) ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = IDNUM;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    MemberReferralSource a = new MemberReferralSource();
                    if (!Convert.IsDBNull(r["RSID"]))
                    {
                        a.RSID = Convert.ToInt64(r["RSID"]);
                    }
                    else
                    {
                        a.RSID = 0;
                    }
                    a.RSFIRSTNAME = r["RSFIRSTNAME"] + "";
                    a.RSLASTNAME = r["RSLASTNAME"] + "";
                    a.RSROLE = r["RSROLE"] + "";
                    a.RSROLEDESCRIPTION = r["RELATIONSHIP"] + "";
                    if (!Convert.IsDBNull(r["RSISAGENCY"]))
                    {
                        a.RSISAGENCY = Convert.ToBoolean(r["RSISAGENCY"]);
                    }
                    else
                    {
                        a.RSISAGENCY = false;
                    }
                    if (!Convert.IsDBNull(r["RSAGENCYID"]))
                    {
                        a.RSAGENCYID = Convert.ToInt64(r["RSAGENCYID"]);
                    }
                    else
                    {
                        a.RSAGENCYID = 0;
                    }
                    //a.RSADDRESS1 = r["ADDRESS1"] + "";
                    //a.RSADDRESS2 = r["RSADDRESS2"] + "";
                    //a.RSADDRESS3 = r["RSADDRESS3"] + "";
                    //a.RSCITY = r["CITY"] + "";
                    //a.RSSTATE = r["STATE"] + "";
                    //a.RSZIP = r["ZIP"] + "";
                    a.RSADDRESS1 = r["RSADDRESS1"] + "";
                    a.RSADDRESS2 = r["RSADDRESS2"] + "";
                    a.RSADDRESS3 = r["RSADDRESS3"] + "";
                    a.RSCITY = r["RSCITY"] + "";
                    a.RSSTATE = r["RSSTATE"] + "";
                    a.RSZIP = r["RSZIP"] + "";
                    a.RSCOUNTY = r["RSCOUNTY"] + "";
                    a.RSEMAIL = r["RSEMAIL"] + "";
                    a.RSHOMEPHONE = r["RSHOMEPHONE"] + "";
                    a.RSWORKPHONE = r["RSWORKPHONE"] + "";
                    if (!Convert.IsDBNull(r["RSACTIVE"]))
                    {
                        a.RSACTIVE = Convert.ToBoolean(r["RSACTIVE"]);
                    }
                    else
                    {
                        a.RSACTIVE = false;
                    }
                    a.RSCREATEDBY = r["RSCREATEDBY"] + "";
                    if (!Convert.IsDBNull(r["RSCREATEDDATE"]))
                    {
                        a.RSCREATEDDATE = Convert.ToDateTime(r["RSCREATEDDATE"]);
                    }
                    else
                    {
                        a.RSCREATEDDATE = Convert.ToDateTime(null);
                    }
                    a.SSN = r["MRSSN"] + "";
                    a.AGENCYDESC = r["AGENCY"] + "";
                    if (!Convert.IsDBNull(r["RSSTARTDATE"]))
                    {
                        a.RSSTARTDATE = Convert.ToDateTime(r["RSSTARTDATE"]);
                        a.strRSSTARTDATE = a.RSSTARTDATE.ToShortDateString();
                    }
                    else
                    {
                        a.RSSTARTDATE = Convert.ToDateTime(null);
                        a.strRSSTARTDATE = "";
                    }
                    if (!Convert.IsDBNull(r["RSENDDATE"]))
                    {
                        a.RSENDDATE = Convert.ToDateTime(r["RSENDDATE"]);
                        a.strRSENDDATE = a.RSENDDATE.ToShortDateString();
                    }
                    else
                    {
                        a.RSENDDATE = Convert.ToDateTime(null);
                        a.strRSENDDATE = "";
                    }
                    a.RSEXTENSION = r["RSEXTENSION"] + "";

                    result.Add(a);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblREFERRALSOURCE.GetListOftblREFERRALSOURCEForMember" + ex.ToString()));
            }
            return Json(result);
        }

        public double GetUnitType(string UNITTYPE)
        {
            double numtype = 0;

            try
            {
                if (UNITTYPE == "01" || UNITTYPE == "1")
                { numtype = Convert.ToDouble("15"); }
                else if (UNITTYPE == "02" || UNITTYPE == "2")
                { numtype = Convert.ToDouble("30"); }
                else if (UNITTYPE == "03" || UNITTYPE == "3")
                { numtype = Convert.ToDouble("45"); }
                else if (UNITTYPE == "04" || UNITTYPE == "4")
                { numtype = Convert.ToDouble("60"); }
                else if (UNITTYPE == "05" || UNITTYPE == "5")
                { numtype = Convert.ToDouble("1"); }

            }
            catch (Exception ex)
            {
                LogError("GetUnitType", ex.Message);
            }


            return numtype;
        }

        [Route("api/Login/EncounterProgressNotes")]
        [HttpGet]
        public JsonResult<List<MemberProgressNotes>> GetListOfEncounterNotesForType()

        {
            List<MemberProgressNotes> result = new List<MemberProgressNotes>();

            string IDNUM = "";
            try
            {

                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("IDNUM"))
                {
                    IDNUM = rhead.GetValues("IDNUM").First();
                }

                //03172016 - per client, do not show encounter notes here
                // added some coalesces to simulate supervision approval business requirements change
                string sql = "SELECT MPNID," +
                    "COALESCE((SELECT TOP 1 DESCRIPTION FROM TBLLOOKUPPROGRESSNOTETYPES WHERE CODE = NOTETYPE)," +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPREMNOTETYPE] WHERE CODE = NOTETYPE)) as 'NTYPE'," +
                    "CREATEDBY,CREATEDATE,NOTATION,SIGNED,SIGNDATE,COALESCE(SUPERAPPROV,CREATEDBY),COALESCE(SUPERAPPROVEDATE,CREATEDATE),SSN," +
                    "COALESCE((SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPNOTECONTACTTYPE] WHERE CODE = NOTECONTACTTYPE)," +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPREMCONTACTWITH] WHERE CODE = NOTECONTACTTYPE)) as 'NCONTACTTYPE', " +
                    "SUPERVISORNOTATION, SUPERVISORACK1, SUPERVISORACK2, SUPERVISORNOTATIONDATE,CONTACTDATE, TRAVELTIME, CONTACTTIME, " +
                    "SAFETYASSESSMENT,SAFETYASSESSMENTLVL, " +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPSAFETYASSESSMENTLVL] WHERE CODE = SAFETYASSESSMENTLVL) as 'SALDESC', SERVICECODE " +
                    //"FROM TBLMEMBERPROGRESSNOTES WHERE SSN = @SSN AND HIDDEN <> 'Y' ORDER BY COALESCE(CONTACTDATE,CREATEDATE) DESC,MPNID ASC";
                    "FROM TBLMEMBERPROGRESSNOTES WHERE SSN = @SSN AND HIDDEN <> 'Y' AND NOTETYPE = '13' ORDER BY COALESCE(CONTACTDATE,CREATEDATE) DESC,MPNID ASC";

                //we only want type 13 returned in this query 'Encounter'

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = IDNUM;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    MemberProgressNotes pn = new MemberProgressNotes();
                    List<ServiceDescription> SVCS = GetListOfServiceDescriptions();

                    pn.mpnID = r.GetInt64(0);
                    pn.NOTETYPEDESC = r[1] + "";
                    pn.AUTHOR = r[2] + "";
                    pn.CREATEDATE = r.GetDateTime(3);
                    pn.NOTATION = r[4] + "";

                    pn.NOTATIONSHORT = Elipsis(pn.NOTATION, 200);

                    pn.SIGNED = r[5] + "";

                    if (!r.IsDBNull(6))
                    {
                        pn.SIGNEDDATE = r.GetDateTime(6);
                    }

                    pn.SUPERAPPROV = r[7] + "";

                    if (!r.IsDBNull(8))
                    {
                        pn.SUPERAPPROVEDATE = r.GetDateTime(8);
                    }

                    pn.SSN = IDNUM;

                    pn.NOTECONTACTDESC = r[10] + "";

                    pn.SUPERVISORNOTATION = r[11] + "";

                    pn.SUPERVISORACK1 = r[12] + "";

                    pn.SUPERVISORACK2 = r[13] + "";

                    if (!r.IsDBNull(14))
                    {
                        pn.SUPERVISORNOTATIONDATE = r.GetDateTime(14);
                    }

                    if (!r.IsDBNull(15))
                    {
                        pn.CONTACTDATE = r.GetDateTime(15);
                        pn.strCONTACTDATE = r.GetDateTime(15).ToShortDateString();
                    }
                    else
                    {
                        pn.CONTACTDATE = Convert.ToDateTime(null);
                        pn.strCONTACTDATE = "";
                    }

                    if (!r.IsDBNull(16))
                    {
                        pn.TRAVELMINUTES = r.GetInt32(16);
                    }
                    else
                    {
                        pn.TRAVELMINUTES = 0;
                    }

                    if (!r.IsDBNull(17))
                    {
                        pn.CONTACTMINUTES = r.GetInt32(17);
                    }
                    else
                    {
                        pn.CONTACTMINUTES = 0;
                    }

                    if (!r.IsDBNull(18))
                    {
                        pn.SAFETYASSESSMENT = r.GetDateTime(18);
                    }
                    else
                    {
                        pn.SAFETYASSESSMENT = Convert.ToDateTime(null);
                    }

                    pn.SAFETYASSESSMENTLVL = r[19] + "";
                    pn.SAFETYASSESSMENTLVLDESC = r[20] + "";

                    if (pn.SUPERAPPROV.Trim() == "")
                    {
                        pn.SUPERAPPROV = pn.AUTHOR;
                        pn.SUPERAPPROVEDATE = pn.CREATEDATE;
                    }

                    pn.SERVICECODE = r[21] + "";

                    foreach (ServiceDescription o in SVCS)
                    {
                        string tmp = o.COSTCENTER;
                        //if (tmp.Length == 1)
                        //    tmp = "0" + tmp;

                        if (pn.SERVICECODE == tmp)
                        {
                            pn.SERVICEDESCRIPTION = o.SVCDESCRIPTION;
                            break;
                        }
                    }

                    result.Add(pn);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfEncounterNotesForType", ex.Message);

                MemberProgressNotes pn = new MemberProgressNotes();

                pn.NOTATION = ex.Message;

                result.Add(pn);
            }

            return Json(result);
        }

        [Route("api/Login/AllProgressNotes/{IDNUM}")]
        [HttpGet]
        public JsonResult<List<MemberProgressNotes>> GetListOfAllNotesForType(string IDNUM)

        {
            List<MemberProgressNotes> result = new List<MemberProgressNotes>();

            try
            {

                //03172016 - per client, do not show encounter notes here
                // added some coalesces to simulate supervision approval business requirements change
                string sql = "SELECT MPNID," +
                    "COALESCE((SELECT TOP 1 DESCRIPTION FROM TBLLOOKUPPROGRESSNOTETYPES WHERE CODE = NOTETYPE)," +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPREMNOTETYPE] WHERE CODE = NOTETYPE)) as 'NTYPE'," +
                    "CREATEDBY,CREATEDATE,NOTATION,SIGNED,SIGNDATE,COALESCE(SUPERAPPROV,CREATEDBY),COALESCE(SUPERAPPROVEDATE,CREATEDATE),SSN," +
                    "COALESCE((SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPNOTECONTACTTYPE] WHERE CODE = NOTECONTACTTYPE)," +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPREMCONTACTWITH] WHERE CODE = NOTECONTACTTYPE)) as 'NCONTACTTYPE', " +
                    "SUPERVISORNOTATION, SUPERVISORACK1, SUPERVISORACK2, SUPERVISORNOTATIONDATE,CONTACTDATE, TRAVELTIME, CONTACTTIME, " +
                    "SAFETYASSESSMENT,SAFETYASSESSMENTLVL, " +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPSAFETYASSESSMENTLVL] WHERE CODE = SAFETYASSESSMENTLVL) as 'SALDESC', SERVICECODE " +
                    "FROM TBLMEMBERPROGRESSNOTES WHERE SSN = @SSN AND HIDDEN <> 'Y' ORDER BY COALESCE(CONTACTDATE,CREATEDATE) DESC,MPNID ASC";
                    //"FROM TBLMEMBERPROGRESSNOTES WHERE SSN = @SSN AND HIDDEN <> 'Y' AND NOTETYPE = '13' ORDER BY COALESCE(CONTACTDATE,CREATEDATE) DESC,MPNID ASC";

                //we only want type 13 returned in this query 'Encounter'

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = IDNUM;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    MemberProgressNotes pn = new MemberProgressNotes();
                    List<ServiceDescription> SVCS = GetListOfServiceDescriptions();

                    pn.mpnID = r.GetInt64(0);
                    pn.NOTETYPEDESC = r[1] + "";
                    pn.AUTHOR = r[2] + "";
                    pn.CREATEDATE = r.GetDateTime(3);
                    pn.NOTATION = r[4] + "";

                    pn.NOTATIONSHORT = Elipsis(pn.NOTATION, 200);

                    pn.SIGNED = r[5] + "";

                    if (!r.IsDBNull(6))
                    {
                        pn.SIGNEDDATE = r.GetDateTime(6);
                    }

                    pn.SUPERAPPROV = r[7] + "";

                    if (!r.IsDBNull(8))
                    {
                        pn.SUPERAPPROVEDATE = r.GetDateTime(8);
                    }

                    pn.SSN = IDNUM;

                    pn.NOTECONTACTDESC = r[10] + "";

                    pn.SUPERVISORNOTATION = r[11] + "";

                    pn.SUPERVISORACK1 = r[12] + "";

                    pn.SUPERVISORACK2 = r[13] + "";

                    if (!r.IsDBNull(14))
                    {
                        pn.SUPERVISORNOTATIONDATE = r.GetDateTime(14);
                    }

                    if (!r.IsDBNull(15))
                    {
                        pn.CONTACTDATE = r.GetDateTime(15);
                        pn.strCONTACTDATE = r.GetDateTime(15).ToShortDateString();
                    }
                    else
                    {
                        pn.CONTACTDATE = Convert.ToDateTime(null);
                        pn.strCONTACTDATE = "";
                    }

                    if (!r.IsDBNull(16))
                    {
                        pn.TRAVELMINUTES = r.GetInt32(16);
                    }
                    else
                    {
                        pn.TRAVELMINUTES = 0;
                    }

                    if (!r.IsDBNull(17))
                    {
                        pn.CONTACTMINUTES = r.GetInt32(17);
                    }
                    else
                    {
                        pn.CONTACTMINUTES = 0;
                    }

                    if (!r.IsDBNull(18))
                    {
                        pn.SAFETYASSESSMENT = r.GetDateTime(18);
                    }
                    else
                    {
                        pn.SAFETYASSESSMENT = Convert.ToDateTime(null);
                    }

                    pn.SAFETYASSESSMENTLVL = r[19] + "";
                    pn.SAFETYASSESSMENTLVLDESC = r[20] + "";

                    if (pn.SUPERAPPROV.Trim() == "")
                    {
                        pn.SUPERAPPROV = pn.AUTHOR;
                        pn.SUPERAPPROVEDATE = pn.CREATEDATE;
                    }

                    pn.SERVICECODE = r[21] + "";

                    foreach (ServiceDescription o in SVCS)
                    {
                        string tmp = o.COSTCENTER;
                        //if (tmp.Length == 1)
                        //    tmp = "0" + tmp;

                        if (pn.SERVICECODE == tmp)
                        {
                            pn.SERVICEDESCRIPTION = o.SVCDESCRIPTION;
                            break;
                        }
                    }

                    result.Add(pn);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfAllNotesForType", ex.Message);

                MemberProgressNotes pn = new MemberProgressNotes();

                pn.NOTATION = ex.Message;

                result.Add(pn);
            }

            return Json(result);
        }

        [Route("api/Login/AllExceptEncounterProgressNotes")]
        [HttpGet]
        public JsonResult<List<MemberProgressNotes>> GetListOfAllNotesExceptEncounterForType()

        {
            List<MemberProgressNotes> result = new List<MemberProgressNotes>();

            string IDNUM = "";

            try
            {
                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("IDNUM"))
                {
                    IDNUM = rhead.GetValues("IDNUM").First();
                }

                //03172016 - per client, do not show encounter notes here
                // added some coalesces to simulate supervision approval business requirements change
                string sql = "SELECT MPNID," +
                    "COALESCE((SELECT TOP 1 DESCRIPTION FROM TBLLOOKUPPROGRESSNOTETYPES WHERE CODE = NOTETYPE)," +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPREMNOTETYPE] WHERE CODE = NOTETYPE)) as 'NTYPE'," +
                    "CREATEDBY,CREATEDATE,NOTATION,SIGNED,SIGNDATE,COALESCE(SUPERAPPROV,CREATEDBY),COALESCE(SUPERAPPROVEDATE,CREATEDATE),SSN," +
                    "COALESCE((SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPNOTECONTACTTYPE] WHERE CODE = NOTECONTACTTYPE)," +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPREMCONTACTWITH] WHERE CODE = NOTECONTACTTYPE)) as 'NCONTACTTYPE', " +
                    "SUPERVISORNOTATION, SUPERVISORACK1, SUPERVISORACK2, SUPERVISORNOTATIONDATE,CONTACTDATE, TRAVELTIME, CONTACTTIME, " +
                    "SAFETYASSESSMENT,SAFETYASSESSMENTLVL, " +
                             "(SELECT TOP 1 DESCRIPTION FROM [tblLOOKUPSAFETYASSESSMENTLVL] WHERE CODE = SAFETYASSESSMENTLVL) as 'SALDESC', SERVICECODE " +
                    //"FROM TBLMEMBERPROGRESSNOTES WHERE SSN = @SSN AND HIDDEN <> 'Y' ORDER BY COALESCE(CONTACTDATE,CREATEDATE) DESC,MPNID ASC";
                    "FROM TBLMEMBERPROGRESSNOTES WHERE SSN = @SSN AND HIDDEN <> 'Y' AND NOTETYPE <> '13' ORDER BY COALESCE(CONTACTDATE,CREATEDATE) DESC,MPNID ASC";

                //we only want everything but 13 returned in this query 'All SANs Encounter'

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = IDNUM;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    MemberProgressNotes pn = new MemberProgressNotes();
                    List<ServiceDescription> SVCS = GetListOfServiceDescriptions();

                    pn.mpnID = r.GetInt64(0);
                    pn.NOTETYPEDESC = r[1] + "";
                    pn.AUTHOR = r[2] + "";
                    pn.CREATEDATE = r.GetDateTime(3);
                    pn.NOTATION = r[4] + "";

                    pn.NOTATIONSHORT = Elipsis(pn.NOTATION, 200);

                    pn.SIGNED = r[5] + "";

                    if (!r.IsDBNull(6))
                    {
                        pn.SIGNEDDATE = r.GetDateTime(6);
                    }

                    pn.SUPERAPPROV = r[7] + "";

                    if (!r.IsDBNull(8))
                    {
                        pn.SUPERAPPROVEDATE = r.GetDateTime(8);
                    }

                    pn.SSN = IDNUM;

                    pn.NOTECONTACTDESC = r[10] + "";

                    pn.SUPERVISORNOTATION = r[11] + "";

                    pn.SUPERVISORACK1 = r[12] + "";

                    pn.SUPERVISORACK2 = r[13] + "";

                    if (!r.IsDBNull(14))
                    {
                        pn.SUPERVISORNOTATIONDATE = r.GetDateTime(14);
                    }

                    if (!r.IsDBNull(15))
                    {
                        pn.CONTACTDATE = r.GetDateTime(15);
                        pn.strCONTACTDATE = r.GetDateTime(15).ToShortDateString();
                    }
                    else
                    {
                        pn.CONTACTDATE = Convert.ToDateTime(null);
                        pn.strCONTACTDATE = "";
                    }

                    if (!r.IsDBNull(16))
                    {
                        pn.TRAVELMINUTES = r.GetInt32(16);
                    }
                    else
                    {
                        pn.TRAVELMINUTES = 0;
                    }

                    if (!r.IsDBNull(17))
                    {
                        pn.CONTACTMINUTES = r.GetInt32(17);
                    }
                    else
                    {
                        pn.CONTACTMINUTES = 0;
                    }

                    if (!r.IsDBNull(18))
                    {
                        pn.SAFETYASSESSMENT = r.GetDateTime(18);
                    }
                    else
                    {
                        pn.SAFETYASSESSMENT = Convert.ToDateTime(null);
                    }

                    pn.SAFETYASSESSMENTLVL = r[19] + "";
                    pn.SAFETYASSESSMENTLVLDESC = r[20] + "";

                    if (pn.SUPERAPPROV.Trim() == "")
                    {
                        pn.SUPERAPPROV = pn.AUTHOR;
                        pn.SUPERAPPROVEDATE = pn.CREATEDATE;
                    }

                    pn.SERVICECODE = r[21] + "";

                    foreach (ServiceDescription o in SVCS)
                    {
                        string tmp = o.COSTCENTER;
                        //if (tmp.Length == 1)
                        //    tmp = "0" + tmp;

                        if (pn.SERVICECODE == tmp)
                        {
                            pn.SERVICEDESCRIPTION = o.SVCDESCRIPTION;
                            break;
                        }
                    }

                    result.Add(pn);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfAllNotesExceptEncounterForType", ex.Message);

                MemberProgressNotes pn = new MemberProgressNotes();

                pn.NOTATION = ex.Message;

                result.Add(pn);
            }

            return Json(result);
        }


        private string Elipsis(string src, int len)
        {
            string result = "";

            try
            {
                string[] delim = { System.Environment.NewLine, "\r", "\n" };

                string[] arr = src.Split(delim, StringSplitOptions.RemoveEmptyEntries);

                int line = 0;

                foreach (string s in arr)
                {
                    result += s;

                    line += 1;

                    if (result.Length < len)
                    {
                        result += System.Environment.NewLine;
                    }
                    else
                    {
                        result += "...";
                        break;
                    }

                    if (line > 4)
                    {
                        break;
                    }
                }

                if (result.Length > len)
                {
                    result = result.Substring(0, len) + "...";
                }
            }
            catch (Exception ex)
            {
                LogError("Elipsis routine", ex.Message);

                result = "";
            }

            return result;
        }


        [Route("api/Login/GetServiceDescriptions")]
        [HttpGet]
        public JsonResult<List<ServiceDescription>> TheServiceDescriptions()
        {
            List<ServiceDescription> TheList = GetListOfServiceDescriptions();

            return Json(TheList);

        }

        private List<ServiceDescription> GetListOfServiceDescriptions()
        {
            List<ServiceDescription> result = new List<ServiceDescription>();

            try
            {
                string sql = "SELECT [SvcID],[Funder],B.DESCRIPTION as 'FUNDERNAME',[CostCenter],[SvcCode]" +
                    ",[SvcDescription],[UnitType],c.DESCRIPTON  as 'UNITTYPEDESC',[CostPerUnit],A.[ACTIVE]" +
                    ",[AUTHREQ],[COPAY],[Modifier1],[Modifier2],[Modifier3],[Modifier4],[AUTOUNIT],[ROUNDRULE] " +
                    "FROM [dbo].[tblLOOKUPSERVICES] A " +
                    "LEFT OUTER JOIN tblLOOKUPFUNDERS B ON A.Funder = b.CODE " +
                    "LEFT OUTER JOIN tblLOOKUPUNITS C on A.UnitType = C.CODE " +
                    "WHERE a.active = 'Y' ORDER BY [SVCDESCRIPTION] ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    ServiceDescription i = new ServiceDescription();

                    i.ID = r.GetInt64(0);
                    i.FUNDER = r["FUNDER"] + "";
                    i.FUNDERDESCRIPTION = r["FUNDERNAME"] + "";
                    i.COSTCENTER = r["COSTCENTER"] + "";
                    i.SVCCODE = r["SVCCODE"] + "";
                    i.SVCDESCRIPTION = r["SVCDESCRIPTION"] + "";
                    i.UNITTYPE = r["UNITTYPE"] + "";
                    i.UNITTYPEDESCRIPTION = r["UNITTYPEDESC"] + "";
                    if (!r.IsDBNull(8))
                    {
                        string v = r[8] + "";

                        double d = 0.0;

                        if (double.TryParse(v, out d))
                        {
                            i.COSTPERUNIT = d;
                        }
                        else
                        {
                            i.COSTPERUNIT = 0;
                        }
                    }

                    i.ACTIVE = r["ACTIVE"] + "";
                    i.AUTHREQ = r["AUTHREQ"] + "";
                    i.COPAY = r["COPAY"] + "";
                    i.MOD1 = r["MODIFIER1"] + "";
                    i.MOD2 = r["MODIFIER2"] + "";
                    i.MOD3 = r["MODIFIER3"] + "";
                    i.MOD4 = r["MODIFIER4"] + "";

                    if (!r.IsDBNull(16))
                    {
                        string v = r[16] + "";

                        int d = -1;

                        if (int.TryParse(v, out d))
                        {
                            i.AUTOUNIT = d;
                        }
                        else
                        {
                            i.AUTOUNIT = -1;
                        }
                    }

                    if (!r.IsDBNull(17))
                    {
                        string v = r[17] + "";

                        int d = 0;

                        if (int.TryParse(v, out d))
                        {
                            i.ROUNDRULE = d;
                        }
                        else
                        {
                            i.ROUNDRULE = 0;
                        }
                    }

                    result.Add(i);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfServiceDescriptions", ex.Message);
            }

            return result;
        }

        [Route("api/Login/GetAllLookups")]
        [HttpGet]
        public JsonResult<TheLookups> GetAllLookups()
        {
            TheLookups result = new TheLookups();
            try
            {
                result.LOOKUPADDRESSTYPE = GetSpecificLookupList("tblLOOKUPADDRESSTYPE");
                result.LOOKUPADMISSIONTYPE = GetSpecificLookupList("tblLOOKUPADMISSIONTYPE");
                result.LOOKUPALERTTYPE = GetSpecificLookupList("tblLOOKUPALERTTYPE");
                result.LOOKUPASSMTTYPE = GetSpecificLookupList("tblLOOKUPASSMTTYPE");
                result.LOOKUPCOMMERCIALINSURANCE = GetSpecificLookupList("tblLOOKUPCOMMERCIALINSURANCE");
                result.LOOKUPCOMMUNITYSUPPORTSTYPE = GetSpecificLookupList("tblLOOKUPCOMMUNITYSUPPORTSTYPE");
                result.LOOKUPCONCURRENT211REFERRALTARGETS = GetSpecificLookupList("tblLOOKUPCONCURRENT211REFERRALTARGETS");
                result.LOOKUPCONTACTTYPE = GetSpecificLookupList("tblLOOKUPCONTACTTYPE");
                result.LOOKUPCOSTCENTER = GetSpecificLookupListExt("tblLOOKUPCOSTCENTER");
                result.LOOKUPUNITTYPE = GetSpecificLookupListExt("tblLOOKUPUNITS");
                result.LOOKUPCOUNTYNAME = GetSpecificLookupList("tblLOOKUPCOUNTYNAME");
                result.LOOKUPDISCHARGETYPE = GetSpecificLookupList("tblLOOKUPDISCHARGETYPE");
                result.LOOKUPDISPOSITIONS = GetSpecificLookupList("tblLOOKUPDISPOSITIONS");
                result.LOOKUPDISCHARGEREASON = GetSpecificLookupList("tblLOOKUPDISCHARGEREASON");
                result.LOOKUPDRUGOFCHOICE = GetSpecificLookupList("tblLOOKUPDRUGOFCHOICE");
                result.LOOKUPEDUCATION = GetSpecificLookupList("tblLOOKUPEDUCATION");

                result.LOOKUPENCOUNTERRELATION = GetSpecificLookupList("tblLOOKUPENCOUNTERRELATION");
                result.LOOKUPENCOUNTERSDS = GetSpecificLookupList("tblLOOKUPENCOUNTERSDS");
                result.LOOKUPENCOUNTERSTATUS = GetSpecificLookupList("tblLOOKUPENCOUNTERSTATUS");
                result.LOOKUPENCOUNTERSTATUSNOBILLED = GetEncounterStatusWithoutBilled();
                result.LOOKUPENCOUNTERSTATUSALL = GetEncounterStatusAll();

                result.LOOKUPEMPLOYMENT = GetSpecificLookupList("tblLOOKUPEMPLOYMENT");
                result.LOOKUPETHNICITY = GetSpecificLookupList("tblLOOKUPETHNICITY");
                result.LOOKUPFACILITY = GetSpecificLookupListWithProgram("tblLOOKUPFACILITY");
                result.LOOKUPFACILITYTYPE = GetSpecificLookupList("tblLOOKUPFACILITYTYPE");
                result.LOOKUPFOLLOWUPREASONS = GetSpecificLookupList("tblLOOKUPFOLLOWUPREASONS");
                result.LOOKUPFOLLOWUPPROVTYPE = GetSpecificLookupList("tblLOOKUPFOLLOWUPPROVTYPE");
                result.LOOKUPFREQ = GetSpecificLookupList("tblLOOKUPFREQ");
                result.LOOKUPFUNDING = GetSpecificLookupList("tblLOOKUPFUNDING");
                result.LOOKUPFUNDERS = GetSpecificLookupList("tblLOOKUPFUNDERS");
                result.LOOKUPFUNDERSFLEXFUND = GetFundersNoMedicaid();
                result.LOOKUPFUNDERSGROUPHEALTH = GetFundersMedicaidandCommercial();
                result.LOOKUPGENDER = GetSpecificLookupList("tblLOOKUPGENDER");
                result.LOOKUPGROUPINFORMATION = GetSpecificLookupList("tblLOOKUPGROUPINFORMATION");
                result.LOOKUPHEALTHSTATUS = GetSpecificLookupList("tblLOOKUPHEALTHSTATUS");
                result.LOOKUPINCIDENTALEXPENSECATEGORIES = GetSpecificLookupList("tblLOOKUPINCIDENTALEXPENSECATEGORIES");
                result.LOOKUPINCOMESOURCE = GetSpecificLookupList("tblLOOKUPINCOMESOURCE");
                result.LOOKUPLANGUAGES = GetSpecificLookupList("tblLOOKUPLANGUAGES");
                result.LOOKUPLEGALSTATUS = GetSpecificLookupList("tblLOOKUPLEGALSTATUS");
                result.LOOKUPLIVINGSIT = GetSpecificLookupList("tblLOOKUPLIVINGSIT");
                result.LOOKUPMARCHMANACT = GetSpecificLookupList("tblLOOKUPMARCHMANACT");
                result.LOOKUPMEDCARECOVERAGEGROUPS = GetSpecificLookupList("tblLOOKUPMEDCARECOVERAGEGROUPS");
                result.LOOKUPMEDICAIDTYPE = GetSpecificLookupList("tblLOOKUPMEDICAIDTYPE");
                result.LOOKUPMESSAGETYPE = GetSpecificLookupList("tblLOOKUPMESSAGETYPE");
                result.LOOKUPMARITALSTATUS = GetSpecificLookupList("tblLOOKUPMARITALSTATUS");
                result.LOOKUPMHPROBLEM = GetSpecificLookupList("tblLOOKUPMHPROBLEM");
                result.LOOKUPMEMBERWATCHLIST = GetSpecificLookupList("tblLOOKUPMEMBERWATCHLIST");
                result.LOOKUPNOTECONTACTTYPE = GetSpecificLookupList("tblLOOKUPNOTECONTACTTYPE");
                result.LOOKUPOTHERSYSTEMS = GetSpecificLookupList("tblLOOKUPOTHERSYSTEMS");
                result.LOOKUPOPFUTYPE = GetSpecificLookupList("tblLOOKUPOPFUTYPES");
                result.LOOKUPPARTICIPANTTYPE = GetSpecificLookupList("tblLOOKUPPARTICIPANTTYPE");
                result.LOOKUPPHONETYPES = GetSpecificLookupList("tblLOOKUPPHONETYPES");
                result.LOOKUPPREGTRIMESTER = GetSpecificLookupList("tblLOOKUPPREGTRIMESTER");
                result.LOOKUPPROGRAM = GetSpecificLookupList("tblLOOKUPPROGRAM");
                result.LOOKUPPROGRAMMEMBERSHIP = GetSpecificLookupList("tblLOOKUPPROGRAMMEMBERSHIP");
                result.LOOKUPPROGRAMDISCHARGEREASONS = GetSpecificLookupList("tblLOOKUPPROGRAMDISCHARGEREASONS");
                result.LOOKUPPROGRAMEVALPURPOSE = GetSpecificLookupList("tblLOOKUPPROGRAMEVALPURPOSE");
                result.LOOKUPPROGRAMTYPE = GetSpecificLookupList("tblLOOKUPPROGRAMTYPE");
                result.LOOKUPPROGRESSNOTETYPES = GetSpecificLookupList("tblLOOKUPPROGRESSNOTETYPES");
                result.LOOKUPPROVIDERSPECIALTY = GetSpecificLookupList("tblLOOKUPPROVIDERSPECIALTY");
                result.LOOKUPPROVIDERTYPE = GetSpecificLookupList("tblLOOKUPPROVIDERTYPE");
                result.LOOKUPPURPOSEASAM = GetSpecificLookupList("tblLOOKUPPURPOSEASAM");
                result.LOOKUPPURPOSEOFEVAL = GetSpecificLookupList("tblLOOKUPPURPOSEOFEVAL");
                result.LOOKUPPURPOSEOFEVALDCF = GetSpecificLookupList("tblLOOKUPPURPOSEOFEVALDCF");
                result.LOOKUPRACE = GetSpecificLookupList("tblLOOKUPRACE");
                result.LOOKUPRECLEVELOFCARE = GetSpecificLookupList("tblLOOKUPRECLEVELOFCARE");
                result.LOOKUPREFERRALSOURCE = GetSpecificLookupList("tblLOOKUPREFERRALSOURCE");
                result.LOOKUPREFERRALREASON = GetSpecificLookupList("tblLOOKUPREFERRALREASON");
                result.LOOKUPREFSOURCEBLUEBOOK = GetReferralSourceBlueBook();
                result.LOOKUPREMCONTACTWITH = GetSpecificLookupList("tblLOOKUPREMCONTACTWITH");
                result.LOOKUPREMNOTETYPE = GetSpecificLookupList("tblLOOKUPREMNOTETYPE");
                result.LOOKUPRESIDENTSTATUS = GetSpecificLookupList("tblLOOKUPRESIDENTSTATUS");
                result.LOOKUPSCALE = GetSpecificLookupList("tblLOOKUPSCALE");
                result.LOOKUPSCREENINGPROGRAM = GetSpecificLookupList("tblLOOKUPSCREENINGPROGRAM");
                result.LOOKUPSERVICEGAPS = GetEntireListOfServiceGapDescriptors();
                result.LOOKUPSERVICEGAPVERIFICATIONMETHODS = GetSpecificLookupList("tblLOOKUPSERVICEGAPVERIFICATIONMETHODS");
                result.LOOKUPTANFSTATUS = GetSpecificLookupList("tblLOOKUPTANFSTATUS");
                result.LOOKUPTREATMENTPLAN_NEEDPROGRESS = GetSpecificLookupList("tblLOOKUPTMTPLAN_NEEDPROGRESS");
                result.LOOKUPROLES = GetSpecificLookupList("tblLOOKUPROLES");
                result.LOOKUPHIERARCHY = GetSpecificLookupList("tblLOOKUPHIERARCHY");
                result.LOOKUPPROVIDERS = GetAllProviderDesignations();
                result.LOOKUP211REFERRALPROVIDERS = GetListOf211ReferralProviders();
                result.LOOKUPROI = GetSpecificLookupListWACtive("tblLOOKUPROITYPE");
                result.LOOKUPSETARGETTYPE = GetSpecificLookupList("tblLOOKUPSETARGETTYPE");
                result.LOOKUPSETYPE = GetSpecificLookupList("tblLOOKUPSETYPE");
                result.LOOKUPDOCUMENTTYPES = GetSpecificLookupList("tblLOOKUPDOCUMENTTYPES");
                result.LOOKUPTHERAPIES = GetSpecificLookupList("tblLOOKUPTHERAPIES");
                result.LOOKUP_V_DETOX = GetSpecificLookupList("tblLOOKUP_V_DETOX");
                result.LOOKUP_V_EMERSTAB = GetSpecificLookupList("tblLOOKUP_V_EMERSTAB");
                result.LOOKUP_V_INCIDENTAL = GetSpecificLookupList("tblLOOKUP_V_INCIDENTAL");
                result.LOOKUP_V_PREVENTION = GetSpecificLookupList("tblLOOKUP_V_PREVENTION");
                result.LOOKUP_V_RECOVERY = GetSpecificLookupList("tblLOOKUP_V_RECOVERY");
                result.LOOKUP_V_RECOVERYCCST = GetSpecificLookupList("tblLOOKUP_V_RECOVERYCCST");
                result.LOOKUP_V_RECOVERYFACT = GetSpecificLookupList("tblLOOKUP_V_RECOVERYFACT");
                result.LOOKUP_V_RECOVERYINCIDENTAL = GetSpecificLookupList("tblLOOKUP_V_RECOVERYINCIDENTAL");
                result.LOOKUP_V_TREATMENTANDAFTERCARE = GetSpecificLookupList("tblLOOKUP_V_TREATMENTANDAFTERCARE");
                result.LOOKUPSUPPORTSRELATIONSHIP = GetSpecificLookupList("tblLOOKUPSUPPORTSRELATIONSHIP");
                result.LOOKUPSUPPORTSRELATIONSHIPTEAM = GetSpecificLookupListTeam("tblLOOKUPSUPPORTSRELATIONSHIP");
                result.LOOKUPSUPPORTSRELATIONSHIPFAMILY = GetSpecificLookupListFAMILY("tblLOOKUPSUPPORTSRELATIONSHIP");
                result.LOOKUPPROVIDERSFORTRACKINGELEMENTS = GetListOfProvidersForTrackingElements();
                result.LOOKUPASSEMENTKINDS = GetSpecificLookupList("tblLOOKUPASSOCIATEDASSESSMENTTYPES");
                result.LOOKUPSERVICES = GetListOfServiceDescriptions();
                result.LOOKUPSERVICESNOID = GetListOfServiceDescriptionsNoID();
                result.LOOKUPSERVICESNOIDNOSPLIT = GetListOfServiceDescriptionsNoIDNoSplit();
                result.LOOKUPSUPERVISORS = GetListOfSupervisors();
                result.LOOKUPUSERSWITHID = GetSpecificLookupWithID();
                result.LOOKUPCOUNTIES = GetListOfCounties();
                result.LOOKUPPAYER = GetSpecificLookupList("tblLOOKUPPAYER");
                result.LOOKUPDSP = GetListOfDSP();
                result.LOOKUPCASESTATUS = GetSpecificLookupList("tblLOOKUPCASESTATUS");
                result.LOOKUPCASESETTING = GetSpecificLookupList("tblLOOKUPCASESETTING");
                result.LOOKUPTASKS = GetSpecificLookupList("tblLOOKUPTASKS");
                result.LOOKUPTASKSTEPS = GetListOfTaskSteps();
                result.LOOKUPTASKSTATUS = GetSpecificLookupList("tblLOOKUPTASKSTATUS");
                result.LOOKUPFUNDERSFORENCOUNTERS = GetFundersForEncounters();
                result.LOOKUPLOOKUPTABLES = GetAllLookupsTables();
                // we will fill this in later...
                //result.LOOKUPZIPS = GetZipCodeList();
                result.LOOKUPGMRegions = GetListOfGMRRegions();
                result.LOOKUPSAFETYASSESSMENTLVL = GetSpecificLookupListAlternatOrder("tblLOOKUPSAFETYASSESSMENTLVL");
                result.LOOKUPDISTINCTSERVICES = GetDistinctServices();
                result.LOOKUPDISTINCTSERVICESWITHID = GetDistinctServicesWithID();
                result.LOOKUPRELIGION = GetSpecificLookupList("tblLOOKUPRELIGION");
                result.LOOKUPCOMPETENCIES = GetSpecificLookupList("tblLOOKUPUSERCOMPETENCY");
                result.LOOKUPCREDENTIALS = GetSpecificLookupList("tblLOOKUPUSERCREDENTIALS");
                result.LOOKUPACTIVEMEMBERS = GetActiveMembers();
                result.LOOKUPWAITLISTREASON = GetSpecificLookupList("tblLOOKUPWAITLISTREASON");
                result.LOOKUPMEDS = GetSpecificLookupListWithOther("tblLOOKUPMEDS");


                // These are OHIO Specific additions to Health Intech and are omitted from the CFCS API Layer
                // new stuff for HealthIntech

                //result.LOOKUPCONTACTPREFERENCES = GetSpecificLookupList("tblLOOKUPCONTACTPREFERENCES");
                //result.LOOKUPMARITIALSTATUS = GetSpecificLookupList("tblLOOKUPMARITALSTATUS");
                //result.LOOKUPASSESSMENTREASON = GetSpecificLookupList("tblLOOKUPASSESSMENTREASON");
                //result.LOOKUPPATIENTSTATUSLIST = GetSpecificLookupList("tblLOOKUPPATIENTSTATUS");

                //result.LOOKUPADLSELFPERFORMANCELIST = GetSpecificLookupList("tblLOOKUPADLSELFPERFORMANCELIST");
                //result.LOOKUPADLSTATUS90 = GetSpecificLookupList("tblLOOKUPADLSTATUS90");

                //result.LOOKUPUNDERSTANDOTHERS = GetSpecificLookupList("tblLOOKUPUNDERSTANDOTHERS");
                //result.LOOKUPSELFUNDERSTOOD = GetSpecificLookupList("tblLOOKUPSELFUNDERSTOOD");

                //result.LOOKUPBEHAVIORSYMPTOMS = GetSpecificLookupList("tblLOOKUPBEHAVIORSYMPTOMS");

                //result.LOOKUPPSYCHIATRICCONTACT = GetSpecificLookupList("tblLOOKUPPSYCHIATRICCONTACT");
                //result.LOOKUPDISCHARGEFROMPSYCHIATRIC = GetSpecificLookupList("tblLOOKUPDISCHARGEFROMPSYCHIATRIC");
                //result.LOOKUPPSYCHIATRICDURATION = GetSpecificLookupList("tblLOOKUPPSYCHIATRICDURATION");
                //result.LOOKUPPSYCHIATRICADMISSION2YEAR = GetSpecificLookupList("tblLOOKUPPSYCHIATRICADMISSION2YEAR");
                //result.LOOKUPPSYCHIATRICADMISSIONLIFETIME = GetSpecificLookupList("tblLOOKUPPSYCHIATRICADMISSIONLIFETIME");
                //result.LOOKUPFIRSTPSYCHIATRICSTAYAGE = GetSpecificLookupList("tblLOOKUPFIRSTPSYCHIATRICSTAYAGE");

                //result.LOOKUPPRIMARYLANGUAGELIST = GetSpecificLookupList("tblLOOKUPPRIMARYLANGUAGELIST");
                //result.LOOKUPRESIDENCENEEDSTOCHANGELIST = GetSpecificLookupList("tblLOOKUPRESIDENCENEEDSTOCHANGELIST");
                //result.LOOKUPFARTHESTWALKEDDISTANCELIST = GetSpecificLookupList("tblLOOKUPFARTHESTWALKEDDISTANCELIST");
                //result.LOOKUPLIVINGSTATUS = GetSpecificLookupList("tblLOOKUPLIVINGSTATUS");
                //result.LOOKUPLIVINGARRANGEMENT = GetSpecificLookupList("tblLOOKUPLIVINGARRANGEMENT");
                //result.LOOKUPHOSPITALSTAY = GetSpecificLookupList("tblLOOKUPHOSPITALSTAY");
                //result.LOOKUPCOGNITIVESKILLSLIST = GetSpecificLookupList("tblLOOKUPCOGNITIVESKILLSLIST");
                //result.LOOKUPBEHAVIOURPRESENT = GetSpecificLookupList("tblLOOKUPBEHAVIOURPRESENT");
                //result.LOOKUPUNDERSTOODLIST = GetSpecificLookupList("tblLOOKUPUNDERSTOODLIST");
                //result.LOOKUPVISIONDIFFICULTYLIST = GetSpecificLookupList("tblLOOKUPVISIONDIFFICULTYLIST");
                //result.LOOKUPHEARINGDIFFICULTYLIST = GetSpecificLookupList("tblLOOKUPHEARINGDIFFICULTYLIST");
                //result.LOOKUPCHANGEIN90LIST = GetSpecificLookupList("tblLOOKUPCHANGEIN90LIST");
                //result.LOOKUPBEHAVIOURLIST = GetSpecificLookupList("tblLOOKUPBEHAVIOURLIST");
                //result.LOOKUPINDICATORLIST = GetSpecificLookupList("tblLOOKUPINDICATORLIST");
                //result.LOOKUPSOCIALLIST = GetSpecificLookupList("tblLOOKUPSOCIALLIST");

                //result.DECLINELIST = GetSpecificLookupList("tblDECLINELIST");
                //result.LOOKUPALONELIST = GetSpecificLookupList("tblLOOKUPALONELIST");
                //result.LOOKUPSELFPERFORMANCELIST = GetSpecificLookupList("tblLOOKUPSELFPERFORMANCELIST");
                //result.LOOKUPLOCOMOTIONWALKINGLIST = GetSpecificLookupList("tblLOOKUPLOCOMOTIONWALKINGLIST");
                //result.LOOKUPLOCOMOTIONWALKINGDISTANCELIST = GetSpecificLookupList("tblLOOKUPLOCOMOTIONWALKINGDISTANCELIST");
                //result.LOOKUPFARTHESTWALKINGDISTANCELIST = GetSpecificLookupList("tblLOOKUPFARTHESTWALKINGDISTANCELIST");
                //result.LOOKUPFARTHESTWHEELEDDISTANCELIST = GetSpecificLookupList("tblLOOKUPFARTHESTWHEELEDDISTANCELIST");

                //result.LOOKUPACTIVITYTLEVELLIST = GetSpecificLookupList("tblLOOKUPACTIVITYTLEVELLIST");
                //result.LOOKUPGOINGOUTLIST = GetSpecificLookupList("tblLOOKUPGOINGOUTLIST");
                //result.LOOKUPCHANGEINADLLIST = GetSpecificLookupList("tblLOOKUPCHANGEINADLLIST");
                //result.LOOKUPBLADDERLIST = GetSpecificLookupList("tblLOOKUPBLADDERLIST");
                //result.LOOKUPURINARYLIST = GetSpecificLookupList("tblLOOKUPURINARYLIST");
                //result.LOOKUPBOWELLIST = GetSpecificLookupList("tblLOOKUPBOWELLIST");


                //result.LOOKUPSLEEPPROBLEM = GetSpecificLookupList("tblLOOKUPSLEEPPROBLEM");
                //result.LOOKUPINSIGHTDEGREE = GetSpecificLookupList("tblLOOKUPINSIGHTDEGREE");
                //result.LOOKUPALCOHOLCONSUMED30LIST = GetSpecificLookupList("tblLOOKUPALCOHOLCONSUMED30LIST");
                //result.LOOKUPINTOXICSUBSTANCETIME = GetSpecificLookupList("tblLOOKUPINTOXICSUBSTANCETIME");
                //result.LOOKUPINJECTIONDRUGUSE = GetSpecificLookupList("tblLOOKUPINJECTIONDRUGUSE");
                //result.LOOKUPWITHDRAWALSYMPTOMS = GetSpecificLookupList("tblLOOKUPWITHDRAWALSYMPTOMS");
                //result.LOOKUPINJURIOUSATTEMPT = GetSpecificLookupList("tblLOOKUPINJURIOUSATTEMPT");
                //result.LOOKUPSELFINJURIOUSTOKILL = GetSpecificLookupList("tblLOOKUPSELFINJURIOUSTOKILL");
                //result.LOOKUPVIOLENCE = GetSpecificLookupList("tblLOOKUPVIOLENCE");
                //result.LOOKUPBEHAVIOURDISTURBANCE = GetSpecificLookupList("tblLOOKUPBEHAVIOURDISTURBANCE");
                //result.LOOKUPPOLICEINTERVENTION = GetSpecificLookupList("tblLOOKUPPOLICEINTERVENTION");
                //result.LOOKUPDISORDEREDTHINKING = GetSpecificLookupList("tblLOOKUPDISORDEREDTHINKING");
                //result.LOOKUPDECISIONMAKING90 = GetSpecificLookupList("tblLOOKUPDECISIONMAKING90");
                //result.LOOKUPIADLCAPACITY = GetSpecificLookupList("tblLOOKUPIADLCAPACITY");
                //result.LOOKUPLIFEEVENTS = GetSpecificLookupList("tblLOOKUPLIFEEVENTS");
                //result.LOOKUPINTENSEFEAREVENTS = GetSpecificLookupList("tblLOOKUPINTENSEFEAREVENTS");

                //result.LOOKUPTREATMENTMODALITIES = GetSpecificLookupList("tblLOOKUPTREATMENTMODALITIES");
                //result.LOOKUPINTERVENTIONFOCUS = GetSpecificLookupList("tblLOOKUPINTERVENTIONFOCUS");
                //result.LOOKUPELECTROCONVULSIVETHERAPY = GetSpecificLookupList("tblLOOKUPELECTROCONVULSIVETHERAPY");
                //result.LOOKUPCONTROLINTERVENTION = GetSpecificLookupList("tblLOOKUPCONTROLINTERVENTION");
                //result.LOOKUPAUTHORIZEDACTIVITIES = GetSpecificLookupList("tblLOOKUPAUTHORIZEDACTIVITIES");
                //result.LOOKUPDISTURBEDRELATIONSHIP = GetSpecificLookupList("tblLOOKUPDISTURBEDRELATIONSHIP");
                //result.LOOKUPSOCIALLIST = GetSpecificLookupList("tblLOOKUPSOCIALLIST");
                //result.LOOKUPTYPEOFADMISSION = GetSpecificLookupList("tblLOOKUPTYPEOFADMISSION");

                //result.LOOKUPEMPLOYMENTSTATUS = GetSpecificLookupList("tblLOOKUPEMPLOYMENTSTATUS");
                //result.LOOKUPEMPLOYMENTARRANGEMENT = GetSpecificLookupList("tblLOOKUPEMPLOYMENTARRANGEMENT");
                //result.LOOKUPFORMALEDUCATION = GetSpecificLookupList("tblLOOKUPFORMALEDUCATION");
                //result.LOOKUPUNEMPLOYMENTRISK = GetSpecificLookupList("tblLOOKUPUNEMPLOYMENTRISK");
                //result.LOOKUPSOCIALSUPPORT = GetSpecificLookupList("tblLOOKUPSOCIALSUPPORT");
                //result.LOOKUPEXPECTATIONOFSTAY = GetSpecificLookupList("tblLOOKUPEXPECTATIONOFSTAY");
                //result.LOOKUPPSYCHIATRICSYMPTOMS = GetSpecificLookupList("tblLOOKUPPSYCHIATRICSYMPTOMS");
                //result.LOOKUPPROVISIONALDSMCATEGORY = GetSpecificLookupList("tblLOOKUPPROVISIONALDSMCATEGORY");
                //result.LOOKUPMEDICALDIAGNOSES = GetSpecificLookupList("tblLOOKUPMEDICALDIAGNOSES");
                //result.LOOKUPDISCHARGEASSESSMENT = GetSpecificLookupList("tblLOOKUPDISCHARGEASSESSMENT");

                //result.LOOKUPDIAGPRESENTLIST = GetSpecificLookupList("tblLOOKUPDIAGPRESENTLIST");
                //result.LOOKUPFALLSLIST = GetSpecificLookupList("tblLOOKUPFALLSLIST");
                //result.LOOKUPPROBLEMFREQUENCYLIST = GetSpecificLookupList("tblLOOKUPPROBLEMFREQUENCYLIST");
                //result.LOOKUPDYSPNEALIST = GetSpecificLookupList("tblLOOKUPDYSPNEALIST");
                //result.LOOKUPFATIGUELIST = GetSpecificLookupList("tblLOOKUPFATIGUELIST");
                //result.LOOKUPPAIN1LIST = GetSpecificLookupList("tblLOOKUPPAIN1LIST");
                //result.LOOKUPPAIN2LIST = GetSpecificLookupList("tblLOOKUPPAIN2LIST");

                //result.LOOKUPPAIN3LIST = GetSpecificLookupList("tblLOOKUPPAIN3LIST");
                //result.LOOKUPPAIN4LIST = GetSpecificLookupList("tblLOOKUPPAIN4LIST");
                //result.LOOKUPSELFREPORTEDHEALTHLIST = GetSpecificLookupList("tblLOOKUPSELFREPORTEDHEALTHLIST");
                //result.LOOKUPTOBACCOLIST = GetSpecificLookupList("tblLOOKUPTOBACCOLIST");
                //result.LOOKUPALCOHOLLIST = GetSpecificLookupList("tblLOOKUPALCOHOLLIST");
                //result.LOOKUPNUTRITIONLIST = GetSpecificLookupList("tblLOOKUPNUTRITIONLIST");
                //result.LOOKUPPRESSUREULCERLIST = GetSpecificLookupList("tblLOOKUPPRESSUREULCERLIST");
                //result.LOOKUPFOOTPROBLEMLIST = GetSpecificLookupList("tblLOOKUPFOOTPROBLEMLIST");
                //result.RELASHIPSHIPTOPERSONLIST = GetSpecificLookupList("tblLOOKUPRELATIONSHIPTOPERSON");
                //result.LIVEWITHPERSONLIST = GetSpecificLookupList("tblLOOKUPLIVESWITHPERSON");
                //result.INFORMALHELPLIST = GetSpecificLookupList("tblLOOKUPINFORMALHELP");
                //result.TREATMENTLIST = GetSpecificLookupList("tblLOOKUPTREATMENTLIST");
                //result.SELFSUFFICIENCYLIST = GetSpecificLookupList("tblLOOKUPSELFSUFFICIENCY90LIST");
                //result.PROBLEMRELATEDTODETERIORATIONLIST = GetSpecificLookupList("tblLOOKUPPRECIPITATINGEVENT");
                //result.LOOKUPMEDICATIONADHERENCE = GetSpecificLookupList("tblLOOKUPMEDICATIONADHERENCE");

                //result.LOOKUP_CA_ADAPTABILITY = GetSpecificLookupList("tblLOOKUP_CA_ADAPTABILITY");
                //result.LOOKUP_CA_ADHERENT_WITH_MEDICATIONS = GetSpecificLookupList("tblLOOKUP_CA_ADHERENT_WITH_MEDICATIONS");
                //result.LOOKUP_CA_ADL_SELF_PERFORMANCE = GetSpecificLookupList("tblLOOKUP_CA_ADL_SELF_PERFORMANCE");
                //result.LOOKUP_CA_ALCOHOL = GetSpecificLookupList("tblLOOKUP_CA_ALCOHOL");
                //result.LOOKUP_CA_ASSESSMENT_REASONS = GetSpecificLookupList("tblLOOKUP_CA_ASSESSMENT_REASONS");
                //result.LOOKUP_CA_BEHAVIOR = GetSpecificLookupList("tblLOOKUP_CA_BEHAVIOR");
                //result.LOOKUP_CA_BEHAVIOR_PRESENT = GetSpecificLookupList("tblLOOKUP_CA_BEHAVIOR_PRESENT");
                //result.LOOKUP_CA_BLADDER_CONTINENCE = GetSpecificLookupList("tblLOOKUP_CA_BLADDER_CONTINENCE");
                //result.LOOKUP_CA_BOWEL_COLLECTION_DEVICE = GetSpecificLookupList("tblLOOKUP_CA_BOWEL_COLLECTION_DEVICE");
                //result.LOOKUP_CA_BOWEL_CONTINENCE = GetSpecificLookupList("tblLOOKUP_CA_BOWEL_CONTINENCE");
                //result.LOOKUP_CA_CARE_GOALS = GetSpecificLookupList("tblLOOKUP_CA_CARE_GOALS");
                //result.LOOKUP_CA_CAREGIVER_HELP = GetSpecificLookupList("tblLOOKUP_CA_CAREGIVER_HELP");
                //result.LOOKUP_CA_CHANGE_IN_ADL = GetSpecificLookupList("tblLOOKUP_CA_CHANGE_IN_ADL");
                //result.LOOKUP_CA_CHANGE_IN_DECISION = GetSpecificLookupList("tblLOOKUP_CA_CHANGE_IN_DECISION");
                //result.LOOKUP_CA_COGNITIVIE_SKILLS = GetSpecificLookupList("tblLOOKUP_CA_COGNITIVIE_SKILLS");
                //result.LOOKUP_CA_DAYS_WENT_OUT = GetSpecificLookupList("tblLOOKUP_CA_DAYS_WENT_OUT");
                //result.LOOKUP_CA_DEGREE_COMPLETED = GetSpecificLookupList("tblLOOKUP_CA_DEGREE_COMPLETED");
                //result.LOOKUP_CA_DISEASE_CODE = GetSpecificLookupList("tblLOOKUP_CA_DISEASE_CODE");
                //result.LOOKUP_CA_DYSPNEA = GetSpecificLookupList("tblLOOKUP_CA_DYSPNEA");
                //result.LOOKUP_CA_EDUCATION_STATUS = GetSpecificLookupList("tblLOOKUP_CA_EDUCATION_STATUS");
                //result.LOOKUP_CA_EFFECT = GetSpecificLookupList("tblLOOKUP_CA_EFFECT");
                //result.LOOKUP_CA_EXPECTED_SERVICES = GetSpecificLookupList("tblLOOKUP_CA_EXPECTED_SERVICES");
                //result.LOOKUP_CA_FATIGUE = GetSpecificLookupList("tblLOOKUP_CA_FATIGUE");
                //result.LOOKUP_CA_FOOT_PROBLEMS = GetSpecificLookupList("tblLOOKUP_CA_FOOT_PROBLEMS");
                //result.LOOKUP_CA_FORMAL_CARE = GetSpecificLookupList("tblLOOKUP_CA_FORMAL_CARE");
                //result.LOOKUP_CA_FORMAL_TREATMENTS = GetSpecificLookupList("tblLOOKUP_CA_FORMAL_TREATMENTS");
                //result.LOOKUP_CA_FUTURE_NEEDS = GetSpecificLookupList("tblLOOKUP_CA_FUTURE_NEEDS");
                //result.LOOKUP_CA_GENDER = GetSpecificLookupList("tblLOOKUP_CA_GENDER");
                //result.LOOKUP_CA_HEARING_DIFFICULTY = GetSpecificLookupList("tblLOOKUP_CA_HEARING_DIFFICULTY");
                //result.LOOKUP_CA_HOME_ENVIRONMENT = GetSpecificLookupList("tblLOOKUP_CA_HOME_ENVIRONMENT");
                //result.LOOKUP_CA_IADL_SELF_PERFORMANCE = GetSpecificLookupList("tblLOOKUP_CA_IADL_SELF_PERFORMANCE");
                //result.LOOKUP_CA_INTELLECTUAL_DISABILITY = GetSpecificLookupList("tblLOOKUP_CA_INTELLECTUAL_DISABILITY");
                //result.LOOKUP_CA_INTENT = GetSpecificLookupList("tblLOOKUP_CA_INTENT");
                //result.LOOKUP_CA_LAST_HOSPITAL_STAY = GetSpecificLookupList("tblLOOKUP_CA_LAST_HOSPITAL_STAY");
                //result.LOOKUP_CA_LEGAL_GUARDIANSHIP = GetSpecificLookupList("tblLOOKUP_CA_LEGAL_GUARDIANSHIP");
                //result.LOOKUP_CA_LIVES_WITH_CHILD_YOUTH = GetSpecificLookupList("tblLOOKUP_CA_LIVES_WITH_CHILD_YOUTH");
                //result.LOOKUP_CA_LIVING_ARRANGEMENT = GetSpecificLookupList("tblLOOKUP_CA_LIVING_ARRANGEMENT");
                //result.LOOKUP_CA_LIVING_STATUS = GetSpecificLookupList("tblLOOKUP_CA_LIVING_STATUS");
                //result.LOOKUP_CA_LOCOMOTION = GetSpecificLookupList("tblLOOKUP_CA_LOCOMOTION");
                //result.LOOKUP_CA_MOOD = GetSpecificLookupList("tblLOOKUP_CA_MOOD");
                //result.LOOKUP_CA_NUTRITION_INTAKE = GetSpecificLookupList("tblLOOKUP_CA_NUTRITION_INTAKE");
                //result.LOOKUP_CA_PAIN1 = GetSpecificLookupList("tblLOOKUP_CA_PAIN1");
                //result.LOOKUP_CA_PAIN2 = GetSpecificLookupList("tblLOOKUP_CA_PAIN2");
                //result.LOOKUP_CA_PAIN3 = GetSpecificLookupList("tblLOOKUP_CA_PAIN3");
                //result.LOOKUP_CA_PAIN4 = GetSpecificLookupList("tblLOOKUP_CA_PAIN4");
                //result.LOOKUP_CA_PARENTS_MARITAL_STATUS = GetSpecificLookupList("tblLOOKUP_CA_PARENTS_MARITAL_STATUS");
                //result.LOOKUP_CA_PHYSICAL_ACTIVITY_HOURS = GetSpecificLookupList("tblLOOKUP_CA_PHYSICAL_ACTIVITY_HOURS");
                //result.LOOKUP_CA_PRENATAL_HISTORY = GetSpecificLookupList("tblLOOKUP_CA_PRENATAL_HISTORY");
                //result.LOOKUP_CA_PRESSURE_ULCER = GetSpecificLookupList("tblLOOKUP_CA_PRESSURE_ULCER");
                //result.LOOKUP_CA_PRIMARY_LANGUAGE = GetSpecificLookupList("tblLOOKUP_CA_PRIMARY_LANGUAGE");
                //result.LOOKUP_CA_PROBLEM_FREQUENCY = GetSpecificLookupList("tblLOOKUP_CA_PROBLEM_FREQUENCY");
                //result.LOOKUP_CA_RELATIONSHIP = GetSpecificLookupList("tblLOOKUP_CA_RELATIONSHIP");
                //result.LOOKUP_CA_SELF_INJURIOUS = GetSpecificLookupList("tblLOOKUP_CA_SELF_INJURIOUS");
                //result.LOOKUP_CA_SELF_REPORTED_MOOD = GetSpecificLookupList("tblLOOKUP_CA_SELF_REPORTED_MOOD");
                //result.LOOKUP_CA_SELF_SUFFICIENCY = GetSpecificLookupList("tblLOOKUP_CA_SELF_SUFFICIENCY");
                //result.LOOKUP_CA_SERVICES_PROVIDED_AT_SCHOOL = GetSpecificLookupList("tblLOOKUP_CA_SERVICES_PROVIDED_AT_SCHOOL");
                //result.LOOKUP_CA_SOCIAL = GetSpecificLookupList("tblLOOKUP_CA_SOCIAL");
                //result.LOOKUP_CA_UNDERSTANDS = GetSpecificLookupList("tblLOOKUP_CA_UNDERSTANDS");
                //result.LOOKUP_CA_UNDERSTOOD = GetSpecificLookupList("tblLOOKUP_CA_UNDERSTOOD");
                //result.LOOKUP_CA_URINARY = GetSpecificLookupList("tblLOOKUP_CA_URINARY");
                //result.LOOKUP_CA_VISION_DIFFICULTY = GetSpecificLookupList("tblLOOKUP_CA_VISION_DIFFICULTY");

                //result.LOOKUP_HCA_ADHERENT_WITH_MEDICATIONS = GetSpecificLookupList("tblLOOKUP_HCA_ADHERENT_WITH_MEDICATIONS");
                //result.LOOKUP_HCA_ADL_SELF_PERFORMANCE = GetSpecificLookupList("tblLOOKUP_HCA_ADL_SELF_PERFORMANCE");
                //result.LOOKUP_HCA_ALCOHOL = GetSpecificLookupList("tblLOOKUP_HCA_ALCOHOL");
                //result.LOOKUP_HCA_ASSESSMENT_REASONS = GetSpecificLookupList("tblLOOKUP_HCA_ASSESSMENT_REASONS");
                //result.LOOKUP_HCA_BEHAVIOR = GetSpecificLookupList("tblLOOKUP_HCA_BEHAVIOR");
                //result.LOOKUP_HCA_BEHAVIOR_PRESENT = GetSpecificLookupList("tblLOOKUP_HCA_BEHAVIOR_PRESENT");
                //result.LOOKUP_HCA_BLADDER_CONTINENCE = GetSpecificLookupList("tblLOOKUP_HCA_BLADDER_CONTINENCE");
                //result.LOOKUP_HCA_BOWEL_CONTINENCE = GetSpecificLookupList("tblLOOKUP_HCA_BOWEL_CONTINENCE");
                //result.LOOKUP_HCA_CHANGE_IN_ADL = GetSpecificLookupList("tblLOOKUP_HCA_CHANGE_IN_ADL");
                //result.LOOKUP_HCA_CHANGE_IN_DECISION = GetSpecificLookupList("tblLOOKUP_HCA_CHANGE_IN_DECISION");
                //result.LOOKUP_HCA_COGNITIVIE_SKILLS = GetSpecificLookupList("tblLOOKUP_HCA_COGNITIVIE_SKILLS");
                //result.LOOKUP_HCA_DAYS_WENT_OUT = GetSpecificLookupList("tblLOOKUP_HCA_DAYS_WENT_OUT");
                //result.LOOKUP_HCA_DISEASE_CODE = GetSpecificLookupList("tblLOOKUP_HCA_DISEASE_CODE");
                //result.LOOKUP_HCA_DISTANCE_WALKED = GetSpecificLookupList("tblLOOKUP_HCA_DISTANCE_WALKED");
                //result.LOOKUP_HCA_DISTANCE_WHEELED = GetSpecificLookupList("tblLOOKUP_HCA_DISTANCE_WHEELED");
                //result.LOOKUP_HCA_DYSPNEA = GetSpecificLookupList("tblLOOKUP_HCA_DYSPNEA");
                //result.LOOKUP_HCA_FALLS = GetSpecificLookupList("tblLOOKUP_HCA_FALLS");
                //result.LOOKUP_HCA_FATIGUE = GetSpecificLookupList("tblLOOKUP_HCA_FATIGUE");
                //result.LOOKUP_HCA_FOOT_PROBLEMS = GetSpecificLookupList("tblLOOKUP_HCA_FOOT_PROBLEMS");
                //result.LOOKUP_HCA_FORMAL_TREATMENTS = GetSpecificLookupList("tblLOOKUP_HCA_FORMAL_TREATMENTS");
                //result.LOOKUP_HCA_HEARING_DIFFICULTY = GetSpecificLookupList("tblLOOKUP_HCA_HEARING_DIFFICULTY");
                //result.LOOKUP_HCA_IADL_SELF_PERFORMANCE = GetSpecificLookupList("tblLOOKUP_HCA_IADL_SELF_PERFORMANCE");
                //result.LOOKUP_HCA_INFORMAL_HELP = GetSpecificLookupList("tblLOOKUP_HCA_INFORMAL_HELP");
                //result.LOOKUP_HCA_LAST_HOSPITAL_STAY = GetSpecificLookupList("tblLOOKUP_HCA_LAST_HOSPITAL_STAY");
                //result.LOOKUP_HCA_LIVES_WITH_PERSON = GetSpecificLookupList("tblLOOKUP_HCA_LIVES_WITH_PERSON");
                //result.LOOKUP_HCA_LIVING_ARRANGEMENT = GetSpecificLookupList("tblLOOKUP_HCA_LIVING_ARRANGEMENT");
                //result.LOOKUP_HCA_LIVING_BETTER_OFF = GetSpecificLookupList("tblLOOKUP_HCA_LIVING_BETTER_OFF");
                //result.LOOKUP_HCA_LIVING_STATUS = GetSpecificLookupList("tblLOOKUP_HCA_LIVING_STATUS");
                //result.LOOKUP_HCA_LOCOMOTION = GetSpecificLookupList("tblLOOKUP_HCA_LOCOMOTION");
                //result.LOOKUP_HCA_LOCOMOTION_TIMED = GetSpecificLookupList("tblLOOKUP_HCA_LOCOMOTION_TIMED");
                //result.LOOKUP_HCA_MARITAL_STATUS = GetSpecificLookupList("tblLOOKUP_HCA_MARITAL_STATUS");
                //result.LOOKUP_HCA_MOOD = GetSpecificLookupList("tblLOOKUP_HCA_MOOD");
                //result.LOOKUP_HCA_NUTRITION_INTAKE = GetSpecificLookupList("tblLOOKUP_HCA_NUTRITION_INTAKE");
                //result.LOOKUP_HCA_PAIN1 = GetSpecificLookupList("tblLOOKUP_HCA_PAIN1");
                //result.LOOKUP_HCA_PAIN2 = GetSpecificLookupList("tblLOOKUP_HCA_PAIN2");
                //result.LOOKUP_HCA_PAIN3 = GetSpecificLookupList("tblLOOKUP_HCA_PAIN3");
                //result.LOOKUP_HCA_PAIN4 = GetSpecificLookupList("tblLOOKUP_HCA_PAIN4");
                //result.LOOKUP_HCA_PHYSICAL_ACTIVITY_HOURS = GetSpecificLookupList("tblLOOKUP_HCA_PHYSICAL_ACTIVITY_HOURS");
                //result.LOOKUP_HCA_PRECIPITATING_EVENT = GetSpecificLookupList("tblLOOKUP_HCA_PRECIPITATING_EVENT");
                //result.LOOKUP_HCA_PRESSURE_ULCER = GetSpecificLookupList("tblLOOKUP_HCA_PRESSURE_ULCER");
                //result.LOOKUP_HCA_PRIMARY_LANGUAGE = GetSpecificLookupList("tblLOOKUP_HCA_PRIMARY_LANGUAGE");
                //result.LOOKUP_HCA_PROBLEM_FREQUENCY = GetSpecificLookupList("tblLOOKUP_HCA_PROBLEM_FREQUENCY");
                //result.LOOKUP_HCA_RELATIONSHIP = GetSpecificLookupList("tblLOOKUP_HCA_RELATIONSHIP");
                //result.LOOKUP_HCA_SELF_REPORTED_HEALTH = GetSpecificLookupList("tblLOOKUP_HCA_SELF_REPORTED_HEALTH");
                //result.LOOKUP_HCA_SELF_REPORTED_MOOD = GetSpecificLookupList("tblLOOKUP_HCA_SELF_REPORTED_MOOD");
                //result.LOOKUP_HCA_SELF_SUFFICIENCY = GetSpecificLookupList("tblLOOKUP_HCA_SELF_SUFFICIENCY");
                //result.LOOKUP_HCA_SOCIAL = GetSpecificLookupList("tblLOOKUP_HCA_SOCIAL");
                //result.LOOKUP_HCA_SOCIAL_ACTIVITES_CHANGE = GetSpecificLookupList("tblLOOKUP_HCA_SOCIAL_ACTIVITES_CHANGE");
                //result.LOOKUP_HCA_TIME_ALONE = GetSpecificLookupList("tblLOOKUP_HCA_TIME_ALONE");
                //result.LOOKUP_HCA_TOBACCO = GetSpecificLookupList("tblLOOKUP_HCA_TOBACCO");
                //result.LOOKUP_HCA_TREATMENTS = GetSpecificLookupList("tblLOOKUP_HCA_TREATMENTS");
                //result.LOOKUP_HCA_UNDERSTANDS = GetSpecificLookupList("tblLOOKUP_HCA_UNDERSTANDS");
                //result.LOOKUP_HCA_UNDERSTOOD = GetSpecificLookupList("tblLOOKUP_HCA_UNDERSTOOD");
                //result.LOOKUP_HCA_URINARY = GetSpecificLookupList("tblLOOKUP_HCA_URINARY");
                //result.LOOKUP_HCA_VISION_DIFFICULTY = GetSpecificLookupList("tblLOOKUP_HCA_VISION_DIFFICULTY");

            }
            catch (Exception ex)
            {
                LogError("GetAllLookups", ex.Message);
            }

            return Json(result);
        }

        [Route("api/Login/SaveNote")]
        [HttpPost]
        public JsonResult<Boolean> SaveNote([FromBody] MemberProgressNotes payload)
        {

            // Console.WriteLine(Request.ToString());

            //    int l = note.Length;

            //    MemberProgressNotes mp = JsonConvert.DeserializeObject<MemberProgressNotes>(note);

            try
            {
                tblMemberProgressNotes pn = new tblMemberProgressNotes(DBCON());

                pn.NOTATION = payload.NOTATION;
                pn.NOTECONTACTTYPE = payload.NOTECONTACTDESC;
                pn.NOTETYPE = payload.NOTETYPEDESC;
                pn.CREATEDATE = ServerDateTime();
                pn.CONTACTDATE = payload.CONTACTDATE;
                pn.CREATEDBY = payload.AUTHOR;
                pn.SSN = payload.SSN;

                pn.Add();
                
                return Json( true);
            }
            catch 
            {
                return Json(false);
            }


        }

        private List<CodedDescriptor> GetSpecificLookupList(string TNAME)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM " + TNAME + " WHERE ACTIVE = 'Y' ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupList", ex.Message + System.Environment.NewLine + TNAME);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptorExt> GetSpecificLookupListExt(string TNAME)
        {
            List<CodedDescriptorExt> result = new List<CodedDescriptorExt>();
            string sql = "";

            try
            {
                if (TNAME == "tblLOOKUPUNITS")
                { sql = "SELECT * FROM " + TNAME + " ORDER BY DESCRIPTON "; }
                else
                { sql = "SELECT * FROM " + TNAME + " ORDER BY DESCRIPTION "; }

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorExt res = new CodedDescriptorExt();

                    res.code = r[0] + "";
                    res.description = r[1] + "";
                    res.authreq = r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupListExt", ex.Message);

                CodedDescriptorExt err = new CodedDescriptorExt();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message
                err.authreq = "";

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetEncounterStatusWithoutBilled()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                //string sql = "SELECT [CODE],[DESCRIPTION] FROM tblLOOKUPENCOUNTERSTATUS ";
                //sql += "WHERE ACTIVE = 'Y' AND DESCRIPTION <> 'BILLED' ORDER BY [DESCRIPTION]";

                string sql = "SELECT [CODE],[DESCRIPTION] FROM tblLOOKUPENCOUNTERSTATUS ";
                sql += "WHERE ACTIVE = 'Y' ";
                sql += "AND CODE NOT IN ('03','04','07','08','09','10') ";
                sql += "ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetEncounterStatusWithoutBilled", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetEncounterStatusAll()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM tblLOOKUPENCOUNTERSTATUS ";
                sql += "ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetEncounterStatusAll", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptorWPrograms> GetSpecificLookupListWithProgram(string TNAME)
        {
            List<CodedDescriptorWPrograms> result = new List<CodedDescriptorWPrograms>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION],[ACTIVE],[PROGRAM] FROM " + TNAME + " WHERE ACTIVE = 'Y' ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWPrograms res = new CodedDescriptorWPrograms();

                    res.code = r[0] + "";
                    res.description = r[1] + "";
                    res.active = r[2] + "";
                    res.program = r[3] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupListWithProgram", ex.Message + System.Environment.NewLine + TNAME);

                CodedDescriptorWPrograms err = new CodedDescriptorWPrograms();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptorWID> GetSpecificLookupWithID()
        {
            List<CodedDescriptorWID> result = new List<CodedDescriptorWID>();

            try
            {
                string sql = "SELECT DISTINCT TBLUSERLOGINID, UL.Username, FIRSTNAME + ' ' + LASTNAME ";
                sql += "FROM TBLUSERLOGINS UL ";
                sql += "LEFT OUTER JOIN tblUserHierarchy UH ON UH.UserName = UL.Username ";
                sql += "WHERE UH.HierCode <> 'SUPER' AND UH.HierCode <> 'CFBH' ";
                sql += "ORDER BY USERNAME ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWID res = new CodedDescriptorWID();

                    if (r[0] != null)
                        res.id = Convert.ToInt64(r[0]);
                    res.code = r[1] + "";
                    res.description = r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupWithID", ex.Message);

                result = new List<CodedDescriptorWID>();
            }

            return result;
        }

        private List<ZipCodeDescriptor> GetZipCodeList()
        {
            List<ZipCodeDescriptor> result = new List<ZipCodeDescriptor>();

            try
            {
                string sql = "SELECT * , (SELECT TOP 1 county from [tblLOOKUPZIPCOUNTYCITY] B where a.zipcode = b.zip) as 'COUNTY' FROM tblLOOKUPZIPCODES A " +
                    "WHERE A.ZIPCODESTATE in ('PA','NJ','TN','FL','MD','NY','DC','RI','MA','CN') ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    ZipCodeDescriptor zip = new ZipCodeDescriptor();

                    zip.zipcode = r[0] + "";
                    zip.zipcodecity = r[1] + "";
                    zip.zipcodestate = r[2] + "";

                    if (!r.IsDBNull(3))
                        zip.ziplong = r.GetDouble(3);

                    if (!r.IsDBNull(4))
                        zip.ziplat = r.GetDouble(4);

                    zip.zipcounty = r[6] + "";

                    result.Add(zip);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetZipCodeList", ex.Message);

                result.Clear();

                ZipCodeDescriptor res = new ZipCodeDescriptor();
                res.zipcode = ex.Message;
                res.zipcodecity = ex.Message;

                result.Add(res);
            }

            return result;
        }

        private List<CodedDescriptor> GetFundersNoMedicaid()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM tblLOOKUPFUNDERS ";
                sql += "WHERE ACTIVE = 'Y' ";
                sql += " AND REFERENCENUM IS NOT NULL ";
                //sql += " AND DESCRIPTION NOT LIKE '%MEDICAID%' ";
                sql += " AND GROUPHEALTH = 1 ";
                sql += "ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetFundersNoMedicaid", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetFundersMedicaidandCommercial()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM tblLOOKUPFUNDERS ";
                sql += "WHERE ACTIVE = 'Y' ";
                sql += " AND REFERENCENUM IS NOT NULL ";
                //sql += " AND DESCRIPTION NOT LIKE '%MEDICAID%' ";
                sql += " AND GROUPHEALTH = 1 OR MEDICAID = 1 ";
                sql += "ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetFundersMedicaidandCommercial", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptorWithOther> GetReferralSourceBlueBook()
        {
            List<CodedDescriptorWithOther> result = new List<CodedDescriptorWithOther>();

            try
            {
                string sql = "SELECT DISTINCT RSID, RSFIRSTNAME, RSLASTNAME ";
                sql += "FROM tblREFERRALSOURCE ";
                sql += " WHERE RSISAGENCY = 0 ";
                sql += " AND RSACTIVE = 1 ";
                sql += "ORDER BY RSFIRSTNAME, RSLASTNAME ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWithOther res = new CodedDescriptorWithOther();

                    if (r[0].ToString() != "")
                    { res.id = Convert.ToInt64(r[0]); }
                    res.code = r[1] + "";
                    res.other = r[2] + "";
                    res.description = r[1] + "" + " " + r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetReferralSourceBlueBook", ex.Message);

                CodedDescriptorWithOther err = new CodedDescriptorWithOther();

                err.id = 0;
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetTeamReferralSource()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT CODE, DESCRIPTION FROM tblLOOKUPREFERRALSOURCE ";
                sql += "ORDER BY DESCRIPTION ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetTeamReferralSource", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;

        }

        private List<CodedDescriptorWID> GetStaffBlueBook(long rsid)
        {
            List<CodedDescriptorWID> result = new List<CodedDescriptorWID>();

            try
            {
                //string sql = "SELECT DISTINCT RSID, RSLASTNAME, LTRIM(RTRIM(COALESCE(RSFIRSTNAME, ''))) + ' ' + LTRIM(RTRIM(RSLASTNAME)) AS DESCRIPTION ";
                string sql = "SELECT DISTINCT RSID, RSFIRSTNAME, RSLASTNAME ";
                sql += "FROM tblREFERRALSOURCE ";
                sql += "WHERE RSAGENCYID = @RSAGENCYID ";
                sql += " AND RSISAGENCY = 0 ";
                sql += " AND RSACTIVE = 1 ";
                sql += "ORDER BY RSFIRSTNAME, RSLASTNAME ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@RSAGENCYID", SqlDbType.BigInt).Value = rsid;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWID res = new CodedDescriptorWID();

                    if (r[0].ToString() != "")
                    { res.id = Convert.ToInt64(r[0]); }
                    res.code = r[1] + "";
                    res.description = r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetStaffBlueBook", ex.Message);

                CodedDescriptorWID err = new CodedDescriptorWID();

                err.id = 0;
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptorWithOther> GetStaffBlueBookWithOther(long rsid)
        {
            List<CodedDescriptorWithOther> result = new List<CodedDescriptorWithOther>();

            try
            {
                //string sql = "SELECT DISTINCT RSID, RSLASTNAME, LTRIM(RTRIM(COALESCE(RSFIRSTNAME, ''))) + ' ' + LTRIM(RTRIM(RSLASTNAME)) AS DESCRIPTION ";
                string sql = "SELECT DISTINCT RSID, RSFIRSTNAME, RSLASTNAME ";
                sql += "FROM tblREFERRALSOURCE ";
                sql += "WHERE RSAGENCYID = @RSAGENCYID ";
                sql += " AND RSISAGENCY = 0 ";
                sql += " AND RSACTIVE = 1 ";
                sql += "ORDER BY RSFIRSTNAME, RSLASTNAME ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@RSAGENCYID", SqlDbType.BigInt).Value = rsid;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWithOther res = new CodedDescriptorWithOther();

                    if (r[0].ToString() != "")
                    { res.id = Convert.ToInt64(r[0]); }
                    res.code = r[1] + "";
                    res.other = r[2] + "";
                    res.description = r[1] + "" + " " + r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetStaffBlueBookWithOther", ex.Message);

                CodedDescriptorWithOther err = new CodedDescriptorWithOther();

                err.id = 0;
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptorWithOther> GetStaffBlueBookWithInactive(long rsid)
        {
            List<CodedDescriptorWithOther> result = new List<CodedDescriptorWithOther>();

            try
            {
                //string sql = "SELECT DISTINCT RSID, RSLASTNAME, LTRIM(RTRIM(COALESCE(RSFIRSTNAME, ''))) + ' ' + LTRIM(RTRIM(RSLASTNAME)) AS DESCRIPTION ";
                string sql = "SELECT DISTINCT RSID, RSFIRSTNAME, RSLASTNAME ";
                sql += "FROM tblREFERRALSOURCE ";
                sql += "WHERE RSAGENCYID = @RSAGENCYID ";
                sql += " AND RSISAGENCY = 0 ";
                sql += "ORDER BY RSFIRSTNAME, RSLASTNAME ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@RSAGENCYID", SqlDbType.BigInt).Value = rsid;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWithOther res = new CodedDescriptorWithOther();

                    if (r[0].ToString() != "")
                    { res.id = Convert.ToInt64(r[0]); }
                    res.code = r[1] + "";
                    res.other = r[2] + "";
                    res.description = r[1] + "" + " " + r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetStaffBlueBookWithInactive", ex.Message);

                CodedDescriptorWithOther err = new CodedDescriptorWithOther();

                err.id = 0;
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<ServiceGapDescriptor> GetEntireListOfServiceGapDescriptors()
        {
            List<ServiceGapDescriptor> result = new List<ServiceGapDescriptor>();
            try
            {
                string sql = "SELECT [CMSID],[GAPDESCRIPTION],[GAPDOMAIN],[MEASURE],[TYPE],[CPT],[HCPCS],[UB],[DIAG],[LOINC] " +
                                "FROM [tblLOOKUPSERVICEGAPDESCRIPTORS] ORDER BY [GAPDESCRIPTION] ";
                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ServiceGapDescriptor a = new ServiceGapDescriptor();

                    a.CMSID = r[0] + "";
                    a.GAPDESCRIPTION = r[1] + "";
                    a.GAPDOMAIN = r[2] + "";
                    a.MEASURE = r[3] + "";
                    a.TYPE = r[4] + "";
                    a.CPT = r[5] + "";
                    a.HCPCS = r[6] + "";
                    a.UB = r[7] + "";
                    a.DIAG = r[8] + "";
                    a.LOINC = r[9] + "";

                    result.Add(a);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("GetEntireListOfServiceGapDescriptors" + ex.ToString()));
            }
            return result;
        }

        private List<CodedDescriptor> GetSpecificLookupListTeam(string TNAME)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM " + TNAME + " WHERE ACTIVE = 'Y' AND TEAMTAB = 'Y' ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupList", ex.Message + System.Environment.NewLine + TNAME);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetSpecificLookupListFAMILY(string TNAME)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM " + TNAME + " WHERE ACTIVE = 'Y' AND FAMILYTAB = 'Y' ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupList", ex.Message + System.Environment.NewLine + TNAME);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }        //

        private List<ProviderDesignation> GetListOf211ReferralProviders()
        {
            List<ProviderDesignation> result = new List<ProviderDesignation>();

            try
            {
                string sql = "SELECT DISTINCT PROVIDERID,PROVIDERNAME " +
                    "from tblLOOKUPREFERRALPROVIDERS WHERE REFERRAL211 = 'Y' ORDER BY PROVIDERNAME";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    ProviderDesignation p = new ProviderDesignation();

                    p.ProviderID = r[0] + "";
                    p.ProviderName = r[1] + "";

                    result.Add(p);
                }

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOf211ReferralProviders", ex.Message);
                result.Clear();

                ProviderDesignation p = new ProviderDesignation();

                p.ProviderName = ex.Message;

                result.Add(p);
            }

            return result;
        }

        private List<ProviderDesignation> GetListOfProvidersForTrackingElements()
        {
            List<ProviderDesignation> result = new List<ProviderDesignation>();

            try
            {
                string sql = "SELECT MIN(PMID) as 'PMID',SUBPROVIDERID,SUBPROVIDERNAME " +
                            "FROM tblProviderSubProvider WHERE SUBPROVIDERID IN (" +
                            "select DISTINCT SUBPROVIDERID " +
                            "FROM tblProviderSubProvider " +
                            "WHERE RIGHT(LEFT(SUBPROVIDERID,3),1) = '-') " +
                            "GROUP BY SUBPROVIDERID,SUBPROVIDERNAME " +
                            "ORDER BY SUBPROVIDERNAME ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    ProviderDesignation p = new ProviderDesignation();
                    p.mpcID = r.GetInt64(0);
                    p.ProviderID = r[1] + "";
                    p.ProviderName = r[2] + "";

                    result.Add(p);
                }

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfProvidersForTrackingElements", ex.Message);
            }

            return result;
        }

        private List<ServiceDescription> GetListOfServiceDescriptionsNoID()
        {
            List<ServiceDescription> result = new List<ServiceDescription>();

            try
            {
                string sql = "SELECT DISTINCT [CostCenter],[SvcCode]" +
                    ",[SvcDescription],[UnitType],[CostPerUnit],A.[ACTIVE]" +
                    ",[AUTHREQ],[RELATEDSPLITCODE] " +
                    //[COPAY],[Modifier1],[Modifier2],[Modifier3],[Modifier4],[AUTOUNIT] " +
                    "FROM [dbo].[tblLOOKUPSERVICES] A " +
                    "WHERE a.active = 'Y' " +
                    " AND FUNDER = 'xxxxx' " +
                    "ORDER BY [SVCDESCRIPTION] ";


                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    ServiceDescription i = new ServiceDescription();

                    i.COSTCENTER = r["COSTCENTER"] + "";
                    i.SVCCODE = r["SVCCODE"] + "";
                    i.SVCDESCRIPTION = r["SVCDESCRIPTION"] + "";
                    i.UNITTYPE = r["UNITTYPE"] + "";
                    if (r["COSTPERUNIT"].ToString() != "")
                    {
                        string v = r["COSTPERUNIT"] + "";

                        double d = 0.0;

                        if (double.TryParse(v, out d))
                        {
                            i.COSTPERUNIT = d;
                        }
                        else
                        {
                            i.COSTPERUNIT = 0;
                        }
                    }

                    i.ACTIVE = r["ACTIVE"] + "";
                    i.AUTHREQ = r["AUTHREQ"] + "";
                    //i.COPAY = r["COPAY"] + "";
                    //i.MOD1 = r["MODIFIER1"] + "";
                    //i.MOD2 = r["MODIFIER2"] + "";
                    //i.MOD3 = r["MODIFIER3"] + "";
                    //i.MOD4 = r["MODIFIER4"] + "";

                    //if (r["AUTOUNIT"].ToString() != "")
                    //{
                    //    string v = r["AUTOUNIT"] + "";

                    //    int d = -1;

                    //    if (int.TryParse(v, out d))
                    //    {
                    //        i.AUTOUNIT = d;
                    //    }
                    //    else
                    //    {
                    //        i.AUTOUNIT = -1;
                    //    }
                    //}

                    i.RELATEDSPLITCODE = r["RELATEDSPLITCODE"] + "";

                    result.Add(i);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfServiceDescriptionsNoID", ex.Message);
            }

            return result;
        }

        private List<ServiceDescription> GetListOfServiceDescriptionsNoIDNoSplit()
        {
            List<ServiceDescription> result = new List<ServiceDescription>();

            try
            {
                //string sql = "SELECT DISTINCT [CostCenter],[SvcCode]" +
                //    ",[SvcDescription],[UnitType],[CostPerUnit],A.[ACTIVE]" +
                //    ",[AUTHREQ],[RELATEDSPLITCODE] " +
                //    //[COPAY],[Modifier1],[Modifier2],[Modifier3],[Modifier4],[AUTOUNIT] " +
                //    "FROM [dbo].[tblLOOKUPSERVICES] A " +
                //    "WHERE a.active = 'Y' " +
                //    " AND FUNDER = 'xxxxx' " +
                //    "ORDER BY [SVCDESCRIPTION] ";

                //string sql = "SELECT DISTINCT SVCID,[CostCenter],[SvcCode], ";
                //sql += "[SvcDescription],[UnitType],[CostPerUnit],A.[ACTIVE], ";
                //sql += "[AUTHREQ],MODIFIER1,MODIFIER2,MODIFIER3,MODIFIER4,[RELATEDSPLITCODE] ";
                //sql += "FROM [dbo].[tblLOOKUPSERVICES] A ";
                //sql += "WHERE  (a.active = 'Y' AND FUNDER = 'xxxxx' ";
                //sql += " AND NOT EXISTS (SELECT * FROM tbllookupservices c WHERE cast(a.svcid as varchar) = c.RelatedSplitCode)) ";
                //sql += "ORDER BY [SVCDESCRIPTION] ";

                string sql = "SELECT * FROM [dbo].[tblLOOKUPSERVICES] A ";
                sql += "WHERE  (a.active = 'Y' AND FUNDER = 'xxxxx' ";
                sql += " AND NOT EXISTS (SELECT * FROM tbllookupservices c WHERE cast(a.svcid as varchar) = c.RelatedSplitCode)) ";
                sql += "ORDER BY [SVCDESCRIPTION] ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    ServiceDescription i = new ServiceDescription();

                    i.ID = Convert.ToInt64(r["SVCID"]);
                    i.COSTCENTER = r["COSTCENTER"] + "";
                    i.SVCCODE = r["SVCCODE"] + "";
                    i.SVCDESCRIPTION = r["SVCDESCRIPTION"] + "";
                    i.UNITTYPE = r["UNITTYPE"] + "";
                    if (r["COSTPERUNIT"].ToString() != "")
                    {
                        string v = r["COSTPERUNIT"] + "";

                        double d = 0.0;

                        if (double.TryParse(v, out d))
                        {
                            i.COSTPERUNIT = d;
                        }
                        else
                        {
                            i.COSTPERUNIT = 0;
                        }
                    }

                    i.ACTIVE = r["ACTIVE"] + "";
                    i.AUTHREQ = r["AUTHREQ"] + "";
                    i.COPAY = r["COPAY"] + "";
                    i.MOD1 = r["MODIFIER1"] + "";
                    i.MOD2 = r["MODIFIER2"] + "";
                    i.MOD3 = r["MODIFIER3"] + "";
                    i.MOD4 = r["MODIFIER4"] + "";
                    i.RELATEDSPLITCODE = r["RELATEDSPLITCODE"] + "";

                    if (r["BCBANOTEREQUIRED"] != DBNull.Value)
                    { i.BCBANOTE = Convert.ToBoolean(r["BCBANOTEREQUIRED"]); }
                    else
                    { i.BCBANOTE = false; }

                    if (r["AUTOUNIT"] != DBNull.Value)
                    { i.AUTOUNIT = Convert.ToInt32(r["AUTOUNIT"]); }
                    else
                    { i.AUTOUNIT = 0; }

                    if (r["ROUNDRULE"] != DBNull.Value)
                    { i.ROUNDRULE = Convert.ToInt32(r["ROUNDRULE"]); }
                    else
                    { i.ROUNDRULE = 0; }

                    result.Add(i);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfServiceDescriptionsNoIDNoSplit", ex.Message);
            }

            return result;
        }

        private List<CodedDescriptorWID> GetListOfSupervisors()
        {
            List<CodedDescriptorWID> result = new List<CodedDescriptorWID>();

            try
            {
                //string sql = "SELECT DISTINCT UL.TBLUSERLOGINID AS ID, US.SUPERVISORNAME AS CODE, UL.FIRSTNAME + UL.LASTNAME AS DESCRIPTION ";
                //sql += "FROM TBLUSERSSUPERVISOR US ";
                //sql += "LEFT OUTER JOIN TBLUSERLOGINS UL ON UL.USERNAME = US.SUPERVISORNAME ";
                //sql += "WHERE UL.TBLUSERLOGINID IS NOT NULL ";
                //sql += "ORDER BY SUPERVISORNAME ";

                string sql = "SELECT DISTINCT UL.TBLUSERLOGINID AS ID, UL.Username AS CODE, UL.FIRSTNAME + ' ' + UL.LASTNAME AS DESCRIPTION ";
                sql += "FROM tblUserLogins UL ";
                sql += "LEFT OUTER JOIN tblUserHierarchy UH ON UL.USERNAME = UH.UserName ";
                sql += "WHERE UL.TBLUSERLOGINID IS NOT NULL ";
                sql += "AND UH.HierCode = 'SUPER' ";
                sql += "ORDER BY ul.Username ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWID res = new CodedDescriptorWID();

                    if (r[0] != null)
                    { res.id = Convert.ToInt64(r[0]); }
                    res.code = r[1] + "";
                    res.description = r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfSupervisors", ex.Message);

                result = null;

                CodedDescriptorWID cd = new CodedDescriptorWID();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetListOfDSP()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT UL.USERNAME, FIRSTNAME + ' ' + LASTNAME ";
                sql += "FROM TBLUSERROLES UR ";
                sql += "LEFT OUTER JOIN TBLUSERLOGINS UL ON UL.USERNAME = UR.USERNAME ";
                sql += "WHERE UR.RoleCode = 'DSP' AND COALESCE(UL.DEACTIVE,'N') <> 'Y' ";
                sql += "ORDER BY UL.USERNAME ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfDSP", ex.Message);

                result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetListOfCounties()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT DISTINCT COUNTY AS CODE, COUNTY AS DESCRIPTION ";
                sql += "FROM tblMemberAddress ";
                sql += "ORDER BY COUNTY ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfCounties", ex.Message);

                result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<ProviderDesignation> GetAllProviderDesignations()
        {
            List<ProviderDesignation> result = new List<ProviderDesignation>();

            try
            {
                string sql = "SELECT DISTINCT PID,PNAME,CNPI,SNPI FROM (" +
                                "select PROVIDERID as 'PID', PROVIDERNAME as 'PNAME',CONTRACTNPI AS 'CNPI',SUBPROVNPI as 'SNPI' from dbo.tblProviderSubProvider " +
                                "union " +
                                "select SUBPROVIDERID , SUBPROVIDERNAME,CONTRACTNPI,SUBPROVNPI from dbo.tblProviderSubProvider " +
                                ") A " +
                                "ORDER BY PNAME";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    ProviderDesignation p = new ProviderDesignation();

                    p.ProviderID = r["PID"] + "";
                    p.ProviderName = r["PNAME"] + "";
                    p.ContractNPI = r["CNPI"] + "";
                    p.ProviderNPI = r["SNPI"] + "";

                    // we might have some cruft in the system so lets weed out all provider id's without a - as the third character
                    //if (p.ProviderID.Substring(0, 3).EndsWith("-"))
                    result.Add(p);
                }

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

                
            }
            catch (Exception ex)
            {
                LogError("GetAllProviderDesignations", ex.Message);
                result.Clear();

                ProviderDesignation p = new ProviderDesignation();

                p.ProviderName = ex.Message;

                result.Add(p);
            }

            return result;
        }

        private List<CodedDescriptorWithActive> GetSpecificLookupListWACtive(string TNAME)
        {
            List<CodedDescriptorWithActive> result = new List<CodedDescriptorWithActive>();

            try
            {
                string sql = "SELECT CODE,[DESCRIPTION],ACTIVE FROM " + TNAME + " ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWithActive res = new CodedDescriptorWithActive();

                    res.code = r[0] + "";
                    res.description = r[1] + "";
                    res.active = r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupListWACtive", ex.Message);

                CodedDescriptorWithActive err = new CodedDescriptorWithActive();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message
                err.active = "";

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<NumericDescriptor> GetSpecificLookupListWithOther(string TNAME)
        {
            List<NumericDescriptor> result = new List<NumericDescriptor>();

            try
            {
                string sql = "SELECT [MEDID],[DESCRIPTION] FROM " + TNAME + " WHERE ACTIVE = 'Y' ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    NumericDescriptor res = new NumericDescriptor();

                    if (r[0] != null)
                        res.number = Convert.ToInt64(r[0]);
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupListWithOther", ex.Message + System.Environment.NewLine + TNAME);

                NumericDescriptor err = new NumericDescriptor();

                err.number = 0;
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetSpecificLookupListAlternatOrder(string TNAME)
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM " + TNAME + " WHERE ACTIVE = 'Y' ORDER BY [CODE]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetSpecificLookupList", ex.Message + System.Environment.NewLine + TNAME);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetListOfTaskSteps()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT DISTINCT TASKCODE AS CODE, TASKSTEPDESCRIPTION AS DESCRIPTION ";
                sql += "FROM tblLOOKUPTASKSTEPS ";
                sql += "ORDER BY TASKCODE ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfTaskSteps", ex.Message);

                //result = null;

                CodedDescriptor cd = new CodedDescriptor();
                cd.code = "----";
                cd.description = ex.Message;

                result.Add(cd);
            }
            return result;
        }

        private List<CodedDescriptor> GetFundersForEncounters()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT [CODE],[DESCRIPTION] FROM tblLOOKUPFUNDERS ";
                sql += "WHERE ACTIVE = 'Y' ";
                sql += " AND REFERENCENUM IS NOT NULL ";
                sql += "ORDER BY [DESCRIPTION]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.Parameters.Add("@TNAME",SqlDbType.VarChar).Value = TNAME;
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetFundersForEncounters", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;
        }

        private List<CodedDescriptor> GetAllLookupsTables()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "select OBJECT_ID,NAME " +
                            "from sys.tables st " +
                            "where name like 'tblLOOKUP%' and type_desc = 'USER_TABLE' " +
                            "and (select COUNT(*) from syscolumns where id = object_id(st.NAME)) = 3 " +
                            "order by name";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    CodedDescriptor cd = new CodedDescriptor();
                    cd.code = r[0] + "";
                    cd.description = r[1] + "";

                    result.Add(cd);
                }

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetAllLookupsTables", ex.Message);
            }

            return result;
        }

        private List<CodedDescriptor> GetActiveMembers()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "select distinct firstname + ' ' + lastname as membername, MM.SSN ";
                sql += "from tblmembermain mm ";
                sql += "inner join tblMemberAuthorizedServices aus on aus.SSN = mm.SSN ";
                sql += "where (aus.ENDDATE is null or aus.ENDDATE = '' or CAST(aus.ENDDATE as Date) >= CAST(DATEADD(Year,-1,GETDATE()) AS Date)) ";
                sql += "and mm.ssn in (select MCDSSN from tblMemberCopayDeductible) ";
                sql += "order by membername ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[1] + "";
                    res.description = r[0] + "";

                    result.Add(res);
                }

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }
            catch (Exception ex)
            {
                LogError("GetActiveMembers", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;

        }

        private List<CodedDescriptor> GetDistinctServices()
        {
            List<CodedDescriptor> result = new List<CodedDescriptor>();

            try
            {
                string sql = "SELECT DISTINCT COSTCENTER,SVCDESCRIPTION FROM tblLookupServices WHERE ACTIVE = 'Y' " +
                                "ORDER BY SvcDescription";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptor res = new CodedDescriptor();

                    res.code = r[0] + "";
                    res.description = r[1] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetDistinctServices", ex.Message);

                CodedDescriptor err = new CodedDescriptor();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;


        }

        private List<CodedDescriptorWID> GetDistinctServicesWithID()
        {
            List<CodedDescriptorWID> result = new List<CodedDescriptorWID>();

            try
            {
                string sql = "SELECT DISTINCT SVCID,COSTCENTER,SVCDESCRIPTION, ";
                sql += "(SELECT DESCRIPTION FROM TBLLOOKUPFUNDERS WHERE CODE = FUNDER) AS FUNDER ";
                sql += "FROM tblLookupServices WHERE ACTIVE = 'Y' ";
                sql += " AND FUNDER = 'xxxxx' ";
                sql += "ORDER BY SvcDescription ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                SqlDataReader r = cmd.ExecuteReader();

                result.Clear();

                while (r.Read())
                {
                    CodedDescriptorWID res = new CodedDescriptorWID();

                    res.id = Convert.ToInt32(r[0]);
                    res.code = r[1] + "";
                    //res.description = r[2] + " - " + r[3] + "";
                    res.description = r[2] + "";

                    result.Add(res);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetDistinctServicesWithID", ex.Message);

                CodedDescriptorWID err = new CodedDescriptorWID();

                err.code = "";
                err.description = ex.Message; // build a aresult that shows the message

                result.Clear(); // clear any results we maay have at the moment

                result.Add(err); // add it to the results list
            }

            return result;


        }

        private List<GMRRegions> GetListOfGMRRegions()
        {
            List<GMRRegions> result = new List<GMRRegions>();

            try
            {
                string sql = "SELECT [gmrID],[gmrNAME],[gmrDESC],[gmrLAT],[gmrLON],[gmrZOOM] FROM [tblPOIGeoMapRegions] ORDER BY [gmrNAME]";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    GMRRegions gmr = new GMRRegions();

                    gmr.ID = r[0].ToString();
                    gmr.NAME = r[1] + "";
                    gmr.DESC = r[2] + "";

                    double d = 0.0;

                    if (double.TryParse(r[3] + "", out d))
                    {
                        gmr.LAT = d;
                    }

                    if (double.TryParse(r[4] + "", out d))
                    {
                        gmr.LON = d;
                    }

                    int i = 0;

                    if (int.TryParse(r[5] + "", out i))
                    {
                        gmr.ZOOM = i;
                    }

                    result.Add(gmr);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("", ex.Message);
            }

            return result;
        }

        

        public bool WriteProgressNote(MemberProgressNotes pn)
        {
            bool result = true;

            try
            {
                if (pn.mpnID <= 0)
                {
                    // adding a new note

                    tblMemberProgressNotes tmp = new tblMemberProgressNotes(DBCON());
                    tmp.Initialize();

                    tmp.SSN = pn.SSN;
                    tmp.NOTETYPE = pn.NOTETYPEDESC;
                    tmp.NOTECONTACTTYPE = pn.NOTECONTACTDESC;

                    //DB 05-16-2012 use session
                    tmp.CREATEDBY = pn.AUTHOR;
                    //tmp.CREATEDBY = GetCurrentUserName();

                    tmp.CREATEDATE = ServerDateTime();
                    tmp.NOTATION = pn.NOTATION;
                    tmp.SIGNED = pn.SIGNED;
                    tmp.SIGNDATE = ServerDateTime();
                    tmp.SUPERVISORNOTATION = pn.SUPERVISORNOTATION;
                    tmp.SUPERVISORACK1 = pn.SUPERVISORACK1;
                    tmp.SUPERVISORACK2 = pn.SUPERVISORACK2;
                    tmp.SUPERVISORNOTATIONDATE = pn.SUPERVISORNOTATIONDATE;
                    tmp.CONTACTDATE = pn.CONTACTDATE;
                    tmp.TRAVELTIME = pn.TRAVELMINUTES;
                    tmp.CONTACTTIME = pn.CONTACTMINUTES;
                    tmp.SAFETYASSESSMENT = pn.SAFETYASSESSMENT;
                    tmp.SAFETYASSESSMENTLVL = pn.SAFETYASSESSMENTLVL;
                    tmp.SERVICECODE = pn.SERVICECODE;

                    tmp.Add();

                    tmp = null;
                }
                else
                {
                    // editing an old note

                    tblMemberProgressNotes tmp = new tblMemberProgressNotes(DBCON());
                    tmp.Initialize();

                    tmp.Read(pn.mpnID);

                    tmp.NOTETYPE = pn.NOTETYPEDESC;
                    tmp.NOTECONTACTTYPE = pn.NOTECONTACTDESC;
                    tmp.OLDNOTATION = tmp.NOTATION; // lets save one prior notation
                    tmp.NOTATION = pn.NOTATION;

                    tmp.SUPERVISORNOTATION = pn.SUPERVISORNOTATION;
                    tmp.SUPERVISORACK1 = pn.SUPERVISORACK1;
                    tmp.SUPERVISORACK2 = pn.SUPERVISORACK2;
                    tmp.SUPERVISORNOTATIONDATE = pn.SUPERVISORNOTATIONDATE;
                    tmp.CONTACTDATE = pn.CONTACTDATE;

                    tmp.TRAVELTIME = pn.TRAVELMINUTES;
                    tmp.CONTACTTIME = pn.CONTACTMINUTES;
                    tmp.SAFETYASSESSMENT = pn.SAFETYASSESSMENT;
                    tmp.SAFETYASSESSMENTLVL = pn.SAFETYASSESSMENTLVL;
                    tmp.SERVICECODE = pn.SERVICECODE;

                    tmp.Update();

                    tmp = null;
                }

                // new stuff for supervision messaging just in case the note requires it
                if (pn.NOTETYPEDESC == "04") // a crisis
                    SendMessageToThisMembersProgramsSupervisors(pn.SSN, pn.AUTHOR);
            }
            catch (Exception ex)
            {
                LogError("WriteProgressNote", ex.Message);

                result = false;
            }

            return result;
        }

        public DateTime ServerDateTime()
        {
            DateTime result = DateTime.Now;

            try
            {
                string sql = "SELECT GETDATE()";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    result = r.GetDateTime(0);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("ServerDateTime", ex.Message);
                result = DateTime.Now;
            }

            return result;
        }

        public MemberDetailsShort GetCompleteMemberDetails(string SSN, string UNAME, string IPADDR)
        {

            if (UNAME != "" && IPADDR != "")
                LogThisAccess(SSN, UNAME, IPADDR, "FULLMEMBER"); // for Hipaa logging

            MemberDetailsShort mem = new MemberDetailsShort();

            try
            {
                string sql = "SELECT MMID,FIRSTNAME,LASTNAME,MIDDLENAME,DOB,GENDER,ETHNICITY,RACE,SSN, Phone1, Phone1Type, Phone1Ext, " +
                    "Phone2, Phone2Type, Phone2Ext, Email, ParentGuardian, ParentGuardPhone, LOCATIONDATE " +
                    "FROM tblMemberMain A " +
                    "WHERE A.SSN=@SSN ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = SSN;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    if (r["MMID"] != DBNull.Value)
                    {
                        mem.MMID = Convert.ToInt64(r["MMID"]);
                    }
                    mem.FirstName = r["FIRSTNAME"].ToString() + "";
                    mem.LastName = r["LASTNAME"].ToString() + "";
                    mem.MiddleName = r["MIDDLENAME"].ToString() + "";

                    if (r["DOB"] != DBNull.Value)
                    {
                        mem.DOB = r.GetDateTime(r.GetOrdinal("DOB")).ToShortDateString();
                    }

                    if (r["LOCATIONDATE"] != DBNull.Value)
                    {
                        mem.LocationDate = r.GetDateTime(r.GetOrdinal("LOCATIONDATE"));
                    }


                    mem.Gender = r["GENDER"].ToString() + "";
                    mem.Ethnicity = r["ETHNICITY"].ToString() + "";
                    mem.Race = r["RACE"].ToString() + "";
                    mem.SSN = r["SSN"].ToString() + "";
                    mem.Phone1 = r["Phone1"].ToString() + "";
                    mem.Phone1Type = r["Phone1Type"].ToString() + "";
                    mem.Phone1Ext = r["Phone1Ext"].ToString() + "";
                    mem.Phone2 = r["Phone2"].ToString() + "";
                    mem.Phone2Type = r["Phone2Type"].ToString() + "";
                    mem.Phone2Ext = r["Phone2Ext"].ToString() + "";
                    mem.Email = r["Email"].ToString() + "";
                    mem.ParentGuardian = r["ParentGuardian"].ToString() + "";
                    mem.ParentGuardPhone = r["ParentGuardPhone"].ToString() + "";
                    //mem.CurrentCaseManager = GetCurrentMemberCaseManager(SSN);
                    //mem.CaseManager = GetFirstAndLastName(mem.CurrentCaseManager);
                }

                mem.memberAddress = GetMemberAddress(SSN);
                //mem.memberAddressHistory = GetMemberAddressHistory(SSN);
                //mem.memberAuths = GetAuthsAvailableForMember(SSN);

                //mem.memberProviders = GetListOfMemberProviders(mem.MMID);
                //mem.MemberSupports = GetListOfMemberRelationships(mem.MMID);
                //mem.MemberServices = GetListOfMemberServices(SSN);
                //mem.MemberLanguages = GetListOfMemberLanguages(SSN);
                //mem.MemberContacts = GetListOfMemberContacts(SSN);
                //mem.MemberAdmissions = GetListOfMemberAdmissions(SSN);
                //mem.MemberPrograms = GetListOfMemberProgramMemberships(SSN);
                //mem.MemberGroups = GetListOfMemberGroupMemberships(SSN);
                //mem.MemberOtherSystems = GetListOfMemberOtherSystemIDs(SSN);
                //mem.MemberMeds = GetListOfMemberMedications(SSN);
                //mem.MemberServiceProviders = GetListOfMemberServiceProviders(SSN);
                //mem.MemberScores = GetAllScoresFor(SSN);
                //mem.MemberROIs = GetListOfMemberROIs(SSN);
                //mem.MemberWatchList = GetListOfMemberWatchListEntries(SSN);
                //mem.MemberEligibilityList = GetListOfEligibilityEntriesFor(SSN);
                //mem.MemberContactList = GetListOfMemberContactInfo(SSN);
                //mem.MemberDMEProviders = GetListOfMemberDMEServiceProviders(SSN);
                //mem.MemberLabs = GetListOfLabsOnFile(SSN);
                //mem.MemberAlerts = GetListofMemberAlertsOnFile(SSN);
                //mem.MemberTherapies = GetListOfMemberTherapies(SSN);
                //mem.MemberImmunizations = GetListOfMemberImmunizations(SSN);
                //mem.MemberCommunitySupports = GetListOftblMemberCommunitySupports(SSN);
                //mem.MemberOtherSystemIDs = GetOtherSystemIDsForThisMember(SSN);

                //mem.ConsumerLoginAndContactPreferences = GetConsumerUserInfo(SSN);


                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }
            catch (Exception ex)
            {
                LogError("GetCompleteMemberDetails", ex.Message);
            }
            return mem;
        }

        private void LogThisAccess(string ssn, string uname, string ipaddr, string context)
        {
            try
            {
                string sql = "INSERT INTO TBLACCESSLOG (USERNAME,USERIP,SSNACCESSED,DATEACCESSED,CONTEXT) VALUES (@UN,@UIP,@SSN,@DT,@CONTXT)";

                //ignore passed in username. Use session value instead DB 05-23-2012
                // MCompton 7/10/2017 This does not appear to be used, and causing exception in new Family Link 
                //string _userName = HttpContext.Current.Session["UserName"].ToString();

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;

                //ignore passed in username. Use session value instead DB 05-23-2012
                //cmd.Parameters.Add("@UN", SqlDbType.VarChar).Value = uname;
                //FB 18190 - dmcgrady
                cmd.Parameters.Add("@UN", SqlDbType.VarChar).Value = uname;
                //cmd.Parameters.Add("@UN", SqlDbType.VarChar).Value = _userName;

                cmd.Parameters.Add("@UIP", SqlDbType.VarChar).Value = ipaddr;
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = ssn;
                cmd.Parameters.Add("@DT", SqlDbType.DateTime).Value = ServerDateTime();
                cmd.Parameters.Add("@CONTXT", SqlDbType.VarChar).Value = context;

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }
            catch (Exception ex)
            {
                LogError("LogThisAccess", ex.Message);

                // Fail Silently?

            }

        }
        
        private bool SendMessageToThisMembersProgramsSupervisors(string SSN, string Login)
        {
            bool res = true;

            try
            {
                // Get a list of programs that the member is part of currently
                string sql = "SELECT [mpmPROGRAM] AS PROGRAM FROM [tblMemberProgramMembership] WHERE [mpmSSN] = @SSN " +
                             "AND ((GETDATE() between [mpmSDATE] AND [mpmEDATE]) OR (GETDATE() >= [mpmSDATE] AND  [mpmEDATE] IS NULL))";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = SSN.Trim();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    List<string> Logins = GetLoginsInASpecificGroup(r[0] + "");
                    List<string> RLogins = RestrictLoginsListToHierarchyType(Logins, "SUPER");

                    MemberDetailsShort mem = GetCompleteMemberDetails(SSN, "Internal Messaging", "");

                    string Message = "An Encounter Note was added by " + Login + "\nFor Consumer " + mem.FirstName + " " + mem.LastName + "\n" +
                        "And needs supervision review.";

                    SendMessageTo(Logins, Message, Login);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("PRIVATE: SendMessageToThisMembersProgramsSupervisors", ex.Message);
            }

            return res;
        }

        private List<string> GetLoginsInASpecificGroup(string groupcode)
        {
            List<string> res = new List<string>();

            try
            {
                string sql = "SELECT UserName AS UGUserName FROM tblUserGroups WHERE GROUPCODE = @GRP";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@GRP", SqlDbType.VarChar).Value = groupcode.Trim();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    res.Add(r[0] + "");
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("PRIVATE: GetLoginsInASpecificGroup", ex.Message);
            }

            return res;
        }

        private List<string> GetLoginsOfUserSupers(string login)
        {
            List<string> res = new List<string>();

            try
            {
                string sql = " SELECT SupervisorName AS Supervisor FROM tblUsersSupervisor WHERE UserName = @Login";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = login.Trim();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    res.Add(r[0] + "");
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("PRIVATE: GetLoginsOfUserSupers", ex.Message);
            }

            return res;
        }

        private bool isuserinhierarchy(string login, string htype)
        {
            bool res = false;

            try
            {
                string sql = "SELECT [UserName] from [tblUserHierarchy] WHERE [HierCode] = @HTYPE AND [UserName] = @LOGIN";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@HTYPE", SqlDbType.VarChar).Value = htype.Trim();
                cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar).Value = login.Trim();

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    res = true;
                    break;
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("PRIVATE: isuserinhierarchy", ex.Message);
            }

            return res;
        }

        private List<string> RestrictLoginsListToHierarchyType(List<string> logins, string HType)
        {
            List<string> res = new List<string>();

            try
            {
                foreach (string s in logins)
                {
                    if (isuserinhierarchy(s, HType))
                        res.Add(s);
                }
            }
            catch (Exception ex)
            {
                LogError("PRIVATE: RestrictLoginsListToHierarchyType", ex.Message);
            }

            return res;
        }

        private bool SendMessageToThisUsersSupervisors(string SSN, string Login)
        {
            bool res = true;
            List<string> resL = new List<string>();
            try
            {
                // Get a list of programs that the member is part of currently
                string sql = "SELECT SupervisorName AS Supervisor FROM tblUsersSupervisor WHERE UserName = @Login";
                //             "AND ((GETDATE() between [mpmSDATE] AND [mpmEDATE]) OR (GETDATE() >= [mpmSDATE] AND  [mpmEDATE] IS NULL))";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = SSN.Trim();
                cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = Login.Trim();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    //List<string> Logins = GetLoginsInASpecificGroup(r[0] + "");
                    // List<string> RLogins = RestrictLoginsListToHierarchyType(Logins, "SUPER");
                    resL.Add(r[0] + "");
                }
                MemberDetailsShort mem = GetCompleteMemberDetails(SSN, "Internal Messaging", "");

                string Message = "An Encounter Note was added by " + Login + "\nFor Consumer " + mem.FirstName + " " + mem.LastName + "\n" +
                    "And needs supervision review.";
                //  resL.Add(Login + ""); //for now we will not be emailing the author and the supervisor of that person
                SendMessageTo(resL, Message, Login);

                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("PRIVATE: SendMessageToThisMembersProgramsSupervisors", ex.Message);
            }

            return res;
        }

        public bool SendMessageTo(List<String> Unames, string MessageBody, string Sender)
        {
            bool result = true;

            try
            {
                foreach (string un in Unames)
                {
                    SendMessage(un, Sender, MessageBody, "03");
                }
            }
            catch (Exception ex)
            {
                LogError("SendMessageTo", ex.Message);

                result = false;
            }

            return result;
        }

        private void SendMessage(string uname, string sender, string body, string type)
        {
            try
            {
                string sql = "INSERT INTO TBLUSERMESSAGES (SOURCE,DESTINATION,DATECREATED,MESSAGETYPE,READSTATUS,BODY) VALUES (" +
                    "@SENDER,@UNAME,@DT,@MTYPE,'N',@BODY)";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 500;
                cmd.Parameters.Add("@SENDER", SqlDbType.VarChar).Value = sender;
                cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = uname;
                cmd.Parameters.Add("@DT", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@MTYPE", SqlDbType.VarChar).Value = type;
                cmd.Parameters.Add("@BODY", SqlDbType.VarChar).Value = body;

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("SendMessage", ex.Message);
            }
        }

        [Route("api/Login/GetActiveServicesForMember")]
        [HttpGet]
        public JsonResult<List<LookupServices>> GetListOfServiceDescriptionsForMember()
        {

            List<LookupServices> result = new List<LookupServices>();

            string memb = "";
            string encd = "01-01-1980";

            try
            {
                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("MEMB"))
                {
                    memb = rhead.GetValues("MEMB").First();
                }

                if (rhead.Contains("ENCD"))
                {
                    encd = rhead.GetValues("ENCD").First();
                }

                DateTime encDate = Convert.ToDateTime(encd);

                string sql = "SELECT [SvcID],A.[Funder],B.DESCRIPTION as 'FUNDERNAME',A.[CostCenter],[SvcCode], ";
                sql += "[SvcDescription],[UnitType],c.DESCRIPTON  as 'UNITTYPEDESC',[CostPerUnit],A.[ACTIVE], ";
                sql += "[AUTHREQ],[COPAY],[Modifier1],[Modifier2],[Modifier3],[Modifier4],[AUTOUNIT],[ROUNDRULE],[RelatedSplitCode],[BCBANOTEREQUIRED] ";
                sql += "FROM [dbo].[tblLOOKUPSERVICES] A ";
                sql += "LEFT OUTER JOIN tblLOOKUPFUNDERS B ON A.Funder = b.CODE ";
                sql += "LEFT OUTER JOIN tblLOOKUPUNITS C on A.UnitType = C.CODE ";
                sql += "LEFT OUTER JOIN tblMemberAuthorizedServices AUS ON AUS.COSTCENTER = CAST(A.SvcID AS VARCHAR) ";
                sql += "WHERE A.ACTIVE = 'Y' ";
                sql += " AND B.DESCRIPTION IS NOT NULL ";
                sql += " AND AUS.SSN = @SSN ";
                sql += " AND (CAST(@ENCDATE AS DATE) BETWEEN CAST(AUS.STARTDATE AS DATE) AND CAST(AUS.ENDDATE AS DATE)) ";
                sql += "ORDER BY [SVCDESCRIPTION] ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.Add("@SSN", SqlDbType.VarChar).Value = memb;
                cmd.Parameters.Add("@ENCDATE", SqlDbType.DateTime).Value = encDate;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    LookupServices i = new LookupServices();

                    i.svcID = r.GetInt64(0);
                    i.Funder = r["FUNDER"] + "";
                    i.CostCenter = r["COSTCENTER"] + "";
                    i.SvcCode = r["SVCCODE"] + "";
                    i.SvcDescription = r["SVCDESCRIPTION"] + "";

                    // Added to duplicate the Stupid Business logic that turns on the BANOTE button in the web version
                    // The UI on the phone will real the boolean to enable or disable that UI element

                    if (i.SvcDescription.ToUpper().Contains("BEHAVIORAL ASSISTANCE"))
                        i.BANote = true;
                    else
                        i.BANote = false;

                    i.UnitType = r["UNITTYPE"] + "";
                    if (!r.IsDBNull(8))
                    {
                        string v = r[8] + "";

                        double d = 0.0;

                        if (double.TryParse(v, out d))
                        {
                            i.CostPerUnit = d;
                        }
                        else
                        {
                            i.CostPerUnit = 0;
                        }
                    }

                    i.ACTIVE = r["ACTIVE"] + "";
                    i.AUTHREQ = r["AUTHREQ"] + "";
                    i.RelatedSplitCode = r["RelatedSplitCode"] + "";

                    if (r["BCBANOTEREQUIRED"] != System.DBNull.Value)
                    {
                        i.BCBANoteRequired = Convert.ToBoolean(r["BCBANOTEREQUIRED"]);
                    }

                    result.Add(i);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfServiceDescriptionsForMember", ex.Message);
            }

            return Json(result);
        }

        [Route("api/Login/GetActiveServicesForAuth")]
        [HttpGet]
        public JsonResult<List<LookupServices>> GetListOfServiceDescriptionsForThisAuth()
        {
            List<LookupServices> result = new List<LookupServices>();

            string AuthID = "0";

            try
            {
                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("AUTHID"))
                {
                    AuthID = rhead.GetValues("AUTHID").First();
                }

                long authID = Convert.ToInt64(AuthID);

                string sql = "SELECT [SvcID],A.[Funder],B.DESCRIPTION as 'FUNDERNAME',A.[CostCenter],[SvcCode], ";
                sql += "[SvcDescription],[UnitType],c.DESCRIPTON  as 'UNITTYPEDESC',[CostPerUnit],A.[ACTIVE], ";
                sql += "[AUTHREQ],[COPAY],[Modifier1],[Modifier2],[Modifier3],[Modifier4],[AUTOUNIT],[ROUNDRULE],[RelatedSplitCode],[BCBANOTEREQUIRED] ";
                sql += "FROM [dbo].[tblLOOKUPSERVICES] A ";
                sql += "LEFT OUTER JOIN tblLOOKUPFUNDERS B ON A.Funder = b.CODE ";
                sql += "LEFT OUTER JOIN tblLOOKUPUNITS C on A.UnitType = C.CODE ";
                sql += "LEFT OUTER JOIN tblMemberAuthorizedServices AUS ON ltrim(rtrim(AUS.COSTCENTER)) = CAST(A.SvcID AS VARCHAR) ";
                sql += "WHERE A.ACTIVE = 'Y' ";
                sql += " AND B.DESCRIPTION IS NOT NULL ";
                sql += " AND AUS.MAUTHID = @MAUTHID ";
                sql += "ORDER BY [SVCDESCRIPTION] ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.Add("@MAUTHID", SqlDbType.BigInt).Value = authID;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    LookupServices i = new LookupServices();

                    i.svcID = r.GetInt64(0);
                    i.Funder = r["FUNDER"] + "";
                    i.CostCenter = r["COSTCENTER"] + "";
                    i.SvcCode = r["SVCCODE"] + "";
                    i.SvcDescription = r["SVCDESCRIPTION"] + "";

                    // Added to duplicate the Stupid Business logic that turns on the BANOTE button in the web version
                    // The UI on the phone will real the boolean to enable or disable that UI element

                    if (i.SvcDescription.ToUpper().Contains("BEHAVIORAL ASSISTANCE"))
                        i.BANote = true;
                    else
                        i.BANote = false;

                    i.UnitType = r["UNITTYPE"] + "";
                    if (!r.IsDBNull(8))
                    {
                        string v = r[8] + "";

                        double d = 0.0;

                        if (double.TryParse(v, out d))
                        {
                            i.CostPerUnit = d;
                        }
                        else
                        {
                            i.CostPerUnit = 0;
                        }
                    }

                    i.ACTIVE = r["ACTIVE"] + "";
                    i.AUTHREQ = r["AUTHREQ"] + "";
                    i.RelatedSplitCode = r["RelatedSplitCode"] + "";

                    if (r["BCBANOTEREQUIRED"] != System.DBNull.Value)
                    {
                        i.BCBANoteRequired = Convert.ToBoolean(r["BCBANOTEREQUIRED"]);
                    }

                    result.Add(i);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfServiceDescriptionsForMember", ex.Message);
            }

            return Json(result);
        }

        [Route("api/Login/GetActiveServicesForThisAuth")]
        [HttpGet]
        public JsonResult<List<LookupServices>> GetListOfServiceDescriptionsForThisAuthII()
        {
            List<LookupServices> result = new List<LookupServices>();

            string authNumber = "";
            
            try
            {

                var req = Request;

                var rhead = req.Headers;

                if (rhead.Contains("AUTHNUMBER"))
                {
                    authNumber = rhead.GetValues("AUTHNUMBER").First();
                }


                string sql = "SELECT [SvcID],A.[Funder],B.DESCRIPTION as 'FUNDERNAME',A.[CostCenter],[SvcCode], ";
                sql += "[SvcDescription],[UnitType],c.DESCRIPTON  as 'UNITTYPEDESC',[CostPerUnit],A.[ACTIVE], ";
                sql += "[AUTHREQ],[COPAY],[Modifier1],[Modifier2],[Modifier3],[Modifier4],[AUTOUNIT],[ROUNDRULE],[RelatedSplitCode],[BCBANOTEREQUIRED] ";
                sql += "FROM [dbo].[tblLOOKUPSERVICES] A ";
                sql += "LEFT OUTER JOIN tblLOOKUPFUNDERS B ON A.Funder = b.CODE ";
                sql += "LEFT OUTER JOIN tblLOOKUPUNITS C on A.UnitType = C.CODE ";
                sql += "LEFT OUTER JOIN tblMemberAuthorizedServices AUS ON ltrim(rtrim(AUS.COSTCENTER)) = CAST(A.SvcID AS VARCHAR) ";
                sql += "WHERE A.ACTIVE = 'Y' ";
                sql += " AND B.DESCRIPTION IS NOT NULL ";
                sql += " AND AUS.AUTHNUMBER = @AUTHNUMBER ";
                sql += "ORDER BY [SVCDESCRIPTION] ";

                SqlConnection cn = new SqlConnection(DBCON());
                cn.Open();

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.Add("@AUTHNUMBER", SqlDbType.VarChar).Value = authNumber;

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    LookupServices i = new LookupServices();

                    i.svcID = r.GetInt64(0);
                    i.Funder = r["FUNDER"] + "";
                    i.CostCenter = r["COSTCENTER"] + "";
                    i.SvcCode = r["SVCCODE"] + "";
                    i.SvcDescription = r["SVCDESCRIPTION"] + "";

                    // Added to duplicate the Stupid Business logic that turns on the BANOTE button in the web version
                    // The UI on the phone will real the boolean to enable or disable that UI element

                    if (i.SvcDescription.ToUpper().Contains("BEHAVIORAL ASSISTANCE"))
                        i.BANote = true;
                    else
                        i.BANote = false;

                    i.UnitType = r["UNITTYPE"] + "";
                    if (!r.IsDBNull(8))
                    {
                        string v = r[8] + "";

                        double d = 0.0;

                        if (double.TryParse(v, out d))
                        {
                            i.CostPerUnit = d;
                        }
                        else
                        {
                            i.CostPerUnit = 0;
                        }
                    }

                    i.ACTIVE = r["ACTIVE"] + "";
                    i.AUTHREQ = r["AUTHREQ"] + "";
                    i.RelatedSplitCode = r["RelatedSplitCode"] + "";

                    if (r["BCBANOTEREQUIRED"] != System.DBNull.Value)
                    {
                        i.BCBANoteRequired = Convert.ToBoolean(r["BCBANOTEREQUIRED"]);
                    }

                    result.Add(i);
                }
                r.Close();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                LogError("GetListOfServiceDescriptionsForMember", ex.Message);
            }

            return Json(result);
        }


    }

    #region Extra Classes


    public class CodedDescriptor
    {
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWList
    {
        public string code = "";
        public string description = "";
        public List<string> theList = new List<string>();
    }

    public class CodedDescriptorExt
    {
        public string authreq = "";
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWithActive
    {
        public string active = "";
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWField
    {
        public string code = "";
        public string description = "";
        public string field = "";
    }

    public class CodedDescriptorWPrograms
    {
        public string active = "";
        public string code = "";
        public string description = "";
        public string program = "";
    }

    public class CodedDescriptorWID
    {
        public long id = 0;
        public string code = "";
        public string description = "";
    }

    public class CodedDescriptorWithOther
    {
        public long id = 0;
        public string code = "";
        public string description = "";
        public string other = "";
    }

    public class LoginResult
    {
        public bool Success = false;
        public string UserName = "";
        public string FirstName = "";
        public string LastName = "";
        public string Address1 = "";
        public string Address2 = "";
        public string City = "";
        public string State = "";
        public string ZipCode = "";
    }
    
    public class UserLogins
    {
        public string ContactNum = "";
        public string CreatedBy = "";
        public DateTime CreatedDate;
        public string Deactive = "";
        public string Email = "";
        public string FirstName = "";
        public string GlobalOrganization = "";
        public List<CodedDescriptor> Groups;
        public List<CodedDescriptor> Hierarchy;
        public string IpAddress = "";
        public bool IsAuthRO = false;
        public bool IsGroupAccessGlobal = false;
        public bool IsMemberDemoRO = false;
        public bool IsOrgAReferralTarget = false;
        public bool IsProgressNotesRO = false;
        public string LastName = "";
        public string Note = "";
        public string Organization = "";
        public string Password = "";
        public DateTime PasswordChangeDate;
        public List<CodedDescriptor> Regions;
        public List<CodedDescriptor> Roles;
        public List<CodedDescriptor> CostCenters;
        public List<CodedDescriptorWID> ServiceIDS;
        public string UpdatedBy = "";
        public DateTime UpdatedDate;
        public int UserID = 0;
        public string Username = "";
        public string Address1 = "";
        public string Address2 = "";
        public string City = "";
        public string State = "";
        public string ZipCode = "";
        public DateTime LastGlobalMesssageDate = Convert.ToDateTime(null);
        public DateTime LastWhatsNewDate = Convert.ToDateTime(null);
        public string County = "";
        public string Email2 = "";
        public string Gender = "";
        public string GenderDescription = "";
        public string Religion = "";
        public string ReligionDescription = "";
        public string Race = "";
        public string RaceDescription = "";
        public List<CodedDescriptor> Languages;
        public string LanguageDescription = "";
        public string ServiceDescription = "";
        public string CountyDescription = "";
        public List<CodedDescriptor> Competencies;
        public string CompetencyDescription = "";

        public string CP1 = "";
        public string CP2 = "";
        public string CP3 = "";
        public string CP4 = "";
        public string CP5 = "";
        public string CP6 = "";
        public string CP7 = "";
        public string CP8 = "";
        public string CP9 = "";
        public string CP10 = "";


        public string CredentialType1 = "";
        public string CredentialType1Description = "";
        public string CredentialType2 = "";
        public string CredentialType2Description = "";
        public string ClinicalLicenseNumber = "";
        public string BCBANumber = "";
        public string IndividualNPINumber = "";
        public bool PhysicalAbuse = false;
        public bool SexualAbuse = false;
        public bool ADHD = false;
        public bool AdoptionFoster = false;
        public bool AngerManagement = false;
        public bool AppliedBehavior = false;
        public bool ArtTherapy = false;
        public bool Autism = false;
        public bool BehaviorModification = false;
        public bool CognitiveBehavior = false;
        public bool DevDisabled = false;
        public bool DomesticViolence = false;
        public bool EatingDisorder = false;
        public bool EMDR = false;
        public bool EvidenceBased = false;
        public bool FaithChrisitian = false;
        public bool FaithJewish = false;
        public bool FaithOther = false;
        public bool FamilyTherapy = false;
        public bool GangInvolvement = false;
        public bool JuvenileJustice = false;
        public bool LearningDisability = false;
        public bool LGBTIssues = false;
        public bool ParentingSkills = false;
        public bool PlayTherapy = false;
        public bool PTSD = false;
        public bool SelfMutilatition = false;
        public bool SexOffenders = false;
        public bool SexualBoundaryIssues = false;
        public bool SocialSkillsTraining = false;
        public bool SubstanceAbuse = false;
        public bool TraumaIssues = false;
        public bool VocationalSkillsTraining = false;
        public bool AcceptTexts = false;
        public double LATITUDE = 0.0;
        public double LONGITUDE = 0.0;
        public string MAPGROUP = "";
        public string TOOLTIP = "";
        public string GEO = "";
        public string OON = "";
        public string MEMBERID = ""; // for member/consumer logins


        //public IHtmlString ToJson = JsonHelpers.ToJson<UserLogins>((object) this);
    }

    public class MemberDetailsShort
    {
        public string CaseManager = "";
        public string CreatedBy = "";
        public DateTime CreatedDate = Convert.ToDateTime(null);
        public string CurrentCaseManager = "";
        public string DOB;
        public string Email = "";
        public string Ethnicity = "";
        public string FirstName = "";
        public string Gender = "";
        public string LastActiveStatus = "";
        public string LastName = "";
        public MemberAddress memberAddress;
        public List<MemberAddress> memberAddressHistory;
        //public List<ihsisMemberAdmissionEntry> MemberAdmissions;
        //public List<ihsisMemberAlert> MemberAlerts;
        //public List<AuthorizedService> memberAuths;
        //public List<objtblMemberCommunitySupports> MemberCommunitySupports;
        //public List<ihsisMEMBERCONTACTINFO> MemberContactList;
        //public List<IhsisMemberContactEntry> MemberContacts;
        //public List<ihsisMemberDMEProvider> MemberDMEProviders;
        //public List<ihsisMemberEligibilityEntry> MemberEligibilityList;
        //public List<MemberOtherSystemIDs> MemberOtherSystemIDs;
        //public List<ihsisGroupInformation> MemberGroups;
        //public List<ihsisImmunizationScreen> MemberImmunizations;
        //public List<ihsisMemberLabs> MemberLabs;
        //public List<IhsisMemberLanguageEntry> MemberLanguages;
        //public List<ihsisMemberMeds> MemberMeds;
        //public List<ihsisOtherSystemIDs> MemberOtherSystems;
        //public List<ihsisMemberProgramMembershipEntry> MemberPrograms;
        //public List<ProviderDesignation> memberProviders;
        //public List<ihsisROI> MemberROIs;
        //public List<ihsisBPRSGAFBundle> MemberScores;
        //public List<ihsisMemberProvider> MemberServiceProviders;
        //public List<MemberServices> MemberServices;
        //public List<MemberRelationship> MemberSupports;
        //public List<MemberTherapies> MemberTherapies;
        //public List<ihsisMEMBERWATCHLISTENTRY> MemberWatchList;
        public string MiddleName = "";
        public Int64 MMID = 0;
        public string ParentGuardian = "";
        public string ParentGuardPhone = "";
        public string Phone1 = "";
        public string Phone1Ext = "";
        public string Phone1Type = "";
        public string Phone2 = "";
        public string Phone2Ext = "";
        public string Phone2Type = "";
        public string Race = "";
        public string SSN = "";
        public string UpdatedBy = "";
        public DateTime UpdatedDate = Convert.ToDateTime(null);
        public DateTime LocationDate = Convert.ToDateTime(null);
        public string MEMBERSTATUS = "";
        public long MEMBERADDRESSHISTORYID = 0;
        public long MEMBERADDRESSID = 0;
        
    }

    public class MemberAddress
    {
        public string Address1 = "";
        public string Address2 = "";
        public string Address3 = "";
        public string AddressType = "";
        public string ApartmentSuite = "";
        public string City = "";
        public string County = "";
        public string CreateBy = "";
        public DateTime CreateDate;
        public Int64 MAID = 0;
        public string SSN = "";
        public string State = "";
        public DateTime UpdateDate;
        public string UpdatedBy = "";
        public string ZipCode = "";
    }

    public class AuthorizedService
    {
        public string ACCEPTED = "";
        public string COSTCENTER = "";
        public string COSTCENTERDESC = "";
        //public DateTime CREATEDATE = Convert.ToDateTime(null);
        public string CREATEUSER = "";

        public double DOLLARS = 0.0;
        public DateTime ENDDATE = Convert.ToDateTime(null);
        public string FUNDER = "";
        public string FUNDERID = "";
        public long mauthID = 0;
        public string PROVIDERID = "";
        public string PROVIDERNAME = "";
        public int REMAININGUNITS = 0;
        public string SSN = "";
        public DateTime STARTDATE = Convert.ToDateTime(null);
        public int UNITS = 0;
        public string AUTHNUMBER = "";
        public string CONNECTEDNPI = "";
        public int HPW = 0;
        public string CASEMANAGER = "";
        public string LINKEDAUTHNUMBER = "";
        public int UNITTYPE = 0;
        public string RelatedSplitCode = "";
        public string ServiceCode = "";
        public int ROUNDLIMIT = 0;
        public bool ISLINKED = false;
        public string OLDAUTHNUMBER = "";
        public string OLDSERVICE = "";
        public bool ISSPLIT = false;
        public string VALID = "";
        public string strSTARTDATE = "";
        public string strENDDATE = "";
        public double DSPRATE = 0.0;
    }

    public class MemberObservers
    {
        public string AUTHOR = "";
        public DateTime CREATEDATE = Convert.ToDateTime(null);
        public DateTime EDATE = Convert.ToDateTime(null);
        public long ID = -1;
        public string OBSERVER = "";
        public string OBSTYPE = "";
        public DateTime SDATE = Convert.ToDateTime(null);
        public string SSN = "";
        public string PHONE = "";
        public string EMAIL = "";
        public string CREDENTIALONE = "";
        public string CREDENTIALTWO = "";
        public string USERNAME = "";
        public string strSDATE = "";
        public string strEDATE = "";
    }

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

    public class MemberProgressNotes
    {
        public string AUTHOR = "";
        public DateTime CONTACTDATE = DateTime.MinValue;
        public string strCONTACTDATE = "";
        public int CONTACTMINUTES = 0;
        public DateTime CREATEDATE = DateTime.MinValue;
        public string MEMBERNAME = "";
        public long mpnID = -1;
        public string NOTATION = "";
        public string NOTATIONSHORT = "";
        public string NOTECONTACTDESC = "";
        public string NOTETYPEDESC = "";
        public DateTime SAFETYASSESSMENT = DateTime.MinValue;
        public string SAFETYASSESSMENTLVL = "";
        public string SAFETYASSESSMENTLVLDESC = "";
        public string SIGNED = "";
        public DateTime SIGNEDDATE = DateTime.MinValue;
        public string SSN = "";
        public string SUPERAPPROV = "";
        public DateTime SUPERAPPROVEDATE = DateTime.MinValue;
        public string SUPERVISORACK1 = "";
        public string SUPERVISORACK2 = "";
        public string SUPERVISORNOTATION = "";
        public DateTime SUPERVISORNOTATIONDATE = DateTime.MinValue;
        public int TRAVELMINUTES = 0;
        public string SERVICECODE = "";
        public string SERVICEDESCRIPTION = "";
        public string NEWSSN = "";
        public int ENCOUNTERID = 0;
        public string CONTACTTYPEDESCRIPTION = "";
        public string strCREATEDATE = "";
    }

    public class ServiceDescription
    {
        public string ACTIVE = "";
        public string AUTHREQ = "";
        public string COSTCENTER = "";
        public double COSTPERUNIT = 0.0;
        public string FUNDER = "";
        public string FUNDERDESCRIPTION = "";
        public long ID = -1;
        public string SVCCODE = "";
        public string SVCDESCRIPTION = "";
        public string UNITTYPE = "";
        public string UNITTYPEDESCRIPTION = "";
        public int AUTOUNIT = -1;
        public int ROUNDRULE = -1;
        public string MOD1 = "";
        public string MOD2 = "";
        public string MOD3 = "";
        public string MOD4 = "";
        public string COPAY = "";
        public string RELATEDSPLITCODE = "";
        public bool BCBANOTE = false;
    }
        
    public class TheLookups
    {
        public List<CodedDescriptor> LOOKUP_V_DETOX;
        public List<CodedDescriptor> LOOKUP_V_EMERSTAB;
        public List<CodedDescriptor> LOOKUP_V_INCIDENTAL;
        public List<CodedDescriptor> LOOKUP_V_PREVENTION;
        public List<CodedDescriptor> LOOKUP_V_RECOVERY;
        public List<CodedDescriptor> LOOKUP_V_RECOVERYCCST;
        public List<CodedDescriptor> LOOKUP_V_RECOVERYFACT;
        public List<CodedDescriptor> LOOKUP_V_RECOVERYINCIDENTAL;
        public List<CodedDescriptor> LOOKUP_V_TREATMENTANDAFTERCARE;
        public List<ProviderDesignation> LOOKUP211REFERRALPROVIDERS;
        public List<CodedDescriptor> LOOKUPADDRESSTYPE;
        public List<CodedDescriptor> LOOKUPADMISSIONTYPE;
        public List<CodedDescriptor> LOOKUPALERTTYPE;
        public List<CodedDescriptor> LOOKUPASSEMENTKINDS;
        public List<CodedDescriptor> LOOKUPASSMTTYPE;
        public List<CodedDescriptor> LOOKUPCOMMERCIALINSURANCE;
        public List<CodedDescriptor> LOOKUPCOMMUNITYSUPPORTSTYPE;
        public List<CodedDescriptor> LOOKUPCONCURRENT211REFERRALTARGETS;
        public List<CodedDescriptor> LOOKUPCONTACTTYPE;
        public List<CodedDescriptorExt> LOOKUPUNITTYPE;
        public List<CodedDescriptorExt> LOOKUPCOSTCENTER;
        public List<CodedDescriptor> LOOKUPCOUNTYNAME;
        public List<CodedDescriptor> LOOKUPDISCHARGEREASON;
        public List<CodedDescriptor> LOOKUPDISCHARGETYPE;
        public List<CodedDescriptor> LOOKUPDISPOSITIONS;
        public List<CodedDescriptor> LOOKUPDOCUMENTTYPES;
        public List<CodedDescriptor> LOOKUPDRUGOFCHOICE;
        public List<CodedDescriptor> LOOKUPEDUCATION;
        public List<CodedDescriptor> LOOKUPEMPLOYMENT;
        public List<CodedDescriptor> LOOKUPENCOUNTERRELATION;
        public List<CodedDescriptor> LOOKUPENCOUNTERSDS;
        public List<CodedDescriptor> LOOKUPENCOUNTERSTATUS;
        public List<CodedDescriptor> LOOKUPENCOUNTERSTATUSNOBILLED;
        public List<CodedDescriptor> LOOKUPENCOUNTERSTATUSALL;
        public List<CodedDescriptor> LOOKUPETHNICITY;
        public List<CodedDescriptorWPrograms> LOOKUPFACILITY;
        public List<CodedDescriptor> LOOKUPFACILITYTYPE;
        public List<CodedDescriptor> LOOKUPFOLLOWUPPROVTYPE;
        public List<CodedDescriptor> LOOKUPFOLLOWUPREASONS;
        public List<CodedDescriptor> LOOKUPFREQ;
        public List<CodedDescriptor> LOOKUPFUNDERS;
        public List<CodedDescriptor> LOOKUPFUNDERSFLEXFUND;
        public List<CodedDescriptor> LOOKUPFUNDERSGROUPHEALTH;
        public List<CodedDescriptor> LOOKUPFUNDING;
        public List<CodedDescriptor> LOOKUPGENDER;
        public List<GMRRegions> LOOKUPGMRegions;
        public List<CodedDescriptor> LOOKUPGROUPINFORMATION;
        public List<CodedDescriptor> LOOKUPHEALTHSTATUS;
        public List<CodedDescriptor> LOOKUPHIERARCHY;
        public List<CodedDescriptor> LOOKUPINCIDENTALEXPENSECATEGORIES;
        public List<CodedDescriptor> LOOKUPINCOMESOURCE;
        public List<CodedDescriptor> LOOKUPLANGUAGES;
        public List<CodedDescriptor> LOOKUPLEGALSTATUS;
        public List<CodedDescriptor> LOOKUPLIVINGSIT;
        public List<CodedDescriptor> LOOKUPLOOKUPTABLES;
        public List<CodedDescriptor> LOOKUPMARCHMANACT;
        public List<CodedDescriptor> LOOKUPMARITALSTATUS;
        public List<CodedDescriptor> LOOKUPMEDCARECOVERAGEGROUPS;
        public List<CodedDescriptor> LOOKUPMEDICAIDTYPE;
        public List<NumericDescriptor> LOOKUPMEDS;
        public List<CodedDescriptor> LOOKUPMEMBERWATCHLIST;
        public List<CodedDescriptor> LOOKUPMESSAGETYPE;
        public List<CodedDescriptor> LOOKUPMHPROBLEM;
        public List<CodedDescriptor> LOOKUPNOTECONTACTTYPE;
        public List<CodedDescriptor> LOOKUPOPFUTYPE;
        public List<CodedDescriptor> LOOKUPOTHERSYSTEMS;
        public List<CodedDescriptor> LOOKUPPARTICIPANTTYPE;
        public List<CodedDescriptor> LOOKUPPHONETYPES;
        public List<CodedDescriptor> LOOKUPPREGTRIMESTER;
        public List<CodedDescriptor> LOOKUPPROGRAM;
        public List<CodedDescriptor> LOOKUPPROGRAMDISCHARGEREASONS;
        public List<CodedDescriptor> LOOKUPPROGRAMEVALPURPOSE;
        public List<CodedDescriptor> LOOKUPPROGRAMMEMBERSHIP;
        public List<CodedDescriptor> LOOKUPPROGRAMTYPE;
        public List<CodedDescriptor> LOOKUPPROGRESSNOTETYPES;

        public List<ProviderDesignation> LOOKUPPROVIDERS;
        public List<ProviderDesignation> LOOKUPPROVIDERSFORTRACKINGELEMENTS;
        public List<CodedDescriptor> LOOKUPPROVIDERSPECIALTY;
        public List<CodedDescriptor> LOOKUPPROVIDERTYPE;

        public List<CodedDescriptor> LOOKUPPURPOSEASAM;
        public List<CodedDescriptor> LOOKUPPURPOSEOFEVAL;
        public List<CodedDescriptor> LOOKUPPURPOSEOFEVALDCF;
        public List<CodedDescriptor> LOOKUPRACE;
        public List<CodedDescriptor> LOOKUPRECLEVELOFCARE;
        public List<CodedDescriptor> LOOKUPREFERRALREASON;
        public List<CodedDescriptor> LOOKUPREFERRALSOURCE;
        public List<CodedDescriptorWithOther> LOOKUPREFSOURCEBLUEBOOK;
        public List<CodedDescriptor> LOOKUPREMCONTACTWITH;
        public List<CodedDescriptor> LOOKUPREMNOTETYPE;
        public List<CodedDescriptor> LOOKUPRESIDENTSTATUS;

        //public List<CodedDescriptor> LOOKUPROI;
        public List<CodedDescriptorWithActive> LOOKUPROI;

        public List<CodedDescriptor> LOOKUPROLES;
        public List<CodedDescriptor> LOOKUPSAFETYASSESSMENTLVL;
        public List<CodedDescriptor> LOOKUPSCALE;
        public List<CodedDescriptor> LOOKUPSCREENINGPROGRAM;
        public List<ServiceGapDescriptor> LOOKUPSERVICEGAPS;
        public List<CodedDescriptor> LOOKUPSERVICEGAPVERIFICATIONMETHODS;
        public List<ServiceDescription> LOOKUPSERVICES;
        public List<ServiceDescription> LOOKUPSERVICESNOID;
        public List<ServiceDescription> LOOKUPSERVICESNOIDNOSPLIT;
        public List<CodedDescriptor> LOOKUPSETARGETTYPE;
        public List<CodedDescriptor> LOOKUPSETYPE;
        public List<CodedDescriptor> LOOKUPSUPPORTSRELATIONSHIP;
        public List<CodedDescriptor> LOOKUPSUPPORTSRELATIONSHIPTEAM;
        public List<CodedDescriptor> LOOKUPSUPPORTSRELATIONSHIPFAMILY;
        public List<CodedDescriptor> LOOKUPTANFSTATUS;
        public List<CodedDescriptor> LOOKUPTHERAPIES;
        public List<CodedDescriptor> LOOKUPTREATMENTPLAN_NEEDPROGRESS;
        public List<ZipCodeDescriptor> LOOKUPZIPS;

        public List<CodedDescriptorWID> LOOKUPSUPERVISORS;
        public List<CodedDescriptorWID> LOOKUPUSERSWITHID;
        public List<CodedDescriptor> LOOKUPMEMBERAUTHS;
        public List<CodedDescriptor> LOOKUPCOUNTIES;
        public List<CodedDescriptor> LOOKUPPAYER;
        public List<CodedDescriptor> LOOKUPDSP;
        public List<CodedDescriptor> LOOKUPCASESTATUS;
        public List<CodedDescriptor> LOOKUPCASESETTING;
        public List<CodedDescriptor> LOOKUPTASKS;
        public List<CodedDescriptor> LOOKUPTASKSTEPS;
        public List<CodedDescriptor> LOOKUPTASKSTATUS;
        public List<CodedDescriptor> LOOKUPFUNDERSFORENCOUNTERS;
        public List<CodedDescriptor> LOOKUPDISTINCTSERVICES;
        public List<CodedDescriptorWID> LOOKUPDISTINCTSERVICESWITHID;
        public List<CodedDescriptor> LOOKUPRELIGION;
        public List<CodedDescriptor> LOOKUPCOMPETENCIES;
        public List<CodedDescriptor> LOOKUPCREDENTIALS;
        public List<CodedDescriptor> LOOKUPACTIVEMEMBERS;
        public List<CodedDescriptor> LOOKUPWAITLISTREASON;
        // Added for HealthIntech

        public List<CodedDescriptor> LOOKUPSLEEPPROBLEM;
        public List<CodedDescriptor> LOOKUPINSIGHTDEGREE;
        public List<CodedDescriptor> LOOKUPALCOHOLCONSUMED30LIST;
        public List<CodedDescriptor> LOOKUPINTOXICSUBSTANCETIME;
        public List<CodedDescriptor> LOOKUPINJECTIONDRUGUSE;
        public List<CodedDescriptor> LOOKUPWITHDRAWALSYMPTOMS;
        public List<CodedDescriptor> LOOKUPINJURIOUSATTEMPT;
        public List<CodedDescriptor> LOOKUPSELFINJURIOUSTOKILL;
        public List<CodedDescriptor> LOOKUPVIOLENCE;
        public List<CodedDescriptor> LOOKUPBEHAVIOURDISTURBANCE;
        public List<CodedDescriptor> LOOKUPPOLICEINTERVENTION;
        public List<CodedDescriptor> LOOKUPDISORDEREDTHINKING;
        public List<CodedDescriptor> LOOKUPDECISIONMAKING90;
        public List<CodedDescriptor> LOOKUPIADLCAPACITY;
        public List<CodedDescriptor> LOOKUPADLSTATUS90;
        public List<CodedDescriptor> LOOKUPLIFEEVENTS;
        public List<CodedDescriptor> LOOKUPINTENSEFEAREVENTS;
        public List<CodedDescriptor> LOOKUPSELFUNDERSTOOD;
        public List<CodedDescriptor> LOOKUPUNDERSTANDOTHERS;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICCONTACT;
        public List<CodedDescriptor> LOOKUPDISCHARGEFROMPSYCHIATRIC;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICDURATION;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICADMISSION2YEAR;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICADMISSIONLIFETIME;
        public List<CodedDescriptor> LOOKUPFIRSTPSYCHIATRICSTAYAGE;
        public List<CodedDescriptor> LOOKUPBEHAVIORSYMPTOMS;
        public List<CodedDescriptor> LOOKUPTREATMENTMODALITIES;
        public List<CodedDescriptor> LOOKUPINTERVENTIONFOCUS;
        public List<CodedDescriptor> LOOKUPELECTROCONVULSIVETHERAPY;
        public List<CodedDescriptor> LOOKUPCONTROLINTERVENTION;
        public List<CodedDescriptor> LOOKUPAUTHORIZEDACTIVITIES;
        public List<CodedDescriptor> LOOKUPDISTURBEDRELATIONSHIP;
        public List<CodedDescriptor> LOOKUPSOCIALLIST;
        public List<CodedDescriptor> LOOKUPEMPLOYMENTSTATUS;
        public List<CodedDescriptor> LOOKUPEMPLOYMENTARRANGEMENT;
        public List<CodedDescriptor> LOOKUPFORMALEDUCATION;
        public List<CodedDescriptor> LOOKUPUNEMPLOYMENTRISK;
        public List<CodedDescriptor> LOOKUPSOCIALSUPPORT;
        public List<CodedDescriptor> LOOKUPEXPECTATIONOFSTAY;
        public List<CodedDescriptor> LOOKUPPSYCHIATRICSYMPTOMS;
        public List<CodedDescriptor> LOOKUPPROVISIONALDSMCATEGORY;
        public List<CodedDescriptor> LOOKUPMEDICALDIAGNOSES;
        public List<CodedDescriptor> LOOKUPDISCHARGEASSESSMENT;
        public List<CodedDescriptor> LOOKUPTYPEOFADMISSION;
        public List<CodedDescriptor> LOOKUPCONTACTPREFERENCES;
        public List<CodedDescriptor> LOOKUPMARITIALSTATUS;
        public List<CodedDescriptor> LOOKUPLIVINGSTATUS;
        public List<CodedDescriptor> LOOKUPLIVINGARRANGEMENT;
        public List<CodedDescriptor> LOOKUPHOSPITALSTAY;
        public List<CodedDescriptor> LOOKUPCOGNITIVESKILLSLIST;
        public List<CodedDescriptor> LOOKUPBEHAVIOURPRESENT;
        public List<CodedDescriptor> LOOKUPASSESSMENTREASON;
        public List<CodedDescriptor> LOOKUPPATIENTSTATUSLIST;
        public List<CodedDescriptor> LOOKUPUNDERSTOODLIST;
        public List<CodedDescriptor> LOOKUPPRIMARYLANGUAGELIST;
        public List<CodedDescriptor> LOOKUPRESIDENCENEEDSTOCHANGELIST;
        public List<CodedDescriptor> LOOKUPVISIONDIFFICULTYLIST;
        public List<CodedDescriptor> LOOKUPHEARINGDIFFICULTYLIST;
        public List<CodedDescriptor> LOOKUPFARTHESTWALKEDDISTANCELIST;
        public List<CodedDescriptor> LOOKUPADLSELFPERFORMANCELIST;
        public List<CodedDescriptor> LOOKUPCHANGEIN90LIST;
        public List<CodedDescriptor> LOOKUPBEHAVIOURLIST;
        public List<CodedDescriptor> LOOKUPINDICATORLIST;

        public List<CodedDescriptor> DECLINELIST;
        public List<CodedDescriptor> LOOKUPALONELIST;
        public List<CodedDescriptor> LOOKUPSELFPERFORMANCELIST;
        public List<CodedDescriptor> LOOKUPLOCOMOTIONWALKINGLIST;
        public List<CodedDescriptor> LOOKUPLOCOMOTIONWALKINGDISTANCELIST;
        public List<CodedDescriptor> LOOKUPFARTHESTWALKINGDISTANCELIST;
        public List<CodedDescriptor> LOOKUPFARTHESTWHEELEDDISTANCELIST;

        public List<CodedDescriptor> LOOKUPACTIVITYTLEVELLIST;
        public List<CodedDescriptor> LOOKUPGOINGOUTLIST;
        public List<CodedDescriptor> LOOKUPCHANGEINADLLIST;
        public List<CodedDescriptor> LOOKUPBLADDERLIST;
        public List<CodedDescriptor> LOOKUPURINARYLIST;
        public List<CodedDescriptor> LOOKUPBOWELLIST;

        public List<CodedDescriptor> LOOKUPDIAGPRESENTLIST;
        public List<CodedDescriptor> LOOKUPFALLSLIST;
        public List<CodedDescriptor> LOOKUPPROBLEMFREQUENCYLIST;
        public List<CodedDescriptor> LOOKUPDYSPNEALIST;
        public List<CodedDescriptor> LOOKUPFATIGUELIST;
        public List<CodedDescriptor> LOOKUPPAIN1LIST;
        public List<CodedDescriptor> LOOKUPPAIN2LIST;

        public List<CodedDescriptor> LOOKUPPAIN3LIST;
        public List<CodedDescriptor> LOOKUPPAIN4LIST;
        public List<CodedDescriptor> LOOKUPSELFREPORTEDHEALTHLIST;
        public List<CodedDescriptor> LOOKUPTOBACCOLIST;
        public List<CodedDescriptor> LOOKUPALCOHOLLIST;
        public List<CodedDescriptor> LOOKUPNUTRITIONLIST;
        public List<CodedDescriptor> LOOKUPPRESSUREULCERLIST;
        public List<CodedDescriptor> LOOKUPFOOTPROBLEMLIST;

        public List<CodedDescriptor> RELASHIPSHIPTOPERSONLIST;
        public List<CodedDescriptor> LIVEWITHPERSONLIST;
        public List<CodedDescriptor> INFORMALHELPLIST;
        public List<CodedDescriptor> TREATMENTLIST;
        public List<CodedDescriptor> SELFSUFFICIENCYLIST;
        public List<CodedDescriptor> PROBLEMRELATEDTODETERIORATIONLIST;
        public List<CodedDescriptor> LOOKUPMEDICATIONADHERENCE;

        public List<CodedDescriptor> LOOKUP_CA_PARENTS_MARITAL_STATUS;
        public List<CodedDescriptor> LOOKUP_CA_ASSESSMENT_REASONS;
        public List<CodedDescriptor> LOOKUP_CA_LIVING_STATUS;
        public List<CodedDescriptor> LOOKUP_CA_LIVING_ARRANGEMENT;
        public List<CodedDescriptor> LOOKUP_CA_LAST_HOSPITAL_STAY;
        public List<CodedDescriptor> LOOKUP_CA_PRIMARY_LANGUAGE;
        public List<CodedDescriptor> LOOKUP_CA_EDUCATION_STATUS;
        public List<CodedDescriptor> LOOKUP_CA_SERVICES_PROVIDED_AT_SCHOOL;
        public List<CodedDescriptor> LOOKUP_CA_PRENATAL_HISTORY;
        public List<CodedDescriptor> LOOKUP_CA_COGNITIVIE_SKILLS;
        public List<CodedDescriptor> LOOKUP_CA_BEHAVIOR_PRESENT;
        public List<CodedDescriptor> LOOKUP_CA_UNDERSTOOD;
        public List<CodedDescriptor> LOOKUP_CA_HEARING_DIFFICULTY;
        public List<CodedDescriptor> LOOKUP_CA_VISION_DIFFICULTY;
        public List<CodedDescriptor> LOOKUP_CA_CHANGE_IN_DECISION;
        public List<CodedDescriptor> LOOKUP_CA_INDICATOR_BEHAVIOR;
        public List<CodedDescriptor> LOOKUP_CA_MOOD;
        public List<CodedDescriptor> LOOKUP_CA_SELF_INJURIOUS;
        public List<CodedDescriptor> LOOKUP_CA_INTENT;
        public List<CodedDescriptor> LOOKUP_CA_SOCIAL;

        public List<CodedDescriptor> LOOKUP_CA_ADAPTABILITY;
        public List<CodedDescriptor> LOOKUP_CA_ADHERENT_WITH_MEDICATIONS;
        public List<CodedDescriptor> LOOKUP_CA_ADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_CA_ALCOHOL;
        public List<CodedDescriptor> LOOKUP_CA_BEHAVIOR;
        public List<CodedDescriptor> LOOKUP_CA_BLADDER_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_CA_BOWEL_COLLECTION_DEVICE;
        public List<CodedDescriptor> LOOKUP_CA_BOWEL_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_CA_CARE_GOALS;
        public List<CodedDescriptor> LOOKUP_CA_CAREGIVER_HELP;
        public List<CodedDescriptor> LOOKUP_CA_CHANGE_IN_ADL;
        public List<CodedDescriptor> LOOKUP_CA_DAYS_WENT_OUT;
        public List<CodedDescriptor> LOOKUP_CA_DEGREE_COMPLETED;
        public List<CodedDescriptor> LOOKUP_CA_DISEASE_CODE;
        public List<CodedDescriptor> LOOKUP_CA_DYSPNEA;
        public List<CodedDescriptor> LOOKUP_CA_EFFECT;
        public List<CodedDescriptor> LOOKUP_CA_EXPECTED_SERVICES;
        public List<CodedDescriptor> LOOKUP_CA_FATIGUE;
        public List<CodedDescriptor> LOOKUP_CA_FOOT_PROBLEMS;
        public List<CodedDescriptor> LOOKUP_CA_FORMAL_CARE;
        public List<CodedDescriptor> LOOKUP_CA_FORMAL_TREATMENTS;
        public List<CodedDescriptor> LOOKUP_CA_FUTURE_NEEDS;
        public List<CodedDescriptor> LOOKUP_CA_GENDER;
        public List<CodedDescriptor> LOOKUP_CA_HOME_ENVIRONMENT;
        public List<CodedDescriptor> LOOKUP_CA_IADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_CA_INTELLECTUAL_DISABILITY;
        public List<CodedDescriptor> LOOKUP_CA_LEGAL_GUARDIANSHIP;
        public List<CodedDescriptor> LOOKUP_CA_LIVES_WITH_CHILD_YOUTH;
        public List<CodedDescriptor> LOOKUP_CA_LOCOMOTION;
        public List<CodedDescriptor> LOOKUP_CA_NUTRITION_INTAKE;
        public List<CodedDescriptor> LOOKUP_CA_PAIN1;
        public List<CodedDescriptor> LOOKUP_CA_PAIN2;
        public List<CodedDescriptor> LOOKUP_CA_PAIN3;
        public List<CodedDescriptor> LOOKUP_CA_PAIN4;
        public List<CodedDescriptor> LOOKUP_CA_PHYSICAL_ACTIVITY_HOURS;
        public List<CodedDescriptor> LOOKUP_CA_PRESSURE_ULCER;
        public List<CodedDescriptor> LOOKUP_CA_PROBLEM_FREQUENCY;
        public List<CodedDescriptor> LOOKUP_CA_RELATIONSHIP;
        public List<CodedDescriptor> LOOKUP_CA_SELF_REPORTED_MOOD;
        public List<CodedDescriptor> LOOKUP_CA_SELF_SUFFICIENCY;
        public List<CodedDescriptor> LOOKUP_CA_UNDERSTANDS;
        public List<CodedDescriptor> LOOKUP_CA_URINARY;

        public List<CodedDescriptor> LOOKUP_HCA_ADHERENT_WITH_MEDICATIONS;
        public List<CodedDescriptor> LOOKUP_HCA_ADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_HCA_ALCOHOL;
        public List<CodedDescriptor> LOOKUP_HCA_ASSESSMENT_REASONS;
        public List<CodedDescriptor> LOOKUP_HCA_BEHAVIOR;
        public List<CodedDescriptor> LOOKUP_HCA_BEHAVIOR_PRESENT;
        public List<CodedDescriptor> LOOKUP_HCA_BLADDER_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_HCA_BOWEL_CONTINENCE;
        public List<CodedDescriptor> LOOKUP_HCA_CHANGE_IN_ADL;
        public List<CodedDescriptor> LOOKUP_HCA_CHANGE_IN_DECISION;
        public List<CodedDescriptor> LOOKUP_HCA_COGNITIVIE_SKILLS;
        public List<CodedDescriptor> LOOKUP_HCA_DAYS_WENT_OUT;
        public List<CodedDescriptor> LOOKUP_HCA_DISEASE_CODE;
        public List<CodedDescriptor> LOOKUP_HCA_DISTANCE_WALKED;
        public List<CodedDescriptor> LOOKUP_HCA_DISTANCE_WHEELED;
        public List<CodedDescriptor> LOOKUP_HCA_DYSPNEA;
        public List<CodedDescriptor> LOOKUP_HCA_FALLS;
        public List<CodedDescriptor> LOOKUP_HCA_FATIGUE;
        public List<CodedDescriptor> LOOKUP_HCA_FOOT_PROBLEMS;
        public List<CodedDescriptor> LOOKUP_HCA_FORMAL_TREATMENTS;
        public List<CodedDescriptor> LOOKUP_HCA_HEARING_DIFFICULTY;
        public List<CodedDescriptor> LOOKUP_HCA_IADL_SELF_PERFORMANCE;
        public List<CodedDescriptor> LOOKUP_HCA_INFORMAL_HELP;
        public List<CodedDescriptor> LOOKUP_HCA_LAST_HOSPITAL_STAY;
        public List<CodedDescriptor> LOOKUP_HCA_LIVES_WITH_PERSON;
        public List<CodedDescriptor> LOOKUP_HCA_LIVING_ARRANGEMENT;
        public List<CodedDescriptor> LOOKUP_HCA_LIVING_BETTER_OFF;
        public List<CodedDescriptor> LOOKUP_HCA_LIVING_STATUS;
        public List<CodedDescriptor> LOOKUP_HCA_LOCOMOTION;
        public List<CodedDescriptor> LOOKUP_HCA_LOCOMOTION_TIMED;
        public List<CodedDescriptor> LOOKUP_HCA_MARITAL_STATUS;
        public List<CodedDescriptor> LOOKUP_HCA_MOOD;
        public List<CodedDescriptor> LOOKUP_HCA_NUTRITION_INTAKE;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN1;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN2;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN3;
        public List<CodedDescriptor> LOOKUP_HCA_PAIN4;
        public List<CodedDescriptor> LOOKUP_HCA_PHYSICAL_ACTIVITY_HOURS;
        public List<CodedDescriptor> LOOKUP_HCA_PRECIPITATING_EVENT;
        public List<CodedDescriptor> LOOKUP_HCA_PRESSURE_ULCER;
        public List<CodedDescriptor> LOOKUP_HCA_PRIMARY_LANGUAGE;
        public List<CodedDescriptor> LOOKUP_HCA_PROBLEM_FREQUENCY;
        public List<CodedDescriptor> LOOKUP_HCA_RELATIONSHIP;
        public List<CodedDescriptor> LOOKUP_HCA_SELF_REPORTED_HEALTH;
        public List<CodedDescriptor> LOOKUP_HCA_SELF_REPORTED_MOOD;
        public List<CodedDescriptor> LOOKUP_HCA_SELF_SUFFICIENCY;
        public List<CodedDescriptor> LOOKUP_HCA_SOCIAL;
        public List<CodedDescriptor> LOOKUP_HCA_SOCIAL_ACTIVITES_CHANGE;
        public List<CodedDescriptor> LOOKUP_HCA_TIME_ALONE;
        public List<CodedDescriptor> LOOKUP_HCA_TOBACCO;
        public List<CodedDescriptor> LOOKUP_HCA_TREATMENTS;
        public List<CodedDescriptor> LOOKUP_HCA_UNDERSTANDS;
        public List<CodedDescriptor> LOOKUP_HCA_UNDERSTOOD;
        public List<CodedDescriptor> LOOKUP_HCA_URINARY;
        public List<CodedDescriptor> LOOKUP_HCA_VISION_DIFFICULTY;
    }

    public class ProviderDesignation
    {
        public DateTime Edate = Convert.ToDateTime(null);
        public long mpcID = -1;
        public string ProviderID = "";
        public string ProviderName = "";
        public DateTime Sdate = Convert.ToDateTime(null);
        public string ProviderNPI = "";
        public string ContractNPI = "";
    }

    public class GMRRegions
    {
        public string DESC = "";
        public string ID = "";
        public double LAT = 0.0;
        public double LON = 0.0;
        public string NAME = "";
        public int ZOOM = 0;
    }

    public class NumericDescriptor
    {
        public string description = "";
        public long number = -1;
    }

    public class ServiceGapDescriptor
    {
        public string CMSID = "";
        public string GAPDESCRIPTION = "";
        public string GAPDOMAIN = "";
        public string MEASURE = "";
        public string TYPE = "";
        public string CPT = "";
        public string HCPCS = "";
        public string UB = "";
        public string DIAG = "";
        public string LOINC = "";

    }

    public class ZipCodeDescriptor
    {
        public string zipcode = "";
        public string zipcodecity = "";
        public string zipcodestate = "";
        public string zipcounty = "";
        public double ziplat = 0.0;
        public double ziplong = 0.0;
    }

    public class LookupServices
    {
        public long svcID = 0;
        public string Funder = "";
        public string CostCenter = "";
        public string SvcCode = "";
        public string SvcDescription = "";
        public string UnitType = "";
        public double CostPerUnit = 0.0;
        public string ACTIVE = "";
        public string AUTHREQ = "";
        public string RelatedSplitCode = "";
        public bool BCBANoteRequired = false;
        public bool BANote = false;
    }


}

#endregion