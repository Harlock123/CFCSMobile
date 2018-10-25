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


}