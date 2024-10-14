namespace Kira;

using System;
using static ClothingContainer;

[GameResource("CitizenStyle", "cstyle", "citizen clothing", Icon = "style")]
public class CitizenStyle : GameResource
{
    public ClothingContainer ClothingContainer { get; set; }

    [Property] public readonly List<Clothing> Shirt = new List<Clothing>();
    [Property] public readonly List<Clothing> Jacket = new List<Clothing>();
    [Property] public readonly List<Clothing> Pants = new List<Clothing>();
    [Property] public readonly List<Clothing> Shoes = new List<Clothing>();
    [Property] public readonly List<Clothing> Hair = new List<Clothing>();
    [Property] public readonly List<Clothing> Beard = new List<Clothing>();

    [Property] public readonly List<Color> HairColors = new List<Color>
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

        Clothing pants = Random.Shared.FromList(Pants);
        Clothing shirt = Random.Shared.FromList(Shirt);
        Clothing hair = Random.Shared.FromList(Hair);
        Clothing shoes = Random.Shared.FromList(Shoes);
        Clothing beard = Random.Shared.FromList(Beard);
        Clothing jacket = Random.Shared.FromList(Jacket);


        var pantsEntry = new ClothingEntry(pants);
        var shirtEntry = new ClothingEntry(shirt);
        var hairEntry = new ClothingEntry(hair);
        var shoesEntry = new ClothingEntry(shoes);
        var beardEntry = new ClothingEntry(beard);

        ClothingContainer container = new ClothingContainer();
        container.Clothing.Add(pantsEntry);
        container.Clothing.Add(shirtEntry);
        container.Clothing.Add(hairEntry);
        container.Clothing.Add(shoesEntry);


        citizenData.shirtClothes = shirt;
        citizenData.pantsClothing = pants;

        bool hasBeard = Beard.Count > 0 && Random.Shared.Float(0, 1) > 0.6f;
        bool hasJacket = Random.Shared.Float(0, 1) < JacketChance;

        if (hasBeard)
        {
            // Make it a little darker then hair
            container.Clothing.Add(beardEntry);
        }

        if (hasJacket)
        {
            var jacketEntry = new ClothingEntry(jacket);
            container.Clothing.Add(jacketEntry);
        }

        citizenData.hasBeard = hasBeard;
        citizenData.firstName = RandomNames.RandomFirstName;
        citizenData.lastName = RandomNames.RandomLastName;
        citizenData.container = container;

        return citizenData;
    }

    public ClothingContainer GenerateClothing()
    {
        Clothing pants = Random.Shared.FromList(Pants);
        Clothing shirt = Random.Shared.FromList(Shirt);
        Clothing hair = Random.Shared.FromList(Hair);
        Clothing shoes = Random.Shared.FromList(Shoes);
        Clothing beard = Random.Shared.FromList(Beard);


        var pantsEntry = new ClothingEntry(pants);
        var shirtEntry = new ClothingEntry(shirt);
        var hairEntry = new ClothingEntry(hair);
        var shoesEntry = new ClothingEntry(shoes);
        var beardEntry = new ClothingEntry(beard);

        ClothingContainer container = new ClothingContainer();
        container.Clothing.Add(pantsEntry);
        container.Clothing.Add(shirtEntry);
        container.Clothing.Add(hairEntry);
        container.Clothing.Add(shoesEntry);
        container.Clothing.Add(beardEntry);

        return container;
    }
}