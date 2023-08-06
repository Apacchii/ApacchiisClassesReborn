using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.ClassWeapons
{
    public class SoulBurner : ModItem
	{
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Holding this weapon increases your Soulmancer's class passive chance by 25%");
        }

		public override void SetDefaults()
		{
            Item.width = 32;
            Item.height = 32;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 3;
            Item.crit = 1;
            Item.mana = 1;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 21;
            Item.useTime = 21;
            Item.UseSound = SoundID.Item8;
            Item.rare = 1;
            Item.knockBack = 0;
            Item.shootSpeed = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.SoulBurn>();
            Item.value = 150000;
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, new Vector2 (Main.MouseWorld.X, Main.MouseWorld.Y), new Vector2(0f, 0f), type, damage, knockback, player.whoAmI);

            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (Collision.CanHitLine(new Vector2(player.Center.X, player.Center.Y - 8), 1, 1, Main.MouseWorld, 1, 1))
                return true;
            else
                return false;
        }
    }
}