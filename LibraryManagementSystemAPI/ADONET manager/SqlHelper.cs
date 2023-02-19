using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystemAPIAPI.ADONET_Manager
{
    public class SqlHelper
    {
        public SqlDataReader ExecuteReader(string query, out SqlConnection connection, CommandType commandType = CommandType.Text, List<SqlParameter> parameters = null)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            connection = connectionManager.Openconnection();
            SqlDataReader reader;

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = commandType;

                if (parameters != null)
                    foreach (SqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);

                reader = command.ExecuteReader();
            }

            return reader;
        }

        public object ExecuteScalar(string query, CommandType commandType = CommandType.Text, List<SqlParameter> parameters = null)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            object result;

            using (SqlConnection connection = connectionManager.Openconnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = commandType;

                    if (parameters != null)
                        foreach (SqlParameter parameter in parameters)
                            command.Parameters.Add(parameter);

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }


        public void ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, List<SqlParameter> parameters = null)
        {
            ConnectionManager connectionManager = new ConnectionManager();

            using (SqlConnection connection = connectionManager.Openconnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = commandType;

                    if (parameters != null)
                        foreach (SqlParameter parameter in parameters)
                            command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }


        public DataTable ExecuteNonQueryAsDataTable(string query, CommandType commandType = CommandType.Text, List<SqlParameter> parameters = null)
        {
            ConnectionManager connectionManager= new ConnectionManager();
            using (SqlConnection  connection = connectionManager.Openconnection())
            {
                SqlDataAdapter adapter = new();
                adapter.SelectCommand = new SqlCommand(query,connection);
                adapter.SelectCommand.CommandType = commandType;
                if (parameters != null)
                    foreach (var parameter in parameters)
                        adapter.SelectCommand.Parameters.Add(parameter);

                    
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
                
            }

        }
    }
}
