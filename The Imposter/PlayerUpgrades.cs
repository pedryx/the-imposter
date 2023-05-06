namespace TheImposter;
internal class PlayerUpgrades
{
    private const float moveSpeedUpgrade = 25.0f;

    public float MoveSpeed { get; private set; }

    public void upgradeMoveSpeed() => MoveSpeed += moveSpeedUpgrade;
}
