namespace Kira;

public class PlayerCamera : Component
{
    public CameraComponent Camera { get; set; }

    public bool IsHovering { get; set; }
    public bool HasSelection { get; set; }

    public ISelectable hovering;
    public ISelectable SelectedUnit { get; set; }

    private Angles DefaultAngle { get; set; }
    private Vector3 DefaultPosition { get; set; }
    private float DefaultFov { get; set; }

    public float CurFov => Camera.FieldOfView;

    public const float MaxFov = 100;
    public const float MinFov = 20;

    protected override void OnAwake()
    {
        Camera = Scene.GetAllComponents<CameraComponent>().FirstOrDefault();

        if (!Camera.IsValid()) return;

        DefaultAngle = Camera.LocalRotation.Angles();
        DefaultPosition = Camera.WorldPosition;
        DefaultFov = Camera.FieldOfView;
    }

    protected override void OnUpdate()
    {
        UpdateRay();
    }

    public void TurnView(float xAngle)
    {
        Camera.LocalRotation = Camera.LocalRotation.Angles() + new Angles(0, xAngle, 0);
    }

    public void ResetZoom()
    {
        Camera.FieldOfView = DefaultFov;
        Camera.WorldPosition = DefaultPosition;
    }

    public void ResetView()
    {
        Camera.LocalRotation = Rotation.From(DefaultAngle);
    }

    public void ZoomIn()
    {
        AdjustZoom(-10f);
    }

    public void ZoomOut()
    {
        AdjustZoom(10f);
    }

    public void AdjustZoom(float zoomAmount)
    {
        Camera.FieldOfView = float.Clamp(Camera.FieldOfView + zoomAmount, MinFov, MaxFov);
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