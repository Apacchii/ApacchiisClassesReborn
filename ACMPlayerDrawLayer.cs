using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2
{
	public class ACMPlayerDrawLayer : PlayerDrawLayer
    {
        public override bool IsHeadLayer => true;
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Head);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return base.GetDefaultVisibility(drawInfo);
        }

        

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow == 0)
            {
                //if(acmPlayer.commanderBannerBuffDuration > 0)
                //{
                //    Vector2 abovePlayer = new Vector2(player.position.X - Main.screenPosition.X + player.width * 0.5f, player.position.Y - Main.screenPosition.Y - player.height * .5f);
                //    Texture2D texture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/WarBanner", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                //
                //    drawInfo.DrawDataCache.Add(new DrawData(
                //    texture,
                //    abovePlayer,
                //    null,
                //    Color.White,
                //    0f,
                //    texture.Size() * 0.5f,
                //    1f,
                //    SpriteEffects.None,
                //    0
                //));

                if (drawInfo.drawPlayer.GetModPlayer<ACMPlayer>().Player.HasBuff<Buffs.Commander.WarBanner>())
                {
                    var position = drawInfo.Center + new Vector2(0f, 32f) - Main.screenPosition;
                    position = new Vector2((int)position.X, (int)position.Y); // To avoid quivering.
                    Texture2D texture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/WarBanner", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

                    drawInfo.DrawDataCache.Add(new DrawData(
                        texture,
                        position,
                        null, // Source rectangle.
                        Color.White,
                        0f, // Rotation.
                        texture.Size() * 0.5f,
                        1f, // Scale.
                        SpriteEffects.None,
                        0 // 'Layer'. This is always 0 in Terraria.
                    ));
                }

                if (drawInfo.drawPlayer.GetModPlayer<ACMPlayer>().Player.HasBuff<Buffs.Commander.Inspire>())
                {
                    var position = drawInfo.Center - Main.screenPosition;
                    position = new Vector2((int)position.X, (int)position.Y); // To avoid quivering.
                    Texture2D texture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/Circle3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    drawInfo.DrawDataCache.Add(new DrawData(
                        texture,
                        position,
                        null, // Source rectangle.
                        Color.DarkOrange,
                        MathHelper.ToRadians(45f), // Rotation.
                        texture.Size() * 0.5f,
                        .225f, // Scale.
                        SpriteEffects.None,
                        0
                    ));
                }

                if (drawInfo.drawPlayer.GetModPlayer<ACMPlayer>().Player.HasBuff<Buffs.Crusader.GuardianAngel>())
                {
                    var position = drawInfo.Center - Main.screenPosition;
                    position = new Vector2((int)position.X, (int)position.Y); // To avoid quivering.
                    Texture2D texture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/Circle1", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

                    drawInfo.DrawDataCache.Add(new DrawData(
                        texture,
                        position,
                        null, // Source rectangle.
                        Color.LightGoldenrodYellow,
                        0f, // Rotation.
                        texture.Size() * 0.5f,
                        .2f, // Scale.
                        SpriteEffects.None,
                        0
                    ));
                }

                
            }
         }
    }
}


