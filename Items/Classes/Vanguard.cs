using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Vanguard : ModItem
	{
        float baseStat1 = .009f;
        float stat1; // Melee Dmg

        float baseStat2 = .011f;
        float stat2; // Defense

        float baseStat3 = .009f;
        float stat3; // HP

        float baseBadStat = .003f;
        float badStat; // Attack Speed

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Vanguard");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Red;

            stat1 = baseStat1 * ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * ACMConfigServer.Instance.classStatMult;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<RedCloth>());
            recipe.Register();
        }

        public override void OnCreate(ItemCreationContext context)
        {
            if (ACMConfigServer.Instance.classWeaponsEnabled)
                Main.player[Main.myPlayer].QuickSpawnItem(ModContent.ItemType<ClassWeapons.TrainingRapier>(), 1);
            base.OnCreate(context);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", "[Hold 'W' to preview abilities]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                "-(P: Enchanted Armor)-\n" +
                "Enemies take damage when hitting you.\n" +
                "-(A1: Spear Of Light)-\n" +
                "Throw a spear, dragging enemies hit with it and exploding when colliding with terrain.\n" +
                "-(A2: Barrier Of Light)-\n" +
                "Cast a barrier around you, reducing damage taken.\n" +
                "-(Ult: Sword Of Judgement)-\n" +
                "A huge sword comes falling from the skies, dealing heavy damage and executing enemies.");

            HoldSToPreview.overrideColor = Color.CadetBlue;
            AbilityPreview.overrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100) + "% Melee Damage p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * 100) + "% Defense p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100) + "% Max Health p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + "% Attack Speed p/lvl (does not apply to tools)");

            var level = modPlayer.vanguardLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (decimal)(level * 1.2f) + "% Melee Damage\n" +
                                                                      "+" + (decimal)(level * stat2 * 100) + "% Defense\n" +
                                                                      "+" + (decimal)(level * stat3 * 100) + "% Max Health");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (decimal)(level * badStat * 100) + "% Attack Speed");

            lineLevel.overrideColor = new Color(200, 150, 25);
            lineBadStat.overrideColor = new Color(200, 50, 25);
            lineBadStatPreview.overrideColor = new Color(200, 50, 25);

            if (modPlayer.vanguardLevel == 0)
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
            acmPlayer.hasVanguard = true;
            acmPlayer.equippedClass = "Vanguard";
            acmPlayer.ability1MaxCooldown = 28; //28
            acmPlayer.ability2MaxCooldown = 48; //48
            acmPlayer.ultChargeMax = 2700;

            stat1 = baseStat1 * ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * ACMConfigServer.Instance.classStatMult;

            if (ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Melee) += acmPlayer.vanguardLevel * stat1;
                    acmPlayer.defenseMult += acmPlayer.vanguardLevel * stat2;
                    acmPlayer.lifeMult += acmPlayer.vanguardLevel * stat3;
                    acmPlayer.attackSpeed -= acmPlayer.vanguardLevel * badStat;
                    if (acmPlayer.defenseMult * acmPlayer.bloodMageLevel * stat2 < 1)
                        Player.statDefense++;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Melee) += acmPlayer.vanguardLevel * stat1;
                acmPlayer.defenseMult += acmPlayer.vanguardLevel * stat2;
                acmPlayer.lifeMult += acmPlayer.vanguardLevel * stat3;
                acmPlayer.attackSpeed -= acmPlayer.vanguardLevel * badStat;
                if (acmPlayer.defenseMult * acmPlayer.bloodMageLevel * stat2 < 1)
                    Player.statDefense++;
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

