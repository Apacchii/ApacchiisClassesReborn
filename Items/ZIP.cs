using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class ZIP : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ZIP (Zeta Interacting Protein)");
            Tooltip.SetDefault("Use to re-learn class talents\n" +
                               "'Cautious use can erase selective memories'");
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
            Item.rare = ItemRarityID.Red;
            Item.consumable = true;
		}

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.SoulofFright, 2);
            recipe.AddIngredient(ItemID.SoulofMight, 2);
            recipe.AddIngredient(ItemID.SoulofSight, 2);
            recipe.AddIngredient(ItemID.TempleKey);
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(ItemID.Obsidian, 5);
            recipe.AddIngredient(ItemID.Bone, 2);
            recipe.AddIngredient(ItemID.GlowingMushroom, 2);
            recipe.AddIngredient(ItemID.Mushroom);
            recipe.AddIngredient(ItemID.Fireblossom);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
                if (player.whoAmI == Main.myPlayer)
                    Item.stack--;

            return base.UseItem(player);
        }

        public override void UseAnimation(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                var modPlayer = player.GetModPlayer<ACMPlayer>();
                if (modPlayer.hasVanguard)
                {
                    modPlayer.vanguardSpentTalentPoints = 0;

                    if (modPlayer.vanguardTalent_1 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_2 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_3 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_4 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_5 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_6 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_7 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_8 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_9 != "N")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_10 != "N")
                        modPlayer.vanguardTalentPoints++;

                    if (modPlayer.vanguardTalent_1 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_2 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_3 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_4 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_5 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_6 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_7 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_8 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_9 == "B")
                        modPlayer.vanguardTalentPoints++;
                    if (modPlayer.vanguardTalent_10 == "B")
                        modPlayer.vanguardTalentPoints++;

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
                    modPlayer.bloodMageSpentTalentPoints = 0;

                    if (modPlayer.bloodMageTalent_1 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_2 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_3 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_4 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_5 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_6 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_7 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_8 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_9 != "N")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_10 != "N")
                        modPlayer.bloodMageTalentPoints++;

                    if (modPlayer.bloodMageTalent_1 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_2 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_3 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_4 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_5 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_6 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_7 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_8 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_9 == "B")
                        modPlayer.bloodMageTalentPoints++;
                    if (modPlayer.bloodMageTalent_10 == "B")
                        modPlayer.bloodMageTalentPoints++;

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
                    modPlayer.commanderSpentTalentPoints = 0;

                    if (modPlayer.commanderTalent_1 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_2 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_3 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_4 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_5 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_6 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_7 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_8 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_9 != "N")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_10 != "N")
                        modPlayer.commanderTalentPoints++;

                    if (modPlayer.commanderTalent_1 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_2 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_3 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_4 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_5 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_6 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_7 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_8 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_9 == "B")
                        modPlayer.commanderTalentPoints++;
                    if (modPlayer.commanderTalent_10 == "B")
                        modPlayer.commanderTalentPoints++;

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
                    modPlayer.scoutSpentTalentPoints = 0;

                    if (modPlayer.scoutTalent_1 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_2 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_3 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_4 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_5 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_6 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_7 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_8 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_9 != "N")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_10 != "N")
                        modPlayer.scoutTalentPoints++;

                    if (modPlayer.scoutTalent_1 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_2 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_3 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_4 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_5 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_6 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_7 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_8 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_9 == "B")
                        modPlayer.scoutTalentPoints++;
                    if (modPlayer.scoutTalent_10 == "B")
                        modPlayer.scoutTalentPoints++;

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
                Main.NewText("ZIP: " + player.GetModPlayer<ACMPlayer>().equippedClass + "'s talents have been reset");

                //if (player.itemAnimation == 2)
                //{
                //    
                //}  
            }
            base.UseAnimation(player);
        }
    }
}

