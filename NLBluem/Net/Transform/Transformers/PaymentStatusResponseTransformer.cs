using NLBluem.Net.Response;
using NLBluem.Structure.Net.Response;
using NLBluem.ValueObjects;
using System.Net;

namespace NLBluem.Net.Transform.Transformers
{
    public class PaymentStatusResponseTransformer : AbstractHttpResponseMessageTransformer
    {
        public override string NodeName => "PaymentStatusUpdate";

        public override IBluemResponse GetBluemResponse(HttpWebResponse response)
        {
            var xml = EPaymentInterface(response);

            return new BluemPaymentStatusResponse
            {
                Status = true,
                DebtorReference = xml.Element("DebtorReference").Value,
                EntranceCode = new EntranceCode(xml.Attribute("entranceCode").Value),
                PaymentReference = xml.Element("PaymentReference").Value,
                TransactionId = xml.Element("TransactionID").Value,
                PaymentStatus = xml.Element("Status").Value,
                Response = xml.ToString()
            };
        }

        public override async Task<IBluemResponse> GetBluemResponse(HttpResponseMessage response)
        {
            var xml = await EPaymentInterfaceAsync(response);

            return new BluemPaymentStatusResponse
            {
                Status = true,
                DebtorReference = xml.Element("DebtorReference").Value,
                EntranceCode = new EntranceCode(xml.Attribute("entranceCode").Value),
                PaymentReference = xml.Element("PaymentReference").Value,
                TransactionId = xml.Element("TransactionID").Value,
                PaymentStatus = xml.Element("Status").Value,
                Response = xml.ToString()
            };
        }
    }
}