using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Added by me
using System.Data;
using System.Data.SqlClient;

namespace Medical_Practice
{
    class Patient
    {
        //Private variables
        private int _PatientId;
        private string _LastName;
        private string _FirstName;
        private string _Notes;
        private string _Address;
        private string _MedicareNum;
        private string _HomePhone;
        private string _Mobile;
        private string _Suburb;
        private string _PostCode;
        private string _State;
        private SQLHelper _DB;

        public int PatientId
        {
            get
            {
                return _PatientId;
            }
            set
            {
                _PatientId = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }

        public string LastName {
            get {
                return _LastName;
            }
            set {
                _LastName = value;
            }
        }

        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }

        public string Suburb
        {
            get
            {
                return _Suburb;
            }
            set
            {
                _Suburb = value;
            }
        }
        public string PostCode
        {
            get
            {
                return _PostCode;
            }
            set
            {
                _PostCode = value;
            }
        }

        public string State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }

        public string HomePhone
        {
            get
            {
                return _HomePhone;
            }
            set
            {
                _HomePhone = value;
            }
        }
        public string Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                _Mobile = value;
            }
        }

        public string MedicareNum
        {
            get
            {
                return _MedicareNum;
            }
            set
            {
                _MedicareNum = value;
            }
        }

        public string Notes {
            get {
                return _Notes;
            }
            set {
                _Notes = value;
            }
        }
                       

        //Constructor
        public Patient()
        {
            //Creating an Instance of SQL Helper Class
            //Enables access to methods and functions of SQLHelper Class
            _DB = new SQLHelper();
        }

        //Create Patient from Data
        public Patient(DataRow dr)
        {
            this.PatientId = (int)dr["PatientId"];
            this.HomePhone = dr["HomePhone"].ToString();
            this.Mobile = dr["Mobile"].ToString();
            this.MedicareNum = dr["MedicareNum"].ToString();
            this.FirstName = dr["FirstName"].ToString();
            this.LastName = dr["LastName"].ToString();
            this.Address = dr["Address"].ToString();
            this.Notes = dr["Notes"].ToString();
            this.Suburb = dr["Suburb"].ToString();
            this.PostCode = dr["PostCode"].ToString();
            this.State = dr["State"].ToString();

            _DB = new SQLHelper();
        }

        /// <summary>
        /// Geting the Patient Details
        /// </summary>
        /// <param name="id">Patient ID</param>
        public void getPatient(int id)
        {
            string sql = "select PatientId, FirstName, LastName, Address, HomePhone, Mobile, MedicareNum, Notes, Suburb, PostCode, State from Patient where PatientId =@PatientId";

            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@PatientId", DbType.Int16);
            objParams[0].Value = id;

            //Execute the statement and populate data table using SQLHelper Class
            DataTable PatientTable = _DB.executeSQL(sql, objParams);

            //Populate Class Fields
            DataRow dr = PatientTable.Rows[0];
            PatientId = (int)dr["PatientId"];
            HomePhone = dr["HomePhone"].ToString();
            Mobile = dr["Mobile"].ToString();
            MedicareNum = dr["MedicareNumber"].ToString();
            FirstName = dr["FirstName"].ToString();
            LastName = dr["LastName"].ToString();
            Address = dr["Address"].ToString();
            Notes = dr["Notes"].ToString();
            Suburb = dr["Suburb"].ToString();
            PostCode = dr["PostCode"].ToString();
            State = dr["State"].ToString();
        }

        /// <summary>
        /// get a count of all Patients
        /// </summary>
        /// <returns>Number of patients</returns>
        public int getNumberOfPatients()
        {
            string sql = "select count(*) from patient";
            int rowCount = (int)_DB.scalarSQL(sql);
            return rowCount;
        }

        /// <summary>
        /// insert a patient
        /// </summary>
        /// <returns>Number of rows inserted</returns>
        public int insertPatient()
        {
            int rowsAffected;
            string sql = "insert into Patient(FirstName, LastName, Suburb, PostCode, State, Notes, Address, HomePhone, Mobile, MedicareNum) " +
                "values(@FirstName, @LastName, @Suburb, @PostCode, @State, @Notes, @Address, @HomePhone, @Mobile, @MedicareNum)";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[10];

            objParams[0] = new SqlParameter("@FirstName", DbType.String);
            objParams[0].Value = FirstName;

            objParams[1] = new SqlParameter("@LastName", DbType.String);
            objParams[1].Value = LastName;

            objParams[2] = new SqlParameter("@Address", DbType.String);
            objParams[2].Value = Address;

            objParams[3] = new SqlParameter("@HomePhone", DbType.String);
            objParams[3].Value = HomePhone;

            objParams[4] = new SqlParameter("@Mobile", DbType.String);
            objParams[4].Value = Mobile;

            objParams[5] = new SqlParameter("@PostCode", DbType.String);
            objParams[5].Value = PostCode;

            objParams[6] = new SqlParameter("@MedicareNum", DbType.String);
            objParams[6].Value = MedicareNum;

            objParams[7] = new SqlParameter("@Suburb", DbType.String);
            objParams[7].Value = Suburb;                     

            objParams[8] = new SqlParameter("@State", DbType.String);
            objParams[8].Value = State;

            objParams[9] = new SqlParameter("@Notes", DbType.String);
            objParams[9].Value = Notes;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }
        /// <summary>
        /// Update a Patient
        /// </summary>
        /// <param name="PatientId">Patient ID to be updated</param>
        /// <returns>Number of rows affected</returns>
        public int updatePatient(int PatientId)
        {
            int rowsAffected;
            string sql = "update Patient set State = @State, PostCode = @PostCode, Suburb = @Suburb, FirstName = @FirstName, Notes = @Notes,  Address = @Address, HomePhone = @HomePhone, Mobile = @Mobile, MedicareNum = @MedicareNum where PatientId = @PatientId";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[11];

            objParams[0] = new SqlParameter("@FirstName", DbType.String);
            objParams[0].Value = FirstName;

            objParams[1] = new SqlParameter("@LastName", DbType.String);
            objParams[1].Value = LastName;

            objParams[2] = new SqlParameter("@Address", DbType.String);
            objParams[2].Value = Address;

            objParams[3] = new SqlParameter("@HomePhone", DbType.String);
            objParams[3].Value = HomePhone;

            objParams[4] = new SqlParameter("@Mobile", DbType.String);
            objParams[4].Value = Mobile;                       
        
            objParams[5] = new SqlParameter("@PostCode", DbType.String);
            objParams[5].Value = PostCode;

            objParams[6] = new SqlParameter("@Suburb", DbType.String);
            objParams[6].Value = Suburb;

            objParams[7] = new SqlParameter("@MedicareNum", DbType.String);
            objParams[7].Value = MedicareNum;

            objParams[8] = new SqlParameter("@Notes", DbType.String);
            objParams[8].Value = Notes;

            objParams[9] = new SqlParameter("@State", DbType.String);
            objParams[9].Value = State;

            objParams[10] = new SqlParameter("@PatientId", DbType.String);
            objParams[10].Value = PatientId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="PateintID">Category ID to be deleted</param>
        /// <returns>Number of rows affected</returns>
        public int deleteCategory(int PatientId)
        {
            int rowsAffected;
            string sql = "delete from Patient where PatientId = @PatientId";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@PatientId", DbType.Int32);
            objParams[0].Value = PatientId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;

        }
    }
}
