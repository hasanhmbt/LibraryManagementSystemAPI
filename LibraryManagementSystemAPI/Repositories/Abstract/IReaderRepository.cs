using LibraryManagementSystemAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Repositories.Abstract
{
    public interface IReaderRepository
    {
        List<Reader> GetAllReaders();
        List<DropdownItems> GetReadersCombo();
        Reader GetReaderById(int id);
        void DeleteReaders(int readerId);
        int AddReader(Reader reader);
        void EditReader(Reader reader);
    }


}
