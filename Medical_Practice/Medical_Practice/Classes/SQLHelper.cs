using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Added by me
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Medical_Practice
{
    class SQLHelper
    {
        //Variable to hold Connection String Settings
        private string _Conn;

        public SQLHelper()
        {
            //Getting Connection String for App.config
            _Conn = ConfigurationManager.ConnectionStrings["MedPrac"].ConnectionString;

        }

        public DataTable executeSQL(string sql)
        {
            //Creating connection object and get connection from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //Creating Command object and getting SQL to execute from sql parameter
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //Open database connection
            objConnection.Open();

            //Execute SQL and return dataReader
            SqlDataReader objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);

            //Create Data Table
            DataTable objDataTable = new DataTable();

            objDataTable.Load(objDataReader);

            return objDataTable;
        }

        public object scalarSQL(string sql)
        {
            //Creating connection object and getting connection string for private variable
            SqlConnection objConnection = new SqlConnection(_Conn);

            //Creating command object and getting SQL from parameter and passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //Create variable that will hold the return value
            object objRetValue;

            //Open database Connection
            objConnection.Open();

            //Execute SQL
            objRetValue = objCommand.ExecuteScalar();

            //Close Connection
            objConnection.Close();

            //Execute SQL and return value
            return objRetValue;
        }

        public int NonQuerySQL(string sql)
        {
            //Create connection object and get the connection string from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //Creating command object
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //Create a variable that will hold return value
            int intRetValue;

            //Open Database connection
            objConnection.Open();

            //Execute SQL
            intRetValue = objCommand.ExecuteNonQuery();

            //Close Connection
            objConnection.Close();


            //Execute SQL and return value
            return intRetValue;           
        }
   
        private void fillParameters(SqlCommand objCommand, SqlParameter[] parameters)            
        {
            int i;
            //for each parameter passed in add it to the command object
            //parameters collection
            for (i = 0; i < parameters.Length; i++)
            {
                objCommand.Parameters.Add(parameters[i]);
            }                  
        }

        public DataTable executeSQL(string sql, SqlParameter[] parameters)
        {
            //Create Connection Object get connection string from Private Variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);
            
            //Create Command Object get SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            
                //Fill parameters. This method is used to attach the parameters to the command object
                fillParameters(objCommand, parameters);
            
                //open database connection
                objConnection.Open();

                //execute SQL and store in dataReader
                SqlDataReader objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //create data table
                DataTable objDataTable = new DataTable();
                objDataTable.Load(objDataReader);
                return objDataTable;           
        }


        public object scalarSQL(string sql, SqlParameter[] parameters)
        {
            //Creating connection object and getting connection string for private variable
            SqlConnection objConnection = new SqlConnection(_Conn);

            //Creating command object and getting SQL from parameter and passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //Create variable that will hold the return value
            object objRetValue;

            //Fill parameters
            fillParameters(objCommand, parameters);

            //Open database Connection
            objConnection.Open();

            //Execute SQL
            objRetValue = objCommand.ExecuteScalar();

            //Close Connection
            objConnection.Close();

            //Execute SQL and return value
            return objRetValue;
        }

        public int NonQuerySQL(string sql, SqlParameter[] parameters)
        {
            //Create connection object and get the connection string from private variable _Conn
            SqlConnection objConnection = new SqlConnection(_Conn);

            //Creating command object
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //Fill parameters
            fillParameters(objCommand, parameters);

            //Create a variable that will hold return value
            int intRetValue;        

            //Open Database connection
            objConnection.Open();

            //Execute SQL
            intRetValue = objCommand.ExecuteNonQuery();

            //Close Connection
            objConnection.Close();

            //Execute SQL and return value
            return intRetValue;
        }

    }
}
