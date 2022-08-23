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
    class Practitioner
    {
        //Private variables
        private int _PractitionerId;
        private string _LastName;
        private string _FirstName;
        private int _TypeId;
        private string _Address;
        private string _RegistrationNum;
        private string _HomePhone;
        private string _Mobile;
        private string _Suburb;
        private string _PostCode;
        private string _State;
        private SQLHelper _DB;

        public int PractitionerId
        {
            get
            {
                return _PractitionerId;
            }
            set
            {
                _PractitionerId = value;
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

        public string RegistrationNum
        {
            get
            {
                return _RegistrationNum;
            }
            set
            {
                _RegistrationNum = value;
            }
        }

        public int TypeId {
            get {
                return _TypeId;
            }
            set {
                _TypeId = value;
            }
        }
                       

        //Constructor
        public Practitioner()
        {
            //Creating an Instance of SQL Helper Class
            //Enables access to methods and functions of SQLHelper Class
            _DB = new SQLHelper();
        }

        //Create Patient from Data
        public Practitioner(DataRow dr)
        {
            this.PractitionerId = (int)dr["PractitionerId"];
            this.HomePhone = dr["HomePhone"].ToString();
            this.Mobile = dr["Mobile"].ToString();
            this.RegistrationNum = dr["RegistrationNum"].ToString();
            this.FirstName = dr["FirstName"].ToString();
            this.LastName = dr["LastName"].ToString();
            this.Address = dr["Address"].ToString();
            this.TypeId = (int)dr["TypeId"];
            this.Suburb = dr["Suburb"].ToString();
            this.PostCode = dr["PostCode"].ToString();
            this.State = dr["State"].ToString();

            _DB = new SQLHelper();
        }

        /// <summary>
        /// Geting the Practitioner Details
        /// </summary>
        /// <param name="id">Practitioner ID</param>
        public void getPractitioner(int id)
        {
            string sql = "select PractitionerId, FirstName, LastName, Address, HomePhone, Mobile, RegistrationNum, TypeId, Suburb, PostCode, State from Practitioner where PractitionerId =@PractitionerId";

            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@PractitionerId", DbType.Int16);
            objParams[0].Value = id;

            //Execute the statement and populate data table using SQLHelper Class
            DataTable PractTable = _DB.executeSQL(sql, objParams);

            //Populate Class Fields
            DataRow dr = PractTable.Rows[0];
            PractitionerId = (int)dr["PractitionerId"];
            HomePhone = dr["HomePhone"].ToString();
            Mobile = dr["Mobile"].ToString();
            RegistrationNum = dr["RegistrationNum"].ToString();
            FirstName = dr["FirstName"].ToString();
            LastName = dr["LastName"].ToString();
            Address = dr["Address"].ToString();
            TypeId = (int)dr["TypeId"];
            Suburb = dr["Suburb"].ToString();
            PostCode = dr["PostCode"].ToString();
            State = dr["State"].ToString();
        }

        /// <summary>
        /// get a count of all Practitioner
        /// </summary>
        /// <returns>Number of Practitioner</returns>
        public int getNumberOPractitioner()
        {
            string sql = "select count(*) from Practitioner";
            int rowCount = (int)_DB.scalarSQL(sql);
            return rowCount;
        }

        /// <summary>
        /// insert a patient
        /// </summary>
        /// <returns>Number of rows inserted</returns>
        public int insertPractitioner()
        {
            int rowsAffected;
            string sql = "insert into Practitioner(FirstName, LastName, Suburb, PostCode, TypeId, Address, HomePhone, Mobile, RegistrationNum, State) " +
                "values (@FirstName, @LastName, @Suburb, @PostCode, @TypeId, @Address, @HomePhone, @Mobile, @RegistrationNum, @State); SELECT SCOPE_IDENTITY()";
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

            objParams[6] = new SqlParameter("@Suburb", DbType.String);
            objParams[6].Value = Suburb;

            objParams[7] = new SqlParameter("@RegistrationNum", DbType.String);
            objParams[7].Value = RegistrationNum;

            objParams[8] = new SqlParameter("@TypeId", DbType.Int16);
            objParams[8].Value = TypeId;

            objParams[9] = new SqlParameter("@State", DbType.String);
            objParams[9].Value = State;

            //object practId = _DB.scalarSQL(sql, objParams);
            //PractitionerId = Convert.ToInt32(practId);
            
            
            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }
        /// <summary>
        /// Update a Patient
        /// </summary>
        /// <param name="PractitionerId">Practitioner ID to be updated</param>
        /// <returns>Number of rows affected</returns>
        public int updatePractitioner(int PractitionerId)
        {
            int rowsAffected;
            string sql = "update Practitioner set State = @State, PostCode = @PostCode, Suburb = @Suburb, FirstName = @FirstName, TypeId = @TypeId,  Address = @Address, HomePhone = @HomePhone, Mobile = @Mobile, RegistrationNum = @RegistrationNum where PractitionerId = @PractitionerId";
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

            objParams[7] = new SqlParameter("@RegistrationNum", DbType.String);
            objParams[7].Value = RegistrationNum;

            objParams[8] = new SqlParameter("@TypeId", DbType.String);
            objParams[8].Value = TypeId;

            objParams[9] = new SqlParameter("@State", DbType.String);
            objParams[9].Value = State;

            objParams[10] = new SqlParameter("@PractitionerId", DbType.Int32);
            objParams[10].Value = PractitionerId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="PractitionerId">Category ID to be deleted</param>
        /// <returns>Number of rows affected</returns>
        public int deletePractitioner(int PractitionerId)
        {
            int rowsAffected;
            string sql = "delete from Practitioner where PractitionerId = @PractitionerId";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@PractitionerId", DbType.Int32);
            objParams[0].Value = PractitionerId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;

        }
    }
}
