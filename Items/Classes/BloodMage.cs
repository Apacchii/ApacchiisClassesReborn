using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class BloodMage : ModItem
	{
        float baseStat1 = .007f;
        float stat1; // Magic Damage

        float baseStat2 = .011f;
        float stat2; // Max Mana

        float baseStat3 = .0045f;
        float stat3; // Health

        float baseBadStat = .005f;
        float badStat; // Defense

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
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.BloodSpray>(), 1);
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_P_Name")})-\n" + //Vein Ripper
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A1_Name")})-\n" + //Transfusion
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A2_Name")})-\n" + //Blood Enchantment
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A2_Prev")}\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A2_Prev_2")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_Ult_Name")})-\n" + //Regeneration
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")} p/lvl\n" +
                                                                         "+" + (stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMana")} p/lvl\n" +
                                                                         "+" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")} p/lvl");

            var level = modPlayer.bloodMageLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")}\n" +
                                                                      "+" + (level * stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMana")}\n" +
                                                                      "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (level * badStat * 100).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.bloodMageLevel == 0)
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
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableFighter")}";

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.hasBloodMage = true;
            acmPlayer.equippedClass = "Blood Mage";
            acmPlayer.ultChargeMax = 3780;
            acmPlayer.ability1MaxCooldown = 38;
            acmPlayer.ability2MaxCooldown = 0;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Magic Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; // Magic Crit
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Health
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMultNegative; // Defense

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Magic) += acmPlayer.bloodMageLevel * stat1 * acmPlayer.classStatMultiplier;
                    acmPlayer.manaMult += stat2 * acmPlayer.bloodMageLevel* acmPlayer.classStatMultiplier;
                    acmPlayer.lifeMult += stat3 * acmPlayer.bloodMageLevel * acmPlayer.classStatMultiplier;
                    acmPlayer.defenseMult -= acmPlayer.bloodMageLevel * badStat;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Magic) += acmPlayer.bloodMageLevel * stat1 * acmPlayer.classStatMultiplier;
                acmPlayer.manaMult += stat2 * acmPlayer.bloodMageLevel* acmPlayer.classStatMultiplier;
                acmPlayer.lifeMult += stat3 * acmPlayer.bloodMageLevel * acmPlayer.classStatMultiplier;
                acmPlayer.defenseMult -= acmPlayer.bloodMageLevel * badStat;
            }

            acmPlayer.classStatMultiplier = 1f;
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += acmPlayer.bloodMageLevel * .01f;

            // Class Menu Text
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_P_Name");
            acmPlayer.P_Desc = $"When hitting enemies with a magic weapon you have a chance to rip their veins, dealing additional damage based on a percentage of your held weapon's damage.\nWeapon percentage increases by 1% per level.";
            acmPlayer.P_Effect_1 = $"Bonus Damage: {(int)((Player.HeldItem.damage * (acmPlayer.bloodMageBasePassiveWeaponDamageMult + acmPlayer.bloodMagePassiveDamageLevel * acmPlayer.bloodMageLevel)) * acmPlayer.abilityPower)}";
            acmPlayer.P_Effect_2 = $"Weapon Percentage: {(decimal)((acmPlayer.bloodMageBasePassiveWeaponDamageMult + acmPlayer.bloodMagePassiveDamageLevel * acmPlayer.bloodMageLevel) * 100)}%";
            acmPlayer.P_Effect_3 = $"Chance: {(int)(acmPlayer.bloodMagePassiveChance * 100)}%";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A1_Name");
            acmPlayer.A1_Desc = $"Throw a blob of your own blood to seek out an enemy, when the blob hits its target it will return to you with a bit of the enemy's blood and heal you for 10% of the enemy's max health.";
            acmPlayer.A1_Effect_1 = $"Damage: {(decimal)((acmPlayer.bloodMageSiphonBaseDamage + acmPlayer.bloodMageLevel * 6) * acmPlayer.abilityPower)} = {acmPlayer.bloodMageSiphonBaseDamage} + 6 p/Level({ acmPlayer.bloodMageLevel * 6}) * AP";
            acmPlayer.A1_Effect_2 = $"Max Healing: {(decimal)acmPlayer.bloodMageSiphonHealMax * 100}% of your max health, except via healing power";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A2_Name");
            acmPlayer.A2_Desc = $"Enchant you weapon with your own blood, making it also cost the same amount of mana it uses as health but increasing all the damage you deal and refunding the health if you hit an enemy.";
            acmPlayer.A2_Effect_1 = $"Bonus Damage: {(decimal)(acmPlayer.bloodMageDamageGain * 100 + (acmPlayer.bloodMageLevel * .5f))}% = {(decimal)(.1f * 100)}% + 0.5% p/Level({acmPlayer.bloodMageLevel * .5f}%)";
            acmPlayer.A2_Effect_2 = $"Health Cost/Refund: {(decimal)(acmPlayer.bloodMageEnchantmentBaseManaCost * 100)}%";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_Ult_Name");
            acmPlayer.Ult_Desc = $"Quickly regenerate the blood of you and all your allies over time, regenerating a percentage of the healed player's max health at a medium rate for a limited number of ticks.";
            acmPlayer.Ult_Effect_1 = $"Max Health Heal: " + (decimal)acmPlayer.bloodMageBaseUltRegen * 100 + "% + 0.08% p/Level(" + acmPlayer.bloodMageLevel * 0.1f * 100 + "%) = " + (acmPlayer.bloodMageBaseUltRegen * 100 + acmPlayer.bloodMageLevel * .1f) + "%";
            acmPlayer.Ult_Effect_2 = $"Ticks: " + acmPlayer.bloodMageUltTicks + " ticks";
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

