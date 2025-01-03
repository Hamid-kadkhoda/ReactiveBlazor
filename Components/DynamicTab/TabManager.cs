using System.Reflection;

namespace ReactiveBlazor;

public class TabManager
{
    public event Action? TabsChanged;

    private void NotifyTabsChanged() => TabsChanged?.Invoke();

    public List<DynamicTabPanel> Tabs { get; set; } = new();

    private DynamicTabPanel ActiveTab => Tabs.First(t => t.IsActive);

    private List<DynamicSheetPanel> ActiveTabSheets => ActiveTab.Sheets;

    public void AddTab(string className, bool isDefault = false, bool disposable = false)
    {
        try
        {
            var tab = new DynamicTabPanel()
            {
                Title = GetTypeAttributeTitle(GetType(className)) ?? className,
                Disposable = disposable,
            };

            Tabs.Add(tab);
            ActivateTab(tab);

            if (isDefault)
            {
                AddSheet(className);
            }
            else
            {
                NotifyTabsChanged();
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public void AddSheet(string className, bool disposable = false)
    {
        try
        {
            var sheet = new DynamicSheetPanel()
            {
                Title = GetTypeAttributeTitle(GetType(className)) ?? className,
                Disposable = disposable,
                Content = GetType(className)
            };
            ActiveTabSheets.Add(sheet);
            ActivateSheet(sheet);
            NotifyTabsChanged();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public void ActivateTab(DynamicTabPanel tab)
    {
        if (Tabs.Count == 1)
        {
            tab.IsActive = true;
        }
        else if (Tabs.Count > 1)
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
    }

    public void ActivateSheet(DynamicSheetPanel sheet)
    {
        if (!ActiveTabSheets.Any())
            return;

        if (ActiveTabSheets.Count == 1)
        {
            sheet.IsActive = true;
        }
        else if (ActiveTabSheets.Count > 1)
        {
            foreach (var item in ActiveTabSheets)
            {
                if (item.Key != sheet.Key)
                {
                    item.IsActive = false;
                }
            }
            sheet.IsActive = true;
        }
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

    public void RemoveSheet(DynamicSheetPanel sheet)
    {
        if (ActiveTab == null)
            return;

        var index = ActiveTabSheets.IndexOf(sheet);
        ActiveTabSheets.RemoveAt(index);
        if (ActiveTabSheets.Count > 0)
        {
            if (index > 0)
            {
                ActivateSheet(ActiveTabSheets[index - 1]);
            }
            else if (index < ActiveTabSheets.Count)
            {
                ActivateSheet(ActiveTabSheets[index]);
            }
        }
        NotifyTabsChanged();
    }

    private string? GetTypeAttributeTitle(Type? type)
    {
        if (type == null) return null;

        var attr = type.GetCustomAttribute<SelectorAttribute>();

        if (attr == null) return null;

        return attr.Title;
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
