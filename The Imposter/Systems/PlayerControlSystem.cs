using Microsoft.Xna.Framework.Input;

using MonoGamePlus;

using TheImposter.GameStates.Level;

namespace TheImposter.Systems;
internal class PlayerControlSystem : GameSystem
{
    private readonly LevelState levelState;

    private KeyboardState lastKeyboardState;

    public Keys IdentifyKey { get; set; } = Keys.E;

    public PlayerControlSystem(LevelState levelState)
    {
        this.levelState = levelState;
        lastKeyboardState = Keyboard.GetState();
    }

    protected override void Update(float elapsed)
    {
        KeyboardState current = Keyboard.GetState();

        if (current.IsKeyDown(IdentifyKey) && lastKeyboardState.IsKeyUp(IdentifyKey))
        {
            levelState.TryFindImposter();
        }

        lastKeyboardState = current;

        base.Update(elapsed);
    }
}
