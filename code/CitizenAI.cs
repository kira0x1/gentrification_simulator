namespace Kira;

public class CitizenAI : Component
{
    private NavMeshAgent Agent { get; set; }
    private Waypoint waypoint;
    private WaypointManager wpManager { get; set; }

    private const float waitTime = 3f;
    private TimeSince timeSinceStop;
    private bool isWaiting;

    protected override void OnAwake()
    {
        base.OnAwake();
        Agent = Components.Get<NavMeshAgent>();
        wpManager = Scene.Components.GetAll<WaypointManager>().FirstOrDefault();
    }

    protected override void OnStart()
    {
        UpdateNextDestination();
    }

    protected override void OnUpdate()
    {
        if (isWaiting && timeSinceStop < waitTime)
        {
            return;
        }

        if (isWaiting && timeSinceStop > waitTime)
        {
            isWaiting = false;
            UpdateNextDestination();
        }

        float dist = Vector3.DistanceBetween(Agent.AgentPosition, waypoint.WorldPosition);

        if (dist < 12f)
        {
            timeSinceStop = 0;
            Agent.Stop();
            isWaiting = true;
        }

        Agent.MoveTo(waypoint.WorldPosition);
    }

    private void UpdateNextDestination()
    {
        waypoint = wpManager.GetWaypoint();
    }
}