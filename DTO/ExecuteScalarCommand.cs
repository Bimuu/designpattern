using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class ExecuteScalarCommand : IDbCommand
    {
        private string _query;
        private object[] _parameters;
        private int _result;

        public ExecuteScalarCommand(string query, object[] parameters)
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
                _result = command.ExecuteNonQuery();
            }
        }

        public object Result { get => _result; }
    }

}
