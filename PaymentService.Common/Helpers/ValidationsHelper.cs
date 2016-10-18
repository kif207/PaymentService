using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace PaymentService.Common
{
    public class ValidationsHelper
    {
        /// <summary>
        /// Checks if the amount is with in given range of minVale and maxValue, both values inclusive
        /// if minValue and maxValue are not provided they will default to long.MinValue and long.MaxValue
        /// </summary>
        /// <param name="amountToCheck">An amount value in cents</param>
        /// <param name="minValue">minimum amount value in cents for lower limit of range.</param>
        /// <param name="maxValue">maximum amount value in cents for upper limit of range.</param>
        /// <remarks>
        /// Validation:
        /// The amountToCheck must be between minValue and maxValue (both values inclusive)
        /// </remarks>
        public static bool IsAmountInRange(long amountToCheck, long minValue = long.MinValue, long maxValue = long.MaxValue)
        {
            return (amountToCheck >= minValue && amountToCheck <= maxValue);
        }

        /// <summary>
        /// Performs following validations on passed number
        ///     1. Be all digits only
        ///     2. Be the expected length as per the card scheme
        ///     3. Pass the Mod10/LUHN Check
        /// </summary>
        /// <param name="cardNumber">Card Number as per Card Scheme</param>
        /// <param name="cardScheme">Card Scheme (VISA, Master, AmericanExpress)</param>
        /// <returns>true if the card number is valid, otherwise false</returns>
        /// <remarks>
        /// Refer here for MOD10 algorithm: https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </remarks>
        public static bool IsValidCardNumber(string cardNumber, CardScheme cardScheme)
        {
            // Handle null cardNumber.
            if (cardNumber == null)
            {
                throw new ArgumentNullException("cardNumber");
            }

            // Handle blank cardNumber.
            if (cardNumber.Trim() == string.Empty)
            {
                throw new ArgumentException("Zero-length string can not be specified as Card Number. ", "cardNumber");
            }

            // Handle Alphanumeric cardNumber.
            if (!Regex.IsMatch(cardNumber.Trim(), @"^[0-9]+$"))
            {
                throw new ArgumentException("Alphanumeric string can not be specified as Card Number. ", "cardNumber");
            }

            switch (cardScheme)
            {
                case CardScheme.Visa:
                    if (cardNumber.Trim().Length != 16) return false;
                    if ((int)char.GetNumericValue(cardNumber[0]) != 4) return false;
                    break;
                case CardScheme.Mastercard:
                    if (cardNumber.Trim().Length != 16) return false;
                    if ((int)char.GetNumericValue(cardNumber[0]) != 5 || (int)char.GetNumericValue(cardNumber[1]) == 0 || (int)char.GetNumericValue(cardNumber[1]) > 5) return false;
                    break;
                case CardScheme.AmericanExpress:
                    if (cardNumber.Trim().Length != 15) return false;
                    if ((int)char.GetNumericValue(cardNumber[0]) != 3 || ((int)char.GetNumericValue(cardNumber[1]) != 4 && (int)char.GetNumericValue(cardNumber[1]) != 7)) return false;
                    break;
                default:
                    return false;
            }

            return Mod10Check(cardNumber.Trim());
        }
       
        private static bool Mod10Check(string creditCardNumber)
        {
            var sum = creditCardNumber.Reverse()
                              .Select(TransformDigit)
                              .Sum();

            return sum % 10 == 0;
        }
        private static int TransformDigit(char digitChar, int position)
        {
            int digit = int.Parse(digitChar.ToString());
            if (position % 2 == 1)
                digit *= 2;

            return digit % 10 + digit / 10;
        }

    }
}
