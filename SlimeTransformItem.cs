using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SlimeTransform;

public abstract class SlimeTransformItem : ModItem
{
    public override string Texture => nameof(SlimeTransform) + "/Transforms/Textures/" + Name;

    protected abstract string ColorName { get; }
    protected abstract int MountType { get; }

    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (Main.expertMode && NPC.downedSlimeKing)
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", Language.GetTextValue("Mods.SlimeTransform.SlimeImmunityNotice")));

        if (ModContent.GetInstance<SlimeTransformConfig>().SpecialSlimeMovement)
            tooltips.Add(new TooltipLine(Mod, "SlimeMovementNotice", Language.GetTextValue("Mods.SlimeTransform.SlimeMovementNotice")));

        tooltips.Add(new TooltipLine(Mod, "SlimeConfigNotice", Language.GetTextValue("Mods.SlimeTransform.SlimeMoveStyleNotice")));
    }

    public override void SetDefaults()
    {
        Item.DefaultToMount(MountType);
        Item.width = 20;
        Item.height = 22;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(0, 0, 5, 0);
        Item.UseSound = SoundID.Item25;
    }

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ItemID.Gel, 100).
            AddTile(TileID.Bottles).
            Register();
    }
}