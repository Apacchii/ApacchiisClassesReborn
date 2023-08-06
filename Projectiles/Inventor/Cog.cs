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
using Terraria.GameContent;

namespace ApacchiisClassesMod2.Projectiles.Inventor
{
    public class Cog : ModProjectile
    {
        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("Inventor's Cog");
            //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            //ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.timeLeft = 60 * 2;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            Projectile.scale = .75f;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.spriteDirection = Projectile.direction;

            base.AI();
        }

        //public override bool PreDraw(ref Color lightColor)
        //{
        //    Main.instance.LoadProjectile(Projectile.type);
        //    Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
        //
        //    // Redraw the projectile with the color not influenced by light
        //    Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        //    for (int k = 0; k < Projectile.oldPos.Length; k++)
        //    {
        //        Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
        //        Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
        //        Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
        //    }
        //
        //    return true;
        //}

        public override bool? CanCutTiles() => false;
    }
}