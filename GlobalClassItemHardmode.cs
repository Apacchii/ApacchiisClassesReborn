//using Microsoft.Xna.Framework;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using static Terraria.ModLoader.ModContent;
//
//namespace ApacchiisClassesMod2
//{
//	public class GlobalClassItemHardmode : GlobalItem
//	{
//        
//        public override bool InstancePerEntity
//        {
//            get
//            {
//                return true;
//            }
//        }
//
//        void UpdateItemStats(Item item, StatModifier damage)
//        {
//            if (Main.hardMode)
//            {
//                if (item.type == ItemType<Items.ClassWeapons.BloodSpray>())
//                {
//                    damage += 27;
//                    item.useTime -= 30;
//                    item.useAnimation -= 30;
//                    item.shootSpeed += 2;
//                }
//        
//                if (item.type == ItemType<Items.ClassWeapons.TraineesSword>())
//                {
//                    damage += 32;
//                    item.useTime -= 2;
//                    item.useAnimation -= 2;
//                }
//            }
//        }
//
//        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
//      {
//          if (item.type == ItemType<Items.ClassWeapons.TraineesSword>())
//              foreach (TooltipLine line in tooltips)
//                  if (line.Name == "Tooltip0")
//                      if(Main.hardMode)
//                      {
//                          line.text = "While holding this weapon you reduce any damage taken from enemies by 20";
//                      }
//                      else
//                      {
//                          line.text = "While holding this weapon you reduce any damage taken from enemies by 6";
//                      }
//                          
//
//          base.ModifyTooltips(item, tooltips);
//      }
//
//        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage, ref float flat)
//        {
//            if (item.type == ItemType<Items.ClassWeapons.BloodSpray>())
//            {
//                flat += 27;
//                item.useTime = 30;
//                item.useAnimation = 30;
//                item.shootSpeed = 16;
//            }
//        
//            if (item.type == ItemType<Items.ClassWeapons.TraineesSword>())
//            {
//                flat += 32;
//                item.useTime = 20;
//                item.useAnimation = 20;
//            }
//        
//            // Code made for TheLoneGamer for scaling a ranged weapon's damage without taking bullet damage into account
//            //int itemNumber = 500;
//            //
//            //if(item.type == ItemID.SDMG && player.GetModPlayer<ACMPlayer>().equippedClass != "")
//            //{
//            //    for (int i = 0; i < player.inventory.Length; i++)
//            //    {
//            //        if (player.inventory[i].ammo == AmmoID.Bullet)
//            //        {
//            //            Item itemIndex = player.inventory[i];
//            //
//            //            if (i < itemNumber)
//            //            {
//            //                itemNumber = i;
//            //                itemIndex = player.inventory[itemNumber];
//            //
//            //                Main.NewText($"Ammo Detected: {itemIndex.Name}");
//            //                Main.NewText($"Ammo Damage: {itemIndex.damage}");
//            //
//            //                damage -= itemIndex.damage;
//            //                damage *= 2;
//            //                damage += itemIndex.damage;
//            //            }
//            //        }
//            //    }
//            //}
//        
//            base.ModifyWeaponDamage(item, player, ref damage, ref flat);
//        }
//    }
//}