namespace ReactiveBlazor;

public class MenuItem
{
    public string? Label { get; set; }

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public string? Target { get; set; }

    public Action? Command { get; set; }

    public bool Disabled { get; set; } = false;

    public bool Expanded { get; set; } = false;

    public List<MenuItem>? Items { get; set; } = new List<MenuItem>();

    public object? Data { get; set; }

    public string? Style { get; set; }

    public string? StyleClass { get; set; }

    public string? Tooltip { get; set; }

    public bool Visible { get; set; } = true;
}
