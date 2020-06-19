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
            var fakePaymentId = "1";
            var fakePaymentdesc = "1";
            var fakePaymentdto = GetPaymentFakeDTOs(fakePaymentId);
            var fakePayment = GetPaymentFake(fakePaymentId);


            _PaymentRepositoryMock.Setup(x => x.GetPaymentAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(fakePayment));

            _mapperMock.Setup(x => x.Map<PaymentDTOs>(It.IsAny<API.Model.Payment>()))
            .Returns((API.Model.Payment source) =>
            {
                return fakePaymentdto;
            });

            //Act
            var paymentController = new PaymentController(
                _PaymentRepositoryMock.Object,
                _mapperMock.Object);

            var actionResult = await paymentController.GetPayment(fakePaymentdesc,fakePaymentId);

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.Equal((((ObjectResult)actionResult.Result).Value as PaymentDTOs).Description, fakePayment.PartitionKey);
        }

        [Fact]
        public async Task Post_Payment_success()
        {
            //Arrange
            var fakePaymentId = "1";
            var fakePaymentdto = GetPaymentFakeDTOs(fakePaymentId);
            var fakePayment = GetPaymentFake(fakePaymentId);

            _PaymentRepositoryMock.Setup(x => x.SavePaymentAsync(It.IsAny<API.Model.Payment>()));

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
            Assert.Equal((((ObjectResult)actionResult.Result).Value as PaymentDTOs).Description, fakePayment.PartitionKey);

        }


        private PaymentDTOs GetPaymentFakeDTOs(string fakePaymentId)
        {
            return new PaymentDTOs()
            {
                Timestamp = DateTime.Now,
                Amount = 10,
                Description = "test"
            };
        }

        private API.Model.Payment GetPaymentFake(string fakePaymentId)
        {
            return new API.Model.Payment("test")
            {
                Timestamp = DateTime.Now,
                Amount = 10,
            };
        }
    }
}
