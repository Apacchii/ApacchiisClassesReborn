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
using System.Drawing.Printing;

namespace ApacchiisClassesMod2.UI.HUD
{
    class HUD : UIState
    {
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

        public bool showQuestHUD = true;
        UIText questName;
        UIText questDesc;
        float questVAlign = .01f;
        float questHAlign = .5f;


        float q1;
        float q2;
        float q3;
        //float lifeRegenLeftOffset = GetInstance<Configs.ACMConfigClient>().HealingHUDOffset;

        int blinkTimer = 0;

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

            curBar1 = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/AbilityBar", ReLogic.Content.AssetRequestMode.DoNotLoad).Value);
            curBar1.Height.Set(12, 0f);
            curBar1.Width.Set(0, 0f);
            curBar1.Top.Set(1, 0f);
            curBar1.Left.Set(1, 0f);

            text1 = new UIText("", .75f);
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

            healthRegen = new UIText("", .75f);
            //healthRegen.Left.Set(Main.screenWidth - 24*22, 0f);
            healthRegen.VAlign = .5f;
            healthRegen.HAlign = .5f; //.0825f
            healthRegen.Top.Set(64, 0f);

            #region Class-Specific
            #endregion

            questName = new UIText("", .9f);
            questName.VAlign = questVAlign;
            questName.HAlign = questHAlign;
            questName.Width.Set(500, 0f);
            questName.TextColor = Color.Orange;
            Append(questName);

            questDesc = new UIText("", .8f);
            questDesc.Top.Set(25, 0f);
            questDesc.IsWrapped = true;
            questDesc.Width.Set(500, 0f);
            questName.Append(questDesc);


            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            //Quest HUD
            showQuestHUD = Configs.ACMConfigClient.Instance.showQuestHUD;
            if (showQuestHUD)
            {
                ACMQuests questPlayer = Player.GetModPlayer<ACMQuests>();
                questName.VAlign = questVAlign;
                questName.HAlign = questHAlign;
                questDesc.Width.Set(500, 0f);
                questName.Width.Set(500, 0f);
                questName.SetText($"{questPlayer.questName}");
                questDesc.SetText($"{questPlayer.questDesc}");
            }
            else
            {
                questName.SetText($"");
                questDesc.SetText($"");
            }
            


            blinkTimer++;

            //lifeRegenLeftOffset = GetInstance<Configs.ACMConfigClient>().HealingHUDOffset;
            int healthToRegenTotal = acmPlayer.healthToRegen + acmPlayer.healthToRegenMedium + acmPlayer.healthToRegenSlow + acmPlayer.healthToRegenSnail + acmPlayer.healthToRegenSecond;

            if (healthToRegenTotal > 0)
            {
                Append(healthRegen);
                //healthRegen.HAlign = lifeRegenLeftOffset;
                healthRegen.SetText($"[i:{ItemID.Heart}]{healthToRegenTotal}");
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

            text1.SetText("A1: " + (acmPlayer.ability1Cooldown / 60) + " / " + (int)(acmPlayer.ability1MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability1cdr));
            text2.SetText("A2: " + (acmPlayer.ability2Cooldown / 60) + " / " + (int)(acmPlayer.ability2MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability2cdr));
            text.SetText("Ult: " + acmPlayer.ultCharge + " / " + acmPlayer.ultChargeMax);

            if (acmPlayer.hasBloodMage)
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
            }

            if (acmPlayer.ability1Cooldown > 0)
            {
                q1 = acmPlayer.ability1Cooldown / acmPlayer.ability1MaxCooldown;
                //q1 = Utils.Clamp(q1, 0f, 1f);
                curBar1.Width.Set(q1 * 100, 0f);
            }

            if (acmPlayer.ability2Cooldown > 0)
            {
                q2 = acmPlayer.ability2MaxCooldown / acmPlayer.ability2Cooldown;
                //q2 = Utils.Clamp(q2, 0f, 1f);
                curBar2.Width.Set(q2 * 100, 0f);
                //Main.NewText("" + q2);
            }

            if (acmPlayer.ultCharge > 0)
            {
                q3 = acmPlayer.ultCharge / acmPlayer.ultChargeMax;
                //q3 = Utils.Clamp(q3, 0f, 1f);
                curBar.Width.Set(q3 * 100, 0f);
            }

            if (acmPlayer.compactHUD)
            {
                backBar.Remove();
                backBar1.Remove();
                backBar2.Remove();

                Append(text);
                text.VAlign = .98f;
                text.HAlign = .02f;

                Append(text1);
                text1.VAlign = .98f;
                text1.HAlign = .02f;
                text1.Top.Set(-60, 0f);

                Append(text2);
                text2.VAlign = .98f;
                text2.HAlign = .02f;
                text2.Top.Set(-40, 0f);

                text1.SetText("A1: " + (acmPlayer.ability1Cooldown / 60) + " / " + (int)(acmPlayer.ability1MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability1cdr));
                text2.SetText("A2: " + (acmPlayer.ability2Cooldown / 60) + " / " + (int)(acmPlayer.ability2MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability2cdr));
                text.SetText("Ult: " + acmPlayer.ultCharge + " / " + acmPlayer.ultChargeMax);
            }

            if (acmPlayer.ability1Cooldown <= 0)
                text1.SetText("A1: Ready");
            if (acmPlayer.ability2Cooldown <= 0)
                text2.SetText("A2: Ready");
            if (acmPlayer.ultCharge == acmPlayer.ultChargeMax)
                text.SetText("Ult: Ready");

            if (acmPlayer.blinkingHUD)
            {
                if (blinkTimer == 30)
                {
                    if (acmPlayer.inBattleTimer > -120)
                    {
                        if (acmPlayer.ability1Cooldown == 0)
                            text1.TextColor = Color.Yellow;

                        if (acmPlayer.ability2Cooldown == 0)
                            text2.TextColor = Color.Yellow;

                        if (acmPlayer.ultCharge == acmPlayer.ultChargeMax)
                            text.TextColor = Color.Red;

                        if (!acmPlayer.compactHUD)
                        {
                            if (acmPlayer.ability1Cooldown == 0)
                            {
                                //backBar1.Color = Color.Yellow;
                                curBar1.Color = Color.Yellow;
                            }

                            if (acmPlayer.ability2Cooldown == 0)
                            {
                                //backBar2.Color = Color.Yellow;
                                curBar2.Color = Color.Yellow;
                            }

                            if (acmPlayer.ultCharge == acmPlayer.ultChargeMax)
                            {
                                //backBar.Color = Color.Red;
                                curBar.backgroundColor = Color.Red;
                            }
                        }
                    }
                }
                if (blinkTimer == 60)
                {
                    text1.TextColor = Color.White;
                    text2.TextColor = Color.White;
                    text.TextColor = Color.White;
                    blinkTimer = 0;

                    if (!acmPlayer.compactHUD)
                    {
                        //backBar1.Color = Color.White;
                        curBar1.Color = Color.White;
                        //backBar2.Color = Color.White;
                        curBar2.Color = Color.White;
                        //backBar.Color = Color.White;
                        curBar.backgroundColor = Color.White;
                    }
                }

                if(acmPlayer.ability1Cooldown > 0)
                    text1.TextColor = Color.White;
                if (acmPlayer.ability2Cooldown > 0)
                    text2.TextColor = Color.White;
                if (acmPlayer.ultCharge < acmPlayer.ultChargeMax)
                    text.TextColor = Color.White;
            }
            base.Update(gameTime);
        }
    }
}