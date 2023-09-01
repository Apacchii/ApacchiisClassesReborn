using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class LostRune : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lost Rune");
            // Tooltip.SetDefault("Consuming grants you 1 rune draw\n'An ancient rune, yet to engrave'");
        }

		public override void SetDefaults()
		{
            Item.maxStack = 999;
			Item.width = 30;
			Item.height = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 1;
            Item.consumable = true;
		}

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<LostRuneFragment>(), 4);
            recipe.Register();
            base.AddRecipes();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.GetModPlayer<ACMPlayer>().cardsPoints++;
                Main.NewText($"+1 Rune draw. You now have {player.GetModPlayer<ACMPlayer>().cardsPoints} draws");
                //Item.stack--;
                //if (Item.stack == 0)
                //    Item.TurnToAir();
            }
            base.RightClick(player);
        }
    }
}

