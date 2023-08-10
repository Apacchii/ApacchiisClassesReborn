using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Scout : ModItem
	{
        float baseStat1 = .0075f;
        float stat1; // Ranged Dmg

        float baseStat2 = .002f;
        float stat2; // Acceleration

        float baseStat3 = .0035f;
        float stat3; // Dodge

        float baseBadStat = .0035f;
        float badStat; // Health | CHANGE IN ACMPlayer.cs !!!

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
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

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
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.FadingDagger>(), 1);
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Prev_1")} " + modPlayer.scoutUltInvDuration / 60 + $" {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Prev_2")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedDamage")} p/lvl\n" +
                                                                         "+" + (stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MovementAcceleration")} p/lvl\n" +
                                                                         "+" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.DodgeChance")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");

            var level = modPlayer.scoutLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedDamage")}\n" +
                                                                      "+" + (level * stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MovementAcceleration")}\n" +
                                                                      "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.DodgeChance")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.scoutLevel == 0)
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
            acmPlayer.hasScout = true;
            acmPlayer.equippedClass = "Scout";
            acmPlayer.ultChargeMax = 1900;
            acmPlayer.ability1MaxCooldown = 43;
            acmPlayer.ability2MaxCooldown = 10;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative;

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1 * acmPlayer.classStatMultiplier;
                    Player.runAcceleration += stat2 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
                    acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1 * acmPlayer.classStatMultiplier;
                Player.runAcceleration += stat2 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
                acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
            }

            if (acmPlayer.scoutTalent_2 == "R" || acmPlayer.scoutTalent_2 == "B")
            {
                if (acmPlayer.scoutCanDoubleJump)
                    Player.hasJumpOption_Blizzard = true;
                else
                    Player.hasJumpOption_Blizzard = false;
            }
            else
            {
                if (acmPlayer.scoutCanDoubleJump)
                    Player.hasJumpOption_Cloud = true;
                else
                    Player.hasJumpOption_Cloud = false;
            }
            
            Player.moveSpeed += acmPlayer.scoutPassiveSpeedBonus;

            acmPlayer.classStatMultiplier = 1f;
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += acmPlayer.scoutLevel * .01f;

            // Class Menu Text
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_Name");
            acmPlayer.P_Desc = $"The scout has a free double jump and has increased movement speed.";
            if (acmPlayer.scoutCanDoubleJump)
                acmPlayer.P_Effect_1 = "Double Jump: Enabled";
            else
                acmPlayer.P_Effect_1 = "Double Jump: Disabled";
            acmPlayer.P_Effect_2 = $"Speed Bonus: {(int)(acmPlayer.scoutPassiveSpeedBonus * 100)}%";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A1_Name");
            acmPlayer.A1_Desc = $"Take a sip from an energy drink you made yourself. This drink will temporarily increase the damage you deal.";
            acmPlayer.A1_Effect_1 = $"Bonus Damage Dealt: {(decimal)(((acmPlayer.scoutColaDamageBonus - 1f) + acmPlayer.scoutColaDamageBonusLevel * acmPlayer.scoutLevel) * 100)}% = {(decimal)(acmPlayer.scoutColaDamageBonus - 1f) * 100}% + {acmPlayer.scoutColaDamageBonusLevel * 100}% p/Level({(decimal)(acmPlayer.scoutColaDamageBonusLevel * acmPlayer.scoutLevel * 100)}%)";
            acmPlayer.A2_Effect_2 = $"Duration: {acmPlayer.scoutColaDuration / 60}s";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A2_Name");
            acmPlayer.A2_Desc = $"Place an explosive trap under your cursor's position, it'll take some time to arm itself. The trap will explode if an enemy gets too close, dealing damage to all enemies within 2x the trap's detection range.";
            acmPlayer.A2_Effect_1 = $"Damage: {(int)((acmPlayer.scoutTrapBaseDamage + acmPlayer.scoutTrapDamageLevel * acmPlayer.scoutLevel) * acmPlayer.abilityPower)} = {acmPlayer.scoutTrapBaseDamage} + {acmPlayer.scoutTrapDamageLevel} p/Level({acmPlayer.scoutTrapDamageLevel * acmPlayer.scoutLevel})";
            acmPlayer.A2_Effect_2 = $"Detection Range: {acmPlayer.scoutTrapRange}";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Name");
            acmPlayer.Ult_Desc = $"Become invincible for {acmPlayer.scoutUltInvDuration / 60} seconds and gain increased movement speed, jump height andauto jump for a longer duration.";
            acmPlayer.Ult_Effect_1 = $"Mobility Duration: {acmPlayer.scoutUltDuration / 60}s";
            acmPlayer.Ult_Effect_2 = $"Speed Bonus: {(decimal)((acmPlayer.scoutUltSpeed + acmPlayer.scoutUltSpeedLevel * acmPlayer.scoutLevel) * 100)}% = {(int)(acmPlayer.scoutUltSpeed * 100)}% + {(int)(acmPlayer.scoutUltSpeedLevel * 100)}% p/Level({(decimal)(acmPlayer.scoutUltSpeedLevel * acmPlayer.scoutLevel * 100)}%)";
            acmPlayer.Ult_Effect_4 = $"Jump Height: {(int)(acmPlayer.scoutUltJump * 100)}%";
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

