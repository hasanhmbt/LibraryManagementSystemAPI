using LibraryManagementSystemAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Repositories.Abstract
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBookById(int id);
        void DeleteBooks(int bookIds);

        int AddBook(Book book);
        void EditBook(Book book);
    }


}
