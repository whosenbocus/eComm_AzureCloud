using Microsoft.EntityFrameworkCore;
using Report.API.Infrastructure.Repository;
using Report.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.API.Infrastructure.Implementation
{
    public class Report : IReportRepository
    {
        ModelContext context;
        public Report(ModelContext _context)
        {
            context = _context;
        }
        public List<Payload> GetPayloads()
        {
            return context.Payloads.ToList();
        }
    }
}
