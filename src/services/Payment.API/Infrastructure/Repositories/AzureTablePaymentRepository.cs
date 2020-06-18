using Payment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Infrastructure.Repositories
{
    public class AzureTablePaymentRepository : IPaymentRepository
    {
        public Task<bool> DeletePaymentAsync(Model.Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Payment> GetPaymentAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model.Payment>> GetPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task SavePaymentAsync(Model.Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Payment> UpdatePaymentAsync(Model.Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
