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
using Terraria.Localization;
using Terraria.GameContent;
using System.Drawing.Printing;

namespace ApacchiisClassesMod2.UI
{
    class ClassesMenu : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        int abilityPanelHeight = 50;
        int abilityPanelWidth = 140;
        int abilityTop = 60;
        bool tick = false;

        public string selectedClass = "";

        //UIPanel background;
        UIImage background;
        UIText className;
        UIPanel buttonTalents;
        UIText talentsText;
        UIPanel relicsButton;
        UIText relicsText;
        UIPanel specsButton;
        UIText specsText;
        UIPanel tips;
        UIText tipsText;
        UIPanel extraFunctionButton;
        UIText extraFunctionText;


        UIPanel passiveButton;
        UIText passiveStaticText;
        UIPanel ability1Button;
        UIText ability1StaticText;
        UIPanel ability2Button;
        UIText ability2StaticText;
        UIPanel ability3Button;
        UIText ability3StaticText;
        
        UIText abilityName;
        UIText abilityText;
        UIText abilityCooldown;
        UIText ultimateCharge;
        UIText abilityEffect1;
        UIText abilityEffect2;
        UIText abilityEffect3;
        UIText abilityEffect4;
        UIText castType;

        UIPanel questPanel;
        UIText questText;
        public int goldGainedFromQuests;

        int tipTimer = 0;
        string[] donoName =
        {
            "Names"
        };

        string[] tip =
        {
            $"[i:{ItemID.Heart}] Thank you for your support, @CaineSenpai! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Capraeus! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Degeneracy! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Derin! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Dr.Oktober! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Dr.Void! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Eloraeon! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Chelsea! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Grass! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Grumpy! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Icarus'Bullet! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Jesus Wie D! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @MajorCare! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Matty! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Peanutbutta187! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @ris! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Serenity! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Sheseck! [i:{ItemID.Heart}]",



            "You can change the max level a class can reach in the mod's config (10-100).",
            $"You level up each time a boss is defeated, you can see which bosses you've defeated using a 'Class Book' [i:{ItemType<Items.ClassBook>()}].",
            $"The 'Class Book' [i:{ItemType<Items.ClassBook>()}] displays bosses defeated in alphabetical order.",
            "Ability Power increases how much effect, such as damage, an ability has.",
            "Cooldown Reduction reduces the cooldown of all your non-ultimate abilities.",
            $"You can craft new classes by crafting 'White Cloth' [i:{ItemType<Items.WhiteCloth>()}] on a 'Loom' [i:{ItemID.Loom}].",
            "You can make it so classes don't give you bonus stats by enabling 'Hidden Accessory Disables Stats' in the mod's config.", // Limit for text
            "You build up your ultimate by being in battle. You are considered to be in battle for 3s everytime you hit or are hit.",
            "Your ultimate build up decays at half the rate it builds up if you haven't been in battle for 5 seconds.",
            "Statue-spawned enemies do not count towards your highest dps, highest crit, enemies killed and damage dealt stats.",
            "If your HUD ever gets stuck and doesn't update anymore, you can type '/acr resetHUD' in chat and it'll fix itself.",
            "Join the discord on the mod's description/workshop page for help, reporting bugs, or just chatting.",
            "This mod is still in development, more classes will come in the future!",
            "Skill Points are awarded every time you level up, spend them by clicking on one of the buttons below!",
            "The HUD on the bottom left of the screen will tell you the current cooldown of all your abilities.",
            "Some vanilla items grant you increased ability power, reduced ability cooldowns and reduced ultimate costs.",
            "This menu can also be opened via a hotkey, you just need to assign one under Settings > Controls > ACM2: Menu.",
            "Enemies deal 5% more damage overall by default. This can be changed in the mod's config.",
            "Relics can be equipped right next to the class' banner slot.",
            "Relics have a low chance to drop from any non-boss enemy defeated.",
            "If you ever change your mind, right clicking your class' specializations removes points so you can use them elsewhere.",
            "Base health is the health you get from anything but class stats.",
            "Base defense is the defense you get from anything but class stats.",
            "All relics have the same drop chance.",
            $"This server's settings has a {(Configs._ACMConfigServer.Instance.classStatMult).ToString("F2")}x multiplier for class stats!",
            $"This server's settings has a {(Configs._ACMConfigServer.Instance.enemyDamageMultiplier).ToString("F2")}x multiplier for all enemy damage!",
            $"Healing Power does NOT affect potions [i:{ItemID.LesserHealingPotion}] or healing from other mods!",
            $"You get Ability Power based on your currently held weapon's base DPS, the higher it is, the more you get!",
            $"Bottom left HUD too big or intrusive for your liking ? Enable 'Compact HUD' on the mod's Client Config!",
            $"Do you keep forgetting to use your abilities ? Enable 'Blinking HUD' on the mod's Client Config!",
            $"Daily quests reset everyday at 4:30am."
        };
        int chosenTip = 0;
        int prevTip = -1;
        int tipsNumber;

        public override void OnInitialize()
        {
            tipTimer = 0;
            tipsNumber = tip.Length;
            selectedClass = Player.GetModPlayer<ACMPlayer>().equippedClass;
            chosenTip = Main.rand.Next(0, tipsNumber);
            if(chosenTip == prevTip)
                chosenTip = Main.rand.Next(0, tipsNumber);
            prevTip = chosenTip;
            //background = new UIPanel();
            //background.HAlign = .5f;
            //background.VAlign = .5f;
            //background.Height.Set(500, 0f);
            //background.Width.Set(900, 0f);
            //background.BackgroundColor = new Color(75, 75, 75);
            //background.BorderColor = new Color(25, 25, 25);
            //Append(background);
            
            background = new UIImage((Texture2D)Request<Texture2D>("ApacchiisClassesMod2/UI/background"));
            background.VAlign = .5f;
            background.HAlign = .5f;
            Append(background);

            className = new UIText("");
            className.VAlign = .03f;
            className.HAlign = .5f;
            background.Append(className);

            buttonTalents = new UIPanel();
            buttonTalents.HAlign = .5f;
            buttonTalents.VAlign = .5f;
            buttonTalents.Top.Set(335, 0f);
            buttonTalents.Left.Set(110, 0f);
            buttonTalents.Width.Set(200, 0f);
            buttonTalents.Height.Set(50, 0f);
            buttonTalents.OnLeftClick += OpenTalents;
            buttonTalents.BackgroundColor = new Color(75, 75, 75);
            buttonTalents.BorderColor = new Color(25, 25, 25);
            Append(buttonTalents);

            talentsText = new UIText("Talents");
            talentsText.VAlign = .5f;
            talentsText.HAlign = .5f;
            buttonTalents.Append(talentsText);

            relicsButton = new UIPanel();
            relicsButton.HAlign = .5f;
            relicsButton.VAlign = .5f;
            relicsButton.Top.Set(335, 0f);
            relicsButton.Left.Set(-310, 0f);
            relicsButton.Width.Set(200, 0f);
            relicsButton.Height.Set(50, 0f);
            relicsButton.OnLeftClick += OpenRelics;
            relicsButton.BackgroundColor = new Color(75, 75, 75);
            relicsButton.BorderColor = new Color(25, 25, 25);
            Append(relicsButton);

            relicsText = new UIText("Relics List");
            relicsText.VAlign = .5f;
            relicsText.HAlign = .5f;
            relicsButton.Append(relicsText);

            specsButton = new UIPanel();
            specsButton.HAlign = .5f;
            specsButton.VAlign = .5f;
            specsButton.Top.Set(335, 0f);
            specsButton.Left.Set(-100, 0f);
            specsButton.Width.Set(200, 0f);
            specsButton.Height.Set(50, 0f);
            specsButton.OnLeftClick += OpenSpecs;
            specsButton.BackgroundColor = new Color(75, 75, 75);
            specsButton.BorderColor = new Color(25, 25, 25);
            Append(specsButton);

            specsText = new UIText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Runes")}");
            specsText.VAlign = .5f;
            specsText.HAlign = .5f;
            specsButton.Append(specsText);

            extraFunctionButton = new UIPanel();
            extraFunctionButton.HAlign = .5f;
            extraFunctionButton.VAlign = .5f;
            extraFunctionButton.Top.Set(-280, 0f);
            extraFunctionButton.Width.Set(710, 0f);
            extraFunctionButton.Height.Set(50, 0f);
            extraFunctionButton.OnLeftClick += ExtraFunction;
            extraFunctionButton.BackgroundColor = new Color(75, 75, 75);
            extraFunctionButton.BorderColor = new Color(25, 25, 25);

            extraFunctionText = new UIText("");
            extraFunctionText.VAlign = .5f;
            extraFunctionText.HAlign = .5f;

            tips = new UIPanel();
            tips.HAlign = .5f;
            tips.VAlign = .5f;
            tips.Top.Set(275, 0f);
            tips.Width.Set(895, 0f);
            tips.Height.Set(50, 0f);
            tips.BackgroundColor = new Color(75, 75, 75);
            tips.BorderColor = new Color(25, 25, 25);
            Append(tips);

            tipsText = new UIText("" + tip[chosenTip], .9f);
            tipsText.VAlign = .5f;
            tipsText.HAlign = .5f;
            tipsText.Left.Set(-10, 0f);
            tips.Append(tipsText);

            passiveButton = new UIPanel();
            passiveButton.Height.Set(abilityPanelHeight, 0f);
            passiveButton.Width.Set(abilityPanelWidth, 0f);
            passiveButton.Top.Set(abilityTop, 0f);
            passiveButton.HAlign = .2f;
            passiveButton.BackgroundColor = new Color(75, 75, 75);
            passiveButton.BorderColor = new Color(25, 25, 25);
            background.Append(passiveButton);

            ability1Button = new UIPanel();
            ability1Button.Height.Set(abilityPanelHeight, 0f);
            ability1Button.Width.Set(abilityPanelWidth, 0f);
            ability1Button.Top.Set(abilityTop, 0f);
            ability1Button.HAlign = .4f;
            ability1Button.BackgroundColor = new Color(75, 75, 75);
            ability1Button.BorderColor = new Color(25, 25, 25);
            background.Append(ability1Button);

            ability2Button = new UIPanel();
            ability2Button.Height.Set(abilityPanelHeight, 0f);
            ability2Button.Width.Set(abilityPanelWidth, 0f);
            ability2Button.Top.Set(abilityTop, 0f);
            ability2Button.HAlign = .6f;
            ability2Button.BackgroundColor = new Color(75, 75, 75);
            ability2Button.BorderColor = new Color(25, 25, 25);
            background.Append(ability2Button);

            ability3Button = new UIPanel();
            ability3Button.Height.Set(abilityPanelHeight, 0f);
            ability3Button.Width.Set(abilityPanelWidth, 0f);
            ability3Button.Top.Set(abilityTop, 0f);
            ability3Button.HAlign = .8f;
            ability3Button.BackgroundColor = new Color(75, 75, 75);
            ability3Button.BorderColor = new Color(25, 25, 25);
            background.Append(ability3Button);

            passiveStaticText = new UIText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Passive")}");
            passiveStaticText.VAlign = .5f;
            passiveStaticText.HAlign = .5f;
            passiveButton.Append(passiveStaticText);

            ability1StaticText = new UIText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Ability1")}");
            ability1StaticText.VAlign = .5f;
            ability1StaticText.HAlign = .5f;
            ability1Button.Append(ability1StaticText);

            ability2StaticText = new UIText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Ability2")}");
            ability2StaticText.VAlign = .5f;
            ability2StaticText.HAlign = .5f;
            ability2Button.Append(ability2StaticText);

            ability3StaticText = new UIText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Ultimate")}");
            ability3StaticText.VAlign = .5f;
            ability3StaticText.HAlign = .5f;
            ability3Button.Append(ability3StaticText);

            abilityName = new UIText("");
            abilityName.Top.Set(200, 0f);
            abilityName.Left.Set(50, 0f);
            background.Append(abilityName);

            abilityText = new UIText("");
            abilityText.Top.Set(250, 0f);
            abilityText.Left.Set(0, 0f);
            abilityText.Width.Set(800, 0f);
            abilityText.IsWrapped = true;
            abilityText.PaddingRight = 35f;
            abilityText.PaddingLeft = 35f;
            background.Append(abilityText);

            castType = new UIText("");
            castType.Top.Set(200, 0f);
            castType.HAlign = .9f;
            background.Append(castType);

            ultimateCharge = new UIText("");
            ultimateCharge.Top.Set(50, 0f);
            ultimateCharge.HAlign = .5f;
            ability3Button.Append(ultimateCharge);

            abilityCooldown = new UIText("");
            //abilityCooldown.Top.Set(450f, 0f);
            //abilityCooldown.Left.Set(650f, 0f);
            abilityCooldown.Top.Set(200, 0f);
            abilityCooldown.HAlign = .9f;
            background.Append(abilityCooldown);

            abilityEffect1 = new UIText("");
            abilityEffect1.Top.Set(450f, 0f);
            abilityEffect1.Left.Set(50, 0f);
            background.Append(abilityEffect1);

            abilityEffect2 = new UIText("");
            abilityEffect2.Top.Set(425f, 0f);
            abilityEffect2.Left.Set(50, 0f);
            background.Append(abilityEffect2);

            abilityEffect3 = new UIText("");
            abilityEffect3.Top.Set(425f, 0f);
            abilityEffect3.Left.Set(540, 0f);
            background.Append(abilityEffect3);

            abilityEffect4 = new UIText("");
            abilityEffect4.Top.Set(450f, 0f);
            abilityEffect4.Left.Set(540, 0f);
            background.Append(abilityEffect4);

            questPanel = new UIPanel();
            questPanel.Width.Set(200, 0f);
            questPanel.Height.Set(50, 0f);
            questPanel.VAlign = .5f;
            questPanel.HAlign = .5f;
            questPanel.Top.Set(335, 0f);
            questPanel.Left.Set(320, 0f);
            questPanel.BackgroundColor = new Color(75, 75, 75);
            questPanel.BorderColor = new Color(25, 25, 25);
            questPanel.OnLeftClick += CompleteQuestButton;
            Append(questPanel);

            questText = new UIText("Daily Quest");
            questText.VAlign = .5f;
            questText.HAlign = .5f;
            questPanel.Append(questText);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            ultimateCharge.SetText(acmPlayer.ultCharge + " / " + acmPlayer.ultChargeMax);

            tipTimer++;
            if (tipTimer >= 330)
            {
                tipTimer = 0;
                chosenTip = Main.rand.Next(0, tipsNumber);
                if (chosenTip == prevTip)
                    chosenTip = Main.rand.Next(0, tipsNumber);
                prevTip = chosenTip;
                tipsText.SetText("" + tip[chosenTip]);
            }

            specsText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Runes")} ({acmPlayer.cardsPoints})");
            selectedClass = Player.GetModPlayer<ACMPlayer>().equippedClass;
            className.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.ClassPrefix")}: " + selectedClass);
            abilityName.SetText("");
            abilityText.SetText("");
            castType.SetText("");
            abilityCooldown.SetText("");
            abilityEffect1.SetText("");
            abilityEffect2.SetText("");
            abilityEffect3.SetText("");
            abilityEffect4.SetText("");

            extraFunctionText.Remove();
            extraFunctionButton.Remove();

            if (acmPlayer.hasScout)
            {
                Append(extraFunctionButton);
                extraFunctionButton.Append(extraFunctionText);

                if (acmPlayer.scoutCanDoubleJump)
                    extraFunctionText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_DisableDoubleJump")}");
                else
                    extraFunctionText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_EnableeDoubleJump")}");
            }
            
            if(acmPlayer.equippedClass == "Gambler")
            {
                Append(extraFunctionButton);
                extraFunctionButton.Append(extraFunctionText);

                if (acmPlayer.gamblerPassiveFeedback)
                    extraFunctionText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_P_Feedback_Enabled")}");
                else
                    extraFunctionText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Gambler_P_Feedback_Disabled")}");
            }

            if(extraFunctionButton.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                extraFunctionButton.BorderColor = Color.Yellow;
            }
            else
            {
                extraFunctionButton.BorderColor = new Color(25, 25, 25);
            }

            if (buttonTalents.IsMouseHovering || talentsText.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                buttonTalents.BorderColor = Color.Yellow;

                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }
            }
            else
            {
                buttonTalents.BorderColor = new Color(25, 25, 25);
            }

            if (relicsButton.IsMouseHovering || relicsText.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                relicsButton.BorderColor = Color.Yellow;

                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }
            }
            else
            {
                relicsButton.BorderColor = new Color(25, 25, 25);
            }

            if (specsButton.IsMouseHovering || specsText.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                specsButton.BorderColor = Color.Yellow;

                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }
            }
            else
            {
                specsButton.BorderColor = new Color(25, 25, 25);
            }

            if (passiveButton.IsMouseHovering)
                passiveButton.BorderColor = Color.Yellow;
            else
                passiveButton.BorderColor = new Color(25, 25, 25);
            if (ability1Button.IsMouseHovering)
                ability1Button.BorderColor = Color.Yellow;
            else
                ability1Button.BorderColor = new Color(25, 25, 25);
            if (ability2Button.IsMouseHovering)
                ability2Button.BorderColor = Color.Yellow;
            else
                ability2Button.BorderColor = new Color(25, 25, 25);
            if (ability3Button.IsMouseHovering)
                ability3Button.BorderColor = Color.Yellow;
            else
                ability3Button.BorderColor = new Color(25, 25, 25);

            if(!passiveButton.IsMouseHovering && ! ability1Button.IsMouseHovering && !ability2Button.IsMouseHovering && !ability3Button.IsMouseHovering)
            {
                abilityName.TextColor = Color.White;
                abilityName.SetText($"-{Language.GetTextValue("Mods.ApacchiisClassesMod2.PlayerStatsText")}-" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.TimesDied")}: {acmPlayer.timesDied}" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.EnemiesKilled")}: {acmPlayer.enemiesKilled}" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.DamageTaken")}: {acmPlayer.totalDamageTaken} " +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.HighestCrit")}: {acmPlayer.highestCrit} " +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.DamageReduction")}: {(Player.endurance * 100).ToString("F2")}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")}: {(acmPlayer.abilityPower * 100).ToString("F2")}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.HealingPower")}: {(acmPlayer.healingPower * 100).ToString("F2")}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.CooldownReduction")}: {(int)-((acmPlayer.cooldownReduction - 1f) * 100)}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.UltCost")}: {(int)(acmPlayer.ultCooldownReduction * 100)}%");

                abilityCooldown.SetText("\n" +
                                       $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.DodgeChance")}: {(acmPlayer.dodgeChance * 100).ToString("F2")}%\n" +
                                       $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMinions")}: {acmPlayer.Player.maxMinions}" +
                                       $"\nMinion Crit: {(acmPlayer.minionCritChance * 100f).ToString("F2")}%");
            }

            if (!passiveButton.IsMouseHovering && !ability1Button.IsMouseHovering && !ability2Button.IsMouseHovering && !ability3Button.IsMouseHovering && !buttonTalents.IsMouseHovering && !questPanel.IsMouseHovering && !relicsText.IsMouseHovering && !specsButton.IsMouseHovering && !specsText.IsMouseHovering && !questPanel.IsMouseHovering && !questText.IsMouseHovering && !relicsButton.IsMouseHovering && !relicsText.IsMouseHovering)
                tick = false;

            if (passiveButton.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if(!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                abilityText.Width.Set(FontAssets.MouseText.Value.MeasureString(acmPlayer.P_Desc).X * 2, 0f);
                abilityName.SetText($"[{acmPlayer.P_Name}]");
                abilityText.SetText($"{acmPlayer.P_Desc}");

                if (passiveButton.IsMouseHovering)
                abilityEffect1.SetText(acmPlayer.P_Effect_1);
                abilityEffect2.SetText(acmPlayer.P_Effect_2);
                abilityEffect3.SetText(acmPlayer.P_Effect_3);
                abilityEffect4.SetText(acmPlayer.P_Effect_4);
            }

            if (ability1Button.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                abilityCooldown.SetText($"Cooldown: {(decimal)acmPlayer.ability1MaxCooldown}s");

                abilityText.Width.Set(FontAssets.MouseText.Value.MeasureString(acmPlayer.A1_Desc).X * 2, 0f);
                abilityName.SetText($"[{acmPlayer.A1_Name}]");
                abilityText.SetText($"{acmPlayer.A1_Desc}");

                abilityEffect1.SetText(acmPlayer.A1_Effect_1);
                abilityEffect2.SetText(acmPlayer.A1_Effect_2);
                abilityEffect3.SetText(acmPlayer.A1_Effect_3);
                abilityEffect4.SetText(acmPlayer.A1_Effect_4);
            }

            if (ability2Button.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                abilityCooldown.SetText($"Cooldown: {(decimal)acmPlayer.ability2MaxCooldown}s");

                abilityText.Width.Set(FontAssets.MouseText.Value.MeasureString(acmPlayer.A2_Desc).X * 2, 0f);
                abilityName.SetText($"[{acmPlayer.A2_Name}]");
                abilityText.SetText($"{acmPlayer.A2_Desc}");

                abilityEffect1.SetText(acmPlayer.A2_Effect_1);
                abilityEffect2.SetText(acmPlayer.A2_Effect_2);
                abilityEffect3.SetText(acmPlayer.A2_Effect_3);
                abilityEffect4.SetText(acmPlayer.A2_Effect_4);

                switch (selectedClass)
                {
                    case "Blood Mage":
                        abilityCooldown.SetText("[Toggleable]");
                        break;
                }
            }

            if (ability3Button.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                abilityCooldown.SetText($"Cooldown: {(decimal)(acmPlayer.ultChargeMax / 60)}s In Battle");

                abilityText.Width.Set(FontAssets.MouseText.Value.MeasureString(acmPlayer.Ult_Desc).X * 2, 0f);
                abilityName.SetText($"[{acmPlayer.Ult_Name}]");
                abilityText.SetText($"{acmPlayer.Ult_Desc}");

                abilityEffect1.SetText(acmPlayer.Ult_Effect_1);
                abilityEffect2.SetText(acmPlayer.Ult_Effect_2);
                abilityEffect3.SetText(acmPlayer.Ult_Effect_3);
                abilityEffect4.SetText(acmPlayer.Ult_Effect_4);
            }

            //Quests
            if (questPanel.IsMouseHovering || questText.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                questPanel.BorderColor = Color.Yellow;

                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                ACMQuests questPlayer = Player.GetModPlayer<ACMQuests>();

                if(questPlayer.chosenQuest != "")
                {
                    abilityName.TextColor = Color.Orange;
                    abilityName.SetText($"{questPlayer.questName}");
                    if (questPlayer.chosenQuest != "")
                        abilityText.Width.Set(500, 0f);
                    abilityText.SetText($"{questPlayer.questDesc}");

                    abilityCooldown.SetText($"");
                    //abilityCooldown.SetText($"Quests completed: {questPlayer.questsCompleted}\n" +
                    //                        $"Gold gained: {goldGainedFromQuests}");

                    //Remove all other info
                    castType.SetText("");
                    abilityEffect1.SetText("");
                    abilityEffect2.SetText("");
                    abilityEffect3.SetText("");
                    abilityEffect4.SetText("Click the 'Daily Quest' button to complete");
                }
                else
                {
                    abilityName.TextColor = Color.Orange;
                    abilityName.SetText($"Daily Quest");
                    if (questPlayer.chosenQuest != "")
                        abilityText.Width.Set(500, 0f);
                    abilityText.SetText($"You've already completed your daily quest!");

                    abilityCooldown.SetText($"");

                    //Remove all other info
                    castType.SetText("");
                    abilityEffect1.SetText("");
                    abilityEffect2.SetText("");
                    abilityEffect3.SetText("");
                    abilityEffect4.SetText("");
                }
            }
            else
            {
                questPanel.BorderColor = new Color(25, 25, 25);
            }

            //if(acmPlayer.equippedClass == "Gambler")
            //{
            //    abilityEffect3.Left.Set(480, 0f);
            //    abilityEffect4.Left.Set(480, 0f)
            //}

            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            //if (specsButton.IsMouseHovering || specsText.IsMouseHovering)
            //{
            //    if (acmPlayer.spentSkillPointsGlobal < 10)
            //        Main.hoverItemName = "Unlocks after spending 10 skill points on the talent tree";
            //}
                base.DrawSelf(spriteBatch);
        }

        private void ExtraFunction(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            SoundEngine.PlaySound(SoundID.MenuTick);

            if (acmPlayer.hasScout)
                if (acmPlayer.scoutCanDoubleJump)
                    acmPlayer.scoutCanDoubleJump = false;
                else
                    acmPlayer.scoutCanDoubleJump = true;

            if (acmPlayer.equippedClass == "Gambler")
                if (acmPlayer.gamblerPassiveFeedback)
                    acmPlayer.gamblerPassiveFeedback = false;
                else
                    acmPlayer.gamblerPassiveFeedback = true;
        }

        private void OpenTalents(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuOpen);

            if (acmPlayer.hasVanguard)
                GetInstance<ACM2ModSystem>()._VanguardTalents.SetState(new VanguardTalents());
            if (acmPlayer.hasBloodMage)
                GetInstance<ACM2ModSystem>()._BloodMageTalents.SetState(new BloodMageTalents());
            if (acmPlayer.hasCommander)
                GetInstance<ACM2ModSystem>()._CommanderTalents.SetState(new CommanderTalents());
            if (acmPlayer.hasScout)
                GetInstance<ACM2ModSystem>()._ScoutTalents.SetState(new ScoutTalents());
            if (acmPlayer.hasSoulmancer)
                GetInstance<ACM2ModSystem>()._SoulmancerTalents.SetState(new SoulmancerTalents());
            if (acmPlayer.hasCrusader)
                GetInstance<ACM2ModSystem>()._CrusaderTalents.SetState(new CrusaderTalents());
            if (acmPlayer.equippedClass == "Gambler")
                GetInstance<ACM2ModSystem>()._GamblerTalents.SetState(new GamblerTalents());
            if (acmPlayer.equippedClass == "Plague")
                GetInstance<ACM2ModSystem>()._PlagueTalents.SetState(new PlagueTalents());
        }

        private void OpenRelics(UIMouseEvent evt, UIElement listeningElement)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);

            if (GetInstance<ACM2ModSystem>()._RelicsUI.CurrentState == null)
                GetInstance<ACM2ModSystem>()._RelicsUI.SetState(new Other.RelicsUI());

            SoundEngine.PlaySound(SoundID.MenuOpen);
        }

        private void OpenSpecs(UIMouseEvent evt, UIElement listeningElement)
        {
            //Cards
            if (GetInstance<ACM2ModSystem>()._MyDeck.CurrentState == null)
                GetInstance<ACM2ModSystem>()._MyDeck.SetState(new Specializations.MyDeck());

                GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
        }

        private void CompleteQuestButton(UIMouseEvent evt, UIElement listeningElement)
        {
            ACMQuests questPlayer = Player.GetModPlayer<ACMQuests>();
            questPlayer.CompleteQuest();
        }
    }
}