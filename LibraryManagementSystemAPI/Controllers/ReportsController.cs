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

        /// <summary>
        /// Get book count report
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns>report table</returns>
        [HttpGet("[action]")]
        public ActionResult GetBookCountReports(string beginDate, string endDate )
        {
            return Ok(_reportRepository.GetBookCountReports(beginDate , endDate));
        }

        /// <summary>
        /// get operation count reports
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns>report table</returns>
        [HttpGet("[action]")]

        public ActionResult GetOperationCountReports(string beginDate, string endDate)
        {
            return Ok(_reportRepository.GetOperationCountReports(beginDate, endDate));
        }
    }
}
