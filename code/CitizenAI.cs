namespace Kira;

using Sandbox.Citizen;

public class CitizenAI : Component
{
    public NavMeshAgent Agent { get; set; }
    public CitizenState CitizenState { get; set; }

    private Waypoint waypoint;
    private WaypointManager wpManager { get; set; }
    private CitizenAnimationHelper Anim { get; set; }

    private const float waitTime = 3f;
    private TimeSince timeSinceStop;
    private bool isWaiting;

    [Property, Group("Clothes")] public SkinnedModelRenderer Hair { get; set; }
    [Property, Group("Clothes")] public SkinnedModelRenderer Beard { get; set; }
    [Property, Group("Clothes")] public SkinnedModelRenderer Pants { get; set; }
    [Property, Group("Clothes")] public SkinnedModelRenderer Shirt { get; set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        Agent = Components.Get<NavMeshAgent>();
        Anim = Components.Get<CitizenAnimationHelper>();
        wpManager = Scene.Components.GetAll<WaypointManager>().FirstOrDefault();
    }

    public void SetCitizenState(CitizenState state)
    {
        CitizenState = state;

        Hair.Model = state.hairModel;
        Hair.Tint = state.hairColor;

        Beard.Model = state.beardModel;
        Beard.Tint = state.hairColor;
    }

    protected override void OnStart()
    {
        UpdateNextDestination();
    }

    protected override void OnUpdate()
    {
        UpdateAnimator();

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

    private void UpdateAnimator()
    {
        Anim.WithVelocity(Agent.Velocity);
    }
}