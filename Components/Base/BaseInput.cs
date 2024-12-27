using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor;

public abstract class BaseInput : BaseComponent
{
    [Parameter] public bool Required { get; set; }

    [Parameter] public string? Label { get; set; }

    [Parameter] public string? Placeholder { get; set; }

    [Parameter] public int? MaxLength { get; set; }

    [Parameter] public int? MinLength { get; set; }

    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    [Parameter] public string? Value { get; set; }

    public async Task SetValue(ChangeEventArgs args)
    {
        await ValueChanged.InvokeAsync((string)args.Value!);
    }
}
