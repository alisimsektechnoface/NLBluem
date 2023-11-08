using NLBluem.Net.RequestTypes;
using NLBluem.Structure.Enums;
using NLBluem.ValueObjects;

namespace NLBluem.Structure.Net.Request
{
    public interface IBluemSignedRequest
    {
        ValueObjects.BluemFile File { get; set; }

        BluemDateTime DateTime { get; set; }

        BluemContentTypeEnum ContentType { get; set; }

        BluemRequestType RequestType { get; set; }

        string UnsignedString { get; set; }
    }
}