using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class NiterihsJewelryBox : ModItem
	{
        public string desc = "Increases max health by 6%\n" +
                             "Increases damage by 4%\n" +
                             "Increases crit chance by 3%\n" +
                             "Increases defense by 5%\n" +
                             "Increases defense by 2\n" +
                             "Increases invulnerability time after being hit by 1 second";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault($"[Relic] Niterih's Bracelet");
            // Tooltip.SetDefault(desc);
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Quest;

            Item.GetGlobalItem<ACMGlobalItem>().isCraftableRelic = true;
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc;
        }

        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasNeterihsToken = true;
            player.GetDamage(DamageClass.Generic) += .04f;
            player.GetCritChance(DamageClass.Generic) += 3;
            acmPlayer.lifeMult += .06f;
            acmPlayer.defenseMult += .05f;
            player.statDefense += 2;

            base.UpdateVanity(player);
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<NiterihsBracelet>());
            recipe.AddIngredient(ModContent.ItemType<NiterihsEarring>());
            recipe.AddIngredient(ModContent.ItemType<NiterihsLuckyToken>());
            recipe.AddIngredient(ModContent.ItemType<NiterihsNecklace>());
            recipe.AddIngredient(ModContent.ItemType<NiterihsRing>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
                if (line.Mod == "Terraria" && line.Name == "Equipable")
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableRelic")}\n[c/a16ce6:[Craftable Relic][c/a16ce6:]]";
            TooltipLine description = new TooltipLine(Mod, "RelicDescription", desc);
            tooltips.Add(description);

            base.ModifyTooltips(tooltips);
        }



        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (!modded)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

