using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.ClassWeapons
{
    public class FadingDagger : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Critical hits deal an additional 4 damage\n" +
                               "[c/6758a1:[Can be upgraded early Hardmode][c/6758a1:]]");
        }

		public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 9;
            Item.crit = 2;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.rare = 1;
            Item.knockBack = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.FadingDagger>();
            Item.shootSpeed = 8f;
            Item.value = 050000;
            Item.autoReuse = true;
        }
    }

    public class MasteredFadingDagger : ModItem
    {
        Player player = Main.player[Main.myPlayer];

        public override string Texture => "ApacchiisClassesMod2/Items/ClassWeapons/FadingDagger";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Critical hits deal an additional 10 damage");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 26;
            Item.crit = 3;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 7;
            Item.useTime = 7;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.rare = 4;
            Item.knockBack = 3;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.FadingDagger>();
            Item.shootSpeed = 8f;
            Item.value = 150000;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<FadingDagger>());
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.Register();

            base.AddRecipes();
        }
    }
}