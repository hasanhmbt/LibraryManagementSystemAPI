using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Tools;
using LibraryManagementSystemAPIAPI.ADONET_Manager;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
 
namespace LibraryManagementSystemAPI.Repositories.Concrete
{
    internal class BookRepository : IBookRepository
    {
        public List<Book> GetAllBooks()
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<Book> Books = new List<Book>();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "select *from vw_Books");
            var jsonString = JsonConvert.SerializeObject(data);
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(jsonString);
            return books;
        }

        public Book GetBookById(int id)
        {
            Book? book = new Book();
            SqlHelper sqlHelper = new SqlHelper();
            ConnectionManager connectionManager = new ConnectionManager();
           
            SqlDataReader reader = sqlHelper.ExecuteReader(query: $"Select  * From  Books where Id={id}", connection: out SqlConnection connection);

            if (reader.Read())
            {
                book.Name = Convert.ToString(reader["Name"]);
                book.Author = Convert.ToString(reader["Author"]);
                book.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                book.Count = Convert.ToInt32(reader["Count"]);

                connectionManager.CloseConnection(connection);
                return book;
            }
            else
                connectionManager.CloseConnection(connection);

            return null;
        }

        public int AddBook(Book book)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", book.Name),
                new SqlParameter("@Author", book.Author),
                new SqlParameter("@CategoryId", book.CategoryId),
                new SqlParameter("@Count", book.Count)
            };

           int newId = Convert.ToInt32(sqlHelper.ExecuteScalar(query: $"Insert into Books(Name,Author,CategoryId,Count)  values(@Name, @Author, @CategoryId, @Count);Select SCOPE_IDENTITY();", parameters: parameters));

            return newId;
        }

        public void EditBook(Book book)
        {
            SqlHelper sqlHelper = new SqlHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", book.Id),
                new SqlParameter("@Name", book.Name),
                new SqlParameter("@Author", book.Author),
                new SqlParameter("@CategoryId", book.CategoryId),
                new SqlParameter("@Count", book.Count)
            };

            sqlHelper.ExecuteNonQuery(query: $"Update Books set  Name=@Name, Author=@Author, CategoryId=@CategoryId, Count=@Count where Id=@Id", parameters: parameters);
        }

        public void DeleteBooks(int bookId)
        {
            SqlHelper sqlHelper = new SqlHelper();

            sqlHelper.ExecuteNonQuery(query: $"Delete from Books where Id = {bookId}");
        }
    }
}
