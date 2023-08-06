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
    class VanguardSpecs : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        float bWidth = 64f;
        float bHeight = 64f;

        float l_node1 = -74 * 2;
        float l_node2 = -74;
        float l_node4 = 74;
        float l_node5 = 74 * 2;

        string className;
        int skillPoints;
        int spentSkillPoints;

        UIPanel background;
        UIText classText;
        UIText effect;
        UIText effect2;

        UIPanel close;
        UIText closeText;

        UIImage button1;
        UIImage button2;
        UIImage button3;
        UIImage button4;
        UIImage button5;

        UIText text1;
        UIText text2;
        UIText text3;
        UIText text4;
        UIText text5;

        public override void OnInitialize()
        {
            className = Player.GetModPlayer<ACMPlayer>().equippedClass;
            background = new UIPanel();
            background.VAlign = .5f;
            background.HAlign = .5f;
            background.Width.Set(700, 0f);
            background.Height.Set(250, 0f);
            background.BackgroundColor = new Color(75, 75, 75);
            background.BorderColor = new Color(25, 25, 25);
            Append(background);

            effect = new UIText("");
            effect.VAlign = .5f;
            effect.HAlign = .5f;
            effect.Top.Set(64, 0f);
            Append(effect);

            effect2 = new UIText("");
            effect2.VAlign = .5f;
            effect2.HAlign = .5f;
            effect2.Top.Set(94, 0f);
            Append(effect2);

            close = new UIPanel();
            close.VAlign = .5f;
            close.HAlign = .5f;
            close.Top.Set(-155, 0f);
            close.Width.Set(700, 0f);
            close.Height.Set(50, 0f);
            close.BackgroundColor = new Color(150, 75, 75);
            close.BorderColor = new Color(25, 25, 25);
            //close.OnClick += Close;
            Append(close);

            closeText = new UIText("Close");
            closeText.VAlign = .5f;
            closeText.HAlign = .5f;
            //closeText.OnClick += Close;
            close.Append(closeText);

            classText = new UIText($"Specializations: {className}");
            classText.Top.Set(5, 0f);
            classText.HAlign = .5f;
            background.Append(classText);



            button1 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Heart").Value);
            button1.Width.Set(bWidth, 0f);
            button1.Height.Set(bHeight, 0f);
            button1.VAlign = .4f;
            button1.HAlign = .5f;
            button1.Left.Set(l_node1, 0f);
            button1.OnLeftClick += Button1_Left;
            button1.OnRightClick += Button1_Right;
            background.Append(button1);

            text1 = new UIText("0/20");
            text1.VAlign = .5f;
            text1.HAlign = .5f;
            text1.Top.Set(47, 0f);
            button1.Append(text1);



            button2 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Sword").Value);
            button2.Width.Set(bWidth, 0f);
            button2.Height.Set(bHeight, 0f);
            button2.VAlign = .4f;
            button2.HAlign = .5f;
            button2.Left.Set(l_node2, 0f);
            button2.OnLeftClick += Button2_Left;
            button2.OnRightClick += Button2_Right;
            background.Append(button2);

            text2 = new UIText("0/20");
            text2.VAlign = .5f;
            text2.HAlign = .5f;
            text2.Top.Set(47, 0f);
            button2.Append(text2);



            button3 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Ability").Value);
            button3.Width.Set(bWidth, 0f);
            button3.Height.Set(bHeight, 0f);
            button3.VAlign = .4f;
            button3.HAlign = .5f;
            button3.OnLeftClick += Button3_Left;
            button3.OnRightClick += Button3_Right;
            background.Append(button3);

            text3 = new UIText("0/20");
            text3.VAlign = .5f;
            text3.HAlign = .5f;
            text3.Top.Set(47, 0f);
            button3.Append(text3);



            button4 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Healing").Value);
            button4.Width.Set(bWidth, 0f);
            button4.Height.Set(bHeight, 0f);
            button4.VAlign = .4f;
            button4.HAlign = .5f;
            button4.Left.Set(l_node4, 0f);
            button4.OnLeftClick += Button4_Left;
            button4.OnRightClick += Button4_Right;
            background.Append(button4);

            text4 = new UIText("0/20");
            text4.VAlign = .5f;
            text4.HAlign = .5f;
            text4.Top.Set(47, 0f);
            button4.Append(text4);



            button5 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Hourglass").Value);
            button5.Width.Set(bWidth, 0f);
            button5.Height.Set(bHeight, 0f);
            button5.VAlign = .4f;
            button5.HAlign = .5f;
            button5.Left.Set(l_node5, 0f);
            button5.OnLeftClick += Button5_Left;
            button5.OnRightClick += Button5_Right;
            background.Append(button5);

            text5 = new UIText("0/20");
            text5.VAlign = .5f;
            text5.HAlign = .5f;
            text5.Top.Set(47, 0f);
            button5.Append(text5);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            skillPoints = acmPlayer.vanguardSkillPoints;
            spentSkillPoints = acmPlayer.vanguardSpentSkillPoints;

            effect.SetText("");
            effect2.SetText("");

            text1.SetText($"{acmPlayer.specVanguard_Defense}/20");
            text2.SetText($"{acmPlayer.specVanguard_MeleeDamage}/20");
            text3.SetText($"{acmPlayer.specVanguard_ShieldDamageReduction}/20");
            text4.SetText($"{acmPlayer.specVanguard_SpearDamage}/20");
            text5.SetText($"{acmPlayer.specVanguard_UltCost}/20");

            if (close.IsMouseHovering || button1.IsMouseHovering || button2.IsMouseHovering || button3.IsMouseHovering || button4.IsMouseHovering || button5.IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;

            classText.SetText($"{className}: {acmPlayer.vanguardSkillPoints} Skill Points");

            if (button1.IsMouseHovering)
            {
                button1.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Shield_S").Value);
                effect.SetText($"Increases defense by {(decimal)(acmPlayer.specVanguard_DefenseBase * 100)}% per point. [{(decimal)(acmPlayer.specVanguard_Defense * acmPlayer.specVanguard_DefenseBase * 100)}%]");
            }
            else
            {
                button1.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Shield").Value);
            }

            if (button2.IsMouseHovering)
            {
                button2.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Sword_S").Value);
                effect.SetText($"Increases melee damage by {(decimal)(acmPlayer.specVanguard_MeleeDamageBase * 100)}% per point. [{(decimal)(acmPlayer.specVanguard_MeleeDamage * acmPlayer.specVanguard_MeleeDamageBase * 100)}%]");
            }
            else
            {
                button2.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Sword").Value);
            }

            if (button3.IsMouseHovering)
            {
                button3.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Shield_S").Value);
                effect.SetText($"Increases [Barrier Of Light]'s damage reduction by {(decimal)(acmPlayer.specVanguard_ShieldDamageReductionBase * 100)}% per point. [{(decimal)(acmPlayer.specVanguard_ShieldDamageReduction * acmPlayer.specVanguard_ShieldDamageReductionBase * 100)}%]");
            }
            else
            {
                button3.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Shield").Value);
            }

            if (button4.IsMouseHovering)
            {
                button4.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Ability_S").Value);
                effect.SetText($"Increases [Spear Of Light]'s explosion's damage by {acmPlayer.specVanguard_SpearDamageBase} per point. [{acmPlayer.specVanguard_SpearDamage * acmPlayer.specVanguard_SpearDamageBase}]");
            }
            else
            {
                button4.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Ability").Value);
            }

            if (button5.IsMouseHovering)
            {
                button5.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Hourglass_S").Value);
                effect.SetText($"Decreases ult cost by {(decimal)(acmPlayer.specVanguard_UltCostBase * 100)}% per point. [{(decimal)(acmPlayer.specVanguard_UltCost * acmPlayer.specVanguard_UltCostBase * 100)}%]");
            }
            else
            {
                button5.SetImage(Request<Texture2D>("ApacchiisClassesMod2/UI/SpecsDraw/Hourglass").Value);
            }

            base.Update(gameTime);
        }

        private void Button1_Left(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if(acmPlayer.specVanguard_Defense < 20 && skillPoints > 0 && spentSkillPoints >= 10)
            {
                acmPlayer.vanguardSkillPoints--;
                acmPlayer.vanguardSpentSkillPoints++;
                acmPlayer.specVanguard_Defense++;
            }
        }

        private void Button1_Right(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if(acmPlayer.specVanguard_Defense > 0)
            {
                acmPlayer.vanguardSkillPoints++;
                acmPlayer.vanguardSpentSkillPoints--;
                acmPlayer.specVanguard_Defense--;
            }
        }



        private void Button2_Left(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_MeleeDamage < 20 && skillPoints > 0 && spentSkillPoints >= 10)
            {
                acmPlayer.vanguardSkillPoints--;
                acmPlayer.vanguardSpentSkillPoints++;
                acmPlayer.specVanguard_MeleeDamage++;
            }
        }

        private void Button2_Right(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_MeleeDamage > 0)
            {
                acmPlayer.vanguardSkillPoints++;
                acmPlayer.vanguardSpentSkillPoints--;
                acmPlayer.specVanguard_MeleeDamage--;
            }
        }



        private void Button3_Left(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_ShieldDamageReduction < 20 && skillPoints > 0 && spentSkillPoints >= 10)
            {
                acmPlayer.vanguardSkillPoints--;
                acmPlayer.vanguardSpentSkillPoints++;
                acmPlayer.specVanguard_ShieldDamageReduction++;
            }
        }

        private void Button3_Right(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_ShieldDamageReduction > 0)
            {
                acmPlayer.vanguardSkillPoints++;
                acmPlayer.vanguardSpentSkillPoints--;
                acmPlayer.specVanguard_ShieldDamageReduction--;
            }
        }



        private void Button4_Left(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_SpearDamage < 20 && skillPoints > 0 && spentSkillPoints >= 10)
            {
                acmPlayer.vanguardSkillPoints--;
                acmPlayer.vanguardSpentSkillPoints++;
                acmPlayer.specVanguard_SpearDamage++;
            }
        }

        private void Button4_Right(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_SpearDamage > 0)
            {
                acmPlayer.vanguardSkillPoints++;
                acmPlayer.vanguardSpentSkillPoints--;
                acmPlayer.specVanguard_SpearDamage--;
            }
        }



        private void Button5_Left(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_UltCost < 20 && skillPoints > 0 && spentSkillPoints >= 10)
            {
                acmPlayer.vanguardSkillPoints--;
                acmPlayer.vanguardSpentSkillPoints++;
                acmPlayer.specVanguard_UltCost++;
            }
        }

        private void Button5_Right(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if (acmPlayer.specVanguard_UltCost > 0)
            {
                acmPlayer.vanguardSkillPoints++;
                acmPlayer.vanguardSpentSkillPoints--;
                acmPlayer.specVanguard_UltCost--;
            }
        }
    }
}