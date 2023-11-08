using NLBluem.Structure.Enums;
using NLBluem.Structure.Net.Transform;
using NLBluem.ValueObjects;

namespace NLBluem.Net.RequestTypes
{
    public abstract class BluemRequestType
    {
        private BluemDateTime _dateTime;

        public abstract Uri RequestUri { get; }

        public abstract HttpMethod RequestMethod { get; }

        public BluemDateTime DateTime => _dateTime ?? (_dateTime = new BluemDateTime(System.DateTime.UtcNow));

        public int FilesCount => 1;

        public ValueObjects.BluemFile File { get; set; }

        public abstract BluemContentTypeEnum ContentType { get; }

        public abstract IHttpResponseMessageTransformer Transformer { get; }
    }
}