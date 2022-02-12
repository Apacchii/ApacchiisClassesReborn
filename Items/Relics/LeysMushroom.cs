using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ApacchiisClassesMod2.Items.Relics
{
	
    public class LeysMushroom : ModItem
    {
        public string desc = "[Relic upgrades in Hardmode]";
        string donator = "Ris";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Relic] Ley's Mushroom");
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
            var acmPlayer = player.GetModPlayer<ACMPlayerOtherEffects>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasLeysMushroom = true;

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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine effect = new TooltipLine(Mod, "Effect", "Effects");

            int duration = 3;
            int heal = 3;
            int crit = 5;
            int endurance = 4;
            int damage = 4;

            if (Main.hardMode)
            {
                duration = 4;
                heal = 4;
                crit = 7;
                endurance = 6;
                damage = 5;
            }

            effect.text = $"Hitting an enemy has a 4% chance to heal you for {heal}% missing health\n" +
                          $"Hitting an enemy has a 10% chance to give you one of these buffs for {duration} seconds:\n" +
                          $"- {crit}% Increased critical strike chance\n" +
                          $"- {endurance}% Decreased damage taken\n" +
                          $"- {damage}% Increased damage dealt";

            tooltips.Add(effect);


            base.ModifyTooltips(tooltips);
        }

    }
}
