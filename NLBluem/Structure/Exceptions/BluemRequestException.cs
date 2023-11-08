using System;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace NLBluem.Structure.Exceptions
{
    public class BluemRequestException : Exception
    {
        public string Code { get; set; }

        public string ErrorMessage { get; set; }

        public string Object { get; set; }

        public BluemRequestException(WebException webException)
        {
            var response = webException.Response;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                var aa = reader.ReadToEnd();
                var xml = XDocument.Parse(aa);
                var error = xml.Element("EPaymentInterface").Element("PaymentErrorResponse").Element("Error");
                Code = error.Element("ErrorCode").Value;
                ErrorMessage = error.Element("ErrorMessage").Value;
                Object = error.Element("Object").Value;
            }
        }
    }
}