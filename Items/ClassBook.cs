using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class ClassBook : ModItem
	{
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("[Can also be used to open the Classes Menu]\nDefeated bosses:\n");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.LightRed;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.Silk);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();
            TooltipLine lineKills = new TooltipLine(Mod, "Kills", "Something went wrong! Please report on discord!");
            TooltipLine classLine = new TooltipLine(Mod, "Class", "Class: " + modPlayer.equippedClass);
            TooltipLine level = new TooltipLine(Mod, "Level", " [" + modPlayer.equippedClass + "]");

            if (modPlayer.equippedClass == "")
            {
                lineKills = new TooltipLine(Mod, "Kills", "Something went wrong! No class is currently equipped or currently equipped class is not recognized!");
                level = new TooltipLine(Mod, "Level", "");
            }
                
            if (modPlayer.hasVanguard)
            {
                modPlayer.vanguardDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.vanguardDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level  [" + modPlayer.vanguardDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }
                
            if (modPlayer.hasBloodMage)
            {
                modPlayer.bloodMageDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.bloodMageDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level  [" + modPlayer.bloodMageDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }

            if (modPlayer.hasCommander)
            {
                modPlayer.commanderDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.commanderDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level [" + modPlayer.commanderDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }

            if (modPlayer.hasScout)
            {
                modPlayer.scoutDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.scoutDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level [" + modPlayer.scoutDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }

            if (modPlayer.hasSoulmancer)
            {
                modPlayer.soulmancerDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.soulmancerDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level [" + modPlayer.soulmancerDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }

            if (modPlayer.hasCrusader)
            {
                modPlayer.crusaderDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.crusaderDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level [" + modPlayer.crusaderDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }

            if (modPlayer.equippedClass == "Gambler")
            {
                modPlayer.gamblerDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.gamblerDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level [" + modPlayer.gamblerDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }

            if (modPlayer.equippedClass == "Plague")
            {
                modPlayer.gamblerDefeatedBosses.Sort();
                lineKills = new TooltipLine(Mod, "Kills", string.Join("\n", modPlayer.plagueDefeatedBosses));
                level = new TooltipLine(Mod, "Level", "Level [" + modPlayer.plagueDefeatedBosses.Count + "/" + Configs._ACMConfigServer.Instance.maxClassLevel + "]");
            }

            lineKills.OverrideColor = new Color(230, 80, 80);
            classLine.OverrideColor = Color.Aqua;
            level.OverrideColor = Color.Orange;
            tooltips.Add(classLine);
            tooltips.Add(level);
            tooltips.Add(lineKills);
            

            base.ModifyTooltips(tooltips);
        }

        public override bool? UseItem(Player player)
        {

            return base.UseItem(player);
        }

        public override void UseAnimation(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                if (ModContent.GetInstance<ACM2ModSystem>()._ClassesMenu.CurrentState == null)
                {
                    ModContent.GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(new UI.ClassesMenu());
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                }
                else
                {
                    ModContent.GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
                    SoundEngine.PlaySound(SoundID.MenuClose);
                }
            }
            base.UseAnimation(player);
        }

        //public override void UseAnimation(Player player)
        //{
        //    if(player.whoAmI == Main.myPlayer)
        //    {
        //        var modPlayer = player.GetModPlayer<ACMPlayer>();
        //
        //        if (modPlayer.hasVanguard)
        //            modPlayer.vanguardDefeatedBosses.Clear();
        //
        //        if (modPlayer.hasBloodMage)
        //            modPlayer.bloodMageDefeatedBosses.Clear();
        //
        //        if (modPlayer.hasCommander)
        //            modPlayer.commanderDefeatedBosses.Clear();
        //
        //        if (modPlayer.hasScout)
        //            modPlayer.scoutDefeatedBosses.Clear();
        //
        //        if (player.itemAnimation == 14)
        //            Main.NewText("DevTool: Currently equipped class levesls have been reset");
        //    }
        //    base.UseAnimation(player);
        //}
    }
}

