using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.ID;

namespace ApacchiisClassesMod2.Projectiles.BloodMage
{
    public class BloodEnchantment : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Enchantment Effect");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 20;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation -= .05f;
                Projectile.netUpdate = true;

                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.rotation);
            base.SendExtraAI(writer);
        }
        
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            float rot = reader.Read();
            Projectile.rotation = rot;
            base.ReceiveExtraAI(reader);
        }

        public override bool? CanHitNPC(NPC target) => false;
    }

    public class BloodEnchantment2 : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Enchantment Effect");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 20;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation += .025f;
                Projectile.netUpdate = true;

                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.rotation);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            float rot = reader.Read();
            Projectile.rotation = rot;
            base.ReceiveExtraAI(reader);
        }

        public override bool? CanHitNPC(NPC target) => false;
    }

    public class BloodEnchantment3 : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Enchantment Effect");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 20;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation -= .025f;
                Projectile.netUpdate = true;

                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.rotation);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            float rot = reader.Read();
            Projectile.rotation = rot;
            base.ReceiveExtraAI(reader);
        }

        public override bool? CanHitNPC(NPC target) => false;
    }

    public class BloodEnchantment4 : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Enchantment Effect");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
            //ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 20;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation -= .025f;
                Projectile.netUpdate = true;

                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.rotation);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            float rot = reader.Read();
            Projectile.rotation = rot;
            base.ReceiveExtraAI(reader);
        }

        public override bool? CanHitNPC(NPC target) => false;
    }
}
