using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Weapons
{
    public class BloodSpray : ModProjectile
    {
        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Spray");
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
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60 * 3;
            Projectile.alpha = 0;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            var dust = Dust.NewDustPerfect(Projectile.Center, DustID.RedMoss, new Vector2(Projectile.velocity.X * .75f, Projectile.velocity.Y * .75f), 0, Color.Red, 1.25f);
            dust.noGravity = true;

            if(Main.hardMode)
                Projectile.velocity.Y += .14f;
            else
                Projectile.velocity.Y += .2f;

        }
    }
}
