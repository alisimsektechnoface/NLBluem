using NLBluem.Net.Response;
using NLBluem.Structure.Net.Response;
using NLBluem.ValueObjects;
using System.Net;

namespace NLBluem.Net.Transform.Transformers
{
    public class PaymentTransactionResponseTransformer : AbstractHttpResponseMessageTransformer
    {
        public override string NodeName => "PaymentTransactionResponse";

        public override IBluemResponse GetBluemResponse(HttpWebResponse response)
        {
            var xml = EPaymentInterface(response);

            return new BluemTransactionResponse
            {
                Status = true,
                DebtorReference = xml.Element("DebtorReference").Value,
                EntranceCode = new EntranceCode(xml.Attribute("entranceCode").Value),
                PaymentReference = xml.Element("PaymentReference").Value,
                TransactionId = xml.Element("TransactionID").Value,
                TransactionUrl = xml.Element("TransactionURL").Value,
                ShortTransactionUrl = xml.Element("ShortTransactionURL").Value,
                Response = xml.ToString()
            };
        }

        public override async Task<IBluemResponse> GetBluemResponse(HttpResponseMessage response)
        {
            var xml = await EPaymentInterfaceAsync(response);
            var entranceCode = new EntranceCode(xml.Attribute("entranceCode").Value);
            return new BluemTransactionResponse
            {
                Status = true,
                DebtorReference = xml.Element("DebtorReference").Value,
                EntranceCode = entranceCode,
                PaymentReference = xml.Element("PaymentReference").Value,
                TransactionId = xml.Element("TransactionID").Value,
                TransactionUrl = xml.Element("TransactionURL").Value,
                ShortTransactionUrl = xml.Element("ShortTransactionURL").Value,
                Response = xml.ToString()
            };
        }
    }
}