using NLBluem.Net.RequestTypes;
using NLBluem.Structure.Enums;
using NLBluem.Structure.Net.Request;
using NLBluem.ValueObjects;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NLBluem.Net.Request
{
    public class BluemPaymentStatusRequest : AbstractBluemRequest, IBluePaymentStatusRequest
    {
        internal BluemPaymentStatusRequest(BluemRequestType requestType) : base(requestType)
        {
        }

        public void AddStatusData(EntranceCode entranceCode, string transactionId)
        {
            var paymentXml = BuildXml(entranceCode, transactionId);

            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                paymentXml);

            using (var mem = new MemoryStream())
            using (var writer = new XmlTextWriter(mem, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteTo(writer);
                writer.Flush();
                mem.Flush();
                mem.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(mem))
                {
                    var xml = reader.ReadToEnd();

                    ValueObjects.BluemFile attachment = new ValueObjects.BluemFile
                    {
                        File = new StringContent(xml),
                        Filename = Filename(),
                        FileType = "xml",
                        MimeType = "application/xml"
                    };

                    AddFile(attachment);
                }
            }
        }

        private XElement BuildXml(EntranceCode entranceCode, string transactionId)
        {
            return new XElement("EPaymentInterface",
                new XAttribute("type", "StatusRequest"),
                new XAttribute("mode", "direct"),
                new XAttribute("senderID", BluemSettings.SenderId),
                new XAttribute("version", "1.0"),
                new XAttribute("createDateTime", GetBluemRequestType().DateTime.ToUtc().ToString("yyyy-MM-ddTHH:mm:ss.000Z")),
                new XAttribute("messageCount", 1),

                new XElement("PaymentStatusRequest",
                    new XAttribute("entranceCode", entranceCode),
                    new XAttribute("brandID", BluemSettings.BrandId),

                    new XElement("TransactionID", transactionId)
                )
            );
        }

        private string Filename()
        {
            var request = GetBluemRequestType();
            var requestType = Enum.GetName(typeof(BluemContentTypeEnum), request.ContentType);
            var senderId = BluemSettings.SenderId;
            var timestamp = request.DateTime.Iso8601Timestamp;

            return $"{requestType}-{senderId}-BSP1-{timestamp}.xml";
        }
    }

    public interface IBluePaymentStatusRequest : IBluemRequest
    {
        void AddStatusData(EntranceCode entranceCode, string transactionId);
    }
}