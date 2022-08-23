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
    class Appointment
    {
        //Private Variables
        private int _AppointmentId;
        private DateTime _AppointmentDate;
        private int _PractitionerId;
        private int _PatientId;
        private int _TimeId;
        private SQLHelper _DB;

        public int AppointmentId
        {
            get
            {
                return _AppointmentId;
            }
            set
            {
                _AppointmentId = value;
            }
        }
        public DateTime AppointmentDate
        {
            get
            {
                return _AppointmentDate;
            }
            set
            {
                _AppointmentDate = value;
            }
        }

        public int PractitionerId {
            get
            {
                return _PractitionerId;
            }
            set
            {
                _PractitionerId = value;
            }
        }

        public int PatientId {
            get
            {
                return _PatientId;
            }
            set
            {
                _PatientId = value;
            }
        }

        public int TimeId
        {
            get
            {
                return _TimeId;
            }
            set
            {
                _TimeId = value;
            }
        }

        public Appointment()
        {
            //Creating an Instance of SQL Helper Class
            _DB = new SQLHelper();
        }

        public Appointment(DataRow dr)
        {
            this.AppointmentId = (int)dr["AppointmentId"];
            this.AppointmentDate = DateTime.Parse(dr["AppointmentDate"].ToString());
            this._PatientId = (int)dr["PatientId"];
            this._PractitionerId = (int)dr["PractitionerId"];
            this._TimeId = (int)dr["TimeId"];

            _DB = new SQLHelper();
        }

        public void getAppointments(int id)
        {
            string sql = "select AppointmentId, AppointmentDate, TimeId, PatientId, PractitionerId from Appointment where AppointmentId =@AppointmentId";

            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@AppointmentId", DbType.Int16);
            objParams[0].Value = id;

            //Execute the statement and populate data table using SQLHelper Class
            DataTable DayTable = _DB.executeSQL(sql, objParams);

            //Populate Class Fields
            DataRow dr = DayTable.Rows[0];
            AppointmentId = (int)dr["AppointmentId"];
            AppointmentDate = DateTime.Parse(dr["AppointmentDate"].ToString());
            PatientId = (int)dr["PatientId"];
            PractitionerId = (int)dr["PractitionerId"];
            TimeId = (int)dr["TimeId"];
        }

        public int getNumberOfAppointments()
        {
            string sql = "select count(*) from Appointment";
            int rowCount = (int)_DB.scalarSQL(sql);
            return rowCount;
        }

        public int insertAppointment()
        {
            int rowsAffected;
            string sql = "insert into Appointment(AppointmentDate, TimeId, PatientId, PractitionerId) values(@AppointmentDate, @TimeId, @PatientId, @PractitionerId)";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[4];

            objParams[0] = new SqlParameter("@AppointmentDate", DbType.DateTime);
            objParams[0].Value = AppointmentDate;

            objParams[1] = new SqlParameter("@TimeId", DbType.String);
            objParams[1].Value = TimeId;

            objParams[2] = new SqlParameter("@PatientId", DbType.String);
            objParams[2].Value = PatientId;

            objParams[3] = new SqlParameter("@PractitionerId", DbType.String);
            objParams[3].Value = PractitionerId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }

        public int updateAppointment(int AppointmentId)
        {
            int rowsAffected;
            string sql = "update Appointment set AppointmentDate = @AppointmentDate, TimeId = @TimeId, PatientId = @PatientId, PractitionerId = @PractitionerId where AppointmentId = @AppointmentId";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[5];

            objParams[0] = new SqlParameter("@AppointmentDate", DbType.DateTime);
            objParams[0].Value = AppointmentDate;

            objParams[1] = new SqlParameter("@TimeId", DbType.String);
            objParams[1].Value = TimeId;

            objParams[2] = new SqlParameter("@PatientId", DbType.String);
            objParams[2].Value = PatientId;

            objParams[3] = new SqlParameter("@PractitionerId", DbType.String);
            objParams[3].Value = PractitionerId;

            objParams[4] = new SqlParameter("@AppointmentId", DbType.String);
            objParams[4].Value = AppointmentId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }

        public int deleteAppointment(int AppointmentId)
        {
            int rowsAffected;
            string sql = "delete from Appointment where AppointmentId = @AppointmentId";
            //Setting Up Parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@AppointmentId", DbType.Int32);
            objParams[0].Value = AppointmentId;

            rowsAffected = _DB.NonQuerySQL(sql, objParams);
            return rowsAffected;
        }
    }
}
