using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeTransform;

public abstract class SlimeMountBuff : ModBuff
{
	public override string Texture => nameof(SlimeTransform) + "/Transforms/Textures/" + Name;

	protected abstract SlimeColours.Colours Color { get; }
	protected abstract int MountType { get; }

	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.buffNoSave[Type] = true;
	}

	public sealed override void Update(Player player, ref int buffIndex)
	{
		player.mount.SetMount(MountType, player);
		player.buffTime[buffIndex] = 10;
		player.GetModPlayer<SlimePlayer>().slimeColor = Color;

		//Copied & fixed up code from vanilla source for the Royal Gel
		player.npcTypeNoAggro[NPCID.BlueSlime] = true;
		player.npcTypeNoAggro[NPCID.MotherSlime] = true;
		player.npcTypeNoAggro[NPCID.LavaSlime] = true;
		player.npcTypeNoAggro[NPCID.DungeonSlime] = true;
		player.npcTypeNoAggro[NPCID.CorruptSlime] = true;
		player.npcTypeNoAggro[NPCID.Slimer] = true;
		player.npcTypeNoAggro[NPCID.Gastropod] = true;
		player.npcTypeNoAggro[NPCID.IlluminantSlime] = true;
		player.npcTypeNoAggro[NPCID.ToxicSludge] = true;
		player.npcTypeNoAggro[NPCID.IceSlime] = true;
		player.npcTypeNoAggro[NPCID.Crimslime] = true;
		player.npcTypeNoAggro[NPCID.SpikedIceSlime] = true;
		player.npcTypeNoAggro[NPCID.SpikedJungleSlime] = true;
		player.npcTypeNoAggro[NPCID.UmbrellaSlime] = true;
		player.npcTypeNoAggro[NPCID.RainbowSlime] = true;
		player.npcTypeNoAggro[NPCID.SlimeMasked] = true;
		player.npcTypeNoAggro[NPCID.SlimeRibbonWhite] = true;
		player.npcTypeNoAggro[NPCID.SlimeRibbonYellow] = true;
		player.npcTypeNoAggro[NPCID.SlimeRibbonGreen] = true;
		player.npcTypeNoAggro[NPCID.SlimeRibbonRed] = true;
		player.npcTypeNoAggro[NPCID.SandSlime] = true;

		SafeUpdate(player, ref buffIndex);
	}

    protected virtual void SafeUpdate(Player player, ref int buffIndex)
    {
    }
}