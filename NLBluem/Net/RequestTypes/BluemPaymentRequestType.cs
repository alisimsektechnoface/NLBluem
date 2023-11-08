using NLBluem.Net.Transform.Transformers;
using NLBluem.Structure.Enums;
using NLBluem.Structure.Net.Transform;

namespace NLBluem.Net.RequestTypes
{
    public class BluemPaymentRequestType : BluemRequestType
    {
        public override Uri RequestUri => BluemSettings.PaymentRequestUri;

        public override HttpMethod RequestMethod => HttpMethod.Post;

        public override BluemContentTypeEnum ContentType => BluemContentTypeEnum.PTX;

        public override IHttpResponseMessageTransformer Transformer => new PaymentTransactionResponseTransformer();
    }
}