namespace System.Net.Http;

public interface IHttpClientBase: IScoped
{
    Task<HttpResponseMessage> SendAsync<T>(IRequest req);
}
