/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Paladin : ModItem
	{
        float baseStat1 = .0225f;
        float stat1; // Melee Damage

        float baseStat2 = .5f;
        float stat2; // Endurance

        float baseStat3 = .006f;
        float stat3; // Cooldown Reduction

        float baseBadStat = .007f;
        float badStat; // Melee Attack Speed

        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Paladin");
        }

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
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

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
            if(_ACMConfigServer.Instance.classWeaponsEnabled)
                //Main.player[Main.myPlayer].QuickSpawnItem(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.SoulBurner>(), 1);
                base.OnCreate(context);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();
            
            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", "[Hold 'W' to preview abilities]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                "-(P: )-\n" +
                ".\n" +
                "-(A1: )-\n" +
                ".\n" +
                "-(A2: )-\n" +
                ".\n" +
                "-(Ult: )-\n" +
                ".");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100) + "% Melee Damage p/lvl\n" +
                                                                         "+" + (decimal)stat2 + "% Endurance p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100) + "% Cooldown Reduction p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + "% Melee Attack Speed p/lvl");

            var level = modPlayer.soulmancerLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100) + "% Melee Damage\n" +
                                                                      "+" + level * (decimal)stat2 + "% Endurance\n" +
                                                                      "+" + level * (decimal)(stat3 * 100) + "% Cooldown Reduction");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)(badStat * 100) + "% Melee Attack Speed");

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

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.hasSoulmancer = true;
            acmPlayer.equippedClass = "Paladin";
            acmPlayer.ultChargeMax = 600;
            acmPlayer.ability1MaxCooldown = 27;
            acmPlayer.ability2MaxCooldown = 12;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Melee Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; // Endurance
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Cooldown Reduction
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult; // Melee Attack Speed

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Melee) += acmPlayer.paladinLevel * stat1;
                    acmPlayer.trueEndurance += stat2 * acmPlayer.paladinLevel;
                    acmPlayer.cooldownReduction -= stat3 * acmPlayer.paladinLevel;
                    Player.GetAttackSpeed(DamageClass.Melee) -= acmPlayer.paladinLevel * badStat;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Melee) += acmPlayer.paladinLevel * stat1;
                acmPlayer.trueEndurance += stat2 * acmPlayer.paladinLevel;
                acmPlayer.cooldownReduction -= stat3 * acmPlayer.paladinLevel;
                Player.GetAttackSpeed(DamageClass.Melee) -= acmPlayer.paladinLevel * badStat;
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

*/