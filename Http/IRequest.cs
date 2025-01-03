namespace System.Net.Http;

public interface IRequest
{
    public string Endpoint { get; }

    public string? Fragments { get; set; }

    public HttpMethod HttpMethod { get; }
}
