using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class PinkCloth : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pink Cloth");
            // Tooltip.SetDefault("Used to craft '???' classes");
        }

		public override void SetDefaults()
		{
            Item.maxStack = 1;
			Item.width = 46;
			Item.height = 38;
			Item.value = Item.buyPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Pink;
		}

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void AddRecipes()
        {
            //var recipe = CreateRecipe(1);
            //recipe.AddIngredient(ModContent.ItemType<WhiteCloth>());
            //recipe.Register();
        }
    }
}

