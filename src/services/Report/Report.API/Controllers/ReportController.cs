using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.API.Infrastructure.Repository;
using Report.API.Model;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository report;
        public ReportController(IReportRepository _report)
        {
            report = _report;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payload>>> GetPayloads()
        {
            var payloads = report.GetPayloads();
            return Ok(payloads);
        }
    }
}
