namespace Kira;

using System;

public class CityManager : Component
{
    public City City { get; set; } = new City();
    public List<CitizenAI> CityCitizens { get; set; } = new List<CitizenAI>();
    private PlayerCamera PlayerCam { get; set; }

    [Property, Range(0, 100)] public float CitizenAcceleration { get; set; } = 80;
    [Property, Range(0, 100)] public float CitizenMaxSpeed { get; set; } = 60;

    [Property, Group("Citizen")] private GameObject CitizenPrefab { get; set; }
    [Property] private SelectedWorldUI SelectedUI { get; set; }

    // ReSharper disable once CollectionNeverUpdated.Global
    [Property] public Dictionary<CitizenType, CitizenStyle> CitizenStyles { get; set; } = new Dictionary<CitizenType, CitizenStyle>();

    protected override void OnAwake()
    {
        CityCitizens = Scene.GetAllComponents<CitizenAI>().ToList();
        PlayerCam = Scene.GetAllComponents<PlayerCamera>().FirstOrDefault();
        RandomNames.Init();
    }

    protected override void OnStart()
    {
        foreach (CitizenAI citizen in CityCitizens)
        {
            citizen.OnCitizenSelected += OnCitizenSelected;
            citizen.OnCitizenDeselected += OnCitizienDeselect;

            citizen.Agent.MaxSpeed = CitizenMaxSpeed;
            citizen.Agent.Acceleration = CitizenAcceleration;
            // citizen.SetCitizenState(GenerateCitizenData(CitizenType.OfficeWorker));
        }

        City.Population = CityCitizens.Count;
    }

    /// <summary>
    /// Spawn a citizen with a distinct style
    /// </summary>
    /// <param name="citizenType"></param>
    public void SpawnCitizen(CitizenType citizenType = CitizenType.OfficeWorker)
    {
        CitizenState citizenData = GenerateCitizenData(citizenType);

        var cam = PlayerCam.Camera;
        var pos = cam.WorldPosition + cam.LocalTransform.Forward * 350f;
        var clone = CitizenPrefab.Clone(pos.WithZ(0));

        CitizenAI ai = clone.Components.Get<CitizenAI>();
        ai.SetCitizenState(citizenData);

        // Break from prefab useful when wanting to inspect citizens in scene
        // clone.BreakFromPrefab();

        ai.OnCitizenSelected += OnCitizenSelected;
        ai.OnCitizenDeselected += OnCitizienDeselect;

        CityCitizens.Add(ai);
        City.Population++;
    }

    private void OnCitizenSelected(CitizenAI citizen)
    {
        SelectedUI.CitizenSelected = citizen.CitizenState;
        SelectedUI.GameObject.SetParent(citizen.GameObject);
        // SelectedUI.LocalPosition = Vector3.Zero.WithZ(SelectedUI.WorldPosition.z);
        SelectedUI.WorldPosition = new Vector3(citizen.WorldPosition.x - 60, citizen.WorldPosition.y + 40, 0);
        SelectedUI.WorldPosition = SelectedUI.WorldPosition.WithZ(65);
        SelectedUI.GameObject.Enabled = true;
    }

    private void OnCitizienDeselect()
    {
        SelectedUI.GameObject.Enabled = false;
    }

    public CitizenState GenerateCitizenData(CitizenType style)
    {
        CitizenStyle cs = CitizenStyles[style];
        return cs.GenerateCitizenData();
    }
}