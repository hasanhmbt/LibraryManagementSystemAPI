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
        



        public List<Report> GetBookCuntReports(Report report)
        {

            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "SP_RPT_BookCount", commandType: CommandType.StoredProcedure);
            string jsonstring = JsonConvert.SerializeObject(data);
            List<Report> Report = JsonConvert.DeserializeObject<List<Report>>(jsonstring);

            return Report;
        } 
        
        
        public List<Report> GetOperationCountReports(Report report)
        {

            SqlHelper sqlHelper = new SqlHelper();
            var data = sqlHelper.ExecuteNonQueryAsDataTable(query: "SP_RPT_OperationCountByUsers", commandType: CommandType.StoredProcedure);
            string jsonstring = JsonConvert.SerializeObject(data);
            List<Report> Report = JsonConvert.DeserializeObject<List<Report>>(jsonstring);

            return Report;
        }

    }
}
