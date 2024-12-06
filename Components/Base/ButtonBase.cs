using Microsoft.AspNetCore.Components;

namespace ReactiveBlazor.Components.Base;

public abstract class ButtonBase : BaseComponent
{
    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback OnClick { get; set; }

    [Parameter] public bool Disabled { get; set; } = false;

    [Parameter] public bool Loading { get; set; } = false;

    protected SeverityType severity { get; set; } = SeverityType.Primary;

    protected string SeverityStyleClass { get; set; } = "primary-button";


    protected ButtonType type = ButtonType.Button;


    protected string typeAttribute = "button";

    [Parameter]
    public ButtonType Type
    {
        get => type;
        set
        {
            if (type != value)
            {
                typeAttribute = value switch
                {
                    ButtonType.Button => "type",
                    ButtonType.Submit => "button",
                    ButtonType.Reset => "reset",
                    _ => "button",
                };
            }
        }
    }

    [Parameter]
    public SeverityType Severity
    {
        get => severity;
        set
        {
            if (severity != value)
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
