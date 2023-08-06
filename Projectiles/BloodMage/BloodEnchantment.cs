using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.ID;
using System.Xml;

namespace ApacchiisClassesMod2.Projectiles.BloodMage
{
    public class BloodEnchantment : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Enchantment Effect");
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
            if (Configs.ACMConfigClient.Instance.smallVFX)
                Projectile.scale = .5f;
            else
                Projectile.scale = 1f;

            Player player = Main.player[Main.myPlayer];
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation -= .05f;

                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
                //Projectile.ai[1] = Projectile.rotation;
                Projectile.netUpdate = true;
            }
        }

        //public override void SendExtraAI(BinaryWriter writer)
        //{
        //    writer.Write(Projectile.ai[1]);
        //    base.SendExtraAI(writer);
        //}
        //
        //public override void ReceiveExtraAI(BinaryReader reader)
        //{
        //    Projectile.rotation = reader.Read();
        //    base.ReceiveExtraAI(reader);
        //}
        public override bool? CanCutTiles() => false;
        public override bool? CanHitNPC(NPC target) => false;
    }

    public class BloodEnchantment2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Enchantment Effect");
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
            if (Configs.ACMConfigClient.Instance.smallVFX)
                Projectile.scale = .5f;
            else
                Projectile.scale = 1f;

            Player player = Main.player[Main.myPlayer];
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation += .025f;

                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
                //Projectile.ai[1] = Projectile.rotation;
                Projectile.netUpdate = true;
            }
        }

        //public override void SendExtraAI(BinaryWriter writer)
        //{
        //    writer.Write(Projectile.ai[1]);
        //    base.SendExtraAI(writer);
        //}
        //
        //public override void ReceiveExtraAI(BinaryReader reader)
        //{
        //    Projectile.rotation = reader.Read();
        //    base.ReceiveExtraAI(reader);
        //}
        public override bool? CanCutTiles() => false;
        public override bool? CanHitNPC(NPC target) => false;
    }

    public class BloodEnchantment3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Enchantment Effect");
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
            if (Configs.ACMConfigClient.Instance.smallVFX)
                Projectile.scale = .5f;
            else
                Projectile.scale = 1f;

            Player player = Main.player[Main.myPlayer];
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation -= .025f;
                
                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
                //Projectile.ai[1] = Projectile.rotation;
                Projectile.netUpdate = true;
            }
        }

        //public override void SendExtraAI(BinaryWriter writer)
        //{
        //    writer.Write(Projectile.ai[1]);
        //    base.SendExtraAI(writer);
        //}
        //
        //public override void ReceiveExtraAI(BinaryReader reader)
        //{
        //    Projectile.rotation = reader.Read();
        //    base.ReceiveExtraAI(reader);
        //}
        public override bool? CanCutTiles() => false;
        public override bool? CanHitNPC(NPC target) => false;
    }

    public class BloodEnchantment4 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Enchantment Effect");
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
            if (Configs.ACMConfigClient.Instance.smallVFX)
                Projectile.scale = .5f;
            else
                Projectile.scale = 1f;

            Player player = Main.player[Main.myPlayer];
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.Center = player.Center;
                Projectile.rotation -= .025f;
                
                if (!player.GetModPlayer<ACMPlayer>().bloodMageBloodEnchantment)
                    Projectile.Kill();
                else
                    Projectile.timeLeft = 60;
                Projectile.ai[1] = Projectile.rotation;
                Projectile.netUpdate = true;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.ai[1]);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.rotation = reader.Read();
            base.ReceiveExtraAI(reader);
        }

        public override bool? CanCutTiles() => false;

        public override bool? CanHitNPC(NPC target) => false;
    }
}
