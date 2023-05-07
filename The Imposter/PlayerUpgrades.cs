using TheImposter.GameStates.Level;

namespace TheImposter;
internal class PlayerUpgrades
{
    private const float moveSpeedUpgrade = 25.0f;
    private const float lightIntensityUpgrade = LevelState.StartVisibility / LevelState.FinalStage;
    private const float lightRadiusUpgrade = 50.0f;

    public float MoveSpeed { get; private set; }
    public float LightIntensity { get; private set; }
    public float LightRadius { get; private set; }

    public void UpgradeMoveSpeed() => MoveSpeed += moveSpeedUpgrade;
    public void UpgradeLightIntensity() => LightIntensity += lightIntensityUpgrade;
    public void UpgradeLightRadius() => LightRadius += lightRadiusUpgrade;
}
