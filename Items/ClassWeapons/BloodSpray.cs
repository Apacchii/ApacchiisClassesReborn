using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.ClassWeapons
{
    public class BloodSpray : ModItem
	{
        int hpCost = 1;

        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("Uses " + hpCost + " health\n" +
                               "Fires a spray of 3 blobs of your own blood"); */
        }

		public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 6;
            Item.crit = 0;
            Item.mana = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.UseSound = SoundID.Item21;
            Item.rare = 1;
            Item.knockBack = 2;
            Item.shootSpeed = 15;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.BloodSpray>();
            Item.value = 050000;
            Item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
            if(player.statLife > hpCost)
               player.statLife -= hpCost;
                

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.hardMode)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                    Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
                else
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                    newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                    Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            //Wtf why was this here
            //var recipe = CreateRecipe(1);
            //recipe.AddIngredient(this);
            //recipe.AddTile(TileID.WorkBenches);
            //recipe.ReplaceResult(ModContent.ItemType<Relics.RandomRelic>(), 1);
            //recipe.Register();
        }

        public override Vector2? HoldoutOrigin()
		{
			return new Vector2(0, 0);
		}
    }

    //public class MasteredBloodSpray : ModItem
    //{
    //    public override string Texture => "ApacchiisClassesMod2/Items/ClassWeapons/BloodSpray";
    //
    //    int hpCost = 3;
    //
    //    public override void SetStaticDefaults()
    //    {
    //        DisplayName.SetDefault("Mastered Blood Spray");
    //        Tooltip.SetDefault("Uses " + hpCost + " health\n" +
    //                           "Fires a spray of 3 blobs of your own blood");
    //    }
    //
    //    public override void SetDefaults()
    //    {
    //        Item.width = 32;
    //        Item.height = 32;
    //        Item.noMelee = true;
    //        Item.scale = 1f;
    //        Item.DamageType = DamageClass.Magic;
    //        Item.damage = 24;
    //        Item.crit = 1;
    //        Item.mana = 6;
    //        Item.useStyle = ItemUseStyleID.Shoot;
    //        Item.useAnimation = 30;
    //        Item.useTime = 30;
    //        Item.UseSound = SoundID.Item21;
    //        Item.rare = 4;
    //        Item.knockBack = 2;
    //        Item.shootSpeed = 16;
    //        Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.BloodSpray>();
    //        Item.value = 150000;
    //        Item.autoReuse = true;
    //    }
    //
    //    public override bool CanUseItem(Player player)
    //    {
    //        if (player.statLife > hpCost)
    //            player.statLife -= hpCost;
    //
    //
    //        return base.CanUseItem(player);
    //    }
    //
    //    public override void AddRecipes()
    //    {
    //        var recipe = CreateRecipe(1);
    //        recipe.AddIngredient(ModContent.ItemType<BloodSpray>());
    //        recipe.AddIngredient(ItemID.SoulofNight, 1);
    //        recipe.AddIngredient(ItemID.SoulofLight, 1);
    //        recipe.Register();
    //
    //        base.AddRecipes();
    //    }
    //
    //    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    //    {
    //        int numberProjectiles = 3;
    //        for (int i = 0; i < numberProjectiles; i++)
    //        {
    //            if (Main.hardMode)
    //            {
    //                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
    //                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
    //                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
    //            }
    //            else
    //            {
    //                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
    //                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
    //                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
    //            }
    //        }
    //        return false;
    //    }
    //
    //    public override Vector2? HoldoutOrigin()
    //    {
    //        return new Vector2(0, 0);
    //    }
    //}
}