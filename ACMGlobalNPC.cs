using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2
{
	public class ACMGlobalNPC : GlobalNPC
	{
        Player Player = Main.player[Main.myPlayer];
        public int battleCryBoost = 0;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        //public bool vanguardSpeared = false;
        //public Vector2 vanguardSpearPos;

        public override void ResetEffects(NPC npc)
        {
            npc.takenDamageMultiplier = 1f;
            base.ResetEffects(npc);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(!npc.boss && !npc.friendly && npc.lifeMax > 5)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Relics.RandomRelic>(), 250, 1, 1));

            base.ModifyNPCLoot(npc, npcLoot);
        }

        //public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        //{
        //    // Dropping relics
        //    //globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Relics.RandomRelic>(), 300, 1, 1));
        //
        //    base.ModifyGlobalLoot(globalLoot);
        //}

        public override void PostAI(NPC npc)
        {
            if(battleCryBoost > 0)
            {
                battleCryBoost--;
                npc.takenDamageMultiplier += Player.GetModPlayer<ACMPlayer>().commanderCryBonusDamage;
            }
                
            base.PostAI(npc);
        }

        public override void OnKill(NPC npc)
        {
            if (npc.boss)
            {
                for (int playerToUpdate = 0; playerToUpdate < 255; playerToUpdate++)
                {
                    if(Main.player[playerToUpdate].active)
                    {
                        var acmPlayer = Main.player[playerToUpdate].GetModPlayer<ACMPlayer>();

                        //Vanguard List
                        if (acmPlayer.hasVanguard && !acmPlayer.vanguardDefeatedBosses.Contains(npc.GivenOrTypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.GivenOrTypeName);
                                packet.Send();

                                var packet2 = Mod.GetPacket();
                                packet2.Write((byte)ACM2.ACMHandlePacketMessage.SyncTalentPoints);
                                packet2.Write((byte)playerToUpdate);
                                packet2.Write(acmPlayer.equippedClass);
                                packet2.Send();
                            }
                            else
                            {
                                acmPlayer.vanguardDefeatedBosses.Add(npc.GivenOrTypeName);
                                acmPlayer.vanguardTalentPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }

                        //Blood Mage List
                        if (acmPlayer.hasBloodMage && !acmPlayer.bloodMageDefeatedBosses.Contains(npc.GivenOrTypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.GivenOrTypeName);
                                packet.Send();

                                var packet2 = Mod.GetPacket();
                                packet2.Write((byte)ACM2.ACMHandlePacketMessage.SyncTalentPoints);
                                packet2.Write((byte)playerToUpdate);
                                packet2.Write(acmPlayer.equippedClass);
                                packet2.Send();
                            }
                            else
                            {
                                acmPlayer.bloodMageDefeatedBosses.Add(npc.GivenOrTypeName);
                                acmPlayer.bloodMageTalentPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }

                        //Commander List
                        if (acmPlayer.hasCommander && !acmPlayer.commanderDefeatedBosses.Contains(npc.GivenOrTypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.GivenOrTypeName);
                                packet.Send();

                                var packet2 = Mod.GetPacket();
                                packet2.Write((byte)ACM2.ACMHandlePacketMessage.SyncTalentPoints);
                                packet2.Write((byte)playerToUpdate);
                                packet2.Write(acmPlayer.equippedClass);
                                packet2.Send();
                            }
                            else
                            {
                                acmPlayer.commanderDefeatedBosses.Add(npc.GivenOrTypeName);
                                acmPlayer.commanderTalentPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }

                        //Scout List
                        if (acmPlayer.hasScout && !acmPlayer.scoutDefeatedBosses.Contains(npc.GivenOrTypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.GivenOrTypeName);
                                packet.Send();

                                var packet2 = Mod.GetPacket();
                                packet2.Write((byte)ACM2.ACMHandlePacketMessage.SyncTalentPoints);
                                packet2.Write((byte)playerToUpdate);
                                packet2.Write(acmPlayer.equippedClass);
                                packet2.Send();
                            }
                            else
                            {
                                acmPlayer.scoutDefeatedBosses.Add(npc.GivenOrTypeName);
                                acmPlayer.scoutTalentPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }
                    }
                } 
            }

            base.OnKill(npc);
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //if (battleCryBoost > 0)
            //    spriteBatch.Draw(ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/BattleCry", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, new Vector2(npc.Center.X, npc.position.Y - 20), default);

            if (battleCryBoost > 0)
            {
                Texture2D texture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/BattleCry", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        npc.position.X - Main.screenPosition.X + npc.width * 0.5f,
                        npc.position.Y - Main.screenPosition.Y - npc.height * .5f - 24
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    0,
                    texture.Size() * 0.5f,
                    npc.scale,
                    SpriteEffects.None,
                    0f
                );
            }
            base.PostDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}