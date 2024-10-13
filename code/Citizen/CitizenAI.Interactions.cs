namespace Kira;

using System;

public partial class CitizenAI
{
    [Property] public GameObject SelectionGlow { get; set; }
    [Property] public Material GlowMaterial { get; set; }

    public Guid id { get; set; }
    public bool IsSelected { get; set; }
    public bool IsHovering { get; set; }
    public SelectableTypes SelectableType { get; set; } = SelectableTypes.Unit;

    public void OnHover()
    {
        // Body.SetMaterialOverride(GlowMaterial, "");
    }

    public void OnLeaveHover()
    {
        // Body.ClearMaterialOverrides();
    }

    public void Select()
    {
        OnCitizenSelected?.Invoke(this);
        canMove = false;

        Agent.Stop();
        Agent.UpdateRotation = false;

        SelectionGlow.Enabled = true;
    }

    public void Deselect()
    {
        OnCitizenDeselected?.Invoke();
        canMove = true;

        Agent.UpdateRotation = true;

        Agent.Velocity = 0;
        UpdateAnimator();

        SelectionGlow.Enabled = false;
    }
}