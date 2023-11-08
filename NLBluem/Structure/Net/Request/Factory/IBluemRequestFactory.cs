using NLBluem.Net.RequestTypes;

namespace NLBluem.Structure.Net.Request.Factory
{
    public interface IBluemRequestFactory
    {
        IBluemRequest CreateRequest(BluemRequestType requestType);
    }
}