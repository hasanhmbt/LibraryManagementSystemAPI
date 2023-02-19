using LibraryManagementSystemAPIAPI.Tools;
using System.Data.SqlClient;

namespace LibraryManagementSystemAPIAPI.ADONET_Manager
{
    public class ConnectionManager
    {
        private static readonly string _connectionString = CommonTools.GetAppSettings("ConnectionStrings:Default");

        public static string ConnectionString => _connectionString;


        public SqlConnection Openconnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }
    }
}
