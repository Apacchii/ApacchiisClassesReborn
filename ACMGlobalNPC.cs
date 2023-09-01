using ApacchiisClassesMod2.Items.Classes;
using ApacchiisClassesMod2.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Buffs.Plague;

namespace ApacchiisClassesMod2
{
	public class ACMGlobalNPC : GlobalNPC
	{
        private int _globalTimer;
        public int plagueDotDamage;
        public int plagueDotStrikes;

        public int battleCryBoost = 0;
        bool hasIncreasedMaxHealth = false;

        public bool plagueInfection;

        protected override bool CloneNewInstances => true; 
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        // Dont do this, this'll make every npc immune to all de/buffs except Infection
        // Im going insane i cant make Mech bosses take DoT damage for plague's kit, help
        //public override void SetStaticDefaults()
        //{
        //    for (int i = 0; i < BuffLoader.BuffCount; i++)
        //    {
        //        if(i != ModContent.BuffType<PlagueInfection>())
        //        {
        //            NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData {
        //            {
        //                SpecificallyImmuneTo = new int[]{i}
        //            };
        //        }
        //    }
        //
        //    base.SetStaticDefaults();
        //}

        //public bool vanguardSpeared = false;
        //public Vector2 vanguardSpearPos;

        public override void ResetEffects(NPC npc)
        {
            npc.takenDamageMultiplier = 1f;
            plagueInfection = false;
            base.ResetEffects(npc);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (plagueInfection && !npc.dontTakeDamage && !npc.friendly)
            {
                if (npc.boss)
                {
                    //Only deal 10% DoT to worm enemies
                    if (npc.realLife != 0)
                    {
                        damage = npc.lifeMax / 10000;
                        npc.lifeRegen -= (int)(npc.lifeMax * .005f * 2);
                    }
                    else
                    {
                        damage = npc.lifeMax / 1000;
                        npc.lifeRegen -= (int)(npc.lifeMax * .005f * 2);
                    }
                }
                else
                {
                    damage = npc.lifeMax / 50;
                    npc.lifeRegen -= (int)(npc.lifeMax * .05f * 2);
                }
            }
            base.UpdateLifeRegen(npc, ref damage);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(!npc.boss && !npc.friendly && npc.lifeMax > 5)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Relics.RandomRelic>(), 225, 1, 1));

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
            Player Player = Main.player[Main.myPlayer];

            if (!hasIncreasedMaxHealth)
            {
                if (npc.lifeMax > 5 && !npc.boss && !npc.townNPC && !npc.CountsAsACritter)
                {
                    npc.lifeMax = (int)(npc.lifeMax * Configs._ACMConfigServer.Instance.enemyHealthMultiplier);
                    npc.life = npc.lifeMax;
                }

                if (npc.boss)
                {
                    npc.lifeMax = (int)(npc.lifeMax * Configs._ACMConfigServer.Instance.bossHealthMultiplier);
                    npc.life = npc.lifeMax;
                }

                hasIncreasedMaxHealth = true;
            }

            if (battleCryBoost > 0)
            {
                battleCryBoost--;
                npc.takenDamageMultiplier += Player.GetModPlayer<ACMPlayer>().commanderCryBonusDamage;
            }


            //Custom DoT (mech bosses &&++ dont take vanilla dot)
            // (I failed... Retry sometime and implement for Plague if successful)
            //_globalTimer++;
            //if(plagueDotStrikes > 0)
            //{
            //    if (_globalTimer % 60 == 0)
            //    {
            //        npc.SimpleStrikeNPC(plagueDotDamage / 10, 0);
            //        plagueDotDamage -= 
            //        plagueDotStrikes--;
            //    }
            //}
            //else
            //{
            //    plagueDotDamage = 0;
            //}

            base.PostAI(npc);
        }

        public override void OnKill(NPC npc)
        {
            if (npc.boss)
            {
                //if (Main.netMode == NetmodeID.Server)
                //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("To level up, type '/acr levelUp' in chat if you haven't defeated this boss before. [TEMPORARY MULTIPLAYER BUG WORKAROUND]"), Color.White);

                for (int playerToUpdate = 0; playerToUpdate < 255; playerToUpdate++)
                {
                    if(Main.player[playerToUpdate].active)
                    {
                        var acmPlayer = Main.player[playerToUpdate].GetModPlayer<ACMPlayer>();

                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            if (acmPlayer.hasVanguard)
                            {
                                if (!acmPlayer.vanguardDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.vanguardDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.vanguardSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }

                            if (acmPlayer.hasBloodMage)
                            {
                                if (!acmPlayer.bloodMageDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.bloodMageDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.bloodMageSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }

                            if (acmPlayer.hasCommander)
                            {
                                if (!acmPlayer.commanderDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.commanderDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.commanderSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }

                            if (acmPlayer.hasScout)
                            {
                                if (!acmPlayer.scoutDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.scoutDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.scoutSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }

                            if (acmPlayer.hasSoulmancer)
                            {
                                if (!acmPlayer.soulmancerDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.soulmancerDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.soulmancerSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }

                            if (acmPlayer.hasCrusader)
                            {
                                if (!acmPlayer.crusaderDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.crusaderDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.crusaderSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }

                            if (acmPlayer.equippedClass == "Gambler")
                            {
                                if (!acmPlayer.gamblerDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.gamblerDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.gamblerSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }

                            if (acmPlayer.equippedClass == "Plague")
                            {
                                if (!acmPlayer.plagueDefeatedBosses.Contains(npc.TypeName))
                                {
                                    acmPlayer.plagueDefeatedBosses.Add(npc.TypeName);
                                    acmPlayer.plagueSkillPoints++;
                                    //acmPlayer.levelUpText = true;
                                    acmPlayer.cardsPoints += 2;
                                }
                            }
                        }
                        else
                        {
                            if (Main.netMode == NetmodeID.Server)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write(playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.TypeName);
                                packet.Send();
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