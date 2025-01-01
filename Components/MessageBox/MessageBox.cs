using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReactiveBlazor;

public static class MessageBox
{
    private static Subject<string> MessageSubject { get; set; } = new Subject<string>();

    public static void Show(string message)
    {
        MessageSubject.OnNext(message);
    }

    public static IObservable<string> OnChanged()
    {
        return MessageSubject.AsObservable();
    }
}
