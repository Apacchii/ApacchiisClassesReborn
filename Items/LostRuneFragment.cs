using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class LostRuneFragment : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lost Rune Fragment");
            // Tooltip.SetDefault("");
        }

		public override void SetDefaults()
		{
            Item.maxStack = 999;
			Item.width = 30;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 0, 5, 0);
            //Item.useStyle = 1;
            //Item.useAnimation = 5;
            //Item.useTime = 5;
            //Item.autoReuse = true;
            Item.rare = 1;
            //Item.createTile = ModContent.TileType<Tiles.Ores.LostRuneFragment>();
            //Item.consumable = true;
		}
    }
}

