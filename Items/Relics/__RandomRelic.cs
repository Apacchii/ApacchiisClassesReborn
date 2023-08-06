using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
 
namespace ApacchiisClassesMod2.Items.Relics
{
    public class RandomRelic : ModItem
    {
        public override void SetStaticDefaults(){
			// Tooltip.SetDefault($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.GivesRelic")}\nRight click to open");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 0;
        }

        public override void SetDefaults()
        {
            //DisplayName.SetDefault($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.RandomRelic")}");

            Item.width = 26;
            Item.height = 20;
            Item.rare = 11;
            Item.maxStack = 999;
            Item.value = 0;
        }

        //public override void AddRecipes()
        //{
        //    var recipe = CreateRecipe(1);
        //    recipe.AddIngredient(ModContent.ItemType<LostRelic>(), 1);
        //    recipe.AddTile(TileID.Anvils);
        //    recipe.Register();
        //}

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            int relicCount = player.GetModPlayer<ACMPlayer>().relicList.Count; //22 | (040222:1730)
            int choice = Main.rand.Next(relicCount);

            //if (Item.type == player.GetModPlayer<ACMPlayer>().relicList[choice].

            //List of item that CANNOT be aquired via unboxing
            for(int i = 0; i < 100; i++)
            {
                if (player.GetModPlayer<ACMPlayer>().relicList[choice] == ModContent.ItemType<NiterihsJewelryBox>())
                {
                    choice = Main.rand.Next(relicCount);
                }
            }
            

            player.QuickSpawnItem(player.GetSource_OpenItem(Type), player.GetModPlayer<ACMPlayer>().relicList[choice]);

            //Play custom sound if we get a Nessie
            if(player.GetModPlayer<ACMPlayer>().relicList[choice] == ModContent.ItemType<Nessie>())
            {
                SoundStyle watty1 = new SoundStyle($"{nameof(ApacchiisClassesMod2)}/Sounds/SoundEffects/Watty1");
                SoundStyle watty2 = new SoundStyle($"{nameof(ApacchiisClassesMod2)}/Sounds/SoundEffects/Watty2");

                int rs = Main.rand.Next(2);
                if (rs == 0)
                    SoundEngine.PlaySound(watty1 with
                    {
                        Volume = 1f,
                    }, player.Center);
                if (rs == 1)
                    SoundEngine.PlaySound(watty2 with
                    {
                        Volume = 1f,
                    }, player.Center);
            }

            base.RightClick(player);
        }
    }
}
