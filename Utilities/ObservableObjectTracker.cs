using System.ComponentModel;

namespace ReactiveBlazor;

public abstract class ObservableObjectTracker<T> : ObjectTracker<T>, INotifyPropertyChanged where T : class, new()
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected override void SetProperty(string propertyName, object? newValue)
    {
        base.SetProperty(propertyName, newValue);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}