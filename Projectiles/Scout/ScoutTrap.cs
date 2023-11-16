using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Scout
{
    public class ScoutTrap : ModProjectile
    {
        bool hasHitEnemy = false;
        float degree = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scout's Trap");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 30;
            Projectile.alpha = 150;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = .15f;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            if (degree < 360f)
                degree += acmPlayer.scoutTrapChargeRate;

            if(Projectile.alpha > 0)
                Projectile.alpha -= acmPlayer.scoutTrapChargeRate;

            //Lighting.AddLight(Projectile.Center, .2f, .05f, .05f);

            // Circle Dust
            Vector2 origin = Projectile.Center;
            float radius = player.GetModPlayer<ACMPlayer>().scoutTrapRange;

            int locations = 24;
            for (int i = 0; i < locations; i++)
            {
                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(degree / locations * i)) * radius;
                var dust = Dust.NewDustPerfect(position, 6, Vector2.Zero, 0, default, 1f);
                dust.color = Color.Green;
                dust.noGravity = true;
            }

            for(int i = 0; i < Main.maxNPCs; i++)
            {
                if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) <= player.GetModPlayer<ACMPlayer>().scoutTrapRange && Collision.CanHitLine(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1) && degree >= 360f && Main.npc[i].type != NPCID.TargetDummy && !Main.npc[i].townNPC && !Main.npc[i].CountsAsACritter && !Main.npc[i].dontTakeDamage && Main.npc[i].type != NPCID.DD2Bartender && Main.npc[i].type != NPCID.DD2EterniaCrystal && Main.npc[i].type != NPCID.DD2LanePortal)
                {
                        int hitDir = 0;
                        if (Main.npc[i].position.X < Projectile.position.X)
                            hitDir = -1;
                        else
                            hitDir = 1;

                        //if (Vector2.Distance(Projectile.Center, Main.npc[x].Center) <= player.GetModPlayer<ACMPlayer>().scoutTrapRange * 2 && Collision.CanHitLine(Projectile.Center, 1, 1, Main.npc[x].Center, 1, 1) && degree >= 360f && !Main.npc[x].townNPC && !Main.npc[x].CountsAsACritter && !Main.npc[x].dontTakeDamage && Main.npc[x].type != NPCID.DD2Bartender && Main.npc[x].type != NPCID.DD2EterniaCrystal && Main.npc[x].type != NPCID.DD2LanePortal)
                            player.ApplyDamageToNPC(Main.npc[i], (int)(player.GetModPlayer<ACMPlayer>().scoutTrapDamage * player.GetModPlayer<ACMPlayer>().abilityPower), 4, hitDir, false);
                        
                    Projectile.Kill();
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.realLife != 0)
                modifiers.FinalDamage /= 4;

            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width * 2, Projectile.height * 2, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }

            // Fire Dust spawn
            for (int i = 0; i < 40; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.width * 5, Projectile.position.Y - Projectile.width * 5), Projectile.width * 10, Projectile.height * 10, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.width * 5, Projectile.position.Y - Projectile.width * 5), Projectile.width * 10, Projectile.height * 10, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }

            base.OnKill(timeLeft);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
    }
}
