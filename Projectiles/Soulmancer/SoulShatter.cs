using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Soulmancer
{
    public class SoulShatter : ModProjectile
    {
        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Shatter");
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = Main.player[Main.myPlayer].GetModPlayer<ACMPlayer>();

            // Circle Dust
            Vector2 origin = Projectile.Center;
            float radius = acmPlayer.soulmancerSoulShatterRange;

            int locations = 50;
            for (int i = 0; i < locations; i++)
            {
                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                var dust = Dust.NewDustPerfect(position, 180, Vector2.Zero, 0, Color.PaleGreen, 2f);
                dust.noGravity = true;
            }

            for(int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) <= acmPlayer.soulmancerSoulShatterRange && !Main.npc[i].townNPC && !Main.npc[i].CountsAsACritter && !Main.npc[i].dontTakeDamage && Main.npc[i].type != NPCID.DD2Bartender && Main.npc[i].type != NPCID.DD2EterniaCrystal && Main.npc[i].type != NPCID.DD2LanePortal && !Main.npc[i].friendly)
                    {
                        int hitDir;
                        if (Main.npc[i].position.X < Main.player[Projectile.owner].position.X)
                            hitDir = -1;
                        else
                            hitDir = 1;

                        player.ApplyDamageToNPC(Main.npc[i], (int)(acmPlayer.soulmancerSoulShatterDamage * player.GetModPlayer<ACMPlayer>().abilityPower), 0, hitDir, false);

                        Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                        speed.Normalize();
                        speed *= 15f;
                        Projectile.NewProjectile(default, Main.npc[i].Center, speed, ModContent.ProjectileType<SoulFragment>(), (int)(acmPlayer.soulmancerSoulRipDamage * acmPlayer.abilityPower), 0, player.whoAmI);
                    }
                }
            }
        }
    }
}
