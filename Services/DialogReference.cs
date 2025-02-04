namespace ReactiveBlazor;

public class DialogReference : IDisposable
{
    private TaskCompletionSource<object?> _taskCompletionSource;
    public RenderFragment? DialogContent { get; private set; }
    public Action OnClose { get; private set; }

    public void Dispose()
    {
        _taskCompletionSource?.TrySetCanceled();
    }

    public void Initialize(RenderFragment dialogContent, Action onClose)
    {
        DialogContent = dialogContent;
        OnClose = onClose;
        _taskCompletionSource = new TaskCompletionSource<object?>();
    }

    public void Close() => _taskCompletionSource.TrySetResult(null);
    public Task Closed => _taskCompletionSource.Task;
}
