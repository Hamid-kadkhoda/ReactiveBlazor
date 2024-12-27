using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor;

public class DropDownBase<TValue> : BaseComponent
{
    [Parameter] public IEnumerable<TValue>? Options { get; set; }

    [Parameter] public object? Value { get; set; }

    [Parameter] public string? OptionValue { get; set; }

    [Parameter] public string? OptionLabel { get; set; }

    [Parameter] public string? Placeholder { get; set; }


    /// <summary>
    /// Height of the viewport in pixels, a scrollbar is defined if height of list exceeds this value.
    /// </summary>
    [Parameter] public string ScrollHeight { get; set; } = "200px";


    /// <summary>
    /// Text to display when there is no data. Defaults to global value in i18n translation configuration.
    /// </summary>
    [Parameter] public string? EmptyMessage { get; set; }

    [Parameter] public string? FilterBy { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Required { get; set; }


    /// <summary>
    /// Whether the dropdown is in loading state.
    /// </summary>
    [Parameter] public bool Loading { get; set; }


    /// <summary>
    /// When enabled, a clear icon is displayed to clear the value.
    /// </summary>
    [Parameter] public bool ShowClear { get; set; }

    [Parameter] public bool AllowFilter { get; set; }


    /// <summary>
    /// Whether to display options as grouped when nested options are provided.
    /// </summary>
    [Parameter] public bool Group { get; set; }

    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
}
