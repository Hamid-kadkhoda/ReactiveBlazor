using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor.Components.Input;

public abstract class BaseInput : BaseComponent
{
    [Parameter] public bool Required { get; set; }

    [Parameter] public string? Label { get; set; }

    [Parameter] public int? MaxLength { get; set; }

    [Parameter] public int? MinLength { get; set; }

    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    private string? value;

    [Parameter] public string? Value
    {
        get
        {
            return value;
        }
        set
        {
            if (!EqualityComparer<string?>.Default.Equals(value, Value))
            {
                Value = value;
                ValueChanged.InvokeAsync(Value);
            }
        }

    }


    public async Task SetValue(ChangeEventArgs args)
    {
        await ValueChanged.InvokeAsync((string)args.Value!);
    }
}
