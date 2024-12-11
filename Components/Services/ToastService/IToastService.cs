using System.Collections.ObjectModel;

namespace ReactiveBlazor.Components.Toast;

public interface IToastService : ISingleton
{
    List<ToastMessage> GetValues();

    event Action? ItemsChanged;

    void Show();

    void RemoveMessage(Guid key);
}
