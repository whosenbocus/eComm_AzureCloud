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
        public List<Payload> GetPayloads()
        {
            throw new NotImplementedException();
        }
    }
}
