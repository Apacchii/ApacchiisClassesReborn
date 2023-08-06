using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class WhiteCloth : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("White Cloth");
            // Tooltip.SetDefault("Used to craft your class damage type of choice");
        }

		public override void SetDefaults()
		{
            Item.maxStack = 1;
			Item.width = 46;
			Item.height = 38;
			Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.White;
		}

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddTile(TileID.Loom);
            recipe.Register();

            var recipe2 = CreateRecipe(1);
            recipe2.AddIngredient(ModContent.ItemType<RedCloth>());
            recipe2.Register();

            var recipe3 = CreateRecipe(1);
            recipe3.AddIngredient(ModContent.ItemType<GreenCloth>());
            recipe3.Register();

            var recipe4 = CreateRecipe(1);
            recipe4.AddIngredient(ModContent.ItemType<BlueCloth>());
            recipe4.Register();

            var recipe5 = CreateRecipe(1);
            recipe5.AddIngredient(ModContent.ItemType<OrangeCloth>());
            recipe5.Register();
        }
    }
}

