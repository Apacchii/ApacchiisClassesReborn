using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.BloodMage
{
    public class Transfusion : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];
        bool hasHitEnemy = false;
        int healing = 0;

        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Transfusion");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 20;
            Projectile.alpha = 0;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, .4f, .05f, .05f);


            var dust = Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.RedMoss, Projectile.velocity.X * .6f, Projectile.velocity.Y * .6f, 0, Color.Red, 2f);
            dust.noGravity = true;
            if (hasHitEnemy)
            {
                for (int x = 0; x < 3; x++)
                { 
                    var dust2 = Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.RedMoss, Projectile.velocity.X * .6f, Projectile.velocity.Y * .6f, 0, Color.Red, 2f);
                    dust2.noGravity = true;
                }
            }

            if (!hasHitEnemy)
            {
                #region Homing To Enemy

            float num132 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
            float num133 = Projectile.localAI[0];
            if (num133 == 0f)
            {
                Projectile.localAI[0] = num132;
                num133 = num132;
            }
            float num134 = Projectile.position.X;
            float num135 = Projectile.position.Y;
            float range = 750f;
            bool flag3 = false;
            int num137 = 0;
            if (Projectile.ai[1] == 0f)
            {
                for (int target = 0; target < 200; target++)
                {
                    if (Main.npc[target].CanBeChasedBy(this, false) && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(target + 1)))
                    {
                        float num139 = Main.npc[target].position.X + (float)(Main.npc[target].width / 2);
                        float num140 = Main.npc[target].position.Y + (float)(Main.npc[target].height / 2);
                        float num141 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num139) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num140);
                        if (num141 < range && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[target].position, Main.npc[target].width, Main.npc[target].height))
                        {
                            range = num141;
                            num134 = num139;
                            num135 = num140;
                            flag3 = true;
                            num137 = target;
                        }
                    }
                }
                if (flag3)
                {
                    Projectile.ai[1] = (float)(num137 + 1);
                }
                flag3 = false;
            }
            if (Projectile.ai[1] > 0f)
            {
                int num142 = (int)(Projectile.ai[1] - 1f);
                if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
                {
                    float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                    float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num143) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num144) < 1000f)
                    {
                        flag3 = true;
                        num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                        num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    }
                }
                else
                {
                    Projectile.ai[1] = 0f;
                }
            }
            if (!Projectile.friendly)
            {
                flag3 = false;
            }
            if (flag3)
            {
                float num145 = num133;
                Vector2 vector10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num146 = num134 - vector10.X;
                float num147 = num135 - vector10.Y;
                float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
                num148 = num145 / num148;
                num146 *= num148;
                num147 *= num148;
                int num149 = 8;
                Projectile.velocity.X = (Projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
            }
            #endregion
            }
            else
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
                float range = 100000f;
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
                        if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num143_2) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num144_2) < 50000f) //Range ???
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
            }

            if(hasHitEnemy)
            {
                float dist = Projectile.Distance(player.Center);

                if (dist < 10)
                {
                    if (player.statLife < player.statLifeMax2)
                    {
                        player.statLife += healing;
                        player.HealEffect(healing);
                    }

                    SoundEngine.PlaySound(SoundID.Item3);

                    Projectile.Kill();
                }
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (hasHitEnemy && !target.townNPC && target.lifeMax > 5)
                return false;
            else
                return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            hasHitEnemy = true;

            float maxHeal = player.GetModPlayer<ACMPlayer>().bloodMageSiphonHealMax;
            healing = (int)(target.life * .1f * player.GetModPlayer<ACMPlayer>().abilityPower);
            if (healing > (int)(player.statLifeMax2 * maxHeal))
                healing = (int)(player.statLifeMax2 * maxHeal);
            if (healing < (int)(player.statLifeMax2 * .05f))
                healing = (int)(player.statLifeMax2 * .05f);

            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
