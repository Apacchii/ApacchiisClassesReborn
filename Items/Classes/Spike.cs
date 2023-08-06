using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Spike : ModItem
	{
        int classLevel;

        float baseStat1 = .005f;
        float stat1; // All Damage

        float baseStat2 = .004f;
        float stat2; // Health

        float baseStat3 = .006f;
        float stat3; // Defense

        public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Pink;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            //var recipe = CreateRecipe(1);
            //recipe.AddIngredient(ModContent.ItemType<OrangeCloth>());
            //recipe.Register();
        }

        //public override void OnCreate(ItemCreationContext context)
        //{
        //    if (_ACMConfigServer.Instance.classWeaponsEnabled)
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.SeekingSpirit>(), 1);
        //        //Main.player[Main.myPlayer].QuickSpawnItem(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.SeekingSpirit>(), 1);
        //
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();
            classLevel = modPlayer.plagueLevel;

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AllDamage")} p/lvl\n" +
                                                                         "+" + (stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl\n" +
                                                                         "+" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")} p/lvl");

            var level = classLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AllDamage")}\n" +
                                                                  "+" + (level * 100 * stat2 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}\n" +
                                                                  "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);

            if (classLevel == 0)
            {
                tooltips.Add(lineLevel);
                tooltips.Add(lineStatsPreview);
            }
            else
            {
                tooltips.Add(lineLevel);
                tooltips.Add(lineStats);
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

        private void ClassStats()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            player.GetDamage(DamageClass.Generic) += classLevel * stat1 * acmPlayer.classStatMultiplier;
            acmPlayer.lifeMult += stat2 * classLevel * acmPlayer.classStatMultiplier;
            acmPlayer.defenseMult += stat3 * classLevel * acmPlayer.classStatMultiplier;
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.equippedClass = "Plague";
            acmPlayer.ultChargeMax = 2900;
            acmPlayer.ability1MaxCooldown = 27;
            acmPlayer.ability2MaxCooldown = 11;
            classLevel = acmPlayer.plagueLevel;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                    ClassStats();
            }
            else ClassStats();

            acmPlayer.classStatMultiplier = 1f;
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += classLevel * .01f;

            // Class Menu Text
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.P_Name");
            acmPlayer.P_Desc = $"Being directly hit by an enemy releases sharp short range quills all around you, damaging enemies impaled by them.\nBeing hit by a projectile has a chance to release 2 low accuracy quill in their direction.";
            acmPlayer.P_Effect_1 = $"";
            acmPlayer.P_Effect_2 = $"";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A1_Name");
            acmPlayer.A1_Desc = $"Release longer range quills around you, these quills can go through walls";
            acmPlayer.A1_Effect_1 = $"";
            acmPlayer.A1_Effect_2 = $"";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.A2_Name");
            acmPlayer.A2_Desc = $"Protect yourself in a barrier of quills, granting yourself 5 shields.";
            acmPlayer.A2_Effect_1 = $"";
            acmPlayer.A2_Effect_2 = $"";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Plague.Ult_Name");
            acmPlayer.Ult_Desc = $"Become unable to move and to use weapons while shooting rapid-fire quills towards your cursor.";
            acmPlayer.Ult_Effect_1 = $"";
            acmPlayer.Ult_Effect_2 = $"s";
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