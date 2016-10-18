using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestHacks;
using System.Collections.Generic;
using PaymentService.Common;

namespace PaymentService.Tests
{
    [TestClass]
    public class IsAmountInRangeUnitTests : TestBase
    {
        /*
            Using https://github.com/Thwaitesy/MSTestHacks for runtime data driven tests
            where data is returned by a class metheod as an IEnumerable
        */

        /// <summary>
        /// This test method uses MSTestHacks features to iterate over list of test values 
        /// returned by AmountRangeTestValues method and run the test individually against 
        /// each test value
        /// </summary>
        [TestMethod]
        [DataSource("PaymentService.Tests.IsAmountInRangeUnitTests.AmountRangeTestValues")]
        public void IsAmountInRange_Validates_Passed_Amount_Is_WithInGivenRange()
        {
            var value = this.TestContext.GetRuntimeDataSourceObject<dynamic>();
            bool actual = ValidationsHelper.IsAmountInRange((long)value.amount, (long)value.minValue, (long)value.maxValue);
            Assert.AreEqual((bool)value.expected, actual,
                            "Failed for amount={0} and Range (minValue:{1}, maxValue:{2}).", value.amount, value.minValue, value.maxValue);
        }

        [TestMethod]
        public void IsAmountInRange_ReturnsTrue_When_Passed_Amount_Is_Same_As_MinValue()
        {
            Assert.IsTrue(ValidationsHelper.IsAmountInRange(0, 0, 999999));
        }

        [TestMethod]
        public void IsAmountInRange_ReturnsTrue_When_Passed_Amount_Is_Same_As_MaxValue()
        {
            Assert.IsTrue(ValidationsHelper.IsAmountInRange(999999, 0, 999999));
        }

        [TestMethod]
        public void IsAmountInRange_ReturnsFalse_When_Passed_Amount_Is_OneNumberSmaller_than_MinValue()
        {
            Assert.IsFalse(ValidationsHelper.IsAmountInRange(-1, 0, 999999));
        }

        [TestMethod]
        public void IsAmountInRange_ReturnsFalse_When_Passed_Amount_Is_OneNumberBigger_than_MaxValue()
        {
            Assert.IsFalse(ValidationsHelper.IsAmountInRange(1000000, 0, 999999));
        }

        [TestMethod]
        public void IsAmountInRange_ReturnsTrue_When_Passed_Amount_MinValue_And_MaxValue_All_Are_Same()
        {
            Assert.IsTrue(ValidationsHelper.IsAmountInRange(10, 10, 10));
        }

        [TestMethod]
        public void IsAmountInRange_ReturnsTrue_When_Passed_Amount_WithInGivenRange_And_Negative_minValue()
        {
            Assert.IsTrue(ValidationsHelper.IsAmountInRange(100, -10, 999999));
        }

        /// <summary>
        /// Returns a list of Test values to be used for IsAmountInRange. Each entry contains
        ///     1 - Amount to be checked
        ///     2 - Min Value of the Range
        ///     3 - Max Value of the Range
        ///     4 - Expected Validation Result
        /// </summary>
        private IEnumerable<dynamic> AmountRangeTestValues
        {
            get
            {
                return new List<dynamic> {                                      
                                    new { amount  =  10, minValue =  0, maxValue =  100, expected = true},
                                    new { amount  =  -10, minValue =  -20, maxValue =  -1, expected = true},
                                    new { amount  =  100, minValue =  0, maxValue =  999999, expected = true},                                    
                                    new { amount  =  -10, minValue =  0, maxValue =  999999, expected = false},
                                    new { amount  =  10, minValue =  0, maxValue =  999999, expected = true},
                                    new { amount  =  10000, minValue = 0, maxValue =  999999, expected = true},
                                    new { amount  =  100000000, minValue = 0, maxValue =  999999, expected = false}	
                    };
            }
        }
    }
}
