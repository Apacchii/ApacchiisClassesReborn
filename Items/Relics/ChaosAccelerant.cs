using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class ChaosAccelerant : ModItem
	{
        public string desc = "Increases ability power by 65%\n" +
                             "Increases cooldown reduction by 35%\n" +
                             "Decreases ult cost by 20%\n" +
                             "Reduces healing power by 60%\n" +
                             "Decreases all weapon damage by 22%\n" +
                             "Decreases max health by 40%\n" +
                             "Decreases max mana by 40%\n" +
                             "[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, Eloraeon!][c/e796e8:]]";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault($"[Relic] Chaos Accelerant");
            // Tooltip.SetDefault(desc + $"\n[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, {donator}!][c/e796e8:]]");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Quest;

            Item.GetGlobalItem<ACMGlobalItem>().isRelic = true;
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc;
        }

        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasChaosAccelerant = true;
            acmPlayer.abilityPower += .65f;
            acmPlayer.cooldownReduction -= .35f;
            acmPlayer.ultCooldownReduction -= .2f;
            acmPlayer.healingPower -= .6f;
            player.GetDamage(DamageClass.Generic) -= .22f;


            base.UpdateVanity(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
                if (line.Mod == "Terraria" && line.Name == "Equipable")
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableRelic")}";
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

