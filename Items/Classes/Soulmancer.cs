using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Soulmancer : ModItem
	{
        float baseStat1 = .021f;
        float stat1; // Ability Power

        float baseStat2 = .5f;
        float stat2; // Magic Crit

        float baseStat3 = .004f;
        float stat3; // Mana Cost

        float baseBadStat = .0085f;
        float badStat; // Magic Damage

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Blue;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<BlueCloth>());
            recipe.Register();
        }

        //public override void OnCreate(ItemCreationContext context)
        //{
        //    if(_ACMConfigServer.Instance.classWeaponsEnabled)
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.SoulBurner>(), 1);
        //        base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();
            
            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Prev_1")}\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Prev_2")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Prev")}\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Prev2")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")} p/lvl\n" +
                                                                         "+" + (stat2 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicCrit")} p/lvl\n" +
                                                                         "-" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.ManaCost")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")} p/lvl");

            var level = modPlayer.soulmancerLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")}\n" +
                                                                      "+" + (level * stat2 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicCrit")}\n" +
                                                                      "-" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.ManaCost")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.soulmancerLevel == 0)
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
            acmPlayer.hasSoulmancer = true;
            acmPlayer.equippedClass = "Soulmancer";
            acmPlayer.ultChargeMax = 1920;
            acmPlayer.ability1MaxCooldown = 29;
            acmPlayer.ability2MaxCooldown = 12;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Magic Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; // Magic Crit
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Health
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative; // Defense

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    acmPlayer.abilityPower += acmPlayer.soulmancerLevel * stat1 * acmPlayer.classStatMultiplier;
                    Player.GetCritChance(DamageClass.Magic) += (int)(stat2 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier);
                    Player.manaCost -= stat3 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier;
                    Player.GetDamage(DamageClass.Magic) -= acmPlayer.soulmancerLevel * badStat;
                }
            }
            else
            {
                acmPlayer.abilityPower += acmPlayer.soulmancerLevel * stat1 * acmPlayer.classStatMultiplier;
                Player.GetCritChance(DamageClass.Magic) += (int)(stat2 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier);
                Player.manaCost -= stat3 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier;
                Player.GetDamage(DamageClass.Magic) -= acmPlayer.soulmancerLevel * badStat;
            }

            acmPlayer.classStatMultiplier = 1f;
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += acmPlayer.soulmancerLevel * .01f;

            // Class Menu Text
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Name");
            acmPlayer.P_Desc = $"Hitting enemies with magic weapons has a chance to rip a fragment of their soul causing it to harm any nearby enemies.\nSoul Rip chance increases by 1% per level.";
            acmPlayer.P_Effect_1 = $"Damage: {(int)(acmPlayer.soulmancerSoulRipDamage * acmPlayer.abilityPower)} = {acmPlayer.soulmancerSoulRipDamage_Base} + {acmPlayer.soulmancerSoulRipDamage_PerLevel} p/Level({acmPlayer.soulmancerSoulRipDamage_PerLevel * acmPlayer.soulmancerLevel}) * AP";
            acmPlayer.P_Effect_2 = $"Rip Chance: {(int)(acmPlayer.soulmancerSoulRipChance * 100)}%";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Name");
            acmPlayer.A1_Desc = $"For a short duration, everytime you hit an enemy with a soul fragment, recall it to yourself, consuming it and healing you for a small percentage of your max health per fragment consumed.\n" +
                                $"This ability can only be activated and will only heal if you are below half health.";
            acmPlayer.A1_Effect_1 = $"Duration: {(decimal)(acmPlayer.soulmancerConsumeDuration / 60)}s";
            acmPlayer.A1_Effect_2 = $"Heal p/Fragment: {(decimal)(acmPlayer.soulmancerConsumeHeal * 100)}%";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Name");
            if (acmPlayer.hasAghanims)
                acmPlayer.A2_Desc = "Shatter the soul of nearby enemies dealing heavy damage and ripping an additional fragment per soul\n" +
                                    "shattered.\n" +
                                    "[Aghanim's Scepter] Now casts at your cursor's position with reduced range";
            else
                acmPlayer.A2_Desc = "Shatter the soul of nearby enemies dealing heavy damage and ripping an additional fragment per soul\n" +
                                    "shattered.";
            acmPlayer.A2_Effect_1 = $"Damage: {(int)(acmPlayer.soulmancerSoulShatterDamage * acmPlayer.abilityPower)} = {acmPlayer.soulmancerSoulShatterDamage_Base} + {acmPlayer.soulmancerShatterDamage_PerLevel} p/Level({acmPlayer.soulmancerLevel * acmPlayer.soulmancerShatterDamage_PerLevel}) * AP";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Name");
            acmPlayer.Ult_Desc = "Rapidly rip fragments of your own soul, slightly draining your own health per fragment.\n" +
                                 "Each soul deals 1.5x [Soul Rip]'s damage.";
            acmPlayer.Ult_Effect_1 = $"Health Cost: {acmPlayer.soulmancerSacrificeHealthCost * 100}% p/Soul";
            acmPlayer.Ult_Effect_2 = $"Souls Released: {acmPlayer.soulmancerSacrificeSoulCount_Base}";
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

