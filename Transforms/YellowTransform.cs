using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeTransform.Transforms;

internal class YellowSlimeTransformationItem : SlimeTransformItem
{
    protected override string ColorName => "Yellow";
    protected override int MountType => ModContent.MountType<YellowSlimeMount>();

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ItemID.Gel, 175).
            AddTile(TileID.Bottles).
            Register();
    }
}

internal class YellowSlimeMountBuff : SlimeMountBuff
{
    protected override SlimeColours.Colours Color => SlimeColours.Colours.Yellow;
    protected override int MountType => ModContent.MountType<YellowSlimeMount>();
}

internal class YellowSlimeMount : BaseSlimeTransformMount
{
    protected override int BuffType => ModContent.BuffType<YellowSlimeMountBuff>();
    protected override int DustType => DustID.YellowStarfish;
}
