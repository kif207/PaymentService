/*
 * Copyright 2016, EP2 Payments.
 * The code in this file is confidential, you may not share the contents without prior written approval of EP2 Payments.
 * Please contact EP2 Payments at info@ep2payments.com for any questions or concerns
 */
using System;

namespace PaymentService.Common
{

    /// <summary> Defines a contract for validating payment instruments and amounts </summary>
    public interface IPaymentValidator
    {
        /// <summary>
        /// Returns the pre-assigned Candidate GUID
        /// </summary>
        /// <returns></returns>
        string CandidateId();

        /// <summary>
        /// Checks whether the passed in card number is valid
        /// </summary>
        /// <remarks>
        /// To be a valid card number, the passed string should pass the following checks:
        /// 1. Be all digits only
        /// 2. Be the expected length as per the card scheme
        /// 3. Pass the Mod10/LUHN Check
        /// </remarks>
        /// <param name="cardNumber">The card number</param>
        /// <returns>true if valid, otherwise false</returns>
        bool IsValidCardNumber(string cardNumber, CardScheme cardScheme);

        /// <summary>
        /// Checks whether the passed amount is a valid transaction amount
        /// </summary>
        /// <remarks>
        /// To be valid, the amount must be:
        /// 1. A positive number
        /// 2. Fall in the range 0 to 999999
        /// </remarks>
        /// <param name="amount">The amount in cents </param>
        /// <returns>true if valid, otherwise false</returns>
        bool IsValidAmount(long amount);

        /// <summary>
        /// Checks whether a payment transaction can be made using the given card and amount
        /// </summary>
        /// <remarks>
        /// For validating the card, the constraints in <see cref="IsValidCardNumber"/> apply
        /// For validating the amount, the constraints in <see cref="IsValidAmount"/> apply
        /// </remarks>
        /// <param name="cardNumber">A card number</param>
        /// <param name="amount">A transaction amount in cents</param>
        /// <returns>true if transaction can be carried out, otherwise false</returns>
        bool IsValidPaymentTransaction(string cardNumber, long amount, CardScheme cardScheme);
    }
}