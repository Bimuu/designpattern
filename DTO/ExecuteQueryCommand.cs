using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DTO
{
    internal class ExecuteQueryCommand : IDbCommand
    {
        private string _query;
        private object[] _parameters;
        private DataTable _result;

        public ExecuteQueryCommand(string query, object[] parameters)
        {
            _query = query;
            _parameters = parameters;
        }

        public void Execute(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(_query, connection))
            {
                if (_parameters != null)
                {
                    string[] listPara = _query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, _parameters[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                _result = new DataTable();
                adapter.Fill(_result);
            }
        }

        public DataTable Result { get => _result; }
    }

}
