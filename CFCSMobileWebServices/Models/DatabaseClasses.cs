using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CFCSMobileWebServices
{

    public partial class tblMemberProgressNotes : INotifyPropertyChanged
    {

        #region Declarations
        string _classDatabaseConnectionString = "";
        string _bulkinsertPath = "";

        SqlConnection _cn = new SqlConnection();
        SqlCommand _cmd = new SqlCommand();

        // Backing Variables for Properties
        long _mpnID = 0;
        string _SSN = "";
        string _NOTECONTACTTYPE = "";
        string _NOTETYPE = "";
        string _CREATEDBY = "";
        DateTime _CREATEDATE = Convert.ToDateTime(null);
        DateTime _CONTACTDATE = Convert.ToDateTime(null);
        string _NOTATION = "";
        string _SIGNED = "";
        DateTime _SIGNDATE = Convert.ToDateTime(null);
        string _SUPERAPPROV = "";
        DateTime _SUPERAPPROVEDATE = Convert.ToDateTime(null);
        string _OLDNOTATION = "";
        string _SUPERVISORNOTATION = "";
        string _SUPERVISORACK1 = "";
        string _SUPERVISORACK2 = "";
        DateTime _SUPERVISORNOTATIONDATE = Convert.ToDateTime(null);
        string _HIDDEN = "";
        int _TRAVELTIME = 0;
        int _CONTACTTIME = 0;
        DateTime _SAFETYASSESSMENT = Convert.ToDateTime(null);
        string _SAFETYASSESSMENTLVL = "";
        string _SERVICECODE = "";

        #endregion

        #region Properties

        public string classDatabaseConnectionString
        {
            get { return _classDatabaseConnectionString; }
            set { _classDatabaseConnectionString = value; }
        }

        public string bulkinsertPath
        {
            get { return _bulkinsertPath; }
            set { _bulkinsertPath = value; }
        }

        public long mpnID
        {
            get { return _mpnID; }
            set
            {
                _mpnID = value;
                RaisePropertyChanged("mpnID");
            }
        }

        public string SSN
        {
            get { return _SSN; }
            set
            {
                if (value.Length > 20)
                { _SSN = value.Substring(0, 20); }
                else
                {
                    _SSN = value;
                    RaisePropertyChanged("SSN");
                }
            }
        }

        public string NOTECONTACTTYPE
        {
            get { return _NOTECONTACTTYPE; }
            set
            {
                if (value.Length > 5)
                { _NOTECONTACTTYPE = value.Substring(0, 5); }
                else
                {
                    _NOTECONTACTTYPE = value;
                    RaisePropertyChanged("NOTECONTACTTYPE");
                }
            }
        }

        public string NOTETYPE
        {
            get { return _NOTETYPE; }
            set
            {
                if (value.Length > 5)
                { _NOTETYPE = value.Substring(0, 5); }
                else
                {
                    _NOTETYPE = value;
                    RaisePropertyChanged("NOTETYPE");
                }
            }
        }

        public string CREATEDBY
        {
            get { return _CREATEDBY; }
            set
            {
                if (value.Length > 20)
                { _CREATEDBY = value.Substring(0, 20); }
                else
                {
                    _CREATEDBY = value;
                    RaisePropertyChanged("CREATEDBY");
                }
            }
        }

        public DateTime CREATEDATE
        {
            get { return _CREATEDATE; }
            set
            {
                _CREATEDATE = value;
                RaisePropertyChanged("CREATEDATE");
            }
        }

        public DateTime CONTACTDATE
        {
            get { return _CONTACTDATE; }
            set
            {
                _CONTACTDATE = value;
                RaisePropertyChanged("CONTACTDATE");
            }
        }

        public string NOTATION
        {
            get { return _NOTATION; }
            set
            {
                if (value.Length > 2147483647)
                { _NOTATION = value.Substring(0, 2147483647); }
                else
                {
                    _NOTATION = value;
                    RaisePropertyChanged("NOTATION");
                }
            }
        }

        public string SIGNED
        {
            get { return _SIGNED; }
            set
            {
                if (value.Length > 1)
                { _SIGNED = value.Substring(0, 1); }
                else
                {
                    _SIGNED = value;
                    RaisePropertyChanged("SIGNED");
                }
            }
        }

        public DateTime SIGNDATE
        {
            get { return _SIGNDATE; }
            set
            {
                _SIGNDATE = value;
                RaisePropertyChanged("SIGNDATE");
            }
        }

        public string SUPERAPPROV
        {
            get { return _SUPERAPPROV; }
            set
            {
                if (value.Length > 20)
                { _SUPERAPPROV = value.Substring(0, 20); }
                else
                {
                    _SUPERAPPROV = value;
                    RaisePropertyChanged("SUPERAPPROV");
                }
            }
        }

        public DateTime SUPERAPPROVEDATE
        {
            get { return _SUPERAPPROVEDATE; }
            set
            {
                _SUPERAPPROVEDATE = value;
                RaisePropertyChanged("SUPERAPPROVEDATE");
            }
        }

        public string OLDNOTATION
        {
            get { return _OLDNOTATION; }
            set
            {
                if (value.Length > 2147483647)
                { _OLDNOTATION = value.Substring(0, 2147483647); }
                else
                {
                    _OLDNOTATION = value;
                    RaisePropertyChanged("OLDNOTATION");
                }
            }
        }

        public string SUPERVISORNOTATION
        {
            get { return _SUPERVISORNOTATION; }
            set
            {
                if (value.Length > 2147483647)
                { _SUPERVISORNOTATION = value.Substring(0, 2147483647); }
                else
                {
                    _SUPERVISORNOTATION = value;
                    RaisePropertyChanged("SUPERVISORNOTATION");
                }
            }
        }

        public string SUPERVISORACK1
        {
            get { return _SUPERVISORACK1; }
            set
            {
                if (value.Length > 1)
                { _SUPERVISORACK1 = value.Substring(0, 1); }
                else
                {
                    _SUPERVISORACK1 = value;
                    RaisePropertyChanged("SUPERVISORACK1");
                }
            }
        }

        public string SUPERVISORACK2
        {
            get { return _SUPERVISORACK2; }
            set
            {
                if (value.Length > 1)
                { _SUPERVISORACK2 = value.Substring(0, 1); }
                else
                {
                    _SUPERVISORACK2 = value;
                    RaisePropertyChanged("SUPERVISORACK2");
                }
            }
        }

        public DateTime SUPERVISORNOTATIONDATE
        {
            get { return _SUPERVISORNOTATIONDATE; }
            set
            {
                _SUPERVISORNOTATIONDATE = value;
                RaisePropertyChanged("SUPERVISORNOTATIONDATE");
            }
        }

        public string HIDDEN
        {
            get { return _HIDDEN; }
            set
            {
                if (value.Length > 1)
                { _HIDDEN = value.Substring(0, 1); }
                else
                {
                    _HIDDEN = value;
                    RaisePropertyChanged("HIDDEN");
                }
            }
        }

        public int TRAVELTIME
        {
            get { return _TRAVELTIME; }
            set
            {
                _TRAVELTIME = value;
                RaisePropertyChanged("TRAVELTIME");
            }
        }

        public int CONTACTTIME
        {
            get { return _CONTACTTIME; }
            set
            {
                _CONTACTTIME = value;
                RaisePropertyChanged("CONTACTTIME");
            }
        }

        public DateTime SAFETYASSESSMENT
        {
            get { return _SAFETYASSESSMENT; }
            set
            {
                _SAFETYASSESSMENT = value;
                RaisePropertyChanged("SAFETYASSESSMENT");
            }
        }

        public string SAFETYASSESSMENTLVL
        {
            get { return _SAFETYASSESSMENTLVL; }
            set
            {
                if (value.Length > 5)
                { _SAFETYASSESSMENTLVL = value.Substring(0, 5); }
                else
                {
                    _SAFETYASSESSMENTLVL = value;
                    RaisePropertyChanged("SAFETYASSESSMENTLVL");
                }
            }
        }

        public string SERVICECODE
        {
            get { return _SERVICECODE; }
            set
            {
                if (value.Length > 2)
                { _SERVICECODE = value.Substring(0, 2); }
                else
                {
                    _SERVICECODE = value;
                    RaisePropertyChanged("SERVICECODE");
                }
            }
        }


        #endregion

        #region Implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor

        public tblMemberProgressNotes()
        {
            // Constructor code goes here.
            Initialize();
        }

        public tblMemberProgressNotes(string DSN)
        {
            // Constructor code goes here.
            Initialize();
            classDatabaseConnectionString = DSN;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _mpnID = 0;
            _SSN = "";
            _NOTECONTACTTYPE = "";
            _NOTETYPE = "";
            _CREATEDBY = "";
            _CREATEDATE = Convert.ToDateTime(null);
            _CONTACTDATE = Convert.ToDateTime(null);
            _NOTATION = "";
            _SIGNED = "";
            _SIGNDATE = Convert.ToDateTime(null);
            _SUPERAPPROV = "";
            _SUPERAPPROVEDATE = Convert.ToDateTime(null);
            _OLDNOTATION = "";
            _SUPERVISORNOTATION = "";
            _SUPERVISORACK1 = "";
            _SUPERVISORACK2 = "";
            _SUPERVISORNOTATIONDATE = Convert.ToDateTime(null);
            _HIDDEN = "";
            _TRAVELTIME = 0;
            _CONTACTTIME = 0;
            _SAFETYASSESSMENT = Convert.ToDateTime(null);
            _SAFETYASSESSMENTLVL = "";
            _SERVICECODE = "";
        }

        public void CopyFields(SqlDataReader r)
        {
            try
            {
                if (!Convert.IsDBNull(r["mpnID"]))
                {
                    _mpnID = Convert.ToInt64(r["mpnID"]);
                }
                if (!Convert.IsDBNull(r["SSN"]))
                {
                    _SSN = r["SSN"] + "";
                }
                if (!Convert.IsDBNull(r["NOTECONTACTTYPE"]))
                {
                    _NOTECONTACTTYPE = r["NOTECONTACTTYPE"] + "";
                }
                if (!Convert.IsDBNull(r["NOTETYPE"]))
                {
                    _NOTETYPE = r["NOTETYPE"] + "";
                }
                if (!Convert.IsDBNull(r["CREATEDBY"]))
                {
                    _CREATEDBY = r["CREATEDBY"] + "";
                }
                if (!Convert.IsDBNull(r["CREATEDATE"]))
                {
                    _CREATEDATE = Convert.ToDateTime(r["CREATEDATE"]);
                }
                if (!Convert.IsDBNull(r["CONTACTDATE"]))
                {
                    _CONTACTDATE = Convert.ToDateTime(r["CONTACTDATE"]);
                }
                if (!Convert.IsDBNull(r["NOTATION"]))
                {
                    _NOTATION = r["NOTATION"] + "";
                }
                if (!Convert.IsDBNull(r["SIGNED"]))
                {
                    _SIGNED = r["SIGNED"] + "";
                }
                if (!Convert.IsDBNull(r["SIGNDATE"]))
                {
                    _SIGNDATE = Convert.ToDateTime(r["SIGNDATE"]);
                }
                if (!Convert.IsDBNull(r["SUPERAPPROV"]))
                {
                    _SUPERAPPROV = r["SUPERAPPROV"] + "";
                }
                if (!Convert.IsDBNull(r["SUPERAPPROVEDATE"]))
                {
                    _SUPERAPPROVEDATE = Convert.ToDateTime(r["SUPERAPPROVEDATE"]);
                }
                if (!Convert.IsDBNull(r["OLDNOTATION"]))
                {
                    _OLDNOTATION = r["OLDNOTATION"] + "";
                }
                if (!Convert.IsDBNull(r["SUPERVISORNOTATION"]))
                {
                    _SUPERVISORNOTATION = r["SUPERVISORNOTATION"] + "";
                }
                if (!Convert.IsDBNull(r["SUPERVISORACK1"]))
                {
                    _SUPERVISORACK1 = r["SUPERVISORACK1"] + "";
                }
                if (!Convert.IsDBNull(r["SUPERVISORACK2"]))
                {
                    _SUPERVISORACK2 = r["SUPERVISORACK2"] + "";
                }
                if (!Convert.IsDBNull(r["SUPERVISORNOTATIONDATE"]))
                {
                    _SUPERVISORNOTATIONDATE = Convert.ToDateTime(r["SUPERVISORNOTATIONDATE"]);
                }
                if (!Convert.IsDBNull(r["HIDDEN"]))
                {
                    _HIDDEN = r["HIDDEN"] + "";
                }
                if (!Convert.IsDBNull(r["TRAVELTIME"]))
                {
                    _TRAVELTIME = Convert.ToInt32(r["TRAVELTIME"]);
                }
                if (!Convert.IsDBNull(r["CONTACTTIME"]))
                {
                    _CONTACTTIME = Convert.ToInt32(r["CONTACTTIME"]);
                }
                if (!Convert.IsDBNull(r["SAFETYASSESSMENT"]))
                {
                    _SAFETYASSESSMENT = Convert.ToDateTime(r["SAFETYASSESSMENT"]);
                }
                if (!Convert.IsDBNull(r["SAFETYASSESSMENTLVL"]))
                {
                    _SAFETYASSESSMENTLVL = r["SAFETYASSESSMENTLVL"] + "";
                }
                if (!Convert.IsDBNull(r["SERVICECODE"]))
                {
                    _SERVICECODE = r["SERVICECODE"] + "";
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.CopyFields " + ex.ToString()));
            }
        }

        public void Add()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SSN == "" || this._SSN == string.Empty)
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = this._SSN;
                }
                if (this._NOTECONTACTTYPE == "" || this._NOTECONTACTTYPE == string.Empty)
                {
                    cmd.Parameters.Add("@NOTECONTACTTYPE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@NOTECONTACTTYPE", System.Data.SqlDbType.VarChar).Value = this._NOTECONTACTTYPE;
                }
                cmd.Parameters.Add("@NOTETYPE", System.Data.SqlDbType.VarChar).Value = this._NOTETYPE;
                if (this._CREATEDBY == "" || this._CREATEDBY == string.Empty)
                {
                    cmd.Parameters.Add("@CREATEDBY", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CREATEDBY", System.Data.SqlDbType.VarChar).Value = this._CREATEDBY;
                }
                cmd.Parameters.Add("@CREATEDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._CREATEDATE);
                cmd.Parameters.Add("@CONTACTDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._CONTACTDATE);
                cmd.Parameters.Add("@NOTATION", System.Data.SqlDbType.VarChar).Value = this._NOTATION;
                if (this._SIGNED == "" || this._SIGNED == string.Empty)
                {
                    cmd.Parameters.Add("@SIGNED", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SIGNED", System.Data.SqlDbType.VarChar).Value = this._SIGNED;
                }
                cmd.Parameters.Add("@SIGNDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SIGNDATE);
                if (this._SUPERAPPROV == "" || this._SUPERAPPROV == string.Empty)
                {
                    cmd.Parameters.Add("@SUPERAPPROV", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SUPERAPPROV", System.Data.SqlDbType.VarChar).Value = this._SUPERAPPROV;
                }
                cmd.Parameters.Add("@SUPERAPPROVEDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SUPERAPPROVEDATE);
                if (this._OLDNOTATION == "" || this._OLDNOTATION == string.Empty)
                {
                    cmd.Parameters.Add("@OLDNOTATION", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@OLDNOTATION", System.Data.SqlDbType.VarChar).Value = this._OLDNOTATION;
                }
                if (this._SUPERVISORNOTATION == "" || this._SUPERVISORNOTATION == string.Empty)
                {
                    cmd.Parameters.Add("@SUPERVISORNOTATION", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SUPERVISORNOTATION", System.Data.SqlDbType.VarChar).Value = this._SUPERVISORNOTATION;
                }
                if (this._SUPERVISORACK1 == "" || this._SUPERVISORACK1 == string.Empty)
                {
                    cmd.Parameters.Add("@SUPERVISORACK1", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SUPERVISORACK1", System.Data.SqlDbType.VarChar).Value = this._SUPERVISORACK1;
                }
                if (this._SUPERVISORACK2 == "" || this._SUPERVISORACK2 == string.Empty)
                {
                    cmd.Parameters.Add("@SUPERVISORACK2", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SUPERVISORACK2", System.Data.SqlDbType.VarChar).Value = this._SUPERVISORACK2;
                }
                cmd.Parameters.Add("@SUPERVISORNOTATIONDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SUPERVISORNOTATIONDATE);
                cmd.Parameters.Add("@HIDDEN", System.Data.SqlDbType.VarChar).Value = this._HIDDEN;
                cmd.Parameters.Add("@TRAVELTIME", System.Data.SqlDbType.Int).Value = this._TRAVELTIME;
                cmd.Parameters.Add("@CONTACTTIME", System.Data.SqlDbType.Int).Value = this._CONTACTTIME;
                cmd.Parameters.Add("@SAFETYASSESSMENT", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SAFETYASSESSMENT);
                if (this._SAFETYASSESSMENTLVL == "" || this._SAFETYASSESSMENTLVL == string.Empty)
                {
                    cmd.Parameters.Add("@SAFETYASSESSMENTLVL", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SAFETYASSESSMENTLVL", System.Data.SqlDbType.VarChar).Value = this._SAFETYASSESSMENTLVL;
                }
                if (this._SERVICECODE == "" || this._SERVICECODE == string.Empty)
                {
                    cmd.Parameters.Add("@SERVICECODE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SERVICECODE", System.Data.SqlDbType.VarChar).Value = this._SERVICECODE;
                }

                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (mpnID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _mpnID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.Add " + ex.ToString()));
            }
        }

        public void Update()
        {
            try
            {
                Add();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.Update " + ex.ToString()));
            }
        }

        public void Delete()
        {
            try
            {
                string sql = "Delete from tblMemberProgressNotes WHERE mpnID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = this._mpnID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.Delete " + ex.ToString()));
            }
        }

        public void Read(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberProgressNotes WHERE mpnID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberProgressNotes WHERE mpnID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblMemberProgressNotes");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.ReadAsDataSet " + ex.ToString()));
            }
        }

        public void Read(string SSN)
        {
            try
            {
                string sql = "Select * from tblMemberProgressNotes WHERE SSN = @SSN";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = SSN;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(string SSN)
        {
            try
            {
                string sql = "Select * from tblMemberProgressNotes WHERE SSN = @SSN";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = SSN;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblMemberProgressNotes");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberProgressNotes.ReadAsDataSet " + ex.ToString()));
            }
        }
        #endregion

        #region Private Methods

        private string GetParameterSQL()
        {
            string sql = "";
            if (_mpnID < 1)
            {
                sql = "INSERT INTO tblMemberProgressNotes";
                sql += "(";
                sql += "[SSN], [NOTECONTACTTYPE], [NOTETYPE], [CREATEDBY], [CREATEDATE], [CONTACTDATE],";
                sql += "[NOTATION], [SIGNED], [SIGNDATE], [SUPERAPPROV], [SUPERAPPROVEDATE], [OLDNOTATION],";
                sql += "[SUPERVISORNOTATION], [SUPERVISORACK1], [SUPERVISORACK2], [SUPERVISORNOTATIONDATE],";
                sql += "[HIDDEN], [TRAVELTIME], [CONTACTTIME], [SAFETYASSESSMENT], [SAFETYASSESSMENTLVL],[SERVICECODE] ";
                sql += ") ";
                sql += "VALUES (";
                sql += "@SSN,@NOTECONTACTTYPE,@NOTETYPE,@CREATEDBY,@CREATEDATE,@CONTACTDATE,@NOTATION,";
                sql += "@SIGNED,@SIGNDATE,@SUPERAPPROV,@SUPERAPPROVEDATE,@OLDNOTATION,@SUPERVISORNOTATION,";
                sql += "@SUPERVISORACK1,@SUPERVISORACK2,@SUPERVISORNOTATIONDATE,@HIDDEN,@TRAVELTIME,";
                sql += "@CONTACTTIME,@SAFETYASSESSMENT,@SAFETYASSESSMENTLVL,@SERVICECODE)";
            }
            else
            {
                sql = "UPDATE tblMemberProgressNotes SET ";
                sql += "[SSN] = @SSN, [NOTECONTACTTYPE] = @NOTECONTACTTYPE, [NOTETYPE] = @NOTETYPE,";
                sql += "[CREATEDBY] = @CREATEDBY, [CREATEDATE] = @CREATEDATE, [CONTACTDATE] = @CONTACTDATE,";
                sql += "[NOTATION] = @NOTATION, [SIGNED] = @SIGNED, [SIGNDATE] = @SIGNDATE, [SUPERAPPROV] = @SUPERAPPROV,";
                sql += "[SUPERAPPROVEDATE] = @SUPERAPPROVEDATE, [OLDNOTATION] = @OLDNOTATION, [SUPERVISORNOTATION] = @SUPERVISORNOTATION,";
                sql += "[SUPERVISORACK1] = @SUPERVISORACK1, [SUPERVISORACK2] = @SUPERVISORACK2, [SUPERVISORNOTATIONDATE] = @SUPERVISORNOTATIONDATE,";
                sql += "[HIDDEN] = @HIDDEN, [TRAVELTIME] = @TRAVELTIME, [CONTACTTIME] = @CONTACTTIME,";
                sql += "[SAFETYASSESSMENT] = @SAFETYASSESSMENT, [SAFETYASSESSMENTLVL] = @SAFETYASSESSMENTLVL,[SERVICECODE] = @SERVICECODE";
                sql += "";
                sql += " WHERE mpnID = " + _mpnID.ToString();
            }
            return sql;
        }

        private object getDateOrNull(DateTime d)
        {
            if (d == Convert.ToDateTime(null))
            {
                return DBNull.Value;
            }
            else
            {
                return d;
            }
        }
        #endregion
    }

    public partial class tblMemberEncounters : INotifyPropertyChanged
    {

        #region Declarations
        string _classDatabaseConnectionString = "";
        string _bulkinsertPath = "";

        SqlConnection _cn = new SqlConnection();
        SqlCommand _cmd = new SqlCommand();

        // Backing Variables for Properties
        long _tblMemberEncountersID = 0;
        string _SSN = "";
        DateTime _EncounterDate = Convert.ToDateTime(null);
        DateTime _EncounterStartTime = Convert.ToDateTime(null);
        DateTime _EncounterEndTime = Convert.ToDateTime(null);
        string _TypeOfServiceDeliverySite = "";
        string _ServiceDelivered = "";
        bool _IsGroupService = false;
        bool _IsIndividualService = false;
        string _DeliverySiteAddress1 = "";
        string _DeliverySiteAddress2 = "";
        string _DeliverySiteAddress3 = "";
        string _DeliverySiteCity = "";
        string _DeliverySiteState = "";
        string _DeliverySiteZipCode = "";
        string _DeliverySiteCounty = "";
        string _DeliverySitePhone = "";
        bool _IsGuardian = false;
        bool _IsResponsibleParty = false;
        string _GuardianResponsiblePerson = "";
        string _GuardianPersonAddress1 = "";
        string _GuardianPersonAddress2 = "";
        string _GuardianPersonAddress3 = "";
        string _GuardianPersonCity = "";
        string _GuardianPersonState = "";
        string _GuardianPersonZipCode = "";
        string _GuardianPersonCounty = "";
        string _GuardianPersonRelationship = "";
        DateTime _DateEncounterSigned = Convert.ToDateTime(null);
        string _EncounterStatus = "";
        DateTime _CreatedDate = Convert.ToDateTime(null);
        string _CreatedBy = "";
        DateTime _BilledDate = Convert.ToDateTime(null);
        DateTime _PaidDate = Convert.ToDateTime(null);
        string _CheckNumber = "";
        string _AuthNumber = "";
        int _ProgressNoteID = 0;
        string _NeedsFixingComment = "";
        string _Notation = "";
        long _BANoteID = 0;
        string _ACTID = "";
        double _ChargedAmount = 0.0;
        double _PaidAmount = 0.0;
        DateTime _ConsumerBilledDate = Convert.ToDateTime(null);
        DateTime _ConsumerPaidDate = Convert.ToDateTime(null);
        string _ConsumerCheckNumber = "";
        double _ConsumerChargedAmount = 0.0;
        double _ConsumerPaidAmount = 0.0;
        bool _SUBMITTED = false;
        DateTime _SUBMITTEDDATE = Convert.ToDateTime(null);
        long _ROLLUPID = 0;
        int _BCBAID = 0;

        #endregion

        #region Properties

        public string classDatabaseConnectionString
        {
            get { return _classDatabaseConnectionString; }
            set { _classDatabaseConnectionString = value; }
        }

        public string bulkinsertPath
        {
            get { return _bulkinsertPath; }
            set { _bulkinsertPath = value; }
        }

        public long tblMemberEncountersID
        {
            get { return _tblMemberEncountersID; }
            set
            {
                _tblMemberEncountersID = value;
                RaisePropertyChanged("tblMemberEncountersID");
            }
        }

        public string SSN
        {
            get { return _SSN; }
            set
            {
                if (value != null && value.Length > 20)
                { _SSN = value.Substring(0, 20); }
                else
                {
                    _SSN = value;
                    RaisePropertyChanged("SSN");
                }
            }
        }

        public DateTime EncounterDate
        {
            get { return _EncounterDate; }
            set
            {
                _EncounterDate = value;
                RaisePropertyChanged("EncounterDate");
            }
        }

        public DateTime EncounterStartTime
        {
            get { return _EncounterStartTime; }
            set
            {
                _EncounterStartTime = value;
                RaisePropertyChanged("EncounterStartTime");
            }
        }

        public DateTime EncounterEndTime
        {
            get { return _EncounterEndTime; }
            set
            {
                _EncounterEndTime = value;
                RaisePropertyChanged("EncounterEndTime");
            }
        }

        public string TypeOfServiceDeliverySite
        {
            get { return _TypeOfServiceDeliverySite; }
            set
            {
                if (value != null && value.Length > 5)
                { _TypeOfServiceDeliverySite = value.Substring(0, 5); }
                else
                {
                    _TypeOfServiceDeliverySite = value;
                    RaisePropertyChanged("TypeOfServiceDeliverySite");
                }
            }
        }

        public string ServiceDelivered
        {
            get { return _ServiceDelivered; }
            set
            {
                if (value != null && value.Length > 5)
                { _ServiceDelivered = value.Substring(0, 5); }
                else
                {
                    _ServiceDelivered = value;
                    RaisePropertyChanged("ServiceDelivered");
                }
            }
        }

        public bool IsGroupService
        {
            get { return _IsGroupService; }
            set
            {
                _IsGroupService = value;
                RaisePropertyChanged("IsGroupService");
            }
        }

        public bool IsIndividualService
        {
            get { return _IsIndividualService; }
            set
            {
                _IsIndividualService = value;
                RaisePropertyChanged("IsIndividualService");
            }
        }

        public string DeliverySiteAddress1
        {
            get { return _DeliverySiteAddress1; }
            set
            {
                if (value != null && value.Length > 50)
                { _DeliverySiteAddress1 = value.Substring(0, 50); }
                else
                {
                    _DeliverySiteAddress1 = value;
                    RaisePropertyChanged("DeliverySiteAddress1");
                }
            }
        }

        public string DeliverySiteAddress2
        {
            get { return _DeliverySiteAddress2; }
            set
            {
                if (value != null && value.Length > 50)
                { _DeliverySiteAddress2 = value.Substring(0, 50); }
                else
                {
                    _DeliverySiteAddress2 = value;
                    RaisePropertyChanged("DeliverySiteAddress2");
                }
            }
        }

        public string DeliverySiteAddress3
        {
            get { return _DeliverySiteAddress3; }
            set
            {
                if (value != null && value.Length > 50)
                { _DeliverySiteAddress3 = value.Substring(0, 50); }
                else
                {
                    _DeliverySiteAddress3 = value;
                    RaisePropertyChanged("DeliverySiteAddress3");
                }
            }
        }

        public string DeliverySiteCity
        {
            get { return _DeliverySiteCity; }
            set
            {
                if (value != null && value.Length > 50)
                { _DeliverySiteCity = value.Substring(0, 50); }
                else
                {
                    _DeliverySiteCity = value;
                    RaisePropertyChanged("DeliverySiteCity");
                }
            }
        }

        public string DeliverySiteState
        {
            get { return _DeliverySiteState; }
            set
            {
                if (value != null && value.Length > 2)
                { _DeliverySiteState = value.Substring(0, 2); }
                else
                {
                    _DeliverySiteState = value;
                    RaisePropertyChanged("DeliverySiteState");
                }
            }
        }

        public string DeliverySiteZipCode
        {
            get { return _DeliverySiteZipCode; }
            set
            {
                if (value != null && value.Length > 12)
                { _DeliverySiteZipCode = value.Substring(0, 12); }
                else
                {
                    _DeliverySiteZipCode = value;
                    RaisePropertyChanged("DeliverySiteZipCode");
                }
            }
        }

        public string DeliverySiteCounty
        {
            get { return _DeliverySiteCounty; }
            set
            {
                if (value != null && value.Length > 25)
                { _DeliverySiteCounty = value.Substring(0, 25); }
                else
                {
                    _DeliverySiteCounty = value;
                    RaisePropertyChanged("DeliverySiteCounty");
                }
            }
        }

        public string DeliverySitePhone
        {
            get { return _DeliverySitePhone; }
            set
            {
                if (value != null && value.Length > 20)
                { _DeliverySitePhone = value.Substring(0, 20); }
                else
                {
                    _DeliverySitePhone = value;
                    RaisePropertyChanged("DeliverySitePhone");
                }
            }
        }

        public bool IsGuardian
        {
            get { return _IsGuardian; }
            set
            {
                _IsGuardian = value;
                RaisePropertyChanged("IsGuardian");
            }
        }

        public bool IsResponsibleParty
        {
            get { return _IsResponsibleParty; }
            set
            {
                _IsResponsibleParty = value;
                RaisePropertyChanged("IsResponsibleParty");
            }
        }

        public string GuardianResponsiblePerson
        {
            get { return _GuardianResponsiblePerson; }
            set
            {
                if (value != null && value.Length > 100)
                { _GuardianResponsiblePerson = value.Substring(0, 100); }
                else
                {
                    _GuardianResponsiblePerson = value;
                    RaisePropertyChanged("GuardianResponsiblePerson");
                }
            }
        }

        public string GuardianPersonAddress1
        {
            get { return _GuardianPersonAddress1; }
            set
            {
                if (value != null && value.Length > 50)
                { _GuardianPersonAddress1 = value.Substring(0, 50); }
                else
                {
                    _GuardianPersonAddress1 = value;
                    RaisePropertyChanged("GuardianPersonAddress1");
                }
            }
        }

        public string GuardianPersonAddress2
        {
            get { return _GuardianPersonAddress2; }
            set
            {
                if (value != null && value.Length > 50)
                { _GuardianPersonAddress2 = value.Substring(0, 50); }
                else
                {
                    _GuardianPersonAddress2 = value;
                    RaisePropertyChanged("GuardianPersonAddress2");
                }
            }
        }

        public string GuardianPersonAddress3
        {
            get { return _GuardianPersonAddress3; }
            set
            {
                if (value != null && value.Length > 50)
                { _GuardianPersonAddress3 = value.Substring(0, 50); }
                else
                {
                    _GuardianPersonAddress3 = value;
                    RaisePropertyChanged("GuardianPersonAddress3");
                }
            }
        }

        public string GuardianPersonCity
        {
            get { return _GuardianPersonCity; }
            set
            {
                if (value != null && value.Length > 50)
                { _GuardianPersonCity = value.Substring(0, 50); }
                else
                {
                    _GuardianPersonCity = value;
                    RaisePropertyChanged("GuardianPersonCity");
                }
            }
        }

        public string GuardianPersonState
        {
            get { return _GuardianPersonState; }
            set
            {
                if (value != null && value.Length > 2)
                { _GuardianPersonState = value.Substring(0, 2); }
                else
                {
                    _GuardianPersonState = value;
                    RaisePropertyChanged("GuardianPersonState");
                }
            }
        }

        public string GuardianPersonZipCode
        {
            get { return _GuardianPersonZipCode; }
            set
            {
                if (value != null && value.Length > 12)
                { _GuardianPersonZipCode = value.Substring(0, 12); }
                else
                {
                    _GuardianPersonZipCode = value;
                    RaisePropertyChanged("GuardianPersonZipCode");
                }
            }
        }

        public string GuardianPersonCounty
        {
            get { return _GuardianPersonCounty; }
            set
            {
                if (value != null && value.Length > 25)
                { _GuardianPersonCounty = value.Substring(0, 25); }
                else
                {
                    _GuardianPersonCounty = value;
                    RaisePropertyChanged("GuardianPersonCounty");
                }
            }
        }

        public string GuardianPersonRelationship
        {
            get { return _GuardianPersonRelationship; }
            set
            {
                if (value != null && value.Length > 5)
                { _GuardianPersonRelationship = value.Substring(0, 5); }
                else
                {
                    _GuardianPersonRelationship = value;
                    RaisePropertyChanged("GuardianPersonRelationship");
                }
            }
        }

        public DateTime DateEncounterSigned
        {
            get { return _DateEncounterSigned; }
            set
            {
                _DateEncounterSigned = value;
                RaisePropertyChanged("DateEncounterSigned");
            }
        }

        public string EncounterStatus
        {
            get { return _EncounterStatus; }
            set
            {
                if (value != null && value.Length > 5)
                { _EncounterStatus = value.Substring(0, 5); }
                else
                {
                    _EncounterStatus = value;
                    RaisePropertyChanged("EncounterStatus");
                }
            }
        }

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                _CreatedDate = value;
                RaisePropertyChanged("CreatedDate");
            }
        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                if (value != null && value.Length > 20)
                { _CreatedBy = value.Substring(0, 20); }
                else
                {
                    _CreatedBy = value;
                    RaisePropertyChanged("CreatedBy");
                }
            }
        }

        public DateTime BilledDate
        {
            get { return _BilledDate; }
            set
            {
                _BilledDate = value;
                RaisePropertyChanged("BilledDate");
            }
        }

        public DateTime PaidDate
        {
            get { return _PaidDate; }
            set
            {
                _PaidDate = value;
                RaisePropertyChanged("PaidDate");
            }
        }

        public string CheckNumber
        {
            get { return _CheckNumber; }
            set
            {
                if (value != null && value.Length > 20)
                { _CheckNumber = value.Substring(0, 20); }
                else
                {
                    _CheckNumber = value;
                    RaisePropertyChanged("CheckNumber");
                }
            }
        }

        public string AuthNumber
        {
            get { return _AuthNumber; }
            set
            {
                if (value != null && value.Length > 50)
                { _AuthNumber = value.Substring(0, 50); }
                else
                {
                    _AuthNumber = value;
                    RaisePropertyChanged("AuthNumber");
                }
            }
        }

        public int ProgressNoteID
        {
            get { return _ProgressNoteID; }
            set
            {
                _ProgressNoteID = value;
                RaisePropertyChanged("ProgressNoteID");
            }
        }

        public string NeedsFixingComment
        {
            get { return _NeedsFixingComment; }
            set
            {
                if (value != null && value.Length > 2147483647)
                { _NeedsFixingComment = value.Substring(0, 2147483647); }
                else
                {
                    _NeedsFixingComment = value;
                    RaisePropertyChanged("NeedsFixingComment");
                }
            }
        }

        public string Notation
        {
            get { return _Notation; }
            set
            {
                if (value != null && value.Length > 2147483647)
                { _Notation = value.Substring(0, 2147483647); }
                else
                {
                    _Notation = value;
                    RaisePropertyChanged("Notation");
                }
            }
        }

        public long BANoteID
        {
            get { return _BANoteID; }
            set
            {
                _BANoteID = value;
                RaisePropertyChanged("BANoteID");
            }
        }

        public string ACTID
        {
            get { return _ACTID; }
            set
            {
                if (value != null && value.Length > 20)
                { _ACTID = value.Substring(0, 20); }
                else
                {
                    _ACTID = value;
                    RaisePropertyChanged("ACTID");
                }
            }
        }

        public double ChargedAmount
        {
            get { return _ChargedAmount; }
            set
            {
                _ChargedAmount = value;
                RaisePropertyChanged("ChargedAmount");
            }
        }

        public double PaidAmount
        {
            get { return _PaidAmount; }
            set
            {
                _PaidAmount = value;
                RaisePropertyChanged("PaidAmount");
            }
        }

        public DateTime ConsumerBilledDate
        {
            get { return _ConsumerBilledDate; }
            set
            {
                _ConsumerBilledDate = value;
                RaisePropertyChanged("ConsumerBilledDate");
            }
        }

        public DateTime ConsumerPaidDate
        {
            get { return _ConsumerPaidDate; }
            set
            {
                _ConsumerPaidDate = value;
                RaisePropertyChanged("ConsumerPaidDate");
            }
        }

        public string ConsumerCheckNumber
        {
            get { return _ConsumerCheckNumber; }
            set
            {
                if (value != null && value.Length > 20)
                { _ConsumerCheckNumber = value.Substring(0, 20); }
                else
                {
                    _ConsumerCheckNumber = value;
                    RaisePropertyChanged("ConsumerCheckNumber");
                }
            }
        }

        public double ConsumerChargedAmount
        {
            get { return _ConsumerChargedAmount; }
            set
            {
                _ConsumerChargedAmount = value;
                RaisePropertyChanged("ConsumerChargedAmount");
            }
        }

        public double ConsumerPaidAmount
        {
            get { return _ConsumerPaidAmount; }
            set
            {
                _ConsumerPaidAmount = value;
                RaisePropertyChanged("ConsumerPaidAmount");
            }
        }

        public bool SUBMITTED
        {
            get { return _SUBMITTED; }
            set
            {
                _SUBMITTED = value;
                RaisePropertyChanged("SUBMITTED");
            }
        }

        public DateTime SUBMITTEDDATE
        {
            get { return _SUBMITTEDDATE; }
            set
            {
                _SUBMITTEDDATE = value;
                RaisePropertyChanged("SUBMITTEDDATE");
            }
        }

        public long ROLLUPID
        {
            get { return _ROLLUPID; }
            set
            {
                _ROLLUPID = value;
                RaisePropertyChanged("ROLLUPID");
            }
        }

        public int BCBAID
        {
            get { return _BCBAID; }
            set
            {
                _BCBAID = value;
                RaisePropertyChanged("BCBAID");
            }
        }


        #endregion

        #region Implement INotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor

        public tblMemberEncounters()
        {
            // Constructor code goes here.
            Initialize();
        }

        public tblMemberEncounters(string DSN)
        {
            // Constructor code goes here.
            Initialize();
            classDatabaseConnectionString = DSN;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _tblMemberEncountersID = 0;
            _SSN = "";
            _EncounterDate = Convert.ToDateTime(null);
            _EncounterStartTime = Convert.ToDateTime(null);
            _EncounterEndTime = Convert.ToDateTime(null);
            _TypeOfServiceDeliverySite = "";
            _ServiceDelivered = "";
            _IsGroupService = false;
            _IsIndividualService = false;
            _DeliverySiteAddress1 = "";
            _DeliverySiteAddress2 = "";
            _DeliverySiteAddress3 = "";
            _DeliverySiteCity = "";
            _DeliverySiteState = "";
            _DeliverySiteZipCode = "";
            _DeliverySiteCounty = "";
            _DeliverySitePhone = "";
            _IsGuardian = false;
            _IsResponsibleParty = false;
            _GuardianResponsiblePerson = "";
            _GuardianPersonAddress1 = "";
            _GuardianPersonAddress2 = "";
            _GuardianPersonAddress3 = "";
            _GuardianPersonCity = "";
            _GuardianPersonState = "";
            _GuardianPersonZipCode = "";
            _GuardianPersonCounty = "";
            _GuardianPersonRelationship = "";
            _DateEncounterSigned = Convert.ToDateTime(null);
            _EncounterStatus = "";
            _CreatedDate = Convert.ToDateTime(null);
            _CreatedBy = "";
            _BilledDate = Convert.ToDateTime(null);
            _PaidDate = Convert.ToDateTime(null);
            _CheckNumber = "";
            _AuthNumber = "";
            _ProgressNoteID = 0;
            _NeedsFixingComment = "";
            _Notation = "";
            _BANoteID = 0;
            _ACTID = "";
            _ChargedAmount = 0.0;
            _PaidAmount = 0.0;
            _ConsumerBilledDate = Convert.ToDateTime(null);
            _ConsumerPaidDate = Convert.ToDateTime(null);
            _ConsumerCheckNumber = "";
            _ConsumerChargedAmount = 0.0;
            _ConsumerPaidAmount = 0.0;
            _SUBMITTED = false;
            _SUBMITTEDDATE = Convert.ToDateTime(null);
            _ROLLUPID = 0;
            _BCBAID = 0;
        }

        public void CopyFields(SqlDataReader r)
        {
            try
            {
                if (!Convert.IsDBNull(r["tblMemberEncountersID"]))
                {
                    _tblMemberEncountersID = Convert.ToInt64(r["tblMemberEncountersID"]);
                }
                if (!Convert.IsDBNull(r["SSN"]))
                {
                    _SSN = r["SSN"] + "";
                }
                if (!Convert.IsDBNull(r["EncounterDate"]))
                {
                    _EncounterDate = Convert.ToDateTime(r["EncounterDate"]);
                }
                if (!Convert.IsDBNull(r["EncounterStartTime"]))
                {
                    _EncounterStartTime = Convert.ToDateTime(r["EncounterStartTime"]);
                }
                if (!Convert.IsDBNull(r["EncounterEndTime"]))
                {
                    _EncounterEndTime = Convert.ToDateTime(r["EncounterEndTime"]);
                }
                if (!Convert.IsDBNull(r["TypeOfServiceDeliverySite"]))
                {
                    _TypeOfServiceDeliverySite = r["TypeOfServiceDeliverySite"] + "";
                }
                if (!Convert.IsDBNull(r["ServiceDelivered"]))
                {
                    _ServiceDelivered = r["ServiceDelivered"] + "";
                }
                if (!Convert.IsDBNull(r["IsGroupService"]))
                {
                    _IsGroupService = Convert.ToBoolean(r["IsGroupService"]);
                }
                if (!Convert.IsDBNull(r["IsIndividualService"]))
                {
                    _IsIndividualService = Convert.ToBoolean(r["IsIndividualService"]);
                }
                if (!Convert.IsDBNull(r["DeliverySiteAddress1"]))
                {
                    _DeliverySiteAddress1 = r["DeliverySiteAddress1"] + "";
                }
                if (!Convert.IsDBNull(r["DeliverySiteAddress2"]))
                {
                    _DeliverySiteAddress2 = r["DeliverySiteAddress2"] + "";
                }
                if (!Convert.IsDBNull(r["DeliverySiteAddress3"]))
                {
                    _DeliverySiteAddress3 = r["DeliverySiteAddress3"] + "";
                }
                if (!Convert.IsDBNull(r["DeliverySiteCity"]))
                {
                    _DeliverySiteCity = r["DeliverySiteCity"] + "";
                }
                if (!Convert.IsDBNull(r["DeliverySiteState"]))
                {
                    _DeliverySiteState = r["DeliverySiteState"] + "";
                }
                if (!Convert.IsDBNull(r["DeliverySiteZipCode"]))
                {
                    _DeliverySiteZipCode = r["DeliverySiteZipCode"] + "";
                }
                if (!Convert.IsDBNull(r["DeliverySiteCounty"]))
                {
                    _DeliverySiteCounty = r["DeliverySiteCounty"] + "";
                }
                if (!Convert.IsDBNull(r["DeliverySitePhone"]))
                {
                    _DeliverySitePhone = r["DeliverySitePhone"] + "";
                }
                if (!Convert.IsDBNull(r["IsGuardian"]))
                {
                    _IsGuardian = Convert.ToBoolean(r["IsGuardian"]);
                }
                if (!Convert.IsDBNull(r["IsResponsibleParty"]))
                {
                    _IsResponsibleParty = Convert.ToBoolean(r["IsResponsibleParty"]);
                }
                if (!Convert.IsDBNull(r["GuardianResponsiblePerson"]))
                {
                    _GuardianResponsiblePerson = r["GuardianResponsiblePerson"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonAddress1"]))
                {
                    _GuardianPersonAddress1 = r["GuardianPersonAddress1"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonAddress2"]))
                {
                    _GuardianPersonAddress2 = r["GuardianPersonAddress2"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonAddress3"]))
                {
                    _GuardianPersonAddress3 = r["GuardianPersonAddress3"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonCity"]))
                {
                    _GuardianPersonCity = r["GuardianPersonCity"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonState"]))
                {
                    _GuardianPersonState = r["GuardianPersonState"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonZipCode"]))
                {
                    _GuardianPersonZipCode = r["GuardianPersonZipCode"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonCounty"]))
                {
                    _GuardianPersonCounty = r["GuardianPersonCounty"] + "";
                }
                if (!Convert.IsDBNull(r["GuardianPersonRelationship"]))
                {
                    _GuardianPersonRelationship = r["GuardianPersonRelationship"] + "";
                }
                if (!Convert.IsDBNull(r["DateEncounterSigned"]))
                {
                    _DateEncounterSigned = Convert.ToDateTime(r["DateEncounterSigned"]);
                }
                if (!Convert.IsDBNull(r["EncounterStatus"]))
                {
                    _EncounterStatus = r["EncounterStatus"] + "";
                }
                if (!Convert.IsDBNull(r["CreatedDate"]))
                {
                    _CreatedDate = Convert.ToDateTime(r["CreatedDate"]);
                }
                if (!Convert.IsDBNull(r["CreatedBy"]))
                {
                    _CreatedBy = r["CreatedBy"] + "";
                }
                if (!Convert.IsDBNull(r["BilledDate"]))
                {
                    _BilledDate = Convert.ToDateTime(r["BilledDate"]);
                }
                if (!Convert.IsDBNull(r["PaidDate"]))
                {
                    _PaidDate = Convert.ToDateTime(r["PaidDate"]);
                }
                if (!Convert.IsDBNull(r["CheckNumber"]))
                {
                    _CheckNumber = r["CheckNumber"] + "";
                }
                if (!Convert.IsDBNull(r["AuthNumber"]))
                {
                    _AuthNumber = r["AuthNumber"] + "";
                }
                if (!Convert.IsDBNull(r["ProgressNoteID"]))
                {
                    _ProgressNoteID = Convert.ToInt32(r["ProgressNoteID"]);
                }
                if (!Convert.IsDBNull(r["NeedsFixingComment"]))
                {
                    _NeedsFixingComment = r["NeedsFixingComment"] + "";
                }
                if (!Convert.IsDBNull(r["Notation"]))
                {
                    _Notation = r["Notation"] + "";
                }
                if (!Convert.IsDBNull(r["BANoteID"]))
                {
                    _BANoteID = Convert.ToInt64(r["BANoteID"]);
                }
                if (!Convert.IsDBNull(r["ACTID"]))
                {
                    _ACTID = r["ACTID"] + "";
                }
                if (!Convert.IsDBNull(r["ChargedAmount"]))
                {
                    _ChargedAmount = Convert.ToDouble(r["ChargedAmount"]);
                }
                if (!Convert.IsDBNull(r["PaidAmount"]))
                {
                    _PaidAmount = Convert.ToDouble(r["PaidAmount"]);
                }
                if (!Convert.IsDBNull(r["ConsumerBilledDate"]))
                {
                    _ConsumerBilledDate = Convert.ToDateTime(r["ConsumerBilledDate"]);
                }
                if (!Convert.IsDBNull(r["ConsumerPaidDate"]))
                {
                    _ConsumerPaidDate = Convert.ToDateTime(r["ConsumerPaidDate"]);
                }
                if (!Convert.IsDBNull(r["ConsumerCheckNumber"]))
                {
                    _ConsumerCheckNumber = r["ConsumerCheckNumber"] + "";
                }
                if (!Convert.IsDBNull(r["ConsumerChargedAmount"]))
                {
                    _ConsumerChargedAmount = Convert.ToDouble(r["ConsumerChargedAmount"]);
                }
                if (!Convert.IsDBNull(r["ConsumerPaidAmount"]))
                {
                    _ConsumerPaidAmount = Convert.ToDouble(r["ConsumerPaidAmount"]);
                }
                if (!Convert.IsDBNull(r["SUBMITTED"]))
                {
                    _SUBMITTED = Convert.ToBoolean(r["SUBMITTED"]);
                }
                if (!Convert.IsDBNull(r["SUBMITTEDDATE"]))
                {
                    _SUBMITTEDDATE = Convert.ToDateTime(r["SUBMITTEDDATE"]);
                }
                if (!Convert.IsDBNull(r["ROLLUPID"]))
                {
                    _ROLLUPID = Convert.ToInt64(r["ROLLUPID"]);
                }
                if (!Convert.IsDBNull(r["BCBAID"]))
                {
                    _BCBAID = Convert.ToInt32(r["BCBAID"]);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberEncounters.CopyFields " + ex.ToString()));
            }
        }

        public bool RecExists(System.Int64 idx)
        {
            bool Result = false;
            try
            {
                string sql = "Select count(*) from tblMemberEncounters WHERE tblMemberEncountersID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    if (r.GetInt32(0) > 0)
                    {
                        Result = true;
                    }
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberEncounters.RecExists " + ex.ToString()));
            }

            return Result;
        }

        public void Add()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SSN == null || this._SSN == "" || this._SSN == string.Empty)
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = this._SSN;
                }
                cmd.Parameters.Add("@EncounterDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EncounterDate);
                cmd.Parameters.Add("@EncounterStartTime", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EncounterStartTime);
                cmd.Parameters.Add("@EncounterEndTime", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EncounterEndTime);
                if (this._TypeOfServiceDeliverySite == null || this._TypeOfServiceDeliverySite == "" || this._TypeOfServiceDeliverySite == string.Empty)
                {
                    cmd.Parameters.Add("@TypeOfServiceDeliverySite", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@TypeOfServiceDeliverySite", System.Data.SqlDbType.VarChar).Value = this._TypeOfServiceDeliverySite;
                }
                if (this._ServiceDelivered == null || this._ServiceDelivered == "" || this._ServiceDelivered == string.Empty)
                {
                    cmd.Parameters.Add("@ServiceDelivered", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ServiceDelivered", System.Data.SqlDbType.VarChar).Value = this._ServiceDelivered;
                }
                cmd.Parameters.Add("@IsGroupService", System.Data.SqlDbType.Bit).Value = this._IsGroupService;
                cmd.Parameters.Add("@IsIndividualService", System.Data.SqlDbType.Bit).Value = this._IsIndividualService;
                if (this._DeliverySiteAddress1 == null || this._DeliverySiteAddress1 == "" || this._DeliverySiteAddress1 == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteAddress1", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteAddress1", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteAddress1;
                }
                if (this._DeliverySiteAddress2 == null || this._DeliverySiteAddress2 == "" || this._DeliverySiteAddress2 == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteAddress2", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteAddress2", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteAddress2;
                }
                if (this._DeliverySiteAddress3 == null || this._DeliverySiteAddress3 == "" || this._DeliverySiteAddress3 == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteAddress3", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteAddress3", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteAddress3;
                }
                if (this._DeliverySiteCity == null || this._DeliverySiteCity == "" || this._DeliverySiteCity == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteCity", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteCity", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteCity;
                }
                if (this._DeliverySiteState == null || this._DeliverySiteState == "" || this._DeliverySiteState == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteState", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteState", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteState;
                }
                if (this._DeliverySiteZipCode == null || this._DeliverySiteZipCode == "" || this._DeliverySiteZipCode == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteZipCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteZipCode", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteZipCode;
                }
                if (this._DeliverySiteCounty == null || this._DeliverySiteCounty == "" || this._DeliverySiteCounty == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteCounty", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteCounty", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteCounty;
                }
                if (this._DeliverySitePhone == null || this._DeliverySitePhone == "" || this._DeliverySitePhone == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySitePhone", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySitePhone", System.Data.SqlDbType.VarChar).Value = this._DeliverySitePhone;
                }
                cmd.Parameters.Add("@IsGuardian", System.Data.SqlDbType.Bit).Value = this._IsGuardian;
                cmd.Parameters.Add("@IsResponsibleParty", System.Data.SqlDbType.Bit).Value = this._IsResponsibleParty;
                if (this._GuardianResponsiblePerson == null || this._GuardianResponsiblePerson == "" || this._GuardianResponsiblePerson == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianResponsiblePerson", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianResponsiblePerson", System.Data.SqlDbType.VarChar).Value = this._GuardianResponsiblePerson;
                }
                if (this._GuardianPersonAddress1 == null || this._GuardianPersonAddress1 == "" || this._GuardianPersonAddress1 == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonAddress1", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonAddress1", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonAddress1;
                }
                if (this._GuardianPersonAddress2 == null || this._GuardianPersonAddress2 == "" || this._GuardianPersonAddress2 == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonAddress2", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonAddress2", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonAddress2;
                }
                if (this._GuardianPersonAddress3 == null || this._GuardianPersonAddress3 == "" || this._GuardianPersonAddress3 == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonAddress3", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonAddress3", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonAddress3;
                }
                if (this._GuardianPersonCity == null || this._GuardianPersonCity == "" || this._GuardianPersonCity == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonCity", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonCity", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonCity;
                }
                if (this._GuardianPersonState == null || this._GuardianPersonState == "" || this._GuardianPersonState == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonState", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonState", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonState;
                }
                if (this._GuardianPersonZipCode == null || this._GuardianPersonZipCode == "" || this._GuardianPersonZipCode == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonZipCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonZipCode", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonZipCode;
                }
                if (this._GuardianPersonCounty == null || this._GuardianPersonCounty == "" || this._GuardianPersonCounty == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonCounty", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonCounty", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonCounty;
                }
                if (this._GuardianPersonRelationship == null || this._GuardianPersonRelationship == "" || this._GuardianPersonRelationship == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonRelationship", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonRelationship", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonRelationship;
                }
                cmd.Parameters.Add("@DateEncounterSigned", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._DateEncounterSigned);
                if (this._EncounterStatus == null || this._EncounterStatus == "" || this._EncounterStatus == string.Empty)
                {
                    cmd.Parameters.Add("@EncounterStatus", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@EncounterStatus", System.Data.SqlDbType.VarChar).Value = this._EncounterStatus;
                }
                cmd.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._CreatedDate);
                if (this._CreatedBy == null || this._CreatedBy == "" || this._CreatedBy == string.Empty)
                {
                    cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.VarChar).Value = this._CreatedBy;
                }
                cmd.Parameters.Add("@BilledDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._BilledDate);
                cmd.Parameters.Add("@PaidDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._PaidDate);
                if (this._CheckNumber == null || this._CheckNumber == "" || this._CheckNumber == string.Empty)
                {
                    cmd.Parameters.Add("@CheckNumber", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CheckNumber", System.Data.SqlDbType.VarChar).Value = this._CheckNumber;
                }
                if (this._AuthNumber == null || this._AuthNumber == "" || this._AuthNumber == string.Empty)
                {
                    cmd.Parameters.Add("@AuthNumber", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@AuthNumber", System.Data.SqlDbType.VarChar).Value = this._AuthNumber;
                }
                cmd.Parameters.Add("@ProgressNoteID", System.Data.SqlDbType.Int).Value = this._ProgressNoteID;
                if (this._NeedsFixingComment == null || this._NeedsFixingComment == "" || this._NeedsFixingComment == string.Empty)
                {
                    cmd.Parameters.Add("@NeedsFixingComment", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@NeedsFixingComment", System.Data.SqlDbType.VarChar).Value = this._NeedsFixingComment;
                }
                if (this._Notation == null || this._Notation == "" || this._Notation == string.Empty)
                {
                    cmd.Parameters.Add("@Notation", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Notation", System.Data.SqlDbType.VarChar).Value = this._Notation;
                }
                cmd.Parameters.Add("@BANoteID", System.Data.SqlDbType.BigInt).Value = this._BANoteID;
                if (this._ACTID == null || this._ACTID == "" || this._ACTID == string.Empty)
                {
                    cmd.Parameters.Add("@ACTID", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ACTID", System.Data.SqlDbType.VarChar).Value = this._ACTID;
                }
                cmd.Parameters.Add("@ChargedAmount", System.Data.SqlDbType.Decimal).Value = this._ChargedAmount;
                cmd.Parameters.Add("@PaidAmount", System.Data.SqlDbType.Decimal).Value = this._PaidAmount;
                cmd.Parameters.Add("@ConsumerBilledDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._ConsumerBilledDate);
                cmd.Parameters.Add("@ConsumerPaidDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._ConsumerPaidDate);
                if (this._ConsumerCheckNumber == null || this._ConsumerCheckNumber == "" || this._ConsumerCheckNumber == string.Empty)
                {
                    cmd.Parameters.Add("@ConsumerCheckNumber", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ConsumerCheckNumber", System.Data.SqlDbType.VarChar).Value = this._ConsumerCheckNumber;
                }
                cmd.Parameters.Add("@ConsumerChargedAmount", System.Data.SqlDbType.Decimal).Value = this._ConsumerChargedAmount;
                cmd.Parameters.Add("@ConsumerPaidAmount", System.Data.SqlDbType.Decimal).Value = this._ConsumerPaidAmount;
                cmd.Parameters.Add("@SUBMITTED", System.Data.SqlDbType.Bit).Value = this._SUBMITTED;
                cmd.Parameters.Add("@SUBMITTEDDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SUBMITTEDDATE);
                cmd.Parameters.Add("@ROLLUPID", System.Data.SqlDbType.BigInt).Value = this._ROLLUPID;
                cmd.Parameters.Add("@BCBAID", System.Data.SqlDbType.Int).Value = this._BCBAID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (tblMemberEncountersID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _tblMemberEncountersID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberEncounters.Add " + ex.ToString()));
            }
        }

        public void Update()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SSN == null || this._SSN == "" || this._SSN == string.Empty)
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = this._SSN;
                }
                cmd.Parameters.Add("@EncounterDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EncounterDate);
                cmd.Parameters.Add("@EncounterStartTime", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EncounterStartTime);
                cmd.Parameters.Add("@EncounterEndTime", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EncounterEndTime);
                if (this._TypeOfServiceDeliverySite == null || this._TypeOfServiceDeliverySite == "" || this._TypeOfServiceDeliverySite == string.Empty)
                {
                    cmd.Parameters.Add("@TypeOfServiceDeliverySite", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@TypeOfServiceDeliverySite", System.Data.SqlDbType.VarChar).Value = this._TypeOfServiceDeliverySite;
                }
                if (this._ServiceDelivered == null || this._ServiceDelivered == "" || this._ServiceDelivered == string.Empty)
                {
                    cmd.Parameters.Add("@ServiceDelivered", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ServiceDelivered", System.Data.SqlDbType.VarChar).Value = this._ServiceDelivered;
                }
                cmd.Parameters.Add("@IsGroupService", System.Data.SqlDbType.Bit).Value = this._IsGroupService;
                cmd.Parameters.Add("@IsIndividualService", System.Data.SqlDbType.Bit).Value = this._IsIndividualService;
                if (this._DeliverySiteAddress1 == null || this._DeliverySiteAddress1 == "" || this._DeliverySiteAddress1 == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteAddress1", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteAddress1", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteAddress1;
                }
                if (this._DeliverySiteAddress2 == null || this._DeliverySiteAddress2 == "" || this._DeliverySiteAddress2 == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteAddress2", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteAddress2", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteAddress2;
                }
                if (this._DeliverySiteAddress3 == null || this._DeliverySiteAddress3 == "" || this._DeliverySiteAddress3 == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteAddress3", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteAddress3", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteAddress3;
                }
                if (this._DeliverySiteCity == null || this._DeliverySiteCity == "" || this._DeliverySiteCity == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteCity", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteCity", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteCity;
                }
                if (this._DeliverySiteState == null || this._DeliverySiteState == "" || this._DeliverySiteState == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteState", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteState", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteState;
                }
                if (this._DeliverySiteZipCode == null || this._DeliverySiteZipCode == "" || this._DeliverySiteZipCode == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteZipCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteZipCode", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteZipCode;
                }
                if (this._DeliverySiteCounty == null || this._DeliverySiteCounty == "" || this._DeliverySiteCounty == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySiteCounty", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySiteCounty", System.Data.SqlDbType.VarChar).Value = this._DeliverySiteCounty;
                }
                if (this._DeliverySitePhone == null || this._DeliverySitePhone == "" || this._DeliverySitePhone == string.Empty)
                {
                    cmd.Parameters.Add("@DeliverySitePhone", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DeliverySitePhone", System.Data.SqlDbType.VarChar).Value = this._DeliverySitePhone;
                }
                cmd.Parameters.Add("@IsGuardian", System.Data.SqlDbType.Bit).Value = this._IsGuardian;
                cmd.Parameters.Add("@IsResponsibleParty", System.Data.SqlDbType.Bit).Value = this._IsResponsibleParty;
                if (this._GuardianResponsiblePerson == null || this._GuardianResponsiblePerson == "" || this._GuardianResponsiblePerson == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianResponsiblePerson", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianResponsiblePerson", System.Data.SqlDbType.VarChar).Value = this._GuardianResponsiblePerson;
                }
                if (this._GuardianPersonAddress1 == null || this._GuardianPersonAddress1 == "" || this._GuardianPersonAddress1 == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonAddress1", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonAddress1", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonAddress1;
                }
                if (this._GuardianPersonAddress2 == null || this._GuardianPersonAddress2 == "" || this._GuardianPersonAddress2 == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonAddress2", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonAddress2", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonAddress2;
                }
                if (this._GuardianPersonAddress3 == null || this._GuardianPersonAddress3 == "" || this._GuardianPersonAddress3 == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonAddress3", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonAddress3", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonAddress3;
                }
                if (this._GuardianPersonCity == null || this._GuardianPersonCity == "" || this._GuardianPersonCity == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonCity", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonCity", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonCity;
                }
                if (this._GuardianPersonState == null || this._GuardianPersonState == "" || this._GuardianPersonState == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonState", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonState", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonState;
                }
                if (this._GuardianPersonZipCode == null || this._GuardianPersonZipCode == "" || this._GuardianPersonZipCode == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonZipCode", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonZipCode", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonZipCode;
                }
                if (this._GuardianPersonCounty == null || this._GuardianPersonCounty == "" || this._GuardianPersonCounty == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonCounty", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonCounty", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonCounty;
                }
                if (this._GuardianPersonRelationship == null || this._GuardianPersonRelationship == "" || this._GuardianPersonRelationship == string.Empty)
                {
                    cmd.Parameters.Add("@GuardianPersonRelationship", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@GuardianPersonRelationship", System.Data.SqlDbType.VarChar).Value = this._GuardianPersonRelationship;
                }
                cmd.Parameters.Add("@DateEncounterSigned", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._DateEncounterSigned);
                if (this._EncounterStatus == null || this._EncounterStatus == "" || this._EncounterStatus == string.Empty)
                {
                    cmd.Parameters.Add("@EncounterStatus", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@EncounterStatus", System.Data.SqlDbType.VarChar).Value = this._EncounterStatus;
                }
                cmd.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._CreatedDate);
                if (this._CreatedBy == null || this._CreatedBy == "" || this._CreatedBy == string.Empty)
                {
                    cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.VarChar).Value = this._CreatedBy;
                }
                cmd.Parameters.Add("@BilledDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._BilledDate);
                cmd.Parameters.Add("@PaidDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._PaidDate);
                if (this._CheckNumber == null || this._CheckNumber == "" || this._CheckNumber == string.Empty)
                {
                    cmd.Parameters.Add("@CheckNumber", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CheckNumber", System.Data.SqlDbType.VarChar).Value = this._CheckNumber;
                }
                if (this._AuthNumber == null || this._AuthNumber == "" || this._AuthNumber == string.Empty)
                {
                    cmd.Parameters.Add("@AuthNumber", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@AuthNumber", System.Data.SqlDbType.VarChar).Value = this._AuthNumber;
                }
                cmd.Parameters.Add("@ProgressNoteID", System.Data.SqlDbType.Int).Value = this._ProgressNoteID;
                if (this._NeedsFixingComment == null || this._NeedsFixingComment == "" || this._NeedsFixingComment == string.Empty)
                {
                    cmd.Parameters.Add("@NeedsFixingComment", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@NeedsFixingComment", System.Data.SqlDbType.VarChar).Value = this._NeedsFixingComment;
                }
                if (this._Notation == null || this._Notation == "" || this._Notation == string.Empty)
                {
                    cmd.Parameters.Add("@Notation", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Notation", System.Data.SqlDbType.VarChar).Value = this._Notation;
                }
                cmd.Parameters.Add("@BANoteID", System.Data.SqlDbType.BigInt).Value = this._BANoteID;
                if (this._ACTID == null || this._ACTID == "" || this._ACTID == string.Empty)
                {
                    cmd.Parameters.Add("@ACTID", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ACTID", System.Data.SqlDbType.VarChar).Value = this._ACTID;
                }
                cmd.Parameters.Add("@ChargedAmount", System.Data.SqlDbType.Decimal).Value = this._ChargedAmount;
                cmd.Parameters.Add("@PaidAmount", System.Data.SqlDbType.Decimal).Value = this._PaidAmount;
                cmd.Parameters.Add("@ConsumerBilledDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._ConsumerBilledDate);
                cmd.Parameters.Add("@ConsumerPaidDate", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._ConsumerPaidDate);
                if (this._ConsumerCheckNumber == null || this._ConsumerCheckNumber == "" || this._ConsumerCheckNumber == string.Empty)
                {
                    cmd.Parameters.Add("@ConsumerCheckNumber", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@ConsumerCheckNumber", System.Data.SqlDbType.VarChar).Value = this._ConsumerCheckNumber;
                }
                cmd.Parameters.Add("@ConsumerChargedAmount", System.Data.SqlDbType.Decimal).Value = this._ConsumerChargedAmount;
                cmd.Parameters.Add("@ConsumerPaidAmount", System.Data.SqlDbType.Decimal).Value = this._ConsumerPaidAmount;
                cmd.Parameters.Add("@SUBMITTED", System.Data.SqlDbType.Bit).Value = this._SUBMITTED;
                cmd.Parameters.Add("@SUBMITTEDDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SUBMITTEDDATE);
                cmd.Parameters.Add("@ROLLUPID", System.Data.SqlDbType.BigInt).Value = this._ROLLUPID;
                cmd.Parameters.Add("@BCBAID", System.Data.SqlDbType.Int).Value = this._BCBAID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (tblMemberEncountersID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _tblMemberEncountersID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberEncounters.Update " + ex.ToString()));
            }
        }

        public void Delete()
        {
            try
            {
                string sql = "Delete from tblMemberEncounters WHERE tblMemberEncountersID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = this._tblMemberEncountersID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberEncounters.Delete " + ex.ToString()));
            }
        }

        public void Read(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberEncounters WHERE tblMemberEncountersID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberEncounters.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberEncounters WHERE tblMemberEncountersID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblMemberEncounters");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberEncounters.ReadAsDataSet " + ex.ToString()));
            }
        }

        #endregion

        #region Private Methods

        private string GetParameterSQL()
        {
            string sql = "";
            if (_tblMemberEncountersID < 1)
            {
                sql = "INSERT INTO tblMemberEncounters";
                sql += "(";
                sql += "[SSN], [EncounterDate], [EncounterStartTime], [EncounterEndTime], [TypeOfServiceDeliverySite],";
                sql += "[ServiceDelivered], [IsGroupService], [IsIndividualService], [DeliverySiteAddress1],";
                sql += "[DeliverySiteAddress2], [DeliverySiteAddress3], [DeliverySiteCity], [DeliverySiteState],";
                sql += "[DeliverySiteZipCode], [DeliverySiteCounty], [DeliverySitePhone], [IsGuardian],";
                sql += "[IsResponsibleParty], [GuardianResponsiblePerson], [GuardianPersonAddress1],";
                sql += "[GuardianPersonAddress2], [GuardianPersonAddress3], [GuardianPersonCity],";
                sql += "[GuardianPersonState], [GuardianPersonZipCode], [GuardianPersonCounty], [GuardianPersonRelationship],";
                sql += "[DateEncounterSigned], [EncounterStatus], [CreatedDate], [CreatedBy], [BilledDate],";
                sql += "[PaidDate], [CheckNumber], [AuthNumber], [ProgressNoteID], [NeedsFixingComment],";
                sql += "[Notation], [BANoteID], [ACTID], [ChargedAmount], [PaidAmount], [ConsumerBilledDate],";
                sql += "[ConsumerPaidDate], [ConsumerCheckNumber], [ConsumerChargedAmount], [ConsumerPaidAmount],";
                sql += "[SUBMITTED], [SUBMITTEDDATE], [ROLLUPID], [BCBAID])";
                sql += " VALUES (";
                sql += "@SSN,@EncounterDate,@EncounterStartTime,@EncounterEndTime,@TypeOfServiceDeliverySite,";
                sql += "@ServiceDelivered,@IsGroupService,@IsIndividualService,@DeliverySiteAddress1,";
                sql += "@DeliverySiteAddress2,@DeliverySiteAddress3,@DeliverySiteCity,@DeliverySiteState,";
                sql += "@DeliverySiteZipCode,@DeliverySiteCounty,@DeliverySitePhone,@IsGuardian,";
                sql += "@IsResponsibleParty,@GuardianResponsiblePerson,@GuardianPersonAddress1,@GuardianPersonAddress2,";
                sql += "@GuardianPersonAddress3,@GuardianPersonCity,@GuardianPersonState,@GuardianPersonZipCode,";
                sql += "@GuardianPersonCounty,@GuardianPersonRelationship,@DateEncounterSigned,@EncounterStatus,";
                sql += "@CreatedDate,@CreatedBy,@BilledDate,@PaidDate,@CheckNumber,@AuthNumber,@ProgressNoteID,";
                sql += "@NeedsFixingComment,@Notation,@BANoteID,@ACTID,@ChargedAmount,@PaidAmount,";
                sql += "@ConsumerBilledDate,@ConsumerPaidDate,@ConsumerCheckNumber,@ConsumerChargedAmount,";
                sql += "@ConsumerPaidAmount,@SUBMITTED,@SUBMITTEDDATE,@ROLLUPID,@BCBAID)";
            }
            else
            {
                sql = "UPDATE tblMemberEncounters SET ";
                sql += "[SSN] = @SSN, [EncounterDate] = @EncounterDate, [EncounterStartTime] = @EncounterStartTime,";
                sql += "[EncounterEndTime] = @EncounterEndTime, [TypeOfServiceDeliverySite] = @TypeOfServiceDeliverySite,";
                sql += "[ServiceDelivered] = @ServiceDelivered, [IsGroupService] = @IsGroupService,";
                sql += "[IsIndividualService] = @IsIndividualService, [DeliverySiteAddress1] = @DeliverySiteAddress1,";
                sql += "[DeliverySiteAddress2] = @DeliverySiteAddress2, [DeliverySiteAddress3] = @DeliverySiteAddress3,";
                sql += "[DeliverySiteCity] = @DeliverySiteCity, [DeliverySiteState] = @DeliverySiteState,";
                sql += "[DeliverySiteZipCode] = @DeliverySiteZipCode, [DeliverySiteCounty] = @DeliverySiteCounty,";
                sql += "[DeliverySitePhone] = @DeliverySitePhone, [IsGuardian] = @IsGuardian, [IsResponsibleParty] = @IsResponsibleParty,";
                sql += "[GuardianResponsiblePerson] = @GuardianResponsiblePerson, [GuardianPersonAddress1] = @GuardianPersonAddress1,";
                sql += "[GuardianPersonAddress2] = @GuardianPersonAddress2, [GuardianPersonAddress3] = @GuardianPersonAddress3,";
                sql += "[GuardianPersonCity] = @GuardianPersonCity, [GuardianPersonState] = @GuardianPersonState,";
                sql += "[GuardianPersonZipCode] = @GuardianPersonZipCode, [GuardianPersonCounty] = @GuardianPersonCounty,";
                sql += "[GuardianPersonRelationship] = @GuardianPersonRelationship, [DateEncounterSigned] = @DateEncounterSigned,";
                sql += "[EncounterStatus] = @EncounterStatus, [CreatedDate] = @CreatedDate, [CreatedBy] = @CreatedBy,";
                sql += "[BilledDate] = @BilledDate, [PaidDate] = @PaidDate, [CheckNumber] = @CheckNumber,";
                sql += "[AuthNumber] = @AuthNumber, [ProgressNoteID] = @ProgressNoteID, [NeedsFixingComment] = @NeedsFixingComment,";
                sql += "[Notation] = @Notation, [BANoteID] = @BANoteID, [ACTID] = @ACTID, [ChargedAmount] = @ChargedAmount,";
                sql += "[PaidAmount] = @PaidAmount, [ConsumerBilledDate] = @ConsumerBilledDate, [ConsumerPaidDate] = @ConsumerPaidDate,";
                sql += "[ConsumerCheckNumber] = @ConsumerCheckNumber, [ConsumerChargedAmount] = @ConsumerChargedAmount,";
                sql += "[ConsumerPaidAmount] = @ConsumerPaidAmount, [SUBMITTED] = @SUBMITTED, [SUBMITTEDDATE] = @SUBMITTEDDATE,";
                sql += "[ROLLUPID] = @ROLLUPID, [BCBAID] = @BCBAID";
                sql += " WHERE tblMemberEncountersID = " + _tblMemberEncountersID.ToString();
            }
            return sql;
        }

        private object getDateOrNull(DateTime d)
        {
            if (d == Convert.ToDateTime(null))
            {
                return DBNull.Value;
            }
            else
            {
                return d;
            }
        }
        #endregion
    }

    public partial class tblMemberMedications : INotifyPropertyChanged
    {

        #region Declarations
        string _classDatabaseConnectionString = "";
        string _bulkinsertPath = "";

        SqlConnection _cn = new SqlConnection();
        SqlCommand _cmd = new SqlCommand();

        // Backing Variables for Properties
        long _tmmID = 0;
        string _SSN = "";
        long _MEDID = 0;
        string _DOSAGE = "";
        string _FREQID = "";
        DateTime _SDATE = Convert.ToDateTime(null);
        DateTime _EDATE = Convert.ToDateTime(null);
        string _COMMENT = "";
        string _CREATEUSER = "";
        DateTime _CREATEDATE = Convert.ToDateTime(null);

        #endregion

        #region Properties

        public string classDatabaseConnectionString
        {
            get { return _classDatabaseConnectionString; }
            set { _classDatabaseConnectionString = value; }
        }

        public string bulkinsertPath
        {
            get { return _bulkinsertPath; }
            set { _bulkinsertPath = value; }
        }

        public long tmmID
        {
            get { return _tmmID; }
            set
            {
                _tmmID = value;
                RaisePropertyChanged("tmmID");
            }
        }

        public string SSN
        {
            get { return _SSN; }
            set
            {
                if (value != null && value.Length > 20)
                { _SSN = value.Substring(0, 20); }
                else
                {
                    _SSN = value;
                    RaisePropertyChanged("SSN");
                }
            }
        }

        public long MEDID
        {
            get { return _MEDID; }
            set
            {
                _MEDID = value;
                RaisePropertyChanged("MEDID");
            }
        }

        public string DOSAGE
        {
            get { return _DOSAGE; }
            set
            {
                if (value != null && value.Length > 20)
                { _DOSAGE = value.Substring(0, 20); }
                else
                {
                    _DOSAGE = value;
                    RaisePropertyChanged("DOSAGE");
                }
            }
        }

        public string FREQID
        {
            get { return _FREQID; }
            set
            {
                if (value != null && value.Length > 5)
                { _FREQID = value.Substring(0, 5); }
                else
                {
                    _FREQID = value;
                    RaisePropertyChanged("FREQID");
                }
            }
        }

        public DateTime SDATE
        {
            get { return _SDATE; }
            set
            {
                _SDATE = value;
                RaisePropertyChanged("SDATE");
            }
        }

        public DateTime EDATE
        {
            get { return _EDATE; }
            set
            {
                _EDATE = value;
                RaisePropertyChanged("EDATE");
            }
        }

        public string COMMENT
        {
            get { return _COMMENT; }
            set
            {
                if (value != null && value.Length > 2147483647)
                { _COMMENT = value.Substring(0, 2147483647); }
                else
                {
                    _COMMENT = value;
                    RaisePropertyChanged("COMMENT");
                }
            }
        }

        public string CREATEUSER
        {
            get { return _CREATEUSER; }
            set
            {
                if (value != null && value.Length > 20)
                { _CREATEUSER = value.Substring(0, 20); }
                else
                {
                    _CREATEUSER = value;
                    RaisePropertyChanged("CREATEUSER");
                }
            }
        }

        public DateTime CREATEDATE
        {
            get { return _CREATEDATE; }
            set
            {
                _CREATEDATE = value;
                RaisePropertyChanged("CREATEDATE");
            }
        }


        #endregion

        #region Implement INotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor

        public tblMemberMedications()
        {
            // Constructor code goes here.
            Initialize();
        }

        public tblMemberMedications(string DSN)
        {
            // Constructor code goes here.
            Initialize();
            classDatabaseConnectionString = DSN;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _tmmID = 0;
            _SSN = "";
            _MEDID = 0;
            _DOSAGE = "";
            _FREQID = "";
            _SDATE = Convert.ToDateTime(null);
            _EDATE = Convert.ToDateTime(null);
            _COMMENT = "";
            _CREATEUSER = "";
            _CREATEDATE = Convert.ToDateTime(null);
        }

        public void CopyFields(SqlDataReader r)
        {
            try
            {
                if (!Convert.IsDBNull(r["tmmID"]))
                {
                    _tmmID = Convert.ToInt64(r["tmmID"]);
                }
                if (!Convert.IsDBNull(r["SSN"]))
                {
                    _SSN = r["SSN"] + "";
                }
                if (!Convert.IsDBNull(r["MEDID"]))
                {
                    _MEDID = Convert.ToInt64(r["MEDID"]);
                }
                if (!Convert.IsDBNull(r["DOSAGE"]))
                {
                    _DOSAGE = r["DOSAGE"] + "";
                }
                if (!Convert.IsDBNull(r["FREQID"]))
                {
                    _FREQID = r["FREQID"] + "";
                }
                if (!Convert.IsDBNull(r["SDATE"]))
                {
                    _SDATE = Convert.ToDateTime(r["SDATE"]);
                }
                if (!Convert.IsDBNull(r["EDATE"]))
                {
                    _EDATE = Convert.ToDateTime(r["EDATE"]);
                }
                if (!Convert.IsDBNull(r["COMMENT"]))
                {
                    _COMMENT = r["COMMENT"] + "";
                }
                if (!Convert.IsDBNull(r["CREATEUSER"]))
                {
                    _CREATEUSER = r["CREATEUSER"] + "";
                }
                if (!Convert.IsDBNull(r["CREATEDATE"]))
                {
                    _CREATEDATE = Convert.ToDateTime(r["CREATEDATE"]);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberMedications.CopyFields " + ex.ToString()));
            }
        }

        public bool RecExists(System.Int64 idx)
        {
            bool Result = false;
            try
            {
                string sql = "Select count(*) from tblMemberMedications WHERE tmmID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    if (r.GetInt32(0) > 0)
                    {
                        Result = true;
                    }
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberMedications.RecExists " + ex.ToString()));
            }

            return Result;
        }

        public void Add()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SSN == null || this._SSN == "" || this._SSN == string.Empty)
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = this._SSN;
                }
                cmd.Parameters.Add("@MEDID", System.Data.SqlDbType.BigInt).Value = this._MEDID;
                if (this._DOSAGE == null || this._DOSAGE == "" || this._DOSAGE == string.Empty)
                {
                    cmd.Parameters.Add("@DOSAGE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DOSAGE", System.Data.SqlDbType.VarChar).Value = this._DOSAGE;
                }
                if (this._FREQID == null || this._FREQID == "" || this._FREQID == string.Empty)
                {
                    cmd.Parameters.Add("@FREQID", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FREQID", System.Data.SqlDbType.VarChar).Value = this._FREQID;
                }
                cmd.Parameters.Add("@SDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SDATE);
                cmd.Parameters.Add("@EDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EDATE);
                if (this._COMMENT == null || this._COMMENT == "" || this._COMMENT == string.Empty)
                {
                    cmd.Parameters.Add("@COMMENT", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@COMMENT", System.Data.SqlDbType.VarChar).Value = this._COMMENT;
                }
                if (this._CREATEUSER == null || this._CREATEUSER == "" || this._CREATEUSER == string.Empty)
                {
                    cmd.Parameters.Add("@CREATEUSER", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CREATEUSER", System.Data.SqlDbType.VarChar).Value = this._CREATEUSER;
                }
                cmd.Parameters.Add("@CREATEDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._CREATEDATE);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (tmmID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _tmmID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberMedications.Add " + ex.ToString()));
            }
        }

        public void Update()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._SSN == null || this._SSN == "" || this._SSN == string.Empty)
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@SSN", System.Data.SqlDbType.VarChar).Value = this._SSN;
                }
                cmd.Parameters.Add("@MEDID", System.Data.SqlDbType.BigInt).Value = this._MEDID;
                if (this._DOSAGE == null || this._DOSAGE == "" || this._DOSAGE == string.Empty)
                {
                    cmd.Parameters.Add("@DOSAGE", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@DOSAGE", System.Data.SqlDbType.VarChar).Value = this._DOSAGE;
                }
                if (this._FREQID == null || this._FREQID == "" || this._FREQID == string.Empty)
                {
                    cmd.Parameters.Add("@FREQID", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FREQID", System.Data.SqlDbType.VarChar).Value = this._FREQID;
                }
                cmd.Parameters.Add("@SDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._SDATE);
                cmd.Parameters.Add("@EDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._EDATE);
                if (this._COMMENT == null || this._COMMENT == "" || this._COMMENT == string.Empty)
                {
                    cmd.Parameters.Add("@COMMENT", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@COMMENT", System.Data.SqlDbType.VarChar).Value = this._COMMENT;
                }
                if (this._CREATEUSER == null || this._CREATEUSER == "" || this._CREATEUSER == string.Empty)
                {
                    cmd.Parameters.Add("@CREATEUSER", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@CREATEUSER", System.Data.SqlDbType.VarChar).Value = this._CREATEUSER;
                }
                cmd.Parameters.Add("@CREATEDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._CREATEDATE);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (tmmID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _tmmID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberMedications.Update " + ex.ToString()));
            }
        }

        public void Delete()
        {
            try
            {
                string sql = "Delete from tblMemberMedications WHERE tmmID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = this._tmmID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberMedications.Delete " + ex.ToString()));
            }
        }

        public void Read(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberMedications WHERE tmmID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberMedications.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberMedications WHERE tmmID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblMemberMedications");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberMedications.ReadAsDataSet " + ex.ToString()));
            }
        }

        #endregion

        #region Private Methods

        private string GetParameterSQL()
        {
            string sql = "";
            if (_tmmID < 1)
            {
                sql = "INSERT INTO tblMemberMedications";
                sql += "(";
                sql += "[SSN], [MEDID], [DOSAGE], [FREQID], [SDATE], [EDATE], [COMMENT], [CREATEUSER],";
                sql += "[CREATEDATE])";
                sql += " VALUES (";
                sql += "@SSN,@MEDID,@DOSAGE,@FREQID,@SDATE,@EDATE,@COMMENT,@CREATEUSER,@CREATEDATE";
                sql += ") ";
            }
            else
            {
                sql = "UPDATE tblMemberMedications SET ";
                sql += "[SSN] = @SSN, [MEDID] = @MEDID, [DOSAGE] = @DOSAGE, [FREQID] = @FREQID, [SDATE] = @SDATE,";
                sql += "[EDATE] = @EDATE, [COMMENT] = @COMMENT, [CREATEUSER] = @CREATEUSER, [CREATEDATE] = @CREATEDATE";
                sql += "";
                sql += " WHERE tmmID = " + _tmmID.ToString();
            }
            return sql;
        }

        private object getDateOrNull(DateTime d)
        {
            if (d == Convert.ToDateTime(null))
            {
                return DBNull.Value;
            }
            else
            {
                return d;
            }
        }
        #endregion
    }

    public partial class tblMemberAllergies : INotifyPropertyChanged
    {

        #region Declarations
        string _classDatabaseConnectionString = "";
        string _bulkinsertPath = "";

        SqlConnection _cn = new SqlConnection();
        SqlCommand _cmd = new SqlCommand();

        // Backing Variables for Properties
        long _maID = 0;
        string _maSSN = "";
        string _maALERGICTO = "";
        string _maALLERGICREACTION = "";
        string _maCOMMENT = "";
        string _maAUTHOR = "";
        DateTime _maCREATEDATE = Convert.ToDateTime(null);
        string _maHIDDEN = "";

        #endregion

        #region Properties

        public string classDatabaseConnectionString
        {
            get { return _classDatabaseConnectionString; }
            set { _classDatabaseConnectionString = value; }
        }

        public string bulkinsertPath
        {
            get { return _bulkinsertPath; }
            set { _bulkinsertPath = value; }
        }

        public long maID
        {
            get { return _maID; }
            set
            {
                _maID = value;
                RaisePropertyChanged("maID");
            }
        }

        public string maSSN
        {
            get { return _maSSN; }
            set
            {
                if (value != null && value.Length > 20)
                { _maSSN = value.Substring(0, 20); }
                else
                {
                    _maSSN = value;
                    RaisePropertyChanged("maSSN");
                }
            }
        }

        public string maALERGICTO
        {
            get { return _maALERGICTO; }
            set
            {
                if (value != null && value.Length > 100)
                { _maALERGICTO = value.Substring(0, 100); }
                else
                {
                    _maALERGICTO = value;
                    RaisePropertyChanged("maALERGICTO");
                }
            }
        }

        public string maALLERGICREACTION
        {
            get { return _maALLERGICREACTION; }
            set
            {
                if (value != null && value.Length > 1000)
                { _maALLERGICREACTION = value.Substring(0, 1000); }
                else
                {
                    _maALLERGICREACTION = value;
                    RaisePropertyChanged("maALLERGICREACTION");
                }
            }
        }

        public string maCOMMENT
        {
            get { return _maCOMMENT; }
            set
            {
                if (value != null && value.Length > 1000)
                { _maCOMMENT = value.Substring(0, 1000); }
                else
                {
                    _maCOMMENT = value;
                    RaisePropertyChanged("maCOMMENT");
                }
            }
        }

        public string maAUTHOR
        {
            get { return _maAUTHOR; }
            set
            {
                if (value != null && value.Length > 20)
                { _maAUTHOR = value.Substring(0, 20); }
                else
                {
                    _maAUTHOR = value;
                    RaisePropertyChanged("maAUTHOR");
                }
            }
        }

        public DateTime maCREATEDATE
        {
            get { return _maCREATEDATE; }
            set
            {
                _maCREATEDATE = value;
                RaisePropertyChanged("maCREATEDATE");
            }
        }

        public string maHIDDEN
        {
            get { return _maHIDDEN; }
            set
            {
                if (value != null && value.Length > 1)
                { _maHIDDEN = value.Substring(0, 1); }
                else
                {
                    _maHIDDEN = value;
                    RaisePropertyChanged("maHIDDEN");
                }
            }
        }


        #endregion

        #region Implement INotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor

        public tblMemberAllergies()
        {
            // Constructor code goes here.
            Initialize();
        }

        public tblMemberAllergies(string DSN)
        {
            // Constructor code goes here.
            Initialize();
            classDatabaseConnectionString = DSN;
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _maID = 0;
            _maSSN = "";
            _maALERGICTO = "";
            _maALLERGICREACTION = "";
            _maCOMMENT = "";
            _maAUTHOR = "";
            _maCREATEDATE = Convert.ToDateTime(null);
            _maHIDDEN = "";
        }

        public void CopyFields(SqlDataReader r)
        {
            try
            {
                if (!Convert.IsDBNull(r["maID"]))
                {
                    _maID = Convert.ToInt64(r["maID"]);
                }
                if (!Convert.IsDBNull(r["maSSN"]))
                {
                    _maSSN = r["maSSN"] + "";
                }
                if (!Convert.IsDBNull(r["maALERGICTO"]))
                {
                    _maALERGICTO = r["maALERGICTO"] + "";
                }
                if (!Convert.IsDBNull(r["maALLERGICREACTION"]))
                {
                    _maALLERGICREACTION = r["maALLERGICREACTION"] + "";
                }
                if (!Convert.IsDBNull(r["maCOMMENT"]))
                {
                    _maCOMMENT = r["maCOMMENT"] + "";
                }
                if (!Convert.IsDBNull(r["maAUTHOR"]))
                {
                    _maAUTHOR = r["maAUTHOR"] + "";
                }
                if (!Convert.IsDBNull(r["maCREATEDATE"]))
                {
                    _maCREATEDATE = Convert.ToDateTime(r["maCREATEDATE"]);
                }
                if (!Convert.IsDBNull(r["maHIDDEN"]))
                {
                    _maHIDDEN = r["maHIDDEN"] + "";
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAllergies.CopyFields " + ex.ToString()));
            }
        }

        public bool RecExists(System.Int64 idx)
        {
            bool Result = false;
            try
            {
                string sql = "Select count(*) from tblMemberAllergies WHERE maID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    if (r.GetInt32(0) > 0)
                    {
                        Result = true;
                    }
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAllergies.RecExists " + ex.ToString()));
            }

            return Result;
        }

        public void Add()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._maSSN == null || this._maSSN == "" || this._maSSN == string.Empty)
                {
                    cmd.Parameters.Add("@maSSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maSSN", System.Data.SqlDbType.VarChar).Value = this._maSSN;
                }
                if (this._maALERGICTO == null || this._maALERGICTO == "" || this._maALERGICTO == string.Empty)
                {
                    cmd.Parameters.Add("@maALERGICTO", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maALERGICTO", System.Data.SqlDbType.VarChar).Value = this._maALERGICTO;
                }
                if (this._maALLERGICREACTION == null || this._maALLERGICREACTION == "" || this._maALLERGICREACTION == string.Empty)
                {
                    cmd.Parameters.Add("@maALLERGICREACTION", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maALLERGICREACTION", System.Data.SqlDbType.VarChar).Value = this._maALLERGICREACTION;
                }
                if (this._maCOMMENT == null || this._maCOMMENT == "" || this._maCOMMENT == string.Empty)
                {
                    cmd.Parameters.Add("@maCOMMENT", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maCOMMENT", System.Data.SqlDbType.VarChar).Value = this._maCOMMENT;
                }
                if (this._maAUTHOR == null || this._maAUTHOR == "" || this._maAUTHOR == string.Empty)
                {
                    cmd.Parameters.Add("@maAUTHOR", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maAUTHOR", System.Data.SqlDbType.VarChar).Value = this._maAUTHOR;
                }
                cmd.Parameters.Add("@maCREATEDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._maCREATEDATE);
                if (this._maHIDDEN == null || this._maHIDDEN == "" || this._maHIDDEN == string.Empty)
                {
                    cmd.Parameters.Add("@maHIDDEN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maHIDDEN", System.Data.SqlDbType.VarChar).Value = this._maHIDDEN;
                }
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (maID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _maID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAllergies.Add " + ex.ToString()));
            }
        }

        public void Update()
        {
            try
            {
                string sql = GetParameterSQL();
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (this._maSSN == null || this._maSSN == "" || this._maSSN == string.Empty)
                {
                    cmd.Parameters.Add("@maSSN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maSSN", System.Data.SqlDbType.VarChar).Value = this._maSSN;
                }
                if (this._maALERGICTO == null || this._maALERGICTO == "" || this._maALERGICTO == string.Empty)
                {
                    cmd.Parameters.Add("@maALERGICTO", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maALERGICTO", System.Data.SqlDbType.VarChar).Value = this._maALERGICTO;
                }
                if (this._maALLERGICREACTION == null || this._maALLERGICREACTION == "" || this._maALLERGICREACTION == string.Empty)
                {
                    cmd.Parameters.Add("@maALLERGICREACTION", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maALLERGICREACTION", System.Data.SqlDbType.VarChar).Value = this._maALLERGICREACTION;
                }
                if (this._maCOMMENT == null || this._maCOMMENT == "" || this._maCOMMENT == string.Empty)
                {
                    cmd.Parameters.Add("@maCOMMENT", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maCOMMENT", System.Data.SqlDbType.VarChar).Value = this._maCOMMENT;
                }
                if (this._maAUTHOR == null || this._maAUTHOR == "" || this._maAUTHOR == string.Empty)
                {
                    cmd.Parameters.Add("@maAUTHOR", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maAUTHOR", System.Data.SqlDbType.VarChar).Value = this._maAUTHOR;
                }
                cmd.Parameters.Add("@maCREATEDATE", System.Data.SqlDbType.DateTime).Value = getDateOrNull(this._maCREATEDATE);
                if (this._maHIDDEN == null || this._maHIDDEN == "" || this._maHIDDEN == string.Empty)
                {
                    cmd.Parameters.Add("@maHIDDEN", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@maHIDDEN", System.Data.SqlDbType.VarChar).Value = this._maHIDDEN;
                }
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                if (maID < 1)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT @@IDENTITY", cn);
                    System.Int64 ii = Convert.ToInt64(cmd2.ExecuteScalar());
                    cmd2.Cancel();
                    cmd2.Dispose();
                    _maID = ii;
                }
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAllergies.Update " + ex.ToString()));
            }
        }

        public void Delete()
        {
            try
            {
                string sql = "Delete from tblMemberAllergies WHERE maID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = this._maID;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAllergies.Delete " + ex.ToString()));
            }
        }

        public void Read(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberAllergies WHERE maID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    this.CopyFields(r);
                }
                r.Close();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAllergies.Read " + ex.ToString()));
            }
        }

        public DataSet ReadAsDataSet(System.Int64 idx)
        {
            try
            {
                string sql = "Select * from tblMemberAllergies WHERE maID = @ID";
                SqlConnection cn = new SqlConnection(_classDatabaseConnectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = idx;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblMemberAllergies");
                da.Dispose();
                cmd.Cancel();
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (new Exception("tblMemberAllergies.ReadAsDataSet " + ex.ToString()));
            }
        }

        #endregion

        #region Private Methods

        private string GetParameterSQL()
        {
            string sql = "";
            if (_maID < 1)
            {
                sql = "INSERT INTO tblMemberAllergies";
                sql += "(";
                sql += "[maSSN], [maALERGICTO], [maALLERGICREACTION], [maCOMMENT], [maAUTHOR], [maCREATEDATE],";
                sql += "[maHIDDEN])";
                sql += " VALUES (";
                sql += "@maSSN,@maALERGICTO,@maALLERGICREACTION,@maCOMMENT,@maAUTHOR,@maCREATEDATE,";
                sql += "@maHIDDEN)";
            }
            else
            {
                sql = "UPDATE tblMemberAllergies SET ";
                sql += "[maSSN] = @maSSN, [maALERGICTO] = @maALERGICTO, [maALLERGICREACTION] = @maALLERGICREACTION,";
                sql += "[maCOMMENT] = @maCOMMENT, [maAUTHOR] = @maAUTHOR, [maCREATEDATE] = @maCREATEDATE,";
                sql += "[maHIDDEN] = @maHIDDEN";
                sql += " WHERE maID = " + _maID.ToString();
            }
            return sql;
        }

        private object getDateOrNull(DateTime d)
        {
            if (d == Convert.ToDateTime(null))
            {
                return DBNull.Value;
            }
            else
            {
                return d;
            }
        }
        #endregion
    }



}