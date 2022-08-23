using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Added by me
using System.Data;

namespace Medical_Practice
{
    class Patients : List<Patient>
    {
        public Patients()
        {
            SQLHelper db = new SQLHelper();
            //Get all Patients from Database
            string sql = "select PatientId, FirstName, LastName, Address, Suburb, PostCode, HomePhone, Mobile, MedicareNum, Notes, State from Patient";

            DataTable PatientTable = db.executeSQL(sql);

            foreach (DataRow dr in PatientTable.Rows)
            {
                //Create New Patient from DataRow
                Patient newPatient = new Patient(dr);

                //Add new Patient to Patient Collection List
                this.Add(newPatient);
            }
        }
    } 

    class Practitioners : List<Practitioner>
    {
        public Practitioners()
        {
            SQLHelper db = new SQLHelper();
            //Get all Patients from Database
            string sql = "select PractitionerId, FirstName, LastName, Address, Suburb, PostCode, HomePhone, Mobile, RegistrationNum, TypeId, State from Practitioner";

            DataTable PractitionerTable = db.executeSQL(sql);

            foreach (DataRow dr in PractitionerTable.Rows)
            {
                //Create New Patient from DataRow
                Practitioner newPractitioner = new Practitioner(dr);

                //Add new Patient to Patient Collection List
                this.Add(newPractitioner);
            }
        }
    }

    class Appointments : List<Appointment>
    {
        public Appointments()
        {
            SQLHelper db = new SQLHelper();
            //Get all Patients from Database
            string sql = "select AppointmentId, TimeId, convert(varchar, AppointmentDate, 3) as AppointmentDate, PatientId, PractitionerId from Appointment";

            DataTable AppointmentTable = db.executeSQL(sql);

            foreach (DataRow dr in AppointmentTable.Rows)
            {
                //Create New Patient from DataRow
                Appointment newAppointment = new Appointment(dr);

                //Add new Patient to Patient Collection List
                this.Add(newAppointment);
            }
        }
    }
}