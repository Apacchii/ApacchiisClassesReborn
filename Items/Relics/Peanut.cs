using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class Peanut : ModItem
	{
        public string desc = "Decreases max health by 16%\n" +
                             "Decreases damage dealt by 8%\n" +
                             "Increases crit damage by 14%\n" +
                             "Increases crit chance by 4%\n" +
                             "Increases movement speed by 10%\n" +
                             "Increases movement acceleration by 1.73%\n" +
                             "[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, Peanutbutta187!][c/e796e8:]]";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault($"[Relic] The 173rd Peanut");
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
            acmPlayer.hasPeanut = true;
            player.GetDamage(DamageClass.Generic) -= .08f;
            player.GetCritChance(DamageClass.Generic) += 4;
            player.moveSpeed += .1f;
            acmPlayer.critDamageMult += .14f;
            player.runAcceleration += .0173f;


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

