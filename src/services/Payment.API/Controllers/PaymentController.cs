using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Payment.API.DTOs;
using Payment.API.Model;

namespace Payment.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository PaymentRepo, IMapper mapper)
        {
            _paymentRepo = PaymentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PaymentDTOs>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PaymentDTOs>>> GetAllPayments()
        {
            var payments = await _paymentRepo.GetPaymentsAsync();
            return Ok(_mapper.Map<IEnumerable<PaymentDTOs>>(payments));
        }


        [HttpGet("{Id}",Name= "GetPayment")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PaymentDTOs), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaymentDTOs>> GetPayment(int Id)
        {
            var payment = await _paymentRepo.GetPaymentAsync(Id);
            return Ok(_mapper.Map<PaymentDTOs>(payment));

        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PaymentDTOs), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaymentDTOs>> SavePayments(PaymentDTOs paymentdto)
        {
            var Payment = _mapper.Map<Model.Payment>(paymentdto);
            await _paymentRepo.SavePaymentAsync(Payment);

            var paymentDto = _mapper.Map<PaymentDTOs>(Payment);


            return CreatedAtAction(nameof(GetPayment),new { Id = Payment.Id }, paymentDto);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PaymentDTOs), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaymentDTOs>> UpdatePayments(int Id, PaymentDTOs paymentdto)
        {
            Model.Payment Payment = await _paymentRepo.GetPaymentAsync(Id);
            if (Payment == null)
            {
                return NotFound();
            }
            var payment = _mapper.Map<Model.Payment>(paymentdto);

            var payments = await _paymentRepo.UpdatePaymentAsync(payment);
            return Ok(payments);
        }


        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<Model.Payment>> DeletePayments(int Id)
        {
            Model.Payment Payment = await _paymentRepo.GetPaymentAsync(Id);
            if (Payment == null)
            {
                return NotFound();
            }
            await _paymentRepo.DeletePaymentAsync(Payment);
            return NoContent();
        }

    }
}
