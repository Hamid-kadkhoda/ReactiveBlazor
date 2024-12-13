namespace ReactiveBlazor.Utilities;
public class Resource<T> : IDisposable where T : class
{
    private readonly SemaphoreSlim _semaphore;

    private bool _disposed;

    private T? _Value { get; set; }

    public Resource(T? value)
    {
        _Value = value;
        _semaphore = new SemaphoreSlim(1, 1);
    }

    public async Task<TResult> ExecuteAsync<TResult>(Func<T, Task<TResult>> operation)
    {
        await _semaphore.WaitAsync();
        try
        {
            return await operation(_Value!);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _semaphore.Dispose();
            _Value = null;
            _disposed = true;
        }
    }
}
