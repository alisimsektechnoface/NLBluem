namespace NLBluem
{
    public class BluemSettings
    {
        public static string Url { get; set; } = "https://test.viamijnbank.net/";
        public static string Token { get; set; } = "Token";
        public static string? SenderId { get; set; } = "SenderId";
        public static string? ReturnUrl { get; set; } = "hostname.com/payment-redirect?entranceCode={entranceCode}&paymentReference={paymentReference}&debtorReference={debtorReference}";
        public static string BrandId { get; set; } = "Payment";
        public static Uri PaymentRequestUri { get; set; } = new Uri(string.Concat(Url, "pr/createTransactionWithToken?token=", Token));
        public static Uri PaymentStatusUri { get; set; } = new Uri(string.Concat(Url, "pr/requestTransactionStatusWithToken?token=", Token));
        public static string Currency { get; set; } = "EUR";
        public static bool DumpReports { get; set; } = false;
    }
}
