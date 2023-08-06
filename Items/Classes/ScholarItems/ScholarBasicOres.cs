using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Classes.ScholarItems
{
    public class ScholarBasicOres : ModItem
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
            //recipe.AddIngredient(ModContent.ItemType<ScholarJournal>());
            //recipe.AddRecipeGroup("ACM2:anyCopperOre", 10);
            //recipe.AddRecipeGroup("ACM2:anyIronOre", 10);
            //recipe.AddRecipeGroup("ACM2:anySilverOre", 10);
            //recipe.AddRecipeGroup("ACM2:anyGoldOre", 10);
            //recipe.AddTile(TileID.Bookcases);
            //recipe.Register();
        }


        public override void OnCreated(ItemCreationContext context)
        {
            Main.player[Main.myPlayer].QuickSpawnItem(null, ItemID.WoodenArrow, 200);
            base.OnCreated(context);
        }
    }
}

