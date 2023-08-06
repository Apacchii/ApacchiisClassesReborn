using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ApacchiisClassesMod2.Tiles.Ores
{
	public class LostRuneFragment : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileOreFinderPriority[Type] = 645; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
			//Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1500;
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Lost Rune Fragment");
			AddMapEntry(new Color(142, 52, 162), name);

			DustType = 84;
			RegisterItemDrop(ModContent.ItemType<Items.LostRuneFragment>());
			HitSound = SoundID.Tink;
		}
	}

	public class OreSystem : ModSystem
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
			// Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.
	
			// The first step is an Ore. Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
			// First, we find out which step "Shinies" is.
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
	
			if (ShiniesIndex != -1)
			{
				// Next, we insert our pass directly after the original "Shinies" pass.
				// ExampleOrePass is a class seen bellow
				tasks.Insert(ShiniesIndex + 1, new OrePass("Generating Lost Rune Fragments", 237.4298f));
			}
		}
	}

	public class OrePass : GenPass
	{
		public OrePass(string name, float loadWeight) : base(name, loadWeight)
		{
		}
	
		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			progress.Message = "Generating Lost Rune Fragments";

			for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * .0002f); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY);
				WorldGen.TileRunner(x, y, 2, 5, ModContent.TileType<LostRuneFragment>());
			}
		}
	}
}