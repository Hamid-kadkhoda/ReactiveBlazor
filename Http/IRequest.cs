namespace System.Net.Http;

public interface IRequest
{
    string Endpoint { get; }

    string? QueryParams { get; set; }

    string? Fragments { get; set; }

    HttpMethod HttpMethod { get; }
}
