
namespace ReactiveBlazor;

public abstract class OwingSectionBase : OwningComponentBase
{
    [Inject] protected TabManager TabManager { get; set; } = default!;

    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject] protected IHttpClientBase http { get; set; } = default!;

    [Parameter] public object? Data { get; set; }

    [CascadingParameter(Name = "ParentSheet")] protected DynamicSheetPanel SheetPanel { get; set; } = null!;

    [CascadingParameter(Name = "ParentTab")] protected DynamicTabPanel TabPanel { get; set; } = null!;

    protected void CloseThisSheet()
    {
        TabManager.RemoveSheet(SheetPanel);
    }

    protected void CloseThisTab()
    {
        TabManager.RemoveTab(TabPanel);
    }

}
