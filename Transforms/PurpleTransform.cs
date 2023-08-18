using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeTransform.Transforms;

internal class PurpleSlimeTransformationItem : SlimeTransformItem
{
    protected override string ColorName => "Purple";
    protected override int MountType => ModContent.MountType<PurpleSlimeMount>();

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ItemID.Gel, 150).
            AddTile(TileID.Bottles).
            Register();
    }
}

internal class PurpleSlimeMountBuff : SlimeMountBuff
{
    protected override SlimeColours.Colours Color => SlimeColours.Colours.Purple;
    protected override int MountType => ModContent.MountType<PurpleSlimeMount>();
}

internal class PurpleSlimeMount : BaseSlimeTransformMount
{
    protected override int BuffType => ModContent.BuffType<PurpleSlimeMountBuff>();
    protected override int DustType => DustID.PurpleMoss;
}
