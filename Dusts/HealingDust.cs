using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Dusts
{
	public class HealingDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 16, 16);
			dust.velocity.Y = Main.rand.NextFloat(-1f, -2f);
			dust.velocity.X = Main.rand.NextFloat(-.5f, .5f);
		}

		public override bool Update(Dust dust)
		{
			// Move the dust based on its velocity and reduce its size to then remove it, as the 'return false;' at the end will prevent vanilla logic.
			dust.position += dust.velocity;
			dust.scale -= 0.01f;

			if (dust.scale < 0.5f)
				dust.active = false;

			return false;
		}
	}
}