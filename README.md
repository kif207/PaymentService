# Payment Service

## Overview
A web-based solution that Implements PaymentValidator and mock payment page:

### PaymentValidator Methods

**IsValidCardNumber**: 

A success response (Boolean: true) would be returned when the input meets ALL the below conditions

1. Is all digits only 
2. Is the expected length as per the card scheme:
    - Visa/Mastercard: 16 digits
    - AmericanExpress: 15 digits
3. Pass the Mod10/LUHN Check
    - Details here: https://en.wikipedia.org/wiki/Luhn_algorithm 


**IsValidAmount**: 

A success response (Boolean: true) would be returned if the input meets ALL the below listed conditions

1. Is a positive number
2. Falls in the range 0 to 999999

**IsValidPaymentTransaction**: 

Returns true when all of the below listed conditions are met:

1. The card number is valid as per the rules stated in IsValidCardNumber above.
2. The amount is valid as per the rules stated in IsValidAmount above.


###Mock Payment Page Fields and Constraints###

1. Card Holder First Name and Last Name:
    - Required
    - String only
    - Maximum of 255 characters
	
2. Card Number:
    - Required
    - Only digits
    - Length depends on selected Card Type:
		- Visa/Mastercard: 16
		- American Express: 15

3. Expiry Month
    - Required 
    - Two digits
    - Between 01 and 12

4. Expiry Year
    - Required
    - Four digits
    - Must be either current year or future

5. Expiry Month + Expiry year should be a date at least 1 month in the future. E.g. In July 2016, 08 2016 is acceptable whereas anything less than that is not.

6. CVV/CVN
    - Required
    - Numbers only
    - Max length 3 digits

7. Card Type (Drop down)
    - Visa
    - Mastercard
    - American Express

8. Clicking the submit button will trigger validation on all fields. 

9. When all fields have valid data, clicking the submit button will show a message *Payment will be processed*
		

## Running the Application
The application has been developed in Visual Studio 2013 using AngularJS for Front-End and Asp&#46;Net Web API 2.0 (C#) as Back-End. Below are the steps for running the application.

1. [Clone](https://help.github.com/articles/cloning-a-repository/) the repository to create a local copy on your computer.
2. Open **PaymentService** Solution in Visual Studio.
3. Build the solution through Visual Studio. This will restore all NuGet packages during the build.
4. Select the project **PaymentService.Web** and press (CTRL+F5). This will open the sample Payment Page.

## Running Tests

### Unit Tests

Unit tests are implemented using MSTest and are part of the **PaymentService.Tests** project. These tests can be run using the Test Explorer within Visual Studio. For more details, please visit [How to: Run Tests from Microsoft Visual Studio](https://msdn.microsoft.com/en-us/library/ms182470(v=vs.120).aspx) 

