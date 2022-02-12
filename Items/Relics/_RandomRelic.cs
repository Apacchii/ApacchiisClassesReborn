using Terraria;
using Terraria.ModLoader;
 
namespace ApacchiisClassesMod2.Items.Relics
{
	
    public class RandomRelic : ModItem
    {
        public override void SetStaticDefaults(){
			DisplayName.SetDefault("Random Relic");
			Tooltip.SetDefault("Gives you a random relic\n{$CommonItemTooltip.RightClickToOpen}");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 0;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = 11;
            Item.maxStack = 99;
            Item.value = 0;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            int relicCount = player.GetModPlayer<ACMPlayer>().relicList.Count; //22 | (040222:1730)
            int choice = Main.rand.Next(relicCount);

            player.QuickSpawnItem(player.GetModPlayer<ACMPlayer>().relicList[choice]);

            base.RightClick(player);
        }
    }
}
