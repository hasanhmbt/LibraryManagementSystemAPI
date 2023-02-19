using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Tools;
using LibraryManagementSystemAPIAPI.ADONET_Manager;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
 
namespace LibraryManagementSystemAPI.Repositories.Concrete
{
    internal class OperationRepository : IOperationRepository
    {
        public List<Operation> GetAllOperations( )
        {
           

            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "select *from  vw_Operations;");
            string jsonstring = JsonConvert.SerializeObject(data);
            List<Operation> operations = JsonConvert.DeserializeObject<List<Operation>>(jsonstring);
            return operations;


        }

        public Operation GetOperationById(int id)
        {
            Operation? operation = new Operation();
            SqlHelper sqlHelper = new SqlHelper();
            ConnectionManager connectionManager = new ConnectionManager();
           
            SqlDataReader reader = sqlHelper.ExecuteReader(query: $"Select ReaderId, BookId, OperationDate from Operations where Id={id}", connection: out SqlConnection connection);

            if (reader.Read())
            {
                operation.ReaderId = Convert.ToInt32(reader["ReaderId"]);
                operation.BookId = Convert.ToInt32(reader["BookId"]);
                operation.OperationDate = Convert.ToDateTime(reader["OperationDate"]);

                connectionManager.CloseConnection(connection);
                return operation;
            }
            else
                connectionManager.CloseConnection(connection);

            return null;
        }

        public List<Operation> GetBooksCombo()
        {
            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "Select -1 as Id, N'Seçilməyib' as Name union all Select b.Id, b.Name from Books b left join Operations o on o.BookId=b.Id and o.AcceptStatus=0 group by b.Id, b.Name, b.Count having count(o.Id)<b.Count");
            string jsonstring = JsonConvert.SerializeObject(data);
            List<Operation> operations = JsonConvert.DeserializeObject<List<Operation>>(jsonstring);
            return operations;
        }

        public int AddOperation(Operation operation)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ReaderId", operation.ReaderId),
                new SqlParameter("@BookId", operation.BookId),
                new SqlParameter("@UserId", operation.UserId),
                new SqlParameter("@OperationDate", operation.OperationDate)
            };

            int id = Convert.ToInt32(sqlHelper.ExecuteScalar(query: $"Insert into Operations(ReaderId,BookId,UserId,OperationDate)\r\nvalues(@ReaderId, @BookId, @UserId, @OperationDate);Select SCOPE_IDENTITY();", parameters: parameters));
            return id;
        }

        public void EditOperation(Operation operation)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", operation.Id),
                new SqlParameter("@ReaderId", operation.ReaderId),
                new SqlParameter("@BookId", operation.BookId),
                new SqlParameter("@UserId", operation.UserId),
                new SqlParameter("@OperationDate", operation.OperationDate)
            };

            sqlHelper.ExecuteNonQuery(query: $"Update Operations set \r\nReaderId=@ReaderId, BookId=@BookId, UserId=@UserId, OperationDate=@OperationDate where Id=@Id", parameters: parameters);
        }

        public void DeleteOperations(int operationIds)
        {
            SqlHelper sqlHelper = new SqlHelper();
           

            sqlHelper.ExecuteNonQuery(query: $"Delete from Operations where Id ={operationIds}");
        }

        public void AcceptBook(int operationId)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", operationId)
            };

            sqlHelper.ExecuteNonQuery(query: $"Update Operations set AcceptStatus=1 where Id=@Id", parameters: parameters);
        }
    }
}
