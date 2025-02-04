namespace ReactiveBlazor;

// DialogService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

public class DialogService
{
    public event Action OnDialogsChanged;
    private readonly List<DialogReference> _activeDialogs = new();

    public DialogReference Open<T>(Dictionary<string, object> parameters = null) where T : ComponentBase
    {
        var dialogReference = new DialogReference();
        var dialogContent = new RenderFragment(builder =>
        {
            var seq = 0;
            builder.OpenComponent<T>(seq++);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    builder.AddAttribute(seq++, param.Key, param.Value);
                }
            }
            builder.CloseComponent();
        });

        dialogReference.Initialize(dialogContent, () => Close(dialogReference));
        _activeDialogs.Add(dialogReference);
        OnDialogsChanged?.Invoke();
        return dialogReference;
    }

    public void Close(DialogReference dialogReference)
    {
        dialogReference.Close();
        _activeDialogs.Remove(dialogReference);
        OnDialogsChanged?.Invoke();
        dialogReference.Dispose();
    }

    public IReadOnlyList<DialogReference> ActiveDialogs => _activeDialogs.AsReadOnly();
}

public class ComponentParameters : Dictionary<string, object> { }