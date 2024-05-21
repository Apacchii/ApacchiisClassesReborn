using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using ApacchiisClassesMod2.Items.Relics;
using Terraria.ModLoader.UI.Elements;
using Terraria.DataStructures;
using Terraria.Localization;

namespace ApacchiisClassesMod2.UI.Other
{
    class Changelog : UIState
    {
        UIPanel changelogPanel;
        UIText changelogText;
        UIScrollbar changelogScrollbar;
        UIPanel closeButton;
        UIText closeButtonText;
        UIPanel backButton;
        UIText backButtonText;
        UIGrid textGrid;

        public override void OnInitialize()
        {
            changelogPanel = new UIPanel();
            changelogPanel.VAlign = .5f;
            changelogPanel.HAlign = .5f;
            changelogPanel.Width.Set(Main.screenWidth * .8f, 0f);
            changelogPanel.Height.Set(Main.screenHeight * .8f, 0f);
            changelogPanel.BackgroundColor = new Color(75, 75, 75);
            changelogPanel.BorderColor = new Color(25, 25, 25);
            Append(changelogPanel);

            changelogScrollbar = new UIScrollbar();
            changelogScrollbar.Height.Set(Main.screenHeight * .8f, 0f);
            changelogPanel.Append(changelogScrollbar);
            changelogPanel.Append(changelogScrollbar);

            textGrid = new UIGrid();
            //textGrid.VAlign = .5f;
            //textGrid.HAlign = .5f;
            textGrid.Width.Set(Main.screenWidth * .8f - 50, 0f);
            textGrid.Height.Set(Main.screenHeight * .8f - 30, 0f);
            textGrid.Left.Set(25, 0f);
            textGrid.Top.Set(15, 0f);
            textGrid.SetScrollbar(changelogScrollbar);
            changelogPanel.Append(textGrid);

            changelogText = new UIText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Changelog.Text")}", .9f);
            changelogText.IsWrapped = true;
            changelogText.Left.Set(10, 0f);
            changelogText.Top.Set(10, 0f);
            changelogText.Width.Set(Main.graphics.PreferredBackBufferWidth * .8f, 0f);
            changelogText.Height.Set(Main.graphics.PreferredBackBufferHeight * .8f, 0f);
            textGrid.Add(changelogText);

            closeButton = new UIPanel();
            closeButton.HAlign = .5f;
            closeButton.VAlign = .96f;
            closeButton.Left.Set(105, 0f);
            closeButton.Top.Set(0, 0f);
            closeButton.Width.Set(200, 0f);
            closeButton.Height.Set(50, 0f);
            closeButton.OnLeftClick += Close;
            closeButton.BackgroundColor = new Color(75, 75, 75);
            closeButton.BorderColor = new Color(25, 25, 25);
            Append(closeButton);

            closeButtonText = new UIText("Close");
            closeButtonText.HAlign = .5f;
            closeButtonText.VAlign = .5f;
            closeButton.Append(closeButtonText);

            backButton = new UIPanel();
            backButton.HAlign = .5f;
            backButton.VAlign = .96f;
            backButton.Left.Set(-105, 0f);
            backButton.Top.Set(0, 0f);
            backButton.Width.Set(200, 0f);
            backButton.Height.Set(50, 0f);
            backButton.OnLeftClick += Back;
            backButton.BackgroundColor = new Color(75, 75, 75);
            backButton.BorderColor = new Color(25, 25, 25);
            Append(backButton);

            backButtonText = new UIText("Back");
            backButtonText.HAlign = .5f;
            backButtonText.VAlign = .5f;
            backButton.Append(backButtonText);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (changelogPanel.IsMouseHovering || changelogText.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            if (closeButton.IsMouseHovering || closeButton.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                closeButton.BorderColor = Color.Yellow;
            }
            else { closeButton.BorderColor = new Color(25, 25, 25); }

            if (backButton.IsMouseHovering || backButton.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                backButton.BorderColor = Color.Yellow;
            }
            else { backButton.BorderColor = new Color(25, 25, 25); }

            if (changelogScrollbar.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            base.Update(gameTime);
        }

        private void Close(UIMouseEvent evt, UIElement listeningElement)
        {
            GetInstance<ACM2ModSystem>()._Changelog.SetState(null);
        }

        private void Back(UIMouseEvent evt, UIElement listeningElement)
        {
            GetInstance<ACM2ModSystem>()._Changelog.SetState(null);
            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(new ClassesMenu());
        }
    }
}