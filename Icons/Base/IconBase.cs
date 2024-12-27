using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor;

public abstract class IconBase: BaseComponent
{
    [Parameter] public int? Width { get; set; }

    [Parameter] public int? Height { get; set; }

    [Parameter] public string Stroke { get; set; } = "#495057";

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? Fill { get; set; }
}
