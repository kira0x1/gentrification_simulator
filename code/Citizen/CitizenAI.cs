﻿namespace Kira;

using System;
using Sandbox.Citizen;

public partial class CitizenAI : Component, ISelectable
{
    public NavMeshAgent Agent { get; set; }
    public CitizenState CitizenState { get; set; }

    private Waypoint waypoint;
    private Vector3 nextPos;

    private WaypointManager wpManager { get; set; }
    private CitizenAnimationHelper Anim { get; set; }

    private const float minWaitTime = 2f;
    private const float maxWaitTime = 4.5f;
    private float waitTime = 3f;

    private TimeSince timeSinceStop;
    private bool isWaiting;

    [Property, Group("Clothes")] public SkinnedModelRenderer Body { get; set; }
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

        // Make it a little darker then hair
        Beard.Tint = Color.Lerp(state.hairColor, Color.Black, 0.5f);

        Shirt.Model = state.shirtModel;
        Pants.Model = state.pantsModel;

        Beard.Enabled = CitizenState.hasBeard;
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

    public Guid id { get; set; }
    public bool IsSelected { get; set; }
    public bool IsHovering { get; set; }
    public SelectableTypes SelectableType { get; set; } = SelectableTypes.Unit;

    public void Deselect()
    {
    }
}