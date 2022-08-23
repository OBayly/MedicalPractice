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
    class AvailabilityClass
    {
        //Private variables
        private int _DayId;
        private int _PractitionerId;
        private SQLHelper _DB;

        public int DayId {
            get
            {
                return _DayId;
            }
            set
            { _DayId = value;
            }
        }
        public int PractitionerId {
            get {
                return _PractitionerId;
            } set
            {
                _PractitionerId = value;
            }
        }
        
        public AvailabilityClass()
        {
            //Creating an Instance of SQL Helper Class
            //Enables access to methods and functions of SQLHelper Class
            _DB = new SQLHelper();
        }

        public AvailabilityClass(DataRow dr)
        {
            this.PractitionerId = (int)dr["PractitionerId"];      
            this.DayId = (int)dr["DayId"];
            
            _DB = new SQLHelper();
        }

        /// <summary>
        /// Geting the Availability Details
        /// </summary>
        /// <param name="id">Availability ID</param>
        public void getAvailabilityClass(int id, int id2)
        {
            string sql = "select PractitionerId, DayId from Availability where DayId =@DayId AND PractitionerId = @PractitionerId";

            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@DayId", DbType.Int16);
            objParams[1] = new SqlParameter("@PractitionerId", DbType.Int16);
            objParams[0].Value = id;
            objParams[1].Value = id2;

            //Execute the statement and populate data table using SQLHelper Class
            DataTable catTable = _DB.executeSQL(sql, objParams);

            //Populate Class Fields
            DataRow dr = catTable.Rows[0];
            PractitionerId = (int)dr["PractitionerId"];
            this.DayId = (int)dr["DayId"];
        }

        public int getNumberOfAvailability()
        {
            string sql = "select count(*) from Availabilites";
            int rowCount = (int)_DB.scalarSQL(sql);
            return rowCount;
        }

        public int insertAvailability(int practitionerId, int dayId)
        {
            int rowsAffected;
            string sql = "insert into Availability(DayId, PractitionerId) values (@DayId, @PractitionerId)";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@DayId", DbType.String);
            objParams[0].Value = DayId;

            objParams[1] = new SqlParameter("@PractitionerId", DbType.String);
            objParams[1].Value = PractitionerId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }

        public int updateAvailability(int PractitionerId, int DayId)
        {
            int rowsAffected;
            string sql = "update Availability set DayId = @DayId, PractitionerId = @PractitionerId where PractitionerId = @PractitionerId AND DayId = @DayId";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@DayId", DbType.String);
            objParams[0].Value = DayId;

            objParams[1] = new SqlParameter("@PractitionerId", DbType.String);
            objParams[1].Value = PractitionerId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }

        public int deleteAvailability(int PractitionerId)
        {
            int rowsAffected;
            string sql = "delete from Availability where PractitionerId = @PractitionerId AND DayId = @DayId";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@PractitionerId", DbType.Int32);
            objParams[0].Value = PractitionerId;

            objParams[1] = new SqlParameter("@DayId", DbType.Int32);
            objParams[1].Value = DayId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;

        }
    }
}
