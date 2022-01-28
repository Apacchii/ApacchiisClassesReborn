using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Projectiles.Vanguard
{
	public class VanguardSpear : ModProjectile
	{
        Player Player = Main.player[Main.myPlayer];

        //public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        bool flag = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vanguard's Spear");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 14;
            Projectile.originalDamage = 1;
            Projectile.height = 14;
            Projectile.timeLeft = 60;
            Projectile.aiStyle = 0;
            //aiType = ProjectileID.WoodenArrowFriendly;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 2;
            Projectile.tileCollide = false;
            return false;
        }

        public override void AI()
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;

            var d1 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * -.2f, Projectile.velocity.Y * -.2f, 0, Color.White, 1.25f);
            var d2 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * .33f, Projectile.velocity.Y * .33f, 0, Color.White, .75f);
            d1.noGravity = true;
            d2.noGravity = true;

            if (Projectile.timeLeft == 1)
            {
                if(acmPlayer.vanguardTalent_6 == "L")
                {
                    Projectile.width = 600;
                    Projectile.height = 600;
                }
                else
                {
                    Projectile.width = 300;
                    Projectile.height = 300;
                }
               
                if (!flag)
                {
                    if(acmPlayer.vanguardTalent_6 == "L")
                    {
                        Projectile.position.X -= 300;
                        Projectile.position.Y -= 300;
                    }
                    else
                    {
                        Projectile.position.X -= 150;
                        Projectile.position.Y -= 150;
                    }
                    flag = true;
                }

                Projectile.damage = (int)(acmPlayer.vanguardSpearDamage * acmPlayer.abilityPower);

                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

                
                if(acmPlayer.vanguardTalent_6 == "L")
                {
                    for (int i = 0; i < 50; i++)
                    {
                        var d3 = Dust.NewDustDirect(Projectile.position, 600, 600, DustID.AmberBolt, Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, Color.White, 2f);
                        var d4 = Dust.NewDustDirect(Projectile.position, 600, 600, DustID.AmberBolt, Main.rand.Next(-10, 10), Main.rand.Next(-10, 10), 0, Color.White, 1f);
                        d3.noGravity = true;
                        d4.noGravity = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var d3 = Dust.NewDustDirect(Projectile.position, 300, 300, DustID.AmberBolt, Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, Color.White, 2f);
                        var d4 = Dust.NewDustDirect(Projectile.position, 300, 300, DustID.AmberBolt, Main.rand.Next(-10, 10), Main.rand.Next(-10, 10), 0, Color.White, 1f);
                        d3.noGravity = true;
                        d4.noGravity = false;
                    }
                }
            }
            else
            {
                if (Main.hardMode)
                    Projectile.damage = 5;
                else
                    Projectile.damage = 2;
            }
                
            base.AI();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //target.GetGlobalNPC<ACMGlobalNPC>().vanguardSpearPos = projectile.Center;
            if(!target.boss && target.type != NPCID.TargetDummy && Projectile.timeLeft > 5)
            {
                target.Center = Projectile.Center;
                //target.GetGlobalNPC<ACMGlobalNPC>().vanguardSpeared = true;
            }

            if (target.boss && Projectile.timeLeft > 5)
                Projectile.timeLeft = 5;

            //if(projectile.timeLeft <= 2)
            //    target.GetGlobalNPC<ACMGlobalNPC>().vanguardSpeared = false;

            target.immune[Projectile.owner] = 0;
           
            base.OnHitNPC(target, damage, knockback, crit);
        }

        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //    //Redraw the projectile with the color not influenced by light
        //    Vector2 drawOrigin = new Vector2(Main.ProjectileTexture[Projectile.type].Width * 0.5f, Projectile.height * 0.5f);
        //    for (int k = 0; k < Projectile.oldPos.Length; k++)
        //    {
        //        Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
        //        Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
        //        spriteBatch.Draw(Main.ProjectileTexture[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
        //    }
        //    return true;
        //}

        public override bool? CanHitNPC(NPC target)
        {
            if (!target.townNPC && !target.CountsAsACritter)
            {
                if (Projectile.timeLeft == 4 || Projectile.timeLeft == 5 || Projectile.timeLeft == 6)
                    return false;
                else
                    return true;
            }
            else
            { return false; }
        }
    }
}

