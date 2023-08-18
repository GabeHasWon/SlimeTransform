using Microsoft.Xna.Framework;

namespace SlimeTransform;

public static class SlimeColours
{
    public enum Colours : byte
    {
        Green,
        Blue,
        Red,
        Yellow,
        Purple
    }

    public static Color AsColor(Colours color)
    {
        return color switch
        {
            Colours.Green => new Color(0, 1f, 0, 0.1f),
            Colours.Red => new Color(1f, 0, 0, 0.1f),
            Colours.Blue => new Color(0f, 0.65f, 1f, 0.1f),
            Colours.Yellow => new Color(1f, 1f, 0, 0.1f),
            Colours.Purple => new Color(0.8f, 0f, 1f, 0.1f),
            _ => Color.Gray
        };
    }
}
