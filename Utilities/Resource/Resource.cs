namespace ReactiveBlazor;

public class Resource<T> : IDisposable where T : class
{
    private bool _disposed;

    private T? _Value { get; set; }

    public Resource(T? value)
    {
        _Value = value;
    }
    
    public async Task<TResult> ExecuteAsync<TResult>(Func<T, Task<TResult>> operation)
    {
        return await operation(_Value!);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _Value = null;
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }
}
