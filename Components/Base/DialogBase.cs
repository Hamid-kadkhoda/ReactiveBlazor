using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor;

public abstract class DialogBase : BaseComponent
{
    [Parameter] public string? Header { get; set; }

    [Parameter] public RenderFragment? HeaderContent { get; set; }

    [Parameter] public RenderFragment? FooterContent { get; set; }

    protected string visibilityStyleClass = "";

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
