namespace ReactiveBlazor.Components.Confirmation;

public class ConfirmationService : IConfirmationService
{
    private TaskCompletionSource<bool> _taskCompletionSource { get; set; } = new();


    public void Confirm(string message, Func<Task> OnApproved, Func<Task> OnCancel)
    {
        
    }

    private void Yes()
    {
        _taskCompletionSource.SetResult(true);
    }

    private void No()
    {
        _taskCompletionSource.SetResult(false);
    }
}
