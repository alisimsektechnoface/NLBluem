using NLBluem.Net.Request;
using NLBluem.Structure.Net.Request;
using NLBluem.Structure.Security;
using System.Security.Cryptography.X509Certificates;

namespace NLBluem.Security
{
    public class Signage : ISignage
    {
        private readonly X509Store _certStore;

        public Signage()
        {
        }
        public Signage(X509Store certStore)
        {
            _certStore = certStore;
        }

        public IBluemSignedRequest SignRequest(IBluemRequest request)
        {
            var requestType = request.GetBluemRequestType();

            return new BluemSignedRequest
            {
                DateTime = requestType.DateTime,
                File = requestType.File,
                ContentType = requestType.ContentType,
                RequestType = requestType,
                UnsignedString = request.BuildSignaturePayload()
            };
        }
    }
}