using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Added by me
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using ListViewItem = System.Windows.Controls.ListViewItem;

namespace Medical_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
                InitializeComponent();

            //Populating Practitioner ListBox with Values from Day Table
            string connection;
            string sql;
            //Read connection string settings from app.config file
            connection = ConfigurationManager.ConnectionStrings["MedPrac"].ConnectionString;
            //set up connection object
            SqlConnection dbConnection = new SqlConnection(connection);
            //listview code
            sql = "select DayId, DayName from Day";
            SqlCommand dbCommand = new SqlCommand(sql, dbConnection);
            //open database connection
            dbConnection.Open();
            //execute query and store in data reader
            SqlDataReader drResults = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            //load data into data table
            DataTable dtDays = new DataTable();
            dtDays.Load(drResults);
            //populate listview
            LisBoxDayAvail.ItemsSource = dtDays.DefaultView;


            //Display Appointment Times to Combo Box
            sql = "select TimeId, CONVERT(VARCHAR(5),AppointmentTime,108) as AppointmentTime from AppointmentTime";
            SqlCommand dbCommandTime = new SqlCommand(sql, dbConnection);
            //open database connection
            dbConnection.Open();
            //execute query and store in data reader
            SqlDataReader drResultsTime = dbCommandTime.ExecuteReader(CommandBehavior.CloseConnection);
            //load data into data table
            DataTable dtTime = new DataTable();
            dtTime.Load(drResultsTime);
            //populate listview
            ComBoxAppointTime.SelectedValuePath = "TimeId";
            ComBoxAppointTime.DisplayMemberPath = "AppointmentTime";
            ComBoxAppointTime.ItemsSource = dtTime.DefaultView;

            //Display Practitioner Types to Combo Box
            sql = "select TypeId, TypeName from PractitionerType";
            SqlCommand dbCommandPracType = new SqlCommand(sql, dbConnection);
            //open database connection
            dbConnection.Open();
            //execute query and store in data reader
            SqlDataReader drResultsPracType = dbCommandPracType.ExecuteReader(CommandBehavior.CloseConnection);
            //load data into data table
            DataTable dtPracType = new DataTable();
            dtPracType.Load(drResultsPracType);
            //populate listview
            ComBoxPracType.SelectedValuePath = "TypeId";
            ComBoxPracType.DisplayMemberPath = "TypeName";
            ComBoxPracType.ItemsSource = dtPracType.DefaultView;


            //Block past dates
            DatePickAppoint.BlackoutDates.AddDatesInPast();

                //Set start date for blocking weekends
                DateTime Min = DateTime.Today;

                //Set end date for blocking weekends
                DateTime Max = DateTime.Today.AddMonths(2);

                //Block Weekends From Calendar 
                for (DateTime AppointmentTime = Min; AppointmentTime <= Max; AppointmentTime = AppointmentTime.AddDays(1))
                {
                    if (AppointmentTime.DayOfWeek == DayOfWeek.Saturday || AppointmentTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        DatePickAppoint.BlackoutDates.Add(new CalendarDateRange(AppointmentTime));
                    }
                }
              
                //get patient collection, contains all patients
                Patients patients = new Patients();
                DataGridPatient.ItemsSource = patients;

            //get practitioners collection, contains all the practitioners
            Practitioners practitioners = new Practitioners();
            DataGridPrac.ItemsSource = practitioners;
            
            //get practitioners and patient collection, contains all the patients and practitioners            
            DataGridAppointPrac.ItemsSource = practitioners;
            DataGridAppointPatient.ItemsSource = patients;
            //get appointments collection, contains all the appointments booked
            Appointments appointments = new Appointments();
            DataGridAppointList.ItemsSource = appointments;

            //disable input at the start
            disableInput();
        }


        private void disableInput()
        {
            //Disable Patient Buttons
            BttnInsertPatient.IsEnabled = false;
            BttnCancelPatient.IsEnabled = false;
            BttnEditPatient.IsEnabled = false;
            BttnDeletePatient.IsEnabled = false;
            //Enable Add Patient Button
            BttnAddPatient.IsEnabled = true;
           
            //Disable Patient Text Boxes/ComboBoxes
            TxtBxFirstNamePatient.IsEnabled = false;
            TxtBxLastNamePatient.IsEnabled = false;
            TxtBxAddressPatient.IsEnabled = false;
            TxtBxAddressSubPatient.IsEnabled = false;
            TxtBxAddressPostPatient.IsEnabled = false;
            TxtBxHomePhNumPatient.IsEnabled = false;
            TxtBxMobNumPatient.IsEnabled = false;
            TxtBxMedCareNumPatient.IsEnabled = false;
            TxtBxNotesPatient.IsEnabled = false;
            ComBoxAddressStatePatient.IsEnabled = false;
            ComBoxAddressStatePrac.IsEnabled = false;
            LisBoxDayAvail.IsEnabled = false;

            //Enable Add Practitioner Button
            BttnAddPrac.IsEnabled = true;
            //Disable Practitioner Buttons
            BttnEditPrac.IsEnabled = false;
            BttnDeletePrac.IsEnabled = false;
            BttnInsertPrac.IsEnabled = false;
            BttnCancelPrac.IsEnabled = false;

            //Disable Practitioner Text Boxes/ComboBoxes
            TxtBxFirstNamePrac.IsEnabled = false;
            TxtBxLastNamePrac.IsEnabled = false;
            TxtBxAddressPrac.IsEnabled = false;
            TxtBxAddressSubPrac.IsEnabled = false;
            TxtBxAddressPostPrac.IsEnabled = false;
            TxtBxHomePhNumPrac.IsEnabled = false;
            TxtBxMobNumPrac.IsEnabled = false;
            TxtBxMedRegNumPrac.IsEnabled = false;
            ComBoxPracType.IsEnabled = false;
            
            //Enable Add Appointment Button
            BttnBookingAdd.IsEnabled = true;
            //Disable Appointment Buttons
            BttnBookingInsert.IsEnabled = false;
            BttnBookingDelete.IsEnabled = false;
            BttnBookingCancel.IsEnabled = false;
            BttnBookingEdit.IsEnabled = false;

            //Disable Appointment DatePicker and Time Combo Box
            ComBoxAppointTime.IsEnabled = false;
            DatePickAppoint.IsEnabled = false;

            //Disable Appointment Practitioner and Patient DataGrids
            DataGridAppointPatient.IsEnabled = false;
            DataGridAppointPrac.IsEnabled = false;
        }

        private void enableInput()
        {
            //Disable Insert and Add New
            BttnInsertPatient.IsEnabled = false;
            BttnAddPatient.IsEnabled = false;

            BttnInsertPrac.IsEnabled = false;
            BttnAddPrac.IsEnabled = false;

            BttnBookingAdd.IsEnabled = false;
            BttnBookingInsert.IsEnabled = false;

            //Enable Cancel, Update and Delete Buttons
            BttnCancelPatient.IsEnabled = true;
            BttnEditPatient.IsEnabled = true;
            BttnDeletePatient.IsEnabled = true;

            BttnCancelPrac.IsEnabled = true;
            BttnEditPrac.IsEnabled = true;
            BttnDeletePrac.IsEnabled = true;

            BttnBookingCancel.IsEnabled = true;
            BttnBookingDelete.IsEnabled = true;
            BttnBookingEdit.IsEnabled = true;

            //Enable Patient Text Boxes/ComboBoxes
            TxtBxFirstNamePatient.IsEnabled = true;
            TxtBxLastNamePatient.IsEnabled = true;
            TxtBxAddressPatient.IsEnabled = true;            
            TxtBxAddressSubPatient.IsEnabled = true;
            TxtBxAddressPostPatient.IsEnabled = true;
            TxtBxHomePhNumPatient.IsEnabled = true;
            TxtBxMobNumPatient.IsEnabled = true;
            TxtBxMedCareNumPatient.IsEnabled = true;
            TxtBxNotesPatient.IsEnabled = true;
            ComBoxAddressStatePatient.IsEnabled = true;

            //Enable Practitioner Text Boxes
            TxtBxFirstNamePrac.IsEnabled = true;
            TxtBxLastNamePrac.IsEnabled = true;
            TxtBxAddressPrac.IsEnabled = true;
            TxtBxAddressSubPrac.IsEnabled = true;
            TxtBxAddressPostPrac.IsEnabled = true;
            TxtBxHomePhNumPrac.IsEnabled = true;
            TxtBxMobNumPrac.IsEnabled = true;
            TxtBxMedRegNumPrac.IsEnabled = true;
            ComBoxPracType.IsEnabled = true;
            ComBoxAddressStatePrac.IsEnabled = true;
            LisBoxDayAvail.IsEnabled = true;

            //Enable Appointment Datepicker and Combo Box
            ComBoxAppointTime.IsEnabled = true;
            DatePickAppoint.IsEnabled = true;
        }

        /// <summary>
        /// DataGrids for Appointment Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DataGridAppointPrac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Practitioner selectedPractitioner;
            //get data from the grid and display in text boxes
            if (DataGridAppointPrac.SelectedIndex >= 0)
            {
                selectedPractitioner = (Practitioner)DataGridAppointPrac.SelectedItem;
                TxtBlocSelectPrac.Text = selectedPractitioner.FirstName + " " + selectedPractitioner.LastName;
            }
            //enable input
            enableInput();
        }

        private void DataGridAppointPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient selectedPatient;
            //get data from the grid and display in text boxes
            enableInput();
            if (DataGridAppointPatient.SelectedIndex >= 0)
            {
                selectedPatient = (Patient)DataGridAppointPatient.SelectedItem;
                TxtBlocSelectPatient.Text = selectedPatient.FirstName + " " + selectedPatient.LastName;
            }
            //enable input
            enableInput();
        }

        private void DataGridAppointList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointment selectedAppointment;
            
            if (DataGridAppointList.SelectedIndex >= 0)
            {
                selectedAppointment = (Appointment)DataGridAppointList.SelectedItem;
                TxtBlocSelectPatient.Text = selectedAppointment.PatientId.ToString();
                TxtBlocSelectPrac.Text = selectedAppointment.PractitionerId.ToString();
                ComBoxAppointTime.SelectedIndex = selectedAppointment.TimeId;
            }
            //enable input
            enableInput();
        }

        /// <summary>
        /// DataGrid for Patient Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DataGridPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient selectedPatient;
            //get data from the grid and display in text boxes           
            if (DataGridPatient.SelectedIndex >= 0)
            {
                selectedPatient = (Patient)DataGridPatient.SelectedItem;
                TxtBxFirstNamePatient.Text = selectedPatient.FirstName;
                TxtBxLastNamePatient.Text = selectedPatient.LastName;
                TxtBxAddressPatient.Text = selectedPatient.Address;
                TxtBxAddressSubPatient.Text = selectedPatient.Suburb;
                TxtBxAddressPostPatient.Text = selectedPatient.PostCode;
                TxtBxHomePhNumPatient.Text = selectedPatient.HomePhone;
                TxtBxMobNumPatient.Text = selectedPatient.Mobile;
                TxtBxMedCareNumPatient.Text = selectedPatient.MedicareNum;
                TxtBxNotesPatient.Text = selectedPatient.Notes;
                ComBoxAddressStatePatient.SelectedItem = ComBoxAddressStatePatient.Items.IndexOf(selectedPatient.State);
                
            }
            //enable input
            enableInput();

        }

        /// <summary>
        /// DataGrid for Practitioner Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DataGridPrac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Practitioner selectedPractitioner;
            //get data from the grid and display in text boxes            
            if (DataGridPrac.SelectedIndex >= 0)
            {
                selectedPractitioner = (Practitioner)DataGridPrac.SelectedItem;
                TxtBxFirstNamePrac.Text = selectedPractitioner.FirstName;
                TxtBxLastNamePrac.Text = selectedPractitioner.LastName;
                TxtBxAddressPrac.Text = selectedPractitioner.Address;
                TxtBxAddressSubPrac.Text = selectedPractitioner.Suburb;
                TxtBxAddressPostPrac.Text = selectedPractitioner.PostCode;
                TxtBxHomePhNumPrac.Text = selectedPractitioner.HomePhone;
                TxtBxMobNumPrac.Text = selectedPractitioner.Mobile;
                TxtBxMedRegNumPrac.Text = selectedPractitioner.RegistrationNum;
                ComBoxPracType.SelectedValuePath = selectedPractitioner.TypeId.ToString();
                ComBoxAddressStatePrac.SelectedItem = ComBoxAddressStatePrac.Items.IndexOf(selectedPractitioner.State);
            }
            //enable input
            enableInput();
        }

       
        /// <summary>
        /// Patient Button Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BttnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            DataGridPatient.SelectedIndex = -1;
            //empty text boxes
            TxtBxFirstNamePatient.Text = "";
            TxtBxLastNamePatient.Text = "";
            TxtBxAddressPatient.Text = "";
            TxtBxAddressSubPatient.Text = "";
            TxtBxAddressPostPatient.Text = "";
            TxtBxHomePhNumPatient.Text = "";
            TxtBxMobNumPatient.Text = "";
            TxtBxMedCareNumPatient.Text = "";
            TxtBxNotesPatient.Text = "";
            ComBoxAddressStatePatient.SelectedIndex = -1;

            //enable insert button
            BttnInsertPatient.IsEnabled = true;

            //enable cancel button
            BttnCancelPatient.IsEnabled = true;

            //disable datagrid, add new, update and delete
            BttnEditPatient.IsEnabled = false;
            BttnDeletePatient.IsEnabled = false;
            BttnAddPatient.IsEnabled = false;
            DataGridPatient.IsEnabled = false;

            //enable textboxes/comboboxes
            TxtBxFirstNamePatient.IsEnabled = true;
            TxtBxLastNamePatient.IsEnabled = true;
            TxtBxAddressPatient.IsEnabled = true;
            TxtBxAddressSubPatient.IsEnabled = true;
            TxtBxAddressPostPatient.IsEnabled = true;
            TxtBxHomePhNumPatient.IsEnabled = true;
            TxtBxMobNumPatient.IsEnabled = true;
            TxtBxMedCareNumPatient.IsEnabled = true;
            TxtBxNotesPatient.IsEnabled = true;
            ComBoxAddressStatePatient.IsEnabled = true;
        }

        private void BttnEditPatient_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPatient.SelectedIndex >= 0)
            {
                //create Patient class
                Patient selectedPatient = new Patient();
                int rowsAffected;

                //get the selected Patient
                selectedPatient = (Patient)DataGridPatient.SelectedItem;

                //populate values from text boxes             
                if (string.IsNullOrWhiteSpace(TxtBxFirstNamePatient.Text) || TxtBxFirstNamePatient.Text.Length > 50)
                {
                    TxtBxFirstNamePatient.Background = Brushes.Yellow;
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else {
                    TxtBxFirstNamePatient.Background = Brushes.White;
                    selectedPatient.FirstName = TxtBxFirstNamePatient.Text; }

                if (string.IsNullOrWhiteSpace(TxtBxLastNamePatient.Text) || TxtBxLastNamePatient.Text.Length > 50)
                {
                    TxtBxLastNamePatient.Background = Brushes.Yellow;
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else {
                    TxtBxLastNamePatient.Background = Brushes.White;
                    selectedPatient.LastName = TxtBxLastNamePatient.Text; }

                if (string.IsNullOrWhiteSpace(TxtBxAddressPatient.Text) || TxtBxAddressPatient.Text.Length > 50)
                {
                    TxtBxAddressPatient.Background = Brushes.Yellow;
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else {
                    TxtBxLastNamePatient.Background = Brushes.White;
                    selectedPatient.Address = TxtBxAddressPatient.Text; }

                if (string.IsNullOrWhiteSpace(TxtBxAddressSubPatient.Text) || TxtBxAddressSubPatient.Text.Length > 50)
                {
                    TxtBxAddressSubPatient.Background = Brushes.Yellow;
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else {
                    TxtBxLastNamePatient.Background = Brushes.White;
                    selectedPatient.Suburb = TxtBxAddressSubPatient.Text; }

                if (string.IsNullOrWhiteSpace(TxtBxAddressPostPatient.Text) || TxtBxAddressPostPatient.Text.Length > 4)
                {
                    TxtBxAddressPostPatient.Background = Brushes.Yellow;
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else {
                    TxtBxLastNamePatient.Background = Brushes.White;
                    selectedPatient.PostCode = TxtBxAddressPostPatient.Text; }

                if (string.IsNullOrWhiteSpace(TxtBxHomePhNumPatient.Text) || TxtBxHomePhNumPatient.Text.Length != 10 && string.IsNullOrWhiteSpace(TxtBxMobNumPatient.Text) || TxtBxMobNumPatient.Text.Length != 10)
                {
                    if (string.IsNullOrWhiteSpace(TxtBxHomePhNumPatient.Text) || TxtBxHomePhNumPatient.Text.Length != 10)
                    {
                        TxtBxHomePhNumPatient.Background = Brushes.Yellow;
                    }
                    else
                    {
                        TxtBxMobNumPatient.Background = Brushes.Yellow;
                    }
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxHomePhNumPatient.Background = Brushes.White;
                    TxtBxMobNumPatient.Background = Brushes.White;
                    selectedPatient.HomePhone = TxtBxHomePhNumPatient.Text;
                    selectedPatient.Mobile = TxtBxMobNumPatient.Text;
                }

                if (string.IsNullOrWhiteSpace(TxtBxMedCareNumPatient.Text) || TxtBxMedCareNumPatient.Text.Length != 10)
                {
                    TxtBxMedCareNumPatient.Background = Brushes.Yellow;
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else {
                    TxtBxMedCareNumPatient.Background = Brushes.White;
                    selectedPatient.MedicareNum = TxtBxMedCareNumPatient.Text; }

                if (TxtBxNotesPatient.Text.Length > 100)
                {
                    TxtBxNotesPatient.Background = Brushes.Yellow;
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else {
                    TxtBxNotesPatient.Background = Brushes.White;
                    selectedPatient.Notes = TxtBxNotesPatient.Text; }

                if (ComBoxAddressStatePatient.SelectedIndex == -1)
                {
                    TxtBlocPatientComfirm.Text = "Error, please check entries";
                    return;
                }
                else { selectedPatient.State = ComBoxAddressStatePatient.SelectedValue.ToString(); }

                //call update method
                rowsAffected = selectedPatient.updatePatient(selectedPatient.PatientId);               

                //create Patients collection
                Patients patients = new Patients();
                //re-display all patients in data grid
                DataGridPatient.ItemsSource = patients;
                DataGridAppointPatient.ItemsSource = patients;

                //display confirmation message
                if (rowsAffected == 1)
                {
                    TxtBlocPatientComfirm.Text = "Patient Updated";
                }
                else
                {
                    TxtBlocPatientComfirm.Text = "Patient Update Failed";
                }

                //disable input
                disableInput();
                
            }
        }

        private void BttnCancelPatient_Click(object sender, RoutedEventArgs e)
        {
            //disable input
            disableInput();

            //enable data grid
            DataGridPatient.IsEnabled = true;
        }

        private void BttnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPatient.SelectedIndex >= 0)
            {
                //create Patient class
                Patient selectedPatient = new Patient();
                int rowsAffected;

                //get the selected Patient
                selectedPatient = (Patient)DataGridPatient.SelectedItem;

                //call delete method
                rowsAffected = selectedPatient.deleteCategory(selectedPatient.PatientId);

                //re-display all Patients in data grid
                //create Patients collection
                Patients patients = new Patients();
                DataGridPatient.ItemsSource = patients;

                //display confirmation message
                if (rowsAffected == 1)
                {
                    TxtBlocPatientComfirm.Text = "Patient Deleted";
                }
                else
                {
                    TxtBlocPatientComfirm.Text = "Patient Delete Failed";
                }
                //disable input
                disableInput();
            }
        }

        private void BttnInsertPatient_Click(object sender, RoutedEventArgs e)
        {
            //create patient class
            Patient newPatient = new Patient();
            int rowsAffected;

            //populate values from text boxes and checking for correct values
            if (string.IsNullOrWhiteSpace(TxtBxFirstNamePatient.Text) || TxtBxFirstNamePatient.Text.Length > 50)
            {
                TxtBxFirstNamePatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else{
                TxtBxFirstNamePatient.Background = Brushes.White;
                newPatient.FirstName = TxtBxFirstNamePatient.Text; }
            
            if (string.IsNullOrWhiteSpace(TxtBxLastNamePatient.Text) || TxtBxLastNamePatient.Text.Length > 50)
            {
                TxtBxLastNamePatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxLastNamePatient.Background = Brushes.White;
                newPatient.LastName = TxtBxLastNamePatient.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxAddressPatient.Text) || TxtBxAddressPatient.Text.Length > 50)
            {
                TxtBxAddressPatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxAddressPatient.Background = Brushes.White;
                newPatient.Address = TxtBxAddressPatient.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxAddressSubPatient.Text) || TxtBxAddressSubPatient.Text.Length > 50)
            {
                TxtBxAddressSubPatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxAddressSubPatient.Background = Brushes.White;
                newPatient.Suburb = TxtBxAddressSubPatient.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxAddressPostPatient.Text) || TxtBxAddressPostPatient.Text.Length > 4)
            {
                TxtBxAddressPostPatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxAddressPostPatient.Background = Brushes.White;
                newPatient.PostCode = TxtBxAddressPostPatient.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxHomePhNumPatient.Text) && string.IsNullOrWhiteSpace(TxtBxMobNumPatient.Text) || TxtBxHomePhNumPatient.Text.Length != 10 || TxtBxMobNumPatient.Text.Length != 10)
            {
                TxtBxHomePhNumPatient.Background = Brushes.Yellow;
                TxtBxMobNumPatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxHomePhNumPatient.Background = Brushes.White;
                TxtBxMobNumPatient.Background = Brushes.White;
                newPatient.HomePhone = TxtBxHomePhNumPatient.Text;
                newPatient.Mobile = TxtBxMobNumPatient.Text;
            }

            if (string.IsNullOrWhiteSpace(TxtBxMedCareNumPatient.Text) || TxtBxMedCareNumPatient.Text.Length != 10)
            {
                TxtBxMedCareNumPatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxMedCareNumPatient.Background = Brushes.White;
                newPatient.MedicareNum = TxtBxMedCareNumPatient.Text; }

            if (TxtBxNotesPatient.Text.Length > 100)
            {
                TxtBxNotesPatient.Background = Brushes.Yellow;
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxNotesPatient.Background = Brushes.White;
                newPatient.Notes = TxtBxNotesPatient.Text; }

            if (ComBoxAddressStatePatient.SelectedIndex == -1)
            {
                TxtBlocPatientComfirm.Text = "Error, please check entries";
                return;
            }
            else { newPatient.State = ComBoxAddressStatePatient.SelectedValue.ToString(); }

            //call insertPatient method
            rowsAffected = newPatient.insertPatient();

            //display all patients in data grid
            //create patients collection
            Patients patients = new Patients();
            DataGridPatient.ItemsSource = patients;
            DataGridAppointPatient.ItemsSource = DataGridPatient.ItemsSource;
          
            //display confirmation message
            if (rowsAffected == 1)
            {
                TxtBlocPatientComfirm.Text = "New Patient Added";

                //disable input
                disableInput();

                //enable datagrid
                DataGridPatient.IsEnabled = true;
            }
            else
            {
                TxtBlocPatientComfirm.Text = "Insert failed";
            }
        }

        /// <summary>
        /// Practitioner Button Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BttnInsertPrac_Click(object sender, RoutedEventArgs e)
        {           
            //create category class
            Practitioner newPractitioner = new Practitioner();
            //AvailabilityClass availability = new AvailabilityClass();

            int rowsAffected;
            
            //populate values from text boxes
            newPractitioner.FirstName = TxtBxFirstNamePrac.Text;
            newPractitioner.LastName = TxtBxLastNamePrac.Text;
            newPractitioner.Address = TxtBxAddressPrac.Text;
            newPractitioner.Suburb = TxtBxAddressSubPrac.Text;
            newPractitioner.PostCode = TxtBxAddressPostPrac.Text;
            newPractitioner.HomePhone = TxtBxHomePhNumPrac.Text;
            newPractitioner.Mobile = TxtBxMobNumPrac.Text;
            newPractitioner.RegistrationNum = TxtBxMedRegNumPrac.Text;
            newPractitioner.TypeId = Convert.ToInt32(ComBoxPracType.SelectedValue);
            newPractitioner.State = ComBoxAddressStatePrac.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(TxtBxFirstNamePrac.Text) || TxtBxFirstNamePrac.Text.Length > 50)
            {
                TxtBxFirstNamePrac.Background = Brushes.Yellow;
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxFirstNamePrac.Background = Brushes.White;
                newPractitioner.FirstName = TxtBxFirstNamePrac.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxLastNamePrac.Text) || TxtBxLastNamePrac.Text.Length > 50)
            {
                TxtBxLastNamePrac.Background = Brushes.Yellow;
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxLastNamePrac.Background = Brushes.White;
                newPractitioner.LastName = TxtBxLastNamePrac.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxAddressPrac.Text) || TxtBxAddressPrac.Text.Length > 50)
            {
                TxtBxAddressPrac.Background = Brushes.Yellow;
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxAddressPrac.Background = Brushes.White;
                newPractitioner.Address = TxtBxAddressPrac.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxAddressSubPrac.Text) || TxtBxAddressSubPrac.Text.Length > 50)
            {
                TxtBxAddressSubPrac.Background = Brushes.Yellow;
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxAddressSubPrac.Background = Brushes.White;
                newPractitioner.Suburb = TxtBxAddressSubPrac.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxAddressPostPrac.Text) || TxtBxAddressPostPrac.Text.Length > 4)
            {
                TxtBxAddressPostPrac.Background = Brushes.Yellow;
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxAddressPostPrac.Background = Brushes.White;
                newPractitioner.PostCode = TxtBxAddressPostPrac.Text; }

            if (string.IsNullOrWhiteSpace(TxtBxHomePhNumPrac.Text) || string.IsNullOrWhiteSpace(TxtBxMobNumPrac.Text) || TxtBxHomePhNumPrac.Text.Length != 10 || TxtBxMobNumPrac.Text.Length != 10)
            {
                TxtBxHomePhNumPrac.Background = Brushes.Yellow;
                TxtBxMobNumPrac.Background = Brushes.Yellow;
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else
            {
                TxtBxHomePhNumPrac.Background = Brushes.White;
                TxtBxMobNumPrac.Background = Brushes.White;
                newPractitioner.HomePhone = TxtBxHomePhNumPrac.Text;
                newPractitioner.Mobile = TxtBxMobNumPrac.Text;
            }

            if (string.IsNullOrWhiteSpace(TxtBxMedRegNumPrac.Text) || TxtBxMedRegNumPrac.Text.Length != 13)
            {
                TxtBxMedRegNumPrac.Background = Brushes.Yellow;
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else {
                TxtBxMedRegNumPrac.Background = Brushes.White;
                newPractitioner.RegistrationNum = TxtBxMedRegNumPrac.Text; }

            if (ComBoxPracType.SelectedIndex == -1)
            {
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else { newPractitioner.TypeId = Convert.ToInt32(ComBoxPracType.SelectedValue); }

            if (ComBoxAddressStatePrac.SelectedIndex == -1)
            {
                TxtBlocPracConfirm.Text = "Error, please check entries";
                return;
            }
            else { newPractitioner.State = ComBoxAddressStatePrac.SelectedValue.ToString(); }



            //call insertCategory method
            rowsAffected = newPractitioner.insertPractitioner();

            ///  Availabilites for Practitioner -- I couldn't get it to work  ///
            
            //int PractitionerId = rowsAffected;

            ////Practitioner Availability DayID(s) + PractitionerID
            //foreach (DataRowView drv in LisBoxDayAvail.SelectedItems)
            //{
            //availability.DayId = Convert.ToInt32(drv.Row["DayId"]);
            //availability.PractitionerId = newPractitioner.PractitionerId;
            //availability.insertAvailability(PractitionerId, (int)drv["DayId"]);
            //}



            //display all categories in data grid
            //create categories collection
            Practitioners practitioners = new Practitioners();
            DataGridPrac.ItemsSource = practitioners;
            DataGridAppointPrac.ItemsSource = practitioners;

            //display confirmation message
            if (rowsAffected == 1)
            {
                TxtBlocPracConfirm.Text = "New Practitioner Added";

                //disable input
                disableInput();

                //enable datagrid
                DataGridPrac.IsEnabled = true;
            }
            else
            {
                TxtBlocPracConfirm.Text = "Insert failed";
            }
        }

        private void BttnEditPrac_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPrac.SelectedIndex >= 0)
            {
                //create Patient class
                Practitioner selectedPractitioner = new Practitioner();
                int rowsAffected;

                //get the selected Patient
                selectedPractitioner = (Practitioner)DataGridPrac.SelectedItem;

                //populate values from text boxes     

                if (string.IsNullOrWhiteSpace(TxtBxFirstNamePrac.Text) || TxtBxFirstNamePrac.Text.Length > 50)
                {
                    TxtBxFirstNamePrac.Background = Brushes.Yellow;
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxFirstNamePrac.Background = Brushes.White;
                    selectedPractitioner.FirstName = TxtBxFirstNamePrac.Text;
                }

                if (string.IsNullOrWhiteSpace(TxtBxLastNamePrac.Text) || TxtBxLastNamePrac.Text.Length > 50)
                {
                    TxtBxLastNamePrac.Background = Brushes.Yellow;
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxLastNamePrac.Background = Brushes.White;
                    selectedPractitioner.LastName = TxtBxLastNamePrac.Text;
                }

                if (string.IsNullOrWhiteSpace(TxtBxAddressPrac.Text) || TxtBxAddressPrac.Text.Length > 50)
                {
                    TxtBxAddressPrac.Background = Brushes.Yellow;
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxAddressPrac.Background = Brushes.White;
                    selectedPractitioner.Address = TxtBxAddressPrac.Text;
                }

                if (string.IsNullOrWhiteSpace(TxtBxAddressSubPrac.Text) || TxtBxAddressSubPrac.Text.Length > 50)
                {
                    TxtBxAddressSubPrac.Background = Brushes.Yellow;
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxAddressSubPrac.Background = Brushes.White;
                    selectedPractitioner.Suburb = TxtBxAddressSubPrac.Text;
                }

                if (string.IsNullOrWhiteSpace(TxtBxAddressPostPrac.Text) || TxtBxAddressPostPrac.Text.Length > 4)
                {
                    TxtBxAddressPostPrac.Background = Brushes.Yellow;
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxAddressPostPrac.Background = Brushes.White;
                    selectedPractitioner.PostCode = TxtBxAddressPostPrac.Text;
                }

                if (string.IsNullOrWhiteSpace(TxtBxHomePhNumPrac.Text) || string.IsNullOrWhiteSpace(TxtBxMobNumPrac.Text) || TxtBxHomePhNumPrac.Text.Length != 10 || TxtBxMobNumPrac.Text.Length != 10)
                {
                    TxtBxHomePhNumPrac.Background = Brushes.Yellow;
                    TxtBxMobNumPrac.Background = Brushes.Yellow;
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxHomePhNumPrac.Background = Brushes.White;
                    TxtBxMobNumPrac.Background = Brushes.White;
                    selectedPractitioner.HomePhone = TxtBxHomePhNumPrac.Text;
                    selectedPractitioner.Mobile = TxtBxMobNumPrac.Text;
                }

                if (string.IsNullOrWhiteSpace(TxtBxMedRegNumPrac.Text) || TxtBxMedRegNumPrac.Text.Length != 13)
                {
                    TxtBxMedRegNumPrac.Background = Brushes.Yellow;
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else
                {
                    TxtBxMedRegNumPrac.Background = Brushes.White;
                    selectedPractitioner.RegistrationNum = TxtBxMedRegNumPrac.Text;
                }

                if (ComBoxPracType.SelectedIndex == -1)
                {
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else { selectedPractitioner.TypeId = Convert.ToInt32(ComBoxPracType.SelectedValue); }

                if (ComBoxAddressStatePrac.SelectedIndex == -1)
                {
                    TxtBlocPracConfirm.Text = "Error, please check entries";
                    return;
                }
                else { selectedPractitioner.State = ComBoxAddressStatePrac.SelectedValue.ToString(); }

                //call update method
                rowsAffected = selectedPractitioner.updatePractitioner(selectedPractitioner.PractitionerId);

                //re-display all categories in data grid

                //create Patients collection
                Practitioners practitioners = new Practitioners();
                DataGridPrac.ItemsSource = practitioners;
                DataGridAppointPrac.ItemsSource = practitioners;

                //display confirmation message
                if (rowsAffected == 1)
                {
                    TxtBlocPracConfirm.Text = "Practitioner Updated";
                }
                else
                {
                    TxtBlocPracConfirm.Text = "Practitioner Update Failed";
                }

                //disable input
                disableInput();

            }
        }

        private void BttnDeletePrac_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPrac.SelectedIndex >= 0)
            {
                //create Patient class
                Practitioner selectedPractitioner = new Practitioner();
                int rowsAffected;

                //get the selected Patient
                selectedPractitioner = (Practitioner)DataGridPrac.SelectedItem;

                //call delete method
                rowsAffected = selectedPractitioner.deletePractitioner(selectedPractitioner.PractitionerId);

                //re-display all Patients in data grid
                //create Patients collection
                Practitioners practitioners = new Practitioners();
                DataGridPrac.ItemsSource = practitioners;

                //display confirmation message
                if (rowsAffected == 1)
                {
                    TxtBlocPracConfirm.Text = "Practitioner Deleted";
                }
                else
                {
                    TxtBlocPracConfirm.Text = "Practitioner Delete Failed";
                }
                //disable input
                disableInput();
            }
        }

        private void BttnAddPrac_Click(object sender, RoutedEventArgs e)
        {
            DataGridPatient.SelectedIndex = -1;
            //empty text boxes/set combo box index to -1
            TxtBxFirstNamePrac.Text = "";
            TxtBxLastNamePrac.Text = "";
            TxtBxAddressPrac.Text = "";
            TxtBxAddressSubPrac.Text = "";
            TxtBxAddressPostPrac.Text = "";
            TxtBxHomePhNumPrac.Text = "";
            TxtBxMobNumPrac.Text = "";
            TxtBxMedRegNumPrac.Text = "";
            ComBoxPracType.SelectedIndex = -1;
            ComBoxAddressStatePrac.SelectedIndex = -1;
            LisBoxDayAvail.SelectedItems.Clear();
        
            //enable insert button
            BttnInsertPrac.IsEnabled = true;

            //enable cancel button
            BttnCancelPrac.IsEnabled = true;

            //disable datagrid, add new, update and delete
            BttnEditPrac.IsEnabled = false;
            BttnDeletePrac.IsEnabled = false;
            BttnAddPrac.IsEnabled = false;
            DataGridPrac.IsEnabled = false;            

            //enable textboxes/comboboxes
            TxtBxFirstNamePrac.IsEnabled = true;
            TxtBxLastNamePrac.IsEnabled = true;
            TxtBxAddressPrac.IsEnabled = true;
            TxtBxAddressSubPrac.IsEnabled = true;
            TxtBxAddressPostPrac.IsEnabled = true;
            TxtBxHomePhNumPrac.IsEnabled = true;
            TxtBxMobNumPrac.IsEnabled = true;
            TxtBxMedRegNumPrac.IsEnabled = true;
            ComBoxPracType.IsEnabled = true;
            ComBoxAddressStatePrac.IsEnabled = true;
            LisBoxDayAvail.IsEnabled = true;
        }

        private void BttnCancelPrac_Click(object sender, RoutedEventArgs e)
        {
            //disable input
            disableInput();

            //enable data grid
            DataGridPrac.IsEnabled = true;
        }

        /// <summary>
        /// Appointment Button Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BttnBookingAdd_Click(object sender, RoutedEventArgs e)
        {
            DataGridAppointPatient.SelectedIndex = -1;
            DataGridAppointPrac.SelectedIndex = -1;
            DataGridAppointList.SelectedIndex = -1;
            
            //empty text blocs, combo box and datepicker
            TxtBlocSelectPatient.Text = "";
            TxtBlocSelectPrac.Text = "";
            DatePickAppoint.SelectedDate = null;
            ComBoxAppointTime.SelectedIndex = -1;


            //enable insert and cancel button
            BttnBookingInsert.IsEnabled = true;
            BttnBookingCancel.IsEnabled = true;

            //disable add, update, delete buttons and AppointmentList Datagrid
            BttnBookingDelete.IsEnabled = false;
            BttnBookingEdit.IsEnabled = false;
            BttnBookingAdd.IsEnabled = false;   
            DataGridAppointList.IsEnabled = false;

            //Enable the Appointment Patient and Practitioner DataGrids
            DataGridAppointPrac.IsEnabled = true;
            DataGridAppointPatient.IsEnabled = true;
        }

        private void BttnBookingInsert_Click(object sender, RoutedEventArgs e)
        {
            //Selected Patient and Practitioner
            Practitioner selectedPractioner;
            Patient selectedPatient;

            //Create Appointment class
            Appointment newAppointment = new Appointment();
            int rowsAffected;

            //Appointment PatientId
            if (DataGridAppointPatient.SelectedIndex >= 0)
            {
                selectedPatient = (Patient)DataGridAppointPatient.SelectedItem;
                newAppointment.PatientId = selectedPatient.PatientId;
            }

            //Appointment PractitionerId
            if (DataGridAppointPrac.SelectedIndex >= 0)
            {
                selectedPractioner = (Practitioner)DataGridAppointPrac.SelectedItem;
                newAppointment.PractitionerId = selectedPractioner.PractitionerId;
            }
            //Appointment Time
            newAppointment.TimeId = Convert.ToInt32(ComBoxAppointTime.SelectedValue);
            //Appointment Date
            newAppointment.AppointmentDate = DatePickAppoint.SelectedDate.Value;

                    
            //call insertAppointment method
            rowsAffected = newAppointment.insertAppointment();

            //display all categories in data grid
            //create categories collection
            Appointments appointments = new Appointments();
            
            DataGridAppointList.ItemsSource = appointments;

            //display confirmation message
            if (rowsAffected == 1)
            {
                TxtBlocAppointConfirm.Text = "New Appointment Added";

                //disable input
                disableInput();

                //enable datagrid
                DataGridAppointList.IsEnabled = true;
            }
            else
            {
                TxtBlocAppointConfirm.Text = "Insert failed";
            }

        }

        private void BttnBookingEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BttnBookingDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAppointList.SelectedIndex >= 0)
            {
                //create Patient class
                Appointment selectedAppointment = new Appointment();
                int rowsAffected;

                //get the selected Patient
                selectedAppointment = (Appointment)DataGridAppointList.SelectedItem;

                //call delete method
                rowsAffected = selectedAppointment.deleteAppointment(selectedAppointment.AppointmentId);

                //re-display all Patients in data grid
                //create Patients collection
                Appointments appointments = new Appointments();
                DataGridAppointList.ItemsSource = appointments;

                //display confirmation message
                if (rowsAffected == 1)
                {
                    TxtBlocAppointConfirm.Text = "Appointment Deleted";
                }
                else
                {
                    TxtBlocAppointConfirm.Text = "Appointment Delete Failed";
                }
                //disable input
                disableInput();
            }
        }

        private void BttnBookingCancel_Click(object sender, RoutedEventArgs e)
        {
            //disable input
            disableInput();
            //enable data grid
            DataGridAppointList.IsEnabled = true;
            
        }
    }
}



        




