
namespace System.Net.Http;

public abstract class RequestBase : IRequest
{
    public string Endpoint { get; set; }

    public string? QueryParams { get; set; } = null;

    public string? Fragments { get; set; } = null;

    public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

    protected RequestBase(string endpoint)
    {
        Endpoint = endpoint;
    }

    protected RequestBase(string endpoint, HttpMethod httpMethod)
    {
        Endpoint = endpoint;
        HttpMethod = httpMethod;
    }
}
