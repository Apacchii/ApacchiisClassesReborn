using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Classes.ScholarItems
{
    public class ScholarJournal : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 0, 0, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            //var recipe = CreateRecipe(1);
            //recipe.AddIngredient(ItemID.Book);
            //recipe.AddTile(TileID.Bookcases);
            //recipe.Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();
            TooltipLine lineBasicOres = new TooltipLine(Mod, "Scholar:BasicOres", "");
            TooltipLine lineSlimes = new TooltipLine(Mod, "Scholar:Slimes", "");
            TooltipLine lineZombies = new TooltipLine(Mod, "Scholar:Zombies", "Zombies Research:");

            if (modPlayer.equippedClass == "Scholar")
            {
                // Basic Ores
                if (modPlayer.scholarStudiedBasicOres)
                {
                    lineBasicOres = new TooltipLine(Mod, "Scholar:BasicOres", "Basic Ores Research:\n" +
                                                                              "[P] Gives you 5% damage reduction.\n" +
                                                                              "[A1] Gives you damage reduction for a duration.");
                    lineBasicOres.OverrideColor = Color.Green;
                }
                else
                {
                    lineBasicOres = new TooltipLine(Mod, "Scholar:BasicOres", "Basic Ores Research: Reseach 5 of each basic pre-hardmode ore.");
                    lineBasicOres.OverrideColor = Color.Red;
                }
                //tooltips.Add(lineBasicOres);

                // Slimes
                if (NPC.killCount[NPCID.BlueSlime] >= 50 )
                {
                    lineSlimes = new TooltipLine(Mod, "Scholar:Slimes", "Slimes Research: [P] Gives you fall damage immunity and faster falling speed.");
                    lineSlimes.OverrideColor = Color.Green;
                }
                else
                {
                    lineSlimes = new TooltipLine(Mod, "Scholar:Slimes", "Slimes Research: Defeat 50 slimes to unlock.");
                    lineSlimes.OverrideColor = Color.Red;
                }
                //tooltips.Add(lineSlimes);

                // Zombies
                if (NPC.killCount[NPCID.Zombie] >= 50)
                {
                    lineZombies = new TooltipLine(Mod, "Scholar:Zombies", "Zombies Research: [A2] .");
                    lineZombies.OverrideColor = Color.Green;
                }
                else
                {
                    lineZombies = new TooltipLine(Mod, "Scholar:Zombies", "Zombies Research: Defeat 50 Zombies to unlock.");
                    lineZombies.OverrideColor = Color.Red;
                }
                //tooltips.Add(lineZombies);
            }
            else
            {
                TooltipLine lineScholarOnly = new TooltipLine(Mod, "Scholar:ScholarItem", "Used only by the Scholar class to research stuff and unlock abilities.");
                tooltips.Add(lineScholarOnly);
            }

            base.ModifyTooltips(tooltips);
        }
    }
}

