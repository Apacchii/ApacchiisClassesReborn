//using Terraria;
//using Terraria.Audio;
//using Terraria.ID;
//using Terraria.ModLoader;
//
//namespace ApacchiisClassesMod2.Items
//{
//	public class ZIP : ModItem
//	{
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("ZIP (Zeta Interacting Protein)");
//            Tooltip.SetDefault("Use to re-learn class talents\n" +
//                               "'Cautious use can erase selective memories'");
//        }
//
//		public override void SetDefaults()
//		{
//            Item.maxStack = 1;
//			Item.width = 30;
//			Item.height = 30;
//            Item.useAnimation = 120;
//            Item.useTime = 120;
//            Item.useStyle = ItemUseStyleID.EatFood;
//			Item.value = Item.buyPrice(0, 75, 0, 0);
//            Item.rare = ItemRarityID.Red;
//            Item.consumable = true;
//		}
//
//        //public override void AddRecipes()
//        //{
//        //    var recipe = CreateRecipe(1);
//        //    recipe.AddIngredient(ItemID.SoulofFright, 2);
//        //    recipe.AddIngredient(ItemID.SoulofMight, 2);
//        //    recipe.AddIngredient(ItemID.SoulofSight, 2);
//        //    recipe.AddIngredient(ItemID.TempleKey);
//        //    recipe.AddIngredient(ItemID.Emerald);
//        //    recipe.AddIngredient(ItemID.Obsidian, 5);
//        //    recipe.AddIngredient(ItemID.Bone, 2);
//        //    recipe.AddIngredient(ItemID.GlowingMushroom, 2);
//        //    recipe.AddIngredient(ItemID.Mushroom);
//        //    recipe.AddIngredient(ItemID.Fireblossom);
//        //    recipe.AddTile(TileID.AlchemyTable);
//        //    recipe.Register();
//        //}
//
//        public override bool? UseItem(Player player)
//        {
//            if (player.whoAmI == Main.myPlayer)
//                if (player.whoAmI == Main.myPlayer)
//                    Item.stack--;
//
//            return base.UseItem(player);
//        }
//
//        public override void UseAnimation(Player player)
//        {
//            if (player.whoAmI == Main.myPlayer)
//            {
//                var modPlayer = player.GetModPlayer<ACMPlayer>();
//                if (modPlayer.hasVanguard)
//                {
//                    if (modPlayer.vanguardTalent_1 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_2 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_3 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_4 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_5 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_6 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_7 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_8 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_9 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//                    if (modPlayer.vanguardTalent_10 != "N")
//                    {
//                        modPlayer.vanguardSkillPoints++;
//                        modPlayer.vanguardSpentSkillPoints--;
//                    }
//
//                    modPlayer.vanguardTalent_1 = "N";
//                    modPlayer.vanguardTalent_2 = "N";
//                    modPlayer.vanguardTalent_3 = "N";
//                    modPlayer.vanguardTalent_4 = "N";
//                    modPlayer.vanguardTalent_5 = "N";
//                    modPlayer.vanguardTalent_6 = "N";
//                    modPlayer.vanguardTalent_7 = "N";
//                    modPlayer.vanguardTalent_8 = "N";
//                    modPlayer.vanguardTalent_9 = "N";
//                    modPlayer.vanguardTalent_10 = "N";
//                }
//
//                if (modPlayer.hasBloodMage)
//               {
//                   if (modPlayer.bloodMageTalent_1 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_2 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_3 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_4 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_5 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_6 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_7 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_8 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_9 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//                   if (modPlayer.bloodMageTalent_10 != "N")
//                   {
//                       modPlayer.bloodMageSkillPoints++;
//                       modPlayer.bloodMageSpentSkillPoints--;
//                   }
//
//                   modPlayer.bloodMageTalent_1 = "N";
//                   modPlayer.bloodMageTalent_2 = "N";
//                   modPlayer.bloodMageTalent_3 = "N";
//                   modPlayer.bloodMageTalent_4 = "N";
//                   modPlayer.bloodMageTalent_5 = "N";
//                   modPlayer.bloodMageTalent_6 = "N";
//                   modPlayer.bloodMageTalent_7 = "N";
//                   modPlayer.bloodMageTalent_8 = "N";
//                   modPlayer.bloodMageTalent_9 = "N";
//                   modPlayer.bloodMageTalent_10 = "N";
//               }
//
//                if (modPlayer.hasCommander)
//                {
//                    if (modPlayer.commanderTalent_1 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_2 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_3 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_4 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_5 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_6 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_7 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_8 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_9 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//                    if (modPlayer.commanderTalent_10 != "N")
//                    {
//                        modPlayer.commanderSkillPoints++;
//                        modPlayer.commanderSpentSkillPoints--;
//                    }
//
//                    modPlayer.commanderTalent_1 = "N";
//                    modPlayer.commanderTalent_2 = "N";
//                    modPlayer.commanderTalent_3 = "N";
//                    modPlayer.commanderTalent_4 = "N";
//                    modPlayer.commanderTalent_5 = "N";
//                    modPlayer.commanderTalent_6 = "N";
//                    modPlayer.commanderTalent_7 = "N";
//                    modPlayer.commanderTalent_8 = "N";
//                    modPlayer.commanderTalent_9 = "N";
//                    modPlayer.commanderTalent_10 = "N";
//                }
//
//                if (modPlayer.hasScout)
//                {
//                    if (modPlayer.scoutTalent_1 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_2 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_3 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_4 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_5 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_6 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_7 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_8 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_9 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//                    if (modPlayer.scoutTalent_10 != "N")
//                    {
//                        modPlayer.scoutSkillPoints++;
//                        modPlayer.scoutSpentSkillPoints--;
//                    }
//
//                    modPlayer.scoutTalent_1 = "N";
//                    modPlayer.scoutTalent_2 = "N";
//                    modPlayer.scoutTalent_3 = "N";
//                    modPlayer.scoutTalent_4 = "N";
//                    modPlayer.scoutTalent_5 = "N";
//                    modPlayer.scoutTalent_6 = "N";
//                    modPlayer.scoutTalent_7 = "N";
//                    modPlayer.scoutTalent_8 = "N";
//                    modPlayer.scoutTalent_9 = "N";
//                    modPlayer.scoutTalent_10 = "N";
//                }
//
//                if (modPlayer.hasSoulmancer)
//                {
//                    if (modPlayer.soulmancerTalent_1 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_2 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_3 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_4 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_5 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_6 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_7 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_8 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_9 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//                    if (modPlayer.soulmancerTalent_10 != "N")
//                    {
//                        modPlayer.soulmancerSkillPoints++;
//                        modPlayer.soulmancerSpentSkillPoints--;
//                    }
//
//                    modPlayer.soulmancerTalent_1 = "N";
//                    modPlayer.soulmancerTalent_2 = "N";
//                    modPlayer.soulmancerTalent_3 = "N";
//                    modPlayer.soulmancerTalent_4 = "N";
//                    modPlayer.soulmancerTalent_5 = "N";
//                    modPlayer.soulmancerTalent_6 = "N";
//                    modPlayer.soulmancerTalent_7 = "N";
//                    modPlayer.soulmancerTalent_8 = "N";
//                    modPlayer.soulmancerTalent_9 = "N";
//                    modPlayer.soulmancerTalent_10 = "N";
//                }
//
//                SoundEngine.PlaySound(SoundID.Item3, player.position);
//                Main.NewText("ZIP: " + player.GetModPlayer<ACMPlayer>().equippedClass + "'s talents have been reset");
//
//                //if (player.itemAnimation == 2)
//                //{
//                //    
//                //}  
//            }
//            base.UseAnimation(player);
//        }
//    }
//}