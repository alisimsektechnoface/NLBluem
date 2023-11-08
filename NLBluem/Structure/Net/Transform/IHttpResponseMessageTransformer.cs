using NLBluem.Structure.Net.Response;
using System.Net;
using System.Xml.Linq;

namespace NLBluem.Structure.Net.Transform
{
    public interface IHttpResponseMessageTransformer
    {
        IBluemResponse GetBluemResponse(HttpWebResponse response);
        XElement EPaymentInterface(HttpWebResponse response);
        Task<XElement> EPaymentInterfaceAsync(HttpResponseMessage response);
        Task<IBluemResponse> GetBluemResponse(HttpResponseMessage response);

        string NodeName { get; }
    }
}