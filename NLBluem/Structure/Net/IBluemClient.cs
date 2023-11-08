using NLBluem.Structure.Net.Request;
using NLBluem.Structure.Net.Response;

namespace NLBluem.Net.Transform
{
    public interface IBluemClient
    {
        //IBluemResponse SendRequest(IBluemSignedRequest request);
        Task<IBluemResponse> SendRequestAsync(IBluemSignedRequest request);
    }
}