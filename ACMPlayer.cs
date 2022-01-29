using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace ApacchiisClassesMod2
{
	public class ACMPlayer : ModPlayer
	{
        int resetHUD = 60;

        #region Relics
        public bool hasBleedingMoonStone;
        public bool hasAghanims;
        public bool hasUnstableConcoction;
        bool isUnstableConcoctionReady;
        public bool hasNeterihsToken;
        #endregion

        public string equippedClass;
        public bool hasClass = false;
        public bool hasRelic = false;
        public int globalLevel = 0;
        public int inBattleTimer = 0;
        int outOfBattleTimer = 480;
        int outOfBattle2 = 3;
        int inBattleTimeMax = 480;
        int outOfBattleTimeMax = 180;
        bool inBattle = false;
        public int ultCharge = 0;
        public int ultChargeMax = 12000;
        public float defenseMult = 1f;
        public float lifeMult = 1f;
        public float manaMult = 1f;
        public float pSecHealthRegen = 0f;
        int pSecHealthTimer = 60;
        int globalSingleSecondTimer = 60;
        bool ultSound = false;
        bool a1Sound = false;
        bool a2Sound = false;
        public int healthToRegen;
        public int healthToRegenMedium;
        int healthToRegenMediumTimer = 0;
        public int healthToRegenSlow;
        int healthToRegenSlowTimer = 0;

        public bool devTool = false;

        public int ability1Cooldown;
        public int ability1MaxCooldown;
        public int ability2Cooldown;
        public int ability2MaxCooldown;
        public int ability3Cooldown;

        public float cooldownReduction = 1f;
        public float ultCooldownReduction = 1f;
        public float abilityPower = 1f;
        public float abilityDuration = 1f;
        public float attackSpeed = 1f; // Positive is +
        public float trueEndurance = 1f;
        public float dodgeChance = 0f;
        public float critDamageMult = 1f;

        #region Player Stats
        public int enemiesKilled;
        public int damageDealt;
        public int timesDied;
        public int highestDPS;
        public int highestCrit;
        bool canAddDeaths = true;
        #endregion

        #region Vanguard
        public bool hasVanguard = false;
        public int vanguardLevel = 0;
        public int vanguardTalentPoints = 0;
        public int vanguardSpentTalentPoints = 0;

        public string vanguardTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string vanguardTalent_2 = "N";
        public string vanguardTalent_3 = "N";
        public string vanguardTalent_4 = "N";
        public string vanguardTalent_5 = "N";
        public string vanguardTalent_6 = "N";
        public string vanguardTalent_7 = "N";
        public string vanguardTalent_8 = "N";
        public string vanguardTalent_9 = "N";
        public string vanguardTalent_10 = "N";

        bool vanguardShieldUp = false;
        bool vanguardShieldRegen = false;
        int vanguardShieldRegenTimer = 0;

        public float vanguardPassiveReflectAmount = 1f;

        public float vanguardShieldBaseDamageReduction = .2f;
        public float vanguardShieldDamageReduction = .2f;
        public int vanguardShieldBaseDuration = 480; //8s
        public int vanguardShieldDuration;
        public int vanguardShieldDurationPerLevel = 20;
        int vanguardShieldCurrentDuration = 0;
        int vanguardDustLocations = 20;
        bool vanguardDustFlag = false;
        int vanguardDustTimer = 5;

        public int vanguardSpearBaseDamage = 20;
        public int vanguardSpearDamage;
        bool vanguardSpearHeal = false;

        public float vanguardUltimateBossExecute = .05f;
        public int vanguardSwordBaseDamage = 24;
        public int vanguardSwordDamage;
        #endregion

        #region Blood Mage
        public bool hasBloodMage = false;
        public int bloodMageLevel = 0;
        public int bloodMageTalentPoints = 0;
        public int bloodMageSpentTalentPoints = 0;
        public int bloodMageSpecPoints = 0;

        public string bloodMageTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string bloodMageTalent_2 = "N";
        public string bloodMageTalent_3 = "N";
        public string bloodMageTalent_4 = "N";
        public string bloodMageTalent_5 = "N";
        public string bloodMageTalent_6 = "N";
        public string bloodMageTalent_7 = "N";
        public string bloodMageTalent_8 = "N";
        public string bloodMageTalent_9 = "N";
        public string bloodMageTalent_10 = "N";

        public string specBloodMage = "N";
        public int specBloodMage_1;
        public int specBloodMage_TransfusionDamageBase;
        public int specBloodMage_2;
        public float specBloodMage_MaxHealthBase;
        public int specBloodMage_3;
        public float specBloodMage_UltCostReductionBase;
        public int specBloodMage_4;
        public float specBloodMage_TransfusionMaxHealBase;
        public int specBloodMage_5;
        public float specBloodMage_EnchantDamageBase;
        public int specBloodMage_6;
        public float specBloodMage_UltHealBase;

        public int bloodMagePassiveBaseMaxStacks = 20;
        public int bloodMagePassiveMaxStacks;
        public int bloodMagePassiveCurrentStacks;
        public float bloodMageBasePassiveRegen = .001f;
        public float bloodMagePassiveRegen;

        public float bloodMageSiphonHealMax = .15f;
        public int bloodMageSiphonBaseDamage = 20;
        public int bloodMageSiphonDamage;

        public bool bloodMageBloodEnchantment = false;
        public float bloodMageBaseHealthDrain = .07f;
        //public float bloodMageHealthDrain;
        public float bloodMageBaseDamageGain = .1f;
        public float bloodMageDamageGain;

        public float bloodMageBaseUltRegen = .02f;
        public float bloodMageUltRegen;
        public int bloodMageUltTicks = 8;
        int bloodMageCurUltTicks = 0;
        
        int bloodMageInBattle = 0;
        #endregion

        #region Commander
        public bool hasCommander;
        public int commanderLevel = 0;
        public int commanderTalentPoints = 0;
        public int commanderSpentTalentPoints = 0;

        public float commanderPassiveEndurance = .01f;

        public int commanderBannerRange = 200;
        public int commanderBannerDuration = 60 * 10;
        public float commanderBannerEndurance = .85f;
        public float commanderBannerDamage = 1.1f;
        public int commanderBannerBuffDuration = 0;
        public int commanderBannerPersist = 60;

        public int commanderCryRange = 400;
        public int commanderCryDamage;
        public int commanderCryBaseDamage = 20;
        public int commanderCryDamageLevel = 4;
        public int commanderCryDuration = 300;
        public float commanderCryBonusDamage = .15f;

        public int commanderUltDuration = 60 * 4;
        public bool commanderUltActive = false;

        public string commanderTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string commanderTalent_2 = "N";
        public string commanderTalent_3 = "N";
        public string commanderTalent_4 = "N";
        public string commanderTalent_5 = "N";
        public string commanderTalent_6 = "N";
        public string commanderTalent_7 = "N";
        public string commanderTalent_8 = "N";
        public string commanderTalent_9 = "N";
        public string commanderTalent_10 = "N";
        #endregion

        #region Scout
        public bool hasScout = false;
        public int scoutLevel = 0;
        public int scoutTalentPoints = 0;
        public int scoutSpentTalentPoints = 0;

        public bool scoutCanDoubleJump = true;
        public bool scoutOtherJump = false;
        public float scoutPassiveSpeedBonus;

        public int scoutColaDuration;
        public int scoutColaCurDuration;
        public float scoutColaDamageBonus;
        public float scoutColaDamageBonusLevel;

        public int scoutTrapBaseDamage;
        public int scoutTrapDamage;
        public int scoutTrapDamageLevel;
        public int scoutTrapRange;
        public int scoutTrapChargeRate;

        public int scoutUltDuration;
        public int scoutUltInvDuration;
        public int scoutUltCurDuration;
        public int scoutUltInvCurDuration;
        public float scoutUltSpeed;
        public float scoutUltSpeedLevel;
        public float scoutUltJump;

        public string scoutTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string scoutTalent_2 = "N";
        public string scoutTalent_3 = "N";
        public string scoutTalent_4 = "N";
        public string scoutTalent_5 = "N";
        public string scoutTalent_6 = "N";
        public string scoutTalent_7 = "N";
        public string scoutTalent_8 = "N";
        public string scoutTalent_9 = "N";
        public string scoutTalent_10 = "N";
        #endregion

        public override void Initialize()
        {
            bloodMageDefeatedBosses = new List<string>();
            scoutDefeatedBosses = new List<string>();
            vanguardDefeatedBosses = new List<string>();
            commanderDefeatedBosses = new List<string>();

            base.Initialize();
        }

        public override void ResetEffects()
        {
            #region Relics
            hasBleedingMoonStone = false;
            hasAghanims = false;
            hasUnstableConcoction = false;
            hasNeterihsToken = false;
            #endregion

            abilityPower = 1f;
            cooldownReduction = 1f;
            ultCooldownReduction = 1f;
            abilityDuration = 1f;
            attackSpeed = 1f;
            trueEndurance = 1f;
            defenseMult = 1f;
            lifeMult = 1f;
            manaMult = 1f;
            pSecHealthRegen = 0f;
            canAddDeaths = true;
            dodgeChance = 0f;
            critDamageMult = 1f;

            hasClass = false;
            hasRelic = false;
            equippedClass = "";

            devTool = false;

            #region Vanguard
            hasVanguard = false;
            ultChargeMax = 12000;
            ability1MaxCooldown = 1;
            ability2MaxCooldown = 1;
            vanguardShieldRegen = false;
            vanguardSpearHeal = false;
            vanguardPassiveReflectAmount = 1f;
            vanguardSpearBaseDamage = 20;
            vanguardSwordBaseDamage = 24;
            vanguardShieldBaseDuration = 480;
            vanguardShieldBaseDamageReduction = .2f;
            vanguardUltimateBossExecute = .05f;
            vanguardShieldDamageReduction = vanguardShieldBaseDamageReduction;
            #endregion

            #region Blood Mage
            if (!hasBloodMage)
                bloodMageBloodEnchantment = false;
            hasBloodMage = false;
            //bloodMageHealthDrain = bloodMageBaseHealthDrain;
            bloodMageBaseHealthDrain = .08f;
            bloodMagePassiveBaseMaxStacks = 20;
            bloodMagePassiveMaxStacks = bloodMagePassiveBaseMaxStacks + 2 * bloodMageLevel;
            bloodMageBaseDamageGain = .1f;
            bloodMageBasePassiveRegen = .0008f;
            bloodMageBaseUltRegen = .02f;
            bloodMageSiphonBaseDamage = 20;
            bloodMageSiphonHealMax = .15f;
            bloodMagePassiveRegen = bloodMageBasePassiveRegen;
            bloodMageDamageGain = bloodMageBaseDamageGain;
            bloodMageUltTicks = 8;
            bloodMageUltRegen = bloodMageBaseUltRegen;

            specBloodMage_TransfusionDamageBase = 2;
            specBloodMage_MaxHealthBase = .005f;
            specBloodMage_UltCostReductionBase = .004f;
            specBloodMage_TransfusionMaxHealBase = .005f;
            specBloodMage_EnchantDamageBase = .01f;
            specBloodMage_UltHealBase = .0015f;
            #endregion

            #region Commander
        hasCommander = false;
            commanderUltActive = false;
            commanderUltDuration = 60 * 4;
            commanderBannerEndurance = .85f;
            commanderBannerDamage = 1.1f;
            commanderBannerRange = 200;
            commanderBannerDuration = 60 * 10;
            commanderBannerPersist = 60;
            commanderCryRange = 375;
            commanderCryDamage = commanderCryBaseDamage;
            commanderCryBonusDamage = .15f;
            commanderCryDuration = 60 * 4;
            commanderPassiveEndurance = .01f;
            #endregion

            #region Scout
            hasScout = false;
            scoutPassiveSpeedBonus = .15f;
            scoutOtherJump = false;
            scoutColaDuration = 60 * 4;
            scoutColaDamageBonus = 1.25f;
            scoutColaDamageBonusLevel = .015f;
            scoutTrapBaseDamage = 20;
            scoutTrapDamageLevel = 7;
            scoutTrapDamage = scoutTrapBaseDamage;
            scoutTrapRange = 125;
            scoutTrapChargeRate = 4;
            scoutUltDuration = 12 * 60;
            scoutUltInvDuration = 3 * 60;
            scoutUltSpeed = .5f;
            scoutUltSpeedLevel = .08f;
            scoutUltJump = 2f;
            #endregion

            base.ResetEffects();
        }

        #region Level Lists
        public List<string> defeatedBosses = new List<string>()
        {
        };
        public List<string> vanguardDefeatedBosses = new List<string>()
        {
        };
        public List<string> bloodMageDefeatedBosses = new List<string>()
        {
        };
        public List<string> commanderDefeatedBosses = new List<string>()
        {
        };
        public List<string> scoutDefeatedBosses = new List<string>()
        {
        };
        #endregion

        #region MP Player Syncing
        public override void clientClone(ModPlayer clientClone)
        {
            ACMPlayer clone = clientClone as ACMPlayer;

            clone.vanguardTalentPoints = vanguardTalentPoints;
            clone.bloodMageTalentPoints = bloodMageTalentPoints;
            clone.commanderTalentPoints = commanderTalentPoints;
            clone.scoutTalentPoints = scoutTalentPoints;

            base.clientClone(clientClone);
        }
        // /\ & \/
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            ACMPlayer clone = clientPlayer as ACMPlayer;

            if (clone.vanguardTalentPoints != vanguardTalentPoints ||
                clone.bloodMageTalentPoints != bloodMageTalentPoints ||
                clone.commanderTalentPoints != commanderTalentPoints ||
                clone.scoutTalentPoints != scoutTalentPoints)
            {        
                var packet = Mod.GetPacket();
                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncTalentPoints);
                packet.Write((byte)Player.whoAmI);
                packet.Write(equippedClass);
                packet.Send();
            }
        }

        //public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        //{
        //    string vanguardBosses = string.Join("\n", commanderDefeatedBosses);
        //
        //    ModPacket packet = Mod.GetPacket();
        //    packet.Write((byte)ACM2.ACMHandlePacketMessage.PlayerSyncPlayer);
        //    packet.Write((byte)Player.whoAmI);
        //    packet.Write(vanguardDefeatedBosses.Count);
        //    packet.Write(vanguardBosses);
        //    packet.Send(toWho, fromWho);
        //    base.SyncPlayer(toWho, fromWho, newPlayer);
        //}
        #endregion

        

        public override float UseTimeMultiplier(Item item)
        {
            if(Player.HeldItem.pick > 0 || Player.HeldItem.axe > 0 || Player.HeldItem.hammer > 0)
                return 1f;
            else
                return 1f - (attackSpeed - 1f);
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            mediumCoreDeath = false;
            return new[] {
                new Item(ItemType<Items.WhiteCloth>()),
                new Item(ItemType<Items.ClassBook>()),
                //new Item(ItemType<Items.SuperZIP>()),
            };
        }

        public override void UpdateDead()
        {
            ultCharge = 0;
            inBattleTimer = 0;
            if (canAddDeaths)
            {
                timesDied++;
                canAddDeaths = false;
            }
                
            bloodMagePassiveCurrentStacks = 0;
            bloodMageBloodEnchantment = false;
            base.UpdateDead();
        }

        //public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        //{
        //    healValue *= 2;
        //    base.GetHealLife(item, quickHeal, ref healValue);
        //}

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            //Dodge
            if (Main.rand.NextFloat() < dodgeChance)
                Player.NinjaDodge();

            damage = (int)(damage * trueEndurance);

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerEndurance);

            

            base.ModifyHitByNPC(npc, ref damage, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            //Dodge
            if (Main.rand.NextFloat() < dodgeChance)
                Player.NinjaDodge();

            damage = (int)(damage * trueEndurance);

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerEndurance);

            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            InBattle();
            if(ultCharge < ultChargeMax)
                ultCharge = (int)(ultCharge * .94f);

            int hitDir;
            if (npc.position.X < Player.position.X)
                hitDir = -1;
            else
                hitDir = 1;

            if (hasVanguard && !npc.dontTakeDamage)
                Player.ApplyDamageToNPC(npc, (int)(Player.statDefense * vanguardPassiveReflectAmount), 0, hitDir, false);

            if(hasNeterihsToken)
                Player.immuneTime += 60;

            base.OnHitByNPC(npc, damage, crit);
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            InBattle();
            if (ultCharge < ultChargeMax)
                ultCharge = (int)(ultCharge * .94f);

            if (hasNeterihsToken)
                Player.immuneTime += 60;

            base.OnHitByProjectile(proj, damage, crit);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            //if (target.type != NPCID.TargetDummy)
                InBattle();

            if (target.life <= 0)
                enemiesKilled++;

            int damageDealtFinal;
            if (!target.friendly && target.type != NPCID.TargetDummy && !target.SpawnedFromStatue)
            {
                damageDealtFinal = damage;
                if (damageDealtFinal > target.life)
                    damageDealtFinal = target.life;
                damageDealt += damageDealtFinal;

                int dpsDealt = Player.getDPS();
                if (dpsDealt > highestDPS)
                    highestDPS = dpsDealt;

                int critDealt = 0;
                if (crit)
                    critDealt = damage;
                if (critDealt > highestCrit)
                    highestCrit = critDealt;
            }

            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            //if (target.type != NPCID.TargetDummy)
                InBattle();

            if (target.life <= 0)
                enemiesKilled++;

            int damageDealtFinal;
            if (!target.friendly && target.type != NPCID.TargetDummy && !target.SpawnedFromStatue)
            {
                damageDealtFinal = damage;
                if (damageDealtFinal > target.life)
                    damageDealtFinal = target.life;
                damageDealt += damageDealtFinal;

                int dpsDealt = Player.getDPS();
                if (dpsDealt > highestDPS)
                    highestDPS = dpsDealt;

                int critDealt = 0;
                if (crit)
                    critDealt = damage;
                if (critDealt > highestCrit)
                    highestCrit = critDealt;
            }

            if (proj.type == ProjectileType<Projectiles.BloodMage.Transfusion>() && hasAghanims)
            {
                Player.statLife += (int)(damage * .08f);
                Player.HealEffect((int)(damage * .08f));
            }
                

            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //Vanguard Execute
            if (target.boss && target.life <= target.lifeMax * vanguardUltimateBossExecute && proj.type == ProjectileType<Projectiles.Vanguard.VanguardUltimate>())
                damage = target.lifeMax * 3;
            if (!target.boss && target.life <= target.lifeMax / 3 && proj.type == ProjectileType<Projectiles.Vanguard.VanguardUltimate>())
                damage = target.lifeMax * 3;

            if (isUnstableConcoctionReady)
            {
                damage *= 4;
                isUnstableConcoctionReady = false;
            }
                

            if (bloodMageBloodEnchantment)
                damage = (int)(damage * (bloodMageDamageGain + 1f));

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerDamage);

            if (commanderUltActive && !crit)
                crit = true;

            if (hasScout && scoutColaCurDuration > 0)
                damage = (int)(damage * scoutColaDamageBonus);

            if (crit) damage = (int)(damage * critDamageMult);
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (isUnstableConcoctionReady)
            {
                damage *= 4;
                isUnstableConcoctionReady = false;
            }
                
            if (bloodMageBloodEnchantment)
                damage = (int)(damage * (bloodMageDamageGain + 1f));

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerDamage);

            if (commanderUltActive && !crit)
                crit = true;

            if (hasScout && scoutColaCurDuration > 0)
                    damage = (int)(damage * scoutColaDamageBonus);

            if (crit) damage = (int)(damage * critDamageMult);
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void PreUpdateBuffs()
        {
           
            base.PreUpdateBuffs();
        }

        public override void PreUpdate()
        {
            if (Player.statLife <= 0 && !Player.dead && bloodMageBloodEnchantment && hasBloodMage)
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " ran out of blood for their 'Blood Enchantment' ability!. Silly player!"), 1, 1);

            if(healthToRegen > 0)
            {
                Player.statLife++;
                healthToRegen--;
            }

            healthToRegenMediumTimer++;
            if(healthToRegenMediumTimer % 3 == 0)
            {
                if(healthToRegenMedium > 0)
                {
                    Player.statLife++;
                    healthToRegenMedium--;
                }    
            }

            healthToRegenSlowTimer++;
            if (healthToRegenSlowTimer % 6 == 0)
            {
                if (healthToRegenSlow > 0)
                {
                    Player.statLife++;
                    healthToRegenSlow--;
                }
            }

            resetHUD--;
            if (resetHUD <= 0)
                resetHUD = 60;
            if (globalSingleSecondTimer == 0)
                globalSingleSecondTimer = 60;
            globalSingleSecondTimer--;
            
            if (ability1Cooldown > 0)
                ability1Cooldown--;
            if(ability2Cooldown > 0)
                ability2Cooldown--;

            pSecHealthTimer--;

            outOfBattleTimer--;
            if (outOfBattleTimer <= 0)
                outOfBattle2--;
            if(outOfBattle2 <= 0)
            {
                outOfBattle2 = 3;
                if (ultCharge > 0)
                    ultCharge--;
            }

            if (inBattleTimer > 0)
            {
                inBattleTimer--;

                if (devTool)
                    ultCharge += 10;
                else
                    ultCharge++;

                inBattle = true;
            }
            else
            {
                inBattle = false;
            }

            if (inBattle)
            {
                bloodMageInBattle--;
                if(bloodMageInBattle <= 0)
                {
                    bloodMageInBattle = 60;
                    if(bloodMagePassiveCurrentStacks < bloodMagePassiveMaxStacks)
                        bloodMagePassiveCurrentStacks++;
                    //Main.NewText(bloodMagePassiveCurrentStacks);
                }
            }

            if (vanguardShieldCurrentDuration > 0)
            {
                vanguardShieldUp = true;
                vanguardShieldCurrentDuration--;
            }
            else
            {
                vanguardShieldUp = false;
            }

            if (vanguardShieldUp)
            {
                Vector2 origin = Player.Center;
                origin.X -= 4;
                float radius = 50;

                if (!vanguardDustFlag)
                {
                    vanguardDustLocations = 1;
                    vanguardDustFlag = true;
                }

                vanguardDustTimer--;
                if(vanguardDustTimer == 0 && vanguardDustLocations < 20)
                {
                    vanguardDustLocations++;
                    vanguardDustTimer = 5;
                }
                
                for (int i = 0; i < vanguardDustLocations; i++)
                {
                    Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / vanguardDustLocations * i)) * radius;

                    var dust = Dust.NewDustDirect(position, 1, 1, 6, 0f, 0f, 0, Color.LightYellow, 1f);
                    dust.noGravity = true;
                }

                trueEndurance -= vanguardShieldDamageReduction;

                if(vanguardShieldRegen)
                    vanguardShieldRegenTimer++;
                if(vanguardShieldRegenTimer == 60)
                {
                    vanguardShieldRegenTimer = 0;
                    Player.statLife += (int)(Player.statLifeMax2 * .012f);
                    Player.HealEffect((int)(Player.statLifeMax2 * .012f));
                }

            }
            else
            {
                vanguardDustFlag = false;
                vanguardDustTimer = 5;
                vanguardDustLocations = 1;
            }

            if (vanguardTalent_4 == "R" || vanguardTalent_4 == "B")
                vanguardShieldBaseDuration += 30;
            if (vanguardTalent_8 == "R" || vanguardTalent_8 == "B")
                vanguardShieldBaseDuration += 48;

            if (commanderBannerBuffDuration > 0)
                commanderBannerBuffDuration--;

            if (hasScout)
            {
                if (scoutColaCurDuration > 0)
                    scoutColaCurDuration--;

                if (scoutUltCurDuration > 0)
                    scoutUltCurDuration--;
                    
                if (scoutUltInvCurDuration > 0)
                    scoutUltInvCurDuration--;
            }

                base.PreUpdate();
        }

        public override void PostUpdate()
        {
            if (Main.netMode != NetmodeID.Server && equippedClass != "")
            {
                if (GetInstance<ACM2ModSystem>()._HUD.CurrentState == null)
                    GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
            }
            else
                GetInstance<ACM2ModSystem>()._HUD.SetState(null);

            if(resetHUD == 0)
            {
                GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
            }
            base.PostUpdate();
        }

        public override void PostUpdateEquips()
        {
            //globalLevel = defeatedBosses.Count;

            #region Vanguard
            vanguardLevel = vanguardDefeatedBosses.Count;
            if (vanguardLevel > ACMConfigServer.Instance.maxClassLevel)
                vanguardLevel = ACMConfigServer.Instance.maxClassLevel;
            if (hasVanguard)
            {
                if (vanguardTalent_1 == "L" || vanguardTalent_1 == "B")
                    Player.statDefense += 2;
                else if (vanguardTalent_1 == "R" || vanguardTalent_1 == "B")
                    cooldownReduction -= .05f;

                if (vanguardTalent_2 == "L" || vanguardTalent_2 == "B")
                    lifeMult += .015f;
                else if (vanguardTalent_2 == "R" || vanguardTalent_2 == "B")
                    Player.GetDamage(DamageClass.Melee) += .01f;

                if (vanguardTalent_3 == "L" || vanguardTalent_3 == "B")
                    vanguardShieldDamageReduction += .05f;
                else if (vanguardTalent_3 == "R" || vanguardTalent_3 == "B")
                    vanguardSpearBaseDamage += 18;

                if (vanguardTalent_4 == "L" || vanguardTalent_4 == "B")
                    Player.statDefense += 4;

                if (vanguardTalent_5 == "L" || vanguardTalent_5 == "B")
                    lifeMult += .02f;
                else if (vanguardTalent_5 == "R" || vanguardTalent_5 == "B")
                    ultCooldownReduction -= .08f;

                if (vanguardTalent_6 == "R" || vanguardTalent_6 == "B")
                    vanguardPassiveReflectAmount += .5f;

                if (vanguardTalent_7 == "L" || vanguardTalent_7 == "B")
                    Player.GetCritChance(DamageClass.Melee) += 2;
                else if (vanguardTalent_7 == "R" || vanguardTalent_7 == "B")
                    vanguardSpearHeal = true;

                if (vanguardTalent_8 == "L" || vanguardTalent_8 == "B")
                    vanguardSwordBaseDamage += 35;

                if (vanguardTalent_9 == "L" || vanguardTalent_9 == "B")
                    vanguardShieldRegen = true;
                else if (vanguardTalent_9 == "R" || vanguardTalent_9 == "B")
                    cooldownReduction -= .15f;

                if (vanguardTalent_10 == "L" || vanguardTalent_10 == "B")
                    lifeMult += .08f;
                else if (vanguardTalent_10 == "R" || vanguardTalent_10 == "B")
                    vanguardUltimateBossExecute += .04f;
            }
            
            vanguardSpearDamage = vanguardSpearBaseDamage + 8 * vanguardLevel;
            vanguardShieldDuration = vanguardShieldBaseDuration + 20 * vanguardLevel;
            vanguardSwordDamage = vanguardSwordBaseDamage + 14 * vanguardLevel;
            #endregion

            #region Blood Mage
            bloodMageLevel = bloodMageDefeatedBosses.Count;
            if (bloodMageLevel > ACMConfigServer.Instance.maxClassLevel)
                bloodMageLevel = ACMConfigServer.Instance.maxClassLevel;
            if (hasBloodMage)
            {
                if (bloodMageTalent_1 == "R" || bloodMageTalent_1 == "B")
                    bloodMageSiphonHealMax += .05f;
                if (bloodMageTalent_1 == "L" || bloodMageTalent_1 == "B")
                    cooldownReduction -= .05f;

                if (bloodMageTalent_2 == "L" || bloodMageTalent_2 == "B")
                    bloodMageBaseDamageGain += .03f;
                else if (bloodMageTalent_2 == "R" || bloodMageTalent_2 == "B") 
                    bloodMageBaseHealthDrain -= .02f;

                if (bloodMageTalent_3 == "L" || bloodMageTalent_3 == "B")
                    lifeMult += .05f;
                else if (bloodMageTalent_3 == "R" || bloodMageTalent_3 == "B")
                    cooldownReduction -= .1f;

                if (bloodMageTalent_4 == "L" || bloodMageTalent_4 == "B")
                    ultCooldownReduction -= .12f;
                else if (bloodMageTalent_4 == "R" || bloodMageTalent_4 == "B")
                    bloodMagePassiveRegen += .0002f;

                if (bloodMageTalent_5 == "L" || bloodMageTalent_5 == "B")
                    bloodMageSiphonBaseDamage += 35;
                else if (bloodMageTalent_5 == "R" || bloodMageTalent_5 == "B")
                    ultCooldownReduction -= .05f;

                if (bloodMageTalent_6 == "L" || bloodMageTalent_6 == "B")
                    manaMult += .18f;
                else if (bloodMageTalent_6 == "R" || bloodMageTalent_6 == "B")
                    Player.GetCritChance(DamageClass.Magic) += 2;

                if (bloodMageTalent_7 == "L" || bloodMageTalent_7 == "B")
                    Player.manaCost -= .15f;
                else if (bloodMageTalent_7 == "R" || bloodMageTalent_7 == "B")
                    bloodMageBaseDamageGain += .04f;

                if (bloodMageTalent_8 == "L" || bloodMageTalent_8 == "B")
                    bloodMageBaseHealthDrain -= .01f;
                else if (bloodMageTalent_8 == "R" || bloodMageTalent_8 == "B")
                    ultCooldownReduction -= .05f;

                if (bloodMageTalent_9 == "L" || bloodMageTalent_9 == "B")
                    Player.GetDamage(DamageClass.Magic) += bloodMagePassiveCurrentStacks * .0008f;
                else if (bloodMageTalent_9 == "R" || bloodMageTalent_9 == "B")
                    bloodMageBaseUltRegen += .005f;

                if (bloodMageTalent_10 == "L" || bloodMageTalent_10 == "B")
                    cooldownReduction -= .12f;
                else if (bloodMageTalent_10 == "R" || bloodMageTalent_10 == "B")
                    bloodMageUltTicks += 3;

                pSecHealthRegen += bloodMagePassiveCurrentStacks * bloodMagePassiveRegen;
                bloodMageUltRegen = bloodMageBaseUltRegen + .001f * bloodMageLevel;
                //bloodMageHealthDrain += bloodMageBaseHealthDrain;
                bloodMageSiphonDamage = bloodMageSiphonBaseDamage + 4 * bloodMageLevel;
                bloodMageDamageGain += .01f * bloodMageLevel;
                //bloodMagePassiveMaxStacks += 2 * bloodMageLevel;

                if (bloodMageBloodEnchantment)
                {
                    if (globalSingleSecondTimer == 0)
                        Player.statLife -= (int)(Player.statLifeMax2 * bloodMageBaseHealthDrain);
                }
            }
            #endregion

            #region Commander
            commanderLevel = commanderDefeatedBosses.Count;
            if (commanderLevel > ACMConfigServer.Instance.maxClassLevel)
                commanderLevel = ACMConfigServer.Instance.maxClassLevel;
            commanderUltDuration += 15 * commanderLevel;
            commanderCryDamage += commanderCryDamageLevel * commanderLevel;

            if (hasCommander)
            {
                if (commanderTalent_1 == "L" || commanderTalent_1 == "B")
                    commanderBannerDamage += .03f;
                else if (commanderTalent_1 == "R" || commanderTalent_1 == "B")
                    Player.whipRangeMultiplier += .05f;

                if (commanderTalent_2 == "L" || commanderTalent_2 == "B")
                    commanderBannerRange = (int)(commanderBannerRange * 1.4f);
                else if (commanderTalent_2 == "R" || commanderTalent_2 == "B")
                    cooldownReduction -= .15f;

                if (commanderTalent_3 == "L" || commanderTalent_3 == "B")
                    commanderPassiveEndurance += .0025f;
                else if (commanderTalent_3 == "R" || commanderTalent_3 == "B")
                    ultCooldownReduction -= .1f;

                if (commanderTalent_4 == "L" || commanderTalent_4 == "B")
                    commanderBannerDamage += .04f;
                else if (commanderTalent_4 == "R" || commanderTalent_4 == "B")
                    commanderCryRange = (int)(commanderCryRange * 1.25f);

                if (commanderTalent_5 == "L" || commanderTalent_5 == "B")
                    Player.maxMinions += 1;
                else if (commanderTalent_5 == "R" || commanderTalent_5 == "B")
                    commanderBannerEndurance -= .05f;

                if (commanderTalent_6 == "L" || commanderTalent_6 == "B")
                    commanderCryDuration += 120;
                else if (commanderTalent_6 == "R" || commanderTalent_6 == "B")
                    commanderBannerPersist += 120;

                if (commanderTalent_7 == "L" || commanderTalent_7 == "B")
                    commanderUltDuration += 60;
                else if (commanderTalent_7 == "R" || commanderTalent_7 == "B")
                    commanderCryDuration += 60;

                if (commanderTalent_8 == "L" || commanderTalent_8 == "B")
                    commanderCryBonusDamage += .05f;
                else if (commanderTalent_8 == "R" || commanderTalent_8 == "B")
                    Player.whipRangeMultiplier += .1f;

                if (commanderTalent_9 == "L" || commanderTalent_9 == "B")
                    ultCooldownReduction -= .1f;
                else if (commanderTalent_9 == "R" || commanderTalent_9 == "B")
                    commanderPassiveEndurance += .0025f;

                if (commanderTalent_10 == "L" || commanderTalent_10 == "B")
                    Player.whipRangeMultiplier += .05f;
                else if (commanderTalent_10 == "R" || commanderTalent_10 == "B")
                    commanderBannerEndurance -= .2f;

                trueEndurance += Player.maxMinions * commanderPassiveEndurance;
                commanderBannerDuration += 24 * commanderLevel;
            }
            #endregion

            #region Scout
            scoutLevel = scoutDefeatedBosses.Count;
            if (scoutLevel > ACMConfigServer.Instance.maxClassLevel)
                scoutLevel = ACMConfigServer.Instance.maxClassLevel;

            if (hasScout)
            {
                if (scoutTalent_1 == "L" || scoutTalent_1 == "B")
                    scoutPassiveSpeedBonus += .05f;
                else if (scoutTalent_1 == "R" || scoutTalent_1 == "B")
                    scoutTrapDamage += 25;

                if (scoutTalent_2 == "L" || scoutTalent_2 == "B")
                    Player.GetCritChance(DamageClass.Ranged) += 1;

                if (scoutTalent_3 == "L" || scoutTalent_3 == "B")
                    scoutTrapRange += 25;
                else if (scoutTalent_3 == "R" || scoutTalent_3 == "B")
                    cooldownReduction -= .16f;

                if (scoutTalent_4 == "L" || scoutTalent_4 == "B")
                    scoutColaDamageBonus += .04f;
                else if (scoutTalent_4 == "R" || scoutTalent_4 == "B")
                    scoutTrapChargeRate += 2;

                if (scoutTalent_5 == "L" || scoutTalent_5 == "B")
                    defenseMult += .05f;
                else if (scoutTalent_5 == "R" || scoutTalent_5 == "B")
                    Player.GetCritChance(DamageClass.Ranged) += 1;

                if (scoutTalent_6 == "L" || scoutTalent_6 == "B")
                    scoutColaDuration += 120;
                else if (scoutTalent_6 == "R" || scoutTalent_6 == "B")
                    ability2MaxCooldown -= 3;

                if (scoutTalent_7 == "L" || scoutTalent_7 == "B")
                    scoutPassiveSpeedBonus += .06f;
                else if (scoutTalent_7 == "R" || scoutTalent_7 == "B")
                    scoutColaDamageBonus += .06f;

                if (scoutTalent_8 == "L" || scoutTalent_8 == "B")
                    scoutTrapRange += 20;
                else if (scoutTalent_8 == "R" || scoutTalent_8 == "B")
                    ultCooldownReduction -= .1f;

                if (scoutTalent_9 == "L" || scoutTalent_9 == "B")
                    Player.GetDamage(DamageClass.Ranged) += .03f;
                else if (scoutTalent_9 == "R" || scoutTalent_9 == "B")
                    scoutTrapDamage += 120;

                if (scoutTalent_10 == "L" || scoutTalent_10 == "B")
                    scoutTrapRange = (int)(scoutTrapRange * 1.45f);
                else if (scoutTalent_10 == "R" || scoutTalent_10 == "B")
                    scoutUltInvDuration += 60;
            }

            scoutTrapDamage += scoutTrapDamageLevel * scoutLevel;
            scoutUltSpeed += scoutUltSpeedLevel * scoutLevel;

            if (scoutUltCurDuration > 0)
            {
                Player.moveSpeed += scoutUltSpeed;
                Player.autoJump = true;
                Player.jumpSpeedBoost += 3f;
                Player.armorEffectDrawShadow = true;
            }

            if (scoutUltInvCurDuration > 0)
            {
                Player.immune = true;
            }
            #endregion

            if (ability1Cooldown == 0 && !a1Sound)
            {
                a1Sound = true;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }

            if (ability2Cooldown == 0 && !a2Sound)
            {
                a2Sound = true;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }

            if (ultCharge == ultChargeMax && !ultSound)
            {
                ultSound = true;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }

            ultChargeMax = (int)(ultChargeMax * ultCooldownReduction);
            Player.statLifeMax2 = (int)(Player.statLifeMax2 * lifeMult);
            Player.statDefense = (int)(Player.statDefense * defenseMult);
            Player.statManaMax2 = (int)(Player.statManaMax2 * manaMult);
            if (ultCharge > ultChargeMax)
                ultCharge = ultChargeMax;

            if (pSecHealthTimer == 0)
            {
                if (!inBattle && bloodMagePassiveCurrentStacks > 0 && outOfBattleTimer <= 0)
                    bloodMagePassiveCurrentStacks--;

                if (Player.active)
                    Player.statLife += (int)(Player.statLifeMax2 * pSecHealthRegen);

                if (bloodMageCurUltTicks > 0)
                {
                    bloodMageCurUltTicks--;
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 255; i++)
                        {
                            if (Main.player[i].active)
                            {
                                ModPacket packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerMedium);
                                packet.Write((byte)i);
                                packet.Write((int)(Main.player[i].statLifeMax2 * Main.player[Player.whoAmI].GetModPlayer<ACMPlayer>().bloodMageUltRegen));
                                packet.Send(-1, -1);
                            }
                        }
                    }
                    else
                    {
                        healthToRegenMedium += (int)(Player.statLifeMax2 * Player.GetModPlayer<ACMPlayer>().bloodMageUltRegen);
                        Player.HealEffect((int)(Player.statLifeMax2 * Player.GetModPlayer<ACMPlayer>().bloodMageUltRegen));
                    }
                    
                }
                pSecHealthTimer = 60;
            }

            if (cooldownReduction < 0f)
                cooldownReduction = 0f;

            if (hasScout && scoutColaCurDuration > 0 && hasAghanims)
                Player.GetCritChance(DamageClass.Ranged) += 15;

            base.PostUpdateEquips();
        }

        public override void OnRespawn(Player player)
        {
            GetInstance<ACM2ModSystem>()._HUD.SetState(null);
            GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
            base.OnRespawn(player);
        }

        public override void UpdateLifeRegen()
        {
            if(bloodMageBloodEnchantment)
                Player.lifeRegenTime = 0;
            if (bloodMageBloodEnchantment && Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
                Player.lifeRegen -= 2;
            }

            base.UpdateLifeRegen();
        }

        public override void UpdateBadLifeRegen()
        {
            if (bloodMageBloodEnchantment)
                Player.lifeRegenTime = 0;
            if (bloodMageBloodEnchantment && Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                    Player.lifeRegen -= 2;
                }

            base.UpdateBadLifeRegen();
        }

        public override void OnEnterWorld(Player player)
        {
            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(new UI.ClassesMenu());
            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
            base.OnEnterWorld(player);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.myPlayer == Player.whoAmI && !Player.dead)
            {
                if(ACM2.Menu.JustReleased)
                {
                    if (GetInstance<ACM2ModSystem>()._ClassesMenu.CurrentState == null)
                    {
                        GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(new UI.ClassesMenu());
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                    }
                    else
                    {
                        GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
                        SoundEngine.PlaySound(SoundID.MenuClose);
                    }
                }

                if (ACM2.ClassAbility1.JustReleased && ability1Cooldown <= 0)
                {
                    if (hasUnstableConcoction)
                        if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;

                    if(hasBleedingMoonStone && Player.statLife < Player.statLifeMax2)
                    {
                        int heal = (int)(Player.statLifeMax2 * .03f);
                        Player.statLife += heal;
                        Player.HealEffect(heal);
                    }

                    Vector2 PointToCursor = Main.MouseWorld - Player.position;
                    switch (equippedClass)
                    {
                        case "Vanguard":
                            
                            PointToCursor.Normalize();
                            PointToCursor *= 12f;

                            if (vanguardSpearHeal && Player.statLife < Player.statLifeMax2)
                            {
                                Player.statLife += (int)(Player.statLifeMax2 * .024f);
                                Player.HealEffect((int)(Player.statLifeMax2 * .024f));
                            }


                            SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot);
                            Projectile.NewProjectile(default, new Vector2(Player.position.X + Player.width / 2, Player.position.Y), new Vector2(PointToCursor.X, PointToCursor.Y), ProjectileType<Projectiles.Vanguard.VanguardSpear>(), (int)(vanguardSpearBaseDamage * abilityPower), 0, Player.whoAmI);

                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Spear Of Light!", true);
                            break;

                        case "Blood Mage":
                            PointToCursor.Normalize();
                            PointToCursor *= 18f;

                            SoundEngine.PlaySound(SoundID.Item21);
                            Projectile.NewProjectile(default, new Vector2(Player.position.X + Player.width / 2, Player.position.Y), new Vector2(PointToCursor.X, PointToCursor.Y), ProjectileType<Projectiles.BloodMage.Transfusion>(), (int)(bloodMageSiphonDamage * abilityPower), 0, Player.whoAmI);

                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Transfusion!", true);
                            break;

                        case "Commander":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            Projectile.NewProjectile(default, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Commander.WarBanner>(), 0, 0, Player.whoAmI);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "War Banner!", true);
                            break;

                        case "Scout":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Hit-a-Soda!", true);
                            scoutColaCurDuration = scoutColaDuration;

                            SoundEngine.PlaySound(SoundID.Item3, Player.position);
                            break;
                    }
                    a1Sound = false;
                }

                if (ACM2.ClassAbility2.JustReleased && ability2Cooldown <= 0)
                {
                    if (hasUnstableConcoction)
                        if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;

                    if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2)
                    {
                        int heal = (int)(Player.statLifeMax2 * .03f);
                        Player.statLife += heal;
                        Player.HealEffect(heal);
                    }

                    switch (equippedClass)
                    {
                        case "Vanguard":
                            vanguardShieldCurrentDuration = (int)(vanguardShieldDuration * abilityDuration);
                            SoundEngine.PlaySound(SoundID.Mech);

                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Light Barrier!", true);
                            break;

                        case "Blood Mage":
                            if (bloodMageBloodEnchantment)
                            {
                                bloodMageBloodEnchantment = false;
                            }
                            else
                            {
                                bloodMageBloodEnchantment = true;
                                Projectile.NewProjectile(default, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment>(), 0, 0, Player.whoAmI);
                                Projectile.NewProjectile(default, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment2>(), 0, 0, Player.whoAmI);
                                Projectile.NewProjectile(default, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment3>(), 0, 0, Player.whoAmI);
                            }
                                
                            if (!bloodMageBloodEnchantment)
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Blood Enchantment: Off!", true);
                            else
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Blood Enchantment: On!", true);
                            break;

                        case "Commander":
                            Projectile.NewProjectile(default, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Commander.BattleCry>(), 0, 0, Player.whoAmI);
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Battle Cry!", true);
                            break;

                        case "Scout":
                            if (Collision.CanHitLine(Player.Center, 1, 1, Main.MouseWorld, 1, 1))
                            {
                                AddAbilityCooldown(2, ability2MaxCooldown);
                                Projectile.NewProjectile(default, Main.MouseWorld, Vector2.Zero, ProjectileType<Projectiles.Scout.ScoutTrap>(), 0, 0, Player.whoAmI);
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Explosive Trap!", true);
                                SoundEngine.PlaySound(SoundID.Item37, Player.position);
                            }
                            break;
                    }
                    a2Sound = false;
                }

                if (ACM2.ClassAbilityUltimate.JustReleased)
                {
                    if(ultCharge >= ultChargeMax)
                    {   
                        ultCharge -= ultChargeMax;

                        if (hasUnstableConcoction)
                            if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;

                        if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2)
                        {
                            int heal = (int)(Player.statLifeMax2 * .05f);
                            Player.statLife += heal;
                            Player.HealEffect(heal);
                        }

                        switch (equippedClass)
                        {
                            case "Vanguard":
                                Projectile.NewProjectile(default, new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 1000), new Vector2(0f, 50f), ProjectileType<Projectiles.Vanguard.VanguardUltimate>(), (int)(vanguardSwordDamage * abilityPower), 0, Player.whoAmI);
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Sword Of Judgement!", true);
                                //NetworkText text;
                                //text = NetworkText.FromKey(player.name + " was healed for " + healAmount + " health", acmPlayer.player.name);
                                //NetMessage.(text, new Color(25, 225, 25));
                                break;

                            case "Blood Mage":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Regeneration!", true);
                                SoundEngine.PlaySound(SoundID.Item4, Player.position);
                                bloodMageCurUltTicks = bloodMageUltTicks;
                                break;

                            case "Commander":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Inspire!", true);
                                SoundEngine.PlaySound(SoundID.Thunder, Player.position);
                                for (int i = 0; i < 255; i++)
                                    if (Main.player[i].active)
                                        Main.player[i].AddBuff(ModContent.BuffType<Buffs.Commander.Inspire>(), commanderUltDuration);
                                break;

                            case "Scout":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Atomic-Slap (TM)!", true);
                                SoundEngine.PlaySound(SoundID.Item3, Player.position);

                                scoutUltCurDuration = scoutUltDuration;
                                scoutUltInvCurDuration = scoutUltInvDuration;
                                break;
                        }
                        ultSound = false;
                    }
                }
            }
            base.ProcessTriggers(triggersSet);
        }

        public override void SaveData(TagCompound tag)
        {
            //tag["defeatedBosses"] = defeatedBosses;
            tag.Add("enemiesKilled", enemiesKilled);
            tag.Add("damageDealt", damageDealt);
            tag.Add("timesDied", timesDied);
            tag.Add("highestDPS", highestDPS);
            tag.Add("highestCrit", highestCrit);

            #region Vanguard Reworked
            tag.Add("vanguardDefeatedBosses", vanguardDefeatedBosses);
            tag.Add("vanguardTalentPoints", vanguardTalentPoints);
            tag.Add("vanguardSpentTalentPoints", vanguardSpentTalentPoints);

            tag.Add("vanguardTalent_1", vanguardTalent_1);
            tag.Add("vanguardTalent_2", vanguardTalent_2);
            tag.Add("vanguardTalent_3", vanguardTalent_3);
            tag.Add("vanguardTalent_4", vanguardTalent_4);
            tag.Add("vanguardTalent_5", vanguardTalent_5);
            tag.Add("vanguardTalent_6", vanguardTalent_6);
            tag.Add("vanguardTalent_7", vanguardTalent_7);
            tag.Add("vanguardTalent_8", vanguardTalent_8);
            tag.Add("vanguardTalent_9", vanguardTalent_9);
            tag.Add("vanguardTalent_10", vanguardTalent_10);
            #endregion

            #region Blood Mage Reworked
            tag.Add("bloodMageDefeatedBosses", bloodMageDefeatedBosses);
            tag.Add("bloodMageTalentPoints", bloodMageTalentPoints);
            tag.Add("bloodMageSpentTalentPoints", bloodMageSpentTalentPoints);

            tag.Add("bloodMageTalent_1", bloodMageTalent_1);
            tag.Add("bloodMageTalent_2", bloodMageTalent_2);
            tag.Add("bloodMageTalent_3", bloodMageTalent_3);
            tag.Add("bloodMageTalent_4", bloodMageTalent_4);
            tag.Add("bloodMageTalent_5", bloodMageTalent_5);
            tag.Add("bloodMageTalent_6", bloodMageTalent_6);
            tag.Add("bloodMageTalent_7", bloodMageTalent_7);
            tag.Add("bloodMageTalent_8", bloodMageTalent_8);
            tag.Add("bloodMageTalent_9", bloodMageTalent_9);
            tag.Add("bloodMageTalent_10", bloodMageTalent_10);
            #endregion

            #region Commander Reworked
            tag.Add("commanderDefeatedBosses", commanderDefeatedBosses);
            tag.Add("commanderTalentPoints", commanderTalentPoints);
            tag.Add("commanderSpentTalentPoints", commanderSpentTalentPoints);

            tag.Add("commanderTalent_1", commanderTalent_1);
            tag.Add("commanderTalent_2", commanderTalent_2);
            tag.Add("commanderTalent_3", commanderTalent_3);
            tag.Add("commanderTalent_4", commanderTalent_4);
            tag.Add("commanderTalent_5", commanderTalent_5);
            tag.Add("commanderTalent_6", commanderTalent_6);
            tag.Add("commanderTalent_7", commanderTalent_7);
            tag.Add("commanderTalent_8", commanderTalent_8);
            tag.Add("commanderTalent_9", commanderTalent_9);
            tag.Add("commanderTalent_10", commanderTalent_10);
            #endregion

            #region Scout Reworked
            tag.Add("scoutDefeatedBosses", scoutDefeatedBosses);
            tag.Add("scoutTalentPoints", scoutTalentPoints);
            tag.Add("scoutSpentTalentPoints", scoutSpentTalentPoints);
            
            tag.Add("scoutCanDoubleJump", scoutCanDoubleJump);
            
            tag.Add("scoutTalent_1", scoutTalent_1);
            tag.Add("scoutTalent_2", scoutTalent_2);
            tag.Add("scoutTalent_3", scoutTalent_3);
            tag.Add("scoutTalent_4", scoutTalent_4);
            tag.Add("scoutTalent_5", scoutTalent_5);
            tag.Add("scoutTalent_6", scoutTalent_6);
            tag.Add("scoutTalent_7", scoutTalent_7);
            tag.Add("scoutTalent_8", scoutTalent_8);
            tag.Add("scoutTalent_9", scoutTalent_9);
            tag.Add("scoutTalent_10", scoutTalent_10);
            #endregion
            base.SaveData(tag);
        }   

        public override void LoadData(TagCompound tag)
        {
            //defeatedBosses.Clear();
            //defeatedBosses.AddRange(tag.GetList<string>("defeatedBosses"));
            enemiesKilled = tag.GetInt("enemiesKilled");
            damageDealt = tag.GetInt("damageDealt");
            timesDied = tag.GetInt("timesDied");
            highestDPS = tag.GetInt("highestDPS");
            highestCrit = tag.GetInt("highestCrit");

            #region Vanguard Reworked
            vanguardDefeatedBosses.AddRange(tag.GetList<string>("vanguardDefeatedBosses"));
            vanguardTalentPoints = tag.GetInt("vanguardTalentPoints");
            vanguardSpentTalentPoints = tag.GetInt("vanguardSpentTalentPoints");

            vanguardTalent_1 = tag.GetString("vanguardTalent_1");
            vanguardTalent_2 = tag.GetString("vanguardTalent_2");
            vanguardTalent_3 = tag.GetString("vanguardTalent_3");
            vanguardTalent_4 = tag.GetString("vanguardTalent_4");
            vanguardTalent_5 = tag.GetString("vanguardTalent_5");
            vanguardTalent_6 = tag.GetString("vanguardTalent_6");
            vanguardTalent_7 = tag.GetString("vanguardTalent_7");
            vanguardTalent_8 = tag.GetString("vanguardTalent_8");
            vanguardTalent_9 = tag.GetString("vanguardTalent_9");
            vanguardTalent_10 = tag.GetString("vanguardTalent_10");
            #endregion

            #region Blood Mage Reworked
            bloodMageDefeatedBosses.AddRange(tag.GetList<string>("bloodMageDefeatedBosses"));
            bloodMageTalentPoints = tag.GetInt("bloodMageTalentPoints");
            bloodMageSpentTalentPoints = tag.GetInt("bloodMageSpentTalentPoints");

            bloodMageTalent_1 = tag.GetString("bloodMageTalent_1");
            bloodMageTalent_2 = tag.GetString("bloodMageTalent_2");
            bloodMageTalent_3 = tag.GetString("bloodMageTalent_3");
            bloodMageTalent_4 = tag.GetString("bloodMageTalent_4");
            bloodMageTalent_5 = tag.GetString("bloodMageTalent_5");
            bloodMageTalent_6 = tag.GetString("bloodMageTalent_6");
            bloodMageTalent_7 = tag.GetString("bloodMageTalent_7");
            bloodMageTalent_8 = tag.GetString("bloodMageTalent_8");
            bloodMageTalent_9 = tag.GetString("bloodMageTalent_9");
            bloodMageTalent_10 = tag.GetString("bloodMageTalent_10");
            #endregion

            #region Commander Reworked
            commanderDefeatedBosses.AddRange(tag.GetList<string>("commanderDefeatedBosses"));
            commanderTalentPoints = tag.GetInt("commanderTalentPoints");
            commanderSpentTalentPoints = tag.GetInt("commanderSpentTalentPoints");

            commanderTalent_1 = tag.GetString("commanderTalent_1");
            commanderTalent_2 = tag.GetString("commanderTalent_2");
            commanderTalent_3 = tag.GetString("commanderTalent_3");
            commanderTalent_4 = tag.GetString("commanderTalent_4");
            commanderTalent_5 = tag.GetString("commanderTalent_5");
            commanderTalent_6 = tag.GetString("commanderTalent_6");
            commanderTalent_7 = tag.GetString("commanderTalent_7");
            commanderTalent_8 = tag.GetString("commanderTalent_8");
            commanderTalent_9 = tag.GetString("commanderTalent_9");
            commanderTalent_10 = tag.GetString("commanderTalent_10");
            #endregion

            #region Scout Reworked
            scoutDefeatedBosses.AddRange(tag.GetList<string>("scoutDefeatedBosses"));
            scoutTalentPoints = tag.GetInt("scoutTalentPoints");
            scoutSpentTalentPoints = tag.GetInt("scoutSpentTalentPoints");
            
            scoutCanDoubleJump = tag.GetBool("scoutCanDoubleJump");
            
            scoutTalent_1 = tag.GetString("scoutTalent_1");
            scoutTalent_2 = tag.GetString("scoutTalent_2");
            scoutTalent_3 = tag.GetString("scoutTalent_3");
            scoutTalent_4 = tag.GetString("scoutTalent_4");
            scoutTalent_5 = tag.GetString("scoutTalent_5");
            scoutTalent_6 = tag.GetString("scoutTalent_6");
            scoutTalent_7 = tag.GetString("scoutTalent_7");
            scoutTalent_8 = tag.GetString("scoutTalent_8");
            scoutTalent_9 = tag.GetString("scoutTalent_9");
            scoutTalent_10 = tag.GetString("scoutTalent_10");
            #endregion
            base.LoadData(tag);
        }

        /// <summary>
        /// Add ability cooldowns. 'ability' corresponds to ability number (1/2).
        /// </summary>
        void AddAbilityCooldown(int ability, int timeInSeconds)
        {
            if (ability == 1)
                ability1Cooldown = (int)(60 * timeInSeconds * cooldownReduction);
            if (ability == 2)
                ability2Cooldown = (int)(60 * timeInSeconds * cooldownReduction);
        }

        /// <summary>
        /// Sets the Player in combat for 3s, increasing ult charge.
        /// </summary>
        public void InBattle()
        {
            inBattleTimer = outOfBattleTimeMax;
            outOfBattleTimer = inBattleTimeMax;
        }
    }
}