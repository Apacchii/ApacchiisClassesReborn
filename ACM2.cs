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

namespace ApacchiisClassesMod2
{
    public class ACM2 : Mod
    {
        Player Player = Main.player[Main.myPlayer];

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
            SyncPlayerHealth,
            BuffPlayer,
            SyncPlayerBuffs,
        }


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ACMHandlePacketMessage msgType = (ACMHandlePacketMessage)reader.ReadByte();

            switch (msgType)
            {
                case ACMHandlePacketMessage.SyncBosses:
                    byte playernumber = reader.ReadByte();
                    string playerClass = reader.ReadString();
                    string bossDefeated = reader.ReadString();

                    ACMPlayer acmPlayer = Main.player[playernumber].GetModPlayer<ACMPlayer>();

                    switch (playerClass)
                    {
                        case "Vanguard":
                            if (!acmPlayer.vanguardDefeatedBosses.Contains(bossDefeated))
                                acmPlayer.vanguardDefeatedBosses.Add(bossDefeated);
                            break;

                        case "Blood Mage":
                            if (!acmPlayer.bloodMageDefeatedBosses.Contains(bossDefeated))
                                acmPlayer.bloodMageDefeatedBosses.Add(bossDefeated);
                            break;

                        case "Commander":
                            if (!acmPlayer.commanderDefeatedBosses.Contains(bossDefeated))
                                acmPlayer.commanderDefeatedBosses.Add(bossDefeated);
                            break;

                        case "Scout":
                            if (!acmPlayer.scoutDefeatedBosses.Contains(bossDefeated))
                                acmPlayer.scoutDefeatedBosses.Add(bossDefeated);
                            break;
                    }
                    break;

                case ACMHandlePacketMessage.SyncTalentPoints:
                    byte playernumber2 = reader.ReadByte();
                    string playerClass2 = reader.ReadString();

                    ACMPlayer acmPlayer2 = Main.player[playernumber2].GetModPlayer<ACMPlayer>();

                    switch (playerClass2)
                    {
                        case "Vanguard":
                                acmPlayer2.vanguardTalentPoints++;
                            break;

                        case "Blood Mage":
                            //if (acmPlayer2.bloodMageLevel < 10)
                                acmPlayer2.bloodMageTalentPoints++;
                            //else
                                //acmPlayer2.bloodMageSpecPoints += 2;
                            break;

                        case "Commander":
                                acmPlayer2.commanderTalentPoints++;
                            break;

                        case "Scout":
                                acmPlayer2.scoutTalentPoints++;
                            break;
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
            }
        }
    }
}