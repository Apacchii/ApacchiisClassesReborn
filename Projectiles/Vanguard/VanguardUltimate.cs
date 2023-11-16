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
            Projectile.timeLeft = 70;
            Projectile.aiStyle = 0;
            //aiType = ProjectileID.WoodenArrowFriendly;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;

            DrawOffsetX = 150;
        }

        public override void AI()
        {
            var v = Dust.NewDustDirect(Projectile.Center, 8, 8, DustID.AmberBolt, 0, -10, 0, Color.White, 2f);
            var v2 = Dust.NewDustDirect(Projectile.Center, 8, 8, DustID.AmberBolt, 0, -5, 0, Color.White, 4f);

            v.noGravity = true;
            v2.noGravity = true;

            var d1 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * -.2f, Projectile.velocity.Y * -.2f, 0, Color.White, 1.25f);
            var d2 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * .33f, Projectile.velocity.Y * .33f, 0, Color.White, .75f);
            d1.noGravity = true;
            d2.noGravity = true;

            var d3 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt,new Vector2(-20, -25), 0, Color.White, 1.5f);
            var d4 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(20, -25), 0, Color.White, 1.5f);
            d3.noGravity = true;
            d4.noGravity = true;

            var d5 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(-10, -25), 0, Color.White, 1.5f);
            var d6 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(10, -25), 0, Color.White, 1.5f);
            d5.noGravity = true;
            d6.noGravity = true;

            var d11 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * -.1f, Projectile.velocity.Y * -.1f, 0, Color.White, 1.25f);
            var d22 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * .16f, Projectile.velocity.Y * .16f, 0, Color.White, .75f);
            d11.noGravity = false;
            d22.noGravity = false;
            
            var d33 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(-5, 2), 0, Color.White, 1.5f);
            var d44 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(5, 2), 0, Color.White, 1.5f);
            d33.noGravity = false;
            d44.noGravity = false;
            
            var d55 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(-1, 1), 0, Color.White, 1.5f);
            var d66 = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(1, 1), 0, Color.White, 1.5f);
            d55.noGravity = false;
            d66.noGravity = false;

            x--;
            if(x == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
                x = 2;
            }
            base.AI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            if (!target.boss && target.life <= target.life / 2)
                target.SimpleStrikeNPC(target.life * 10, -target.direction);
            else if (target.boss && target.life <= (int)(target.life * acmPlayer.vanguardUltimateBossExecute))
                target.SimpleStrikeNPC(target.life * 10, -target.direction);


            //if (target.realLife != 0)
            if (target.type != NPCID.EaterofWorldsHead && target.type != NPCID.EaterofWorldsBody && target.type != NPCID.EaterofWorldsTail && target.type != NPCID.TheDestroyer && target.type != NPCID.TheDestroyerBody && target.type != NPCID.TheDestroyerBody)
                modifiers.FinalDamage /= 4;

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

