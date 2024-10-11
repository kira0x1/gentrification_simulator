namespace Kira;

public partial class CitizenAI
{
    public void OnHover()
    {
        // Log.Info($"Hovering: {CitizenState.firstName}");
    }

    public void OnLeaveHover()
    {
    }


    public void Select()
    {
        Log.Info($"Selected: {CitizenState.fullName}");
    }
}