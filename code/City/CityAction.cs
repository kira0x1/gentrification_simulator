namespace Kira;

using System;

public class CityAction
{
    public float Cooldown { get; set; }
    public TimeSince TimeSinceLastUse { get; set; }
    public Action OnUseAction { get; set; }

    public CityAction(Action onUseAction, float coolDown = 1f)
    {
        this.OnUseAction = onUseAction;
        this.Cooldown = coolDown;
    }

    public void Use()
    {
        if (!CanUse) return;
        TimeSinceLastUse = 0;
        OnUseAction?.Invoke();
    }

    public bool CanUse => TimeSinceLastUse > Cooldown;
}