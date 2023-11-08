using System.Net.Http;
using NLBluem.Structure.Net.Response;

namespace NLBluem.Net.Response
{
    public class BluemResponse : IBluemResponse
    {
        public bool Status { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public HttpContent Content { get; set; }
    }
}