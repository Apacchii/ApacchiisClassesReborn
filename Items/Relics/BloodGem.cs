using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class BloodGem : ModItem
	{
        public string desc = "Your projectile and minion hits heal you for 1% max health (4s Cooldown)\n" +
                             "Your non-projectile melee hits heal you for 2% max health (2s Cooldown)";
        string donator = "Matty";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Relic] Blood Gem");
            Tooltip.SetDefault(desc + $"\n[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, {donator}!][c/e796e8:]]");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Quest;

            Item.GetGlobalItem<ACMGlobalItem>().isRelic = true;
        }

        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayerOtherEffects>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasBloodGem = true;

            base.UpdateVanity(player);
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<ACMPlayer>().hasRelic == true)
                return false;

            if (!modded)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

