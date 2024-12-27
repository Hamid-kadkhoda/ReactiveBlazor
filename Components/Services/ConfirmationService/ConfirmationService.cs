namespace ReactiveBlazor;

public static class ConfirmationService
{
    private static TaskCompletionSource<bool>? _currentConfirmation;
    public static event Action<string>? OnConfirmationRequested;

    public static void HandleConfirmationResponse(bool confirmed)
    {
        if (_currentConfirmation != null)
        {
            _currentConfirmation.SetResult(confirmed);
            _currentConfirmation = null;
        }
    }

    public static async Task<bool> ConfirmAsync(string message)
    {
        if (_currentConfirmation != null)
        {
            throw new InvalidOperationException("Another confirmation dialog is already active");
        }

        _currentConfirmation = new TaskCompletionSource<bool>();
        OnConfirmationRequested?.Invoke($"you maybe have unsaved changes, Are you sure?");

        return await _currentConfirmation.Task;
    }

    public static async Task<bool> ConfirmWithCallbackAsync(
        string message,
        Func<Task> onConfirm,
        Func<Task>? onCancel = null)
    {
        try
        {
            var isConfirmed = await ConfirmAsync(message);

            if (isConfirmed)
            {
                await onConfirm();
                return true;
            }
            else
            {
                if (onCancel != null)
                {
                    await onCancel();
                }
                return false;
            }
        }
        catch (Exception)
        {
            if (onCancel != null)
            {
                await onCancel();
            }
            return false;
        }
    }
}
