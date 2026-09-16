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
            string damageType = "MagicDamage";
            if (_ACMConfigServer.Instance.generalistClasses) damageType = "AllDamage";
            string critType = "MagicCrit";
            if (_ACMConfigServer.Instance.generalistClasses) critType = "AllCrit";

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Prev_1")}\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Prev_2")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Prev")}\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Prev_2")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")} p/lvl\n" +
                                                                         "+" + (stat2 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2." + critType)} p/lvl\n" +
                                                                         "-" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.ManaCost")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2." + damageType)} p/lvl");

            var level = modPlayer.globalClassLevel.Count;
            string classStats;
            if (Player.controlUp)
            {
                classStats = $"+{level * stat1 * 100:F2}% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")}\n" +
                             $"+{level * stat3 * 100:F2}% {Language.GetTextValue("Mods.ApacchiisClassesMod2.ManaCost")}\n" +
                             $"+{level * stat2:F2}% {Language.GetTextValue("Mods.ApacchiisClassesMod2." + critType)}";
            }
            else
            {
                classStats = $"+{level * stat1 * 100 * modPlayer.classStatMultiplier:F2}% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")}\n" +
                             $"+{level * stat3 * 100 * modPlayer.classStatMultiplier:F2}% {Language.GetTextValue("Mods.ApacchiisClassesMod2.ManaCost")}\n" +
                             $"+{level * stat2 * modPlayer.classStatMultiplier:F2}% {Language.GetTextValue("Mods.ApacchiisClassesMod2." + critType)}";
            }

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", classStats);
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2." + damageType)}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (level == 0)
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
            acmPlayer.ability1MaxCooldown = 42;
            acmPlayer.ability2MaxCooldown = 12;
            int currentClassLevel = acmPlayer.globalClassLevel.Count;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Magic Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; // Magic Crit
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Health
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative; // Defense

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                    ClassStats();
            }
            else { ClassStats(); }

            // Class Menu Text [x = y + z p/lvl]
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Name");
            acmPlayer.P_Desc = $"Hitting enemies with magic weapons has a chance to rip a fragment of their soul causing it to harm any nearby enemies.\nSoul Rip chance increases by 1% per level.";
            acmPlayer.P_Effect_1 = $"Damage: {(int)((acmPlayer.soulmancerSoulRipDamage_Base + acmPlayer.soulmancerSoulRipDamage_PerLevel * currentClassLevel) * acmPlayer.abilityPower)} = {acmPlayer.soulmancerSoulRipDamage_Base} + {acmPlayer.soulmancerSoulRipDamage_PerLevel} p/Level({acmPlayer.soulmancerSoulRipDamage_PerLevel * currentClassLevel}) * AP";
            acmPlayer.P_Effect_2 = $"Rip Chance: {(int)((acmPlayer.soulmancerSoulRipChance_Base + acmPlayer.soulmancerSoulRipChance_PerLevel * currentClassLevel) * 100)}%";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Name");
            acmPlayer.A1_Desc = $"For a short duration, everytime you hit an enemy with a soul fragment, recall it to yourself, consuming it and healing you for a small percentage of your max health per fragment consumed.";
            acmPlayer.A1_Effect_1 = $"Duration: {(decimal)(acmPlayer.soulmancerConsumeDuration_Base / 60)}s";
            acmPlayer.A1_Effect_2 = $"Heal p/Fragment: {(decimal)(acmPlayer.soulmancerConsumeHeal_Base * 100)}% of your max health";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Name");
            if (acmPlayer.hasAghanims)
                acmPlayer.A2_Desc = "Shatter the soul of nearby enemies dealing heavy damage and ripping an additional fragment per soul\n" +
                                    "shattered.\n" +
                                    "[Aghanim's Scepter] Now casts at your cursor's position with reduced area of effect";
            else
                acmPlayer.A2_Desc = "Shatter the soul of nearby enemies dealing heavy damage and ripping an additional fragment per soul\n" +
                                    "shattered.";
            acmPlayer.A2_Effect_1 = $"Damage: {(int)((acmPlayer.soulmancerSoulShatterDamage_Base + acmPlayer.soulmancerShatterDamage_PerLevel * currentClassLevel) * acmPlayer.abilityPower)} = {acmPlayer.soulmancerSoulShatterDamage_Base} + {acmPlayer.soulmancerShatterDamage_PerLevel} p/Level({currentClassLevel * acmPlayer.soulmancerShatterDamage_PerLevel}) * AP";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Name");
            acmPlayer.Ult_Desc = "Rapidly rip fragments of your own soul, slightly draining your own health per fragment.\n" +
                                 "Each soul deals 1.5x [Soul Rip]'s damage.";
            acmPlayer.Ult_Effect_1 = $"Health Cost: {acmPlayer.soulmancerSacrificeHealthCost_Base * 100}% p/Soul (Total: {(decimal)(acmPlayer.soulmancerSacrificeHealthCost_Base * acmPlayer.soulmancerSacrificeSoulCount * 100)}%)";
            acmPlayer.Ult_Effect_2 = $"Souls Released: {acmPlayer.soulmancerSacrificeSoulCount_Base}";

            acmPlayer.aghanimsText = "- Soul Shatter now casts at your cursor's position\n" +
                                     "- Soul Shatter area of effect decreased by 150\n" +
                                     "- Ability power is increased by 6%";

            acmPlayer.classStatMultiplier = 1f;
        }

        private void ClassStats()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            int currentClassLevel = acmPlayer.globalClassLevel.Count;

            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += (float)(currentClassLevel * .01f);

            if (_ACMConfigServer.Instance.generalistClasses)
            {
                acmPlayer.abilityPower += currentClassLevel * stat1 * acmPlayer.classStatMultiplier;
                player.GetCritChance(DamageClass.Magic) += (int)(stat2 * currentClassLevel * acmPlayer.classStatMultiplier);
                player.manaCost -= stat3 * currentClassLevel * acmPlayer.classStatMultiplier;
                player.GetDamage(DamageClass.Generic) -= currentClassLevel * badStat;
            }
            else
            {
                acmPlayer.abilityPower += currentClassLevel * stat1 * acmPlayer.classStatMultiplier;
                player.GetCritChance(DamageClass.Magic) += (int)(stat2 * currentClassLevel * acmPlayer.classStatMultiplier);
                player.manaCost -= stat3 * currentClassLevel * acmPlayer.classStatMultiplier;
                player.GetDamage(DamageClass.Magic) -= currentClassLevel * badStat;
            }

                
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