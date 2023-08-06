using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ApacchiisClassesMod2.Projectiles.Vanguard
{
	public class VanguardSpear : ModProjectile
	{
        //public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        bool flag = false;
        int range;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vanguard's Spear");
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 60;
            Projectile.aiStyle = 0;
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

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            

            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;

            var d1 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * -.2f, Projectile.velocity.Y * -.2f, 0, Color.White, 1.25f);
            var d2 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * .33f, Projectile.velocity.Y * .33f, 0, Color.White, .75f);
            d1.noGravity = true;
            d2.noGravity = true;

            if (acmPlayer.vanguardTalent_4 == "L")
                range = 800;
            else
                range = 400;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) <= 100 && !Main.npc[i].townNPC && !Main.npc[i].dontTakeDamage && Main.npc[i].type != NPCID.DD2Bartender && Main.npc[i].type != NPCID.DD2EterniaCrystal && Main.npc[i].type != NPCID.DD2LanePortal && !Main.npc[i].friendly)
                    {
                        int hitDir;
                        if (Main.npc[i].position.X < Main.player[Projectile.owner].position.X)
                            hitDir = -1;
                        else
                            hitDir = 1;

                        if (acmPlayer.vanguardTalent_6 == "L") // Double range
                        {
                            Projectile.width = 800;
                            Projectile.height = 800;
                            range = 800;
                        }
                        else
                        {
                            Projectile.width = 400;
                            Projectile.height = 400;
                            range = 400;
                        }

                        if (!flag)
                        {
                            if (acmPlayer.vanguardTalent_6 == "L")
                            {
                                Projectile.position.X -= 400;
                                Projectile.position.Y -= 400;
                            }
                            else
                            {
                                Projectile.position.X -= 200;
                                Projectile.position.Y -= 200;
                            }
                            flag = true;
                        }

                        for (int i2 = 0; i2 < Main.maxNPCs; i2++)
                            if (Vector2.Distance(Projectile.Center, Main.npc[i2].Center) <= range && !Main.npc[i2].townNPC && !Main.npc[i2].dontTakeDamage && Main.npc[i2].type != NPCID.DD2Bartender && Main.npc[i2].type != NPCID.DD2EterniaCrystal && Main.npc[i2].type != NPCID.DD2LanePortal && !Main.npc[i2].friendly)
                            {
                                if (Main.npc[i2].realLife != 0)
                                    player.ApplyDamageToNPC(Main.npc[i2], (int)(acmPlayer.vanguardSpearDamage * acmPlayer.abilityPower * player.GetModPlayer<ACMPlayer>().abilityPower / 4), 5f, hitDir, false);
                                else
                                    player.ApplyDamageToNPC(Main.npc[i2], (int)(acmPlayer.vanguardSpearDamage * acmPlayer.abilityPower * player.GetModPlayer<ACMPlayer>().abilityPower), 5f, hitDir, false);
                            }


                        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

                        if (acmPlayer.vanguardTalent_6 == "L")
                        {
                            for (int x = 0; x < 30; x++)
                            {
                                var d3 = Dust.NewDustDirect(Projectile.position, 800, 800, DustID.AmberBolt, Main.rand.Next(-20, 20), Main.rand.NextFloat(-20f, 20f), 0, Color.White, 2.25f);
                                var d4 = Dust.NewDustDirect(Projectile.position, 800, 800, DustID.AmberBolt, Main.rand.Next(-10, 10), Main.rand.NextFloat(-10f, 10f), 0, Color.White, .75f);
                                d3.noGravity = true;
                                d4.noGravity = false;
                            }
                        }
                        else
                        {
                            for (int y = 0; y < 70; y++)
                            {
                                var d3 = Dust.NewDustDirect(Projectile.position, 400, 400, DustID.AmberBolt, Main.rand.Next(-20, 20), Main.rand.NextFloat(-10f, 10f), 0, Color.White, 2.25f);
                                var d4 = Dust.NewDustDirect(Projectile.position, 400, 400, DustID.AmberBolt, Main.rand.Next(-10, 10), Main.rand.NextFloat(-10f, 10f), 0, Color.White, .75f);
                                d3.noGravity = true;
                                d4.noGravity = false;
                            }
                        }

                        Projectile.Kill();
                    }
                }
            }

            base.AI();
        }

    }
}

