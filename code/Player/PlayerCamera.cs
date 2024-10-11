﻿namespace Kira;

public class PlayerCamera : Component
{
    private CameraComponent Camera { get; set; }
    private Player player;

    public bool IsHovering { get; set; }
    public bool HasSelection { get; set; }

    public ISelectable hovering;
    public ISelectable SelectedUnit { get; set; }

    protected override void OnAwake()
    {
        Camera = Scene.GetAllComponents<CameraComponent>().FirstOrDefault();
        player = Components.Get<Player>();
    }

    protected override void OnUpdate()
    {
        UpdateRay();
    }


    private void UpdateRay()
    {
        var ray = Camera.ScreenPixelToRay(Mouse.Position).Project(1400f);
        var trace = Scene.Trace.Ray(Camera.LocalPosition, ray).WithTag("selectable").Run();

        if (!trace.Hit)
        {
            if (IsHovering)
            {
                hovering.OnLeaveHover();
                IsHovering = false;
            }

            if (HasSelection && Input.Pressed("attack1"))
            {
                SelectedUnit.Deselect();
                HasSelection = false;
            }

            return;
        }

        var sl = trace.GameObject.Components.Get<ISelectable>();

        if (sl != null)
        {
            if (Input.Pressed("attack1"))
            {
                SelectUnit(sl);
                return;
            }

            HandleHovering(sl);
        }
    }

    private void SelectUnit(ISelectable unit)
    {
        if (HasSelection)
        {
            SelectedUnit.Deselect();
        }

        unit.Select();
        SelectedUnit = unit;
        HasSelection = true;
    }

    private void HandleHovering(ISelectable selectable)
    {
        if (IsHovering && selectable.id != hovering.id)
        {
            hovering.OnLeaveHover();
        }

        if (!IsHovering)
        {
            selectable.OnHover();
            hovering = selectable;
            IsHovering = true;
        }
    }
}