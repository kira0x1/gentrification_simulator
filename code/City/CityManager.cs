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
    [Property, Group("Citizen")] public readonly List<Color> HairColors = new List<Color>();
    [Property, Group("Citizen")] public readonly List<Model> Pants = new List<Model>();
    [Property, Group("Citizen")] public readonly List<Model> Shirt = new List<Model>();
    [Property, Group("Citizen")] public readonly List<Model> Hair = new List<Model>();
    [Property, Group("Citizen")] public readonly List<Model> Beard = new List<Model>();

    [Property] private GameObject SelectedUI { get; set; }

    protected override void OnAwake()
    {
        CityCitizens = Scene.GetAllComponents<CitizenAI>().ToList();
        PlayerCam = Scene.GetAllComponents<PlayerCamera>().FirstOrDefault();

        RandomNames.Init();

        foreach (CitizenAI citizen in CityCitizens)
        {
            citizen.OnCitizenSelected += OnCitizenSelected;
            citizen.OnCitizenDeselected += OnCitizienDeselect;

            citizen.Agent.MaxSpeed = CitizenMaxSpeed;
            citizen.Agent.Acceleration = CitizenAcceleration;
            citizen.SetCitizenState(GenerateCitizenData());
        }

        City.Population = CityCitizens.Count;
    }

    public void SpawnCitizen()
    {
        var citizenData = GenerateCitizenData();
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
        SelectedUI.SetParent(citizen.GameObject);
        // SelectedUI.LocalPosition = Vector3.Zero.WithZ(SelectedUI.WorldPosition.z);
        SelectedUI.WorldPosition = new Vector3(citizen.WorldPosition.x - 60, citizen.WorldPosition.y + 40, 0);
        SelectedUI.WorldPosition = SelectedUI.WorldPosition.WithZ(65);
        SelectedUI.Enabled = true;
    }

    private void OnCitizienDeselect()
    {
        SelectedUI.Enabled = false;
    }

    public CitizenState GenerateCitizenData()
    {
        CitizenState citizenData = new CitizenState();

        citizenData.hairColor = Random.Shared.FromList(HairColors);
        citizenData.pantsModel = Random.Shared.FromList(Pants);
        citizenData.shirtModel = Random.Shared.FromList(Shirt);
        citizenData.hairModel = Random.Shared.FromList(Hair);
        citizenData.beardModel = Random.Shared.FromList(Beard);
        citizenData.hasBeard = Random.Shared.Float(0, 1) > 0.6f;

        citizenData.firstName = RandomNames.RandomFirstName;
        citizenData.lastName = RandomNames.RandomLastName;

        return citizenData;
    }
}