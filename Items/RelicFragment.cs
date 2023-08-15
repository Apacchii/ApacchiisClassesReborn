using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class RelicFragment : ModItem
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
			Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = 1;
		}
    }
}

