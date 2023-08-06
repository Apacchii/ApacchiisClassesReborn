//using System;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria.Audio;
//
//namespace ApacchiisClassesMod2.Projectiles
//{
//    public class Deadzone : ModProjectile
//    {
//        bool flag = false;
//        int firerate;
//        float range;
//
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Deadzone");
//            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
//            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
//        }
//
//        public override void SetDefaults()
//        {
//            Projectile.width = 32;
//            Projectile.height = 32;
//            Projectile.aiStyle = 0;
//            Projectile.friendly = true;
//            Projectile.hostile = false;
//            Projectile.penetrate = -1;
//            Projectile.timeLeft = 60;
//            Projectile.alpha = 0;
//            Projectile.ignoreWater = false;
//            Projectile.tileCollide = false;
//        }
//
//        public override void AI()
//        {
//            Player player = Main.player[Main.myPlayer];
//            Projectile.timeLeft = 60;
//
//            if(!flag)
//            {
//                firerate = 10;
//                range = 600;
//                flag = true;
//            }
//
//            firerate--;
//
//            // Circle Dust
//            Vector2 origin = Projectile.Center;
//            origin.X -= Projectile.width / 2;
//
//            int locations = 100;
//            for (int i = 0; i < locations; i++)
//            {
//                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * range;
//                var dust = Dust.NewDustPerfect(position, 2, Vector2.Zero, 0, Color.Orange, 1.25f);
//                dust.noGravity = true;
//            }
//
//            if(firerate <= 0)
//            {
//                for (int i = 0; i < Main.maxNPCs; i++)
//                {
//                    if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) <= range && Collision.CanHitLine(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1) && !Main.npc[i].friendly && Main.npc[i].type != NPCID.TargetDummy && !Main.npc[i].townNPC && !Main.npc[i].CountsAsACritter && !Main.npc[i].dontTakeDamage && Main.npc[i].type != NPCID.DD2Bartender && Main.npc[i].type != NPCID.DD2EterniaCrystal && Main.npc[i].type != NPCID.DD2LanePortal)
//                    {
//                        player.ApplyDamageToNPC(Main.npc[i], 5, 0, 0, false);
//                        firerate = 60;
//                    }
//                }
//            }
//            
//        }
//
//        public override bool? CanHitNPC(NPC target) => false;
//    }
//}