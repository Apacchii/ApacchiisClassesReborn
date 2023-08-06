using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Projectiles.Vanguard
{
	public class VanguardUltimate : ModProjectile
	{
        //public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        int x = 2;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vanguard's Sword");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 500;
            Projectile.height = 398;
            Projectile.timeLeft = 60;
            Projectile.aiStyle = 0;
            //aiType = ProjectileID.WoodenArrowFriendly;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;

            DrawOffsetX = 150;
        }

        public override void AI()
        {
            var d1 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * -.2f, Projectile.velocity.Y * -.2f, 0, Color.White, 1.25f);
            var d2 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * .33f, Projectile.velocity.Y * .33f, 0, Color.White, .75f);
            d1.noGravity = true;
            d2.noGravity = true;

            var d3 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt,new Vector2(-20, 0), 0, Color.White, 1.5f);
            var d4 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(20, 0), 0, Color.White, 1.5f);
            d3.noGravity = true;
            d4.noGravity = true;

            var d5 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(-10, 0), 0, Color.White, 1.5f);
            var d6 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(10, 0), 0, Color.White, 1.5f);
            d5.noGravity = true;
            d6.noGravity = true;

            x--;
            if(x == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
                x = 2;
            }
            base.AI();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            if (!target.boss && target.life <= target.life / 2)
                target.SimpleStrikeNPC(target.life * 10, -target.direction);
            else if (target.boss && target.life <= (int)(target.life * acmPlayer.vanguardUltimateBossExecute))
                target.SimpleStrikeNPC(target.life * 10, -target.direction);

            if (target.realLife != 0)
                modifiers.FinalDamage /= 3;

            base.ModifyHitNPC(target, ref modifiers);
        }

        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //    //Trail
        //    Vector2 drawOrigin = new Vector2(Main.ProjectileTexture[Projectile.type].Width * 0.5f, Projectile.height * 0.5f);
        //    for (int k = 0; k < Projectile.oldPos.Length; k++)
        //    {
        //        Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(150f, Projectile.gfxOffY);
        //        Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
        //        spriteBatch.Draw(Main.ProjectileTexture[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
        //    }
        //    return true;
        //}
    }
}

