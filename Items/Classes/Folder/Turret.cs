/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Inventor : ModItem
	{
        float baseStat1 = .0078f;
        float stat1; // Ranged Dmg

        float baseStat2 = .6f;
        float stat2; // Ranged crit

        float baseStat3 = .01f;
        float stat3; // Ultimate Cost

        int baseBadStat = 15;
        int badStat; // Potion Sickness

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Inventor");
        }

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
            badStat = (int)(baseBadStat * _ACMConfigServer.Instance.classStatMult);

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
        //        //Main.player[Main.myPlayer].QuickSpawnItemDirect(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.FadingDagger>(), 1);
        //        base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", "[Hold 'W' to preview abilities]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                "-(P: On-The-Go Maintenance)-\n" +
                "If the Inventor is within their turret's range, both of them get a 10% attack speed boost.\n" +
                "-(A1: Deploy Turret)-\n" +
                "Deploy your turret at ground closest to your cursor, the turret will identify enemies and attack them even through walls.\n" +
                "-(A2: Pocket Cog Shotgun)-\n" +
                "Fire your cog shotgun, shooting a barrage of 5 fast-moving cogs towards your cursor's position\n" +
                "-(Ult: Overclock)-\n" +
                "Overclock your turret, granting it increased range and attack speed.");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + "% Ranged Damage p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + "% Movement Acceleration p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + "% Dodge Chance p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + "% Health p/lvl");

            var level = modPlayer.scoutLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + "% Ranged Damage\n" +
                                                                      "+" + level * (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + "% Movement Acceleration\n" +
                                                                      "+" + level * (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + "% Dodge Chance");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)(badStat * 100) + "% Health");

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

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.hasInventor = true;
            acmPlayer.equippedClass = "Inventor";
            acmPlayer.ultChargeMax = 60;
            acmPlayer.ability1MaxCooldown = 6;
            acmPlayer.ability2MaxCooldown = 18;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = (int)(baseBadStat * _ACMConfigServer.Instance.classStatMult);

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    //Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1;
                    //Player.runAcceleration += stat2 * acmPlayer.scoutLevel;
                    //acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel;
                    //acmPlayer.lifeMult -= (int)(badStat * acmPlayer.scoutLevel);
                }
            }
            else
            {
                //Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1;
                //Player.runAcceleration += stat2 * acmPlayer.scoutLevel;
                //acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel;
                //acmPlayer.lifeMult -= (int)(badStat * acmPlayer.scoutLevel);
            }
            
            acmPlayer.classStatMultiplier = 1f;
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
}*/