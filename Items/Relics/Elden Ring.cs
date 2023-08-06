//using Microsoft.Xna.Framework;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//
//namespace ApacchiisClassesMod2.Items.Relics
//{
//	public class EldenRing : ModItem
//	{
//        public string desc = "Reduces immunity time after being hit by 15 framesn\n" +
//                             "Increases damage dealt by 8%";
//        string donator = "Dr.Oktober";
//
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("[Relic] The Elden Ring");
//            Tooltip.SetDefault(desc + $"\n[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, {donator}!][c/e796e8:]]");
//        }
//
//
//		public override void SetDefaults()
//		{
//			Item.width = 30;
//			Item.height = 30;
//			Item.accessory = true;	
//			Item.value = Item.sellPrice(0, 5, 0, 0);
//            Item.rare = ItemRarityID.Quest;
//
//            Item.GetGlobalItem<ACMGlobalItem>().isRelic = true;
//            Item.GetGlobalItem<ACMGlobalItem>().desc = desc + $"\n[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, {donator}!][c/e796e8:]]";
//        }
//
//        public override void UpdateVanity(Player player)
//        {
//            var acmPlayer = player.GetModPlayer<ACMPlayer>();
//            acmPlayer.hasRelic = true;
//            acmPlayer.hasEldenRing = true;
//            player.GetDamage(DamageClass.Generic) += .08f;
//
//            base.UpdateVanity(player);
//        }
//
//        public override bool CanEquipAccessory(Player player, int slot, bool modded)
//        {
//            
//                return false;
//
//            if (!modded)
//                return false;
//
//            return base.CanEquipAccessory(player, slot, modded);
//        }
//    }
//}