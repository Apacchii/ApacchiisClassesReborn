using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using System.Reflection.Metadata;

namespace ApacchiisClassesMod2.Projectiles
{
    public class MajorsCareProj : ModProjectile
    {
        int range = 300;
        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Major's Care");
        }

        public override void SetDefaults()
        {
            Projectile.width = range;
            Projectile.height = range;
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
            Player player = Main.player[Main.myPlayer];
            int heal = (int)(player.statLifeMax2 * .01f);
            if (heal < 1) heal = 1;

            // Circle Dust
            Vector2 origin = Projectile.Center;
            //origin.X -= Projectile.width / 2;
            float radius = Projectile.width;

            int locations = 40;
            for (int i = 0; i < locations; i++)
            {
                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                var dust = Dust.NewDustPerfect(position, 120, Vector2.Zero, 0, Color.White, 1f);
                dust.noGravity = true;
            }

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int i = 0; i < 255; i++)
                    if (Main.player[i].active && !Main.player[i].dead)
                        if (Vector2.Distance(origin, Main.player[i].Center) <= radius)
                        {
                        
                            ModPacket packet = Mod.GetPacket();
                            packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayer);
                            packet.Write((byte)i);
                            packet.Write(heal);
                            packet.Send(-1, -1);
                        }        
            }
            else
            {
                if(player.statLife < player.statLifeMax2)
                {
                    player.statLife += heal;
                    player.HealEffect(heal);
                }
            }
        }

        public override bool? CanHitNPC(NPC target) => false;
        public override bool? CanCutTiles() => false;
    }
}
