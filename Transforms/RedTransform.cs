using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeTransform.Transforms;

internal class RedSlimeTransformationItem : SlimeTransformItem
{
    protected override string ColorName => "Red";
    protected override int MountType => ModContent.MountType<RedSlimeMount>();

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ItemID.Gel, 130).
            AddTile(TileID.Bottles).
            Register();
    }
}

internal class RedSlimeMountBuff : SlimeMountBuff
{
    protected override SlimeColours.Colours Color => SlimeColours.Colours.Red;
    protected override int MountType => ModContent.MountType<RedSlimeMount>();
}

internal class RedSlimeMount : BaseSlimeTransformMount
{
    protected override int BuffType => ModContent.BuffType<RedSlimeMountBuff>();
    protected override int DustType => DustID.RedStarfish;
}
