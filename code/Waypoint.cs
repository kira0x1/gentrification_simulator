namespace Kira;

public class Waypoint : Component
{
    protected override void DrawGizmos()
    {
        Gizmo.Draw.Color = new Color(30, 60, 40);
        Gizmo.Draw.LineSphere(Vector3.Zero, 6);
    }
}