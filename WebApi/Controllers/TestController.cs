using Microsoft.AspNetCore.Mvc;
using NLBluem.Net;
using NLBluem.Net.Request;
using NLBluem.Net.Request.Factory;
using NLBluem.Net.RequestTypes;
using NLBluem.Security;
using NLBluem.Structure.Net.Response;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }

        [HttpGet(Name = "CreateTransactionRequest")]
        public async Task<IBluemResponse> Get()
        {

            try
            {
                var signage = new Signage();

                var paymentRequest = new BluemRequestFactory().CreateRequest(new BluemPaymentRequestType()) as IBluePaymentRequest;
                paymentRequest.AddPaymentData("foobar", "qooxdoo", "description", 30.58, DateTime.Today.AddMonths(1));

                BluemClient client = new BluemClient();
                var signedRequest = signage.SignRequest(paymentRequest);
                var response = await client.SendRequestAsync(signedRequest);
                return response;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}