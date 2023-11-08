using NLBluem.Structure.Net.Response;
using NLBluem.ValueObjects;

namespace NLBluem.Net.Response
{
    public class BluemTransactionResponse : IBluemResponse
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public EntranceCode EntranceCode { get; set; }

        public string PaymentReference { get; set; }

        public string DebtorReference { get; set; }

        public string TransactionId { get; set; }

        public string TransactionUrl { get; set; }

        public string ShortTransactionUrl { get; set; }
        public string Response { get; set; }
    }
}