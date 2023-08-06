using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Commander
{
    public class WarBanner : ModProjectile
    {
        bool flag = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("War Banner");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 82;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            DrawOffsetX = -32;
            DrawOriginOffsetX = -32;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];

            if (!flag)
            {
                Projectile.position.Y -= Projectile.height / 4;
                Projectile.timeLeft = player.GetModPlayer<ACMPlayer>().commanderBannerDuration;
                flag = true;
            }

            // Circle Dust
            Vector2 origin = Projectile.Center;
            origin.X -= Projectile.width / 2;
            float radius = player.GetModPlayer<ACMPlayer>().commanderBannerRange;

            int locations = 30;
            for (int i = 0; i < locations; i++)
            {
                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                var dust = Dust.NewDustPerfect(position, 2, Vector2.Zero, 0, Color.Orange, 1.25f);
                dust.noGravity = true;
            }

            for (int i = 0; i < 255; i++)
            {
                if (Vector2.Distance(origin, Main.player[i].Center) <= radius && Collision.CanHitLine(origin, 1, 1, Main.player[i].Center, 1, 1))
                    Main.player[i].AddBuff(ModContent.BuffType<Buffs.Commander.WarBanner>(), Main.player[i].GetModPlayer<ACMPlayer>().commanderBannerPersist);
            }

            if (player.GetModPlayer<ACMPlayer>().bannerFollowsPlayer)
            {
                Projectile.position = Main.player[Projectile.owner].Center;
                Projectile.position.Y -= 64;
            }
        }

        public override bool? CanHitNPC(NPC target) => false;
    }
}
