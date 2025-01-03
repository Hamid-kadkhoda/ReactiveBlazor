namespace ReactiveBlazor;
public interface IHttpClientBase: IScoped
{
    Task<T?> SendAsync<T>(IRequest req);
}
