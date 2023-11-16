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

namespace ApacchiisClassesMod2.UI.Other
{
    class RelicsUIReworked : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        int relicCount;
        bool updated = false;

        UIPanel background;
        UIText relicListText;
        UIText craftableRelicListText;
        UIText relicsFoundText;

        UIGrid relicsUIGrid;
        UIScrollbar relicsUIScrollbar;
        UIGrid craftableRelicsUIGrid;
        UIScrollbar craftableRelicsUIScrollbar;

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
            background.Width.Set(650, 0f);
            background.Height.Set(500, 0f);
            background.BackgroundColor = new Color(75, 75, 75);
            background.BorderColor = new Color(25, 25, 25);
            Append(background);

            relicListText = new UIText("Relics", 1.25f);
            relicListText.HAlign = .5f;
            relicListText.VAlign = .5f;
            relicListText.Left.Set(-150, 0f);
            relicListText.Top.Set(-275, 0f);
            background.Append(relicListText);

            relicsFoundText = new UIText("[]", 1.25f);
            relicsFoundText.HAlign = .5f;
            relicsFoundText.VAlign = .5f;
            relicsFoundText.Top.Set(-275, 0f);
            background.Append(relicsFoundText);

            craftableRelicListText = new UIText("Craftable Relics", 1.25f);
            craftableRelicListText.HAlign = .5f;
            craftableRelicListText.VAlign = .5f;
            craftableRelicListText.Left.Set(150, 0f);
            craftableRelicListText.Top.Set(-275, 0f);
            background.Append(craftableRelicListText);

            relicsUIScrollbar = new UIScrollbar();
            relicsUIScrollbar.Height.Set(700, 0f);
            background.Append(relicsUIScrollbar);

            craftableRelicsUIScrollbar = new UIScrollbar();
            craftableRelicsUIScrollbar.Height.Set(700, 0f);
            craftableRelicsUIScrollbar.HAlign = .5f;
            background.Append(craftableRelicsUIScrollbar);

            relicsUIGrid = new UIGrid();
            relicsUIGrid.Left.Set(25, 0f);
            relicsUIGrid.Width.Set(300, 0f);
            relicsUIGrid.Height.Set(700, 0f);
            relicsUIGrid.SetScrollbar(relicsUIScrollbar);
            background.Append(relicsUIGrid);

            craftableRelicsUIGrid = new UIGrid();
            craftableRelicsUIGrid.Left.Set(330, 0f);
            craftableRelicsUIGrid.Width.Set(300, 0f);
            craftableRelicsUIGrid.Height.Set(700, 0f);
            craftableRelicsUIGrid.SetScrollbar(craftableRelicsUIScrollbar);
            background.Append(craftableRelicsUIGrid);

            for (int i = 0; i < relicCount; i++)
            {
                if (!ItemLoader.GetItem(Player.GetModPlayer<ACMPlayer>().relicList[i]).Item.GetGlobalItem<ACMGlobalItem>().isCraftableRelic)
                {
                    relicSlot[i] = new UIPanel();
                    relicSlot[i].Width.Set(50, 0f);
                    relicSlot[i].Height.Set(50, 0f);
                    relicSlot[i].BackgroundColor = new Color(75, 75, 75); //75
                    relicSlot[i].BorderColor = new Color(25, 25, 25); //25
                    relicsUIGrid.Add(relicSlot[i]);

                    relicSlotText[i] = new UIText("", 1.25f);
                    relicSlotText[i].VAlign = .5f;
                    relicSlotText[i].HAlign = .5f;
                    relicSlotText[i].Top.Set(-2, 0f);
                    relicSlot[i].Append(relicSlotText[i]);
                }
                else
                {
                    relicSlot[i] = new UIPanel();
                    relicSlot[i].Width.Set(50, 0f);
                    relicSlot[i].Height.Set(50, 0f);
                    relicSlot[i].BackgroundColor = new Color(75, 75, 75);
                    relicSlot[i].BorderColor = new Color(25, 25, 25);
                    craftableRelicsUIGrid.Add(relicSlot[i]);

                    relicSlotText[i] = new UIText("", 1.25f);
                    relicSlotText[i].VAlign = .5f;
                    relicSlotText[i].HAlign = 0f;
                    relicSlotText[i].Top.Set(-2, 0f);
                    relicSlot[i].Append(relicSlotText[i]);
                }

                close = new UIPanel();
                close.VAlign = .5f;
                close.HAlign = .5f;
                close.Top.Set(280, 0f);
                close.Width.Set(650, 0f);
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
                helpPanel.Top.Set(280, 0f);
                helpPanel.Left.Set(355, 0f);
                helpPanel.Width.Set(50, 0f);
                helpPanel.Height.Set(50, 0f);
                helpPanel.BackgroundColor = Color.Orange;
                helpPanel.BorderColor = Color.OrangeRed;
                Append(helpPanel);

                helpText = new UIText("?");
                helpText.VAlign = .5f;
                helpText.HAlign = .5f;
                helpPanel.Append(helpText);

                base.OnInitialize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if(close.IsMouseHovering || background.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            relicCount = acmPlayer.relicList.Count;

            if (!updated)
            {
                for(int i = 0; i < relicCount; i++)
                    if(relicSlotText[i] != null)
                    {
                        relicSlotText[i].SetText($"[i:{acmPlayer.relicList[i]}]");

                        if (acmPlayer.relicsFound.Contains(ItemLoader.GetItem(acmPlayer.relicList[i]).Item.type))
                            relicSlot[i].BorderColor = Color.DarkOrange;

                        relicsFoundText.SetText($"[{acmPlayer.relicsFound.Count}/{acmPlayer.relicList.Count}]");
                    }
                updated = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            //If we're hovering a specific relic's slot
            for (int i = 0; i < relicCount; i++)
            {
                if(relicSlot[i] != null)
                    if (relicSlot[i].IsMouseHovering)
                    {
                        //Display the relic's name
                        Main.hoverItemName += $"[c/e69d00:{ItemLoader.GetItem(acmPlayer.relicList[i]).Item.Name}]\n";

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