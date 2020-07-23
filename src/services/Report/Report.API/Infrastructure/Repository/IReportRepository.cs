using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.API.Infrastructure.Repository
{
    public interface IReportRepository
    {
        public List<Model.Payload> GetPayloads();
    }
}
