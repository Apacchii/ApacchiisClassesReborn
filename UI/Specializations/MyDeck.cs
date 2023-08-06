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
using ApacchiisClassesMod2.Configs;

namespace ApacchiisClassesMod2.UI.Specializations
{
    class MyDeck : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        bool tick = false;

        int bgW = 690;
        int bgH = 300;

        int cardW = 64;
        int cardH = 64;

        bool updated = false;
        int cardNumber = 20;

        UIPanel Background;
        UIPanel DrawCard;
        UIText DrawCardText;
        UIText Tip;

        UIImage[] Card;
        UIText[] Count;
        UIPanel PanelCard;
        UIImage PanelCardImage;
        UIText PanelCardName;
        UIText PanelCardEffect1;
        UIText PanelCardEffect2;
        UIText PanelCardCount;

        #region Card Icons
        string baseIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Base";
        string apIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/AbilityPower";
        string cdrIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/CooldownReduction";
        string hpIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Health";
        string dodgeIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Dodge";
        string asIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/AttackSpeed";
        string dmgIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Damage";
        string cdmgIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/CritDamage";
        string ucdrIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/UltCost";
        string magicalIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Magical";
        string defIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Defense";
        string healIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Healing";
        string fortifiedIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Fortified";
        string masterfulIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Masterful";
        string sogIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/SparkOfGenius";
        string prowlerIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Prowler";
        string veteranIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Veteran";
        string healerIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Healer";
        string mischievousIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Mischievous";
        string seerIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Seer";
        string ferociousIcon = "ApacchiisClassesMod2/UI/Specializations/Cards/Ferocious";
        #endregion

        public override void OnInitialize()
        {
            Background = new UIPanel();
            Background.VAlign = .5f;
            Background.HAlign = .5f;
            Background.Width.Set(bgW, 0f);
            Background.Height.Set(bgH, 0f);
            Background.BackgroundColor = new Color(0, 0, 0);
            Background.BorderColor = new Color(255, 255, 255);
            Background.OnLeftClick += Click;
            Append(Background);

            Tip = new UIText("Clicking anywhere on this menu closes it", .9f);
            Tip.Top.Set(-32, 0f);
            Tip.HAlign = .5f;
            Background.Append(Tip);

            DrawCard = new UIPanel();
            DrawCard.VAlign = .5f;
            DrawCard.HAlign = .5f;
            DrawCard.Top.Set(bgH / 2 + 15, 0f);
            DrawCard.Width.Set(bgW, 0f);
            DrawCard.Height.Set(35, 0f);
            DrawCard.BackgroundColor = new Color(0, 0, 0);
            DrawCard.BorderColor = new Color(255, 255, 255);
            DrawCard.OnLeftClick += ClickDrawCard;
            Append(DrawCard);

            DrawCardText = new UIText("Draw Rune");
            DrawCardText.VAlign = .5f;
            DrawCardText.HAlign = .5f;
            DrawCard.Append(DrawCardText);

            PanelCard = new UIPanel();
            PanelCard.Left.Set(365, 0f);
            PanelCard.Height.Set(265, 0f);
            PanelCard.Width.Set(295, 0f);
            PanelCard.Top.Set(5, 0f);
            PanelCard.BackgroundColor = new Color(0, 0, 0);
            PanelCard.BorderColor = new Color(255, 255, 255);
            Background.Append(PanelCard);

            PanelCardName = new UIText("");
            PanelCardName.VAlign = .1f;
            PanelCardName.HAlign = .5f;
            PanelCard.Append(PanelCardName);

            PanelCardImage = new UIImage(Request<Texture2D>(baseIcon).Value);
            PanelCardImage.VAlign = .5f;
            PanelCardImage.HAlign = .5f;

            PanelCardEffect1 = new UIText("");
            PanelCardEffect1.VAlign = .85f;
            PanelCardEffect1.HAlign = .5f;
            PanelCard.Append(PanelCardEffect1);

            PanelCardEffect2 = new UIText("");
            PanelCardEffect2.VAlign = .95f;
            PanelCardEffect2.HAlign = .5f;
            PanelCard.Append(PanelCardEffect2);

            PanelCardCount = new UIText("");
            PanelCard.Top.Set(10, 0f);
            PanelCardCount.Left.Set(10, 0f);
            PanelCard.Append(PanelCardCount);

            #region Card
            Card = new UIImage[cardNumber];
            Count = new UIText[cardNumber];

            for (int i = 0; i < cardNumber; i++)
            {
                Card[i] = new UIImage(Request<Texture2D>(baseIcon).Value);
                if (i < 5)
                {
                    Card[i].Top.Set(5, 0f);
                    Card[i].Left.Set(5 + i * 64, 0f);
                }

                if (i >= 5 && i < 10)
                {
                    Card[i].Top.Set(5 + 64, 0f);
                    Card[i].Left.Set(5 + i * 64 - 64 * 5/*<-*/, 0f);
                }

                if (i >= 10 && i < 15)
                {
                    Card[i].Top.Set(5 + 128, 0f);
                    Card[i].Left.Set(5 + i * 64 - 64 * 10/*<-*/, 0f);
                }

                if (i >= 15 && i < 20)
                {
                    Card[i].Top.Set(5 + 192, 0f);
                    Card[i].Left.Set(5 + i * 64 - 64 * 15/*<-*/, 0f);
                }

                Card[i].Width.Set(cardW, 0f);
                Card[i].Height.Set(cardH, 0f);

                Background.Append(Card[i]);


                Count[i] = new UIText("", .7f);
                Count[i].VAlign = 1.01f;
                Count[i].HAlign = 1.01f;
                Card[i].Append(Count[i]);
            }
            #endregion



            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if (DrawCard.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            if (DrawCard.IsMouseHovering)
            {
                if (!tick)
                {
                    tick = true;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
                DrawCardText.TextColor = Color.Yellow;
            }
            else
            {
                tick = false;
                DrawCardText.TextColor = Color.White;
            }

            if (Player.controlInv)
                GetInstance<ACM2ModSystem>()._MyDeck.SetState(null);

            DrawCardText.SetText($"Draw rune ({acmPlayer.cardsPoints})");
            PanelCardName.SetText("");
            PanelCardImage.SetImage(Request<Texture2D>(baseIcon).Value);
            PanelCardEffect1.SetText("");
            PanelCardEffect2.SetText("");
            PanelCardCount.SetText("");
            PanelCardImage.Remove();

            for (int i = 0; i < cardNumber; i++)
            {
                if (Card[i].IsMouseHovering)
                {
                    switch (i)
                    {
                        case 0:
                            PanelCardName.SetText("Powerful");
                            PanelCardEffect1.SetText($" +{(decimal)(acmPlayer.card_PowerfulCount * acmPlayer.card_PowerfulValue * 100)}% Ability Power");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(apIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_PowerfulCount}");
                            break;

                        case 1:
                            PanelCardName.SetText("Timeless");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_TimelessCount * acmPlayer.card_TimelessValue * 100)}% Cooldown Reduction");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(cdrIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_TimelessCount}");
                            break;

                        case 2:
                            PanelCardName.SetText("Healthy");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_HealthyCount * acmPlayer.card_HealthyValue * 100)}% Max Health");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(hpIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_HealthyCount}");
                            break;

                        case 3:
                            PanelCardName.SetText("Sneaky");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_SneakyCount * acmPlayer.card_SneakyValue * 100)}% Dodge Chance");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(dodgeIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_SneakyCount}");
                            break;

                        case 4:
                            PanelCardName.SetText("Nimble Hands");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_NimbleHandsCount * acmPlayer.card_NimbleHandsValue * 100)}% Attack Speed");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(asIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_NimbleHandsCount}");
                            break;

                        case 5:
                            PanelCardName.SetText("Carry");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_CarryCount * acmPlayer.card_CarryValue * 100)}% Damage");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(dmgIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_CarryCount}");
                            break;

                        case 6:
                            PanelCardName.SetText("Deadeye");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_DeadeyeCount * acmPlayer.card_DeadeyeValue * 100)}% Crit Damage");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(cdmgIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_DeadeyeCount}");
                            break;

                        case 7:
                            PanelCardName.SetText("Mending");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_MendingCount * acmPlayer.card_MendingValue * 100)}% Heal Power");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(healIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_MendingCount}");
                            break;

                        case 8:
                            PanelCardName.SetText("Mighty");
                            PanelCardEffect1.SetText($"-{(decimal)(acmPlayer.card_MightyCount * acmPlayer.card_MightyValue * 100)}% Ultimate Cost");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(ucdrIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_MightyCount}");
                            break;

                        case 9:
                            PanelCardName.SetText("Impenetrable");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_ImpenetrableCount * acmPlayer.card_ImpenetrableValue * 100)}% Damage Reduction");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(defIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_ImpenetrableCount}");
                            break;

                        case 10:
                            PanelCardName.SetText("Prodigy");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_MagicalCount * acmPlayer.card_MagicalValue * 100)}% Class Banner Stats");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(magicalIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_MagicalCount}");
                            break;

                        case 11:
                            PanelCardName.SetText("Fortified");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_FortifiedCount * acmPlayer.card_FortifiedValue_1 * 100)}% Max Health");
                            PanelCardEffect2.SetText($"+{(decimal)(acmPlayer.card_FortifiedCount * acmPlayer.card_FortifiedValue_2 * 100)}% Damage Reduction");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(fortifiedIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_FortifiedCount}");
                            break;

                        case 12:
                            PanelCardName.SetText("Masterful");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_MasterfulCount * acmPlayer.card_MasterfulValue_1 * 100)}% Ability Power");
                            PanelCardEffect2.SetText($"+{(decimal)(acmPlayer.card_MasterfulCount * acmPlayer.card_MasterfulValue_2 * 100)}% Heal Power");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(masterfulIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_MasterfulCount}");
                            break;

                        case 13:
                            PanelCardName.SetText("Spark Of Genius");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_SparkOfGeniusCount * acmPlayer.card_SparkOfGeniusValue_1 * 100)}% Cooldown Reduction");
                            PanelCardEffect2.SetText($"-{(decimal)(acmPlayer.card_SparkOfGeniusCount * acmPlayer.card_SparkOfGeniusValue_2 * 100)}% Ultimate Cost");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(sogIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_SparkOfGeniusCount}");
                            break;

                        case 14:
                            PanelCardName.SetText("Prowler");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_ProwlerCount * acmPlayer.card_ProwlerValue_1 * 100)}% Damage");
                            PanelCardEffect2.SetText($"+{(decimal)(acmPlayer.card_ProwlerCount * acmPlayer.card_ProwlerValue_2 * 100)}% Crit Damage");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(prowlerIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_ProwlerCount}");
                            break;

                        case 15:
                            PanelCardName.SetText("Veteran");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_VeteranCount * acmPlayer.card_VeteranValue_1 * 100)}% Max Health");
                            PanelCardEffect2.SetText($"+{(decimal)(acmPlayer.card_VeteranCount * acmPlayer.card_VeteranValue_2 * 100)}% Class Banner Stats");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(veteranIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_VeteranCount}");
                            break;

                        case 16:
                            PanelCardName.SetText("Healer");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_HealerCount * acmPlayer.card_HealerValue_1 * 100)}% Heal Power");
                            PanelCardEffect2.SetText($"+{(decimal)(acmPlayer.card_HealerCount * acmPlayer.card_HealerValue_2 * 100)}% Cooldown Reduction");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(healerIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_HealerCount}");
                            break;

                        case 17:
                            PanelCardName.SetText("Mischievous");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_MischievousCount * acmPlayer.card_MischievousValue_1 * 100)}% Crit Damage");
                            PanelCardEffect2.SetText($"+{(decimal)(acmPlayer.card_MischievousCount * acmPlayer.card_MischievousValue_2 * 100)}% Dodge Chance");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(mischievousIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_MischievousCount}");
                            break;

                        case 18:
                            PanelCardName.SetText("Seer");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_SeerCount * acmPlayer.card_SeerValue_1 * 100)}% Ability Power");
                            PanelCardEffect2.SetText($"-{(decimal)(acmPlayer.card_SeerCount * acmPlayer.card_SeerValue_2 * 100)}% Ultimate Cost");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(seerIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_SeerCount}");
                            break;

                        case 19:
                            PanelCardName.SetText("Ferocious");
                            PanelCardEffect1.SetText($"+{(decimal)(acmPlayer.card_FerociousCount * acmPlayer.card_FerociousValue_1 * 100)}% Damage");
                            PanelCardEffect2.SetText($"+{(decimal)(acmPlayer.card_FerociousCount * acmPlayer.card_FerociousValue_2 * 100)}% Attack Speed");
                            PanelCard.Append(PanelCardImage);
                            PanelCardImage.SetImage(Request<Texture2D>(ferociousIcon).Value);
                            PanelCardCount.SetText($"Owned: {acmPlayer.card_FerociousCount}");
                            break;
                    }
                }

                switch (i)
                {
                    case 0:
                        Count[i].SetText($"{acmPlayer.card_PowerfulCount}");
                        Card[i].SetImage(Request<Texture2D>(apIcon).Value);
                        break;

                    case 1:
                        Count[i].SetText($"{acmPlayer.card_TimelessCount}");
                        Card[i].SetImage(Request<Texture2D>(cdrIcon).Value);
                        break;

                    case 2:
                        Count[i].SetText($"{acmPlayer.card_HealthyCount}");
                        Card[i].SetImage(Request<Texture2D>(hpIcon).Value);
                        break;

                    case 3:
                        Count[i].SetText($"{acmPlayer.card_SneakyCount}");
                        Card[i].SetImage(Request<Texture2D>(dodgeIcon).Value);
                        break;

                    case 4:
                        Count[i].SetText($"{acmPlayer.card_NimbleHandsCount}");
                        Card[i].SetImage(Request<Texture2D>(asIcon).Value);
                        break;

                    case 5:
                        Count[i].SetText($"{acmPlayer.card_CarryCount}");
                        Card[i].SetImage(Request<Texture2D>(dmgIcon).Value);
                        break;

                    case 6:
                        Count[i].SetText($"{acmPlayer.card_DeadeyeCount}");
                        Card[i].SetImage(Request<Texture2D>(cdmgIcon).Value);
                        break;

                    case 7:
                        Count[i].SetText($"{acmPlayer.card_MendingCount}");
                        Card[i].SetImage(Request<Texture2D>(healIcon).Value);
                        break;

                    case 8:
                        Count[i].SetText($"{acmPlayer.card_MightyCount}");
                        Card[i].SetImage(Request<Texture2D>(ucdrIcon).Value);
                        break;

                    case 9:
                        Count[i].SetText($"{acmPlayer.card_ImpenetrableCount}");
                        Card[i].SetImage(Request<Texture2D>(defIcon).Value);
                        break;

                    case 10:
                        Count[i].SetText($"{acmPlayer.card_MagicalCount}");
                        Card[i].SetImage(Request<Texture2D>(magicalIcon).Value);
                        break;

                    case 11:
                        Count[i].SetText($"{acmPlayer.card_FortifiedCount}");
                        Card[i].SetImage(Request<Texture2D>(fortifiedIcon).Value);
                        break;

                    case 12:
                        Count[i].SetText($"{acmPlayer.card_MasterfulCount}");
                        Card[i].SetImage(Request<Texture2D>(masterfulIcon).Value);
                        break;

                    case 13:
                        Count[i].SetText($"{acmPlayer.card_SparkOfGeniusCount}");
                        Card[i].SetImage(Request<Texture2D>(sogIcon).Value);
                        break;

                    case 14:
                        Count[i].SetText($"{acmPlayer.card_ProwlerCount}");
                        Card[i].SetImage(Request<Texture2D>(prowlerIcon).Value);
                        break;

                    case 15:
                        Count[i].SetText($"{acmPlayer.card_VeteranCount}");
                        Card[i].SetImage(Request<Texture2D>(veteranIcon).Value);
                        break;

                    case 16:
                        Count[i].SetText($"{acmPlayer.card_HealerCount}");
                        Card[i].SetImage(Request<Texture2D>(healerIcon).Value);
                        break;

                    case 17:
                        Count[i].SetText($"{acmPlayer.card_MischievousCount}");
                        Card[i].SetImage(Request<Texture2D>(mischievousIcon).Value);
                        break;

                    case 18:
                        Count[i].SetText($"{acmPlayer.card_SeerCount}");
                        Card[i].SetImage(Request<Texture2D>(seerIcon).Value);
                        break;

                    case 19:
                        Count[i].SetText($"{acmPlayer.card_FerociousCount}");
                        Card[i].SetImage(Request<Texture2D>(ferociousIcon).Value);
                        break;
                }
            }

            base.Update(gameTime);
        }

        private void Click(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            GetInstance<ACM2ModSystem>()._MyDeck.SetState(null);
        }

        private void ClickDrawCard(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if(acmPlayer.cardsPoints > 0)
            {
                acmPlayer.cardsPoints--;
                GetInstance<ACM2ModSystem>()._Cards.SetState(new GeneralCards());
                GetInstance<ACM2ModSystem>()._MyDeck.SetState(null);
            }
        }
    }
}