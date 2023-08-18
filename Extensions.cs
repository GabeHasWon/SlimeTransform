using Microsoft.Xna.Framework;
using Terraria;

namespace SlimeTransform;

internal static class Extensions
{
    public static bool OnGround(this Player player) 
    {
        var direcion = Vector2.UnitY * 16f;
        if (Collision.TileCollision(player.BottomLeft - direcion, direcion, player.width, 6, player.controlDown, false, (int)player.gravDir) != direcion)
            return true;
        return false;
    } 
}