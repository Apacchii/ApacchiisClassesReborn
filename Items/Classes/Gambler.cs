using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Biomes;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;
using ApacchiisClassesMod2.UI;
using System.Linq;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Gambler : ModItem
	{
        float baseStat1 = .0065f;
        float stat1; // Ranged Dmg

        float baseStat2 = .003f;
        float stat2; // Attack Speed

        float baseStat3 = .0045f;
        float stat3; // Ult Cost

        float baseBadStat = .00335f;
        float badStat; // Ranged Crit

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Green;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GreenCloth>());
            recipe.Register();
        }

        //public override void OnCreate(ItemCreationContext context)
        //{
        //    if (_ACMConfigServer.Instance.classWeaponsEnabled)
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.TrainingRapier>(), 1);
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_Ult_Prev")}\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_Ult_Prev2")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedDamage")} p/lvl\n" +
                                                                         "+" + (stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AttackSpeed")} p/lvl\n" +
                                                                         "-" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.UltCostReduction")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" +(badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedCrit")} p/lvl");

            var level = modPlayer.gamblerLevel;
            
            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedDamage")}\n" +
                                                                      "+" + (level * stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AttackSpeed")}\n" +
                                                                      "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.UltCostReduction")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedCrit")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.gamblerLevel == 0)
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
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableCarry")}";

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.equippedClass = "Gambler";
            acmPlayer.ability1MaxCooldown = 6;
            acmPlayer.ability2MaxCooldown = 53;
            acmPlayer.ultChargeMax = 3900;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Ranged) += acmPlayer.gamblerLevel * stat1 * acmPlayer.classStatMultiplier;
                    Player.GetAttackSpeed(DamageClass.Ranged) += acmPlayer.gamblerLevel * stat2 * acmPlayer.classStatMultiplier;
                    acmPlayer.ultCooldownReduction -= acmPlayer.gamblerLevel * stat3 * acmPlayer.classStatMultiplier;
                    Player.GetCritChance(DamageClass.Ranged) -= acmPlayer.gamblerLevel * badStat;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Ranged) += acmPlayer.gamblerLevel * stat1 * acmPlayer.classStatMultiplier;
                Player.GetAttackSpeed(DamageClass.Ranged) += acmPlayer.gamblerLevel * stat2 * acmPlayer.classStatMultiplier;
                acmPlayer.ultCooldownReduction -= acmPlayer.gamblerLevel * stat3 * acmPlayer.classStatMultiplier;
                Player.GetCritChance(DamageClass.Ranged) -= acmPlayer.gamblerLevel * badStat;
            }

            acmPlayer.classStatMultiplier = 1f;
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += acmPlayer.gamblerLevel * .01f;

            // Class Menu Text
            acmPlayer.P_Name = "Luck Of The Draw";
            acmPlayer.P_Desc = $"Any damage you deal is randomized, having the same chance to deal either higher or lower damage than normal.";
            if (acmPlayer.gamblerPassiveFeedback)
                acmPlayer.P_Desc += "(A green > will appear below you when you deal more damage than normal)\n(A red < will appear below you when you deal more damage than normal)";
            acmPlayer.P_Effect_1 = $"Damage Dealt Min/Max Multipliers: {(decimal)(acmPlayer.gamblerPassiveMinDmg * 100)}%-{(decimal)(acmPlayer.gamblerPassiveMaxDmg * 100)}%";
            acmPlayer.P_Effect_2 = $"Increase p/Level: {(decimal)(acmPlayer.gamblerPassiveMinDmgPerLevel * 100)}% p/Lvl(+{(decimal)(acmPlayer.gamblerPassiveMinDmgPerLevel * acmPlayer.gamblerLevel * 100)}%)";

            acmPlayer.A1_Name = "Roll The Dice";
            acmPlayer.A1_Desc = "Throw dice in a cone shape, the dice home in on enemies and can temporarily pass through blocks.";
            if (acmPlayer.gamblerTalent_5 == "L")
                acmPlayer.A1_Desc += "\n[Talent] You now throw 2 additional dice.";
            acmPlayer.A1_Effect_1 = $"Dice count: {acmPlayer.gamblerDiceCount}";
            acmPlayer.A1_Effect_2 = $"Dice damage: {(int)((acmPlayer.gamblerDiceDamageBase + acmPlayer.gamblerDiceDamagePerLevel * acmPlayer.gamblerLevel) * acmPlayer.abilityPower)} = {acmPlayer.gamblerDiceDamageBase} + {acmPlayer.gamblerDiceDamagePerLevel} p/Lvl({acmPlayer.gamblerDiceDamagePerLevel * acmPlayer.gamblerLevel}) * AP";

            acmPlayer.A2_Name = "Lucky Streak";
            acmPlayer.A2_Desc = "Gain a buff that increases both the minimum and maximum damage your passive deals.";
            if(acmPlayer.gamblerTalent_7 == "L")
                acmPlayer.A2_Desc += "\n[Talent] The buff now also grants 5% dodge chance.";
            acmPlayer.A2_Effect_1 = $"Min/Max Increase: {(decimal)(acmPlayer.gamblerPassiveBoostDamage * 100)}% = 25% + {acmPlayer.gamblerPassiveBoostPerLevel * 100}% p/Lvl({(decimal)(acmPlayer.gamblerPassiveBoostPerLevel * acmPlayer.gamblerLevel * 100)}%)";
            acmPlayer.A2_Effect_2 = $"Duration: {(decimal)(acmPlayer.gamblerPassiveBoostMaxDuration / 60)}s";

            acmPlayer.Ult_Name = "Jackpot";
            acmPlayer.Ult_Desc = "Gain a buff that increases your attack speed, ability power and decreases your [Roll the Dice]'s ability cooldown.";
            acmPlayer.Ult_Effect_1 = $"Ability Power: {(decimal)(acmPlayer.gamblerUltAbilityPower * 100)}% = 100% + {(decimal)(acmPlayer.gamblerUltAbilityPowerPerLevel * 100)}% p/Lvl({(decimal)(acmPlayer.gamblerUltAbilityPowerPerLevel * acmPlayer.gamblerLevel * 100)}%)";
            acmPlayer.Ult_Effect_2 = $"Cooldown Reduction: {(decimal)(acmPlayer.gamblerUltCooldownReduction * 100)}% = 50% + {(decimal)(acmPlayer.gamblerUltCooldownReductionPerLevel * 100)}% p/Lvl({(decimal)(acmPlayer.gamblerUltCooldownReductionPerLevel * acmPlayer.gamblerLevel * 100)}%)";
            acmPlayer.Ult_Effect_3 = $"Attack Speed: {(decimal)(acmPlayer.gamblerUltAttackSpeed * 100)}%";
            acmPlayer.Ult_Effect_4 = $"Duration: {(decimal)(acmPlayer.gamblerUltDurationBase / 60)}s";

            acmPlayer.aghanimsText = "- Lucky Streak duration increased by 1 second\n" +
                                     "- Roll The Dice base damage increased by 12\n" +
                                     "- Each dice that hits an enemy heals you between 0.25% to 0.5% of your max health";
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