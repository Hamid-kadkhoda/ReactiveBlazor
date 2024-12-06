using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor.Components.Base;

public class ButtonBase: ComponentBase
{
    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback OnClick { get; set; }

    protected SeverityType severity { get; set; } = SeverityType.Primary;

    protected string SeverityStyleClass { get; set; } = "primary-button";

    [Parameter] public SeverityType Severity
    {
        get => severity;
        set
        {
            if(severity != value)
            {
                SeverityStyleClass = value switch
                {
                    SeverityType.Primary => "primary-button",
                    SeverityType.Danger => "danger-button",
                    SeverityType.Info => "info-button",
                    SeverityType.Warning => "warning-button",
                    SeverityType.Secondary => "secondary-button",
                    _ => "primary-button",
                };
            }
        }
    }
}
