using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace ApacchiisClassesMod2.UI.HUD
{
    class Bar : UIElement
    {
        public Color backgroundColor = Color.White;
        private static Texture2D _backgroundTexture;

        public Bar()
        {
            if (_backgroundTexture == null)
                _backgroundTexture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/Bar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            spriteBatch.Draw(_backgroundTexture, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
        }
    }
}