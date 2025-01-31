using System.Dynamic;
using System.Reflection;

namespace ReactiveBlazor;

public abstract class ObjectTracker<T> where T : class, new()
{
    private readonly ExpandoObject _defaultValue = new();
    private readonly Dictionary<string, object?> _currentValues = new();
    private readonly Stack<Dictionary<string, object?>> _history = new();

    public event Action<string, object?, object?>? OnPropertyChanged;

    public void Initialize(object value)
    {
        Console.WriteLine("Initialize Tracking...");
        _currentValues.Clear();
        InitializeRecursive(value, _defaultValue, _currentValues);
    }

    private void InitializeRecursive(object value, ExpandoObject defaultValues, Dictionary<string, object?> currentValues)
    {
        var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(value);

            if (propertyValue is null)
            {
                defaultValues.TryAdd(property.Name, null);
                currentValues[property.Name] = null;
                continue;
            }

            if (IsSimpleType(property.PropertyType))
            {
                defaultValues.TryAdd(property.Name, propertyValue);
                currentValues[property.Name] = propertyValue;
            }
            else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                // Handle lists/arrays
                var collection = ((System.Collections.IEnumerable)propertyValue).Cast<object>().ToList();
                defaultValues.TryAdd(property.Name, collection.ToList());
                currentValues[property.Name] = collection.ToList();
            }
            else
            {
                // Handle nested objects
                var nestedDefault = new ExpandoObject();
                var nestedCurrent = new Dictionary<string, object?>();
                InitializeRecursive(propertyValue, nestedDefault, nestedCurrent);
                defaultValues.TryAdd(property.Name, nestedDefault);
                currentValues[property.Name] = nestedCurrent;
            }
        }
    }

    public void ResetToDefault()
    {
        Console.WriteLine("Resetting to ToDefault");
        ResetRecursive(_defaultValue, _currentValues);
    }

    private void ResetRecursive(ExpandoObject defaultValues, Dictionary<string, object?> currentValues)
    {
        foreach (var kvp in (IDictionary<string, object?>)defaultValues)
        {
            if (kvp.Value is ExpandoObject nestedExpando && currentValues[kvp.Key] is Dictionary<string, object?> nestedDict)
            {
                ResetRecursive(nestedExpando, nestedDict);
            }
            else
            {
                currentValues[kvp.Key] = kvp.Value;
            }
        }
    }

    protected virtual void SetProperty(string propertyName, object? newValue)
    {
        if (_currentValues.ContainsKey(propertyName))
        {
            var oldValue = _currentValues[propertyName];
            if (!Equals(oldValue, newValue))
            {
                _history.Push(new Dictionary<string, object?>(_currentValues));
                _currentValues[propertyName] = newValue;
                OnPropertyChanged?.Invoke(propertyName, oldValue, newValue);
            }
        }
    }

    public bool IsModified()
    {
        return IsModifiedRecursive(_defaultValue, _currentValues);
    }

    private bool IsModifiedRecursive(ExpandoObject defaultValues, Dictionary<string, object?> currentValues)
    {
        foreach (var kvp in (IDictionary<string, object?>)defaultValues)
        {
            if (kvp.Value is ExpandoObject nestedExpando && currentValues[kvp.Key] is Dictionary<string, object?> nestedDict)
            {
                if (IsModifiedRecursive(nestedExpando, nestedDict))
                {
                    return true;
                }
            }
            else if (!Equals(kvp.Value, currentValues[kvp.Key]))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string) || type == typeof(decimal);
    }
}
