using LibraryManagementSystemAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Repositories.Abstract
{
    public interface IReportRepository
    {
        List<BookCountReport> GetBookCountReports(  string beginDate = "", string endDate = "");
        List<OperationReports> GetOperationCountReports(  string beginDate = "", string endDate = "");


    }
}
