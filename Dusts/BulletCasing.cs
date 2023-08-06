using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Dusts
{
	public class BulletCasing : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 2, 6);
			dust.velocity.Y = -1f;
			dust.rotation += Main.rand.NextFloat(-3f, 3f);
		}

		public override bool Update(Dust dust)
		{
			// Move the dust based on its velocity and reduce its size to then remove it, as the 'return false;' at the end will prevent vanilla logic.
			dust.position += dust.velocity;
			dust.velocity.Y += .2f;
			return false;
		}
	}
}