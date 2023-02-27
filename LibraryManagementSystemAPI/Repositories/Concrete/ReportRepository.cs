using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPIAPI.ADONET_Manager;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystemAPI.Repositories.Concrete
{
    public class ReportRepository : IReportRepository
    {



        public List<BookCountReport> GetBookCountReports(string beginDate , string endDate )
        {

            List<SqlParameter> parameters = new List<SqlParameter>
            {
              new SqlParameter("@Firstdate",beginDate),
              new SqlParameter("@Lastdate",endDate)

            };
            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "SP_RPT_BookCount", commandType: CommandType.StoredProcedure,parameters:parameters);
            var jsonString = JsonConvert.SerializeObject(data);
            List<BookCountReport> bookCountReport = JsonConvert.DeserializeObject<List<BookCountReport>>(jsonString);

            return bookCountReport;
        }


       


        public List<OperationReports> GetOperationCountReports(string beginDate, string endDate)
        {

            List<SqlParameter> parameters = new List<SqlParameter>
            {
              new SqlParameter("@Firstdate",beginDate),
              new SqlParameter("@Lastdate",endDate)

            };
            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "SP_RPT_OperationCountByUsers", commandType: CommandType.StoredProcedure, parameters: parameters);
            var jsonString = JsonConvert.SerializeObject(data);
            List<OperationReports> bookCountReport = JsonConvert.DeserializeObject<List<OperationReports>>(jsonString);

            return bookCountReport;
        }



    }
}
