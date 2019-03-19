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
    public partial class LoginController : ApiController
    {
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
}