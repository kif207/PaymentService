using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestHacks;
using System.Collections.Generic;
using PaymentService.Common;

namespace PaymentService.Tests
{
    [TestClass]
    public class IsValidCardNumberTests : TestBase
    {
        /*
          * Using https://github.com/Thwaitesy/MSTestHacks for runtime data driven tests
          * where data is returned by a class metheod as an IEnumerable         
          */

        /// <summary>
        /// This test method uses MSTestHacks features to iterate over list of test values 
        /// returned by ValidSixteenDigitCreditCardNumbers method and run the test individually against 
        /// each credit card number
        /// </summary>
        ///
        [TestMethod]
        [DataSource("PaymentService.Tests.IsValidCardNumberTests.CardNumbersPassingLUHNCheck")]
        public void IsValidCardNumber_ReturnsTrue_When_Passed_Valid_Length_As_Per_CardScheme_That_Passes_LUHN_Check()
        {
            var value = this.TestContext.GetRuntimeDataSourceObject<dynamic>();
            bool actual = ValidationsHelper.IsValidCardNumber((string)value.cardNumber, (CardScheme)value.cardScheme);
            Assert.IsTrue(actual, "Failed for Valid 16 Digit Credit Card Number {0}", value.cardNumber);
        }

        [TestMethod]
        public void IsValidCardNumber_ReturnsFalse_When_Passed_Valid_Length_As_Per_CardScheme_That_Fails_LUHN_Check()
        {
            Assert.IsFalse(ValidationsHelper.IsValidCardNumber("4111111111111112",CardScheme.Visa));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsCardNumberValid_Throws_ArgumentNullException_If_CardNumber_Is_Null()
        {
            ValidationsHelper.IsValidCardNumber(null,CardScheme.Visa);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsCardNumberValid_Throws_ArgumentException_If_Blank_CardNumber_Is_Passed()
        {
            ValidationsHelper.IsValidCardNumber(string.Empty,CardScheme.Visa);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsCardNumberValid_Throws_ArgumentException_If_Alphanumeric_CardNumber_Is_Passed()
        {
            ValidationsHelper.IsValidCardNumber("1234XXXXX456",CardScheme.Visa);
        }

        /// <summary>
        /// Returns a list of Test credit card numbers that are 
        ///     1 - Valid Card Numbers
        ///     2 - Valid (passes Mod-10/LUHN check)
        /// </summary>
        private IEnumerable<dynamic> CardNumbersPassingLUHNCheck
        {
            get
            {
                return new List<dynamic> {
                                            new { cardNumber = "5555555555554444", cardScheme = CardScheme.Mastercard},
                                            new { cardNumber = "5454545454545454", cardScheme = CardScheme.Mastercard},
                                            new { cardNumber = "4444333322221111", cardScheme = CardScheme.Visa},
                                            new { cardNumber = "4917610000000000", cardScheme = CardScheme.Visa},
                                            new { cardNumber = "4462030000000000", cardScheme = CardScheme.Visa},
                                            new { cardNumber = "5105105105105100", cardScheme = CardScheme.Mastercard},
                                            new { cardNumber = "4111111111111111", cardScheme = CardScheme.Visa},
                                            new { cardNumber = "371449635398431", cardScheme = CardScheme.AmericanExpress},
                                            new { cardNumber = "378282246310005", cardScheme = CardScheme.AmericanExpress}
                };
            }
        }
    }
}
