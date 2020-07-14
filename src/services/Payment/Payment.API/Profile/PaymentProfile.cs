using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Payment.API.Profile
{
    public class PaymentProfile : AutoMapper.Profile
    {
        public PaymentProfile()
        {
            CreateMap<Model.Payment, DTOs.PaymentDTOs>();
            CreateMap<DTOs.PaymentDTOs,Model.Payment>();
        }
    }
}
