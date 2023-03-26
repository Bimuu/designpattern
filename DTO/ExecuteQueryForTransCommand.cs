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
    internal class ExecuteQueryForTransCommand : IDbCommand
    {
        private string _query;
        private object[] _parameters;
        private DataTable _result;

        public ExecuteQueryForTransCommand(string query, object[] parameters)
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
                            if (i == 0)
                            {
                                SqlParameter temp = command.Parameters.AddWithValue(item, _parameters[i]);
                                temp.SqlDbType = SqlDbType.Structured;
                                temp.TypeName = "dbo.medicineHandler";
                                i++;
                                continue;
                            }
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
