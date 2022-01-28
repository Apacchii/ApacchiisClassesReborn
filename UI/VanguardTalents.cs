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

namespace ApacchiisClassesMod2.UI
{
    class VanguardTalents : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        int abilityPanelHeight = 50;
        int abilityPanelWidth = 140;
        int abilityTop = 60;

        float bWidth = 250f;
        float bHeight = 40f;
        float bLeftSpace = 0f;
        float bRightSpace = 265f;

        public string selectedClass = "";
        string className = "Vanguard";

        float L1Scale = 1f;
        float R1Scale = 1f;
        string L1 = "+2 Defense";
        string R1 = "+5% Cooldown Reduction";

        float L2Scale = 1f;
        float R2Scale = 1f;
        string L2 = "+1.5% Max Health";
        string R2 = "+1% Melee Damage";

        float L3Scale = 1f;
        float R3Scale = .95f;
        string L3 = "+5% Barrier Endurance";
        string R3 = "+18 Spear Explosion Damage";

        float L4Scale = 1f;
        float R4Scale = 1f;
        string L4 = " +4 Defense";
        string R4 = "+0.5s Barrier Duration";

        float L5Scale = 1f;
        float R5Scale = 1f;
        string L5 = "+2% Max Health";
        string R5 = "-10% Ult Cost";

        float L6Scale = 1f;
        float R6Scale = 1f;
        string L6 = "2x Spear Explosion Range";
        string R6 = "+50% Passive Reflect";

        float L7Scale = 1f;
        float R7Scale = .9f;
        string L7 = "+2% Melee Crit";
        string R7 = "Spear Heals 2.4% Max Health";

        float L8Scale = 1f;
        float R8Scale = 1f;
        string L8 = "+35 Ult Damage";
        string R8 = "+0.8s Barrier Duration";

        float L9Scale = .9f;
        float R9Scale = 1f;
        string L9 = "Barrier Provides 1.2% HP Regen";
        string R9 = "15% Cooldown Reduction";

        float L10Scale = 1f;
        float R10Scale = 1f;
        string L10 = "+8% Max Health";
        string R10 = "+4% Ult Boss Execute";

        UIPanel background;
        UIText classText;

        UIPanel infoLeft;
        UIPanel infoRight;
        UIText textInfoLeft;
        UIText textInfoRight;
        
        UIPanel leftPanel;
        UIPanel rightPanel;
        UIText leftText;
        UIText rightText;

        UIPanel close;
        UIText closeText;

        UIPanel button1_Left;
        UIPanel button1_Right;
        UIPanel button2_Left;
        UIPanel button2_Right;
        UIPanel button3_Left;
        UIPanel button3_Right;
        UIPanel button4_Left;
        UIPanel button4_Right;
        UIPanel button5_Left;
        UIPanel button5_Right;
        UIPanel button6_Left;
        UIPanel button6_Right;
        UIPanel button7_Left;
        UIPanel button7_Right;
        UIPanel button8_Left;
        UIPanel button8_Right;
        UIPanel button9_Left;
        UIPanel button9_Right;
        UIPanel button10_Left;
        UIPanel button10_Right;

        UIText text1_Left;
        UIText text1_Right;
        UIText text2_Left;
        UIText text2_Right;
        UIText text3_Left;
        UIText text3_Right;
        UIText text4_Left;
        UIText text4_Right;
        UIText text5_Left;
        UIText text5_Right;
        UIText text6_Left;
        UIText text6_Right;
        UIText text7_Left;
        UIText text7_Right;
        UIText text8_Left;
        UIText text8_Right;
        UIText text9_Left;
        UIText text9_Right;
        UIText text10_Left;
        UIText text10_Right;


        public override void OnInitialize()
        {
            background = new UIPanel();
            background.VAlign = .5f;
            background.HAlign = .5f;
            background.Width.Set(540, 0f);
            background.Height.Set(700, 0f);
            background.BackgroundColor = new Color(75, 75, 75);
            background.BorderColor = new Color(25, 25, 25);
            Append(background);

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



            infoLeft = new UIPanel();
            infoLeft.VAlign = .5f;
            infoLeft.HAlign = .5f;
            infoLeft.Left.Set(-430, 0f);
            infoLeft.Width.Set(300, 0f);
            infoLeft.Height.Set(150, 0f);
            infoLeft.BackgroundColor = new Color(75, 75, 75);
            infoLeft.BorderColor = new Color(25, 25, 25);
            //background.Append(infoLeft);

            textInfoLeft = new UIText("");
            textInfoLeft.VAlign = .3f;
            textInfoLeft.HAlign = .5f;
            //infoLeft.Append(textInfoLeft);

            infoRight = new UIPanel();
            infoRight.VAlign = .5f;
            infoRight.HAlign = .5f;
            infoRight.Left.Set(430, 0f);
            infoRight.Width.Set(300, 0f);
            infoRight.Height.Set(150, 0f);
            infoRight.BackgroundColor = new Color(75, 75, 75);
            infoRight.BorderColor = new Color(25, 25, 25);
            //background.Append(infoRight);

            textInfoRight = new UIText("");
            textInfoRight.VAlign = .3f;
            textInfoRight.HAlign = .5f;
            //infoRight.Append(textInfoRight);

            classText = new UIText("" + className);
            classText.Top.Set(5, 0f);
            classText.HAlign = .5f;
            background.Append(classText);



            button1_Left = new UIPanel();
            button1_Left.Left.Set(0f, 0f);
            button1_Left.Top.Set(635, 0f);
            button1_Left.Width.Set(bWidth, 0f);
            button1_Left.Height.Set(bHeight, 0f);
            button1_Left.BackgroundColor = new Color(75, 75, 75);
            button1_Left.BorderColor = new Color(25, 25, 25);
            button1_Left.OnClick += Button1L;
            background.Append(button1_Left);

            button1_Right = new UIPanel();
            button1_Right.Left.Set(bRightSpace, 0f);
            button1_Right.Top.Set(635, 0f);
            button1_Right.Width.Set(bWidth, 0f);
            button1_Right.Height.Set(bHeight, 0f);
            button1_Right.BackgroundColor = new Color(75, 75, 75);
            button1_Right.BorderColor = new Color(25, 25, 25);
            button1_Right.OnClick += Button1R;
            background.Append(button1_Right);

            text1_Left = new UIText("" + L1, L1Scale);
            text1_Left.VAlign = .5f;
            text1_Left.HAlign = .5f;
            text1_Left.OnClick += Button1L;
            button1_Left.Append(text1_Left);

            text1_Right = new UIText("" + R1, R1Scale);
            text1_Right.VAlign = .5f;
            text1_Right.HAlign = .5f;
            text1_Right.OnClick += Button1R;
            button1_Right.Append(text1_Right);



            button2_Left = new UIPanel();
            button2_Left.Left.Set(0f, 0f);
            button2_Left.Top.Set(585, 0f);
            button2_Left.Width.Set(bWidth, 0f);
            button2_Left.Height.Set(bHeight, 0f);
            button2_Left.BackgroundColor = new Color(75, 75, 75);
            button2_Left.BorderColor = new Color(25, 25, 25);
            button2_Left.OnClick += Button2L;
            background.Append(button2_Left);

            button2_Right = new UIPanel();
            button2_Right.Left.Set(bRightSpace, 0f);
            button2_Right.Top.Set(585, 0f);
            button2_Right.Width.Set(bWidth, 0f);
            button2_Right.Height.Set(bHeight, 0f);
            button2_Right.BackgroundColor = new Color(75, 75, 75);
            button2_Right.BorderColor = new Color(25, 25, 25);
            button2_Right.OnClick += Button2R;
            background.Append(button2_Right);

            text2_Left = new UIText("" + L2, L2Scale);
            text2_Left.VAlign = .5f;
            text2_Left.HAlign = .5f;
            text2_Left.OnClick += Button2L;
            button2_Left.Append(text2_Left);

            text2_Right = new UIText("" + R2, R2Scale);
            text2_Right.VAlign = .5f;
            text2_Right.HAlign = .5f;
            text2_Right.OnClick += Button2R;
            button2_Right.Append(text2_Right);



            button3_Left = new UIPanel();
            button3_Left.Left.Set(0f, 0f);
            button3_Left.Top.Set(535, 0f);
            button3_Left.Width.Set(bWidth, 0f);
            button3_Left.Height.Set(bHeight, 0f);
            button3_Left.BackgroundColor = new Color(75, 75, 75);
            button3_Left.BorderColor = new Color(25, 25, 25);
            button3_Left.OnClick += Button3L;
            background.Append(button3_Left);

            button3_Right = new UIPanel();
            button3_Right.Left.Set(bRightSpace, 0f);
            button3_Right.Top.Set(535, 0f);
            button3_Right.Width.Set(bWidth, 0f);
            button3_Right.Height.Set(bHeight, 0f);
            button3_Right.BackgroundColor = new Color(75, 75, 75);
            button3_Right.BorderColor = new Color(25, 25, 25);
            button3_Right.OnClick += Button3R;
            background.Append(button3_Right);

            text3_Left = new UIText("" + L3, L3Scale);
            text3_Left.VAlign = .5f;
            text3_Left.HAlign = .5f;
            text3_Left.OnClick += Button3L;
            button3_Left.Append(text3_Left);

            text3_Right = new UIText("" + R3, R3Scale);
            text3_Right.VAlign = .5f;
            text3_Right.HAlign = .5f;
            text3_Right.OnClick += Button3R;
            button3_Right.Append(text3_Right);



            button4_Left = new UIPanel();
            button4_Left.Left.Set(0f, 0f);
            button4_Left.Top.Set(485, 0f);
            button4_Left.Width.Set(bWidth, 0f);
            button4_Left.Height.Set(bHeight, 0f);
            button4_Left.BackgroundColor = new Color(75, 75, 75);
            button4_Left.BorderColor = new Color(25, 25, 25);
            button4_Left.OnClick += Button4L;
            background.Append(button4_Left);

            button4_Right = new UIPanel();
            button4_Right.Left.Set(bRightSpace, 0f);
            button4_Right.Top.Set(485, 0f);
            button4_Right.Width.Set(bWidth, 0f);
            button4_Right.Height.Set(bHeight, 0f);
            button4_Right.BackgroundColor = new Color(75, 75, 75);
            button4_Right.BorderColor = new Color(25, 25, 25);
            button4_Right.OnClick += Button4R;
            background.Append(button4_Right);

            text4_Left = new UIText("" + L4, L4Scale);
            text4_Left.VAlign = .5f;
            text4_Left.HAlign = .5f;
            text4_Left.OnClick += Button4L;
            button4_Left.Append(text4_Left);

            text4_Right = new UIText("" + R4, R4Scale);
            text4_Right.VAlign = .5f;
            text4_Right.HAlign = .5f;
            text4_Right.OnClick += Button4R;
            button4_Right.Append(text4_Right);



            button5_Left = new UIPanel();
            button5_Left.Left.Set(0f, 0f);
            button5_Left.Top.Set(435, 0f);
            button5_Left.Width.Set(bWidth, 0f);
            button5_Left.Height.Set(bHeight, 0f);
            button5_Left.BackgroundColor = new Color(75, 75, 75);
            button5_Left.BorderColor = new Color(25, 25, 25);
            button5_Left.OnClick += Button5L;
            background.Append(button5_Left);

            button5_Right = new UIPanel();
            button5_Right.Left.Set(bRightSpace, 0f);
            button5_Right.Top.Set(435, 0f);
            button5_Right.Width.Set(bWidth, 0f);
            button5_Right.Height.Set(bHeight, 0f);
            button5_Right.BackgroundColor = new Color(75, 75, 75);
            button5_Right.BorderColor = new Color(25, 25, 25);
            button5_Right.OnClick += Button5R;
            background.Append(button5_Right);

            text5_Left = new UIText("" + L5, L5Scale);
            text5_Left.VAlign = .5f;
            text5_Left.HAlign = .5f;
            text5_Left.OnClick += Button5L;
            button5_Left.Append(text5_Left);

            text5_Right = new UIText("" + R5, R5Scale);
            text5_Right.VAlign = .5f;
            text5_Right.HAlign = .5f;
            text5_Right.OnClick += Button5R;
            button5_Right.Append(text5_Right);



            button6_Left = new UIPanel();
            button6_Left.Left.Set(0f, 0f);
            button6_Left.Top.Set(385, 0f);
            button6_Left.Width.Set(bWidth, 0f);
            button6_Left.Height.Set(bHeight, 0f);
            button6_Left.BackgroundColor = new Color(75, 75, 75);
            button6_Left.BorderColor = new Color(25, 25, 25);
            button6_Left.OnClick += Button6L;
            background.Append(button6_Left);

            button6_Right = new UIPanel();
            button6_Right.Left.Set(bRightSpace, 0f);
            button6_Right.Top.Set(385, 0f);
            button6_Right.Width.Set(bWidth, 0f);
            button6_Right.Height.Set(bHeight, 0f);
            button6_Right.BackgroundColor = new Color(75, 75, 75);
            button6_Right.BorderColor = new Color(25, 25, 25);
            button6_Right.OnClick += Button6R;
            background.Append(button6_Right);

            text6_Left = new UIText("" + L6, L6Scale);
            text6_Left.VAlign = .5f;
            text6_Left.HAlign = .5f;
            text6_Left.OnClick += Button6L;
            button6_Left.Append(text6_Left);

            text6_Right = new UIText("" + R6, R6Scale);
            text6_Right.VAlign = .5f;
            text6_Right.HAlign = .5f;
            text6_Right.OnClick += Button6R;
            button6_Right.Append(text6_Right);



            button7_Left = new UIPanel();
            button7_Left.Left.Set(0f, 0f);
            button7_Left.Top.Set(335, 0f);
            button7_Left.Width.Set(bWidth, 0f);
            button7_Left.Height.Set(bHeight, 0f);
            button7_Left.BackgroundColor = new Color(75, 75, 75);
            button7_Left.BorderColor = new Color(25, 25, 25);
            button7_Left.OnClick += Button7L;
            background.Append(button7_Left);

            button7_Right = new UIPanel();
            button7_Right.Left.Set(bRightSpace, 0f);
            button7_Right.Top.Set(335, 0f);
            button7_Right.Width.Set(bWidth, 0f);
            button7_Right.Height.Set(bHeight, 0f);
            button7_Right.BackgroundColor = new Color(75, 75, 75);
            button7_Right.BorderColor = new Color(25, 25, 25);
            button7_Right.OnClick += Button7R;
            background.Append(button7_Right);

            text7_Left = new UIText("" + L7, L7Scale);
            text7_Left.VAlign = .5f;
            text7_Left.HAlign = .5f;
            text7_Left.OnClick += Button7L;
            button7_Left.Append(text7_Left);

            text7_Right = new UIText("" + R7, R7Scale);
            text7_Right.VAlign = .5f;
            text7_Right.HAlign = .5f;
            text7_Right.OnClick += Button7R;
            button7_Right.Append(text7_Right);



            button8_Left = new UIPanel();
            button8_Left.Left.Set(0f, 0f);
            button8_Left.Top.Set(285, 0f);
            button8_Left.Width.Set(bWidth, 0f);
            button8_Left.Height.Set(bHeight, 0f);
            button8_Left.BackgroundColor = new Color(75, 75, 75);
            button8_Left.BorderColor = new Color(25, 25, 25);
            button8_Left.OnClick += Button8L;
            background.Append(button8_Left);

            button8_Right = new UIPanel();
            button8_Right.Left.Set(bRightSpace, 0f);
            button8_Right.Top.Set(285, 0f);
            button8_Right.Width.Set(bWidth, 0f);
            button8_Right.Height.Set(bHeight, 0f);
            button8_Right.BackgroundColor = new Color(75, 75, 75);
            button8_Right.BorderColor = new Color(25, 25, 25);
            button8_Right.OnClick += Button8R;
            background.Append(button8_Right);

            text8_Left = new UIText("" + L8, L8Scale);
            text8_Left.VAlign = .5f;
            text8_Left.HAlign = .5f;
            text8_Left.OnClick += Button8L;
            button8_Left.Append(text8_Left);

            text8_Right = new UIText("" + R8, R8Scale);
            text8_Right.VAlign = .5f;
            text8_Right.HAlign = .5f;
            text8_Right.OnClick += Button8R;
            button8_Right.Append(text8_Right);



            button9_Left = new UIPanel();
            button9_Left.Left.Set(0f, 0f);
            button9_Left.Top.Set(235, 0f);
            button9_Left.Width.Set(bWidth, 0f);
            button9_Left.Height.Set(bHeight, 0f);
            button9_Left.BackgroundColor = new Color(75, 75, 75);
            button9_Left.BorderColor = new Color(25, 25, 25);
            button9_Left.OnClick += Button9L;
            background.Append(button9_Left);

            button9_Right = new UIPanel();
            button9_Right.Left.Set(bRightSpace, 0f);
            button9_Right.Top.Set(235, 0f);
            button9_Right.Width.Set(bWidth, 0f);
            button9_Right.Height.Set(bHeight, 0f);
            button9_Right.BackgroundColor = new Color(75, 75, 75);
            button9_Right.BorderColor = new Color(25, 25, 25);
            button9_Right.OnClick += Button9R;
            background.Append(button9_Right);

            text9_Left = new UIText("" + L9, L9Scale);
            text9_Left.VAlign = .5f;
            text9_Left.HAlign = .5f;
            text9_Left.OnClick += Button9L;
            button9_Left.Append(text9_Left);

            text9_Right = new UIText("" + R9, R9Scale);
            text9_Right.VAlign = .5f;
            text9_Right.HAlign = .5f;
            text9_Right.OnClick += Button9R;
            button9_Right.Append(text9_Right);



            button10_Left = new UIPanel();
            button10_Left.Left.Set(0f, 0f);
            button10_Left.Top.Set(185, 0f);
            button10_Left.Width.Set(bWidth, 0f);
            button10_Left.Height.Set(bHeight, 0f);
            button10_Left.BackgroundColor = new Color(75, 75, 75);
            button10_Left.BorderColor = new Color(25, 25, 25);
            button10_Left.OnClick += Button10L;
            background.Append(button10_Left);

            button10_Right = new UIPanel();
            button10_Right.Left.Set(bRightSpace, 0f);
            button10_Right.Top.Set(185, 0f);
            button10_Right.Width.Set(bWidth, 0f);
            button10_Right.Height.Set(bHeight, 0f);
            button10_Right.BackgroundColor = new Color(75, 75, 75);
            button10_Right.BorderColor = new Color(25, 25, 25);
            button10_Right.OnClick += Button10R;
            background.Append(button10_Right);

            text10_Left = new UIText("" + L10, L10Scale);
            text10_Left.VAlign = .5f;
            text10_Left.HAlign = .5f;
            text10_Left.OnClick += Button10L;
            button10_Left.Append(text10_Left);

            text10_Right = new UIText("" + R10, R10Scale);
            text10_Right.VAlign = .5f;
            text10_Right.HAlign = .5f;
            text10_Right.OnClick += Button10R;
            button10_Right.Append(text10_Right);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if(close.IsMouseHovering || button1_Left.IsMouseHovering || button1_Right.IsMouseHovering || button2_Left.IsMouseHovering || button2_Right.IsMouseHovering || button3_Left.IsMouseHovering || button3_Right.IsMouseHovering || button4_Left.IsMouseHovering || button4_Right.IsMouseHovering || button5_Left.IsMouseHovering || button5_Right.IsMouseHovering || button6_Left.IsMouseHovering || button6_Right.IsMouseHovering || button7_Left.IsMouseHovering || button7_Right.IsMouseHovering || button8_Left.IsMouseHovering || button8_Right.IsMouseHovering || button9_Left.IsMouseHovering || button9_Right.IsMouseHovering || button10_Left.IsMouseHovering || button10_Right.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            classText.SetText(className + ": " + acmPlayer.vanguardTalentPoints + " Talent Points (TP)");

            if (button1_Left.IsMouseHovering)
                button1_Left.BorderColor = Color.Yellow;
            else
                button1_Left.BorderColor = new Color(25, 25, 25);
            if (button2_Left.IsMouseHovering)
                button2_Left.BorderColor = Color.Yellow;
            else
                button2_Left.BorderColor = new Color(25, 25, 25);
            if (button3_Left.IsMouseHovering)
                button3_Left.BorderColor = Color.Yellow;
            else
                button3_Left.BorderColor = new Color(25, 25, 25);
            if (button4_Left.IsMouseHovering)
                button4_Left.BorderColor = Color.Yellow;
            else
                button4_Left.BorderColor = new Color(25, 25, 25);
            if (button5_Left.IsMouseHovering)
                button5_Left.BorderColor = Color.Yellow;
            else
                button5_Left.BorderColor = new Color(25, 25, 25);
            if (button6_Left.IsMouseHovering)
                button6_Left.BorderColor = Color.Yellow;
            else
                button6_Left.BorderColor = new Color(25, 25, 25);
            if (button7_Left.IsMouseHovering)
                button7_Left.BorderColor = Color.Yellow;
            else
                button7_Left.BorderColor = new Color(25, 25, 25);
            if (button8_Left.IsMouseHovering)
                button8_Left.BorderColor = Color.Yellow;
            else
                button8_Left.BorderColor = new Color(25, 25, 25);
            if (button9_Left.IsMouseHovering)
                button9_Left.BorderColor = Color.Yellow;
            else
                button9_Left.BorderColor = new Color(25, 25, 25);
            if (button10_Left.IsMouseHovering)
                button10_Left.BorderColor = Color.Yellow;
            else
                button10_Left.BorderColor = new Color(25, 25, 25);
            if (button1_Right.IsMouseHovering)
                button1_Right.BorderColor = Color.Yellow;
            else
                button1_Right.BorderColor = new Color(25, 25, 25);
            if (button2_Right.IsMouseHovering)
                button2_Right.BorderColor = Color.Yellow;
            else
                button2_Right.BorderColor = new Color(25, 25, 25);
            if (button3_Right.IsMouseHovering)
                button3_Right.BorderColor = Color.Yellow;
            else
                button3_Right.BorderColor = new Color(25, 25, 25);
            if (button4_Right.IsMouseHovering)
                button4_Right.BorderColor = Color.Yellow;
            else
                button4_Right.BorderColor = new Color(25, 25, 25);
            if (button5_Right.IsMouseHovering)
                button5_Right.BorderColor = Color.Yellow;
            else
                button5_Right.BorderColor = new Color(25, 25, 25);
            if (button6_Right.IsMouseHovering)
                button6_Right.BorderColor = Color.Yellow;
            else
                button6_Right.BorderColor = new Color(25, 25, 25);
            if (button7_Right.IsMouseHovering)
                button7_Right.BorderColor = Color.Yellow;
            else
                button7_Right.BorderColor = new Color(25, 25, 25);
            if (button8_Right.IsMouseHovering)
                button8_Right.BorderColor = Color.Yellow;
            else
                button8_Right.BorderColor = new Color(25, 25, 25);
            if (button9_Right.IsMouseHovering)
                button9_Right.BorderColor = Color.Yellow;
            else
                button9_Right.BorderColor = new Color(25, 25, 25);
            if (button10_Right.IsMouseHovering)
                button10_Right.BorderColor = Color.Yellow;
            else
                button10_Right.BorderColor = new Color(25, 25, 25);

            if (acmPlayer.vanguardSpentTalentPoints < 1 && acmPlayer.vanguardTalent_2 == "N")
            {
                button2_Left.BackgroundColor = new Color(50, 50, 50);
                button2_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if(acmPlayer.vanguardTalent_2 == "N")
            {
                button2_Left.BackgroundColor = new Color(75, 75, 75);
                button2_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 2 && acmPlayer.vanguardTalent_3 == "N")
            {
                button3_Left.BackgroundColor = new Color(50, 50, 50);
                button3_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if(acmPlayer.vanguardTalent_3 == "N")
            {
                button3_Left.BackgroundColor = new Color(75, 75, 75);
                button3_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 3 && acmPlayer.vanguardTalent_4 == "N")
            {
                button4_Left.BackgroundColor = new Color(50, 50, 50);
                button4_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if(acmPlayer.vanguardTalent_4 == "N")
            {
                button4_Left.BackgroundColor = new Color(75, 75, 75);
                button4_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 4 && acmPlayer.vanguardTalent_5 == "N")
            {
                button5_Left.BackgroundColor = new Color(50, 50, 50);
                button5_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if(acmPlayer.vanguardTalent_5 == "N")
            {
                button5_Left.BackgroundColor = new Color(75, 75, 75);
                button5_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 5 && acmPlayer.vanguardTalent_6 == "N")
            {
                button6_Left.BackgroundColor = new Color(50, 50, 50);
                button6_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if (acmPlayer.vanguardTalent_6 == "N")
            {
                button6_Left.BackgroundColor = new Color(75, 75, 75);
                button6_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 6 && acmPlayer.vanguardTalent_7  == "N")
            {
                button7_Left.BackgroundColor = new Color(50, 50, 50);
                button7_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if (acmPlayer.vanguardTalent_7 == "N")
            {
                button7_Left.BackgroundColor = new Color(75, 75, 75);
                button7_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 7 && acmPlayer.vanguardTalent_8 == "N")
            {
                button8_Left.BackgroundColor = new Color(50, 50, 50);
                button8_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if (acmPlayer.vanguardTalent_8 == "N")
            {
                button8_Left.BackgroundColor = new Color(75, 75, 75);
                button8_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 8 && acmPlayer.vanguardTalent_9 == "N")
            {
                button9_Left.BackgroundColor = new Color(50, 50, 50);
                button9_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if (acmPlayer.vanguardTalent_9 == "N")
            {
                button9_Left.BackgroundColor = new Color(75, 75, 75);
                button9_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardSpentTalentPoints < 9 && acmPlayer.vanguardTalent_10 == "N")
            {
                button10_Left.BackgroundColor = new Color(50, 50, 50);
                button10_Right.BackgroundColor = new Color(50, 50, 50);
            }
            else if (acmPlayer.vanguardTalent_10 == "N")
            {
                button10_Left.BackgroundColor = new Color(75, 75, 75);
                button10_Right.BackgroundColor = new Color(75, 75, 75);
            }

            if (acmPlayer.vanguardTalent_1 == "L" && acmPlayer.vanguardSpentTalentPoints <= 10)
                button1_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_1 == "R" && acmPlayer.vanguardSpentTalentPoints <= 10)
                button1_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_2 == "L" && acmPlayer.vanguardSpentTalentPoints <= 11)
                button2_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_2 == "R" && acmPlayer.vanguardSpentTalentPoints <= 11)
                button2_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_3 == "L" && acmPlayer.vanguardSpentTalentPoints <= 12)
                button3_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_3 == "R" && acmPlayer.vanguardSpentTalentPoints <= 12)
                button3_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_4 == "L" && acmPlayer.vanguardSpentTalentPoints <= 13)
                button4_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_4 == "R" && acmPlayer.vanguardSpentTalentPoints <= 13)
                button4_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_5 == "L" && acmPlayer.vanguardSpentTalentPoints <= 14)
                button5_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_5 == "R" && acmPlayer.vanguardSpentTalentPoints <= 14)
                button5_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_6 == "L" && acmPlayer.vanguardSpentTalentPoints <= 15)
                button6_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_6 == "R" && acmPlayer.vanguardSpentTalentPoints <= 15)
                button6_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_7 == "L" && acmPlayer.vanguardSpentTalentPoints <= 16)
                button7_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_7 == "R" && acmPlayer.vanguardSpentTalentPoints <= 16)
                button7_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_8 == "L" && acmPlayer.vanguardSpentTalentPoints <= 17)
                button8_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_8 == "R" && acmPlayer.vanguardSpentTalentPoints <= 17)
                button8_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_9 == "L" && acmPlayer.vanguardSpentTalentPoints <= 18)
                button9_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_9 == "R" && acmPlayer.vanguardSpentTalentPoints <= 18)
                button9_Right.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_10 == "L" && acmPlayer.vanguardSpentTalentPoints <= 19)
                button10_Left.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.vanguardTalent_10 == "R" && acmPlayer.vanguardSpentTalentPoints <= 19)
                button10_Right.BackgroundColor = Color.DarkOrange;

            if (acmPlayer.vanguardTalent_1 == "B")
            {
                button1_Left.BackgroundColor = Color.DarkOrange;
                button1_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_2 == "B")
            {
                button2_Left.BackgroundColor = Color.DarkOrange;
                button2_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_3 == "B")
            {
                button3_Left.BackgroundColor = Color.DarkOrange;
                button3_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_4 == "B")
            {
                button4_Left.BackgroundColor = Color.DarkOrange;
                button4_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_5 == "B")
            {
                button5_Left.BackgroundColor = Color.DarkOrange;
                button5_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_6 == "B")
            {
                button6_Left.BackgroundColor = Color.DarkOrange;
                button6_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_7 == "B")
            {
                button7_Left.BackgroundColor = Color.DarkOrange;
                button7_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_8 == "B")
            {
                button8_Left.BackgroundColor = Color.DarkOrange;
                button8_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_9 == "B")
            {
                button9_Left.BackgroundColor = Color.DarkOrange;
                button9_Right.BackgroundColor = Color.DarkOrange;
            }
            if (acmPlayer.vanguardTalent_10 == "B")
            {
                button10_Left.BackgroundColor = Color.DarkOrange;
                button10_Right.BackgroundColor = Color.DarkOrange;
            }

            base.Update(gameTime);
        }

        private void Button1L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if(acmPlayer.vanguardTalent_1 == "N" && acmPlayer.vanguardTalentPoints > 0)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_1 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_1 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 10)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_1 = "B";
                }
            }

        }
        private void Button1R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_1 == "N" && acmPlayer.vanguardTalentPoints > 0)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_1 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_1 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 10)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_1 = "B";
                }
            }
        }

        private void Button2L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_2 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 1)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_2 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_2 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 11)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_2 = "B";
                }
            }
        }
        private void Button2R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_2 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 1)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_2 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_2 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 11)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_2 = "B";
                }
            }
        }

        private void Button3L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_3 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 2)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_3 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_3 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 12)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_3 = "B";
                }
            }
        }
        private void Button3R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_3 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 2)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_3 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_3 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 12)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_3 = "B";
                }
            }
        }

        private void Button4L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_4 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 3)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_4 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_4 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 13)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_4 = "B";
                }
            }
        }
        private void Button4R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_4 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 3)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_4 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_4 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 13)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_4 = "B";
                }
            }
        }

        private void Button5L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_5 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 4)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_5 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_5 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 14)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_5 = "B";
                }
            }
        }
        private void Button5R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_5 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 4)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_5 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_5 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 14)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_5 = "B";
                }
            }
        }

        private void Button6L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_6 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 5)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_6 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_6 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 15)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_6 = "B";
                }
            }
        }
        private void Button6R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_6 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 5)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_6 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_6 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 15)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_6 = "B";
                }
            }
        }

        private void Button7L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_7 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 6)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_7 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_7 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 16)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_7 = "B";
                }
            }
        }
        private void Button7R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_7 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 6)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_7 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_7 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 16)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_7 = "B";
                }
            }
        }

        private void Button8L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_8 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 7)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_8 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_8 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 17)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_8 = "B";
                }
            }
        }
        private void Button8R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_8 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 7)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_8 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_8 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 17)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_8 = "B";
                }
            }
        }

        private void Button9L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_9 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 8)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_9 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_9 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 18)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_9 = "B";
                }
            }
        }
        private void Button9R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_9 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 8)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_9 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_9 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 18)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_9 = "B";
                }
            }
        }

        private void Button10L(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_10 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 9)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_10 = "L";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_10 == "R" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 19)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_10 = "B";
                }
            }
        }
        private void Button10R(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.vanguardTalent_10 == "N" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 9)
            {
                acmPlayer.vanguardTalentPoints--;
                acmPlayer.vanguardSpentTalentPoints++;
                acmPlayer.vanguardTalent_10 = "R";
            }

            if (ACMConfigServer.Instance.doubleTalents)
            {
                if (acmPlayer.vanguardTalent_10 == "L" && acmPlayer.vanguardTalentPoints > 0 && acmPlayer.vanguardSpentTalentPoints >= 19)
                {
                    acmPlayer.vanguardTalentPoints--;
                    acmPlayer.vanguardSpentTalentPoints++;
                    acmPlayer.vanguardTalent_10 = "B";
                }
            }
        }

        private void Close(UIMouseEvent evt, UIElement listeningElement)
        {
            GetInstance<ACM2ModSystem>()._VanguardTalents.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
    }
}