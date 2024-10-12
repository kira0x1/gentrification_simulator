﻿namespace Kira;

using System;

public class CityAction
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public float Cooldown { get; set; }
    public TimeSince TimeSinceLastUse { get; set; }
    public Action OnUseAction { get; set; }

    public CityAction(Action onUseAction, float coolDown = 1f)
    {
        this.OnUseAction = onUseAction;
        this.Cooldown = coolDown;
    }

    public CityAction(string name, string icon, Action onUseAction, float cooldown = 1f)
    {
        this.Name = name;
        this.Icon = icon;
        this.OnUseAction = onUseAction;
        this.Cooldown = cooldown;
    }

    public void Use()
    {
        if (!CanUse) return;
        TimeSinceLastUse = 0;
        OnUseAction?.Invoke();
    }

    public bool CanUse => TimeSinceLastUse > Cooldown;
}