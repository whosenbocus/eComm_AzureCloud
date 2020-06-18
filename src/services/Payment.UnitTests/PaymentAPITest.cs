using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Payment.API.Controllers;
using Payment.API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Payment.UnitTests
{
    public class PaymentAPITest
    {
        private readonly Mock<Payment.API.Model.IPaymentRepository> _PaymentRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public PaymentAPITest()
        {
            _PaymentRepositoryMock = new Mock<Payment.API.Model.IPaymentRepository>();
            _mapperMock = new Mock<IMapper>();

        }

        [Fact]
        public async Task Get_payment_success()
        {
            //Arrange
            var fakePaymentId = 1;
            var fakePaymentdto = GetPaymentFakeDTOs(fakePaymentId);
            var fakePayment = GetPaymentFake(fakePaymentId);


            _PaymentRepositoryMock.Setup(x => x.GetPaymentAsync(It.IsAny<int>())).Returns(Task.FromResult(fakePayment));

            _mapperMock.Setup(x => x.Map<PaymentDTOs>(It.IsAny<API.Model.Payment>()))
            .Returns((API.Model.Payment source) =>
            {
                return fakePaymentdto;
            });

            //Act
            var paymentController = new PaymentController(
                _PaymentRepositoryMock.Object,
                _mapperMock.Object);

            var actionResult = await paymentController.GetPayment(fakePaymentId);

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.Equal((((ObjectResult)actionResult.Result).Value as PaymentDTOs).Description, fakePayment.Description);
        }

        [Fact]
        public async Task Post_Payment_success()
        {
            //Arrange
            var fakePaymentId = 1;
            var fakePaymentdto = GetPaymentFakeDTOs(fakePaymentId);
            var fakePayment = GetPaymentFake(fakePaymentId);

            _PaymentRepositoryMock.Setup(x => x.SavePaymentAsync(It.IsAny<API.Model.Payment>()))
                .Returns(Task.FromResult(fakePayment));

            _mapperMock.Setup(x => x.Map<PaymentDTOs>(It.IsAny<API.Model.Payment>()))
            .Returns((API.Model.Payment source) =>
            {
                return fakePaymentdto;
            });

            _mapperMock.Setup(x => x.Map<API.Model.Payment>(It.IsAny<PaymentDTOs>()))
            .Returns((PaymentDTOs source) =>
            {
                return fakePayment;
            });

            //Act
            var paymentController = new PaymentController(
                _PaymentRepositoryMock.Object,
                _mapperMock.Object);

            var actionResult = await paymentController.SavePayments(fakePaymentdto);

            //Assert
            Assert.Equal((actionResult.Result as CreatedAtActionResult).StatusCode, (int)System.Net.HttpStatusCode.Created);
            Assert.Equal((((ObjectResult)actionResult.Result).Value as PaymentDTOs).Description, fakePayment.Description);

        }


        private PaymentDTOs GetPaymentFakeDTOs(int fakePaymentId)
        {
            return new PaymentDTOs()
            {
                Timestamp = DateTime.Now,
                Amount = 10,
                Description = "test"
            };
        }

        private API.Model.Payment GetPaymentFake(int fakePaymentId)
        {
            return new API.Model.Payment()
            {
                Id = fakePaymentId,
                Timestamp = DateTime.Now,
                Amount = 10,
                Description = "test"
            };
        }
    }
}
