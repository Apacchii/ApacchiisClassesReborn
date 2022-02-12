using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class AghanimsScepter : ModItem
	{
        public string desc = "[Effect varies on class]\n" +
                             "Upgrades some of your class' abilities and/or stats:";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Relic] Aghanim's Scepter");
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
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc;
        }

        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasAghanims = true;

            if (acmPlayer.hasBloodMage)
            {
                acmPlayer.abilityPower += .15f;
                acmPlayer.cooldownReduction -= .05f;
            }
                

            if (acmPlayer.hasCommander)
            {
                acmPlayer.ability1MaxCooldown -= 6;
                acmPlayer.commanderBannerRange += 50;
            }
                

            if (acmPlayer.hasVanguard)
            {
                acmPlayer.vanguardPassiveReflectAmount += .65f;
                player.endurance += .04f;
            }

            if(acmPlayer.hasScout)
                acmPlayer.scoutUltInvDuration += 60;

            base.UpdateVanity(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine effect = new TooltipLine(Mod, "Effect", "No class equipped");

            switch (modPlayer.equippedClass)
            {
                case "Blood Mage":
                    effect.text = "- Ability power is increased by 15%\n" +
                                  "- Cooldown reduction increased by 5%\n" +
                                  "- Transfusion now also heals for 8% of the damage it deals\n" +
                                  "- Transfusion now heals teammates at a medium rate for 25% of the healing you get";
                    break;
                case "Commander":
                    effect.text = "- Banner cooldown decreased by 6 seconds\n" +
                                  "- Banner range is increased by 50";
                    break;
                case "Scout":
                    effect.text = "- Ultimate invulnerability increased by 1 second\n" +
                                  "- Hit-a-Soda now increases ranged crit chance by 15% for its duration";
                    break;
                case "Vanguard":
                    effect.text = "- Decreases damage taken by enemies by 4%\n" +
                                  "- Passive reflected damage is increased by 65%";
                    break;
                default:
                    effect.text = "No class equipped";
                    break;
            }


            tooltips.Add(effect);


            base.ModifyTooltips(tooltips);
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

