namespace Kira;

using System;

public class Waypoint : Component
{
    public Vector3 GetRandomizedPos()
    {
        var pos = WorldPosition;
        pos.x += Random.Shared.Float(-60, 60);
        pos.y += Random.Shared.Float(-60, 60);

        return pos;
    }

    protected override void DrawGizmos()
    {
        Gizmo.Draw.Color = new Color(30, 60, 40);
        Gizmo.Draw.LineSphere(Vector3.Zero, 6);
    }
}