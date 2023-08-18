using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeTransform;

public class SlimeLayer : PlayerDrawLayer
{
    private static Vector2 _scale = Vector2.One;
    private static int _timer = 0;

    public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.ArmOverItem);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player player = drawInfo.drawPlayer;

        if (player.GetModPlayer<SlimePlayer>().Active)
        {
            float yVel = Math.Abs(player.velocity.Y);

            if (yVel > 12f)
                yVel = 12f;

            var targetScale = new Vector2(1 - yVel * 0.05f, 1 + yVel * 0.05f); //Stretch & squash
            _scale = Vector2.Lerp(_scale, targetScale, 0.5f);
            int heldType = player.HeldItem.type;

            DrawInternalItem(drawInfo, player, heldType);
            DrawActualSlime(drawInfo, player);
        }
    }

    private static void DrawActualSlime(PlayerDrawSet drawInfo, Player player)
    {
        Main.instance.LoadNPC(NPCID.BlueSlime);
        var tex = TextureAssets.Npc[NPCID.BlueSlime].Value;
        var col = Lighting.GetColor(player.Center.ToTileCoordinates(), SlimeColours.AsColor(player.GetModPlayer<SlimePlayer>().slimeColor));
        var origin = new Vector2(tex.Width / 2f, tex.Height / 2);
        var pos = player.Bottom - Main.screenPosition + new Vector2(0, 2 + player.gfxOffY);
        var data = new DrawData(tex, pos, GetFrame(drawInfo.drawPlayer, tex.Size()), col * 0.7f, player.velocity.X * 0.005f, origin, _scale, SpriteEffects.None, 0);
        data.shader = drawInfo.drawPlayer.cMount;
        drawInfo.DrawDataCache.Add(data);
    }

    private static void DrawInternalItem(PlayerDrawSet drawInfo, Player player, int heldType)
    {
        if (ItemID.Sets.IsFood[heldType])
            return;

        var itemTex = TextureAssets.Item[heldType].Value;
        var itemCol = Lighting.GetColor(player.Center.ToTileCoordinates());
        var itemPos = player.Center - Main.screenPosition - new Vector2(0, MathF.Sin(_timer * 0.01f) * 2f) - new Vector2(0, player.gfxOffY);
        Rectangle? source = null;

        if (Main.itemAnimations[heldType] is not null)
            source = Main.itemAnimations[heldType].GetFrame(itemTex);

        var itemScale = new Vector2(12f / (source is not null ? Math.Max(source.Value.Width, source.Value.Height) : Math.Max(itemTex.Width, itemTex.Height)));
        var itemOrigin = source is not null ? source.Value.Size() / 2f : itemTex.Size() / 2f;
        var itemData = new DrawData(itemTex, itemPos, source, itemCol, player.velocity.X * 0.005f, itemOrigin, itemScale, SpriteEffects.None, 0);
        itemData.shader = drawInfo.drawPlayer.cMount;
        drawInfo.DrawDataCache.Add(itemData);
    }

    private static Rectangle GetFrame(Player player, Vector2 texSize)
    {
        _timer++;
        int frame = 1;

        if (player.OnGround()) //Animate only when on ground
        {
            float factor = 1 - (player.statLife / (float)player.statLifeMax2);
            int danger = 80 - (int)(60 * factor);
            frame = (_timer % danger) / (danger / 2);

            if (frame > 1)
                frame = 1;
        }

        return new Rectangle(0, (int)texSize.Y / 2 * frame, (int)texSize.X, (int)texSize.Y / 2);
    }
}
