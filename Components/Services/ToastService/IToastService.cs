
namespace ReactiveBlazor;

public interface IToastService : ISingleton, IDisposable
{
    List<ToastMessage> GetValues();

    event Action? ItemsChanged;

    void Show(ToastMessage message);

    void RemoveMessage(Guid key);
}
