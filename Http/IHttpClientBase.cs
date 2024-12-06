namespace System.Net.Http;

public interface IHttpClientBase: IScoped
{
    Task<T?> SendAsync<T>(IRequest req);
}
