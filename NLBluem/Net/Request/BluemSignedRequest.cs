using NLBluem.Net.RequestTypes;
using NLBluem.Structure.Enums;
using NLBluem.Structure.Net.Request;
using NLBluem.ValueObjects;

namespace NLBluem.Net.Request
{
    class BluemSignedRequest : IBluemSignedRequest
    {
        public ValueObjects.BluemFile File { get; set; }
        public BluemDateTime DateTime { get; set; }
        public BluemContentTypeEnum ContentType { get; set; }
        public BluemRequestType RequestType { get; set; }
        public string UnsignedString { get; set; }
    }
}