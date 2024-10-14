namespace Kira;

using System;
using Sandbox.Citizen;

public enum CitizenType
{
    Artist,
    Hipster,
    OfficeWorker,
    Teenager,
    DrugLord,
    Old,
    Dwarven,
    Demon,
    Nerd,
    TechBro,
    BourgeoisieHipster,
}

public partial class CitizenAI : Component, ISelectable
{
    public NavMeshAgent Agent { get; set; }
    public CitizenState CitizenState { get; set; }

    private Waypoint waypoint;
    private Vector3 nextPos;

    private WaypointManager wpManager { get; set; }
    public CitizenAnimationHelper Anim { get; set; }

    private const float minWaitTime = 2f;
    private const float maxWaitTime = 4.5f;
    private float waitTime = 3f;

    private TimeSince timeSinceStop;
    private bool isWaiting;
    private bool canMove = true;

    #pragma warning disable CS0067 // Event is never used
    public event Action<CitizenAI> OnCitizenSelected;
    public event Action OnCitizenDeselected;
    #pragma warning restore CS0067 // Event is never used

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
    }

    protected override void OnStart()
    {
        UpdateNextDestination();
    }

    protected override void OnUpdate()
    {
        UpdateAnimator();

        if (isWaiting && timeSinceStop < waitTime || !canMove)
        {
            return;
        }

        if (isWaiting && timeSinceStop > waitTime)
        {
            UpdateNextDestination();
        }

        float dist = Vector3.DistanceBetween(Agent.AgentPosition, nextPos);

        if (dist < 30f)
        {
            timeSinceStop = 0;
            Agent.Stop();
            Agent.MoveTo(Agent.AgentPosition);
            Agent.Velocity = Vector3.Zero;
            Agent.UpdateRotation = false;
            isWaiting = true;
            return;
        }

        Agent.MoveTo(nextPos);
    }

    private void UpdateNextDestination()
    {
        waypoint = wpManager.GetWaypoint();
        nextPos = waypoint.GetRandomizedPos();

        Agent.UpdateRotation = true;
        Agent.UpdatePosition = true;
        isWaiting = false;
        waitTime = Random.Shared.Float(minWaitTime, maxWaitTime);
    }

    private void UpdateAnimator()
    {
        Anim.WithVelocity(Agent.Velocity);
        Anim.WithWishVelocity(Agent.WishVelocity);
    }
}