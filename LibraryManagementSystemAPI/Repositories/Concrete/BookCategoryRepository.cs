using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPIAPI.ADONET_Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Repositories.Concrete
{
    public class BookCategoryRepository : IBookCategoryRepository
    {




        public List<BookCategory> GetAllCategories()
        {
            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "Select Id, Name from BookCategories;");
            string jsonstring = JsonConvert.SerializeObject(data);
            List<BookCategory> bookCategory = JsonConvert.DeserializeObject<List<BookCategory>>(jsonstring);
            return bookCategory;
        }


        public List<BookCategory> GetCategoriesCombo()
        {

            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "Select -1 as Id, N'Seçilməyib' as Name union  Select Id, Name from BookCategories;");
            string jsonstring = JsonConvert.SerializeObject(data);
            List<BookCategory> bookCategory = JsonConvert.DeserializeObject<List<BookCategory>>(jsonstring);
            return bookCategory;
             
        }

        public void DeleteCategory(int categoryId)
        {
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.ExecuteNonQuery($"Delete from BookCategories where Id = {categoryId}");
        }

        public int AddCategory(BookCategory bookCategory)
        {
            SqlHelper sqlHelper = new();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@Name",bookCategory.Name)

            };

            int id = Convert.ToInt32(sqlHelper.ExecuteScalar(query: "insert into BookCategories (Name) values(@Name);Select SCOPE_IDENTITY();", parameters: parameters));

            return id;
        }

        public void UpdateCategory(BookCategory bookCategory)
        {

            SqlHelper sqlHelper = new();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Name",bookCategory.Name),
               new SqlParameter("@Id",bookCategory.Id)

            };

            sqlHelper.ExecuteNonQuery(query: "update BookCategories set name=@Name where Id=@Id", parameters: parameters);

        }


        public BookCategory GetCategoryById(int id)
        {
            BookCategory? bookCategory = new BookCategory();
            SqlHelper sqlHelper = new SqlHelper();
            ConnectionManager connectionManager = new ConnectionManager();

            SqlDataReader reader = sqlHelper.ExecuteReader(query: $"Select  * From  BookCategories where Id={id}", connection: out SqlConnection connection);

            if (reader.Read())
            {
                bookCategory.Id = Convert.ToInt32(reader["Id"]);
                bookCategory.Name = Convert.ToString(reader["Name"]);
                bookCategory.Status = Convert.ToBoolean(reader["Status"]);

                connectionManager.CloseConnection(connection);
                return bookCategory;
            }
            else
                connectionManager.CloseConnection(connection);

            return null;
        }

    }
}