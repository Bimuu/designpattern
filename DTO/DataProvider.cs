using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DataProvider
    {

        //Singleton
        private static DataProvider instance;
        private string strCon = "Data Source=BAORLYS\\SQLEXPRESS;Initial Catalog=longchau;Integrated Security=True";

        public static DataProvider Instance {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() {}


        public DataTable ExecuteQuery(string query, object[] parameter)
        {

            
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                ExecuteQueryCommand executeQueryCommand = new ExecuteQueryCommand(query, parameter);
                executeQueryCommand.Execute(conn);
                data = executeQueryCommand.Result;
                conn.Close();
                return data;
            }
        }
        

        public DataTable ExecuteQueryForTrans(string query, object[] parameter)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                ExecuteQueryForTransCommand executeQueryForTransCommand = new ExecuteQueryForTransCommand(query, parameter);
                executeQueryForTransCommand.Execute(conn);
                data = executeQueryForTransCommand.Result;
                conn.Close();
                return data;
            }
        }

        public int ExecuteNonQuery(string query, object[] parameter)
        {
            int data = 0;
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                ExecuteNonQueryCommand executeNonQueryCommand = new ExecuteNonQueryCommand(query,parameter);
                executeNonQueryCommand.Execute(conn);
                data = executeNonQueryCommand.Result;
                conn.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] parameter)
        {
            object data = 0;
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                ExecuteScalarCommand executeScalarCommand = new ExecuteScalarCommand(query, parameter);
                executeScalarCommand.Execute(conn);
                data = executeScalarCommand.Result;
                conn.Close();
            }

            return data;
        }
    }
}
