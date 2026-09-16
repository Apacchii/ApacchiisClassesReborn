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
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using ApacchiisClassesMod2.UI.Other;
using ApacchiisClassesMod2.Configs;
using ApacchiisClassesMod2.UI.HUD;
using Terraria.ModLoader.UI.Elements;
using Terraria.GameContent.Bestiary;

namespace ApacchiisClassesMod2.Items.Classes.Apothecary
{
    class ApothecaryUI : UIState
    {
        Player Player = Main.player[Main.myPlayer];

        UIGrid abilitiesGrid;

        UIPanel[] ability;
        Bar[] abilityCooldown;
        UIText[] abilityText;
        UIText[] abilityCharges;

        UIText abilityDescription;
        UIText abilityDescription2;
        UIText abilityDescription3;
        UIText abilityName;
        UIText abilityRecipe;

        UIPanel helpPanel;
        UIText helpText;

        public override void OnInitialize()
        {
            abilitiesGrid = new UIGrid();
            abilitiesGrid.IgnoresMouseInteraction = false;
            abilitiesGrid.ListPadding = 10f;
            abilitiesGrid.SetPadding(5f);
            abilitiesGrid.Height.Set(600, 0f);
            abilitiesGrid.Width.Set(600, 0f);
            abilitiesGrid.VAlign = .5f;
            abilitiesGrid.HAlign = .5f;
            abilitiesGrid.Top.Set(70, 0f);
            Append(abilitiesGrid);

            if (ACMConfigClient.Instance.ApothecaryHelpPanel)
            {
                helpPanel = new UIPanel();
                helpPanel.Width.Set(30, 0f);
                helpPanel.Height.Set(30, 0f);
                helpPanel.VAlign = .5f;
                helpPanel.HAlign = .5f;
                helpPanel.BackgroundColor = new Color(75, 75, 75);
                helpPanel.BorderColor = new Color(25, 25, 25);
                helpPanel.Top.Set(170, 0f);
                abilitiesGrid.Append(helpPanel);

                helpText = new UIText("?", .75f);
                helpText.IgnoresMouseInteraction = true;
                helpText.VAlign = .5f;
                helpText.HAlign = .5f;
                helpPanel.Append(helpText);
            }
            
            int abilityCount = 10;
            ability = new UIPanel[abilityCount];
            abilityCooldown = new Bar[abilityCount];
            abilityText = new UIText[abilityCount];
            abilityCharges = new UIText[abilityCount];
            for (int i = 0; i < abilityCount; i++)
            {
                ability[i] = new UIPanel();
                ability[i].Width.Set(50, 0f);
                ability[i].Height.Set(50, 0f);
                ability[i].VAlign = .5f;
                ability[i].BackgroundColor = new Color(75, 75, 75);
                ability[i].BorderColor = new Color(25, 25, 25);
                ability[i].PaddingLeft = 0;
                ability[i].PaddingRight = 0;
                abilitiesGrid.Add(ability[i]);

                //Double clicking ability closes menu
                ability[i].OnLeftDoubleClick += CloseUI;

                abilityCooldown[i] = new Bar();
                abilityCooldown[i].Width.Set(40, 0f);
                abilityCooldown[i].Height.Set(5, 0f);
                abilityCooldown[i].Top.Set(45, 0f);
                abilityCooldown[i].Left.Set(5, 0f);
                //ability[i].Append(abilityCooldown[i]);

                abilityText[i] = new UIText($"A{i}", 1.2f);
                abilityText[i].IgnoresMouseInteraction = true;
                abilityText[i].VAlign = .5f;
                abilityText[i].HAlign = .5f;
                abilityText[i].Top.Set(-2, 0f);
                ability[i].Append(abilityText[i]);

                abilityCharges[i] = new UIText("0", .7f);
                abilityCharges[i].IgnoresMouseInteraction = true;
                abilityCharges[i].VAlign = .5f;
                abilityCharges[i].Width.Set(50, 0f);
                abilityCharges[i].Height.Set(20, 0f);
                abilityCharges[i].Top.Set(-35, 0f);
                abilityText[i].Append(abilityCharges[i]);
            }

            //Ability panel images
            abilityText[0].SetText($"[i:{ItemID.HealingPotion}]");
            abilityText[1].SetText($"[i:{ItemID.SuperHealingPotion}]");
            abilityText[2].SetText($"[i:{ItemID.WoodBreastplate}]");
            abilityText[4].SetText($"[i:{ItemID.JungleSpores}]");
            abilityText[6].SetText($"[i:{ItemID.AnkhShield}]");
            abilityText[7].SetText($"[i:{ItemID.Torch}]");
            abilityText[8].SetText($"[i:{ItemID.FishingBobber}]");
            abilityText[9].SetText($"[i:{ItemID.RodofDiscord}]");

            //Ability selecting functionality
            ability[0].OnLeftClick += SelectHealingMix;
            ability[1].OnLeftClick += SelectMassHealingMix;
            ability[4].OnLeftClick += SelectPoisonMix;
            ability[7].OnLeftClick += SelectBrightDustMix;
            ability[8].OnLeftClick += SelectLuckyRodMix;

            //Ability crafting functionality
            ability[0].OnRightClick += CraftHealingMix;
            ability[1].OnRightClick += CraftMassHealingMix;
            ability[4].OnRightClick += CraftPoisonMix;
            ability[7].OnRightClick += CraftBrightDustMix;
            ability[8].OnRightClick += CraftLuckyRodMix;

            abilityName = new UIText("", .9f);
            abilityName.VAlign = .5f;
            abilityName.HAlign = .5f;
            abilityName.Top.Set(125, 0f);
            abilityName.TextColor = Colors.RarityOrange;
            Append(abilityName);    

            abilityDescription = new UIText("", .9f);
            abilityDescription.IgnoresMouseInteraction = true;
            abilityDescription.Width.Set(Main.screenWidth * .66f, 0f);
            abilityDescription.Height.Set(50, 0f);
            abilityDescription.VAlign = .5f;
            abilityDescription.HAlign = .5f;
            abilityDescription.IsWrapped = true;
            abilityDescription.Top.Set(170, 0f);
            Append(abilityDescription);

            abilityDescription2 = new UIText("", .9f);
            abilityDescription2.IgnoresMouseInteraction = true;
            abilityDescription2.Width.Set(Main.screenWidth * .66f, 0f);
            abilityDescription2.Height.Set(50, 0f);
            abilityDescription2.VAlign = .5f;
            abilityDescription2.HAlign = .5f;
            abilityDescription2.IsWrapped = true;
            abilityDescription2.Top.Set(200, 0f);
            Append(abilityDescription2);

            abilityDescription3 = new UIText("", .9f);
            abilityDescription3.IgnoresMouseInteraction = true;
            abilityDescription3.Width.Set(Main.screenWidth * .66f, 0f);
            abilityDescription3.Height.Set(50, 0f);
            abilityDescription3.VAlign = .5f;
            abilityDescription3.HAlign = .5f;
            abilityDescription3.IsWrapped = true;
            abilityDescription3.Top.Set(230, 0f);
            Append(abilityDescription3);

            abilityRecipe = new UIText("", 1f);
            abilityRecipe.VAlign = .5f;
            abilityRecipe.HAlign = .5f;
            abilityRecipe.Top.Set(250, 0f);
            Append(abilityRecipe);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            for(int i = 0; i < 10; i++)
            {
                if (ability[i].IsMouseHovering)
                {
                    Main.LocalPlayer.mouseInterface = true;
                }

                int cooldown = apoPlayer.cooldowns[i];
                float q = 0;

                if (i == (int)ApothecaryAbilities.HealingMix)
                    q = (float)cooldown / (float)(apoPlayer.healingMixCooldown);
                if (i == (int)ApothecaryAbilities.MaxHealthMix)
                    q = (float)cooldown / (float)(apoPlayer.maxHealthMixDurationAndCooldown);


                if (apoPlayer.cooldowns[i] > 0)
                {
                    ability[i].Append(abilityCooldown[i]);
                    abilityCooldown[i].Width.Set(40 * q, 0f);
                }
                else
                {
                    abilityCooldown[i].Remove();
                }
            }


            //Help panel
            if(ACMConfigClient.Instance.ApothecaryHelpPanel)
                if (helpPanel.IsMouseHovering )
                {
                    abilityName.SetText("");
                    abilityDescription.SetText("" +
                        "[c/a16ce6:Left clicking] on an mixture will select it, allowing you to cast it as your [c/a16ce6:first ability]\n" +
                        "(Double left clicking also closes this UI in the process)\n" +
                        "Using mixtures requires [c/a16ce6:charges], which can be made by [c/a16ce6:right clicking] said mixtures if you have the required materials");
                    abilityRecipe.SetText("");
                }
                else
                {
                    abilityName.SetText("");
                    abilityDescription.SetText("");
                    abilityRecipe.SetText("");
                }

            //Change panel colors based on selected ability
            if (apoPlayer.selectedAbility == ApothecaryAbilities.HealingMix)
                ability[0].BorderColor = Color.Yellow;
            else
                ability[0].BorderColor = new Color(25, 25, 25);

            if (apoPlayer.selectedAbility == ApothecaryAbilities.MaxHealthMix)
                ability[1].BorderColor = Color.Yellow;
            else
                ability[1].BorderColor = new Color(25, 25, 25);

            if (apoPlayer.selectedAbility == ApothecaryAbilities.PoisonMix)
                ability[4].BorderColor = Color.Yellow;
            else
                ability[4].BorderColor = new Color(25, 25, 25);

            if (apoPlayer.selectedAbility == ApothecaryAbilities.CleansingMix)
                ability[6].BorderColor = Color.Yellow;
            else
                ability[6].BorderColor = new Color(25, 25, 25);

            if (apoPlayer.selectedAbility == ApothecaryAbilities.BrightDustMix)
                ability[7].BorderColor = Color.Yellow;
            else
                ability[7].BorderColor = new Color(25, 25, 25);

            if (apoPlayer.selectedAbility == ApothecaryAbilities.LuckyRodMix)
                ability[8].BorderColor = Color.Yellow;
            else
                ability[8].BorderColor = new Color(25, 25, 25);

            if (apoPlayer.selectedAbility == ApothecaryAbilities.DiscordTeleportMix)
                ability[9].BorderColor = Color.Yellow;
            else
                ability[9].BorderColor = new Color(25, 25, 25);

            //Ability names and descriptions
            //Healing Mix
            if (ability[0].IsMouseHovering)
            {
                float healPercentage = apoPlayer.healingMixHeal * p.healingPower;

                abilityName.SetText($"Healing Mix ({((float)apoPlayer.healingMixCooldown / 60):F2}s Cooldown)");
                abilityDescription.SetText($"Heals yourself for {healPercentage * 100:F2}% of your max health");
                abilityDescription2.SetText($"");
                abilityDescription3.SetText($"");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.Mushroom}]" +
                    $"[i:{ItemID.Mushroom}]" +
                    $"[i:{ItemID.Daybloom}]");
            }

            //Max Health Mix
            if (ability[1].IsMouseHovering)
            {
                float healthIncrease = .1f;

                abilityName.SetText("Max Health Mix");
                abilityDescription.SetText($"Increases every player's max health by {healthIncrease * 100}%");
                abilityDescription2.SetText($"Has a duration of {apoPlayer.maxHealthMixDurationAndCooldown / 60}s");
                abilityDescription3.SetText($"");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.GoldOre}]/[i:{ItemID.PlatinumOre}]" +
                    $"[i:{ItemID.GoldOre}]/[i:{ItemID.PlatinumOre}]" +
                    $"[i:{ItemID.Mushroom}]" +
                    $"[i:{ItemID.Bone}]" +
                    $"[i:{ItemID.Bone}]" +
                    $"[i:{ItemID.Waterleaf}]");
            }

            //Defensive Mix
            if (ability[2].IsMouseHovering)
            {
                float endurance = .08f;
                float duration = 0f;

                abilityName.SetText("Defensive Mix");
                abilityDescription.SetText($"Grants {endurance * 100:F1}% damage reduction for {duration:F1} seconds");
                abilityDescription2.SetText($"");
                abilityDescription3.SetText($"");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.Wood}]"  +
                    $"[i:{ItemID.Wood}]" +
                    $"[i:{ItemID.IronOre}]/[i:{ItemID.LeadOre}]" +
                    $"[i:{ItemID.Daybloom}]");
            }


            //Poison Mix
            if (ability[4].IsMouseHovering)
            {
                float duration = apoPlayer.poisonMixBuffDuration;
                float duration2 = apoPlayer.poisonDuration;

                abilityName.SetText("Poison Mix");
                abilityDescription.SetText($"For {duration/60:F1} seconds, attacks apply a damage over time effect to enemies for {duration2/60:F1} seconds");
                abilityDescription2.SetText($"The damage is based on the enemy's max health");
                abilityDescription3.SetText($"Deals reduced damage vs bosses");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.Stinger}]" +
                    $"[i:{ItemID.Stinger}]" +
                    $"[i:{ItemID.RichMahogany}]" +
                    $"[i:{ItemID.JungleSpores}]" +
                    $"[i:{ItemID.Deathweed}]");
            }

            //Cleansing Mix
            if (ability[6].IsMouseHovering)
            {
                abilityName.SetText("Cleansing Mix");
                abilityDescription.SetText($"Drink to cleanse yourself of most debuffs");
                abilityDescription2.SetText($"");
                abilityDescription3.SetText($"");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.GlowingMushroom}]" +
                    $"[i:{ItemID.GlowingMushroom}]");//todo
            }

            //Bright Dust Mix
            if (ability[7].IsMouseHovering)
            {
                float duration = apoPlayer.brightDustBuffDuration;

                abilityName.SetText("Bright Dust Mix");
                abilityDescription.SetText($"Grants 'Shine' and 'Night Owl' potion effects for {duration / 60 / 60:F2} minutes");
                abilityDescription2.SetText($"");
                abilityDescription3.SetText($"");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.GlowingMushroom}]" +
                    $"[i:{ItemID.GlowingMushroom}]" +
                    $"[i:{ItemID.Blinkroot}]");
            }

            //Lucky Rod Mix
            if (ability[8].IsMouseHovering)
            {
                float duration = apoPlayer.luckyRodBuffDuration;

                abilityName.SetText("Lucky Rod Mix");
                abilityDescription.SetText($"Grants 'Fishing' and 'Sonar' potion effects for {duration / 60 / 60:F2} minutes");
                abilityDescription2.SetText($"");
                abilityDescription3.SetText($"");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.Waterleaf}]" +
                    $"[i:{ItemID.Waterleaf}]" +
                    $"[i:{ItemID.Waterleaf}]" +
                    $"[i:{ItemID.Coral}]" +
                    $"[i:{ItemID.GoldOre}]/[i:{ItemID.PlatinumOre}]");
            }

            //Discord Teleport Mix
            if (ability[9].IsMouseHovering)
            {
                abilityName.SetText("Discord Teleport Mix");
                abilityDescription.SetText($"A mix that teleports you a medium distance towards your mouse's position");
                abilityDescription2.SetText($"Has an internal cooldown not reduced by Cooldown Reduction");
                abilityDescription3.SetText($"");
                abilityRecipe.SetText($"" +
                    $"[i:{ItemID.Bottle}]" +
                    $"[i:{ItemID.CrystalShard}]" +
                    $"[i:{ItemID.CrystalShard}]" +
                    $"[i:{ItemID.CrystalShard}]" +
                    $"[i:{ItemID.Moonglow}]" +
                    $"[i:{ItemID.Blinkroot}]" +
                    $"[i:{ItemID.ChaosFish}]" +
                    $"[i:{ItemID.SoulofFlight}]");
            }

            //Ability charges
            abilityCharges[0].SetText($"{apoPlayer.healingMixCharges}");
            abilityCharges[1].SetText($"{apoPlayer.maxHealthMixCharges}");
            abilityCharges[4].SetText($"{apoPlayer.poisonMixCharges}");
            abilityCharges[7].SetText($"{apoPlayer.brightDustMixCharges}");
            abilityCharges[8].SetText($"{apoPlayer.luckyRodMixCharges}");
            abilityCharges[9].SetText($"{apoPlayer.luckyRodMixCharges}");

            base.Update(gameTime);
        }

        private void SelectHealingMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            apoPlayer.selectedAbility = ApothecaryAbilities.HealingMix;
            SoundEngine.PlaySound(SoundID.MenuTick);
            SoundEngine.PlaySound(SoundID.MaxMana with { Volume = .6f });
        }

        private void SelectMassHealingMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            apoPlayer.selectedAbility = ApothecaryAbilities.MaxHealthMix;
            SoundEngine.PlaySound(SoundID.MenuTick); 
            SoundEngine.PlaySound(SoundID.MaxMana with { Volume = .6f });
        }

        private void SelectPoisonMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            apoPlayer.selectedAbility = ApothecaryAbilities.PoisonMix;
            SoundEngine.PlaySound(SoundID.MenuTick);
            SoundEngine.PlaySound(SoundID.MaxMana with { Volume = .6f });
        }

        private void SelectLuckyRodMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            apoPlayer.selectedAbility = ApothecaryAbilities.LuckyRodMix;
            SoundEngine.PlaySound(SoundID.MenuTick);
            SoundEngine.PlaySound(SoundID.MaxMana with { Volume = .6f });
        }

        private void SelectBrightDustMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            apoPlayer.selectedAbility = ApothecaryAbilities.BrightDustMix;
            SoundEngine.PlaySound(SoundID.MenuTick);
            SoundEngine.PlaySound(SoundID.MaxMana with { Volume = .6f });
        }

        private void CraftHealingMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            int ingredient1 = ItemID.Daybloom; //x1
            int ingredient2 = ItemID.Mushroom; //x2

            int bottleSlot = -1;
            int daybloomSlot = -1;
            int mushroomSlot = -1;
            string ingredientsMissing = "";

            if(apoPlayer.healingMixCharges < apoPlayer.maxCharges)
            {
                for (int i = 0; i < 50; i++)
                {
                    //Check for ingredients in inventory
                    //Bottle
                    if (Player.inventory[i].type == ItemID.Bottle)
                        bottleSlot = i;
                    //Daybloom
                    if (Player.inventory[i].type == ingredient1)
                        daybloomSlot = i;
                    //Mushroom
                    if (Player.inventory[i].type == ingredient2 && Player.inventory[i].stack >= 2)
                        mushroomSlot = i;

                    //If we manage to find all the ingredients consume them and craft the mix
                    if (bottleSlot != -1 && daybloomSlot != -1 && mushroomSlot != -1)
                    {
                        Player.inventory[bottleSlot].stack--;
                        if (Player.inventory[bottleSlot].stack <= 0)
                            Player.inventory[bottleSlot].TurnToAir();

                        Player.inventory[daybloomSlot].stack--;
                        if (Player.inventory[daybloomSlot].stack <= 0)
                            Player.inventory[daybloomSlot].TurnToAir();

                        Player.inventory[mushroomSlot].stack -= 2;
                        if (Player.inventory[mushroomSlot].stack <= 0)
                            Player.inventory[mushroomSlot].TurnToAir();

                        apoPlayer.healingMixCharges++;
                        SoundEngine.PlaySound(SoundID.MaxMana);
                        SoundEngine.PlaySound(SoundID.Research with { Volume = .5f });
                        break;
                    }
                }

                if (bottleSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Bottle}](Bottle)";
                if (daybloomSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Daybloom}](Daybloom)";
                if (mushroomSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Mushroom}](Mushroom)";

                if (bottleSlot == -1 || daybloomSlot == -1 || mushroomSlot == -1)
                {
                    Main.NewText($"Missing ingredients:{ingredientsMissing}");
                    SoundEngine.PlaySound(SoundID.MenuClose with { Volume = .7f });
                    SoundEngine.PlaySound(SoundID.DD2_LightningBugZap with { Volume = .9f });
                }
            }
            else
            {
                Main.NewText("Max charges of 'Healing Mix' reached.");
            }
            
        }

        private void CraftMassHealingMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            int ingredient1 = ItemID.GoldOre; //x2
            int ingredient1_2 = ItemID.PlatinumOre; //x2
            int ingredient2 = ItemID.Mushroom; //x1
            int ingredient3 = ItemID.Bone; //x2
            int ingredient4 = ItemID.Waterleaf; //x1

            int bottleSlot = -1;
            int goldPlatSlot = -1;
            int mushroomSlot = -1;
            int boneSlot = -1;
            int waterleafSlot = -1;
            string ingredientsMissing = "";

            if (apoPlayer.maxHealthMixCharges < apoPlayer.maxCharges)
            {
                for (int i = 0; i < 50; i++)
                {
                    //Check for ingredients in inventory
                    //Bottle
                    if (Player.inventory[i].type == ItemID.Bottle)
                        bottleSlot = i;
                    //Gold/Plat
                    if (Player.inventory[i].type == ingredient1 && Player.inventory[i].stack >= 2 || Player.inventory[i].type == ingredient1_2 && Player.inventory[i].stack >= 2)
                        goldPlatSlot = i;
                    //Mushroom
                    if (Player.inventory[i].type == ingredient2)
                        mushroomSlot = i;
                    //Bone
                    if (Player.inventory[i].type == ingredient3 && Player.inventory[i].stack >= 2)
                        boneSlot = i;
                    //Waterleaf
                    if (Player.inventory[i].type == ingredient4)
                        waterleafSlot = i;

                    //If we manage to find all the ingredients consume them and craft the mix
                    if (bottleSlot != -1 && goldPlatSlot != -1  && mushroomSlot != -1 && boneSlot != -1 && waterleafSlot != -1)
                    {
                        Player.inventory[bottleSlot].stack--;
                        if (Player.inventory[bottleSlot].stack <= 0)
                            Player.inventory[bottleSlot].TurnToAir();

                        Player.inventory[goldPlatSlot].stack -= 2;
                        if (Player.inventory[goldPlatSlot].stack <= 0)
                            Player.inventory[goldPlatSlot].TurnToAir();

                        Player.inventory[mushroomSlot].stack--;
                        if (Player.inventory[mushroomSlot].stack <= 0)
                            Player.inventory[mushroomSlot].TurnToAir();

                        Player.inventory[boneSlot].stack -= 2;
                        if (Player.inventory[boneSlot].stack <= 0)
                            Player.inventory[boneSlot].TurnToAir();

                        Player.inventory[waterleafSlot].stack--;
                        if (Player.inventory[waterleafSlot].stack <= 0)
                            Player.inventory[waterleafSlot].TurnToAir();

                        apoPlayer.maxHealthMixCharges++;
                        SoundEngine.PlaySound(SoundID.Research);
                        SoundEngine.PlaySound(SoundID.MaxMana);
                        break;
                    }
                }

                if (bottleSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Bottle}](Bottle)";
                if (goldPlatSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.GoldOre}]/[i:{ItemID.PlatinumOre}](Gold or Platinum ore)";
                if (mushroomSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Mushroom}](Mushroom)";
                if (boneSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Bone}](Bone)";
                if (waterleafSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Waterleaf}](Waterleaf)";

                if (bottleSlot == -1 || goldPlatSlot == -1 || mushroomSlot == -1 || boneSlot == -1 || waterleafSlot == -1)
                {
                    Main.NewText($"Missing ingredients:{ingredientsMissing}");
                    SoundEngine.PlaySound(SoundID.MenuClose with { Volume = .7f });
                    SoundEngine.PlaySound(SoundID.DD2_LightningBugZap with { Volume = .9f });
                }
            }
            else
            {
                Main.NewText("Max charges of 'Mass Healing Mix' reached.");
            }
        }

        private void CraftPoisonMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            int ingredient1 = ItemID.Stinger; //x2
            int ingredient2 = ItemID.RichMahogany; //x1
            int ingredient3 = ItemID.JungleSpores; //x2

            int bottleSlot = -1;
            int stingerSlot = -1;
            int mahoganySlot = -1;
            int sporeSlot = -1;
            string ingredientsMissing = "";

            if (apoPlayer.poisonMixCharges < apoPlayer.maxCharges)
            {
                for (int i = 0; i < 50; i++)
                {
                    //Check for ingredients in inventory
                    //Bottle
                    if (Player.inventory[i].type == ItemID.Bottle)
                        bottleSlot = i;
                    //Stinger
                    if (Player.inventory[i].type == ingredient1 && Player.inventory[i].stack >= 2)
                        stingerSlot = i;
                    //Mahogany
                    if (Player.inventory[i].type == ingredient2)
                        mahoganySlot = i;
                    //Spore
                    if (Player.inventory[i].type == ingredient3)
                        sporeSlot = i;


                    //If we manage to find all the ingredients consume them and craft the mix
                    if (bottleSlot != -1 && stingerSlot != -1 && mahoganySlot != -1 && sporeSlot != -1)
                    {
                        Player.inventory[bottleSlot].stack--;
                        if (Player.inventory[bottleSlot].stack <= 0)
                            Player.inventory[bottleSlot].TurnToAir();

                        Player.inventory[stingerSlot].stack -= 2;
                        if (Player.inventory[stingerSlot].stack <= 0)
                            Player.inventory[stingerSlot].TurnToAir();

                        Player.inventory[mahoganySlot].stack--;
                        if (Player.inventory[mahoganySlot].stack <= 0)
                            Player.inventory[mahoganySlot].TurnToAir();

                        Player.inventory[sporeSlot].stack--;
                        if (Player.inventory[sporeSlot].stack <= 0)
                            Player.inventory[sporeSlot].TurnToAir();

                        apoPlayer.poisonMixCharges++;
                        SoundEngine.PlaySound(SoundID.Research);
                        SoundEngine.PlaySound(SoundID.MaxMana);
                        break;
                    }
                }

                if (bottleSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Bottle}](Bottle)";
                if (stingerSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Stinger}](Stinger)";
                if (mahoganySlot == -1)
                    ingredientsMissing += $"[i:{ItemID.RichMahogany}](Rich Mahogany)";
                if (sporeSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.JungleSpores}](Jungle Spores)";

                if (bottleSlot == -1 || stingerSlot == -1 || mahoganySlot == -1 || sporeSlot == -1)
                {
                    Main.NewText($"Missing ingredients:{ingredientsMissing}");
                    SoundEngine.PlaySound(SoundID.MenuClose with { Volume = .7f });
                    SoundEngine.PlaySound(SoundID.DD2_LightningBugZap with { Volume = .9f });
                }
            }
            else
            {
                Main.NewText("Max charges of 'Poison Mix' reached.");
            }
        }

        private void CraftLuckyRodMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            int ingredient1 = ItemID.Waterleaf; //x3
            int ingredient2 = ItemID.Coral; //x1
            int ingredient3 = ItemID.GoldOre; //x1
            int ingredient3_2 = ItemID.PlatinumOre; //x1

            int bottleSlot = -1;
            int waterleafSlot = -1;
            int coralSlot = -1;
            int goldPlatSlot = -1;
            string ingredientsMissing = "";

            if (apoPlayer.luckyRodMixCharges < apoPlayer.maxCharges)
            {
                for (int i = 0; i < 50; i++)
                {
                    //Check for ingredients in inventory
                    //Bottle
                    if (Player.inventory[i].type == ItemID.Bottle)
                        bottleSlot = i;
                    //Waterleaf
                    if (Player.inventory[i].type == ingredient1 && Player.inventory[i].stack >= 3)
                        waterleafSlot = i;
                    //Coral
                    if (Player.inventory[i].type == ingredient2)
                        coralSlot = i;
                    //Gold/Plat
                    if (Player.inventory[i].type == ingredient3)
                        goldPlatSlot = i;


                    //If we manage to find all the ingredients consume them and craft the mix
                    if (bottleSlot != -1 && waterleafSlot != -1 && coralSlot != -1 && goldPlatSlot != -1)
                    {
                        Player.inventory[bottleSlot].stack--;
                        if (Player.inventory[bottleSlot].stack <= 0)
                            Player.inventory[bottleSlot].TurnToAir();

                        Player.inventory[waterleafSlot].stack -= 3;
                        if (Player.inventory[waterleafSlot].stack <= 0)
                            Player.inventory[waterleafSlot].TurnToAir();

                        Player.inventory[coralSlot].stack--;
                        if (Player.inventory[coralSlot].stack <= 0)
                            Player.inventory[coralSlot].TurnToAir();

                        Player.inventory[goldPlatSlot].stack--;
                        if (Player.inventory[goldPlatSlot].stack <= 0)
                            Player.inventory[goldPlatSlot].TurnToAir();

                        apoPlayer.luckyRodMixCharges++;
                        SoundEngine.PlaySound(SoundID.Research);
                        SoundEngine.PlaySound(SoundID.MaxMana);
                        break;
                    }
                }

                if (bottleSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Bottle}](Bottle)";
                if (waterleafSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Waterleaf}](Waterleaf)";
                if (coralSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Coral}](Coral)";
                if (goldPlatSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.GoldOre}](Gold Ore) /[i:{ItemID.GoldOre}](Platinum Ore)";

                if (bottleSlot == -1 || waterleafSlot == -1 || coralSlot == -1 || goldPlatSlot == -1)
                {
                    Main.NewText($"Missing ingredients:{ingredientsMissing}");
                    SoundEngine.PlaySound(SoundID.MenuClose with { Volume = .7f });
                    SoundEngine.PlaySound(SoundID.DD2_LightningBugZap with { Volume = .9f });
                }
            }
            else
            {
                Main.NewText("Max charges of 'Lucky Rod Mix' reached.");
            }
        }

        private void CraftBrightDustMix(UIMouseEvent evt, UIElement listeningElement)
        {
            Player Player = Main.player[Main.myPlayer];
            ApothecaryPlayer apoPlayer = Player.GetModPlayer<ApothecaryPlayer>();
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            int ingredient1 = ItemID.GlowingMushroom; //x2
            int ingredient2 = ItemID.Blinkroot; //x1


            int bottleSlot = -1;
            int glowingMushroomSlot = -1;
            int blinkrootSlot = -1;
            string ingredientsMissing = "";

            if (apoPlayer.brightDustMixCharges < apoPlayer.maxCharges)
            {
                for (int i = 0; i < 50; i++)
                {
                    //Check for ingredients in inventory
                    //Bottle
                    if (Player.inventory[i].type == ItemID.Bottle)
                        bottleSlot = i;
                    //Glowing Mushroom
                    if (Player.inventory[i].type == ingredient1 && Player.inventory[i].stack >= 2)
                        glowingMushroomSlot = i;
                    //Blinkroot
                    if (Player.inventory[i].type == ingredient2)
                        blinkrootSlot = i;

                    //If we manage to find all the ingredients consume them and craft the mix
                    if (bottleSlot != -1 && glowingMushroomSlot != -1 && blinkrootSlot != -1)
                    {
                        Player.inventory[bottleSlot].stack--;
                        if (Player.inventory[bottleSlot].stack <= 0)
                            Player.inventory[bottleSlot].TurnToAir();

                        Player.inventory[glowingMushroomSlot].stack -= 2;
                        if (Player.inventory[glowingMushroomSlot].stack <= 0)
                            Player.inventory[glowingMushroomSlot].TurnToAir();

                        Player.inventory[blinkrootSlot].stack--;
                        if (Player.inventory[blinkrootSlot].stack <= 0)
                            Player.inventory[blinkrootSlot].TurnToAir();

                        apoPlayer.brightDustMixCharges++;
                        SoundEngine.PlaySound(SoundID.MaxMana);
                        SoundEngine.PlaySound(SoundID.Research with { Volume = .5f });
                        break;
                    }
                }

                if (bottleSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Bottle}](Bottle)";
                if (glowingMushroomSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.GlowingMushroom}](Glowing Mushroom)";
                if (blinkrootSlot == -1)
                    ingredientsMissing += $"[i:{ItemID.Blinkroot}](Blinkroot)";

                if (bottleSlot == -1 || glowingMushroomSlot == -1 || blinkrootSlot == -1)
                {
                    Main.NewText($"Missing ingredients:{ingredientsMissing}");
                    SoundEngine.PlaySound(SoundID.MenuClose with { Volume = .7f });
                    SoundEngine.PlaySound(SoundID.DD2_LightningBugZap with { Volume = .9f });
                }
            }
            else
            {
                Main.NewText("Max charges of 'Bright Dust' reached.");
            }

        }

        private void CloseUI(UIMouseEvent evt, UIElement listeningElement)
        {
            ModContent.GetInstance<ACM2ModSystem>()._ApothecaryUI.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
    }
}