using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class SuperZIP : ModItem
	{
        public override string Texture => "ApacchiisClassesMod2/Items/ZIP";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super ZIP (Zeta Interacting Protein)");
            Tooltip.SetDefault("Use to reset class level and talents back to 0\n" +
                               $"[c/c83319:[CANNOT BE REVERTED][c/c83319:]]\n" +
                               "'This isnt cautious use anymore'");
        }

		public override void SetDefaults()
		{
            Item.maxStack = 1;
			Item.width = 30;
			Item.height = 30;
            Item.useAnimation = 120;
            Item.useTime = 120;
            Item.useStyle = ItemUseStyleID.EatFood;
			Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.consumable = true;
		}

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<ZIP>());
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }

        public override bool? UseItem(Player player)
        {
            if(player.whoAmI == Main.myPlayer)
            {
                if (player.whoAmI == Main.myPlayer)
                    Item.stack--;

                if (Main.rand.Next(5) == 1)
                    player.statLife = player.statLifeMax2 / 10;
            }
                


            return base.UseItem(player);
        }

        public override void UseAnimation(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                var modPlayer = player.GetModPlayer<ACMPlayer>();
                if (modPlayer.hasVanguard)
                {
                    modPlayer.vanguardDefeatedBosses.Clear();
                    modPlayer.vanguardTalentPoints = 0;
                    modPlayer.vanguardSpentTalentPoints = 0;
                   
                    modPlayer.vanguardTalent_1 = "N";
                    modPlayer.vanguardTalent_2 = "N";
                    modPlayer.vanguardTalent_3 = "N";
                    modPlayer.vanguardTalent_4 = "N";
                    modPlayer.vanguardTalent_5 = "N";
                    modPlayer.vanguardTalent_6 = "N";
                    modPlayer.vanguardTalent_7 = "N";
                    modPlayer.vanguardTalent_8 = "N";
                    modPlayer.vanguardTalent_9 = "N";
                    modPlayer.vanguardTalent_10 = "N";
                }

                if (modPlayer.hasBloodMage)
                {
                    modPlayer.bloodMageDefeatedBosses.Clear();
                    modPlayer.bloodMageTalentPoints = 0;
                    modPlayer.bloodMageSpentTalentPoints = 0;

                    modPlayer.bloodMageTalent_1 = "N";
                    modPlayer.bloodMageTalent_2 = "N";
                    modPlayer.bloodMageTalent_3 = "N";
                    modPlayer.bloodMageTalent_4 = "N";
                    modPlayer.bloodMageTalent_5 = "N";
                    modPlayer.bloodMageTalent_6 = "N";
                    modPlayer.bloodMageTalent_7 = "N";
                    modPlayer.bloodMageTalent_8 = "N";
                    modPlayer.bloodMageTalent_9 = "N";
                    modPlayer.bloodMageTalent_10 = "N";
                }

                if (modPlayer.hasCommander)
                {
                    modPlayer.commanderDefeatedBosses.Clear();
                    modPlayer.commanderTalentPoints = 0;
                    modPlayer.commanderSpentTalentPoints = 0;

                    modPlayer.commanderTalent_1 = "N";
                    modPlayer.commanderTalent_2 = "N";
                    modPlayer.commanderTalent_3 = "N";
                    modPlayer.commanderTalent_4 = "N";
                    modPlayer.commanderTalent_5 = "N";
                    modPlayer.commanderTalent_6 = "N";
                    modPlayer.commanderTalent_7 = "N";
                    modPlayer.commanderTalent_8 = "N";
                    modPlayer.commanderTalent_9 = "N";
                    modPlayer.commanderTalent_10 = "N";
                }

                if (modPlayer.hasScout)
                {
                    modPlayer.scoutDefeatedBosses.Clear();
                    modPlayer.scoutTalentPoints = 0;
                    modPlayer.scoutSpentTalentPoints = 0;

                    modPlayer.scoutTalent_1 = "N";
                    modPlayer.scoutTalent_2 = "N";
                    modPlayer.scoutTalent_3 = "N";
                    modPlayer.scoutTalent_4 = "N";
                    modPlayer.scoutTalent_5 = "N";
                    modPlayer.scoutTalent_6 = "N";
                    modPlayer.scoutTalent_7 = "N";
                    modPlayer.scoutTalent_8 = "N";
                    modPlayer.scoutTalent_9 = "N";
                    modPlayer.scoutTalent_10 = "N";
                }

                SoundEngine.PlaySound(SoundID.Item3, player.position);
                Main.NewText("Super ZIP: '" + player.GetModPlayer<ACMPlayer>().equippedClass + "' class has been entirely reset");

                //if (player.itemAnimation == 2)
                //{
                //    
                //}  
            }
            base.UseAnimation(player);
        }
    }
}

