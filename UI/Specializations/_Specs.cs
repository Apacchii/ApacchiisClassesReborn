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
    class Specs : UIState
    {
        UIPanel background;

        UIPanel[] spec;
        UIText[] specName;
        UIText[] specEffect;

        public override void OnInitialize()
        {
            background = new UIPanel();
            background.Width.Set(500, 0f);
            background.Height.Set(500, 0f);
            background.VAlign = -.5f;
            background.HAlign = .5f;
            background.BackgroundColor = new Color(75, 75, 75);
            background.BorderColor = new Color(25, 25, 25);
            Append(background);

            InitSpecs();
            
            base.OnInitialize();
        }

        void InitSpecs()
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            string[] specText = acmPlayer.classSpecsText;
            spec = new UIPanel[acmPlayer.classSpecsText.Length];

            for (int x = 0; x < acmPlayer.classSpecsText.Length; x++)
            {
                int top = 10;
                //if (x < 3) top = 10; if (x > 3 && x < 6) top = 20; if (x > 6) top = 30;

                spec[x] = new UIPanel();
                spec[x].Height.Set(100, 0f);
                spec[x].Width.Set(100, 0f);
                spec[x].Left.Set(10 + x * 100 + x * 10, 0f);
                spec[x].Top.Set(top, 0f);
                spec[x].BackgroundColor = Color.LightGreen;
                spec[x].BorderColor = Color.DarkGreen;
                background.Append(spec[x]);

                specName[x] = new UIText("");
                specName[x].VAlign = .5f;
                specName[x].HAlign = .5f;
                spec[x].Append(specName[x]);

                //specEffect[x] = new UIText("");
                //specEffect[x].VAlign = .5f;
                //specEffect[x].HAlign = .5f;
                //specEffect[x].Top.Set(30, 0f);
                //specName[x].Append(specEffect[x]);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            HandleBackgroundAnimation();

            for (int x = 0; x < acmPlayer.classSpecsText.Length; x++)
            {
                specName[x].SetText(acmPlayer.classSpecsText[x]);
            }
            
            base.Update(gameTime);
        }

        void HandleBackgroundAnimation()
        {
            if (background.VAlign < .5f)
                background.VAlign += .05f;
        }
    }

    class _GlobalSpecsPlayer : ModPlayer
    {
        //
    }
}