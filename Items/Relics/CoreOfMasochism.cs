using ApacchiisClassesMod2.Configs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class CoreOfMasochism : ModItem
	{
        public string desc = "Increases the damage you take by 2x\n" +
                             $"Enemies you defeat have a low chance (configurable) of dropping 'Lost Rune Fragments'\n" +
                             "[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, Thunderaz!][c/e796e8:]]";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault($"[Relic] Blood Gem");
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
            acmPlayer.hasCoreOfMasochism = true;

            base.UpdateVanity(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            foreach (TooltipLine line in tooltips)
                if (line.Mod == "Terraria" && line.Name == "Equipable")
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableRelic")}";

            desc = "Increases the damage you take by 2x\n" +
                   $"Enemies you defeat have a low chance ({_ACMConfigServer.Instance.CoreMasochismChance:F2}%) of dropping 'Lost Rune Fragments'\n" +
                   "[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, Thunderaz!][c/e796e8:]]";
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

