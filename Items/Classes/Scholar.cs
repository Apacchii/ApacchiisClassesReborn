//using Microsoft.Xna.Framework;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using ApacchiisClassesMod2.Configs;
//using Terraria.Localization;
//
//namespace ApacchiisClassesMod2.Items.Classes
//{
//	public class Scholar : ModItem
//	{
//        int classLevel;
//
//        float baseStat1 = .005f;
//        float stat1; // Minion Damage
//
//        float baseStat2 = .0125f;
//        float stat2; // Ability Power
//
//        float baseStat3 = .005f;
//        float stat3; // Minion Crit
//
//        float baseBadStat = .006f;
//        float badStat; // Health
//
//        public override void SetDefaults()
//		{
//            Item.width = 30;
//			Item.height = 30;
//			Item.accessory = true;	
//			Item.value = 0;
//			Item.rare = ItemRarityID.Pink;
//
//            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
//            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
//            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
//            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;
//
//            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
//        }
//
//		public override void AddRecipes()
//		{
//            var recipe = CreateRecipe(1);
//            recipe.AddIngredient(ModContent.ItemType<OrangeCloth>());
//            recipe.Register();
//        }
//
//        //public override void OnCreate(ItemCreationContext context)
//        //{
//        //    if (_ACMConfigServer.Instance.classWeaponsEnabled)
//        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.SeekingSpirit>(), 1);
//        //        //Main.player[Main.myPlayer].QuickSpawnItem(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.SeekingSpirit>(), 1);
//        //
//        //    base.OnCreate(context);
//        //}
//
//        public override void ModifyTooltips(List<TooltipLine> tooltips)
//        {
//            Player Player = Main.player[Main.myPlayer];
//
//            var modPlayer = Player.GetModPlayer<ACMPlayer>();
//            classLevel = modPlayer.plagueLevel;
//
//            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
//            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
//                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.P_Name")})-\n" +
//                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.P_Prev")}\n" +
//                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A1_Name")})-\n" +
//                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A1_Prev")}\n" +
//                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A2_Name")})-\n" +
//                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A2_Prev")}\n" +
//                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.Ult_Name")})-\n" +
//                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.Ult_Prev")}");
//
//            HoldSToPreview.OverrideColor = Color.CadetBlue;
//            AbilityPreview.OverrideColor = Color.CadetBlue;
//
//            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.SummonDamage")} p/lvl\n" +
//                                                                         "+" + (stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")} p/lvl\n" +
//                                                                         "+" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% Minion Crit Chance p/lvl");
//            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");
//
//            var level = classLevel;
//
//            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
//            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.SummonDamage")}\n" +
//                                                                      "+" + (level * 100 * stat2 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")}\n" +
//                                                                      "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% Minion Crit Chance");
//            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");
//
//            lineLevel.OverrideColor = new Color(200, 150, 25);
//            lineBadStat.OverrideColor = new Color(200, 50, 25);
//            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);
//
//            if (classLevel == 0)
//            {
//                tooltips.Add(lineLevel);
//                tooltips.Add(lineStatsPreview);
//                tooltips.Add(lineBadStatPreview);
//            }
//            else
//            {
//                tooltips.Add(lineLevel);
//                tooltips.Add(lineStats);
//                tooltips.Add(lineBadStat);
//            }
//
//            if (Player.controlUp)
//                tooltips.Add(AbilityPreview);
//            else
//                tooltips.Add(HoldSToPreview);
//
//            foreach (TooltipLine line in tooltips)
//                if (line.Mod == "Terraria" && line.Name == "Equipable")
//                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableCarry")}";
//
//            base.ModifyTooltips(tooltips);
//        }
//
//        public override void UpdateAccessory (Player Player, bool hideVisual)
//		{
//            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
//            acmPlayer.hasClass = true;
//            acmPlayer.equippedClass = "Scholar";
//            acmPlayer.ultChargeMax = 3000;
//            acmPlayer.ability1MaxCooldown = 27;
//            acmPlayer.ability2MaxCooldown = 11;
//            classLevel = acmPlayer.plagueLevel;
//
//            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Minion Damage
//            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; // Ability Power
//            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Minion Crit
//            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative; // Health
//
//            if (_ACMConfigServer.Instance.configHidden)
//            {
//                if (!hideVisual)
//                {
//                    Player.GetDamage(DamageClass.Summon) += classLevel * stat1 * acmPlayer.classStatMultiplier;
//                    acmPlayer.abilityPower += stat2 * classLevel * acmPlayer.classStatMultiplier;
//                    acmPlayer.minionCritChance += stat3 * classLevel * acmPlayer.classStatMultiplier;
//                    acmPlayer.lifeMult -= classLevel * badStat;
//                }
//            }
//            else
//            {
//                Player.GetDamage(DamageClass.Summon) += classLevel * stat1 * acmPlayer.classStatMultiplier;
//                acmPlayer.abilityPower += stat2 * classLevel * acmPlayer.classStatMultiplier;
//                acmPlayer.minionCritChance += stat3 * classLevel * acmPlayer.classStatMultiplier;
//                acmPlayer.lifeMult -= classLevel * badStat;
//            }
//
//            acmPlayer.classStatMultiplier = 1f;
//            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += classLevel * .01f;
//
//            // Class Menu Text
//            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.P_Name");
//            acmPlayer.P_Desc = $"Your minions are relentless, having a chance to land critical hits on enemies they hit.\nYour minions deal increased damage vs Plagued enemies and their crit chance is multiplicatively increased by 25% vs Plagued enemies too.";
//            acmPlayer.P_Effect_1 = $"Plagued Damage Bonus: {acmPlayer.plaguePassivePlaguedDamageBase * 100}% + 0.25% p/Lvl({(acmPlayer.plaguePassivePlaguedPerLevel * 100 * acmPlayer.plagueLevel).ToString("F2")}%) = {((acmPlayer.plaguePassivePlaguedDamageBase + acmPlayer.plaguePassivePlaguedPerLevel * classLevel) * 100).ToString("F2")}%";
//            acmPlayer.P_Effect_2 = $"Bonus Base Minion Crit Chance: {((acmPlayer.plaguePassiveCritBase) * 100f).ToString("F2")}%";
//
//            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A1_Name");
//            acmPlayer.A1_Desc = $"Release an insect to damage enemies and infect enemies with Plagued. This insect and all others have a small chance to multiply themselves everytime they hit an enemy.\nEnemies affected by Plagued take damage heavy over time based on their max health.\n[Bosses take reduced damage over time]";
//            acmPlayer.A1_Effect_1 = $"Contact Damage: {acmPlayer.plagueInsectDamageBase} + {acmPlayer.plagueInsectDamagePerLevel} p/Lvl({acmPlayer.plagueInsectDamagePerLevel * classLevel}) * AP = {(acmPlayer.plagueInsectDamageBase + acmPlayer.plagueInsectDamagePerLevel * classLevel * acmPlayer.abilityPower).ToString("F0")}";
//            acmPlayer.A1_Effect_2 = $"Plagued Duration: {acmPlayer.plagueDebuffDuration}s";
//
//            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A2_Name");
//            acmPlayer.A2_Desc = $"Deal damage to any enemy affected by Plagued within range and re-apply it's damage over time.\nIf an enemy is not affected by Plagued, apply it to them with 33% of its original duration.";
//            acmPlayer.A2_Effect_1 = $"Damage: {acmPlayer.plagueInfectionBurstBase} + {acmPlayer.plagueInfectionBurstPerLevel} p/Lvl({acmPlayer.plagueInfectionBurstPerLevel * classLevel}) * AP = {((acmPlayer.plagueInfectionBurstBase + acmPlayer.plagueInfectionBurstPerLevel * classLevel) * acmPlayer.abilityPower).ToString("F0")}";
//            acmPlayer.A2_Effect_2 = $"Range: {acmPlayer.plagueInfectionRange}";
//
//            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.Ult_Name");
//            acmPlayer.Ult_Desc = $"Create a deadzone around yourself that continously strikes enemies, dealing true damage to any enemies that get close to you.";
//            acmPlayer.Ult_Effect_1 = $"Damage: {acmPlayer.plagueDeadzoneBaseDamage} + {acmPlayer.plagueDeadzoneDamagePerLevel} p/Lvl({acmPlayer.plagueDeadzoneDamagePerLevel * acmPlayer.plagueLevel}) * AP = {((acmPlayer.plagueDeadzoneBaseDamage + acmPlayer.plagueDeadzoneDamagePerLevel * acmPlayer.plagueLevel) * acmPlayer.abilityPower).ToString("F0")}";
//            acmPlayer.Ult_Effect_2 = $"Duration: {acmPlayer.plagueDeadzoneDuration / 60}s";
//        }
//
//        public override bool CanEquipAccessory(Player player, int slot, bool modded)
//        {
//            if (player.GetModPlayer<ACMPlayer>().hasClass == true)
//                return false;
//
//            if (!modded)
//                return false;
//
//            return base.CanEquipAccessory(player, slot, modded);
//        }
//    }
//}