using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor.Components.Base;
public abstract class BaseComponent : ComponentBase
{
    [Parameter] public string? Style { get; set; }

    [Parameter] public string? StyleClass { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }
}

