using NLBluem.Structure.Net.Request;

namespace NLBluem.Structure.Security
{
    public interface ISignage
    {
        IBluemSignedRequest SignRequest(IBluemRequest request);
    }
}