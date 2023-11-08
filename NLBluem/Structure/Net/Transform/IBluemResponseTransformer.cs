using System.Net.Http;
using NLBluem.Structure.Net.Request;

namespace NLBluem.Structure.Net.Transform
{
    public interface IBluemResponseTransformer
    {
        HttpRequestMessage GetHttpRequestMessage(IBluemSignedRequest request);
    }
}