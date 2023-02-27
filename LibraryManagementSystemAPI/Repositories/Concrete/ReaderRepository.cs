using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Tools;
using LibraryManagementSystemAPIAPI.ADONET_Manager;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystemAPI.Repositories.Concrete
{
    internal class ReaderRepository : IReaderRepository
    {
        public List<Reader> GetAllReaders()
        {
            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "select *from Readers");
            string jsonstring = JsonConvert.SerializeObject(data);
            List<Reader> reader = JsonConvert.DeserializeObject<List<Reader>>(jsonstring);
            return reader;
        }

        public List<DropdownItems> GetReadersCombo( )
        {
            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "Select -1 as Id, N'Seçilməyib' as Name union  Select Id, Name from Readers;");
            string jsonstring = JsonConvert.SerializeObject(data);
            List<DropdownItems> reader = JsonConvert.DeserializeObject<List<DropdownItems>>(jsonstring);
            return reader;
        }

        public Reader GetReaderById(int id)
        {
            Reader? reader1 = new Reader();
            SqlHelper sqlHelper = new SqlHelper();
            ConnectionManager connectionManager = new ConnectionManager();
            SqlDataReader reader = sqlHelper.ExecuteReader(query: $"select * from Readers where Id={id}", connection: out SqlConnection connection);

            if (reader.Read())
            {
                reader1.Id = Convert.ToInt32(reader["Id"]);
                reader1.Name = Convert.ToString(reader["Name"]);
                reader1.Email = Convert.ToString(reader["Email"]);
                reader1.PhoneNo = Convert.ToString(reader["PhoneNo"]);


                connectionManager.CloseConnection(connection);
                return reader1;
            }
            else
                connectionManager.CloseConnection(connection);

            return null;
        }



        public int AddReader(Reader reader)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", reader.Name),
                new SqlParameter("@Email", reader.Email),
                new SqlParameter("@PhoneNo",reader.PhoneNo)

            };

             int newId =  Convert.ToInt32(sqlHelper.ExecuteScalar(query: $"Insert into Readers(Name,Email,PhoneNo) values(@Name, @Email, @PhoneNo );Select SCOPE_IDENTITY();", parameters: parameters));
            return newId;
        }

        public void EditReader(Reader reader)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {

                new SqlParameter("@Id", reader.Id),
                new SqlParameter("@Name", reader.Name),
                new SqlParameter("@Email", reader.Email),
                new SqlParameter("@PhoneNo",reader.PhoneNo)
            };

            sqlHelper.ExecuteNonQuery(query: $"Update Readers set Name=@Name, Email=@Email,  PhoneNo=@PhoneNo  where Id=@Id", parameters: parameters);
        }

        public void DeleteReaders(int readerId)
        {
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.ExecuteNonQuery(query: $"Delete from Readers where Id  = {readerId}");
        }
    }
}
