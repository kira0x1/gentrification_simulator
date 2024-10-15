namespace Kira;

using System;
using static ClothingContainer;

[GameResource("CitizenStyle", "cstyle", "citizen clothing", Icon = "style"), Serializable]
public class CitizenStyle : GameResource
{
    [Property] public List<Clothing> Shirt { get; set; } = new List<Clothing>();
    [Property] public List<Clothing> Jacket { get; set; } = new List<Clothing>();
    [Property] public List<Clothing> Pants { get; set; } = new List<Clothing>();
    [Property] public List<Clothing> Shoes { get; set; } = new List<Clothing>();
    [Property] public List<Clothing> Hair { get; set; } = new List<Clothing>();
    [Property] public List<Clothing> Beard { get; set; } = new List<Clothing>();

    [Property] public List<Color> HairColors { get; set; } = new List<Color>
    {
        new Color(0, 0, 0, 1),
        new Color(0.32558f, 0.32558f, 0.32558f, 1),
        new Color(0.73953f, 0.73953f, 0.73953f, 1),
        new Color(0.30698f, 0.26243f, 0.20418f, 1),
        new Color(0.64186f, 0.57966f, 0.49259f, 1),
        new Color(0.49302f, 0.14676f, 0.14676f, 1),
        new Color(0.49627f, 0.64035f, 0.68837f, 1)
    };

    [Property, Range(0, 1)] public float JacketChance { get; set; } = 0.2f;

    public CitizenState GenerateCitizenData()
    {
        CitizenState citizenData = new CitizenState();

        bool hasBeard = Beard.Count > 0 && Random.Shared.Float(0, 1) > 0.6f;
        bool hasJacket = Random.Shared.Float(0, 1) < JacketChance;

        ClothingContainer container = GenerateClothing(hasBeard, hasJacket);

        citizenData.hasBeard = hasBeard;
        citizenData.firstName = RandomNames.RandomFirstName;
        citizenData.lastName = RandomNames.RandomLastName;
        citizenData.container = container;

        return citizenData;
    }

    public ClothingContainer GenerateClothing(bool hasJacket, bool hasBeard)
    {
        Clothing pants = Random.Shared.FromList(Pants);
        Clothing hair = Random.Shared.FromList(Hair);
        Clothing shoes = Random.Shared.FromList(Shoes);
        Clothing jacket = Random.Shared.FromList(Jacket);
        Clothing shirt = hasJacket ? Random.Shared.FromList(Shirt.FindAll(s => s.CanBeWornWith(jacket)).ToList()) : Random.Shared.FromList(Shirt);

        var shirtEntry = new ClothingEntry(shirt);
        var pantsEntry = new ClothingEntry(pants);
        var hairEntry = new ClothingEntry(hair);
        var shoesEntry = new ClothingEntry(shoes);

        ClothingContainer container = new ClothingContainer();
        container.Clothing.Add(pantsEntry);
        container.Clothing.Add(shirtEntry);
        container.Clothing.Add(hairEntry);
        container.Clothing.Add(shoesEntry);

        if (hasBeard)
        {
            Clothing beard = Random.Shared.FromList(Beard);
            var beardEntry = new ClothingEntry(beard);
            container.Clothing.Add(beardEntry);
        }

        if (hasJacket)
        {
            var jacketEntry = new ClothingEntry(jacket);
            container.Clothing.Add(jacketEntry);
        }

        return container;
    }
}