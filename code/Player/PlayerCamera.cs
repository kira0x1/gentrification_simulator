namespace Kira;

public class PlayerCamera : Component
{
    private CameraComponent Camera { get; set; }
    private Player player;

    protected override void OnAwake()
    {
        Camera = Scene.GetAllComponents<CameraComponent>().FirstOrDefault();
        player = Components.Get<Player>();
    }
}