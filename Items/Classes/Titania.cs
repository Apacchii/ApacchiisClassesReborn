using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
    public class Titania : ModItem
    {
        int classLevel; //Obsolete

        float baseStat1 = .005f;
        float stat1; // Selected Type Damage

        float baseStat2 = .004f;
        float stat2; // Health

        float baseStat3 = .0035f;
        float stat3; // Dodge

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

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

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% Generic Damage p/lvl\n" +
                                                                         "+" + (stat2 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl\n" +
                                                                         "+" + (stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")} p/lvl");

            var level = modPlayer.globalClassLevel.Count;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (level * stat1 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% Generic Damage\n" +
                                                                  "+" + (level * 100 * stat2 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}\n" +
                                                                  "+" + (level * stat3 * 100 * modPlayer.classStatMultiplier).ToString("F2") + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);

            if (level == 0)
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

        public override void UpdateAccessory(Player Player, bool hideVisual)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.equippedClass = "Titania";
            acmPlayer.ultChargeMax = 2400;
            acmPlayer.ability1MaxCooldown = 24;
            //acmPlayer.ability1MaxCharges = 6;
            acmPlayer.ability2MaxCooldown = 16;
            int currentClassLevel = acmPlayer.globalClassLevel.Count;

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
            if (_ACMConfigServer.Instance.calamityScaling && Main.hardMode) acmPlayer.classStatMultiplier += currentClassLevel * .01f;

            // Class Menu Text [x = y + z p/lvl]
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Titania.P_Name");
            acmPlayer.P_Desc = $"";
            acmPlayer.P_Effect_1 = $"";
            acmPlayer.P_Effect_2 = $"";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Titania.A1_Name");
            acmPlayer.A1_Desc = $"";
            acmPlayer.A1_Effect_1 = $"";
            acmPlayer.A1_Effect_2 = $"";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Titania.A2_Name");
            acmPlayer.A2_Desc = $"";
            acmPlayer.A2_Effect_1 = $"";
            acmPlayer.A2_Effect_2 = $"";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Titania.Ult_Name");
            acmPlayer.Ult_Desc = $"";
            acmPlayer.Ult_Effect_1 = $"";
            acmPlayer.Ult_Effect_2 = $"";

            acmPlayer.aghanimsText = "- ";
        }

        private void ClassStats()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            player.GetDamage(DamageClass.Generic) += stat1 * classLevel * acmPlayer.classStatMultiplier;
            acmPlayer.lifeMult += stat2 * classLevel * acmPlayer.classStatMultiplier;
            acmPlayer.dodgeChance += stat3 * classLevel * acmPlayer.classStatMultiplier;

            //[Scrap]
            //if (acmPlayer.titaniaSelectedDamageClass  > acmPlayer.titaniaAllowedClassesList.Count)
            //    acmPlayer.titaniaSelectedDamageClass = 0;
            //
            //player.GetDamage(DamageClassLoader.GetDamageClass(acmPlayer.titaniaAllowedClassesList[0])) += 50 + classLevel * stat1 * acmPlayer.classStatMultiplier;
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