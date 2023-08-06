using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ApacchiisClassesMod2
{
    public class ACM2RecipeGroups : ModSystem
    {
        public static RecipeGroup anyCopperOre;
        public static RecipeGroup anyIronOre;
        public static RecipeGroup anySilverOre;
        public static RecipeGroup anyGoldOre;

        public override void AddRecipeGroups()
        {
            //Copper
            anyCopperOre = new RecipeGroup(() => $"Copper/Tin Ore",
                ItemID.CopperOre,
                ItemID.TinOre
                );
            RecipeGroup.RegisterGroup("ACM2:anyCopperOre", anyCopperOre);

            //Iron
            anyIronOre = new RecipeGroup(() => $"Iron/Lead Ore",
                ItemID.IronOre,
                ItemID.LeadOre
                );
            RecipeGroup.RegisterGroup("ACM2:anyIronOre", anyIronOre);

            //Silver
            anySilverOre = new RecipeGroup(() => $"Silver/Tungsten Ore",
                ItemID.SilverOre,
                ItemID.TungstenOre
                );
            RecipeGroup.RegisterGroup("ACM2:anySilverOre", anySilverOre);

            //Gold
            anyGoldOre = new RecipeGroup(() => $"Gold/Platinum Ore",
               ItemID.GoldOre,
               ItemID.PlatinumOre
               );
            RecipeGroup.RegisterGroup("ACM2:anyGoldOre", anyGoldOre);
        }
    }
}