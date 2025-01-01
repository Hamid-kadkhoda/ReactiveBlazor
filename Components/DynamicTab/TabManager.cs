using System.Reflection;

namespace ReactiveBlazor;

public class TabManager
{
    public event Action? TabsChanged;

    private void NotifyTabsChanged() => TabsChanged?.Invoke();

    public List<DynamicTabPanel> Tabs { get; set; } = new();

    public void AddTab(string className, string? title = null, bool disposable = false)
    {
        try
        {
            var tab = new DynamicTabPanel()
            {
                Title = title ?? className,
                Disposable = disposable,
                Content = GetType(className)
            };

            Tabs.Add(tab);
            ActivateTab(tab);
            NotifyTabsChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void ActivateTab(DynamicTabPanel tab)
    {
        foreach (var item in Tabs)
        {
            if (item.Key != tab.Key)
            {
                item.IsActive = false;
            }
        }
        tab.IsActive = true;
    }

    public void RemoveTab(DynamicTabPanel tab)
    {
        var index = Tabs.IndexOf(tab);
        Tabs.RemoveAt(index);
        if (Tabs.Count > 0)
        {
            if (index > 0)
            {
                ActivateTab(Tabs[index - 1]);
            }
            else if (index < Tabs.Count)
            {
                ActivateTab(Tabs[index]);
            }
        }
    }

    private Type? GetType(string key)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var type = assembly
            .GetTypes()
            .Where(x => x.Name.Contains(key))
            .First();

        return type;
    }
}
