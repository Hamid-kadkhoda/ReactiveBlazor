using System;
using System.Collections.Generic;
using System.Text.Json;

public abstract class ModelTracker<T> where T : class, new()
{
    private T _defaultValue;
    private T _currentValue;

    public ModelTracker()
    {
        _defaultValue = new T(); // Stores the original default state
        _currentValue = DeepCopy(_defaultValue); // Keeps track of changes
    }

    public void Initialize(T value)
    {
        _defaultValue = DeepCopy(value);
        _currentValue = DeepCopy(value);
    }

    public void ResetToDefault()
    {
        _currentValue = DeepCopy(_defaultValue);
        NotifyStateChanged();
    }

    public T GetTrackedObject() => _currentValue;

    private static T DeepCopy(T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json)!;
    }

    public event Action? StateChanged;
    private void NotifyStateChanged() => StateChanged?.Invoke();
}
