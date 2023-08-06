using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Inventor
{
    public class Sentry : ModProjectile
    {
        bool flag = false;
        //int firerate = 60;
        int firerateBase = 60;
        float range = 600;
        int animationTimer = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Inventor's Turret");
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 48;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.alpha = 0;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            Projectile.timeLeft = 60;

            if (!flag)
            {
                //firerate = acmPlayer.inventorSentryFirerate;
                firerateBase = acmPlayer.inventorSentryFirerate;
                range = acmPlayer.inventorSentryRange;
                flag = true;
            }

            // Circle Dust
            Vector2 origin = Projectile.Center;
            origin.X -= Projectile.width / 2;

            int locations = (int)(range / 10);
            for (int i = 0; i < locations; i++)
            {
                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * range;
                var dust = Dust.NewDustPerfect(position, 2, Vector2.Zero, 0, Color.Orange, 1.25f);
                dust.noGravity = true;
            }

            Projectile.ai[0]--;

            if (animationTimer > 0)
            {
                //Projectile.frame = 1;
                if(animationTimer == 2)
                    Lighting.AddLight(Projectile.Center, new Vector3(.75f, .75f, 0f));
            }
            else
                Projectile.frame = 0;

            animationTimer--;

            ShootToTarget();

            // Apply passive to projectile owner
            if (Vector2.Distance(Projectile.Center, Main.player[Projectile.owner].Center) < range && player.whoAmI == Main.player[Projectile.owner].whoAmI)
                player.AddBuff(ModContent.BuffType<Buffs.Inventor.InventorPassive>(), 20);
        }

        public override bool? CanHitNPC(NPC target) => false;

        void ShootToTarget()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();


            //Shooting
            for (int i = 0; i < 200; i++)
            {
                // Get target
                NPC target = Main.npc[i];

                Vector2 casingPos = Projectile.Center;
                casingPos.X -= 12;
                casingPos.Y += 4;

                //Shoot to where ?
                float shootToX = target.Center.X + (float)target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.Center.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                float casingRandX = Main.rand.NextFloat(-1f, -1.25f);
                float casingRandY = Main.rand.NextFloat(-2f, -2.25f);

                // Enemy checks and shooting
                if (distance < range && !target.friendly && target.active && target.type != NPCID.TargetDummy && target.lifeMax > 5)
                {
                    if (Projectile.ai[0] <= 0)
                    {
                         Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, 0f, -25f, ModContent.ProjectileType<SentryShot>(), acmPlayer.inventorSentryDamage, 1, player.whoAmI);

                        SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);
                        Dust.NewDustDirect(casingPos, 1, 1, ModContent.DustType<Dusts.BulletCasing>(), casingRandX, casingRandY, 0, default);
                         animationTimer = 3;

                        if(acmPlayer.inventorOverclockCurDuration > 0)
                            Projectile.ai[0] = firerateBase * .5f;
                        else
                            Projectile.ai[0] = firerateBase;

                    }
                }
            }
        }

        //Old unused
        void ShootToTarget2()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                if (distance < range && !target.friendly && target.active && target.type != NPCID.TargetDummy && target.lifeMax > 5)
                {
                    distance = 5f / distance;
                    shootToX *= distance * 5;
                    shootToY *= distance * 5;

                    if (Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
                    {
                        Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, ProjectileID.BulletHighVelocity, acmPlayer.inventorSentryDamage, 1, player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);
                    }
                }
            }
        }
    }
}
