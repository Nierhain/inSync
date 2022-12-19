using System;
using System.Net;

namespace inSync.Api.Utils
{
    public class MediatorResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class Response<T> : MediatorResponse where T : class, new()
    {
        public T Data { get; set; } = new();

        public static Response<T> OK(T data)
        {
            return new() { Data = data};
        }

        public static Response<T> BadRequest(string message)
        {
            return new() { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = message };
        }

        public static Response<T> Unauthorized(string message)
        {
            return new() { StatusCode = HttpStatusCode.Unauthorized, ErrorMessage = message};
        }

        public static Response<T> NotFound(string message)
        {
            return new() { StatusCode = HttpStatusCode.NotFound, ErrorMessage = message };
        }
    }
}

