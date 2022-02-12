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

namespace ApacchiisClassesMod2.UI.HUD
{
    class HUD : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        UIImage backBar1;
        UIImage curBar1;
        UIText text1;
        UIImage backBar2;
        UIImage curBar2;
        UIText text2;
        UIImage backBar;
        Bar curBar;
        UIText text;
        UIText inCombat;
        UIText healthRegen;

        UIText Blood;

        float q = 1f;
        float lifeRegenLeftOffset = GetInstance<ACMConfigClient>().HealingHUDOffset;

        public override void OnInitialize()
        {
            backBar = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/BackBar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            backBar.VAlign = .94f; // Pivot
            backBar.HAlign = .02f;
            backBar.Width.Set(102, 0f);
            backBar.Height.Set(22, 0f);
            Append(backBar);

            curBar = new Bar();
            curBar.Height.Set(20, 0f);
            curBar.Width.Set(0, 0f);
            curBar.Top.Set(1, 0f);
            curBar.Left.Set(1, 0f);
            backBar.Append(curBar);

            text = new UIText("", .75f);
            text.Top.Set(-20, 0f);
            text.HAlign = .5f;
            backBar.Append(text);

            inCombat = new UIText("", .55f);
            inCombat.VAlign = .5f;
            inCombat.Left.Set(110, 0f);
            backBar.Append(inCombat);



            backBar1 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/AbilityBack", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            backBar1.Top.Set(-75, 0f);
            backBar1.Width.Set(102, 0f);
            backBar1.Height.Set(14, 0f);
            backBar.Append(backBar1);

            curBar1 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/AbilityBar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            curBar1.Height.Set(12, 0f);
            curBar1.Width.Set(0, 0f);
            curBar1.Top.Set(1, 0f);
            curBar1.Left.Set(1, 0f);

            text1 = new UIText("", .65f);
            text1.Top.Set(-15, 0f);
            text1.HAlign = .5f;
            backBar1.Append(text1);



            backBar2 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/AbilityBack", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            backBar2.Top.Set(-40, 0f);
            backBar2.Width.Set(102, 0f);
            backBar2.Height.Set(14, 0f);
            backBar.Append(backBar2);

            curBar2 = new UIImage((Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/AbilityBar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value));
            curBar2.Height.Set(12, 0f);
            curBar2.Width.Set(0, 0f);
            curBar2.Top.Set(1, 0f);
            curBar2.Left.Set(1, 0f);

            text2 = new UIText("", .75f);
            text2.Top.Set(-15, 0f);
            text2.HAlign = .5f;
            backBar2.Append(text2);

            healthRegen = new UIText("");
            //healthRegen.Left.Set(Main.screenWidth - 24*22, 0f);
            healthRegen.VAlign = .04f;
            healthRegen.HAlign = lifeRegenLeftOffset; //.0825f

            #region Class-Specific
            Blood = new UIText("Blood: ", .8f);
            Blood.Top.Set(-45, 0f);
            //Blood.HAlign = .5f;
            #endregion

            base.OnInitialize();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            q = (float)(acmPlayer.ultCharge / acmPlayer.ultChargeMax);
            q = Utils.Clamp(q, 0f, 1f);

            curBar.Width.Set(q * 100, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            lifeRegenLeftOffset = GetInstance<ACMConfigClient>().HealingHUDOffset;
            int healthToRegenTotal = acmPlayer.healthToRegen + acmPlayer.healthToRegenMedium + acmPlayer.healthToRegenSlow + acmPlayer.healthToRegenSnail;

            if(healthToRegenTotal > 0)
            {
                Append(healthRegen);
                healthRegen.HAlign = lifeRegenLeftOffset;
                healthRegen.SetText($"{healthToRegenTotal} [i:{ItemID.Heart}]  >", 1.25f, false);
                healthRegen.TextColor = Color.LightGreen;
            }
            else
            {
                healthRegen.SetText("");
                healthRegen.Remove();
            }

            
            text1.SetText("");
            text1.SetText("");

            if (acmPlayer.ability1Cooldown <= 0)
                backBar1.Append(curBar1);
            else
                curBar1.Remove();

            if (acmPlayer.ability2Cooldown <= 0)
                backBar2.Append(curBar2);
            else
                curBar2.Remove();

            inCombat.SetText("" + (acmPlayer.inBattleTimer / 60 + 1));
            if (acmPlayer.inBattleTimer > 0)
                backBar.Append(inCombat);
            else
                inCombat.Remove();

            //q = acmPlayer.ability1Cooldown / acmPlayer.ability1MaxCooldown;
            //curBar1.Width.Set(q * -100, 0f);
            //
            //q = acmPlayer.ability2Cooldown / acmPlayer.ability2MaxCooldown;
            //curBar2.Width.Set(q * -100, 0f);

            text1.SetText((acmPlayer.ability1Cooldown / 60) + " / " + (int)(acmPlayer.ability1MaxCooldown * acmPlayer.cooldownReduction));
            text2.SetText((acmPlayer.ability2Cooldown / 60) + " / " + (int)(acmPlayer.ability2MaxCooldown * acmPlayer.cooldownReduction));
            text.SetText(acmPlayer.ultCharge + " / " + acmPlayer.ultChargeMax);

            if(acmPlayer.hasBloodMage)
            {
                if (acmPlayer.bloodMageBloodEnchantment)
                {
                    text2.SetText("Toggled: On");
                    backBar2.Append(curBar2);
                }
                else
                {
                    text2.SetText("Toggled: Off");
                    curBar2.Remove();
                }

                backBar1.Append(Blood);
                Blood.SetText("Blood: " + acmPlayer.bloodMagePassiveCurrentStacks + " / " + acmPlayer.bloodMagePassiveMaxStacks);
            }
            else
            {
                Blood.Remove(); 
            }

            base.Update(gameTime);
        }
    }
}