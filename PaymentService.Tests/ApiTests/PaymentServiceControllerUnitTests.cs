using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentService.WebApi.Controllers;
using PaymentService.Common;

namespace PaymentService.Tests
{
    [TestClass]
    public class PaymentServiceControllerUnitTests
    {
        static PaymentServiceController controller;
        static DateTime Today;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            controller = new PaymentServiceController();
            Today = DateTime.Now;
        }


        [TestMethod]
        public void WebApi_CandidateId_Returns_Correct_Value()
        {
            string id = controller.CandidateId();
            Assert.AreEqual("B68D28C1-59C0-406C-82DB-7BFDA3853163", id);
        }

        [TestMethod]
        public void WebApi_IsValidCardNumber_ReturnsTrue_When_Passed_Valid_Length_As_Per_CardScheme_And_Passes_LUHN_Check()
        {
            var cardNumber = "4111111111111111";
            bool actual = controller.IsValidCardNumber(cardNumber,CardScheme.Visa);
            Assert.IsTrue(actual, "Failed for Valid 16 Digit VISA Credit Card Number {0}", cardNumber);
        }

        [TestMethod]
        public void WebApi_IsValidCardNumber_ReturnsFalse_When_Passed_CardNumber_That_Passes_LUHN_Check_But_Is_NOT_A_Valid_Length_As_Per_CardScheme()
        {
            var cardNumber = "4222222222222";
            bool actual = controller.IsValidCardNumber(cardNumber, CardScheme.Visa);
            Assert.IsFalse(actual, "Passed for Valid but NOT A 16 Digit Credit Card Number {0}", cardNumber);
        }

        [TestMethod]
        public void WebApi_IsValidCardNumber_ReturnsFalse_When_Passed_Valid_Length_As_Per_CardScheme_That_Fails_LUHN_Check()
        {
            Assert.IsFalse(controller.IsValidCardNumber("4111111111111112",CardScheme.Visa));
        }

        [TestMethod]
        public void WebApi_IsValidCardNumber_ReturnsFalse_When_Passed_CardNumber_That_Fails_LUHN_Check_But_Is_NOT_A_Valid_Length_As_Per_CardScheme()
        {
            Assert.IsFalse(controller.IsValidCardNumber("4222222222233",CardScheme.Visa));
        }

        [TestMethod]
        public void WebApi_IsValidAmount_ReturnsTrue_When_Passed_Amount_Is_Valid()
        {
            Assert.IsTrue(controller.IsValidAmount(10));
        }

        [TestMethod]
        public void WebApi_IsValidAmount_ReturnsFalse_When_Passed_Amount_Is_InValid()
        {
            Assert.IsFalse(controller.IsValidAmount(-50));
        }

        [TestMethod]
        public void WebApi_IsValidPaymentTransaction_ReturnsTrue_When_Passed_Valid_CardNumber_And_Amount()
        {
            Assert.IsTrue(controller.IsValidPaymentTransaction("4111111111111111", 10,CardScheme.Visa));
        }

        [TestMethod]
        public void WebApi_IsValidPaymentTransaction_ReturnsFalse_When_Passed_InValid_CardNumber_But_Valid_Amount()
        {
            Assert.IsFalse(controller.IsValidPaymentTransaction("4111111111111112", 10,CardScheme.Visa));
        }

        [TestMethod]
        public void WebApi_IsValidPaymentTransaction_ReturnsFalse_When_Passed_Valid_CardNumber_But_Invalid_Amount()
        {
            Assert.IsFalse(controller.IsValidPaymentTransaction("4111111111111111", -50, CardScheme.Visa));
        }
    }
}
