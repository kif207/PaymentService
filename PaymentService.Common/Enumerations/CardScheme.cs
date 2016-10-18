/*
 * Copyright 2016, EP2 Payments.
 * The code in this file is confidential, you may not share the contents without prior written approval of EP2 Payments.
 * Please contact EP2 Payments at info@ep2payments.com for any questions or concerns
 */

using System;
namespace PaymentService.Common
{
    public enum CardScheme
    {
        /// <summary> The Visa scheme </summary>
        Visa,

        /// <summary> The Mastercard scheme </summary>
        Mastercard,

        /// <summary> The American Express scheme </summary>
        AmericanExpress
    }
}