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

namespace ApacchiisClassesMod2.Projectiles.Titania
{
    public class TitaniaPixie : ModProjectile
    {
        int timeToHit = 4;
        int attackCooldown = 0;

        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults() {
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.timeLeft = 60 * 16;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.aiStyle = ProjAIStyleID.Raven;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            Projectile.rotation = Projectile.velocity.ToRotation();

            var dust = Dust.NewDustPerfect(Projectile.Center, DustID.UnusedWhiteBluePurple, new Vector2(Projectile.velocity.X * .8f, Projectile.velocity.Y * .8f), 110, Color.LightPink, 1f);
            dust.noGravity = true;

            var dust2 = Dust.NewDustPerfect(Projectile.Center, DustID.BeachShell, new Vector2(Projectile.velocity.X * .8f, Projectile.velocity.Y * .8f), 110, Color.LightBlue, .75f);
            dust2.noGravity = true;

            attackCooldown--;
            if (attackCooldown > 0)
                Projectile.aiStyle = ProjAIStyleID.Arrow;
            else
                Projectile.aiStyle = ProjAIStyleID.Raven;
            base.AI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            attackCooldown = 20;
            base.OnHitNPC(target, hit, damageDone);
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.timeLeft <= 60 * 16 - timeToHit && !target.townNPC && !target.friendly && target.type != NPCID.TargetDummy && attackCooldown <= 0)
                return true;
            else
                return false;
        }

        public override bool? CanCutTiles() => false;
    }
}