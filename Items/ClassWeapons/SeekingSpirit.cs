using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.ClassWeapons
{
    public class SeekingSpirit : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seeking Spirit");
            Tooltip.SetDefault("Fires a homing spirit at enemies\n" +
                               "[c/6758a1:[Weapon upgrades after Hardmode][c/6758a1:]]");
        }

		public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Summon;
            Item.damage = 14;
            Item.crit = 0;
            Item.mana = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 54;
            Item.useTime = 54;
            Item.UseSound = SoundID.Item8;
            Item.rare = 1;
            Item.knockBack = 2;
            Item.shootSpeed = 12;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.SeekingSpirit>();
            Item.value = 050000;
            Item.autoReuse = true;
        }
    }

    public class MasteredSeekingSpirit : ModItem
    {
        public override string Texture => "ApacchiisClassesMod2/Items/ClassWeapons/SeekingSpirit";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mastered Seeking Spirit");
            Tooltip.SetDefault("Fires a burst of 4 homing spirits at enemies");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Summon;
            Item.damage = 29;
            Item.crit = 0;
            Item.mana = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 5;
            Item.UseSound = SoundID.Item8;
            Item.rare = 4;
            Item.knockBack = 3;
            Item.shootSpeed = 15;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.SeekingSpirit>();
            Item.value = 150000;
            Item.autoReuse = true;
            Item.reuseDelay = 34;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<SeekingSpirit>());
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.Register();

            base.AddRecipes();
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
             Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(7));
             newVelocity *= 1f - Main.rand.NextFloat(0.15f);
             Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);

            return false;
        }
    }
}