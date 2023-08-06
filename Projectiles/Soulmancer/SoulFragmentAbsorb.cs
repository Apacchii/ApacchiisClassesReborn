using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework.Graphics;

namespace ApacchiisClassesMod2.Projectiles.Soulmancer
{
    public class SoulFragmentAbsorb : ModProjectile
    {
        int timeToHit = 10;
        bool canHome = false;

        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("Absorved Soul Fragment");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.timeLeft = 60 * 5;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            var dust = Dust.NewDustPerfect(Projectile.Center, 63, new Vector2(Projectile.velocity.X * .8f, Projectile.velocity.Y * .8f), 110, Color.Red, 3f);
            dust.noGravity = true;
            dust.noLight = true;
            dust.noLightEmittence = true;

            if (Projectile.timeLeft <= 60 * 5 - timeToHit)
                canHome = true;

            if (canHome)
            {
                #region Homing To Self
                float num132_2 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
                float num133_2 = Projectile.localAI[0];
                if (num133_2 == 0f)
                {
                    Projectile.localAI[0] = num132_2;
                    num133_2 = num132_2;
                }
                float num134_2 = Projectile.position.X;
                float num135_2 = Projectile.position.Y;
                float range = 50000f;
                bool flag3_2 = false;
                int num137_2 = 0;
                if (Projectile.ai[1] == 0f)
                {
                    for (int target = 0; target < 200; target++)
                    {
                        if (Main.player[target].whoAmI == Main.myPlayer && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(target + 1)))
                        {
                            float num139_2 = Main.player[target].position.X + (float)(Main.player[target].width / 2);
                            float num140_2 = Main.player[target].position.Y + (float)(Main.player[target].height / 2);
                            float num141_2 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num139_2) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num140_2);
                            if (num141_2 < range /*&& Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.player[target].position, Main.player[target].width, Main.player[target].height)*/)
                            {
                                range = num141_2;
                                num134_2 = num139_2;
                                num135_2 = num140_2;
                                flag3_2 = true;
                                num137_2 = target;
                            }
                        }
                    }
                    if (flag3_2)
                    {
                        Projectile.ai[1] = (float)(num137_2 + 1);
                    }
                    flag3_2 = false;
                }
                if (Projectile.ai[1] > 0f)
                {
                    int num142_2 = (int)(Projectile.ai[1] - 1f);
                    if (Main.player[num142_2].active && Main.player[num142_2].whoAmI == Main.myPlayer)
                    {
                        float num143_2 = Main.player[num142_2].position.X + (float)(Main.player[num142_2].width / 2);
                        float num144_2 = Main.player[num142_2].position.Y + (float)(Main.player[num142_2].height / 2);
                        if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num143_2) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num144_2) < range)
                        {
                            flag3_2 = true;
                            num134_2 = Main.player[num142_2].position.X + (float)(Main.player[num142_2].width / 2);
                            num135_2 = Main.player[num142_2].position.Y + (float)(Main.player[num142_2].height / 2);
                        }
                    }
                    else
                    {
                        Projectile.ai[1] = 0f;
                    }
                }
                if (!Projectile.friendly)
                {
                    flag3_2 = false;
                }
                if (flag3_2)
                {
                    float num145_2 = num133_2;
                    Vector2 vector10_2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num146_2 = num134_2 - vector10_2.X;
                    float num147_2 = num135_2 - vector10_2.Y;
                    float num148_2 = (float)Math.Sqrt((double)(num146_2 * num146_2 + num147_2 * num147_2));
                    num148_2 = num145_2 / num148_2;
                    num146_2 *= num148_2;
                    num147_2 *= num148_2;
                    int num149_2 = 8;
                    Projectile.velocity.X = (Projectile.velocity.X * (float)(num149_2 - 1) + num146_2) / (float)num149_2;
                    Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num149_2 - 1) + num147_2) / (float)num149_2;
                }
                #endregion

                float dist = Projectile.Distance(player.Center);
                if (dist < 16)
                {
                    int healing = (int)(player.statLifeMax2 * acmPlayer.soulmancerConsumeHeal);
                    player.GetModPlayer<ACMPlayer>().HealPlayer(0, 0, healing);
                    Projectile.Kill();
                }
            }

            base.AI();
        }
    }
}
