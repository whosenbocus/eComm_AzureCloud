using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Model
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentAsync(string desc,string Id);
        Task<IEnumerable<Payment>> GetPaymentsAsync();
        Task<Payment> SavePaymentAsync(Payment payment);
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(Payment payment);
    }
}
