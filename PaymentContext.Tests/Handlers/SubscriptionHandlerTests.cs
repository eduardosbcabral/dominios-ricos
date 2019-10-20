using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(
                new FakeStudentRepository(),
                new FakeEmailService());

            var command = new CreateBoletoSubscriptionCommand()
            {
                FirstName = "Bruce",
                LastName = "Wayne",
                Document = "99999999999",
                Email = "hello@balta.io2",
                BarCode = "123456789",
                PaymentNumber = "123431",
                PaidDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMonths(1),
                Total = 60,
                TotalPaid = 60,
                Payer = "WARNE CORP.",
                PayerDocument = "12343123451",
                PayerDocumentType = EDocumentType.CPF,
                PayerEmail = "batman@dc.com",
                Street = "asdasd",
                Number = "dsafasd",
                Neighborhood = "asdasd",
                City = "afds",
                State = "gfdg",
                Country = "sada",
                ZipCode = "215443"
            };

            handler.Handle(command);

            Assert.AreEqual(false, handler.Valid);
        }
    }
}