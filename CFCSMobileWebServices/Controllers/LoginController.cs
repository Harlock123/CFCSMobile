﻿using System;
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

    }

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
}
