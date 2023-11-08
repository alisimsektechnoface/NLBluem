using NLBluem.Net.RequestTypes;
using NLBluem.Structure.Net.Request;
using NLBluem.Structure.Net.Request.Factory;

namespace NLBluem.Net.Request.Factory
{
    public class BluemRequestFactory : IBluemRequestFactory
    {
        public IBluemRequest CreateRequest(BluemRequestType requestType)
        {
            if (requestType is BluemPaymentRequestType)
            {
                return new BluemPaymentRequest(requestType);
            }

            if (requestType is BluemPaymentStatusRequestType)
            {
                return new BluemPaymentStatusRequest(requestType);
            }

            return null;
        }
    }
}