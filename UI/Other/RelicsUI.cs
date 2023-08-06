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

        UIPanel background;
        UIPanel[] relicSlot;
        UIText[] relicSlotText;

        UIPanel close;
        UIText closeText;

        UIPanel helpPanel;
        UIText helpText;

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

                if (i >= 75 && i < 90)
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
            close.OnLeftClick += Close;
            Append(close);

            closeText = new UIText("Close");
            closeText.VAlign = .5f;
            closeText.HAlign = .5f;
            closeText.OnLeftClick += Close;
            close.Append(closeText);

            helpPanel = new UIPanel();
            helpPanel.VAlign = .5f;
            helpPanel.HAlign = .5f;
            helpPanel.Top.Set(-270, 0f);
            helpPanel.Left.Set(305, 0f);
            helpPanel.Width.Set(50, 0f);
            helpPanel.Height.Set(50, 0f);
            helpPanel.BackgroundColor = Color.Blue;
            helpPanel.BorderColor = Color.SkyBlue;
            Append(helpPanel);

            helpText = new UIText("?");
            helpText.VAlign = .5f;
            helpText.HAlign = .5f;
            helpPanel.Append(helpText);

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
                    relicSlotText[i].SetText($"[i:{acmPlayer.relicList[i]}]");

                updated = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            //If we're hovering a specific relic's slot
            for (int i = 0; i < relicCount; i++)
            {
                if (relicSlot[i].IsMouseHovering)
                {
                    //Display the relic's name
                    Main.hoverItemName += $"[c/e69d00:{ItemLoader.GetItem(acmPlayer.relicList[i]).Item.HoverName}]\n";

                    //Display if they're a craftable only relic
                    if (ItemLoader.GetItem(acmPlayer.relicList[i]).Item.GetGlobalItem<ACMGlobalItem>().isCraftableRelic)
                        Main.hoverItemName += "[c/a16ce6:[Craftable Relic][c/a16ce6:]]\n";

                    //Or if they're a material relic, used to craft into other relics
                    if (ItemLoader.GetItem(acmPlayer.relicList[i]).Item.GetGlobalItem<ACMGlobalItem>().isMaterialRelic)
                        Main.hoverItemName += "[c/a16ce6:[Material Relic][c/a16ce6:]]\n";

                    //Display the relic's description
                    Main.hoverItemName += $"{ItemLoader.GetItem(acmPlayer.relicList[i]).Item.GetGlobalItem<ACMGlobalItem>().desc}";
                }
            }

            if (helpPanel.IsMouseHovering || helpText.IsMouseHovering)
                Main.hoverItemName = "Relics are items dropped from [c/a16ce6:Random Relics], which randomly drop from all enemies\n" +
                                     "These relics are equipable on the slot right next to the your class' banner accessory slot\n" +
                                     "All relics have the same chance to drop and only one can be equipped at a time\n" +
                                     "[c/a16ce6:Craftable Relics] [c/f33333:cannot] be obtained through [c/a16ce6:Random Relics], they must be crafted using [c/a16ce6:Material Relics]";

            base.Draw(spriteBatch);
        }

        private void Close(UIMouseEvent evt, UIElement listeningElement)
        {
            GetInstance<ACM2ModSystem>()._RelicsUI.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
    }
}