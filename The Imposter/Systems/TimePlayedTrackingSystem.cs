using MonoGamePlus;

using TheImposter.GameStates.Level;

namespace TheImposter.Systems;
internal class TimePlayedTrackingSystem : GameSystem
{
    private readonly LevelState levelState;

    public TimePlayedTrackingSystem(LevelState levelState)
    {
        this.levelState = levelState;
    }

    protected override void Update(float elapsed)
    {
        levelState.Statistics.TimePlayed += elapsed;

        base.Update(elapsed);
    }
}
