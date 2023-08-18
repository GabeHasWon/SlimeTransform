using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SlimeTransform;

internal class SlimeTransformConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

	[DefaultValue(true)]
	public bool SpecialSlimeMovement { get; set; }

	[DefaultValue(true)]
	public bool FloatInWater { get; set; }
}
