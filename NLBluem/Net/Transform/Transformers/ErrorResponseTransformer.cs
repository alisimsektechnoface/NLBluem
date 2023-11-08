using NLBluem.Net.Response;
using NLBluem.Structure.Net.Response;
using System.Net;

namespace NLBluem.Net.Transform.Transformers
{
    public class ErrorResponseTransformer : AbstractHttpResponseMessageTransformer
    {
        public override string NodeName => "PaymentErrorResponse";

        public override IBluemResponse GetBluemResponse(HttpWebResponse response)
        {
            var xml = EPaymentInterface(response);
            var error = xml.Element("Error");

            return new BluemErrorResponse()
            {
                ErrorCode = error.Element("ErrorCode").Value,
                ErrorMessage = error.Element("ErrorMessage").Value,
                Object = error.Element("Object").Value
            };
        }

        public override async Task<IBluemResponse> GetBluemResponse(HttpResponseMessage response)
        {
            var xml = await EPaymentInterfaceAsync(response);
            var error = xml.Element("Error");

            return new BluemErrorResponse()
            {
                ErrorCode = error.Element("ErrorCode").Value,
                ErrorMessage = error.Element("ErrorMessage").Value,
                Object = error.Element("Object").Value
            };
        }
    }
}