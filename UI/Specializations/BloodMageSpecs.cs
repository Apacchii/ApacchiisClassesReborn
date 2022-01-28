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

namespace ApacchiisClassesMod2.UI.Specializations
{
    class BloodMageSpecs : UIState
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
        string className = "Blood Mage";

        float B1Scale = 1f;
        string B1 = "";

        float B2Scale = 1f;
        string B2 = "";

        float B3Scale = 1f;
        string B3 = "";

        float B4Scale = 1f;
        string B4 = "";

        float B5Scale = 1f;
        string B5 = "";

        float B6Scale = 1f;
        string B6 = "";

        UIPanel background;
        UIText classText;

        UIPanel close;
        UIText closeText;

        UIPanel button1;
        UIPanel button2;
        UIPanel button3;
        UIPanel button4;
        UIPanel button5;
        UIPanel button6;

        UIText text1;
        UIText text2;
        UIText text3;
        UIText text4;
        UIText text5;
        UIText text6;

        public override void OnInitialize()
        {
            background = new UIPanel();
            background.VAlign = .5f;
            background.HAlign = .5f;
            background.Width.Set(700, 0f);
            background.Height.Set(540, 0f);
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

            classText = new UIText("" + className);
            classText.Top.Set(5, 0f);
            classText.HAlign = .5f;
            background.Append(classText);



            //button1_Left = new UIPanel();
            //button1_Left.Left.Set(0f, 0f);
            //button1_Left.Top.Set(635, 0f);
            //button1_Left.Width.Set(bWidth, 0f);
            //button1_Left.Height.Set(bHeight, 0f);
            //button1_Left.BackgroundColor = new Color(75, 75, 75);
            //button1_Left.BorderColor = new Color(25, 25, 25);
            //button1_Left.OnClick += Button1L;
            //background.Append(button1_Left);

            //text1_Left = new UIText("" + L1, L1Scale);
            //text1_Left.VAlign = .5f;
            //text1_Left.HAlign = .5f;
            //text1_Left.OnClick += Button1L;
            //button1_Left.Append(text1_Left);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if(close.IsMouseHovering || button1.IsMouseHovering || button2.IsMouseHovering || button3.IsMouseHovering || button4.IsMouseHovering || button5.IsMouseHovering || button6.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            classText.SetText(className + ": " + acmPlayer.bloodMageSpecPoints + " Spec. Points");

            if (button1.IsMouseHovering)
                button1.BorderColor = Color.Yellow;
            else
                button1.BorderColor = new Color(25, 25, 25);
            if (button2.IsMouseHovering)
                button2.BorderColor = Color.Yellow;
            else
                button2.BorderColor = new Color(25, 25, 25);
            if (button3.IsMouseHovering)
                button3.BorderColor = Color.Yellow;
            else
                button3.BorderColor = new Color(25, 25, 25);
            if (button4.IsMouseHovering)
                button4.BorderColor = Color.Yellow;
            else
                button4.BorderColor = new Color(25, 25, 25);
            if (button5.IsMouseHovering)
                button5.BorderColor = Color.Yellow;
            else
                button5.BorderColor = new Color(25, 25, 25);
            if (button6.IsMouseHovering)
                button6.BorderColor = Color.Yellow;
            else
                button6.BorderColor = new Color(25, 25, 25);

            if (acmPlayer.specBloodMage_1 == 10)
                button1.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.specBloodMage_2 == 10)
                button1.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.specBloodMage_3 == 10)
                button1.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.specBloodMage_4 == 10)
                button1.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.specBloodMage_5 == 10)
                button1.BackgroundColor = Color.DarkOrange;
            if (acmPlayer.specBloodMage_6 == 10)
                button1.BackgroundColor = Color.DarkOrange;

            base.Update(gameTime);
        }

        private void Button1(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if(acmPlayer.specBloodMage_1 < 10 && acmPlayer.bloodMageTalentPoints > 0)
            {
                acmPlayer.bloodMageTalentPoints--;
                acmPlayer.bloodMageSpentTalentPoints++;
                acmPlayer.specBloodMage_1++;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }

        private void Button2(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specBloodMage_2 < 10 && acmPlayer.bloodMageTalentPoints > 0)
            {
                acmPlayer.bloodMageTalentPoints--;
                acmPlayer.bloodMageSpentTalentPoints++;
                acmPlayer.specBloodMage_2++;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }

        private void Button3(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specBloodMage_3 < 10 && acmPlayer.bloodMageTalentPoints > 0)
            {
                acmPlayer.bloodMageTalentPoints--;
                acmPlayer.bloodMageSpentTalentPoints++;
                acmPlayer.specBloodMage_3++;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }

        private void Button4(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specBloodMage_4 < 10 && acmPlayer.bloodMageTalentPoints > 0)
            {
                acmPlayer.bloodMageTalentPoints--;
                acmPlayer.bloodMageSpentTalentPoints++;
                acmPlayer.specBloodMage_4++;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }

        private void Button5(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specBloodMage_5 < 10 && acmPlayer.bloodMageTalentPoints > 0)
            {
                acmPlayer.bloodMageTalentPoints--;
                acmPlayer.bloodMageSpentTalentPoints++;
                acmPlayer.specBloodMage_5++;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }

        private void Button6(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specBloodMage_6 < 10 && acmPlayer.bloodMageTalentPoints > 0)
            {
                acmPlayer.bloodMageTalentPoints--;
                acmPlayer.bloodMageSpentTalentPoints++;
                acmPlayer.specBloodMage_6++;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }


        private void Close(UIMouseEvent evt, UIElement listeningElement)
        {
            GetInstance<ACM2ModSystem>()._BloodMageTalents.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
    }
}