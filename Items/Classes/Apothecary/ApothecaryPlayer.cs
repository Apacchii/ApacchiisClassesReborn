using System.IO;
using System;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ApacchiisClassesMod2.UI;
using Terraria.UI;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.Chat;
using Terraria.Audio;
using Terraria.GameInput;
using Steamworks;
using Terraria.GameContent.UI.ResourceSets;

namespace ApacchiisClassesMod2.Items.Classes.Apothecary
{
    public enum ApothecaryAbilities
    {
        //Defensive
        HealingMix, //Heals
        MaxHealthMix, //Increases max health
        DefensiveMix, //Increases damage resistance
        CleansingMix, //Cleanses from most debuffs
        DodgingMix, //Grants invincibility

        //Offensive
        PoisonMix, //Adds DoT to attacks
        ExplosiveClusterMix, //Hurls cluster explosive flask

        //Utility
        BrightDustMix, //Grants shine and night owl buff
        LuckyRodMix, //Grants fishing and sonar potions
        DiscordTeleportMix, //Nerfed rod of discord
    };

    public class ApothecaryPlayer : ModPlayer
    {
        public int[] cooldowns = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        //Healing mix
        public float healingMixHeal = .06f;
        public int healingMixCooldown;
        public int healingMixCooldownBase = 60 * 14;

        //Max Health Mix
        public float maxHealthMixHealth = .1f;
        public int maxHealthMixDurationAndCooldown = 60 * 60;


        //Defensive mix
        public float defenseResistance = .2f;

        //Poison mix
        public int poisonMixBuffDuration = 60 * 5;
        int _poisonBuffCurrentDuration;
        public int poisonDuration = 60 * 14;
        public int poisonDurationPerLevel = 30;

        //Lucky Rod Mix
        public int luckyRodBuffDuration = 60 * 60 * 4;

        //BrightDustMix
        public int brightDustBuffDuration = 60 * 60 * 3;

        //Ability charges
        public int maxCharges;
        public int healingMixCharges, maxHealthMixCharges, defensiveMixCharges, damageBoostCharges, poisonMixCharges, explosiveMixCharges, brightDustMixCharges, luckyRodMixCharges = 0;

        public ApothecaryAbilities selectedAbility;

        public override void ResetEffects()
        {
            maxCharges = 6;

            poisonMixBuffDuration = 60 * 6;

            base.ResetEffects();
        }

        public override void PreUpdate()
        {
            //Aghanims grants small chance to make 2 mixes at once or another random mix
            
            for(int i = 0; i < 10; i++)
                    cooldowns[i]--;

            _poisonBuffCurrentDuration--;
            base.PreUpdate();
        }

        public override void PostUpdateEquips()
        {
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            //Base
            healingMixCooldown = (int)(healingMixCooldownBase * p.cooldownReduction);

            //Scalings
            poisonDuration = 60 * 7 + poisonDurationPerLevel * p.globalClassLevel.Count;

            base.PostUpdateEquips();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(_poisonBuffCurrentDuration > 0)
                target.GetGlobalNPC<ACMGlobalNPC>().apothecaryPoison = poisonDuration;

            base.OnHitNPC(target, hit, damageDone);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            int classLevel = p.globalClassLevel.Count;
            int A1Cooldown = (int)(p.ability1MaxCooldown * p.cooldownReduction * p.ability1cdr);
            int A2Cooldown = (int)(p.ability2MaxCooldown * p.cooldownReduction * p.ability2cdr);

            if (Main.myPlayer == Player.whoAmI && !Player.dead && p.equippedClass == "Apothecary")
            {
                //Apothecary UI
                if (ACM2.ClassAbility2.JustReleased)
                {
                    if (ModContent.GetInstance<ACM2ModSystem>()._ApothecaryUI.CurrentState == null)
                    {
                        ModContent.GetInstance<ACM2ModSystem>()._ApothecaryUI.SetState(new ApothecaryUI());
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                    }
                    else
                    {
                        ModContent.GetInstance<ACM2ModSystem>()._ApothecaryUI.SetState(null);
                        SoundEngine.PlaySound(SoundID.MenuClose);
                    }
                }

                //Ability casting
                if (ACM2.ClassAbility1.JustPressed && p.ability2Cooldown >= A2Cooldown * 60)
                {
                    switch (selectedAbility)
                    {
                        case ApothecaryAbilities.HealingMix:
                            
                            if(healingMixCharges > 0 && cooldowns[(int)ApothecaryAbilities.HealingMix] <= 0)
                            {
                                cooldowns[(int)ApothecaryAbilities.HealingMix] = (int)(healingMixCooldownBase * p.cooldownReduction);
                                int heal = (int)(Player.statLifeMax2 * healingMixHeal * p.healingPower);
                                if (heal < 1)
                                    heal = 1;
                                p.HealPlayer(0, 1, heal); //talent tree inrease healing speed to 2
                                healingMixCharges--;
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"Healing mixes left: {healingMixCharges}", true);
                                SoundEngine.PlaySound(SoundID.Item3);
                            }
                            else
                            {
                                if(cooldowns[(int)ApothecaryAbilities.HealingMix] > 0)
                                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"Healing mix is on cooldown!", true);
                                else
                                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"No healing mixes left!", true);

                                SoundEngine.PlaySound(SoundID.MenuClose);
                            }
                            break;

                        case ApothecaryAbilities.MaxHealthMix:
                            if (maxHealthMixCharges > 0 && cooldowns[(int)ApothecaryAbilities.MaxHealthMix] <= 0)
                            {
                                if(Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    Player.AddBuff(ModContent.BuffType<Buffs.Apothecary.ApothecaryMaxHealthMix>(), maxHealthMixDurationAndCooldown);
                                }
                                else
                                {
                                    for (int i = 0; i < 255; i++)
                                    {
                                        if (Main.player[i].active && !Main.player[i].dead)
                                        {
                                            ModPacket packet = Mod.GetPacket();
                                            packet.Write((byte)ACM2.ACMHandlePacketMessage.BuffPlayer);
                                            packet.Write((byte)i);
                                            packet.Write(ModContent.BuffType<Buffs.Apothecary.ApothecaryMaxHealthMix>());
                                            packet.Write(maxHealthMixDurationAndCooldown);
                                            packet.Send(-1, -1);
                                        }
                                    }
                                }
                                    
                                cooldowns[(int)ApothecaryAbilities.MaxHealthMix] = maxHealthMixDurationAndCooldown;
                                maxHealthMixCharges--;
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"Max health mixes left: {maxHealthMixCharges}", true);
                                SoundEngine.PlaySound(SoundID.Item3);
                            }
                            else
                            {
                                if (cooldowns[(int)ApothecaryAbilities.MaxHealthMix] > 0)
                                {
                                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"Max health mix is already in effect!", true);
                                }
                                else
                                {
                                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"No max health mixes left!", true);
                                }

                                SoundEngine.PlaySound(SoundID.MenuClose);
                            }
                            break;

                        case ApothecaryAbilities.DefensiveMix:
                            break;

                        case ApothecaryAbilities.PoisonMix:
                            if(poisonMixCharges > 0)
                            {
                                _poisonBuffCurrentDuration = poisonMixBuffDuration;
                                poisonMixCharges--;
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"Poison mixes left: {poisonMixCharges}", true);
                                SoundEngine.PlaySound(SoundID.LiquidsHoneyWater);
                            }
                            else
                            {
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"No poison mixes left!", true);
                                SoundEngine.PlaySound(SoundID.MenuClose);
                            }
                            break;

                        case ApothecaryAbilities.ExplosiveClusterMix:
                            break;

                        case ApothecaryAbilities.LuckyRodMix:
                            if (luckyRodMixCharges > 0)
                            {
                                Player.AddBuff(BuffID.Fishing, luckyRodBuffDuration);
                                Player.AddBuff(BuffID.Sonar, luckyRodBuffDuration);
                                luckyRodMixCharges--;
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"Lucky rod mixes left: {luckyRodMixCharges}", true);
                                SoundEngine.PlaySound(SoundID.Item3);

                                
                            }
                            else
                            {
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"No lucky rod mixes left!", true);
                                SoundEngine.PlaySound(SoundID.MenuClose);
                            }
                            break;

                        case ApothecaryAbilities.BrightDustMix:
                            if (brightDustMixCharges > 0)
                            {
                                Player.AddBuff(BuffID.Shine, brightDustBuffDuration);
                                Player.AddBuff(BuffID.NightOwl, brightDustBuffDuration);
                                brightDustMixCharges--;
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"Bright dust mixes left: {brightDustMixCharges}", true);
                                SoundEngine.PlaySound(SoundID.Item20);

                                for (int i = 0; i < 16; i++)
                                {
                                    Vector2 pos = new Vector2(Player.Center.X - Player.height / 2, Player.Center.Y - Player.width / 2);
                                    var d = Dust.NewDust(pos, 32, 32, DustID.Firework_Yellow, Main.rand.NextFloat(-4f, 4f), -6, 0);
                                    Main.dust[d].noGravity = false;
                                }

                                for (int i = 0; i < 14; i++)
                                {
                                    Vector2 pos = new Vector2(Player.Center.X - Player.height / 2, Player.Center.Y - Player.width / 2);
                                    var d = Dust.NewDust(pos, 32, 32, DustID.Firework_Blue, Main.rand.NextFloat(-3f, 3f), -5, 0);
                                    Main.dust[d].noGravity = false;
                                }
                            }
                            else
                            {
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, $"No bright dust mixes left!", true);
                                SoundEngine.PlaySound(SoundID.MenuClose);
                            }
                            break;
                    }
                }
            }
            base.ProcessTriggers(triggersSet);
        }

        public override void SaveData(TagCompound tag)
        {
            //Charges
            tag.Add("healingMixCharges", healingMixCharges);
            tag.Add("maxHealthMixCharges", maxHealthMixCharges);
            tag.Add("poisonMixCharges", poisonMixCharges);

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            //Charges
            healingMixCharges = tag.GetInt("healingMixCharges");
            maxHealthMixCharges = tag.GetInt("maxHealthMixCharges");
            poisonMixCharges = tag.GetInt("poisonMixCharges");

            base.LoadData(tag);
        }

        void OutOfIngredient(int ingredientItem)
        {
            ACMPlayer p = Player.GetModPlayer<ACMPlayer>();

            p.AddAbilityCooldown(2, 1);
            Main.NewText($"I'm out of [i:{ingredientItem}] to create the mix!");
        }
    }

    #region Scrapped, might come in handy later
    //int ingredient = ItemID.Mushroom;
    //for(int i = 0; i < 50; i++)
    //{
    //    //Regular inventory
    //    if (Player.inventory[i].type == ingredient)
    //    {
    //        p.AddAbilityCooldown(2, p.ability2MaxCooldown);
    //        Player.inventory[i].stack--;
    //        if (Player.inventory[i].stack <= 0) Player.inventory[i].TurnToAir();
    //        int heal = (int)(Player.statLifeMax2 * healingMixHeal * p.healingPower);
    //        if (heal < 1)
    //            heal = 1;
    //        p.HealPlayer(0, 3, heal); //talent tree inrease healing speed to 2
    //        break;
    //    }
    //
    //    //Safe bank
    //    if (i < 40)
    //    {
    //        if (Player.bank2.item[i].type == ingredient)
    //        {
    //            p.AddAbilityCooldown(2, p.ability2MaxCooldown);
    //            Player.bank2.item[i].stack--;
    //            if (Player.bank2.item[i].stack <= 0) Player.bank2.item[i].TurnToAir();
    //            int heal = (int)(Player.statLifeMax2 * healingMixHeal * p.healingPower);
    //            if (heal < 1)
    //                heal = 1;
    //            p.HealPlayer(0, 3, heal); //talent tree inrease healing speed to 2
    //            break;
    //        }
    //    }
    //
    //    if (i >= 49)
    //        OutOfIngredient(ingredient);   
    //}
    #endregion
}