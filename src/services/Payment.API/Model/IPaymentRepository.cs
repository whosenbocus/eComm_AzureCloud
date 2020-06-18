using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Model
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentAsync(int Id);
        Task<IEnumerable<Payment>> GetPaymentsAsync();
        Task SavePaymentAsync(Payment payment);
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(Payment payment);
    }
}
