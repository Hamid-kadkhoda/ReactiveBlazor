
namespace ReactiveBlazor.Components.Toast;

public class ToastService : IToastService
{
    private List<ToastMessage> Messages { get; set; } = [];

    public event Action? ItemsChanged;

    private void NotifyItemsChanged() => ItemsChanged!.Invoke();

    public List<ToastMessage> GetValues() => Messages;

    public void Show(ToastMessage message)
    {
        Add(message);
    }

    private void Add(ToastMessage message)
    {
        Messages.Add(message);
        NotifyItemsChanged();
    }

    public void RemoveMessage(Guid key)
    {
        var stack = Messages.First(x => x.Key == key);
        Messages.Remove(stack);
    }

    public void Dispose()
    {
        // implement logic
    }
}
