using Newtonsoft.Json;
using System.Reflection;

namespace ReactiveBlazor;

public static class MyObjectExtensions
{
    public static T CloneObject<T>(this T original) where T : class, new()
    {
        T copy = new T();
        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            property.SetValue(copy, property.GetValue(original));
        }
        return copy;
    }

    public static T DeepCopy<T>(this T original) where T : class, new()
    {
        string json = JsonConvert.SerializeObject(original);

        T copy = JsonConvert.DeserializeObject<T>(json)!;

        return copy;
    }
}
