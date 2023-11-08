# NLBluem - Bluem payment & identity services

NLBluem is a .NET Core 7 library that enables usage of the [Bluem](https://bluem.nl/) Viamijnbank payment provider.

# Usage

## Configration
```csharp
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
```
## Basic Example
## CreateTransactionRequest
```csharp
  var signage = new Signage();
  var paymentRequest = new BluemRequestFactory()
        .CreateRequest(new BluemPaymentRequestType()) as IBluePaymentRequest;
  paymentRequest.AddPaymentData(paymentReference, debtorReference, description, amount, dueDateTime);
  
  BluemClient client = new BluemClient();
  var signedRequest = signage.SignRequest(paymentRequest);
  var response = await client.SendRequestAsync(signedRequest);
  return response;
```
## CreateTransactionStatusRequest
```csharp
  var paymentRequest = new BluemRequestFactory()
      .CreateRequest(new BluemPaymentStatusRequestType()) as IBluePaymentStatusRequest;
  paymentRequest.AddStatusData(new payment.Bluem.ValueObjects.EntranceCode(entranceCode), transactionId);

  BluemClient client = new BluemClient();
  var signage = new Signage();
  var signedRequest = signage.SignRequest(paymentRequest);
  BluemPaymentStatusResponse response = (BluemPaymentStatusResponse)await client.SendRequestAsync(signedRequest);
```
