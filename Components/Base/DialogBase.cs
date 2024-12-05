using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor.Components.Dialog;

public abstract class DialogBase : BaseComponent
{
    [Parameter] public string? Header { get; set; }

    [Parameter] public RenderFragment? HeaderContent { get; set; }

    [Parameter] public RenderFragment? FooterContent { get; set; }

    protected string visibilityStyleClass = "dialog-close";

    private bool visibility = false;

    [Parameter]
    public bool Visibility
    {
        get => visibility;
        set
        {
            if (visibility != value)
            {
                visibility = value;
                HandleVisibilityChange(value).ConfigureAwait(true);
            }
        }
    }

    private async Task HandleVisibilityChange(bool value)
    {
        if (value)
        {
            await Task.Delay(200);
            visibilityStyleClass = "dialog-open";
            await InvokeAsync(StateHasChanged);
        }
        else
        {
            visibilityStyleClass = "dialog-close";
            await Task.Delay(200);
        }
    }

    [Parameter] public EventCallback<bool> VisibilityChanged { get; set; }

    public async Task OnCloseButtonClicked()
    {
        await HandleVisibilityChange(false);
        Visibility = false;
        await VisibilityChanged.InvokeAsync(Visibility);
    }
}
