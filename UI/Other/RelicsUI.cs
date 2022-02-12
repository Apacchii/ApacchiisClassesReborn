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

namespace ApacchiisClassesMod2.UI.Other
{
    class RelicsUI : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        int relicCount; // 15 relics per line
        bool updated = false;
        int n = 0;
        int line = 0;

        UIPanel background;
        UIText relics;
        UIPanel[] relicSlot;
        UIText[] relicSlotText;

        UIPanel close;
        UIText closeText;

        public override void OnInitialize()
        {
            relicCount = Player.GetModPlayer<ACMPlayer>().relicList.Count;
            relicSlot = new UIPanel[relicCount];
            relicSlotText = new UIText[relicCount];

            background = new UIPanel();
            background.VAlign = .5f;
            background.HAlign = .5f;
            background.Width.Set(555, 0f);
            background.Height.Set(700, 0f);
            background.BackgroundColor = new Color(75, 75, 75);
            background.BorderColor = new Color(25, 25, 25);
            Append(background);
          
            for(int i = 0; i < relicCount; i++)
            {
                relicSlot[i] = new UIPanel();
                if (i <= 15)
                {
                    relicSlot[i].Left.Set(7 + 35 * i, 0f);
                    relicSlot[i].Top.Set(5, 0f);
                }

                if (i >= 15 && i < 30)
                {
                    relicSlot[i].Left.Set(7 + 35 * i - 35 * 15/*<-*/, 0f);
                    relicSlot[i].Top.Set(5 + 40, 0f);
                }

                if (i >= 30 && i < 45)
                {
                    relicSlot[i].Left.Set(7 + 35 * i - 35 * 30/*<-*/, 0f);
                    relicSlot[i].Top.Set(5 + 80, 0f);
                }

                if (i >= 45 && i < 60)
                {
                    relicSlot[i].Left.Set(7 + 35 * i - 35 * 45/*<-*/, 0f);
                    relicSlot[i].Top.Set(5 + 120, 0f);
                }

                if (i >= 60 && i < 75)
                {
                    relicSlot[i].Left.Set(7 + 35 * i - 35 * 60/*<-*/, 0f);
                    relicSlot[i].Top.Set(5 + 160, 0f);
                }

                if (i >= 75 && i < 80)
                {
                    relicSlot[i].Left.Set(7 + 35 * i - 35 * 75/*<-*/, 0f);
                    relicSlot[i].Top.Set(5 + 200, 0f);
                }

                relicSlot[i].Width.Set(32, 0f);
                relicSlot[i].Height.Set(32, 0f);
                relicSlot[i].BackgroundColor = new Color(75, 75, 75);
                relicSlot[i].BorderColor = new Color(75, 75, 75);
                background.Append(relicSlot[i]);

                relicSlotText[i] = new UIText("");
                relicSlotText[i].VAlign = .5f;
                relicSlotText[i].HAlign = .5f;
                relicSlot[i].Append(relicSlotText[i]);
            }

            close = new UIPanel();
            close.VAlign = .5f;
            close.HAlign = .5f;
            close.Top.Set(-325, 0f);
            close.Left.Set(305, 0f);
            close.Width.Set(50, 0f);
            close.Height.Set(50, 0f);
            close.BackgroundColor = new Color(150, 75, 75);
            close.BorderColor = new Color(25, 25, 25);
            close.OnClick += Close;
            Append(close);

            closeText = new UIText("Close");
            closeText.VAlign = .5f;
            closeText.HAlign = .5f;
            closeText.OnClick += Close;
            close.Append(closeText);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if(close.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            relicCount = Player.GetModPlayer<ACMPlayer>().relicList.Count;

            if (!updated)
            {
                for(int i = 0; i < relicCount; i++)
                    relicSlotText[i].SetText($"     [i:{acmPlayer.relicList[i]}]");

                updated = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            for (int i = 0; i < relicCount; i++)
            {
                if (relicSlot[i].IsMouseHovering)
                {
                    Main.hoverItemName = $"{ItemLoader.GetItem(acmPlayer.relicList[i]).Item.HoverName}\n" +
                                         $"{ItemLoader.GetItem(acmPlayer.relicList[i]).Item.GetGlobalItem<ACMGlobalItem>().desc}";
                }
            }

            base.Draw(spriteBatch);
        }

        private void Close(UIMouseEvent evt, UIElement listeningElement)
        {
            GetInstance<ACM2ModSystem>()._RelicsUI.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
    }
}