using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Repositories.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository= reportRepository;
        }

        [HttpGet("[action]")]

        public ActionResult GetBookCuntReports(Report report)
        {
            return Ok(_reportRepository.GetBookCuntReports(report));
        }
        [HttpGet("[action]")]

        public ActionResult GetOperationCountReports(Report report)
        {
            return Ok(_reportRepository.GetOperationCountReports(report));
        }
    }
}
