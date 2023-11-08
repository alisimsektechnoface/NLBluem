using System.Net.Http;
using NLBluem.Structure.Net.Request;
using NLBluem.Structure.Net.Transform;

namespace NLBluem.Net.Transform
{
    public class BluemResponseTransformer : IBluemResponseTransformer
    {
        public HttpRequestMessage GetHttpRequestMessage(IBluemSignedRequest request)
        {
            var type = request.RequestType;

            return new HttpRequestMessage(type.RequestMethod, type.RequestUri);
        }
    }
}