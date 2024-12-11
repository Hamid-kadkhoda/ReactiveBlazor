
namespace ReactiveBlazor.Components.Toast;

public class ToastService : IToastService
{
    private List<ToastMessage> Messages { get; set; } = [];

    public event Action? ItemsChanged;

    private void NotifyItemsChanged() => ItemsChanged!.Invoke();

    public List<ToastMessage> GetValues() => Messages;

    public void Show()
    {
        var message = new ToastMessage() { Text = "Text", Summery = "This is a Summery" };

        Add(message);
        NotifyItemsChanged();
    }

    private void Add(ToastMessage message)
    {
        Messages.Add(message);
    }

    public void RemoveMessage(Guid key)
    {
        var stack = Messages.First(x => x.Key == key);
        Messages.Remove(stack);
    }
}
