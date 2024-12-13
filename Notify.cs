
namespace ReactiveBlazor;
public static class Notify
{
    public static event Action? StateHasChanged;

    public static void StateHasChanging() => StateHasChanged!.Invoke();
}
