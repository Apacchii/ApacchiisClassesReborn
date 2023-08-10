using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class AghanimsShard : ModItem
	{
        public string desc = "Permanently grants the effects of the Aghanim's Scepter relic";

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault(desc);
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Quest;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.maxStack = 1;

            //Item.GetGlobalItem<ACMGlobalItem>().isRelic = true;
            //Item.GetGlobalItem<ACMGlobalItem>().isCraftableRelic = true;
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc;
        }

        public override bool? UseItem(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasAghanimsShard = true;
            Item.stack--;
            return base.UseItem(player);
        }

        public override bool ConsumeItem(Player player)
        {

            return base.ConsumeItem(player);
        }

        public override void AddRecipes()
        {
            
        }
    }
}

