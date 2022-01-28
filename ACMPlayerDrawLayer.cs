//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.ID;
//using Terraria.ModLoader;
//
//namespace ApacchiisClassesMod2
//{
//	public class ACMPlayerDrawLayer : PlayerDrawLayer
//    {
//        Player player = Main.player[Main.myPlayer];
//        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Head);
//
//        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
//        {
//            if (player.GetModPlayer<ACMPlayer>().commanderBannerBuffDuration > 0)
//                return true;
//            else
//                return false;
//        }
//
//        protected override void Draw(ref PlayerDrawSet drawInfo)
//        {
//            if(Main.myPlayer == player.whoAmI)
//            {
//                Vector2 position = new Vector2(player.position.X - Main.screenPosition.X + player.width * 0.5f, player.position.Y - Main.screenPosition.Y - player.height * .5f - 24);
//                Texture2D texture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/WarBanner", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
//
//                drawInfo.DrawDataCache.Add(new DrawData(
//                texture,
//                position,
//                null,
//                Color.White,
//                0f,
//                texture.Size() * 0.5f,
//                1f,
//                SpriteEffects.None,
//                0
//            ));
//            }
//        }
//    }
//}

