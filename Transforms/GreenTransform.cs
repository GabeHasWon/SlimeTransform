using Terraria.ModLoader;

namespace SlimeTransform.Transforms;

internal class GreenSlimeTransformationItem : SlimeTransformItem
{
    protected override string ColorName => "Green";
    protected override int MountType => ModContent.MountType<GreenSlimeMount>();
}

internal class GreenSlimeMountBuff : SlimeMountBuff
{
    protected override SlimeColours.Colours Color => SlimeColours.Colours.Green;
    protected override int MountType => ModContent.MountType<GreenSlimeMount>();
}

internal class GreenSlimeMount : BaseSlimeTransformMount
{
    protected override int BuffType => ModContent.BuffType<GreenSlimeMountBuff>();
}
