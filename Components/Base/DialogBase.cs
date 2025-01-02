using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor;

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
                if (value)
                {
                    visibilityStyleClass = "dialog-open";
                }
                else
                {
                    visibilityStyleClass = "dialog-close";
                    InvokeAsync(async () =>
                    {
                        await Task.Delay(300); // Wait for the close animation to finish
                        visibility = false;
                        await VisibilityChanged.InvokeAsync(visibility);
                        StateHasChanged();
                    });
                    return;
                }
                visibility = value;
                VisibilityChanged.InvokeAsync(visibility);
                StateHasChanged();
            }
        }
    }

    [Parameter] public EventCallback<bool> VisibilityChanged { get; set; }

    public async Task OnCloseButtonClicked()
    {
        Visibility = false;
        await VisibilityChanged.InvokeAsync(Visibility);
    }
}
