using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Commander
{
    public class BattleCry : ModProjectile
    {
        public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Battle Cry");
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
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

            // Circle Dust
            Vector2 origin = player.Center;
            origin.X -= 1;
            float radius = player.GetModPlayer<ACMPlayer>().commanderCryRange;

            int locations = 50;
            for (int i = 0; i < locations; i++)
            {
                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                var dust = Dust.NewDustPerfect(position, 2, Vector2.Zero, 0, Color.Orange, 2f);
                dust.noGravity = true;
            }

            for(int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) <= player.GetModPlayer<ACMPlayer>().commanderCryRange && Collision.CanHitLine(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1) && !Main.npc[i].townNPC && !Main.npc[i].dontTakeDamage && Main.npc[i].type != NPCID.DD2Bartender && Main.npc[i].type != NPCID.DD2EterniaCrystal && Main.npc[i].type != NPCID.DD2LanePortal && !Main.npc[i].friendly)
                    {
                        int hitDir;
                        if (Main.npc[i].position.X < Main.player[Projectile.owner].position.X)
                            hitDir = -1;
                        else
                            hitDir = 1;

                        Main.npc[i].GetGlobalNPC<ACMGlobalNPC>().battleCryBoost = player.GetModPlayer<ACMPlayer>().commanderCryDuration;
                        player.ApplyDamageToNPC(Main.npc[i], (int)(player.GetModPlayer<ACMPlayer>().commanderCryDamage * player.GetModPlayer<ACMPlayer>().abilityPower), 10, hitDir, false);
                        Main.npc[i].AddBuff(BuffID.Confused, 60 * 2);
                    }
                }
            }
        }
    }
}
