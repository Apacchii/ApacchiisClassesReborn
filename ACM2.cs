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

namespace ApacchiisClassesMod2
{
    public class ACM2 : Mod
    {
        public static ModKeybind ClassAbility1;
        public static ModKeybind ClassAbility2;
        public static ModKeybind ClassAbilityUltimate;
        public static ModKeybind Menu;

        //public ACM2()
        //{
        //    Properties = new ModProperties()
        //    {
        //        Autoload = true,
        //        AutoloadGores = true,
        //        AutoloadSounds = true
        //    };
        //}

        

        public override void Load()
        {
            ClassAbility1 = KeybindLoader.RegisterKeybind(this, "Class Ability: 1", "Q");
            ClassAbility2 = KeybindLoader.RegisterKeybind(this, "Class Ability: 2", "C");
            ClassAbilityUltimate = KeybindLoader.RegisterKeybind(this, "Class Ability: Ultimate", "V");
            Menu = KeybindLoader.RegisterKeybind(this, "Menu", "N");



            base.Load();
        }

        internal enum ACMHandlePacketMessage : byte
        {
            SyncBosses,
            SyncTalentPoints,

            PlayerSyncPlayer,
            HealPlayer,
            HealPlayerFast,
            HealPlayerMedium,
            HealPlayerSlow,
            HealPlayerSnail,
            HealPlayerSecond,
            SyncRegenStats,
            SyncPlayerHealth,
            BuffPlayer,
            SyncPlayerBuffs,

            SendChatMessage,
            PlaySyncedSound,
        }


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ACMHandlePacketMessage msgType = (ACMHandlePacketMessage)reader.ReadByte();

            switch (msgType)
            {
                case ACMHandlePacketMessage.SendChatMessage:
                    string message = reader.ReadString();
                    byte colorR = reader.ReadByte();
                    byte colorG = reader.ReadByte();
                    byte colorB = reader.ReadByte();

                    Color textColor = new Color(colorR, colorG, colorB);
                    NetworkText text;
                    text = NetworkText.FromKey(message);
                    ChatHelper.BroadcastChatMessage(text, textColor);
                    break;

                case ACMHandlePacketMessage.PlaySyncedSound:

                    string soundPath = reader.ReadString();
                    Vector2 pos = reader.ReadVector2();

                    if(Main.netMode != NetmodeID.Server)
                        SoundEngine.PlaySound(new SoundStyle(soundPath), pos);
                    break;

                case ACMHandlePacketMessage.SyncBosses:
                    int playernumber = reader.ReadInt32();
                    string playerClass = reader.ReadString();
                    string bossDefeated = reader.ReadString();

                    ACMPlayer acmPlayer = Main.player[playernumber].GetModPlayer<ACMPlayer>();


                    switch (playerClass)
                    {
                        case "Vanguard":
                            if (!acmPlayer.vanguardDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.vanguardDefeatedBosses.Add(bossDefeated);
                                acmPlayer.vanguardSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Blood Mage":
                            if (!acmPlayer.bloodMageDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.bloodMageDefeatedBosses.Add(bossDefeated);
                                acmPlayer.bloodMageSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Commander":
                            if (!acmPlayer.commanderDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.commanderDefeatedBosses.Add(bossDefeated);
                                acmPlayer.commanderSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Scout":
                            if (!acmPlayer.scoutDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.scoutDefeatedBosses.Add(bossDefeated);
                                acmPlayer.scoutSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Soulmancer":
                            if (!acmPlayer.soulmancerDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.soulmancerDefeatedBosses.Add(bossDefeated);
                                acmPlayer.soulmancerSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Crusader":
                            if (!acmPlayer.crusaderDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.crusaderDefeatedBosses.Add(bossDefeated);
                                acmPlayer.crusaderSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Gambler":
                            if (!acmPlayer.gamblerDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.gamblerDefeatedBosses.Add(bossDefeated);
                                acmPlayer.gamblerSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Plague":
                            if (!acmPlayer.plagueDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.plagueDefeatedBosses.Add(bossDefeated);
                                acmPlayer.plagueSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;
                    }
                    acmPlayer.levelUpText = true;
                    break;

                case ACMHandlePacketMessage.BuffPlayer:
                    byte playerToBuff = reader.ReadByte();
                    int buffType = reader.ReadInt32();
                    int duration = reader.ReadInt32();

                    Main.player[playerToBuff].AddBuff(buffType, duration);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.BuffPlayer);
                        packet.Write((byte)playerToBuff);
                        packet.Write(buffType);
                        packet.Write(duration);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerFast:

                    byte PlayerNumber2 = reader.ReadByte();
                    int totalHealAmount = reader.ReadInt32();

                    Main.player[PlayerNumber2].GetModPlayer<ACMPlayer>().healthToRegen += totalHealAmount;
                    Main.player[PlayerNumber2].HealEffect(totalHealAmount);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((int)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber2);
                        packet.Write(totalHealAmount);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerMedium:

                    byte PlayerNumber3 = reader.ReadByte();
                    int totalHealAmountMedium = reader.ReadInt32();

                    Main.player[PlayerNumber3].GetModPlayer<ACMPlayer>().healthToRegenMedium += totalHealAmountMedium;
                    Main.player[PlayerNumber3].HealEffect(totalHealAmountMedium);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber3);
                        packet.Write(0);
                        packet.Write(totalHealAmountMedium);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerSlow:

                    byte PlayerNumber4 = reader.ReadByte();
                    int totalHealAmountSlow = reader.ReadInt32();

                    Main.player[PlayerNumber4].GetModPlayer<ACMPlayer>().healthToRegenSlow += totalHealAmountSlow;
                    Main.player[PlayerNumber4].HealEffect(totalHealAmountSlow);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber4);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(totalHealAmountSlow);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerSnail:

                    byte PlayerNumber5 = reader.ReadByte();
                    int totalHealAmountSnail = reader.ReadInt32();

                    Main.player[PlayerNumber5].GetModPlayer<ACMPlayer>().healthToRegenSnail += totalHealAmountSnail;
                    Main.player[PlayerNumber5].HealEffect(totalHealAmountSnail);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber5);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(totalHealAmountSnail);
                        packet.Write(0);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerSecond:

                    byte PlayerNumber6 = reader.ReadByte();
                    int totalHealAmountSecond = reader.ReadInt32();

                    Main.player[PlayerNumber6].GetModPlayer<ACMPlayer>().healthToRegenSecond += totalHealAmountSecond;
                    Main.player[PlayerNumber6].HealEffect(totalHealAmountSecond);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber6);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(totalHealAmountSecond);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayer:

                    byte PlayerNumber = reader.ReadByte();
                    int healAmount = reader.ReadInt32();

                    Main.player[PlayerNumber].statLife += healAmount;
                    Main.player[PlayerNumber].HealEffect(healAmount);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncPlayerHealth);
                        packet.Write((byte)PlayerNumber);
                        packet.Write(Main.player[PlayerNumber].statLife);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.SyncPlayerHealth:

                    PlayerNumber = reader.ReadByte();
                    int PlayerHealth = reader.ReadInt32();

                    Main.player[PlayerNumber].statLife = PlayerHealth;
                    break;

                case ACMHandlePacketMessage.SyncRegenStats:

                    PlayerNumber = reader.ReadByte();
                    int regenFast = reader.ReadInt32();
                    int regenMedium = reader.ReadInt32();
                    int regenSlow = reader.ReadInt32();
                    int regenSnail = reader.ReadInt32();
                    int regenSecond = reader.ReadInt32();

                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegen += regenFast;
                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegenMedium += regenMedium;
                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegenSlow += regenSlow;
                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegenSnail += regenSnail;
                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegenSecond += regenSecond;

                    break;

                default:
                    Logger.WarnFormat("ACM2:Message type unknown: {0}", msgType);
                    break;
            }
        }
    }
}