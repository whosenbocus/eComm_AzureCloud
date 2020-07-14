using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.DTOs
{
    public class PaymentDTOs
    {
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
