using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class SleepingBabySqueaker : ModItem
	{
        public string desc = "Reduces your base max health by 60%\n" +
                             "Increases damage dealt by 82%\n" +
                             "During invulnerability frames your mobility is highly increased\n" +
                             "Increases invulnerability frames by 20";
        string donator = "Matty";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Relic] Sleeping Baby Squeaker");
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
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc;
        }

        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasSqueaker = true;
            acmPlayer.lifeMult -= .6f;
            player.GetDamage(DamageClass.Generic) += .82f;

            if (player.immune)
            {
                player.moveSpeed += 1f;
                player.runAcceleration += .06f;
                player.jumpSpeedBoost += 3f;
            }

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

