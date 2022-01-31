using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
 
namespace ApacchiisClassesMod2.Items.Relics
{
	
    public class RandomRelic : ModItem
    {
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults(){
			DisplayName.SetDefault("Random Relic");
			Tooltip.SetDefault("Gives you a random relic\n{$CommonItemTooltip.RightClickToOpen}");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 0;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
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
            int relicCount = 20;
            int choice = Main.rand.Next(relicCount);

            switch (choice)
            {
                case 0:
                    player.QuickSpawnItem(ItemType<CursedCandle>());
                    break;
                case 1:
                    player.QuickSpawnItem(ItemType<VoidMirror>());
                    break;
                case 2:
                    player.QuickSpawnItem(ItemType<BrokenHeart>());
                    break;
                case 3:
                    player.QuickSpawnItem(ItemType<BleedingMoonStone>());
                    break;
                case 4:
                    player.QuickSpawnItem(ItemType<BloodGem>());
                    break;
                case 5:
                    player.QuickSpawnItem(ItemType<AghanimsScepter>());
                    break;
                case 6:
                    player.QuickSpawnItem(ItemType<NiterihsRing>());
                    break;
                case 7:
                    player.QuickSpawnItem(ItemType<NiterihsBracelet>());
                    break;
                case 8:
                    player.QuickSpawnItem(ItemType<NiterihsEarring>());
                    break;
                case 9:
                    player.QuickSpawnItem(ItemType<NiterihsNecklace>());
                    break;
                case 10:
                    player.QuickSpawnItem(ItemType<LeysMushroom>());
                    break;
                case 11:
                    player.QuickSpawnItem(ItemType<MushroomConcentrate>());
                    break;
                case 12:
                    player.QuickSpawnItem(ItemType<StrangeMushroom>());
                    break;
                case 13:
                    player.QuickSpawnItem(ItemType<UnstableConcoction>());
                    break;
                case 14:
                    player.QuickSpawnItem(ItemType<TearsOfLife>());
                    break;
                case 15:
                    player.QuickSpawnItem(ItemType<OldBelt>());
                    break;
                case 16:
                    player.QuickSpawnItem(ItemType<StrangeGem>());
                    break;
                case 17:
                    player.QuickSpawnItem(ItemType<Croissant>());
                    break;
                case 18:
                    player.QuickSpawnItem(ItemType<NiterihsLuckyToken>());
                    break;
                case 19:
                    player.QuickSpawnItem(ItemType<BerserkersBrew>());
                    break;
                default:
                    player.QuickSpawnItem(ItemType<RandomRelic>());
                    break;
            }

            base.RightClick(player);
        }
    }
}
