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

        [Route("api/Login/MOTD/{uname}")]
        [HttpGet]
        public JsonResult<string> MOTD(string uname)
        {

            string result = "This Message of the day was retrieved directly from the server " +
                "and illustrates a convienient method for getting the word out to the Mobile " +
                "user community supported by the CFCS System. These may be targeted to specific " +
                "users or roles of users";

            return Json(result);

        }


        [Route("api/Login/DoLogin/{uname}/{pw}")]
        [HttpGet]
        public JsonResult<LoginResult>DoLogin(string uname, string pw)
        {
            bool res = DBLocked();
                      

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
            catch (Exception ex)
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
            catch (Exception Ex)
            {
                // fail silently
                locked = false;
            }

            return locked;
        }

        [Route("api/Login/GetCaseLoad/{userName}")]
        [HttpGet]
        public JsonResult<List<MemberDetailsShort>> GetCurrentCaseLoad(string userName)
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

        [Route("api/Login/GetAuths/{IDNUM}")]
        [HttpGet]
        public JsonResult<List<AuthorizedService>> GetAuthsAvailableForMember(string IDNUM)
        {
            List<AuthorizedService> result = new List<AuthorizedService>();

            try
            {
               
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

        [Route("api/Login/MemberObservers/{IDNUM}")]
        [HttpGet]
        public JsonResult<List<MemberObservers>> GetListOfMemberObservers(string IDNUM)
        {
            List<MemberObservers> ret = new List<MemberObservers>();

            try
            {
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

        [Route("api/Login/MemberReferrals/{IDNUM}")]
        [HttpGet]
        public JsonResult<List<MemberReferralSource>> GetListOfMemberReferralSourceForMember(string IDNUM)
        {
            List<MemberReferralSource> result = new List<MemberReferralSource>();
            try
            {
                
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

        [Route("api/Login/EncounterProgressNotes/{IDNUM}")]
        [HttpGet]
        public JsonResult<List<MemberProgressNotes>> GetListOfEncounterNotesForType(string IDNUM)

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
                LogError("GetListOfProgressNotesForType", ex.Message);

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
                LogError("GetListOfProgressNotesForType", ex.Message);

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


}

#endregion