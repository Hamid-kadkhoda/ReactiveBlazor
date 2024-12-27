namespace ReactiveBlazor;

public class ToastMessage
{
    public Guid Key { get; set; }

    public string Text { get; set; } = null!;

    public string? Summery { get; set; }

    public string? Style { get; set; }

    public string? StyleClass { get; set; }

    public int Life { get; set; } = 3000;

    public SeverityType Type { get; set; } = SeverityType.Success;
}
