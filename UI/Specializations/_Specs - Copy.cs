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
//    class _GlobalSpecs : UIState
//    {
//        UIPanel background;
//
//        UIPanel[] specsDefensive;
//
//        UIText specName;
//        UIText specEffect;
//
//        string classSpecificDefensiveName = "Class Specific Name";
//        string classSpecificDefensiveEffect = "Does something";
//        string[] specDefensiveNames;
//        string[] specDefensiveEffects;
//
//        public override void OnInitialize()
//        {
//            string[] specDefensiveNames = { "Health", "Defense", "Dodge", classSpecificDefensiveName};
//            string[] specDefensiveEffects = { "Increases max health by 1%", "Increases defense by 1%", "Increases dodge chance by 1%", classSpecificDefensiveEffect};
//
//            background = new UIPanel();
//            background.Width.Set(500, 0f);
//            background.Height.Set(500, 0f);
//            background.VAlign = -.5f;
//            background.HAlign = .5f;
//            background.BackgroundColor = new Color(75, 75, 75);
//            background.BorderColor = new Color(25, 25, 25);
//            Append(background);
//
//            specName = new UIText("");
//            specName.VAlign = .5f;
//            specName.HAlign = .5f;
//            specName.Top.Set(280, 0f);
//            background.Append(specName);
//
//            specEffect = new UIText("");
//            specEffect.VAlign = .5f;
//            specEffect.HAlign = .5f;
//            specEffect.Top.Set(30, 0f);
//            specName.Append(specEffect);
//
//            //specHealth = new UIPanel();
//            //specHealth.Height.Set(100, 0f);
//            //specHealth.Width.Set(100, 0f);
//            //specHealth.Left.Set(10, 0f);
//            //specHealth.Top.Set(10, 0f);
//            //background.Append(specHealth);
//            //
//            //specDodge = new UIPanel();
//            //specDodge.Height.Set(100, 0f);
//            //specDodge.Width.Set(100, 0f);
//            //specDodge.Left.Set(120, 0f);
//            //specDodge.Top.Set(10, 0f);
//            //background.Append(specDodge);
//
//            InitSpecs();
//            
//
//            base.OnInitialize();
//        }
//
//        void InitSpecs()
//        {
//            specsDefensive = new UIPanel[4];
//
//            for (int s = 0; s < 4; s++)
//            {
//                int top = 10;
//                //if (s < 3) top = 10; if (s > 3 && s < 6) top = 20; if (s > 6) top = 30;
//
//                specsDefensive[s] = new UIPanel();
//                specsDefensive[s].Height.Set(100, 0f);
//                specsDefensive[s].Width.Set(100, 0f);
//                specsDefensive[s].Left.Set(10 + s * 100 + s * 10, 0f);
//                specsDefensive[s].Top.Set(top, 0f);
//                specsDefensive[s].BackgroundColor = Color.LightGreen;
//                specsDefensive[s].BorderColor = Color.DarkGreen;
//                background.Append(specsDefensive[s]);
//            }
//        }
//
//        public override void Update(GameTime gameTime)
//        {
//            HandleBackgroundAnimation();
//
//            specName.SetText("");
//            specEffect.SetText("");
//
//            for(int s = 0; s < 4; s++)
//            {
//                if (specsDefensive[s].IsMouseHovering)
//                {
//                    specName.SetText(specDefensiveNames[s]);
//                    specEffect.SetText(specDefensiveEffects[s]);
//                }
//            }
//            
//            base.Update(gameTime);
//        }
//
//        void HandleBackgroundAnimation()
//        {
//            if (background.VAlign < .5f)
//                background.VAlign += .05f;
//        }
//    }
//
//    class _GlobalSpecsPlayer : ModPlayer
//    {
//        //
//    }
//}