using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.ClassWeapons
{
    public class TrainingRapier : ModItem
	{
        Player player = Main.player[Main.myPlayer];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Training Rapier");
            Tooltip.SetDefault("While holding this weapon you reduce any damage taken from enemies by 6\n" +
                               "[c/6758a1:[Can be upgraded early Hardmode][c/6758a1:]]");
        }

		public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 12;
            Item.crit = 0;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.rare = 1;
            Item.knockBack = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.TrainingRapier>();
            Item.shootSpeed = 3f;
            Item.value = 050000;
            Item.autoReuse = false;
        }
    }

    public class masteredTrainingRapier : ModItem
    {
        Player player = Main.player[Main.myPlayer];

        public override string Texture => "ApacchiisClassesMod2/Items/ClassWeapons/TrainingRapier";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mastered Training Rapier");
            Tooltip.SetDefault("While holding this weapon you reduce any damage taken from enemies by 14");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 38;
            Item.crit = 1;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.rare = 4;
            Item.knockBack = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.TrainingRapier>();
            Item.shootSpeed = 3f;
            Item.value = 150000;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<TrainingRapier>());
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.Register();

            base.AddRecipes();
        }
    }
}