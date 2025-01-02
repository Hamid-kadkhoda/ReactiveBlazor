namespace ReactiveBlazor;

public class DynamicTabPanel : DynamicSectionPanel
{
    public List<DynamicSheetPanel> Sheets { get; set; } = new();
}
