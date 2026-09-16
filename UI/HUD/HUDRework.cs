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
using ApacchiisClassesMod2.Configs;
using System.Linq;
using Humanizer;
using System.Data.Odbc;
using Terraria.ModLoader.UI;

namespace ApacchiisClassesMod2.UI.HUD
{
    class HUDRework : UIState
    {
        UIPanel pivotPivot; //lmao
        UIPanel abilitiesPivot;

        UIImage hudCenter;
        UIImage hudCenterFill;
        UIText hudCenterAbilityCooldown;
        UIText hudCenterAbilityName;

        UIImage hudLeft;
        UIImage hudLeftFill;
        UIText hudLeftAbilityCooldown;
        UIText hudLeftAbilityName;

        UIImage hudRight;
        UIImage hudRightFill;
        UIText hudRightAbilityCooldown;
        UIText hudRightAbilityName;

        UIText text;
        UIText inCombat;
        UIText healthRegen;

        public bool showQuestHUD = true;
        UIText questName;
        UIText questDesc;
        float questVAlign = .01f;
        float questHAlign = .5f;

        //Team HUD
        int _playersConnected;
        Bar[] teamPanel;
        UIText[] teamName;
        Bar[] teamHealth;
        Bar[] teamBackHealth;
        UIText[] teamHealthNumber;

        //float lifeRegenLeftOffset = GetInstance<Configs.ACMConfigClient>().HealingHUDOffset;

        UIPanel titaniaHUDPivot;
        Bar titaniaResource;

        int blinkTimer = 0;

        public override void OnInitialize()
        {
            pivotPivot = new UIPanel();
            pivotPivot.VAlign = .5f;
            pivotPivot.HAlign = .5f;
            Append(pivotPivot);

            abilitiesPivot = new UIPanel();
            abilitiesPivot.VAlign = .5f;
            abilitiesPivot.HAlign = .5f;
            abilitiesPivot.Left.Set(-12, 0f);
            abilitiesPivot.Width.Set(400, 0f);
            abilitiesPivot.Height.Set(140, 0f);
            abilitiesPivot.BackgroundColor = new Color(0, 0, 0, 0);
            abilitiesPivot.BorderColor = new Color(0, 0, 0, 0);
            pivotPivot.Append(abilitiesPivot);

            //Ult
            hudCenter = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/Rework/HudCenter", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            hudCenter.ImageScale = 1f;
            hudCenter.VAlign = .5f;
            hudCenter.HAlign = .5f;
            abilitiesPivot.Append(hudCenter);

            hudCenterFill = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/Rework/HudCenterFill", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            hudCenterFill.ImageScale = 1f;
            hudCenterFill.VAlign = .5f;
            hudCenterFill.HAlign = .5f;
            hudCenter.Append(hudCenterFill);

            hudCenterAbilityCooldown = new UIText("", .6f);
            hudCenterAbilityCooldown.VAlign = 0f;
            hudCenterAbilityCooldown.HAlign = .5f;
            hudCenterAbilityCooldown.Top.Set(-12, 0f);
            hudCenterAbilityCooldown.Left.Set(11, 0f);
            hudCenter.Append(hudCenterAbilityCooldown);

            //A1
            hudLeft = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/Rework/HudCenter", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            hudLeft.ImageScale = .75f;
            hudLeft.VAlign = .5f;
            hudLeft.HAlign = .5f;
            hudLeft.Top.Set(30, 0f);
            hudLeft.Left.Set(-30, 0f);
            abilitiesPivot.Append(hudLeft);

            hudLeftFill = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/Rework/HudCenterFill", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            hudLeftFill.ImageScale = .75f;
            hudLeftFill.VAlign = .5f;
            hudLeftFill.HAlign = .5f;
            hudLeft.Append(hudLeftFill);

            hudLeftAbilityCooldown = new UIText("", .6f);
            hudLeftAbilityCooldown.VAlign = 1f;
            hudLeftAbilityCooldown.HAlign = .5f;
            hudLeftAbilityCooldown.Top.Set(3, 0f);
            hudLeftAbilityCooldown.Left.Set(11, 0f);
            hudLeft.Append(hudLeftAbilityCooldown);

            //A2
            hudRight = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/Rework/HudCenter", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            hudRight.ImageScale = .75f;
            hudRight.VAlign = .5f;
            hudRight.HAlign = .5f;
            hudRight.Top.Set(30, 0f);
            hudRight.Left.Set(30, 0f);
            abilitiesPivot.Append(hudRight);

            hudRightFill = new UIImage(Request<Texture2D>("ApacchiisClassesMod2/UI/HUD/Rework/HudCenterFill", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            hudRightFill.ImageScale = .75f;
            hudRightFill.VAlign = .5f;
            hudRightFill.HAlign = .5f;
            hudRight.Append(hudRightFill);

            hudRightAbilityCooldown = new UIText("", .6f);
            hudRightAbilityCooldown.VAlign = 1f;
            hudRightAbilityCooldown.HAlign = .5f;
            hudRightAbilityCooldown.Top.Set(3, 0f);
            hudRightAbilityCooldown.Left.Set(11, 0f);
            hudRight.Append(hudRightAbilityCooldown);

            //-----

            healthRegen = new UIText("", .75f);
            //healthRegen.Left.Set(Main.screenWidth - 24*22, 0f);
            healthRegen.VAlign = .5f;
            healthRegen.HAlign = .5f; //.0825f
            healthRegen.Top.Set(64, 0f);

            #region Class-Specific
            #endregion

            //Quests
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


            //Team HUD
            //Get connected players, except ourselves, unused but might be useful later (does not work, needs per-frame/event updating)
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && Main.myPlayer != i && Main.netMode == NetmodeID.MultiplayerClient && Main.player[i].name != null)
                    _playersConnected++;
            }

            teamPanel = new Bar[10];
            teamHealth = new Bar[10];
            teamBackHealth = new Bar[10];
            teamName = new UIText[10];
            teamHealthNumber = new UIText[10];
            int spacing;

            for (int i = 0; i < 10; i++)
            {    
                //Avoids the panels from skipping 1 slot
                if (i >= Main.myPlayer)
                    spacing = 35;
                else
                    spacing = 0;

                if (Main.myPlayer != i && Main.netMode == NetmodeID.MultiplayerClient)
                {
                    //Invisible background Panels
                    teamPanel[i] = new Bar();
                    teamPanel[i].Top.Set(35 * i - spacing, 0f);
                    teamPanel[i].Width.Set(150, 0f);
                    teamPanel[i].Height.Set(35, 0f);
                    teamPanel[i].VAlign = ACMConfigClient.Instance.teamHUDPlacementVertical;
                    teamPanel[i].HAlign = ACMConfigClient.Instance.teamHUDPlacementHorizontal;
                    teamPanel[i].backgroundColor = new Color(0, 0, 0, 0);

                    //Player Name
                    teamName[i] = new UIText("", .6f);
                    teamName[i].Left.Set(5, 0f);
                    teamName[i].Top.Set(5, 0f);

                    //Health Bar Background
                    teamBackHealth[i] = new Bar();
                    teamBackHealth[i].Width.Set(120, 0f);
                    teamBackHealth[i].Height.Set(10, 0f);
                    teamBackHealth[i].Top.Set(21, 0f);
                    teamBackHealth[i].Left.Set(5, 0f);
                    teamBackHealth[i].backgroundColor = new Color(25, 25, 25);

                    //Health Bar Front
                    teamHealth[i] = new Bar();
                    teamHealth[i].Width.Set(120, 0f);
                    teamHealth[i].Height.Set(10, 0f);
                    teamHealth[i].Top.Set(21, 0f);
                    teamHealth[i].Left.Set(5, 0f);
                    teamHealth[i].backgroundColor = Color.Green;

                    //Player Health Percentage
                    teamHealthNumber[i] = new UIText("", .6f);
                    teamHealthNumber[i].Left.Set(130, 0f);
                    teamHealthNumber[i].Top.Set(21, 0f);
                }
            }

            titaniaHUDPivot = new UIPanel();
            titaniaHUDPivot.VAlign = .5f;
            titaniaHUDPivot.HAlign = .5f;
            titaniaHUDPivot.Height.Set(100, 0f);
            titaniaHUDPivot.Width.Set(100, 0f);
            titaniaHUDPivot.BackgroundColor = new Color(0, 0, 0, 0);
            titaniaHUDPivot.BorderColor = new Color(0, 0, 0, 0);
            //Append(titaniaHUDPivot);

            titaniaResource = new Bar();
            titaniaResource.HAlign = .5f;
            titaniaResource.VAlign = .5f;
            titaniaResource.Top.Set(32, 0f);
            titaniaResource.Width.Set(60, 0f);
            titaniaResource.Height.Set(8, 0f);
            titaniaResource.backgroundColor = Color.Orange;
            //titaniaHUDPivot.Append(titaniaResource);

            base.OnInitialize();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            int A1Cooldown = (int)(acmPlayer.ability1MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability1cdr);
            hudLeftAbilityCooldown.SetText($"{acmPlayer.ability1Cooldown / 60}/{A1Cooldown}");
            if(acmPlayer.ability1Cooldown / 60 == A1Cooldown)
                hudLeftAbilityCooldown.SetText($"Ready");

            if (acmPlayer.ability1Cooldown >= acmPlayer.ability1MaxCooldown)
            {
                if (acmPlayer.ability1Cooldown / 60 >= A1Cooldown)
                    hudLeftFill.Color = Color.White;
                else
                    hudLeftFill.Color = Color.DarkSlateGray;

                float q = (float)acmPlayer.ability1Cooldown / (float)(acmPlayer.ability1MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability1cdr) / 60;
                hudLeftFill.ImageScale = q * .75f;
            }

            int A2Cooldown = (int)(acmPlayer.ability2MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability2cdr);
            hudRightAbilityCooldown.SetText($"{acmPlayer.ability2Cooldown / 60}/{A2Cooldown}");
            if (acmPlayer.ability2Cooldown / 60 == A2Cooldown)
                hudRightAbilityCooldown.SetText($"Ready");

            if (acmPlayer.ability2Cooldown >= A2Cooldown)
            {
                if (acmPlayer.ability2Cooldown / 60 >= acmPlayer.ability2MaxCooldown)
                    hudRightFill.Color = Color.White;
                else
                    hudRightFill.Color = Color.DarkSlateGray;

                float q = (float)acmPlayer.ability2Cooldown / (float)(acmPlayer.ability2MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability2cdr) / 60;
                hudRightFill.ImageScale = q * .75f;
            }

            hudCenterAbilityCooldown.SetText($"{acmPlayer.ultCharge}/{acmPlayer.ultChargeMax}");
            if (acmPlayer.ultCharge == acmPlayer.ultChargeMax)
                hudCenterAbilityCooldown.SetText($"Ready");

            if (acmPlayer.ultCharge > 0)
            {
                if (acmPlayer.ultCharge >= acmPlayer.ultChargeMax)
                    hudCenterFill.Color = Color.White;
                else
                    hudCenterFill.Color = Color.DarkSlateGray;

                float q = (float)acmPlayer.ultCharge / (float)acmPlayer.ultChargeMax;
                hudCenterFill.ImageScale = q;
            }

            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            pivotPivot.VAlign = ACMConfigClient.Instance.reworkedHudVPos;
            pivotPivot.HAlign = ACMConfigClient.Instance.reworkedHudHPos - .09f;

            switch (ACMConfigClient.Instance.hudStyle)
            {
                case "Left":
                    break;

                case "Center":
                    hudCenter.VAlign = .5f;
                    hudCenter.HAlign = .5f;
                    hudCenter.Top.Set(0, 0f);
                    hudCenter.Left.Set(0, 0f);
                    hudCenterAbilityCooldown.VAlign = 0f;
                    hudCenterAbilityCooldown.HAlign = .5f;
                    hudCenterAbilityCooldown.Top.Set(-12, 0f);
                    hudCenterAbilityCooldown.Left.Set(11, 0f);

                    hudLeft.VAlign = .5f;
                    hudLeft.HAlign = .5f;
                    hudLeft.Top.Set(30, 0f);
                    hudLeft.Left.Set(-30, 0f);
                    hudLeftAbilityCooldown.VAlign = 1f;
                    hudLeftAbilityCooldown.HAlign = .5f;
                    hudLeftAbilityCooldown.Top.Set(3, 0f);
                    hudLeftAbilityCooldown.Left.Set(11, 0f);

                    hudRight.VAlign = .5f;
                    hudRight.HAlign = .5f;
                    hudRight.Top.Set(30, 0f);
                    hudRight.Left.Set(30, 0f);
                    hudRightAbilityCooldown.VAlign = 1f;
                    hudRightAbilityCooldown.HAlign = .5f;
                    hudRightAbilityCooldown.Top.Set(3, 0f);
                    hudRightAbilityCooldown.Left.Set(11, 0f);
                    break;

                case "Right":
                    break;
            }

            if (ACMConfigClient.Instance.teamHUD)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Main.player[i].active && Main.myPlayer != i && Main.netMode == NetmodeID.MultiplayerClient && Main.player[i].team == Main.player[Main.myPlayer].team && Main.player[Main.myPlayer].team != 0)
                    {
                        Append(teamPanel[i]);
                        teamPanel[i].Append(teamName[i]);
                        teamPanel[i].Append(teamBackHealth[i]);
                        teamPanel[i].Append(teamHealth[i]);
                        teamPanel[i].Append(teamHealthNumber[i]);

                        //!! - Type '/acr rhud' in chat for these changes to update! <- text for config
                        teamPanel[i].VAlign = ACMConfigClient.Instance.teamHUDPlacementVertical;
                        teamPanel[i].HAlign = ACMConfigClient.Instance.teamHUDPlacementHorizontal;

                        //Name
                        if (!Main.player[i].dead)
                            teamName[i].SetText($"{Main.player[i].name} [{Main.player[i].GetModPlayer<ACMPlayer>().equippedClass}]");
                        else
                            teamName[i].SetText($"{Main.player[i].name} [Respawning in: {Main.player[i].respawnTimer / 60}s]");

                        //Health
                        int _healingPlayerIsTaking = Main.player[i].GetModPlayer<ACMPlayer>().healthToRegen + Main.player[i].GetModPlayer<ACMPlayer>().healthToRegenMedium + Main.player[i].GetModPlayer<ACMPlayer>().healthToRegenSlow + Main.player[i].GetModPlayer<ACMPlayer>().healthToRegenSnail + Main.player[i].GetModPlayer<ACMPlayer>().healthToRegenSecond;
                        float _fill = (float)Main.player[i].statLife / (float)Main.player[i].statLifeMax2;
                        teamHealth[i].Width.Set(120f * _fill, 0f);
                        teamHealth[i].backgroundColor = Color.Lerp(Color.Red, Color.Green, _fill);
                        if (_healingPlayerIsTaking <= 0) //Sometimes bugs, does not update values for other players, ininitely stacks and later randomly resets
                            teamHealthNumber[i].SetText($"{((float)Main.player[i].statLife / (float)Main.player[i].statLifeMax2 * 100f).ToString("F0")}%");
                        else
                            teamHealthNumber[i].SetText($"{((float)Main.player[i].statLife / (float)Main.player[i].statLifeMax2 * 100f).ToString("F0")}% [c/90ee90:+{_healingPlayerIsTaking}]");
                    }
                    else
                    {
                        if (teamPanel[i] != null)
                        {
                            teamPanel[i].RemoveAllChildren();
                            teamPanel[i].Remove();
                        }
                    }
                }
                
            }

            //Titania Resource Bar
            if (acmPlayer.titaniaPhaseResource < 120f)
            {
                Append(titaniaHUDPivot);
                titaniaHUDPivot.Append(titaniaResource);
            }
            else if (acmPlayer.titaniaPhaseResource >= 120f && acmPlayer.globalTickTimer % 120 == 0)
                titaniaHUDPivot.Remove();

            float opResourceQ = (float)acmPlayer.titaniaPhaseResource / 120f;
            titaniaResource.Width.Set(60f * opResourceQ, 0f);
            if(acmPlayer.titaniaCanPhase)
                titaniaResource.backgroundColor = Color.Lerp(Color.Red, Color.Orange, opResourceQ);
            else
                titaniaResource.backgroundColor = Color.Red;

            //Quest HUD
            showQuestHUD = ACMConfigClient.Instance.showQuestHUD;
            if (showQuestHUD)
            {
                ACMQuests questPlayer = Player.GetModPlayer<ACMQuests>();
                questName.VAlign = questVAlign;
                questName.HAlign = questHAlign;
                questDesc.Width.Set(ACMConfigClient.Instance.questDescTextWidth, 0f);
                questName.Width.Set(ACMConfigClient.Instance.questDescTextWidth, 0f);
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

            //--text1.SetText("");
            //--text1.SetText("");

            //if (acmPlayer.ability1Cooldown <= 0)
            //    backBar1.Append(curBar1);
            //else
            //    curBar1.Remove();
            //
            //if (acmPlayer.ability2Cooldown <= 0)
            //    backBar2.Append(curBar2);
            //else
            //    curBar2.Remove();

            //--inCombat.SetText($"{(acmPlayer.inBattleTimer / 60 + 1)}");
            //--if (acmPlayer.inBattleTimer > 0)
            //--    backBar.Append(inCombat);
            //--else
            //--    inCombat.Remove();

            //float q;
            //q = (float)acmPlayer.ability1Cooldown / (float)acmPlayer.ability1MaxCooldown;
            //curBar1.Width.Set(q * 100, 0f);
            //
            //q = (float)acmPlayer.ability2Cooldown / (float)acmPlayer.ability2MaxCooldown;
            //curBar2.Width.Set(q * 100, 0f);

            //--if(acmPlayer.ability1MaxCharges > 1)
            //--    text1.SetText($"Charges: {acmPlayer.ability1Charges} / {acmPlayer.ability1MaxCharges}");
            //--else
            //--    text1.SetText($"A1: {acmPlayer.ability1Cooldown / 60} / {(int)(acmPlayer.ability1MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability1cdr)}");
            //--text2.SetText("A2: " + (acmPlayer.ability2Cooldown / 60) + " / " + (int)(acmPlayer.ability2MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability2cdr));
            //--text.SetText("Ult: " + acmPlayer.ultCharge + " / " + acmPlayer.ultChargeMax);
            

            if (acmPlayer.hasBloodMage)
            {
                if (acmPlayer.bloodMageBloodEnchantment)
                {
                    //--text2.SetText("Toggled: On");
                    //--backBar2.Append(curBar2);
                    //--backBar2.Width.Set(100, 0f);
                }
                else
                {
                    //--text2.SetText("Toggled: Off");
                    //--curBar2.Remove();
                }
            }

            if (acmPlayer.compactHUD)
            {
                //--backBar.Remove();
                //--backBar1.Remove();
                //--backBar2.Remove();

                //--Append(text);
                //--text.VAlign = .98f;
                //--text.HAlign = .02f;

                //--Append(text1);
                //--text1.VAlign = .98f;
                //--text1.HAlign = .02f;
                //--text1.Top.Set(-60, 0f);

                //--Append(text2);
                //--text2.VAlign = .98f;
                //--text2.HAlign = .02f;
                //--text2.Top.Set(-40, 0f);

                //--text1.SetText("A1: " + (acmPlayer.ability1Cooldown / 60) + " / " + (int)(acmPlayer.ability1MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability1cdr));
                //--text2.SetText("A2: " + (acmPlayer.ability2Cooldown / 60) + " / " + (int)(acmPlayer.ability2MaxCooldown * acmPlayer.cooldownReduction * acmPlayer.ability2cdr));
                //--text.SetText("Ult: " + acmPlayer.ultCharge + " / " + acmPlayer.ultChargeMax);
            }

            //--if (acmPlayer.ability1Cooldown <= 0 && acmPlayer.ability1MaxCharges <= 1)
            //--    text1.SetText("A1: Ready");
            //--if (acmPlayer.ability2Cooldown <= 0 && acmPlayer.ability2MaxCharges <= 1)
            //--    text2.SetText("A2: Ready");
            //--if (acmPlayer.ultCharge == acmPlayer.ultChargeMax)
            //--    text.SetText("Ult: Ready");

            if (acmPlayer.blinkingHUD)
            {
                if (blinkTimer == 30)
                {
                    if (acmPlayer.inBattleTimer > -120)
                    {
                        //--if (acmPlayer.ability1Cooldown == 0)
                        //--    text1.TextColor = Color.Yellow;
                        //--
                        //--if (acmPlayer.ability2Cooldown == 0)
                        //--    text2.TextColor = Color.Yellow;

                        //--if (acmPlayer.ultCharge == acmPlayer.ultChargeMax)
                        //--    text.TextColor = Color.Red;

                        if (!acmPlayer.compactHUD)
                        {
                            if (acmPlayer.ability1Cooldown == 0)
                            {
                                //backBar1.Color = Color.Yellow;
                                //--curBar1.backgroundColor = Color.Yellow;
                            }

                            if (acmPlayer.ability2Cooldown == 0)
                            {
                                //backBar2.Color = Color.Yellow;
                                //--curBar2.backgroundColor = Color.Yellow;
                            }

                            if (acmPlayer.ultCharge == acmPlayer.ultChargeMax)
                            {
                                //backBar.Color = Color.Red;
                                //--curBar.backgroundColor = Color.Red;
                            }
                        }
                    }
                }
                if (blinkTimer == 60)
                {
                    //--text1.TextColor = Color.White;
                    //--text2.TextColor = Color.White;
                    //--text.TextColor = Color.White;
                    blinkTimer = 0;

                    if (!acmPlayer.compactHUD)
                    {
                        //--curBar.backgroundColor = Color.White;
                        //--curBar1.backgroundColor = Color.White;
                        //--curBar2.backgroundColor = Color.White;
                    }
                }

                //--if(acmPlayer.ability1Cooldown > 0)
                //--    text1.TextColor = Color.White;
                //--if (acmPlayer.ability2Cooldown > 0)
                //--    text2.TextColor = Color.White;
                //--if (acmPlayer.ultCharge < acmPlayer.ultChargeMax)
                //--    text.TextColor = Color.White;
            }
            base.Update(gameTime);
        }
    }
}