using System;
using System.Net;

namespace inSync.Api.Utils
{
    public class MediatorResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}

