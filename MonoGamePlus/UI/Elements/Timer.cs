using Microsoft.Xna.Framework;

using System;

namespace MonoGamePlus.UI.Elements;
public class Timer : UIElement
{
    public float Time;
    public Label Label;

    public event EventHandler OnFinish;

    protected override void Initialize()
    {
        AddChild(Label);
    }

    public override void Update(float elapsed, Vector2 position)
    {
        Time -= elapsed;
        if (Time <= 0.0f)
        {
            Time = 0.0f;
            OnFinish?.Invoke(this, new EventArgs());
        }

        Label.SpriteText.Text = $"{(int)Time / 60,2}:{((int)Time % 60).ToString().PadLeft(2, '0')}";

        base.Update(elapsed, position);
    }
}