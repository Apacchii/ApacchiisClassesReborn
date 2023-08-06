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
    class GeneralCards : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        bool updated = false;
        bool updatedImage = false;
        int cardSlot = -32451; //random

        int cardW = 230;
        int cardH = 400;

        int bgW = 755;
        int bgH = 300;

        Color doubledColor = Color.PaleVioletRed;

        UIPanel Background;
        UIPanel Card1;
        UIImage _CardIcon1;
        UIPanel Card2;
        UIImage _CardIcon2;
        UIPanel Card3;
        UIImage _CardIcon3;
        UIText tip;

        #region Cards
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

        UIText CardName1;
        UIText CardEffect1;
        UIText CardEffect1_2;
        string CardIcon1;
        int Card1_Type;
        bool isDouble_1 = false;
        UIText doubleText1;
        UIText count_1;

        UIText CardName2;
        UIText CardEffect2;
        UIText CardEffect2_2;
        string CardIcon2;
        int Card2_Type;
        bool isDouble_2 = false;
        UIText doubleText2;
        UIText count_2;

        UIText CardName3;
        UIText CardEffect3;
        UIText CardEffect3_2;
        string CardIcon3;
        int Card3_Type;
        bool isDouble_3 = false;
        UIText doubleText3;
        UIText count_3;

        public override void OnInitialize()
        {
            Background = new UIPanel();
            Background.VAlign = .5f;
            Background.HAlign = .5f;
            Background.Width.Set(bgW, 0f);
            Background.Height.Set(bgH, 0f);
            Background.BackgroundColor = new Color(0, 0, 0);
            Background.BorderColor = new Color(255, 255, 255);
            Append(Background);

            tip = new UIText("Select one rune to add to your collection"); //This rune is doubled and will grant you 2 copy of itself
            tip.HAlign = .5f;
            tip.Top.Set(-35, 0f);
            Background.Append(tip);

            #region Card 1
            Card1 = new UIPanel();
            Card1.Width.Set(cardW, 0f);
            Card1.Height.Set(cardH, 0f);
            Card1.VAlign = .5f;
            Card1.Left.Set(5, 0f);
            Card1.BackgroundColor = new Color(0, 0, 0);
            Card1.BorderColor = new Color(255, 255, 255);
            Card1.OnLeftClick += Click;
            Background.Append(Card1);

            CardName1 = new UIText("", 1.05f);
            CardName1.VAlign = .1f;
            CardName1.HAlign = .5f;
            Card1.Append(CardName1);

            CardEffect1 = new UIText("", .85f);
            CardEffect1.VAlign = .85f;
            CardEffect1.HAlign = .5f;
            Card1.Append(CardEffect1);

            CardEffect1_2 = new UIText("", .85f);
            CardEffect1_2.VAlign = .95f;
            CardEffect1_2.HAlign = .5f;
            Card1.Append(CardEffect1_2);

            doubleText1 = new UIText("doubled", .7f);
            doubleText1.VAlign = .5f;
            doubleText1.HAlign = .5f;
            doubleText1.Top.Set(18, 0f);
            doubleText1.TextColor = doubledColor;

            count_1 = new UIText("x", .75f);
            count_1.Top.Set(0, 0f);
            count_1.Left.Set(cardW - 40, 0f);
            Card1.Append(count_1);

            _CardIcon1 = new UIImage(Request<Texture2D>(baseIcon).Value);
            _CardIcon1.VAlign = .5f;
            _CardIcon1.HAlign = .5f;
            Card1.Append(_CardIcon1);
            #endregion

            #region Card 2
            Card2 = new UIPanel();
            Card2.Width.Set(cardW, 0f);
            Card2.Height.Set(cardH, 0f);
            Card2.VAlign = .5f;
            Card2.Left.Set(250, 0f);
            Card2.BackgroundColor = new Color(0, 0, 0);
            Card2.BorderColor = new Color(255, 255, 255);
            Card2.OnLeftClick += Click;
            Background.Append(Card2);

            CardName2 = new UIText("", 1.05f);
            CardName2.VAlign = .1f;
            CardName2.HAlign = .5f;
            Card2.Append(CardName2);

            CardEffect2 = new UIText("", .85f);
            CardEffect2.VAlign = .85f;
            CardEffect2.HAlign = .5f;
            Card2.Append(CardEffect2);

            CardEffect2_2 = new UIText("", .85f);
            CardEffect2_2.VAlign = .95f;
            CardEffect2_2.HAlign = .5f;
            Card2.Append(CardEffect2_2);

            doubleText2 = new UIText("doubled", .7f);
            doubleText2.VAlign = .5f;
            doubleText2.HAlign = .5f;
            doubleText2.Top.Set(18, 0f);
            doubleText2.TextColor = doubledColor;

            count_2 = new UIText("x", .75f);
            count_2.Top.Set(0, 0f);
            count_2.Left.Set(cardW - 40, 0f);
            Card2.Append(count_2);

            _CardIcon2 = new UIImage(Request<Texture2D>(baseIcon).Value);
            _CardIcon2.VAlign = .5f;
            _CardIcon2.HAlign = .5f;
            Card2.Append(_CardIcon2);
            #endregion

            #region Card 3
            Card3 = new UIPanel();
            Card3.Width.Set(cardW, 0f);
            Card3.Height.Set(cardH, 0f);
            Card3.VAlign = .5f;
            Card3.Left.Set(495, 0f);
            Card3.BackgroundColor = new Color(0, 0, 0);
            Card3.BorderColor = new Color(255, 255, 255);
            Card3.OnLeftClick += Click;
            Background.Append(Card3);

            CardName3 = new UIText("", 1.05f);
            CardName3.VAlign = .1f;
            CardName3.HAlign = .5f;
            Card3.Append(CardName3);

            CardEffect3 = new UIText("", .85f);
            CardEffect3.VAlign = .85f;
            CardEffect3.HAlign = .5f;
            Card3.Append(CardEffect3);

            CardEffect3_2 = new UIText("", .85f);
            CardEffect3_2.VAlign = .95f;
            CardEffect3_2.HAlign = .5f;
            Card3.Append(CardEffect3_2);

            doubleText3 = new UIText("doubled", .7f);
            doubleText3.VAlign = .5f;
            doubleText3.HAlign = .5f;
            doubleText3.Top.Set(18, 0f);
            doubleText3.TextColor = doubledColor;

            count_3 = new UIText("x", .75f);
            count_3.Top.Set(0, 0f);
            count_3.Left.Set(cardW - 40, 0f);
            Card3.Append(count_3);

            _CardIcon3 = new UIImage(Request<Texture2D>(baseIcon).Value);
            _CardIcon3.VAlign = .5f;
            _CardIcon3.HAlign = .5f;
            Card3.Append(_CardIcon3);
            #endregion


            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if (Card1.IsMouseHovering || Card2.IsMouseHovering || Card3.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            if (!updated)
            {
                int cards = 20;
                Card1_Type = Main.rand.Next(cards);
                Card2_Type = Main.rand.Next(cards);
                Card3_Type = Main.rand.Next(cards);

                if (Card2_Type == Card1_Type) Card2_Type++;
                if (Card3_Type == Card1_Type) Card3_Type++;
                if (Card3_Type == Card2_Type) Card3_Type++;

                if (Card1_Type > 10) Card1_Type = Main.rand.Next(cards);
                if (Card2_Type > 10) Card2_Type = Main.rand.Next(cards);
                if (Card3_Type > 10) Card3_Type = Main.rand.Next(cards);

                if (Card2_Type == Card1_Type) Card2_Type++;
                if (Card3_Type == Card1_Type) Card3_Type++;
                if (Card3_Type == Card2_Type) Card3_Type++;

                float doubledChance = .15f;
                if (Main.rand.NextFloat() < doubledChance) isDouble_1 = true;
                if (Main.rand.NextFloat() < doubledChance) isDouble_2 = true;
                if (Main.rand.NextFloat() < doubledChance) isDouble_3 = true;

                for (int i = 0; i < 3; i++)
                {
                    int slot = -1;
                    var name = CardName1;
                    var effect = CardEffect1;
                    var effect2 = CardEffect1_2;
                    var count = count_1;
                    if (i == 0) slot = Card1_Type; if (i == 1) slot = Card2_Type; if (i == 2) slot = Card3_Type;
                    if (i == 0) name = CardName1; if (i == 1) name = CardName2; if (i == 2) name = CardName3;
                    if (i == 0) effect = CardEffect1; if (i == 1) effect = CardEffect2; if (i == 2) effect = CardEffect3;
                    if (i == 0) effect2 = CardEffect1_2; if (i == 1) effect2 = CardEffect2_2; if (i == 2) effect2 = CardEffect3_2;
                    if (i == 0) count = count_1; if (i == 1) count = count_2; if (i == 2) count = count_3;

                    effect.SetText("");
                    effect2.SetText("");

                    switch (slot)
                    {
                        case 0:
                            name.SetText("Powerful");
                            effect.SetText($"+{(decimal)(acmPlayer.card_PowerfulValue * 100)}% Ability Power");
                            count.SetText($"{acmPlayer.card_PowerfulCount}x");
                            break;

                        case 1:
                            name.SetText("Timeless");
                            effect.SetText($"+{(decimal)(acmPlayer.card_TimelessValue * 100)}% Cooldown Reduction");
                            count.SetText($"{acmPlayer.card_TimelessCount}x");
                            break;

                        case 2:
                            name.SetText("Healthy");
                            effect.SetText($"+{(decimal)(acmPlayer.card_HealthyValue * 100)}% Max Health");
                            count.SetText($"{acmPlayer.card_HealthyCount}x");
                            break;

                        case 3:
                            name.SetText("Sneaky");
                            effect.SetText($"+{(decimal)(acmPlayer.card_SneakyValue * 100)}% Dodge Chance");
                            count.SetText($"{acmPlayer.card_SneakyCount}x");
                            break;

                        case 4:
                            name.SetText("Nimble Hands");
                            effect.SetText($"+{(decimal)(acmPlayer.card_NimbleHandsValue * 100)}% Attack Speed");
                            count.SetText($"{acmPlayer.card_NimbleHandsCount}x");
                            break;

                        case 5:
                            name.SetText("Carry");
                            effect.SetText($"+{(decimal)(acmPlayer.card_CarryValue * 100)}% Damage");
                            count.SetText($"{acmPlayer.card_CarryCount}x");
                            break;

                        case 6:
                            name.SetText("Deadeye");
                            effect.SetText($"+{(decimal)(acmPlayer.card_DeadeyeValue * 100)}% Crit Damage");
                            count.SetText($"{acmPlayer.card_DeadeyeCount}x");
                            break;

                        case 7:
                            name.SetText("Mending");
                            effect.SetText($"+{(decimal)(acmPlayer.card_MendingValue * 100)}% Heal Power");
                            count.SetText($"{acmPlayer.card_MendingCount}x");
                            break;

                        case 8:
                            name.SetText("Mighty");
                            effect.SetText($"-{(decimal)(acmPlayer.card_MightyValue * 100)}% Ultimate Cost");
                            count.SetText($"{acmPlayer.card_MightyCount}x");
                            break;

                        case 9:
                            name.SetText("Impenetrable");
                            effect.SetText($"+{(decimal)(acmPlayer.card_ImpenetrableValue * 100)}% Damage Reduction");
                            count.SetText($"{acmPlayer.card_ImpenetrableCount}x");
                            break;

                        case 10:
                            name.SetText("Prodigy");
                            effect.SetText($"+{(decimal)(acmPlayer.card_MagicalValue * 100)}% Class Banner Stats");
                            count.SetText($"{acmPlayer.card_MagicalCount}x");
                            break;

                        case 11:
                            name.SetText("Fortified");
                            effect.SetText($"+{(decimal)(acmPlayer.card_FortifiedValue_1 * 100)}% Max Health");
                            effect2.SetText($"+{(decimal)(acmPlayer.card_FortifiedValue_2 * 100)}% Damage Reduction");
                            count.SetText($"{acmPlayer.card_FortifiedCount}x");
                            break;

                        case 12:
                            name.SetText("Masterful");
                            effect.SetText($"+{(decimal)(acmPlayer.card_MasterfulValue_1 * 100)}% Ability Power");
                            effect2.SetText($"+{(decimal)(acmPlayer.card_MasterfulValue_2 * 100)}% Heal Power");
                            count.SetText($"{acmPlayer.card_MasterfulCount}x");
                            break;

                        case 13:
                            name.SetText("Spark Of Genius");
                            effect.SetText($"+{(decimal)(acmPlayer.card_SparkOfGeniusValue_1 * 100)}% Cooldown Reduction");
                            effect2.SetText($"-{(decimal)(acmPlayer.card_SparkOfGeniusValue_2 * 100)}% Ultimate Cost");
                            count.SetText($"{acmPlayer.card_SparkOfGeniusCount}x");
                            break;

                        case 14:
                            name.SetText("Prowler");
                            effect.SetText($"+{(decimal)(acmPlayer.card_ProwlerValue_1 * 100)}% Damage");
                            effect2.SetText($"+{(decimal)(acmPlayer.card_ProwlerValue_2 * 100)}% Crit Damage");
                            count.SetText($"{acmPlayer.card_ProwlerCount}x");
                            break;

                        case 15:
                            name.SetText("Veteran");
                            effect.SetText($"+{(decimal)(acmPlayer.card_VeteranValue_1 * 100)}% Max Health");
                            effect2.SetText($"+{(decimal)(acmPlayer.card_VeteranValue_2 * 100)}% Class Banner Stats");
                            count.SetText($"{acmPlayer.card_VeteranCount}x");
                            break;

                        case 16:
                            name.SetText("Healer");
                            effect.SetText($"+{(decimal)(acmPlayer.card_HealerValue_1 * 100)}% Heal Power");
                            effect2.SetText($"+{(decimal)(acmPlayer.card_HealerValue_2 * 100)}% Cooldown Reduction");
                            count.SetText($"{acmPlayer.card_HealerCount}x");
                            break;

                        case 17:
                            name.SetText("Mischievous");
                            effect.SetText($"+{(decimal)(acmPlayer.card_MischievousValue_1 * 100)}% Crit Damage");
                            effect2.SetText($"+{(decimal)(acmPlayer.card_MischievousValue_2 * 100)}% Dodge Chance");
                            count.SetText($"{acmPlayer.card_MischievousCount}x");
                            break;

                        case 18:
                            name.SetText("Seer");
                            effect.SetText($"+{(decimal)(acmPlayer.card_SeerValue_1 * 100)}% Ability Power");
                            effect2.SetText($"-{(decimal)(acmPlayer.card_SeerValue_2 * 100)}% Ultimate Cost");
                            count.SetText($"{acmPlayer.card_SeerCount}x");
                            break;

                        case 19:
                            name.SetText("Ferocious");
                            effect.SetText($"+{(decimal)(acmPlayer.card_FerociousValue_1 * 100)}% Damage");
                            effect2.SetText($"+{(decimal)(acmPlayer.card_FerociousValue_2 * 100)}% Attack Speed");
                            count.SetText($"{acmPlayer.card_FerociousCount}x");
                            break;

                        default:
                            name.SetText("Healthy");
                            effect.SetText($"+{(decimal)(acmPlayer.card_HealthyValue * 100)}% Max Health");
                            count.SetText($"{acmPlayer.card_HealthyCount}x");
                            break;
                    }
                }
                    
                updated = true;
            }

            #region Card 1 Icons
            switch (Card1_Type)
            {
                case 0:
                    _CardIcon1.SetImage(Request<Texture2D>(apIcon).Value);
                    
                    break;

                case 1:
                    _CardIcon1.SetImage(Request<Texture2D>(cdrIcon).Value);
                    break;

                case 2:
                    _CardIcon1.SetImage(Request<Texture2D>(hpIcon).Value);
                    break;

                case 3:
                    _CardIcon1.SetImage(Request<Texture2D>(dodgeIcon).Value);
                    break;

                case 4:
                    _CardIcon1.SetImage(Request<Texture2D>(asIcon).Value);
                    break;

                case 5:
                    _CardIcon1.SetImage(Request<Texture2D>(dmgIcon).Value);
                    break;

                case 6:
                    _CardIcon1.SetImage(Request<Texture2D>(cdmgIcon).Value);
                    break;

                case 7:
                    _CardIcon1.SetImage(Request<Texture2D>(healIcon).Value);
                    break;

                case 8:
                    _CardIcon1.SetImage(Request<Texture2D>(ucdrIcon).Value);
                    break;

                case 9:
                    _CardIcon1.SetImage(Request<Texture2D>(defIcon).Value);
                    break;

                case 10:
                    _CardIcon1.SetImage(Request<Texture2D>(magicalIcon).Value);
                    break;

                case 11:
                    _CardIcon1.SetImage(Request<Texture2D>(fortifiedIcon).Value);
                    break;

                case 12:
                    _CardIcon1.SetImage(Request<Texture2D>(masterfulIcon).Value);
                    break;

                case 13:
                    _CardIcon1.SetImage(Request<Texture2D>(sogIcon).Value);
                    break;

                case 14:
                    _CardIcon1.SetImage(Request<Texture2D>(prowlerIcon).Value);
                    break;

                case 15:
                    _CardIcon1.SetImage(Request<Texture2D>(veteranIcon).Value);
                    break;

                case 16:
                    _CardIcon1.SetImage(Request<Texture2D>(healerIcon).Value);
                    break;

                case 17:
                    _CardIcon1.SetImage(Request<Texture2D>(mischievousIcon).Value);
                    break;

                case 18:
                    _CardIcon1.SetImage(Request<Texture2D>(seerIcon).Value);
                    break;

                case 19:
                    _CardIcon1.SetImage(Request<Texture2D>(ferociousIcon).Value);
                    break;

                default:
                    _CardIcon1.SetImage(Request<Texture2D>(hpIcon).Value);
                    break;
            }
            #endregion

            #region Card 2 Icons
            switch (Card2_Type)
            {
                case 0:
                    _CardIcon2.SetImage(Request<Texture2D>(apIcon).Value);

                    break;

                case 1:
                    _CardIcon2.SetImage(Request<Texture2D>(cdrIcon).Value);
                    break;

                case 2:
                    _CardIcon2.SetImage(Request<Texture2D>(hpIcon).Value);
                    break;

                case 3:
                    _CardIcon2.SetImage(Request<Texture2D>(dodgeIcon).Value);
                    break;

                case 4:
                    _CardIcon2.SetImage(Request<Texture2D>(asIcon).Value);
                    break;

                case 5:
                    _CardIcon2.SetImage(Request<Texture2D>(dmgIcon).Value);
                    break;

                case 6:
                    _CardIcon2.SetImage(Request<Texture2D>(cdmgIcon).Value);
                    break;

                case 7:
                    _CardIcon2.SetImage(Request<Texture2D>(healIcon).Value);
                    break;

                case 8:
                    _CardIcon2.SetImage(Request<Texture2D>(ucdrIcon).Value);
                    break;

                case 9:
                    _CardIcon2.SetImage(Request<Texture2D>(defIcon).Value);
                    break;

                case 10:
                    _CardIcon2.SetImage(Request<Texture2D>(magicalIcon).Value);
                    break;

                case 11:
                    _CardIcon2.SetImage(Request<Texture2D>(fortifiedIcon).Value);
                    break;

                case 12:
                    _CardIcon2.SetImage(Request<Texture2D>(masterfulIcon).Value);
                    break;

                case 13:
                    _CardIcon2.SetImage(Request<Texture2D>(sogIcon).Value);
                    break;

                case 14:
                    _CardIcon2.SetImage(Request<Texture2D>(prowlerIcon).Value);
                    break;

                case 15:
                    _CardIcon2.SetImage(Request<Texture2D>(veteranIcon).Value);
                    break;

                case 16:
                    _CardIcon2.SetImage(Request<Texture2D>(healerIcon).Value);
                    break;

                case 17:
                    _CardIcon2.SetImage(Request<Texture2D>(mischievousIcon).Value);
                    break;

                case 18:
                    _CardIcon2.SetImage(Request<Texture2D>(seerIcon).Value);
                    break;

                case 19:
                    _CardIcon2.SetImage(Request<Texture2D>(ferociousIcon).Value);
                    break;

                default:
                    _CardIcon2.SetImage(Request<Texture2D>(hpIcon).Value);
                    break;
            }
            #endregion

            #region Card 3 Icons
            switch (Card3_Type)
            {
                case 0:
                    _CardIcon3.SetImage(Request<Texture2D>(apIcon).Value);

                    break;

                case 1:
                    _CardIcon3.SetImage(Request<Texture2D>(cdrIcon).Value);
                    break;

                case 2:
                    _CardIcon3.SetImage(Request<Texture2D>(hpIcon).Value);
                    break;

                case 3:
                    _CardIcon3.SetImage(Request<Texture2D>(dodgeIcon).Value);
                    break;

                case 4:
                    _CardIcon3.SetImage(Request<Texture2D>(asIcon).Value);
                    break;

                case 5:
                    _CardIcon3.SetImage(Request<Texture2D>(dmgIcon).Value);
                    break;

                case 6:
                    _CardIcon3.SetImage(Request<Texture2D>(cdmgIcon).Value);
                    break;

                case 7:
                    _CardIcon3.SetImage(Request<Texture2D>(healIcon).Value);
                    break;

                case 8:
                    _CardIcon3.SetImage(Request<Texture2D>(ucdrIcon).Value);
                    break;

                case 9:
                    _CardIcon3.SetImage(Request<Texture2D>(defIcon).Value);
                    break;

                case 10:
                    _CardIcon3.SetImage(Request<Texture2D>(magicalIcon).Value);
                    break;

                case 11:
                    _CardIcon3.SetImage(Request<Texture2D>(fortifiedIcon).Value);
                    break;

                case 12:
                    _CardIcon3.SetImage(Request<Texture2D>(masterfulIcon).Value);
                    break;

                case 13:
                    _CardIcon3.SetImage(Request<Texture2D>(sogIcon).Value);
                    break;

                case 14:
                    _CardIcon3.SetImage(Request<Texture2D>(prowlerIcon).Value);
                    break;

                case 15:
                    _CardIcon3.SetImage(Request<Texture2D>(veteranIcon).Value);
                    break;

                case 16:
                    _CardIcon3.SetImage(Request<Texture2D>(healerIcon).Value);
                    break;

                case 17:
                    _CardIcon3.SetImage(Request<Texture2D>(mischievousIcon).Value);
                    break;

                case 18:
                    _CardIcon3.SetImage(Request<Texture2D>(seerIcon).Value);
                    break;

                case 19:
                    _CardIcon3.SetImage(Request<Texture2D>(ferociousIcon).Value);
                    break;

                default:
                    _CardIcon3.SetImage(Request<Texture2D>(hpIcon).Value);
                    break;
            }
            #endregion

            tip.SetText("Select one rune to add to your collection");
            tip.TextColor = Color.White;
            if (isDouble_1)
            {
                _CardIcon1.Color = doubledColor;
                CardName1.TextColor = doubledColor;
                CardEffect1.TextColor = doubledColor;
                CardEffect1_2.TextColor = doubledColor;
                count_1.TextColor = doubledColor;
                Card1.BorderColor = doubledColor;
                CardName1.Append(doubleText1);
                if (Card1.IsMouseHovering)
                {
                    tip.SetText("This rune is doubled and will grant you 2 copy of itself");
                    tip.TextColor = doubledColor;
                }
            }

            if (isDouble_2)
            {
                _CardIcon2.Color = doubledColor;
                CardName2.TextColor = doubledColor;
                CardEffect2.TextColor = doubledColor;
                CardEffect2_2.TextColor = doubledColor;
                count_2.TextColor = doubledColor;
                Card2.BorderColor = doubledColor;
                CardName2.Append(doubleText2);
                if (Card2.IsMouseHovering)
                {
                    tip.SetText("This rune is doubled and will grant you 2 copy of itself");
                    tip.TextColor = doubledColor;
                }
            }

            if (isDouble_3)
            {
                _CardIcon3.Color = doubledColor;
                CardName3.TextColor = doubledColor;
                CardEffect3.TextColor = doubledColor;
                CardEffect3_2.TextColor = doubledColor;
                count_3.TextColor = doubledColor;
                Card3.BorderColor = doubledColor;
                CardName3.Append(doubleText3);
                if (Card3.IsMouseHovering)
                {
                    tip.SetText("This rune is doubled and will grant you 2 copy of itself");
                    tip.TextColor = doubledColor;
                }
            }

            //if(!Card1.IsMouseHovering && !Card2.IsMouseHovering && !Card3.IsMouseHovering)
            //{
            //    tip.SetText("Select one rune to add to your collection");
            //    tip.TextColor = Color.White;
            //}

            if (Card1.IsMouseHovering)
            { 
                Card1.BorderColor = Color.Yellow;
            }
            else if(isDouble_1)
                Card1.BorderColor = doubledColor;
            else
                Card1.BorderColor = Color.White;

            if (Card2.IsMouseHovering)
            {
                Card2.BorderColor = Color.Yellow;
            }
            else if (isDouble_2)
                Card2.BorderColor = doubledColor;
            else
                Card2.BorderColor = Color.White;

            if (Card3.IsMouseHovering)
            {
                Card3.BorderColor = Color.Yellow;
            }
            else if (isDouble_3)
                Card3.BorderColor = doubledColor;
            else
                Card3.BorderColor = Color.White;

            base.Update(gameTime);
        }

        private void Click(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            var cardSlot = Card1_Type;

            if (Card1.IsMouseHovering)
                SelectCard(Card1_Type);
            if (Card2.IsMouseHovering)
                SelectCard(Card2_Type);
            if (Card3.IsMouseHovering)
                SelectCard(Card3_Type);

            GetInstance<ACM2ModSystem>()._Cards.SetState(null);
            GetInstance<ACM2ModSystem>()._MyDeck.SetState(new Specializations.MyDeck());
            SoundEngine.PlaySound(SoundID.MaxMana);
        }

        public void SelectCard(int slot)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            int p = 1;
            if (Card1.IsMouseHovering && isDouble_1) p = 2;
            if (Card2.IsMouseHovering && isDouble_2) p = 2;
            if (Card3.IsMouseHovering && isDouble_3) p = 2;

            switch (slot)
            {
                case 0:
                    if(p == 1)
                        acmPlayer.card_PowerfulCount++;
                    else
                        acmPlayer.card_PowerfulCount += 2;
                    
                    break;

                case 1:
                    if (p == 1)
                        acmPlayer.card_TimelessCount++;
                    else
                        acmPlayer.card_TimelessCount += 2;
                    break;

                case 2:
                    if (p == 1)
                        acmPlayer.card_HealthyCount++;
                    else
                        acmPlayer.card_HealthyCount += 2;
                    break;

                case 3:
                    if (p == 1)
                        acmPlayer.card_SneakyCount++;
                    else
                        acmPlayer.card_SneakyCount += 2;
                    break;

                case 4:
                    if (p == 1)
                        acmPlayer.card_NimbleHandsCount++;
                    else
                        acmPlayer.card_NimbleHandsCount += 2;
                    break;

                case 5:
                    if (p == 1)
                        acmPlayer.card_CarryCount++;
                    else
                        acmPlayer.card_CarryCount += 2;
                    break;

                case 6:
                    if (p == 1)
                        acmPlayer.card_DeadeyeCount++;
                    else
                        acmPlayer.card_DeadeyeCount += 2;
                    break;

                case 7:
                    if (p == 1)
                        acmPlayer.card_MendingCount++;
                    else
                        acmPlayer.card_MendingCount += 2;
                    break;

                case 8:
                    if (p == 1)
                        acmPlayer.card_MightyCount++;
                    else
                        acmPlayer.card_MightyCount += 2;
                    break;

                case 9:
                    if (p == 1)
                        acmPlayer.card_ImpenetrableCount++;
                    else
                        acmPlayer.card_ImpenetrableCount += 2;
                    break;

                case 10:
                    if (p == 1)
                        acmPlayer.card_MagicalCount++;
                    else
                        acmPlayer.card_MagicalCount += 2;
                    break;

                case 11:
                    if (p == 1)
                        acmPlayer.card_FortifiedCount++;
                    else
                        acmPlayer.card_FortifiedCount += 2;
                    break;

                case 12:
                    if (p == 1)
                        acmPlayer.card_MasterfulCount++;
                    else
                        acmPlayer.card_MasterfulCount += 2;
                    break;

                case 13:
                    if (p == 1)
                        acmPlayer.card_SparkOfGeniusCount++;
                    else
                        acmPlayer.card_SparkOfGeniusCount += 2;
                    break;

                case 14:
                    if (p == 1)
                        acmPlayer.card_ProwlerCount++;
                    else
                        acmPlayer.card_ProwlerCount += 2;
                    break;

                case 15:
                    if (p == 1)
                        acmPlayer.card_VeteranCount++;
                    else
                        acmPlayer.card_VeteranCount += 2;
                    break;

                case 16:
                    if (p == 1)
                        acmPlayer.card_HealerCount++;
                    else
                        acmPlayer.card_HealerCount += 2;
                    break;

                case 17:
                    if (p == 1)
                        acmPlayer.card_MischievousCount++;
                    else
                        acmPlayer.card_MischievousCount += 2;
                    break;

                case 18:
                    if (p == 1)
                        acmPlayer.card_SeerCount++;
                    else
                        acmPlayer.card_SeerCount += 2;
                    break;

                case 19:
                    if (p == 1)
                        acmPlayer.card_FerociousCount++;
                    else
                        acmPlayer.card_FerociousCount += 2;
                    break;

                default:
                    if (p == 1)
                        acmPlayer.card_HealthyCount++;
                    else
                        acmPlayer.card_HealthyCount += 2;
                    break;
            }
        }
    }
}