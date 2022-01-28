using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class BlueCloth : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Cloth");
            Tooltip.SetDefault("Used to craft 'Magic Damage' classes");
        }

		public override void SetDefaults()
		{
            Item.maxStack = 1;
			Item.width = 46;
			Item.height = 38;
			Item.value = Item.buyPrice(0, 50, 0, 0);
			Item.rare = ItemRarityID.Blue;
		}

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<WhiteCloth>());
            recipe.Register();
        }
    }
}

