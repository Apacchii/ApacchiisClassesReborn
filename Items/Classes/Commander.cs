using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Commander : ModItem
	{
        float baseStat1 = .0055f;
        float stat1; // Minion Dmg

        float baseStat2 = .1f;
        float stat2; // Minion Slots

        float baseStat3 = .0115f;
        float stat3;// Whip Range

        float baseBadStat = .0011f;
        float badStat; // Acceleration

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Orange;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<OrangeCloth>());
            recipe.Register();
        }

        //public override void OnCreate(ItemCreationContext context)
        //{
        //    if (_ACMConfigServer.Instance.classWeaponsEnabled)
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.SeekingSpirit>(), 1);
        //        //Main.player[Main.myPlayer].QuickSpawnItem(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.SeekingSpirit>(), 1);
        //
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.SummonDamage")} p/lvl\n" +
                                                                         "+" + (stat2 * modPlayer.classStatMultiplier).ToString("F2") + $" {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMinions")} p/lvl\n" +
                                                                         "+" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.WhipRange")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MovementAcceleration")} p/lvl");

            var level = modPlayer.commanderLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.SummonDamage")}\n" +
                                                                      "+" + (level * stat2 * modPlayer.classStatMultiplier).ToString("F2") + $" {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMinions")}\n" +
                                                                      "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.WhipRange")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MovementAcceleration")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.commanderLevel == 0)
            {
                tooltips.Add(lineLevel);
                tooltips.Add(lineStatsPreview);
                tooltips.Add(lineBadStatPreview);
            }
            else
            {
                tooltips.Add(lineLevel);
                tooltips.Add(lineStats);
                tooltips.Add(lineBadStat);
            }

            if (Player.controlUp)
                tooltips.Add(AbilityPreview);
            else
                tooltips.Add(HoldSToPreview);

            foreach (TooltipLine line in tooltips)
                if (line.Mod == "Terraria" && line.Name == "Equipable")
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableSupport")}";

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.hasCommander = true;
            acmPlayer.equippedClass = "Commander";
            acmPlayer.ultChargeMax = 3000;
            acmPlayer.ability1MaxCooldown = 50;
            acmPlayer.ability2MaxCooldown = 28;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Minion Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; ; // Minion Slots
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Whip Range
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative; // Acceleration

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Summon) += acmPlayer.commanderLevel * stat1 * acmPlayer.classStatMultiplier;
                    Player.maxMinions += (int)(stat2 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier);
                    Player.whipRangeMultiplier += stat3 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier;
                    Player.runAcceleration -= badStat;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Magic) += acmPlayer.commanderLevel * stat1 * acmPlayer.classStatMultiplier;
                Player.maxMinions += (int)(stat2 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier);
                Player.whipRangeMultiplier += stat3 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier;
                Player.runAcceleration -= badStat;
            }

            acmPlayer.classStatMultiplier = 1f;
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += acmPlayer.commanderLevel * .01f;

            // Class Menu Text
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_P_Name");
            acmPlayer.P_Desc = $"Gain bonus damage reduction for each minion slot you have.";
            acmPlayer.P_Effect_1 = $"Endurance: {(decimal)(acmPlayer.commanderPassiveEndurance * acmPlayer.Player.maxMinions * 100)}%";
            acmPlayer.P_Effect_2 = $"Max Minions: {acmPlayer.Player.maxMinions}";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A1_Name");
            if(!acmPlayer.hasAghanims)
                acmPlayer.A1_Desc = $"Place a war banner at your feet.\nPlayers inside the banner's radius are granted damage reduction and increased damage. This buff persists for a while after leaving the banner's radius.";
            else
                acmPlayer.A1_Desc = $"Place a war banner that will follow you around.\nPlayers inside the banner's radius are granted damage reduction and increased damage. This buff persists for a while after leaving the banner's radius.";
            acmPlayer.A1_Effect_1 = $"Endurance: {(decimal)((1f - acmPlayer.commanderBannerEndurance) * 100)}%";
            acmPlayer.A1_Effect_2 = $"Bonus Damage: {(int)((acmPlayer.commanderBannerDamage - 1f) * 100)}%";
            acmPlayer.A1_Effect_4 = $"Duration: {acmPlayer.commanderBannerDuration / 60}s + 0.5s p/Level({(acmPlayer.commanderLevel * .4f).ToString("F0")}s) = {((acmPlayer.commanderLevel * 24 + acmPlayer.commanderBannerDuration) / 60).ToString("F0")}s";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A2_Name");
            acmPlayer.A2_Desc = $"Scream loudly, intimidating enemies around you and knocking them back, increasing the damage they take for a period of time.";
            acmPlayer.A2_Effect_1 = $"Bonus Damage: {(decimal)(acmPlayer.commanderCryBonusDamage * 100)}% + 1% p/Level({acmPlayer.commanderLevel}%) = {(int)((acmPlayer.commanderCryBonusDamage + acmPlayer.commanderLevel * .01f) * 100)}%";
            acmPlayer.A2_Effect_2 = $"Damage: {(int)((acmPlayer.commanderCryBaseDamage + acmPlayer.commanderCryDamageLevel * acmPlayer.commanderLevel) * acmPlayer.abilityPower)} = {acmPlayer.commanderCryBaseDamage} + {acmPlayer.commanderCryDamageLevel} p/Level({acmPlayer.commanderCryDamageLevel * acmPlayer.commanderLevel}) * AP";
            acmPlayer.A2_Effect_3 = $"Debuff Duration: {(decimal)(acmPlayer.commanderCryDuration / 60)}s";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_Ult_Name");
            acmPlayer.Ult_Desc = $"Inspire all players, causing them to always land critical hits on enemies.\nMinions can also crit during this duration.";
            acmPlayer.Ult_Effect_1 = $"Duration: {acmPlayer.commanderUltDuration / 60}s + 0.25s p/Level({(decimal)(acmPlayer.commanderLevel * .15f)}s) = {(decimal)((acmPlayer.commanderLevel * 15 + acmPlayer.commanderUltDuration) / 60)}s";
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<ACMPlayer>().hasClass == true)
                return false;

            if (!modded)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

