using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Resources;
using MonoGamePlus.UI.Elements;

namespace TheImposter.GameStates;
internal class GameOverState : GameState
{
    private readonly Statistics statistics = new();
    private readonly string reason;
    private readonly bool victory;

    public GameOverState(Statistics statistics, string reason, bool victory = false)
    {
        this.statistics = statistics;
        this.reason = reason;
        this.victory = victory;
    }

    protected override void Initialize()
    {
        UILayer.AddElement(new Image(new Sprite(Game.Textures["dark background"])
        {
            Origin = Vector2.Zero,
            Position = new Vector2(-100.0f)
        }));

        UILayer.AddElement(new Label(new SpriteText(Game.Fonts["Storm Gust;128"], victory ? "Victory" : "Game Over", victory ? Color.Gold : Color.DarkRed))
        {
            Offset = new Vector2(Game.Resolution.X / 2.0f, 90.0f),
        });
        UILayer.AddElement(new Label(new SpriteText(Game.Fonts["Curse of the Zombie;32"], reason, Color.White))
        {
            Offset = new Vector2(Game.Resolution.X / 2.0f, 240.0f),
        });

        StackPanel panel = new(Game.Resolution / 2.0f + new Vector2(0.0f, 170.0f))
        {
            Padding = 40.0f,
        };
        UILayer.AddElement(panel);
        panel.Add(CreateLabel($"TIME  PLAYED:  {(int)statistics.TimePlayed / 60,2}"
            + $":{((int)statistics.TimePlayed % 60).ToString().PadLeft(2, '0')}"));
        panel.Add(CreateLabel($"COMPLETED  STAGES:  {statistics.CompletedStages}"));
        panel.Add(CreateLabel($"IMPOSTERS  FOUND:  {statistics.ImpostersFound}"));

        Button button = new();
        button.Label.SpriteText = new SpriteText(Game.Fonts["Curse of the Zombie;48"], "MAIN MENU", Color.Gray);
        button.HoverColor = Color.Yellow;
        button.OnClick += (sender, e) =>
        {
            Game.RemoveGameState(this);
            Game.AddGameState(new TitleState());
        };
        panel.Add(button);

        base.Initialize();
    }

    private Label CreateLabel(string text, int size = 48)
        => new(new SpriteText(Game.Fonts[$"Curse of the Zombie;{size}"], text, Color.Gray));
}
