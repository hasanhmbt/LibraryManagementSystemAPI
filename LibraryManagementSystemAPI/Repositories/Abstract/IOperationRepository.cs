
using LibraryManagementSystemAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Repositories.Abstract
{
    public interface IOperationRepository
    {
        List<Operation> GetAllOperations();
        List<Operation> GetBooksCombo();

        Operation GetOperationById(int id);
        void DeleteOperations(int operationIds);
        int AddOperation(Operation book);
        void EditOperation(Operation book);
        void AcceptBook(int operationId);
    }


}
