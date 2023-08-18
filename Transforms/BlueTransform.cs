using Terraria.ID;
using Terraria.ModLoader;

namespace SlimeTransform.Transforms;

internal class BlueSlimeTransformationItem : SlimeTransformItem
{
    protected override string ColorName => "Blue";
    protected override int MountType => ModContent.MountType<BlueSlimeMount>();

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ItemID.Gel, 120).
            AddTile(TileID.Bottles).
            Register();
    }
}

internal class BlueSlimeMountBuff : SlimeMountBuff
{
    protected override SlimeColours.Colours Color => SlimeColours.Colours.Blue;
    protected override int MountType => ModContent.MountType<BlueSlimeMount>();
}

internal class BlueSlimeMount : BaseSlimeTransformMount
{
    protected override int BuffType => ModContent.BuffType<BlueSlimeMountBuff>();
    protected override int DustType => DustID.DungeonBlue;
}
