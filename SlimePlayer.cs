using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeTransform;

internal class SlimePlayer : ModPlayer
{
    internal bool Active => Player is not null && Player.mount is not null && Player.mount._data is not null && Player.HasBuff(Player.mount.BuffType) 
        && ModContent.GetModBuff(Player.mount.BuffType) is SlimeMountBuff;

    internal SlimeColours.Colours slimeColor;

    public override void Load() => On_PlayerDrawLayers.DrawPlayer_RenderAllLayers += DrawSlime;

    private void DrawSlime(On_PlayerDrawLayers.orig_DrawPlayer_RenderAllLayers orig, ref PlayerDrawSet drawinfo)
    {
        if (!drawinfo.drawPlayer.GetModPlayer<SlimePlayer>().Active)
            orig(ref drawinfo);
        else //Draws only the slime when active
        {
            drawinfo.DrawDataCache.Clear();
            drawinfo.ItemLocation.Y += 8f;
            ModContent.GetInstance<SlimeLayer>().DrawWithTransformationAndChildren(ref drawinfo);
            PlayerDrawLayers.DrawPlayer_27_HeldItem(ref drawinfo);
            orig(ref drawinfo);
        }
    }

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        if (Active) //Clears all layers when active
            (r, g, b, a) = (0, 0, 0, 0);
    }

    public override void ModifyHurt(ref Player.HurtModifiers modifiers)/* tModPorter Override ImmuneTo, FreeDodge or ConsumableDodge instead to prevent taking damage */
    {
        if (Active) //Replaces the sound and adds dust
        {
            modifiers.DisableSound();
            SoundEngine.PlaySound(Main.rand.NextBool() ? SoundID.Item154 : SoundID.Item155);

            for (int i = 0; i < 25; i++)
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.TintableDust, 2 * modifiers.HitDirection, -2f, 210, new Color(0, 255, 0) * 0.7f);
        }
    }

    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        for (int i = 0; i < 50; i++) //Adds dust
            Dust.NewDust(Player.position, Player.width, Player.height, DustID.TintableDust, 2 * hitDirection, -2f, 210, new Color(0, 255, 0) * 0.7f);
    }
}
