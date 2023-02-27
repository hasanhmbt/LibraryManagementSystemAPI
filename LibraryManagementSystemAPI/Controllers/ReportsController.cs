using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Repositories.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {

        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }


        [HttpGet("[action]")]
        public ActionResult GetBookCuntReports(string beginDate, string endDate )
        {
            return Ok(_reportRepository.GetBookCountReports(beginDate , endDate));
        }


       [HttpGet("[action]")]

        public ActionResult GetOperationCountReports(string beginDate, string endDate)
        {
            return Ok(_reportRepository.GetOperationCountReports(beginDate, endDate));
        }
    }
}
