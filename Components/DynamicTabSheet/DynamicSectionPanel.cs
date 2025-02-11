﻿namespace ReactiveBlazor;

public abstract class DynamicSectionPanel
{
    public Type? Content { get; set; }

    public string? Title { get; set; }

    public Guid Key { get; set; } = Guid.NewGuid();

    public bool IsActive { get; set; }

    public bool Disposable { get; set; }

    public object? Data { get; set; }

    public bool IsDefault { get; set; }
}
