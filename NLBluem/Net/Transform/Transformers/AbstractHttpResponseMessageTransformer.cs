using NLBluem.Structure.Exceptions;
using NLBluem.Structure.Net.Response;
using NLBluem.Structure.Net.Transform;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;

namespace NLBluem.Net.Transform.Transformers
{
    public abstract class AbstractHttpResponseMessageTransformer : IHttpResponseMessageTransformer
    {
        private const string EPaymentInterfaceNode = "EPaymentInterface";

        public abstract string NodeName { get; }

        public abstract IBluemResponse GetBluemResponse(HttpWebResponse response);

        public XElement EPaymentInterface(HttpWebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var el = XDocument.Parse(reader.ReadToEnd())
                    .Element(EPaymentInterfaceNode);

                if (null == el.Element(NodeName))
                    throw new BluemTransformException($"Element with name {NodeName} could not be found.");

                if (Convert.ToBoolean(el.Element(NodeName).XPathEvaluate(@"boolean(//Error)")))
                {
                    var code = el.XPathSelectElement("//Error/ErrorCode");
                    var message = el.XPathSelectElement("//Error/ErrorMessage");
                    throw new BluemTransformException($"An error with code {code.Value} has occured: {message.Value}");
                }

                return el.Element(NodeName);
            }
        }

        public abstract Task<IBluemResponse> GetBluemResponse(HttpResponseMessage response);

        public async Task<XElement> EPaymentInterfaceAsync(HttpResponseMessage response)
        {
            using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var el = XDocument.Parse(reader.ReadToEnd())
                    .Element(EPaymentInterfaceNode);

                if (null == el.Element(NodeName))
                    throw new BluemTransformException($"Element with name {NodeName} could not be found.");

                if (Convert.ToBoolean(el.Element(NodeName).XPathEvaluate(@"boolean(//Error)")))
                {
                    var code = el.XPathSelectElement("//Error/ErrorCode");
                    var message = el.XPathSelectElement("//Error/ErrorMessage");
                    throw new BluemTransformException($"An error with code {code.Value} has occured: {message.Value}");
                }

                return el.Element(NodeName);
            }
        }
    }
}