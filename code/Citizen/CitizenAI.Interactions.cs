namespace Kira;

public partial class CitizenAI
{
    [Property] public Material GlowMaterial { get; set; }

    public void OnHover()
    {
        Body.SetMaterialOverride(GlowMaterial, "");
    }

    public void OnLeaveHover()
    {
        Body.ClearMaterialOverrides();
    }


    public void Select()
    {
        Log.Info($"Selected: {CitizenState.fullName}");
    }
}