using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class BrokenHeart : ModItem
	{
        public string desc = "Reduces potion sickness duration by 5 seconds\nPotions heal for an additional 7% of your max health";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Relic] Broken Heart");
            Tooltip.SetDefault(desc);
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
            acmPlayer.hasBrokenHeart = true;
            player.potionDelayTime -= 300;

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

