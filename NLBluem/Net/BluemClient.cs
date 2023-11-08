using NLBluem.Net.Transform;
using NLBluem.Structure.Enums;
using NLBluem.Structure.Exceptions;
using NLBluem.Structure.Net.Request;
using NLBluem.Structure.Net.Response;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;

namespace NLBluem.Net
{
    public class BluemClient : IBluemClient
    {
        public BluemClient()
        {

        }

        public async Task<IBluemResponse> SendRequestAsync(IBluemSignedRequest request)
        {
            var bluemContentType = Enum.GetName(typeof(BluemContentTypeEnum), request.ContentType);
            var xmlData = request.File.File.ReadAsStringAsync().Result;

            var client = new HttpClient();
            var requestWeb = new HttpRequestMessage(HttpMethod.Post, request.RequestType.RequestUri);
            try
            {
                requestWeb.Headers.Add("x-ttrs-files-count", "1");
                requestWeb.Headers.Add("x-ttrs-filename", $"{request.File.Filename}");
                requestWeb.Headers.Add("x-ttrs-date", request.DateTime.ToString());
                var content = new StringContent(xmlData, null, MediaTypeHeaderValue.Parse($"application/xml;type={bluemContentType};charset=utf-8"));

                requestWeb.Content = content;
                var response = await client.SendAsync(requestWeb);
                var res = await response.Content.ReadAsStringAsync();
                var transformer = request.RequestType.Transformer;

                return await transformer.GetBluemResponse(response);

            }
            catch (WebException e)
            {
                var report = DumpReport(request.UnsignedString, requestWeb, xmlData);

                throw new BluemRequestException(e);
            }
        }

        private string DumpReport(string unsignedString, HttpRequestMessage request, string formData)
        {
            var report = $"Unsigned string:\r\n{unsignedString}\r\nHeaders:\r\n";
            foreach (var header in request.Headers)
                report += $"{header.Key}: {header.Value}\r\n";
            report += $"\r\nFormdata:\r\n{formData}";

            if (BluemSettings.DumpReports)
            {

                Debug.WriteLine(report);
            }

            return report;
        }
    }
}