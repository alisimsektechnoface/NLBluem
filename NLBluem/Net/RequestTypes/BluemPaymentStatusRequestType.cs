using NLBluem.Net.Transform.Transformers;
using NLBluem.Structure.Enums;
using NLBluem.Structure.Net.Transform;

namespace NLBluem.Net.RequestTypes
{
    public class BluemPaymentStatusRequestType : BluemRequestType
    {
        public override Uri RequestUri => BluemSettings.PaymentStatusUri;

        public override HttpMethod RequestMethod => HttpMethod.Post;

        public override BluemContentTypeEnum ContentType => BluemContentTypeEnum.PSX;

        public override IHttpResponseMessageTransformer Transformer => new PaymentStatusResponseTransformer();
    }
}