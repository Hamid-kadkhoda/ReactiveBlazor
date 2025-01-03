namespace ReactiveBlazor;

public class MessageBus
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

    private readonly object _lock = new object();

    public void Subscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);
        lock (_lock)
        {
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers[type] = new List<Delegate>();
            }
            if (!_subscribers[type].Contains(handler))
            {
                _subscribers[type].Add(handler);
            }
        }
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);
        lock (_lock)
        {
            if (_subscribers.ContainsKey(type))
            {
                _subscribers[type].Remove(handler);
                if (_subscribers[type].Count == 0)
                {
                    _subscribers.Remove(type);
                }
            }
        }
    }

}
