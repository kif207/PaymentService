using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using PaymentService.Common;

namespace PaymentService.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:57758", headers: "*", methods: "*")]
    public class PaymentServiceController : ApiController, IPaymentValidator
    {
        // GET: api/paymentservice/CandidateId
        [HttpGet]
        [Route("api/paymentservice/CandidateId")]
        public string CandidateId()
        {
            return "B68D28C1-59C0-406C-82DB-7BFDA3853163";
        }

        // GET: api/paymentservice/IsValidCardNumber/4111111111111111/1
        [HttpGet]
        [Route("api/paymentservice/IsValidCardNumber/{cardNumber}/{cardScheme}")]
        public bool IsValidCardNumber(string cardNumber, CardScheme cardScheme)
        {
            try
            {
                return ValidationsHelper.IsValidCardNumber(cardNumber,cardScheme);
            }
            catch (ArgumentNullException ane)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ane.Message));
            }
            catch (ArgumentException ae)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ae.Message));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        // GET: api/paymentservice/IsValidAmount/10
        [HttpGet]
        [Route("api/paymentservice/IsValidAmount/{amount}")]
        public bool IsValidAmount(long amount)
        {
            try
            {
                return ValidationsHelper.IsAmountInRange(amount, 0, 999999);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        // GET: api/paymentservice/IsValidPaymentTransaction/4111111111111111/10/1
        [HttpGet]
        [Route("api/paymentservice/IsValidPaymentTransaction/{cardNumber}/{amount}/{cardScheme}")]
        public bool IsValidPaymentTransaction(string cardNumber, long amount, CardScheme cardScheme)
        {
            try
            {
                return (ValidationsHelper.IsValidCardNumber(cardNumber, cardScheme) && ValidationsHelper.IsAmountInRange(amount, 0, 999999));
            }
            catch (ArgumentNullException ane)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ane.Message));
            }
            catch (ArgumentException ae)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ae.Message));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
