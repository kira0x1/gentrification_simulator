namespace Kira;

using System;

public class WaypointManager : Component
{
    private List<Waypoint> Waypoints { get; set; } = new List<Waypoint>();

    protected override void OnAwake()
    {
        base.OnAwake();
        Waypoints = GetComponentsInChildren<Waypoint>().ToList();
    }

    public Waypoint GetWaypoint()
    {
        return Random.Shared.FromList(Waypoints);
    }
}