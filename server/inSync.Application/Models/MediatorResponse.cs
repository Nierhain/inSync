#region

using System.Net;

#endregion

namespace inSync.Application.Models;

public class MediatorResponse
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string ErrorMessage { get; set; } = string.Empty;
}

public class Response<T> : MediatorResponse where T : new()
{
    public T Data { get; set; } = new();

    public static Response<T> OK(T data)
    {
        return new Response<T> { Data = data };
    }

    public static Response<T> Created(T data)
    {
        return new Response<T> { Data = data, StatusCode = HttpStatusCode.Created, ErrorMessage = "" };
    }

    public static Response<T> BadRequest(string message)
    {
        return new Response<T> { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = message };
    }

    public static Response<T> Unauthorized(string message)
    {
        return new Response<T> { StatusCode = HttpStatusCode.Unauthorized, ErrorMessage = message };
    }

    public static Response<T> NotFound(string message)
    {
        return new Response<T> { StatusCode = HttpStatusCode.NotFound, ErrorMessage = message };
    }
}