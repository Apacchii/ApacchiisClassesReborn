using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Crusader : ModItem
	{
        float baseStat1 = .005f;
        float stat1; // Melee Dmg

        float baseStat2 = .0045f;
        float stat2; // Healing Power

        float baseStat3 = .008f;
        float stat3; // Defense

        float baseBadStat = .002f;
        float badStat; // HP

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Red;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<RedCloth>());
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
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Crusader_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MeleeDamage")} p/lvl\n" +
                                                                         "+" + (stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.HealingPower")} p/lvl\n" +
                                                                         "+" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");

            var level = modPlayer.crusaderLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MeleeDamage")}\n" +
                                                                      "+" + (level * stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.HealingPower")}\n" +
                                                                      "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.crusaderLevel == 0)
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
            acmPlayer.hasCrusader = true;
            acmPlayer.equippedClass = "Crusader";
            acmPlayer.ability1MaxCooldown = 72;
            acmPlayer.ability2MaxCooldown = 6;
            acmPlayer.ultChargeMax = 3100;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Melee) += acmPlayer.crusaderLevel * stat1 * acmPlayer.classStatMultiplier;
                    acmPlayer.healingPower += acmPlayer.crusaderLevel * stat2 * acmPlayer.classStatMultiplier;
                    acmPlayer.defenseMult += acmPlayer.crusaderLevel * stat3 * acmPlayer.classStatMultiplier;
                    acmPlayer.lifeMult -= acmPlayer.crusaderLevel * badStat;
                    if (acmPlayer.defenseMult * acmPlayer.crusaderLevel * stat3 * acmPlayer.classStatMultiplier < 1)
                        Player.statDefense++;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Melee) += acmPlayer.crusaderLevel * stat1 * acmPlayer.classStatMultiplier;
                acmPlayer.healingPower += acmPlayer.crusaderLevel * stat2 * acmPlayer.classStatMultiplier;
                acmPlayer.defenseMult += acmPlayer.crusaderLevel * stat3 * acmPlayer.classStatMultiplier;
                acmPlayer.lifeMult -= acmPlayer.crusaderLevel * badStat;
                if (acmPlayer.defenseMult * acmPlayer.crusaderLevel * stat3 * acmPlayer.classStatMultiplier < 1)
                    Player.statDefense++;
            }

            acmPlayer.classStatMultiplier = 1f;
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += acmPlayer.crusaderLevel * .01f;

            // Class Menu Text
            acmPlayer.P_Name = "Tenacity";
            acmPlayer.P_Desc = $"Reduce damage taken by {(-(acmPlayer.crusaderEndurance - 1f) * 100).ToString("F0")}%.\nThis effect is increased by an additional {(-(acmPlayer.crusaderEnduranceBuff - 1f) * 100).ToString("F0")}% while under the effects of a healing buff.";

            acmPlayer.A1_Name = "Protection";
            acmPlayer.A1_Desc = "Apply a healing buff to all players that slowly heals them each second.\nPlayers regen health at a rate of 1% of their max health per second";
            acmPlayer.A1_Effect_1 = $"Healing: {(acmPlayer.crusaderHealing * 100 * acmPlayer.healingPower).ToString("F2")}% Max Health";

            acmPlayer.A2_Name = "Hallowed Hammer";
            acmPlayer.A2_Desc = "Throw a hallowed hammer towards your cursor, the hammer will damage all enemies in its path and return to you, dealing the same damage on the way back.";
            acmPlayer.A2_Effect_1 = $"Damage: {(int)((acmPlayer.crusaderHammerDamageBase + acmPlayer.crusaderHammerDamageLevel * acmPlayer.crusaderLevel) * acmPlayer.abilityPower)} = {acmPlayer.crusaderHammerDamageBase} + {acmPlayer.crusaderHammerDamageLevel} p/Level({acmPlayer.crusaderHammerDamageLevel * acmPlayer.crusaderLevel})";

            acmPlayer.Ult_Name = "Guardian Angel";
            acmPlayer.Ult_Desc = "A guardian angel watches over everyone, granting all players a reduction to the damage they take for a duration.";
            acmPlayer.Ult_Effect_1 = $"Damage Reduction: {(decimal)((acmPlayer.crusaderGuardianAngelEndurance + acmPlayer.crusaderGuardianAngelEnduranceLevel * acmPlayer.crusaderLevel) * 100)}% = {acmPlayer.crusaderGuardianAngelEndurance * 100}% + {acmPlayer.crusaderGuardianAngelEnduranceLevel * 100}% p/Level({(decimal)(acmPlayer.crusaderGuardianAngelEnduranceLevel * acmPlayer.crusaderLevel * 100)}%)";
            acmPlayer.Ult_Effect_2 = $"Duration: {acmPlayer.crusaderGuardianAngelDuration / 60}s";

            acmPlayer.aghanimsText = "- Ability Power increased by 10%\n" +
                                     "- Cooldown Reduction increased by 10%\n" +
                                     "- Healing Power increased by 10%\n" +
                                     "- Ultimate Cost reduced by 10%";
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