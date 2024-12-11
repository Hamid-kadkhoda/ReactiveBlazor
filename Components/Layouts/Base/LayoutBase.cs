using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor.Layout;

public class LayoutBase: BaseComponent
{
    [Parameter] public bool Vertical { get; set; }

    [Parameter] public string Spacing { get; set; } = "1";

    [Parameter] public bool Full { get; set; } = false;
}
