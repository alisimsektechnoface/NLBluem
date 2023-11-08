using NLBluem.Structure.Net.Response;
using NLBluem.ValueObjects;

namespace NLBluem.Net.Response
{
    public class BluemPaymentStatusResponse : IBluemResponse
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public EntranceCode EntranceCode { get; set; }

        public string PaymentReference { get; set; }

        public string DebtorReference { get; set; }

        public string TransactionId { get; set; }

        public string PaymentStatus { get; set; }

        public double Amount { get; set; }

        public double AmountPaid { get; set; }
        public string Response { get; set; }
    }
}