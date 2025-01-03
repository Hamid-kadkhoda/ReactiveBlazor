namespace ReactiveBlazor;

public class SelectorAttribute : Attribute
{
    public string? Title { get; set; }

    public SelectorAttribute(string title)
    {
        Title = title;
    }
}
