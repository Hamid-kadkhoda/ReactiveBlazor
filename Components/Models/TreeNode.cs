namespace ReactiveBlazor.Components.Models;

public class TreeNode
{
    public string? Label { get; set; }

    public string? Icon { get; set; }

    public object? Data { get; set; }

    public bool Expanded { get; set; } = false;

    public bool Leaf { get; set; } = false;

    public bool Selectable { get; set; } = true;

    public string? StyleClass { get; set; }

    public string? Type { get; set; }

    public List<TreeNode>? Children { get; set; } = new List<TreeNode>();
}
