//using System;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.UI;
//using Terraria.GameContent.UI.Elements;
//using static Terraria.ModLoader.ModContent;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria.Audio;
//using ApacchiisClassesMod2.Configs;
//
//namespace ApacchiisClassesMod2.UI.Specializations
//{
//    class MyDeck : UIState
//    {
//        Player Player = Main.player[Main.myPlayer];
//
//        int bgW = 690;
//        int bgH = 300;
//
//        int cardW = 64;
//        int cardH = 64;
//
//        bool updated = false;
//        int cardNumber = 11;
//
//        UIPanel Background;
//        UIImage[] CardImage;
//        UIPanel[] Card;
//        UIText[] Count;
//        UIPanel PanelCard;
//        UIText PanelCardName;
//        UIText PanelCardEffect1;
//        UIText PanelCardEffect2;
//
//        #region Card Icons
//        string baseIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Base";
//        string apIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/AbilityPower";
//        string cdrIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/CooldownReduction";
//        string hpIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Health";
//        string dodgeIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Dodge";
//        string asIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/AttackSpeed";
//
//        #endregion
//
//        public override void OnInitialize()
//        {
//            Background = new UIPanel();
//            Background.VAlign = .5f;
//            Background.HAlign = .5f;
//            Background.Width.Set(bgW, 0f);
//            Background.Height.Set(bgH, 0f);
//            Background.BackgroundColor = new Color(75, 75, 75);
//            Background.BorderColor = new Color(25, 25, 25);
//            Background.OnClick += Click;
//            Append(Background);
//
//            PanelCard = new UIPanel();
//            PanelCard.Left.Set(365, 0f);
//            PanelCard.Height.Set(270, 0f);
//            PanelCard.Width.Set(300, 0f);
//            PanelCard.Top.Set(5, 0f);
//            PanelCard.BackgroundColor = new Color(0, 0, 0);
//            PanelCard.BorderColor = new Color(255, 255, 255);
//            Background.Append(PanelCard);
//
//            PanelCardName = new UIText("");
//            PanelCardName.VAlign = .1f;
//            PanelCardName.HAlign = .5f;
//            PanelCard.Append(PanelCardName);
//
//            PanelCardEffect1 = new UIText("");
//            PanelCardEffect1.VAlign = .85f;
//            PanelCardEffect1.HAlign = .5f;
//            PanelCard.Append(PanelCardEffect1);
//
//            #region Card
//            Card = new UIPanel[cardNumber];
//            Count = new UIText[cardNumber];
//            CardImage = new UIImage[cardNumber];
//
//            for (int i = 0; i < cardNumber; i++)
//            {
//                Card[i] = new UIPanel();
//                if (i < 5)
//                {
//                    Card[i].Top.Set(5, 0f);
//                    Card[i].Left.Set(5 + i * 64, 0f);
//                }
//
//                if (i >= 5 && i < 10)
//                {
//                    Card[i].Top.Set(5 + 64, 0f);
//                    Card[i].Left.Set(5 + i * 64 - 64 * 5/*<-*/, 0f);
//                }
//
//                if (i >= 10 && i < 15)
//                {
//                    Card[i].Top.Set(5 + 128, 0f);
//                    Card[i].Left.Set(5 + i * 64 - 64 * 10/*<-*/, 0f);
//                }
//
//                Card[i].Width.Set(cardW, 0f);
//                Card[i].Height.Set(cardH, 0f);
//                Card[i].BackgroundColor = new Color(0, 0, 0);
//                Card[i].BorderColor = new Color(255, 255, 255);
//                Background.Append(Card[i]);
//
//
//                Count[i] = new UIText("", .6f);
//                Count[i].VAlign = 1f;
//                Count[i].HAlign = 1f;
//                Card[i].Append(Count[i]);
//
//
//                CardImage[i] = new UIImage(Request<Texture2D>(apIcon).Value);
//                CardImage[i].VAlign = .5f;
//                CardImage[i].HAlign = .5f;
//                Card[i].Append(CardImage[i]);
//            }
//            #endregion
//
//
//
//            base.OnInitialize();
//        }
//
//        public override void Update(GameTime gameTime)
//        {
//            Player Player = Main.player[Main.myPlayer];
//            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
//
//            //if (Card.IsMouseHovering)
//            //    Main.LocalPlayer.mouseInterface = true;
//
//            if (Player.controlInv)
//                GetInstance<ACM2ModSystem>()._MyDeck.SetState(null);
//
//            PanelCardName.SetText("");
//            PanelCardEffect1.SetText("");
//            //PanelCardEffect2.SetText("");
//
//            for (int i = 0; i < cardNumber; i++)
//            {
//                if (Card[i].IsMouseHovering)
//                {
//                    switch (i)
//                    {
//                        case 0:
//                            PanelCardName.SetText("Powerful");
//                            PanelCardEffect1.SetText($" +{(decimal)(acmPlayer.card_PowerfulCount * acmPlayer.card_PowerfulValue * 100)}% Ability Power");
//                            break;
//
//                        case 1:
//                            PanelCardName.SetText("Timeless");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_TimelessValue * 100)}% Cooldown Reduction");
//                            break;
//
//                        case 2:
//                            PanelCardName.SetText("Healthy");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_HealthyValue * 100)}% Max Health");
//                            break;
//
//                        case 3:
//                            PanelCardName.SetText("Sneaky");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_SneakyValue * 100)}% Dodge Chance");
//                            break;
//
//                        case 4:
//                            PanelCardName.SetText("Nimble Hands");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_NimbleHandsValue * 100)}% Attack Speed");
//                            break;
//
//                        case 5:
//                            PanelCardName.SetText("Carry");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_CarryValue * 100)}% Damage");
//                            break;
//
//                        case 6:
//                            PanelCardName.SetText("Deadeye");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_DeadeyeValue * 100)}% Crit Damage");
//                            break;
//
//                        case 7:
//                            PanelCardName.SetText("Mending");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_MendingValue * 100)}% Heal Power");
//                            break;
//
//                        case 8:
//                            PanelCardName.SetText("Mighty");
//                            PanelCardEffect1.SetText($"-{(decimal)(acmPlayer.card_MightyValue * 100)}% Ultimate Cost");
//                            break;
//
//                        case 9:
//                            PanelCardName.SetText("Impenetrable");
//                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_ImpenetrableValue * 100)}% Defense");
//                            break;
//
//                        case 10:
//                            PanelCardName.SetText("Magical");
//                            PanelCardEffect1.SetText($"+{(decimal)acmPlayer.card_MagicalValue * 100}% Max Mana");
//                            break;
//                    }
//                }
//
//                switch (i)
//                {
//                    case 0:
//                        Count[i].SetText($"x{acmPlayer.card_PowerfulCount}");
//                        break;
//
//                    case 1:
//                        Count[i].SetText($"x{acmPlayer.card_TimelessCount}");
//                        break;
//
//                    case 2:
//                        Count[i].SetText($"x{acmPlayer.card_HealthyCount}");
//                        break;
//
//                    case 3:
//                        Count[i].SetText($"x{acmPlayer.card_SneakyCount}");
//                        break;
//
//                    case 4:
//                        Count[i].SetText($"x{acmPlayer.card_NimbleHandsCount}");
//                        break;
//
//                    case 5:
//                        Count[i].SetText($"x{acmPlayer.card_CarryCount}");
//                        break;
//
//                    case 6:
//                        Count[i].SetText($"x{acmPlayer.card_DeadeyeCount}");
//                        break;
//
//                    case 7:
//                        Count[i].SetText($"x{acmPlayer.card_MendingCount}");
//                        break;
//
//                    case 8:
//                        Count[i].SetText($"x{acmPlayer.card_MightyCount}");
//                        break;
//
//                    case 9:
//                        Count[i].SetText($"x{acmPlayer.card_ImpenetrableCount}");
//                        break;
//
//                    case 10:
//                        Count[i].SetText($"x{acmPlayer.card_MagicalCount}");
//                        break;
//                }
//            }
//
//            if (!updated)
//            {
//                
//                //icons
//
//                updated = true;
//            }
//
//            base.Update(gameTime);
//        }
//
//        private void Click(UIMouseEvent evt, UIElement listeningElement)
//        {
//            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
//
//            GetInstance<ACM2ModSystem>()._MyDeck.SetState(null);
//        }
//    }
//}