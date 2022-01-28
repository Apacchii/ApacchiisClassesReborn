using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Scout : ModItem
	{
        float baseStat1 = .0095f;
        float stat1; // Ranged Dmg

        float baseStat2 = .0022f;
        float stat2; // Acceleration

        float baseStat3 = .0033f;
        float stat3; // Dodge

        float baseBadStat = .006f;
        float badStat; // Health

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Scout");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Green;

            stat1 = baseStat1 * ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * ACMConfigServer.Instance.classStatMult;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GreenCloth>());
            recipe.Register();
        }

        public override void OnCreate(ItemCreationContext context)
        {
            if (ACMConfigServer.Instance.classWeaponsEnabled)
                Main.player[Main.myPlayer].QuickSpawnItem(ModContent.ItemType<ClassWeapons.FadingDagger>(), 1);
            base.OnCreate(context);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", "[Hold 'W' to preview abilities]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                "-(P: Agility)-\n" +
                "The scout can double jump and has slightly increased movement speed.\n" +
                "-(A1: Hit-a-Soda)-\n" +
                "Take a sip an energy drink you made yourself, increasing the damage you deal by temporarily.\n" +
                "-(A2: Explosive Trap)-\n" +
                "Place a low cooldown explosive trap under your cursor, enemies that get too close to the trap will take damage.\n" +
                "-(Ult: Nuclear-Slap (TM))-\n" +
                "Become invincible for " + modPlayer.scoutUltInvDuration / 60 + " seconds, and gain increased mobility for a perid of time.");

            HoldSToPreview.overrideColor = Color.CadetBlue;
            AbilityPreview.overrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100) + "% Ranged Damage p/lvl\n" +
                                                                         "+" + (decimal)stat2 * 100 + "% Movement Acceleration p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100) + "% Dodge Chance p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + "% Health p/lvl");

            var level = modPlayer.scoutLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100) + "% Ranged Damage\n" +
                                                                      "+" + level * (decimal)stat2 * 100 + "% Movement Acceleration\n" +
                                                                      "+" + level * (decimal)(stat3 * 100) + "% Dodge Chance");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)(badStat * 100) + "% Health");

            lineLevel.overrideColor = new Color(200, 150, 25);
            lineBadStat.overrideColor = new Color(200, 50, 25);
            lineBadStatPreview.overrideColor = new Color(200, 50, 25);

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

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.hasScout = true;
            acmPlayer.equippedClass = "Scout";
            acmPlayer.ultChargeMax = 1800;
            acmPlayer.ability1MaxCooldown = 40;
            acmPlayer.ability2MaxCooldown = 10;

            stat1 = baseStat1 * ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * ACMConfigServer.Instance.classStatMult;

            if (ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1;
                    Player.runAcceleration += stat2 * acmPlayer.scoutLevel;
                    acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel;
                    Player.statLifeMax2 -= (int)(badStat * acmPlayer.scoutLevel);
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1;
                Player.runAcceleration += stat2 * acmPlayer.scoutLevel;
                acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel;
                Player.statLifeMax2 -= (int)(badStat * acmPlayer.scoutLevel);
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

