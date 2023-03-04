using LibraryManagementSystemAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Repositories.Abstract
{
    public interface IBookCategoryRepository
    {

        List<DropdownItems> GetAllCategories( );
        List<DropdownItems> GetCategoriesCombo();
        void DeleteCategory(int categoryId);

        int AddCategory(BookCategory bookCategory);
        void UpdateCategory(BookCategory bookCategory);

        BookCategory GetCategoryById(int id);

    }
}