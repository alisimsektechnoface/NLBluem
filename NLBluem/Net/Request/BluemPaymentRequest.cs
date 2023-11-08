using NLBluem.Net.RequestTypes;
using NLBluem.Structure.Enums;
using NLBluem.Structure.Net.Request;
using NLBluem.ValueObjects;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NLBluem.Net.Request
{
    public class BluemPaymentRequest : AbstractBluemRequest, IBluePaymentRequest
    {
        internal BluemPaymentRequest(BluemRequestType requestType) : base(requestType)
        {
        }

        public void AddPaymentData(string paymentReference, string debtorReference, string description, double amount, DateTime dueDateTime)
        {
            var paymentXml = BuildXml(new EntranceCode(), paymentReference, debtorReference, description, amount, dueDateTime);

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

        private XElement BuildXml(EntranceCode entranceCode, string paymentReference, string debtorReference, string description, double amount, DateTime dueDateTime)
        {
            var returnUrl = BluemSettings.ReturnUrl
                .Replace("{entranceCode}", entranceCode.ToString())
                .Replace("{paymentReference}", paymentReference)
                .Replace("{debtorReference}", debtorReference);

            return new XElement("EPaymentInterface",
                new XAttribute("type", "TransactionRequest"),
                new XAttribute("mode", "direct"),
                new XAttribute("senderID", BluemSettings.SenderId),
                new XAttribute("version", "1.0"),
                new XAttribute("createDateTime", GetBluemRequestType().DateTime.ToUtc().ToString("yyyy-MM-ddTHH:mm:ss.000Z")),
                new XAttribute("messageCount", 1),

                new XElement("PaymentTransactionRequest",
                    new XAttribute("entranceCode", entranceCode),
                    new XAttribute("brandID", BluemSettings.BrandId),
                    new XAttribute("documentType", "PayRequest"),
                    new XAttribute("sendOption", "none"),
                    new XAttribute("language", "nl"),

                    new XElement("PaymentReference", paymentReference),
                    new XElement("DebtorReference", debtorReference),
                    new XElement("Description", description),
                    //new XElement("SkinID", BluemConfiguration.SkinId),
                    new XElement("Currency", BluemSettings.Currency),
                    new XElement("Amount", amount.ToString("F2", CultureInfo.InvariantCulture)),
                    new XElement("DueDateTime", dueDateTime.ToUtc().ToString("yyyy-MM-ddTHH:mm:ss.000Z")),
                    new XElement("DebtorReturnURL", new XAttribute("automaticRedirect", "1"), returnUrl),
                    new XElement("DebtorWallet",
                        new XElement("IDEAL",
                            new XElement("BIC", "RABONL2U")
                        )
                    )
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

    public interface IBluePaymentRequest : IBluemRequest
    {
        void AddPaymentData(string paymentReference, string debtorReference, string description, double amount, DateTime dueDateTime);
    }

    public static class UtcDateTimeExtension
    {
        public static DateTime ToUtc(this DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks, DateTimeKind.Utc);
        }
    }
}