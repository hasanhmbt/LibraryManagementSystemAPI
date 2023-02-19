using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Tools;
using LibraryManagementSystemAPIAPI.ADONET_Manager;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace LibraryManagementSystemAPI.Repositories.Concrete
{
    internal class UserRepository : IUserRepository
    {
        public User AuthenticateUser(string username, string password)
        {
            User? user = new User();
            SqlHelper sqlHelper = new SqlHelper();
            ConnectionManager connectionManager = new ConnectionManager();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@encryptedPassword", CryptographyManager.GetSHA256(CryptographyManager.GetMd5(password))),

            };

            SqlDataReader reader = sqlHelper.ExecuteReader(query: "SP_AuthenticateUser", connection: out SqlConnection connection, commandType: CommandType.StoredProcedure, parameters: parameters);

            if (reader.Read())
            {
                user.Id = Convert.ToInt32(reader["Id"]);
                user.Name = Convert.ToString(reader["Name"]);
                user.Username = Convert.ToString(reader["Username"]);
                user.Email = Convert.ToString(reader["Email"]);
                user.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                user.ChangePassword = Convert.ToBoolean(reader["ChangePassword"]);

                connectionManager.CloseConnection(connection);
                return user;
            }
            else
                connectionManager.CloseConnection(connection);

            return null;


        }



        public User GetUserById(int id)
        {
            User? user = new User();
            SqlHelper sqlHelper = new SqlHelper();
            ConnectionManager connectionManager = new ConnectionManager();
            SqlDataReader reader = sqlHelper.ExecuteReader(query: $"select Id, Name, Username, Email, IsAdmin from Users where Id={id}", connection: out SqlConnection connection);

            if (reader.Read())
            {
                user.Id = Convert.ToInt32(reader["Id"]);
                user.Name = Convert.ToString(reader["Name"]);
                user.Username = Convert.ToString(reader["Username"]);
                user.Email = Convert.ToString(reader["Email"]);
                user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);

                connectionManager.CloseConnection(connection);
                return user;
            }
            else
                connectionManager.CloseConnection(connection);

            return null;

        }


       

        public List<User> GetAllUsers()
        {

            SqlHelper sqlHelper = new SqlHelper();
            List<User> Readers = new List<User>();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "select Id,Name,Username,Email,Password,IsAdmin,CreatedDate,Status from Users ;");
            string jsonstring = JsonConvert.SerializeObject(data);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonstring);
            return users;

            
        }


        public int AddUsers(User user)
        {
            SqlHelper sqlHelper = new SqlHelper();

            List<SqlParameter> parameters = new List<SqlParameter>
            {

                new SqlParameter("@Name",user.Name),
                new SqlParameter("@Username",user.Username),
                new SqlParameter("@Email",user.Email),
                new SqlParameter($"@Password",CryptographyManager.GetSHA256(CryptographyManager.GetMd5(user.Password))),
                new SqlParameter("@IsAdmin",user.IsAdmin),
            };


            int id = Convert.ToInt32(sqlHelper.ExecuteScalar(query: $"insert into Users(Name,Username,Email,Password,IsAdmin) values ( @Name, @Username, @Email, @Password, @IsAdmin);Select SCOPE_IDENTITY();", parameters: parameters));
            return id;
        }


        public void EditUser(User user)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id",user.Id),
                new SqlParameter("@IsAdmin",user.IsAdmin),
                new SqlParameter("@Name",user.Name),
                new SqlParameter("@Username",user.Username),
                new SqlParameter("@Email",user.Email),
            };

            sqlHelper.ExecuteNonQuery(query: $"Update Users set  Name=@Name, Username=@Username, Email=@Email, IsAdmin=@IsAdmin where Id=@Id", parameters: parameters);
        }

        public void DeleteUsers(int  userIds)
        {
            SqlHelper sqlHelper = new SqlHelper();
             

            sqlHelper.ExecuteNonQuery(query: $"Delete from Users where Id ={userIds}");
        }

        
    }
}
