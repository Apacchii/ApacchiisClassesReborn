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

namespace ApacchiisClassesMod2.Projectiles.Inventor
{
    public class SentryShot : ModProjectile
    {
        bool canHome = false;
        int timeToHit = 0;

        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("Sentry Shot");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.timeLeft = 60 * 4;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;
            Projectile.alpha = 110;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            var dust = Dust.NewDustPerfect(Projectile.Center, 10, new Vector2(Projectile.velocity.X * .5f, Projectile.velocity.Y * .5f), 110, Color.Yellow, 1f);
            dust.noGravity = true;
               
            if (Projectile.timeLeft <= 60 * 4 - timeToHit)
                canHome = true;

            #region Homing
            if (canHome)
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
                float range = 3000f;
                int turnRate = 6; // 8 Default
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
                            if (num141 < range /*&& Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[target].position, Main.npc[target].width, Main.npc[target].height)*/)
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
                        if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num143) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num144) < range)
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
                    int num149 = turnRate;
                    Projectile.velocity.X = (Projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                    Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
                }
                #endregion
            }
            #endregion

            base.AI();
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.timeLeft <= 60 * 4 - timeToHit && !target.townNPC && !target.friendly && target.type != NPCID.TargetDummy)
                return true;
            else
                return false;
        }

        public override bool? CanCutTiles() => false;

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}