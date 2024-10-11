namespace Kira;

using System;

public class CityManager : Component
{
    public City City { get; set; } = new City();
    public List<CitizenAI> CityCitizens { get; set; } = new List<CitizenAI>();

    [Property, Range(0, 100)] public float CitizenAcceleration { get; set; } = 80;
    [Property, Range(0, 100)] public float CitizenMaxSpeed { get; set; } = 60;

    [Property]
    private GameObject CitizenPrefab { get; set; }

    [Property, Group("Citizen Clothes")] public List<Color> HairColors = new List<Color>();
    [Property, Group("Citizen Clothes")] public List<(Color, int)> WeightedColors { get; set; }
    [Property, Group("Citizen Clothes")] public List<Model> Pants = new List<Model>();
    [Property, Group("Citizen Clothes")] public List<Model> Shirt = new List<Model>();
    [Property, Group("Citizen Clothes")] public List<Model> Hair = new List<Model>();
    [Property, Group("Citizen Clothes")] public List<Model> Beard = new List<Model>();

    protected override void OnAwake()
    {
        CityCitizens = Scene.GetAllComponents<CitizenAI>().ToList();
        RandomNames.Init();

        foreach (CitizenAI citizen in CityCitizens)
        {
            citizen.Agent.MaxSpeed = CitizenMaxSpeed;
            citizen.Agent.Acceleration = CitizenAcceleration;
            citizen.SetCitizenState(GenerateCitizenData());
        }

        City.Population = CityCitizens.Count;
    }

    public void SpawnCitizen()
    {
        var citizenData = GenerateCitizenData();
        var clone = CitizenPrefab.Clone(Vector3.Zero);

        CitizenAI ai = clone.Components.Get<CitizenAI>();
        ai.SetCitizenState(citizenData);

        // Break from prefab useful when wanting to inspect citizens in scene
        // clone.BreakFromPrefab();

        CityCitizens.Add(ai);
        City.Population++;
    }

    public CitizenState GenerateCitizenData()
    {
        CitizenState citizenData = new CitizenState();

        citizenData.hairColor = Random.Shared.FromList(HairColors);
        citizenData.pantsModel = Random.Shared.FromList(Pants);
        citizenData.shirtModel = Random.Shared.FromList(Shirt);
        citizenData.hairModel = Random.Shared.FromList(Hair);
        citizenData.beardModel = Random.Shared.FromList(Beard);

        citizenData.firstName = RandomNames.RandomFirstName;
        citizenData.lastName = RandomNames.RandomLastName;

        return citizenData;
    }
}