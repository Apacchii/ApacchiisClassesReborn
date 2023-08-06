using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Weapons
{
    public class SoulBurn : ModProjectile
    {

        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Burn");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
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
            for (int i = 0; i < 25; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var dust = Dust.NewDustPerfect(Projectile.Center, 180, speed * 2, Scale: 1f);
                dust.noGravity = true;
            }
        }
    }
}
