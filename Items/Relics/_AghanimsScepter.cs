using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class AghanimsScepter : ModItem
	{
        bool warning = false;
        public string desc = "[Effect varies on class]\n" +
                             "Upgrades some of your class' abilities and/or stats";

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault(desc);
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<AghanimsShard>();
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Quest;


            Item.GetGlobalItem<ACMGlobalItem>().isRelic = true;
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc + "\n";
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
                acmPlayer.ability1MaxCooldown -= 5;
                acmPlayer.commanderBannerRange += 25;
                acmPlayer.bannerFollowsPlayer = true;
            }
                
            if (acmPlayer.hasVanguard)
            {
                acmPlayer.vanguardPassiveReflectAmount += .65f;
                player.endurance += .04f;
            }

            if(acmPlayer.hasScout)
                acmPlayer.scoutUltInvDuration += 60;

            if (acmPlayer.hasSoulmancer)
            {
                acmPlayer.soulmancerSoulShatterRange -= 175;
                acmPlayer.abilityPower += .06f;
                acmPlayer.soulmancerSoulShatterCastTarget = Main.MouseWorld;
            }

            if (acmPlayer.hasCrusader)
            {
                acmPlayer.abilityPower += .1f;
                acmPlayer.cooldownReduction -= .1f;
                acmPlayer.healingPower += .1f;
                acmPlayer.ultCooldownReduction -= .1f;
            }

            if(acmPlayer.equippedClass == "Gambler")
            {
                acmPlayer.gamblerDiceAghanimsHeal = true;
                acmPlayer.gamblerDiceDamageBase += 12;
                acmPlayer.gamblerPassiveBoostMaxDuration += 60;
            }

            if (acmPlayer.equippedClass == "Plague")
            {
                acmPlayer.plagueDeadzoneRange += 100;
                acmPlayer.ultCooldownReduction -= .1f;
            }

            base.UpdateVanity(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine effect = new TooltipLine(Mod, "Effect", "No class equipped");

            //switch (modPlayer.equippedClass)
            //{
            //    case "Blood Mage":
            //        effect.Text = "- Ability power is increased by 15%\n" +
            //                      "- Cooldown reduction increased by 5%\n" +
            //                      "- Transfusion now also heals you and teammates for 10% of the damage it deals";
            //        break;
            //    case "Commander":
            //        effect.Text = "- Banner cooldown decreased by 5 seconds\n" +
            //                      "- Banner range increased by 25\n" +
            //                      "- Banner now follows you around";
            //        break;
            //    case "Scout":
            //        effect.Text = "- Ultimate invulnerability increased by 1 second\n" +
            //                      "- Hit-a-Soda now increases ranged crit chance by 15% for its duration";
            //        break;
            //    case "Vanguard":
            //        effect.Text = "- Decreases damage taken by 4%\n" +
            //                      "- Passive reflected damage is increased by 65%";
            //        break;
            //    case "Soulmancer":
            //        effect.Text = "- Soul Shatter now casts at your cursor's position\n" +
            //                      "- Soul Shatter range decreased by 175\n" +
            //                      "- Ability power is increased by 6%";
            //        break;
            //    case "Crusader":
            //        effect.Text = "- Ability Power increased by 10%\n" +
            //                      "- Cooldown Reduction increased by 10%\n" +
            //                      "- Healing Power increased by 10%\n" +
            //                      "- Ultimate Cost reduced by 10%";
            //        break;
            //    case "Gambler":
            //        effect.Text = "- Lucky Streak duration increased by 1 second\n" +
            //                      "- Roll The Dice base damage increased by 12\n" +
            //                      "- Each dice that hits an enemy heals you between 0.25% to 0.5% of your max health";
            //        break;
            //
            //    case "Plague":
            //
            //        break;
            //
            //    default:
            //        effect.Text = "No class equipped!";
            //        break;
            //}
            effect.Text = modPlayer.aghanimsText;
            tooltips.Add(effect);

            foreach (TooltipLine line in tooltips)
                if (line.Mod == "Terraria" && line.Name == "Equipable")
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableRelic")}";

            base.ModifyTooltips(tooltips);
        }

        public override void AddRecipes()
        {
            //// AddCustomShimmerResult can be used to change the decrafting results.
            //CreateRecipe()
            //    .AddIngredient(ModContent.ItemType<AghanimsScepter>())
            //    .AddCustomShimmerResult(ModContent.ItemType<AghanimsShard>())
            //    .AddCondition(Condition.Hardmode)
            //    .Register();

            base.AddRecipes();
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (!modded || player.GetModPlayer<ACMPlayer>().hasAghanimsShard)
                if (player.GetModPlayer<ACMPlayer>().hasAghanimsShard && Main.mouseLeft)
                {
                    Main.NewText("Cannot equip. Aghanim's Shard has already been consumed!");
                    return false;
                }
                else return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

