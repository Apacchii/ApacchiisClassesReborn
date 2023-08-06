/*using System;
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


        int tipTimer = 0;
        string[] donoName =
        {
            "Names"
        };

        string[] tip =
        {
            $"[i:{ItemID.Heart}] Thank you for your support, @Dr.Oktober! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Grass! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Chelsea! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Jesus Wie D! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Matty! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Peanutbutta187! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @ris! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Dr.Void! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Eloraeon! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Grumpy! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Derin! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @CaineSenpai! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Icarus' Bullet! [i:{ItemID.Heart}]",
            $"[i:{ItemID.Heart}] Thank you for your support, @Capraeus! [i:{ItemID.Heart}]",

            "You can change the max level a class can reach in the mod's config (10-100).",
            $"You level up each time a boss is defeated, you can see which bosses you've defeated using a 'Class Book' [i:{ItemType<Items.ClassBook>()}].",
            "Ability Power increases how much effect, such as damage, an ability has.",
            "Cooldown Reduction reduces the cooldown of all your non-ultimate abilities.",
            $"You can craft new classes by crafting 'White Cloth' [i:{ItemType<Items.WhiteCloth>()}] on a 'Loom' [i:{ItemID.Loom}].",
            $"You can reset your equipped class by cheating in a dose of 'Super ZIP' [i:{ItemType<Items.SuperZIP>()}].",
            "You can make it so classes don't give you bonus stats by enabling 'Hidden Accessory Disables Stats' in the mod's config.", // Limit for text
            "You build up your ultimate by being in battle. You are considered to be in battle for 3s everytime you hit or are hit.",
            "Your ultimate build up decays at half the rate it builds up if you haven't been in battle for 5 seconds.",
            "Everytime you are hit by an enemy you lose 5% of your current ultimate charge.",
            "Statue-spawned enemies do not count towards your highest dps, highest crit, enemies killed and damage dealt stats.",
            "If your HUD ever gets stuck and doesn't update anymore, you can type '/acr resetHUD' in chat and it'll fix itself.",
            "Join the discord on the mod's description/workshop page for help, reporting bugs, or just chatting.",
            "This mod is still in development, more classes will come in the future!",
            "Skill Points are awarded every time you level up, spend them by clicking on one of the buttons below!",
            "After unlocking all 10 talents a new way to spend skill points unlocks!",
            "The HUD on the bottom left of the screen will tell you the current cooldown of all your abilities.",
            "Some vanilla items grant you increased ability power, reduced ability cooldowns and reduced ultimate costs.",
            "This menu can also be opened via a hotkey, you just need to assign one under Settings > Controls > ACM2: Menu.",
            "Enemies deal 5% more damage overall by default. This can be changed in the mod's config.",
            "Relics can be equipped right next to the class' banner slot.",
            "Relics have a low chance to drop from any non-boss enemy defeated.",
            $"If your 'Health To Regen [i:{ItemID.Heart}]' HUD next to your life bar/hearts is out of place, you can change it in the mod's config.",
            "If you ever change your mind, right clicking your class' specializations removes points so you can use them elsewhere.",
            "Base health is the health you get from anything but class stats.",
            "Base defense is the defense you get from anything but class stats.",
            "All relics have the same drop chance.",
            $"This server's settings has a {Configs._ACMConfigServer.Instance.classStatMult}x multiplier for class stats!",
            $"This server's settings has a {Configs._ACMConfigServer.Instance.enemyDamageMultiplier}x multiplier for all enemy damage!",
            $"Healing Power does NOT affect potions [i:{ItemID.LesserHealingPotion} and/or relics!]",
            $"You get Ability Power based on your currently held weapon's base DPS, the higher it is, the more you get!"
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
            buttonTalents.Left.Set(210, 0f);
            buttonTalents.Width.Set(200, 0f);
            buttonTalents.Height.Set(50, 0f);
            buttonTalents.OnClick += OpenTalents;
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
            relicsButton.Left.Set(-210, 0f);
            relicsButton.Width.Set(200, 0f);
            relicsButton.Height.Set(50, 0f);
            relicsButton.OnClick += OpenRelics;
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
            //specsButton.Left.Set(-105, 0f);
            specsButton.Width.Set(200, 0f);
            specsButton.Height.Set(50, 0f);
            specsButton.OnClick += OpenSpecs;
            specsButton.BackgroundColor = new Color(75, 75, 75);
            specsButton.BorderColor = new Color(25, 25, 25);
            Append(specsButton);

            specsText = new UIText("");
            specsText.VAlign = .5f;
            specsText.HAlign = .5f;
            specsButton.Append(specsText);

            extraFunctionButton = new UIPanel();
            extraFunctionButton.HAlign = .5f;
            extraFunctionButton.VAlign = .5f;
            extraFunctionButton.Top.Set(-280, 0f);
            extraFunctionButton.Width.Set(710, 0f);
            extraFunctionButton.Height.Set(50, 0f);
            extraFunctionButton.OnClick += ExtraFunction;
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
            abilityText.Left.Set(50, 0f);
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

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            ultimateCharge.SetText(acmPlayer.ultCharge + " / " + acmPlayer.ultChargeMax);

            tipTimer++;
            if (tipTimer >= 300)
            {
                tipTimer = 0;
                chosenTip = Main.rand.Next(0, tipsNumber);
                if (chosenTip == prevTip)
                    chosenTip = Main.rand.Next(0, tipsNumber);
                prevTip = chosenTip;
                tipsText.SetText("" + tip[chosenTip]);
            }
                

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

            if (acmPlayer.hasScout)
            {
                Append(extraFunctionButton);
                extraFunctionButton.Append(extraFunctionText);

                if (acmPlayer.scoutCanDoubleJump)
                    extraFunctionText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_DisableDoubleJump")}");
                else
                    extraFunctionText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_EnableeDoubleJump")}");
            }
            else
            {
                extraFunctionText.Remove();
                extraFunctionButton.Remove();
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

            if (acmPlayer.spentSkillPointsGlobal >= 10)
                specsText.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Specializations")}");
            else
                specsText.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Locked")}]");

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
                abilityName.SetText($"-{Language.GetTextValue("Mods.ApacchiisClassesMod2.PlayerStatsText")}-" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.TimesDied")}: {acmPlayer.timesDied}" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.EnemiesKilled")}: {acmPlayer.enemiesKilled}" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.DamageTaken")}: {acmPlayer.totalDamageTaken} " +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.HighestCrit")}: {acmPlayer.highestCrit} " +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.DamageReduction")}: {(decimal)((acmPlayer.trueEndurance + Player.endurance - 1f) * 100)}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")}: {(decimal)(acmPlayer.abilityPower * 100)}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.HealingPower")}: {(decimal)(acmPlayer.healingPower * 100)}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.CooldownReduction")}: {-(int)((acmPlayer.cooldownReduction - 1f) * 100)}%" +
                                    $"\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.UltCost")}: {(int)(acmPlayer.ultCooldownReduction * 100)}%");

                abilityCooldown.SetText("\n" +
                                       $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.DodgeChance")}: {(decimal)(acmPlayer.dodgeChance * 100)}%\n" +
                                       $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMinions")}: {acmPlayer.Player.maxMinions}");
            }

            if (!passiveButton.IsMouseHovering && !ability1Button.IsMouseHovering && !ability2Button.IsMouseHovering && !ability3Button.IsMouseHovering && !buttonTalents.IsMouseHovering && !relicsButton.IsMouseHovering && !relicsText.IsMouseHovering && !specsButton.IsMouseHovering && !specsText.IsMouseHovering)
                tick = false;

            if (passiveButton.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if(!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                switch (selectedClass)
                {
                    case "Vanguard":
                        //castType.SetText("-Constant Passive-");
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_P_Name")}]");
                        abilityText.SetText("Your armor is enchanted with light properties.\n" +
                                            "Enemies that directly attack you take a percentage of your defense as mitigable damage.");
                        abilityEffect1.SetText($"Reflected Damage: {acmPlayer.vanguardPassiveReflectAmount * 100 + acmPlayer.vanguardLevel * 2.5f}% = {acmPlayer.vanguardPassiveReflectAmount * 100}% + 2.5% p/Level({acmPlayer.vanguardLevel * 2.5f}%)");
                        break;

                    case "Blood Mage":
                        //castType.SetText("-Constant Passive-");
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_P_Name")}]");
                        abilityText.SetText("Every second in battle earn a stack of 'Blood'.\n" +
                                            "Each stack will regenerate a small percentage of yourhealth each second.\n" +
                                            "Stacks decay out of battle and are lost on death.");
                        if (acmPlayer.bloodMageTalent_9 == "L")
                            abilityText.SetText("Every second in battle earn a stack of 'Blood'.\n" +
                                            "Each stack will regenerate a small percentage of yourhealth each second.\n" +
                                            "Stacks decay out of battle and are lost on death.\n" +
                                            "[Talent] Now also grants 0.08% bonus magic damage per stack of 'Blood'");
                        else
                            abilityText.SetText("Every second in battle earn a stack of 'Blood'.\n" +
                                            "Each stack will regenerate a small percentage of yourhealth each second.\n" +
                                            "Stacks decay out of battle and are lost on death.");
                        abilityEffect1.SetText("Regen: " + acmPlayer.bloodMageBasePassiveRegen * 100 + "% p/Stack = " + (decimal)(acmPlayer.bloodMagePassiveRegen * (acmPlayer.bloodMagePassiveMaxStacks + acmPlayer.bloodMageLevel * 2) * 100) + "% at max stacks");
                        abilityEffect2.SetText("Max Stacks: " + acmPlayer.bloodMagePassiveBaseMaxStacks + " + 2 p/Level(" + acmPlayer.bloodMageLevel * 2 + ") = " + acmPlayer.bloodMagePassiveMaxStacks);
                        if (acmPlayer.bloodMageTalent_9 == "L")
                            abilityEffect4.SetText($"Max Bonus Damage: {(decimal)(acmPlayer.bloodMagePassiveMaxStacks * .008f * 100)}%");
                        break;

                    case "Commander":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_P_Name")}]");
                        abilityText.SetText("Gain bonus damage reduction for each minion slot you have.");

                        abilityEffect1.SetText($"Endurance: {(decimal)(acmPlayer.commanderPassiveEndurance * acmPlayer.Player.maxMinions * 100)}%");
                        abilityEffect2.SetText($"Max Minions: {acmPlayer.Player.maxMinions}");
                        break;

                    case "Scout":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_Name")}]");
                        abilityText.SetText("The scout has a free double jump and has increased movement speed.");

                        if(acmPlayer.scoutCanDoubleJump)
                            abilityEffect1.SetText("Double Jump: Enabled");
                        else
                            abilityEffect1.SetText("Double Jump: Disabled");

                        abilityEffect2.SetText($"Speed Bonus: {(int)(acmPlayer.scoutPassiveSpeedBonus * 100)}%");
                        break;

                    case "Soulmancer":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Name")}]");
                        abilityText.SetText("Hitting enemies with magic weapons has a chance to rip a fragment of their soul causing it to\n" +
                                            "harm any nearby enemies.\n" +
                                            "Soul Rip chance increases by 1% per level.");

                        abilityEffect1.SetText($"Damage: {(int)(acmPlayer.soulmancerSoulRipDamage * acmPlayer.abilityPower)} = {acmPlayer.soulmancerSoulRipDamage_Base} + {acmPlayer.soulmancerSoulRipDamage_PerLevel} p/Level({acmPlayer.soulmancerSoulRipDamage_PerLevel * acmPlayer.soulmancerLevel})");
                        abilityEffect2.SetText($"Rip Chance: {(int)(acmPlayer.soulmancerSoulRipChance * 100)}%");
                        break;
                }
            }

            if (ability1Button.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                abilityName.SetText($"[{acmPlayer.A1_Name}]");
                abilityText.SetText($"{acmPlayer.A1_Desc_1}\n{acmPlayer.A1_Desc_2}\n{acmPlayer.A1_Desc_3}\n{acmPlayer.A1_Desc_4}");
                abilityCooldown.SetText($"Cooldown: {acmPlayer.ability1MaxCooldown}s");

                switch (selectedClass)
                {
                    case "Vanguard":
                        //castType.SetText("-Cursor Target-");
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A1_Name")}]");
                        abilityText.SetText("Throw a spear of light that will explode if an enemy is nearby, dealing damage to all enemies around.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability1MaxCooldown}s");
                        abilityEffect1.SetText($"Explosion Damage: {(int)(acmPlayer.vanguardSpearBaseDamage + acmPlayer.vanguardLevel * 10)} = {acmPlayer.vanguardSpearBaseDamage} + 10 p/Level({acmPlayer.vanguardLevel * 10})");
                        break;

                    case "Blood Mage":
                        //castType.SetText("-Cursor Target-");
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A1_Name")}]");
                        abilityText.SetText("Throw a blob of your own blood to seek out an enemy, when the blob hits its target it will return to\n" +
                                            "you with a bit of the enemy's blood and heal you for 10% of the enemy's max health.\n" +
                                            $"(Healing cannot be greater than {(decimal)(acmPlayer.bloodMageSiphonHealMax * 100)}% of your max health)");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability1MaxCooldown}s");
                        abilityEffect1.SetText($"Damage: {(decimal)(acmPlayer.bloodMageSiphonBaseDamage + acmPlayer.bloodMageLevel * 6)} = {acmPlayer.bloodMageSiphonBaseDamage} + 6 p/Level({ acmPlayer.bloodMageLevel * 6})");
                        abilityEffect2.SetText($"Max Healing: {(decimal)acmPlayer.bloodMageSiphonHealMax * 100}% of your max health, except via healing power");
                        break;

                    case "Commander":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A1_Name")}]");
                        abilityText.SetText("Place a war banner at your feet." +
                                            "Players inside the banner's radius are granted damage reduction and\n" +
                                            "increased damage.\n" +
                                            "This buff persists for a while after leaving the banner's radius.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability1MaxCooldown}s");

                        abilityEffect1.SetText($"Endurance: {(decimal)((1f - acmPlayer.commanderBannerEndurance) * 100)}%");
                        abilityEffect2.SetText("Bonus Damage: " + (int)((acmPlayer.commanderBannerDamage - 1f) * 100) + "%");
                        abilityEffect4.SetText("Duration: " + acmPlayer.commanderBannerDuration / 60 + "s + 0.5s p/Level(" + acmPlayer.commanderLevel * .4f + "s) = " + (decimal)((acmPlayer.commanderLevel * 24 + acmPlayer.commanderBannerDuration) / 60) + "s");
                        break;

                    case "Scout":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A1_Name")}]");
                        abilityText.SetText("Take a sip from an energy drink you made yourself.\n" +
                                            "This drink will increase the damage you deal temporarily.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability1MaxCooldown}s");

                        abilityEffect1.SetText($"Bonus Damage Dealt: {(decimal)(acmPlayer.scoutColaDamageBonus -1f) * 100}% + {acmPlayer.scoutColaDamageBonusLevel * 100}% p/Level({(decimal)(acmPlayer.scoutColaDamageBonusLevel * acmPlayer.scoutLevel * 100)}%) = {(decimal)(((acmPlayer.scoutColaDamageBonus - 1f) + acmPlayer.scoutColaDamageBonusLevel * acmPlayer.scoutLevel) * 100)}%");
                        abilityEffect2.SetText($"Duration: {acmPlayer.scoutColaDuration / 60}s");
                        break;

                    case "Soulmancer":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Name")}]");
                        abilityText.SetText("For a short duration, everytime you hit an enemy with a soul fragment, recall it to yourself, consuming\n" +
                                            "it and healing you for a small percentage of your max health per fragment consumed.\n" +
                                            "Healing only applies if you are below 50% health.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability1MaxCooldown}s");

                        abilityEffect2.SetText($"Heal p/Fragment: {(decimal)(acmPlayer.soulmancerConsumeHeal * 100)}%");
                        abilityEffect1.SetText($"Duration: {(decimal)(acmPlayer.soulmancerConsumeDuration / 60)}s");
                        break;

                    case "Crusader":
                        break;
                }
            }

            if (ability2Button.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!tick)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    tick = true;
                }

                switch (selectedClass)
                {
                    case "Vanguard":
                        //castType.SetText("-No Target-");
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A2_Name")}]");
                        abilityText.SetText("Surround yourself in a barrier of light. Any damage taken when the barrier is active will be\n" +
                                            "reduced by a percentage.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability2MaxCooldown}s");
                        abilityEffect1.SetText("Damage Reduction: " + (decimal)(acmPlayer.vanguardShieldDamageReduction * 100) + "%");
                        abilityEffect2.SetText("Duration: " + (decimal)(acmPlayer.vanguardShieldBaseDuration / 60) + "s + 0.25s p/Level(" + (decimal)(acmPlayer.vanguardLevel * 0.25) + "s) = " + (decimal)(acmPlayer.vanguardShieldBaseDuration / 60 + acmPlayer.vanguardLevel * 0.25f) + "s");
                        break;

                    case "Blood Mage":
                        //castType.SetText("-No Target Toggle-");
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A2_Name")}]");
                        abilityText.SetText("Enchant you weapon with your own blood, draining your health each second, but increasing the\n" +
                                            "damage you deal.");
                        abilityCooldown.SetText("[Toggleable]");
                        //abilityEffect1.SetText("Bonus Damage: " + (decimal)(acmPlayer.bloodMageBaseDamageGain * 100) + "% + 0.5% p/Level(" + acmPlayer.bloodMageLevel * 0.5f + "%) = " + (decimal)(acmPlayer.bloodMageBaseDamageGain * 100 + acmPlayer.bloodMageLevel * .5f) + "%");
                        abilityEffect1.SetText($"Bonus Damage: {(decimal)(acmPlayer.bloodMageBaseDamageGain * 100 + acmPlayer.bloodMageLevel * .5f + acmPlayer.specBloodMage_EnchantDamage * acmPlayer.specBloodMage_EnchantDamageBase * 100)}% = {(decimal)(.1f * 100 + acmPlayer.specBloodMage_EnchantDamage * acmPlayer.specBloodMage_EnchantDamageBase * 100)}% + 0.5% p/Level({acmPlayer.bloodMageLevel * 0.5f}%)");
                        abilityEffect2.SetText("Health Drain: " + (decimal)(acmPlayer.bloodMageBaseHealthDrain * 100) + "% p/Sec");
                        break;

                    case "Commander":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_A2_Name")}]");
                        abilityText.SetText("Scream loudly, intimidating enemies around you and knocking them back, increasing the damage they\n" +
                                            "take for a period of time.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability2MaxCooldown}s");

                        abilityEffect1.SetText("Bonus Damage: " + (decimal)(acmPlayer.commanderCryBonusDamage * 100) + "% + 1% p/Level(" + acmPlayer.commanderLevel + "%) = " + (int)((acmPlayer.commanderCryBonusDamage + acmPlayer.commanderLevel * .01f) * 100) + "%");
                        abilityEffect3.SetText("Debuff Duration: " + (decimal)(acmPlayer.commanderCryDuration / 60) + "s");
                        abilityEffect2.SetText("Damage: " + acmPlayer.commanderCryBaseDamage + " + " + acmPlayer.commanderCryDamageLevel + " p/lvl(" + acmPlayer.commanderCryDamageLevel * acmPlayer.commanderLevel + ") = " + (int)(acmPlayer.commanderCryBaseDamage + acmPlayer.commanderCryDamageLevel * acmPlayer.commanderLevel));
                        break;

                    case "Scout":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A2_Name")}]");
                        abilityText.SetText("Place an explosive trap under your cursor's position, it'll take some time to arm itself.\n" +
                                            "The trap will explode if an enemy gets too close, dealing damage to all enemies within 2x the trap's\n" +
                                            "detection range.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability2MaxCooldown}s");

                        abilityEffect1.SetText("Damage: " + acmPlayer.scoutTrapBaseDamage + " + " + acmPlayer.scoutTrapDamageLevel + " p/Level(" + acmPlayer.scoutTrapDamageLevel * acmPlayer.scoutLevel + ") = " + (acmPlayer.scoutTrapBaseDamage + acmPlayer.scoutTrapDamageLevel * acmPlayer.scoutLevel));
                        abilityEffect2.SetText("Detection Range: " + acmPlayer.scoutTrapRange);
                        break;

                    case "Soulmancer":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Name")}]");
                        if(acmPlayer.hasAghanims)
                            abilityText.SetText("Shatter the soul of nearby enemies dealing heavy damage and ripping an additional fragment per soul\n" +
                                                "shattered.\n" +
                                                "[Aghanim's Scepter] Now casts at your cursor's position with reduced range");
                        else
                            abilityText.SetText("Shatter the soul of nearby enemies dealing heavy damage and ripping an additional fragment per soul\n" +
                                                "shattered.");
                        abilityCooldown.SetText($"Cooldown: {acmPlayer.ability2MaxCooldown}s");

                        abilityEffect1.SetText($"Damage: {(int)(acmPlayer.soulmancerSoulShatterDamage * acmPlayer.abilityPower)} = {acmPlayer.soulmancerSoulShatterDamage_Base} + {acmPlayer.soulmancerShatterDamage_PerLevel} p/Level({acmPlayer.soulmancerLevel * acmPlayer.soulmancerShatterDamage_PerLevel})");
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

                switch (selectedClass)
                {
                    case "Vanguard":
                        //castType.SetText("-Point Target-");
                        int dmgLvl = 9;
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_Ult_Name")}]");                                                                            //100 chars p/line
                        abilityText.SetText("Call in a giant sword from the heavens. The sword hits enemies all around it, dealing massive damage\n" +
                                            "and executing enemies below 50% health.\n" +
                                            "(Bosses are executed below " + acmPlayer.vanguardUltimateBossExecute * 100 + "% health)");
                        abilityCooldown.SetText("Cooldown: " + acmPlayer.ultChargeMax / 60 + "s In Battle");
                        abilityEffect1.SetText($"Damage: " + acmPlayer.vanguardSwordBaseDamage + $" + {dmgLvl} p/Level(" + acmPlayer.vanguardLevel * dmgLvl + ") = " + (int)(acmPlayer.vanguardSwordBaseDamage + (acmPlayer.vanguardLevel * dmgLvl)));
                        break;

                    case "Blood Mage":
                        //castType.SetText("-No Target-");
                        abilityName.SetText($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_Ult_Name")}]");
                        abilityText.SetText("Quickly regenerate the blood of you and all your allies over time, regenerating a percentage of the\n" +
                                            "healed player's max health at a medium rate for a limited number of ticks.");
                        abilityCooldown.SetText("Cooldown: " + acmPlayer.ultChargeMax / 60 + "s In Battle");
                        abilityEffect1.SetText("Max Health Heal: " + (decimal)acmPlayer.bloodMageBaseUltRegen * 100 + "% + 0.1% p/Level(" + acmPlayer.bloodMageLevel * 0.1f * 100 + "%) = " + (acmPlayer.bloodMageBaseUltRegen * 100 + acmPlayer.bloodMageLevel * .1f) + "%");
                        abilityEffect2.SetText("Ticks: " + acmPlayer.bloodMageUltTicks + " ticks");
                        break;

                    case "Commander":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Commander_Ult_Name")}]");
                        abilityText.SetText("Inspire all players, causing them to always land critical hits on enemies.\n" +
                                            "Minions can also crit during this duration");
                        abilityCooldown.SetText("Cooldown: " + acmPlayer.ultChargeMax / 60 + "s In Battle");

                        abilityEffect1.SetText("Duration: " + acmPlayer.commanderUltDuration / 60 + "s + 0.25s p/Level(" + (decimal)(acmPlayer.commanderLevel * .15f) + "s) = " + (decimal)((acmPlayer.commanderLevel * 15 + acmPlayer.commanderUltDuration) / 60) + "s");
                        break;

                    case "Scout":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Name")}]");
                        abilityText.SetText("Become invincible for " + acmPlayer.scoutUltInvDuration / 60 + " seconds and gain increased movement speed, jump height and\n" +
                                            "auto jump for a longer duration.");
                        abilityCooldown.SetText("Cooldown: " + acmPlayer.ultChargeMax / 60 + "s In Battle");

                        abilityEffect1.SetText("Mobility Duration: " + acmPlayer.scoutUltDuration / 60 + "s");
                        abilityEffect2.SetText("Speed Bonus: " + (int)(acmPlayer.scoutUltSpeed * 100) + "% + " + (int)(acmPlayer.scoutUltSpeedLevel * 100) + "% p/Level(" + (decimal)(acmPlayer.scoutUltSpeedLevel * acmPlayer.scoutLevel * 100) + "%) = " + (decimal)((acmPlayer.scoutUltSpeed + acmPlayer.scoutUltSpeedLevel * acmPlayer.scoutLevel) * 100) + "%");
                        abilityEffect4.SetText("Jump Height: " + (int)(acmPlayer.scoutUltJump * 100) + "%");
                        break;

                    case "Soulmancer":
                        abilityName.SetText($"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Name")}]");
                        abilityText.SetText("Rapidly rip fragments of your own soul, slightly draining your own health per fragment.\n" +
                                            "Each soul deals 1.5x [Soul Rip]'s damage.");

                        abilityCooldown.SetText("Cooldown: " + acmPlayer.ultChargeMax / 60 + "s In Battle");

                        abilityEffect2.SetText($"Souls Released: {acmPlayer.soulmancerSacrificeSoulCount_Base}");
                        abilityEffect1.SetText($"Health Cost: {acmPlayer.soulmancerSacrificeHealthCost * 100}% p/Soul");
                        break;
                }
            }

            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if (specsButton.IsMouseHovering || specsText.IsMouseHovering)
            {
                if (acmPlayer.spentSkillPointsGlobal < 10)
                    Main.hoverItemName = "Unlocks after spending 10 skill points on the talent tree";
            }
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
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            if(acmPlayer.spentSkillPointsGlobal >= 10)
            {
                GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);

                if (acmPlayer.hasBloodMage && GetInstance<ACM2ModSystem>()._BloodMageSpecs.CurrentState == null)
                    GetInstance<ACM2ModSystem>()._BloodMageSpecs.SetState(new Specializations.BloodMageSpecs());

                if (acmPlayer.hasVanguard && GetInstance<ACM2ModSystem>()._VanguardSpecs.CurrentState == null)
                    GetInstance<ACM2ModSystem>()._VanguardSpecs.SetState(new Specializations.VanguardSpecs());

                if (acmPlayer.hasCommander && GetInstance<ACM2ModSystem>()._CommanderSpecs.CurrentState == null)
                    GetInstance<ACM2ModSystem>()._CommanderSpecs.SetState(new Specializations.CommanderSpecs());

                if (acmPlayer.hasScout && GetInstance<ACM2ModSystem>()._ScoutSpecs.CurrentState == null)
                    GetInstance<ACM2ModSystem>()._ScoutSpecs.SetState(new Specializations.ScoutSpecs());

                if(acmPlayer.hasSoulmancer && GetInstance<ACM2ModSystem>()._SoulmancerSpecs.CurrentState == null)
                    GetInstance<ACM2ModSystem>()._SoulmancerSpecs.SetState(new Specializations.SoulmancerSpecs());

                //if (acmPlayer.hasSoulmancer && GetInstance<ACM2ModSystem>()._Specs.CurrentState == null)
                //    GetInstance<ACM2ModSystem>()._Specs.SetState(new Specializations.Specs());

                    SoundEngine.PlaySound(SoundID.MenuOpen);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Unlock);
            }
            
        }
    }
}*/