using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ReactiveBlazor;
public abstract class BaseComponent : OwningComponentBase
{
    [Inject] protected IJSRuntime JSRuntime { get; set; } = default!;

    protected Guid Key { get; set; } = Guid.NewGuid();

    protected virtual string? Path { get; set; }

    protected virtual IJSObjectReference? JSObjectReference { get; set; }

    [Parameter] public string? Id { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public string? StyleClass { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Path != null)
        {
            JSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",Path);
        }
    }
}

