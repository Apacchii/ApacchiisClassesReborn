using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Tiles
{
	public class ACMGlobalTile : GlobalTile
	{
        Player Player = Main.player[Main.myPlayer];

        //public override bool Drop(int i, int j, int type)
        //{
        //    //var tile = Main.tile[i, j];
        //    if (type == TileID.Pots && Main.rand.NextFloat() < .5f)
        //        Item.NewItem(new Vector2(i, j), ModContent.ItemType<Items.LostRelic>());
        //
        //    return base.Drop(i, j, type);
        //}
        //
        //public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        //{
        //    
        //
        //    base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
        //}

        //public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        //{
        //    
        //    //return base.Drop(i, j, type);
        //}
        //
        //public override bool Drop(int i, int j, int type)
        //{
        //    if (type != TileID.Pots)
        //        return base.Drop(i, j, type);
        //
        //    if ((Main.tile[i, j].TileFrameX % 36) == 0 && (Main.tile[i, j].TileFrameY % 36) == 0)
        //    {
        //        Player.QuickSpawnItemDirect(ModContent.ItemType<Items.LostRelic>(), 1);
        //        //Item.NewItem(new Vector2(i, j), ModContent.ItemType<Items.LostRelic>());
        //        Main.NewText($"Pot broken ({i}, {j})");
        //    }
        //    return base.Drop(i, j, type);
        //}
    }
}