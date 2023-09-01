using ApacchiisClassesMod2.Configs;
using ApacchiisClassesMod2.Items.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using static ApacchiisClassesMod2.ACM2;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace ApacchiisClassesMod2
{
	public class ACMPlayer : ModPlayer
	{
        public List<int> relicList = new List<int>()
        {
        };
        bool updatedRelicList = false;

        public bool compactHUD;
        public bool blinkingHUD;

        int resetHUD = 600;
        public bool levelUpText = false;
        public string P_Name;
        public string P_Desc;
        public string P_Effect_1;
        public string P_Effect_2;
        public string P_Effect_3;
        public string P_Effect_4;

        public string A1_Name;
        public string A1_Desc;
        public string A1_Effect_1;
        public string A1_Effect_2;
        public string A1_Effect_3;
        public string A1_Effect_4;

        public string A2_Name;
        public string A2_Desc;
        public string A2_Effect_1;
        public string A2_Effect_2;
        public string A2_Effect_3;
        public string A2_Effect_4;

        public string Ult_Name;
        public string Ult_Desc;
        public string Ult_Effect_1;
        public string Ult_Effect_2;
        public string Ult_Effect_3;
        public string Ult_Effect_4;

        #region Cards
        public int cardsPoints = 0;

        // Basic
        public int card_CarryCount; //dmg
        public float card_CarryValue;
        public int card_HealthyCount; //+hp
        public float card_HealthyValue;
        public int card_PowerfulCount; //+ap
        public float card_PowerfulValue;
        public int card_MendingCount; //+heal
        public float card_MendingValue;
        public int card_TimelessCount; //+cdr
        public float card_TimelessValue;
        public int card_MightyCount; //+ucdr
        public float card_MightyValue;
        public int card_SneakyCount; //+dodge
        public float card_SneakyValue;
        public int card_NimbleHandsCount; //+as
        public float card_NimbleHandsValue;
        public int card_ImpenetrableCount; //+def
        public float card_ImpenetrableValue;
        public int card_MagicalCount; //+mana
        public float card_MagicalValue;
        public int card_DeadeyeCount; //+cdmg
        public float card_DeadeyeValue;


        // Complex
        public int card_FortifiedCount; //+hp +def
        public float card_FortifiedValue_1;
        public float card_FortifiedValue_2;
        public int card_MasterfulCount; //+ap +heal
        public float card_MasterfulValue_1;
        public float card_MasterfulValue_2;
        public int card_SparkOfGeniusCount; //+cdr +ucdr
        public float card_SparkOfGeniusValue_1;
        public float card_SparkOfGeniusValue_2;
        public int card_ProwlerCount; //+ms +jump
        public float card_ProwlerValue_1;
        public float card_ProwlerValue_2;
        public int card_VeteranCount; //+hp +mana
        public float card_VeteranValue_1;
        public float card_VeteranValue_2;
        public int card_HealerCount; //+cdr +heal
        public float card_HealerValue_1;
        public float card_HealerValue_2;
        public int card_MischievousCount; //+cdmg +dodge
        public float card_MischievousValue_1;
        public float card_MischievousValue_2;
        public int card_SeerCount; //+ap +ucdr
        public float card_SeerValue_1;
        public float card_SeerValue_2;
        public int card_FerociousCount; //+dmg +as
        public float card_FerociousValue_1;
        public float card_FerociousValue_2;
        #endregion

        bool gotFreeRelic = false;

        #region Relics
        public bool hasBleedingMoonStone;
        public bool hasAghanims;
        public bool hasAghanimsShard;
        public bool hasUnstableConcoction;
        bool isUnstableConcoctionReady;
        public bool hasNeterihsToken;
        public bool hasSqueaker;
        public bool hasOldShield;
        public bool hasFlanPudding;
        int flanPuddingTimer = 120;
        public bool hasArcaneBlade;
        int arcaneBladeTimer = 30;
        public bool hasManaBag;
        public bool hasDarkSign;
        public bool hasEldenRing;
        public bool hasPocketSlime;
        public bool hasChocolateBar;
        int chocolateBarTimer = -1; // Start <0 so it doesnt instanly proc when joining a world
        int chocolateBarStoredHealth;
        public bool hasaccountantRat;
        int accountantRatDamageAccumulated = 0;
        int accountantRatTicks = 0;
        int accountantRatTimer = 30;
        public bool hasChaosAccelerant;
        public bool hasTearsOfLife;
        public bool hasOldBlood;
        public bool hasLuckyLeaf;
        public bool hasScalingWarbanner;
        public bool hasPeanut;
        public bool hasCactusRing;
        public bool hasPorcelainMask;
        public bool hasBrokenHeart;
        public bool hasBloodGem;
        int bloodGemProjectileTimer = 0;
        int bloodGemMeleeTimer = 0;
        public bool hasLeysMushroom;
        float leysMushroomBuffChance = .14f;
        float leysMushroomHealChance = .03f;
        float leysMushroomHeal = .03f;
        public bool hasStrangeMushroom;
        public bool hasMushroomConcentrate;
        public bool hasBerserkersBrew;
        public bool hasNessie;
        int nessieCooldown = -1;
        int nessieBaseCooldown;
        public bool hasMajorsCare;
        float majorsCareTimer;
        public bool hasWindsRoar;
        float _windsRoarCooldown;
        #endregion

        string[] nessieProcText =
{
            "Nessie!",
            "Little Nessie!",
            "Try as you might, you cant kill me, son!",
            "So cute!",
            "Squishy!",
            "*Squish*",
            "Ol' Nessie!",
            "^-^"
        };

        public string[] classSpecsText = {"a", "b"};

        string[] accountantRatDeathText =
        {
            "'s accountant rat has forgot how to do basic maths!",
            "'s accountant rat quit their job!",
            "'s accountant rat has overdosed on math!",
            "'s accountant rat had a stroke trying to do math!",
            "'s accountant rat has given up hope!",
            "'s accountant rat has failed at doing their job!",
            "'s accountant rat had a 'bruh moment'!",
            "'s accountant rat has fell asleep!",
            "'s accountant rat got distracted by some cheese!"
        };

        public string equippedClass;
        public bool hasClass = false;
        public int spentSkillPointsGlobal;
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
        public float pSecHealthRegen = 0;
        public float classStatMultiplier = 1f;
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
        public int healthToRegenSnail;
        int healthToRegenSnailTimer = 0;
        public int healthToRegenSecond;
        int healthToRegenSecondTimer = 0;

        public bool devTool = false;

        public int ability1Cooldown;
        public int ability1MaxCooldown;
        public int ability2Cooldown;
        public int ability2MaxCooldown;
        public int ability3Cooldown;

        public float cooldownReduction = 1f;
        public float ability1cdr = 1f;
        public float ability2cdr = 1f;
        public float ultCooldownReduction = 1f;
        public float abilityPower = 1f;
        public float healingPower = 1f;
        public float abilityDuration = 1f;
        //public float attackSpeed = 1f;
        //public float trueEndurance = 1f;
        public float dodgeChance = 0f;
        public float critDamageMult = 1f;
        public float minionCritChance;

        #region Player Stats
        public int enemiesKilled;
        public int damageDealt;
        public int timesDied;
        public int highestDPS;
        public int highestCrit;
        bool canAddDeaths = true;
        public int totalDamageTaken;
        #endregion

        #region Vanguard
        public bool hasVanguard = false;
        public int vanguardLevel = 0;
        public int vanguardSkillPoints = 0;
        public int vanguardSpentSkillPoints = 0;

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

        public int specVanguard_Defense;
        public float specVanguard_DefenseBase;
        public int specVanguard_ShieldDamageReduction;
        public float specVanguard_ShieldDamageReductionBase;
        public int specVanguard_MeleeDamage;
        public float specVanguard_MeleeDamageBase;
        public int specVanguard_UltCost;
        public float specVanguard_UltCostBase;
        public int specVanguard_SpearDamage;
        public int specVanguard_SpearDamageBase;

        bool vanguardShieldUp = false;
        bool vanguardShieldRegen = false;
        int vanguardShieldRegenTimer = 0;

        public float vanguardPassiveReflectAmount = 1f;

        public float vanguardShieldBaseDamageReduction;
        public float vanguardShieldDamageReduction;
        public int vanguardShieldBaseDuration;
        public int vanguardShieldDuration;
        public int vanguardShieldDurationPerLevel;
        int vanguardShieldCurrentDuration = 0;
        int vanguardDustLocations = 20;
        bool vanguardDustFlag = false;
        int vanguardDustTimer = 5;

        public int vanguardSpearBaseDamage;
        public int vanguardSpearDamage;
        bool vanguardSpearHeal = false;

        public float vanguardUltimateBossExecute;
        public int vanguardSwordBaseDamage;
        public int vanguardSwordDamage;

        public int talentSinkVanguardLeft;
        public float talentSinkVanguardLeftValue = .002f; //Health
        public int talentSinkVanguardRight;
        public float talentSinkVanguardRightValue = .002f; //Melee Damage
        #endregion

        #region Blood Mage
        public bool hasBloodMage = false;
        public int bloodMageLevel = 0;
        public int bloodMageSkillPoints = 0;
        public int bloodMageSpentSkillPoints = 0;

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

        public int specBloodMage_MaxHealth;
        public float specBloodMage_MaxHealthBase;
        public int specBloodMage_TransfusionDamage;
        public int specBloodMage_TransfusionDamageBase;
        public int specBloodMage_UltHeal;
        public float specBloodMage_UltHealBase;
        public int specBloodMage_CooldownReduction;
        public float specBloodMage_CooldownReductionBase;
        public float specBloodMage_CooldownReductionUltBase;
        public int specBloodMage_EnchantDamage;
        public float specBloodMage_EnchantDamageBase;

        public float bloodMageBasePassiveWeaponDamageMult;
        public float bloodMagePassiveChance;
        public float bloodMagePassiveDamageLevel;
        public bool bloodMagePassiveHealing = false;

        public float bloodMageSiphonHealMax = .15f;
        public int bloodMageSiphonBaseDamage = 20;
        public int bloodMageSiphonDamage;

        public bool bloodMageBloodEnchantment = false;
        public bool bloodMageEnchantmentOnCooldown = false;
        public float bloodMageEnchantmentBaseManaCost = 1f;
        public float bloodMageBaseDamageGain = .1f;
        public float bloodMageDamageGain;

        public float bloodMageBaseUltRegen = .02f;
        public float bloodMageUltRegen;
        public int bloodMageUltTicks = 8;
        int bloodMageCurUltTicks = 0;
        
        //int bloodMageInBattle = 0;

        public int talentSinkBloodMageLeft;
        public float talentSinkBloodMageLeftValue = .015f; //Ab. Power
        public int talentSinkBloodMageRight;
        public float talentSinkBloodMageRightValue = .0075f; //Heal Power
        #endregion

        #region Commander
        public bool hasCommander;
        public int commanderLevel = 0;
        public int commanderSkillPoints = 0;
        public int commanderSpentSkillPoints = 0;

        public float commanderPassiveEndurance = .01f;

        public int commanderBannerRange = 200;
        public int commanderBannerDuration = 60 * 10;
        public float commanderBannerEndurance = .85f;
        public float commanderBannerDamage = .1f;
        public int commanderBannerBuffDuration = 0;
        public int commanderBannerPersist = 60;
        public bool bannerFollowsPlayer;

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

        public int specCommander_BannerRange;
        public int specCommander_BannerRangeBase;
        public int specCommander_BannerDamageReduction;
        public float specCommander_BannerDamageReductionBase;
        public int specCommander_PassiveEndurance;
        public float specCommander_PassiveEnduranceBase;
        public int specCommander_WhipRange;
        public float specCommander_WhipRangeBase;
        public int specCommander_MinionDamage;
        public float specCommander_MinionDamageBase;

        public int talentSinkCommanderLeft;
        public float talentSinkCommanderLeftValue = .0035f; //Cooldown Reduction
        public int talentSinkCommanderRight;
        public float talentSinkCommanderRightValue = .004f; //Ultimate Cost Reduction
        #endregion

        #region Scout
        public bool hasScout = false;
        public int scoutLevel = 0;
        public int scoutSkillPoints = 0;
        public int scoutSpentSkillPoints = 0;

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

        public int specScout_TrapDamage;
        public float specScout_TrapDamageBase;
        public int specScout_Dodge;
        public float specScout_DodgeBase;
        public int specScout_UltCost;
        public float specScout_UltCostBase;
        public int specScout_CooldownReduction;
        public float specScout_CooldownReductionBase;
        public int specScout_ColaDuration;
        public float specScout_ColaDurationBase;

        public int talentSinkScoutLeft;
        public float talentSinkScoutLeftValue = .0035f; //Health
        public int talentSinkScoutRight;
        public float talentSinkScoutRightValue = .005f; //Ultimate Cost Reduction
        #endregion

        #region Soulmancer
        public bool hasSoulmancer;
        public int soulmancerLevel = 0;
        public int soulmancerSkillPoints = 0;
        public int soulmancerSpentSkillPoints = 0;
        
        public float soulmancerSoulRipChance;
        public float soulmancerSoulRipChance_Base;
        public float soulmancerSoulRipChance_PerLevel;
        public int soulmancerSoulRipDamage;
        public int soulmancerSoulRipDamage_Base;
        public int soulmancerSoulRipDamage_PerLevel;

        public int soulmancerConsumeDuration;
        public int soulmancerConsumeDuration_Cur;
        public int soulmancerConsumeDuration_Base;
        public int soulmancerConsumeDuration_PerLevel;
        public float soulmancerConsumeHeal;
        public float soulmancerConsumeHeal_Base;

        public int soulmancerSoulShatterDamage;
        public int soulmancerSoulShatterDamage_Base;
        public int soulmancerShatterDamage_PerLevel;
        public int soulmancerSoulShatterRange;
        public Vector2 soulmancerSoulShatterCastTarget;

        public float soulmancerSacrificeHealthCost;
        public float soulmancerSacrificeHealthCost_Base;
        public int soulmancerSacrificeSoulCount;
        public int soulmancerSacrificeSoulCount_Base;
        public int soulmancerSacrificeSoulCount_Cur;
        public int soulmancerSacrificeTimer;

        public string soulmancerTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string soulmancerTalent_2 = "N";
        public string soulmancerTalent_3 = "N";
        public string soulmancerTalent_4 = "N";
        public string soulmancerTalent_5 = "N";
        public string soulmancerTalent_6 = "N";
        public string soulmancerTalent_7 = "N";
        public string soulmancerTalent_8 = "N";
        public string soulmancerTalent_9 = "N";
        public string soulmancerTalent_10 = "N";

        public int specSoulmancer_SoulRipChance;
        public float specSoulmancer_SoulRipChanceBase;
        public int specSoulmancer_ConsumeDuration;
        public int specSoulmancer_ConsumeDurationBase;
        public int specSoulmancer_CooldownReduction;
        public float specSoulmancer_CooldownReductionBase;
        public int specSoulmancer_AbilityPower;
        public float specSoulmancer_AbilityPowerBase;
        public int specSoulmancer_MagicAttackSpeed;
        public float specSoulmancer_MagicAttackSpeedBase;

        public int talentSinkSoulmancerLeft;
        public float talentSinkSoulmancerLeftValue = .01f; //Ability Power
        public int talentSinkSoulmancerRight;
        public float talentSinkSoulmancerRightValue = .003f; //Magic Damage
        #endregion

        #region Crusader
        public bool hasCrusader;
        public int crusaderLevel = 0;
        public int crusaderSkillPoints = 0;
        public int crusaderSpentSkillPoints = 0;

        public float crusaderEndurance;
        public float crusaderEnduranceBuff;

        public float crusaderHealing;

        public int crusaderHammerDamageBase;
        public int crusaderHammerDamageLevel;
        public int crusaderHammerDamage;
        public float crusaderHammerVelocity;
        public float crusaderHammerVelocityMult;

        public bool crusaderGuardianAngel;
        public int crusaderGuardianAngelDuration;
        public float crusaderGuardianAngelEndurance;
        public float crusaderGuardianAngelEnduranceLevel;

        public string crusaderTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string crusaderTalent_2 = "N";
        public string crusaderTalent_3 = "N";
        public string crusaderTalent_4 = "N";
        public string crusaderTalent_5 = "N";
        public string crusaderTalent_6 = "N";
        public string crusaderTalent_7 = "N";
        public string crusaderTalent_8 = "N";
        public string crusaderTalent_9 = "N";
        public string crusaderTalent_10 = "N";

        public int talentSinkCrusaderLeft; 
        public float talentSinkCrusaderLeftValue = .015f; //Ab. Power
        public int talentSinkCrusaderRight;
        public float talentSinkCrusaderRightValue = .0075f; //Heal Power.

        #endregion

        #region Gambler
        public int gamblerLevel = 0;
        public int gamblerSkillPoints = 0;
        public int gamblerSpentSkillPoints = 0;

        //P
        public bool gamblerPassiveFeedback = true;
        public float gamblerPassiveMinDmg;
        public float gamblerPassiveMaxDmg;
        public float gamblerPassiveMinDmgPerLevel;

        //A1
        public int gamblerDiceCount;
        public int gamblerDiceDamageBase;
        public int gamblerDiceDamage;
        public int gamblerDiceDamagePerLevel;
        public bool gamblerDiceAghanimsHeal;

        //A2 
        public bool gamblerLuckyStreak;
        public float gamblerPassiveBoostDamage;
        public int gamblerPassiveBoostMaxDuration;
        public float gamblerPassiveBoostPerLevel;
        public bool gamblerLuckyStreakDodge;

        //Ult
        public bool gamblerUlt = false;
        public float gamblerUltAttackSpeed;
        public float gamblerUltAbilityPower;
        public float gamblerUltAbilityPowerPerLevel;
        public float gamblerUltCooldownReduction;
        public float gamblerUltCooldownReductionPerLevel;
        public int gamblerUltDurationBase;
        public int gamblerUltDuration;

        //Talents
        public string gamblerTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string gamblerTalent_2 = "N";
        public string gamblerTalent_3 = "N";
        public string gamblerTalent_4 = "N";
        public string gamblerTalent_5 = "N";
        public string gamblerTalent_6 = "N";
        public string gamblerTalent_7 = "N";
        public string gamblerTalent_8 = "N";
        public string gamblerTalent_9 = "N";
        public string gamblerTalent_10 = "N";

        public int talentSinkGamblerLeft;
        public float talentSinkGamblerLeftValue = .005f; //Cooldown Reduction
        public int talentSinkGamblerRight;
        public float talentSinkGamblerRightValue = .002f; //Ranged Damage
        #endregion

        #region Inventor
        public bool hasInventor;
        public int inventorLevel = 0;
        public int inventorSkillPoints = 0;
        public int inventorSpentSkillPoints = 0;

        public int inventorSentryFirerate;
        public int inventorSentryRange;
        public int inventorRangePerLevel;
        public int inventorSentryDamage;
        public int inventorSentryDamageBase;
        public int inventorSentryDamagePerLevel;
        public int inventorCogShotgunProjectiles;
        public int inventorCogShotgunDamage;
        public int inventorCogShotgunDamageBase;
        public int inventorCogShotgunDamagePerLevel;
        public int inventorOverclockCurDuration;
        public int inventorOverclockDuration;
        #endregion

        #region Plague
        public int plagueLevel;
        public int plagueSkillPoints;
        public int plagueSpentSkillPoints;

        public int plagueDebuffDuration; //In seconds

        //P
        public float plaguePassiveCritBase;
        public float plaguePassiveCrit;
        public float plaguePassivePlaguedDamageBase;
        public float plaguePassivePlaguedPerLevel;
        public float plaguePassivePlaguedDamage;

        //A1
        public int plagueInsectDamageBase;
        public int plagueInsectDamagePerLevel;
        public int plagueInsectDamageTotal;
        public int plagueInsectsBase;
        public int plagueInsectsTotal;
        private int _plagueInsectsToSpawn;

        //A2
        public int plagueInfectionBurstBase;
        public int plagueInfectionBurstPerLevel;
        public int plagueInfectionBurstTotal;
        public int plagueInfectionRange;

        //Ult
        public int plagueDeadzoneDuration;
        private int _plagueDeadzoneDurationCurrent;
        public int plagueDeadzoneBaseDamage;
        public int plagueDeadzoneDamagePerLevel;
        public int plagueDeadzoneDamage;
        public int plagueDeadzoneRange;
        public int plagueDeadzoneStrikeInterval;

        //Talents
        public string plagueTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string plagueTalent_2 = "N";
        public string plagueTalent_3 = "N";
        public string plagueTalent_4 = "N";
        public string plagueTalent_5 = "N";
        public string plagueTalent_6 = "N";
        public string plagueTalent_7 = "N";
        public string plagueTalent_8 = "N";
        public string plagueTalent_9 = "N";
        public string plagueTalent_10 = "N";

        public int talentSinkPlagueLeft;
        public float talentSinkPlagueLeftValue = .0125f; //Ab. Power
        public int talentSinkPlagueRight;
        public float talentSinkPlagueRightValue = .0025f; //Minion Crit
        #endregion

        #region Spike
        public int spikeLevel;
        public int spikeSkillPoints;
        public int spikeSpentSkillPoints;

        #endregion

        #region Scholar
        public int scholarLevel;
        public int scholarSkillPoints;
        public int scholarSpentSkillPoints;


        #region Scholar Studies
        public bool scholarStudiedBasicOres;
        #endregion
        #endregion

        int lazerTaget = -1;

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
        public List<string> soulmancerDefeatedBosses = new List<string>()
        {
        };
        public List<string> inventorDefeatedBosses = new List<string>()
        {
        };
        public List<string> crusaderDefeatedBosses = new List<string>()
        {
        };
        public List<string> gamblerDefeatedBosses = new List<string>()
        {
        };
        public List<string> plagueDefeatedBosses = new List<string>()
        {
        };
        public List<string> spikeDefeatedBosses = new List<string>()
        {
        };
        #endregion

        public override void Initialize()
        {
            bloodMageDefeatedBosses = new List<string>();
            scoutDefeatedBosses = new List<string>();
            vanguardDefeatedBosses = new List<string>();
            commanderDefeatedBosses = new List<string>();
            soulmancerDefeatedBosses = new List<string>();
            crusaderDefeatedBosses = new List<string>();
            gamblerDefeatedBosses = new List<string>();
            plagueDefeatedBosses = new List<string>();

            inventorDefeatedBosses = new List<string>();

            base.Initialize();
        }

        public override void ResetEffects()
        {
            #region Relics
            hasBleedingMoonStone = false;
            hasAghanims = false;
            hasUnstableConcoction = false;
            hasNeterihsToken = false;
            hasSqueaker = false;
            hasOldShield = false;
            hasFlanPudding = false;
            hasArcaneBlade = false;
            hasManaBag = false;
            hasDarkSign = false;
            hasEldenRing = false;
            hasPocketSlime = false;
            hasChocolateBar = false;
            hasaccountantRat = false;
            hasChaosAccelerant = false;
            hasTearsOfLife = false;
            hasOldBlood = false;
            hasLuckyLeaf = false;
            hasScalingWarbanner = false;
            hasPeanut = false;
            hasCactusRing = false;
            hasPorcelainMask = false;
            hasBrokenHeart = false;
            hasBloodGem = false;
            hasLeysMushroom = false;
            hasStrangeMushroom = false;
            hasMushroomConcentrate = false;
            hasBerserkersBrew = false;
            hasNessie = false;
            nessieBaseCooldown = 60 * 60;
            hasMajorsCare = false;
            hasWindsRoar = false;
            #endregion

            #region Player Stats
            abilityPower = 1f;
            healingPower = 1f;
            cooldownReduction = 1f;
            ability1cdr = 1f;
            ability2cdr = 1f;
            ultCooldownReduction = 1f;
            abilityDuration = 1f;
            //attackSpeed = 1f;
            //trueEndurance = 1f;
            defenseMult = 1f;
            lifeMult = 1f;
            manaMult = 1f;
            pSecHealthRegen = 0;
            canAddDeaths = true;
            dodgeChance = 0f;
            critDamageMult = 1f;
            minionCritChance = 0f;
            #endregion

            #region Class Menu Text
            P_Name = "";
            P_Desc = "";
            P_Effect_1 = "";
            P_Effect_2 = "";
            P_Effect_3 = "";
            P_Effect_4 = "";
            A1_Name = "";
            A1_Desc = "";
            A1_Effect_1 = "";
            A1_Effect_2 = "";
            A1_Effect_3 = "";
            A1_Effect_4 = "";
            A2_Name = "";
            A2_Desc = "";
            A2_Effect_1 = "";
            A2_Effect_2 = "";
            A2_Effect_3 = "";
            A2_Effect_4 = "";
            Ult_Name = "";
            Ult_Desc = "";
            Ult_Effect_1 = "";
            Ult_Effect_2 = "";
            Ult_Effect_3 = "";
            Ult_Effect_4 = "";
            #endregion

            #region Cards
            // Basic
            card_CarryValue = .0021f * Configs._ACMConfigServer.Instance.runesStatMult; //dmg
            card_HealthyValue = .0023f * Configs._ACMConfigServer.Instance.runesStatMult; //hp
            card_PowerfulValue = .016f * Configs._ACMConfigServer.Instance.runesStatMult; //ap
            card_MendingValue = .008f * Configs._ACMConfigServer.Instance.runesStatMult; //heal
            card_TimelessValue = .007f * Configs._ACMConfigServer.Instance.runesStatMult; //cdr
            card_MightyValue = .0064f * Configs._ACMConfigServer.Instance.runesStatMult; //ucdr
            card_SneakyValue = .0034f * Configs._ACMConfigServer.Instance.runesStatMult; //dodge
            card_NimbleHandsValue = .0035f * Configs._ACMConfigServer.Instance.runesStatMult; //as
            card_ImpenetrableValue = .0032f * Configs._ACMConfigServer.Instance.runesStatMult; //def
            card_MagicalValue = .0048f * Configs._ACMConfigServer.Instance.runesStatMult; //banner stats
            card_DeadeyeValue = .004f * Configs._ACMConfigServer.Instance.runesStatMult; //cdmg

            // Complex
            card_FortifiedValue_1 = .0016f * Configs._ACMConfigServer.Instance.runesStatMult; //hp
            card_FortifiedValue_2 = .0015f * Configs._ACMConfigServer.Instance.runesStatMult; //def
            card_MasterfulValue_1 = .01f * Configs._ACMConfigServer.Instance.runesStatMult; //ap
            card_MasterfulValue_2 = .006f * Configs._ACMConfigServer.Instance.runesStatMult; //heal
            card_SparkOfGeniusValue_1 = .0052f * Configs._ACMConfigServer.Instance.runesStatMult; //cdr
            card_SparkOfGeniusValue_2 = .0045f * Configs._ACMConfigServer.Instance.runesStatMult; //ucdr
            card_ProwlerValue_1 = .0014f * Configs._ACMConfigServer.Instance.runesStatMult; //dmg
            card_ProwlerValue_2 = .003f * Configs._ACMConfigServer.Instance.runesStatMult; //cdmg
            card_VeteranValue_1 = .0015f * Configs._ACMConfigServer.Instance.runesStatMult; //hp
            card_VeteranValue_2 = .0035f * Configs._ACMConfigServer.Instance.runesStatMult; //banner stats
            card_HealerValue_1 = .0052f * Configs._ACMConfigServer.Instance.runesStatMult; //heal
            card_HealerValue_2 = .0036f * Configs._ACMConfigServer.Instance.runesStatMult; //cdr
            card_MischievousValue_1 = .0025f * Configs._ACMConfigServer.Instance.runesStatMult; //cdmg
            card_MischievousValue_2 = .0025f * Configs._ACMConfigServer.Instance.runesStatMult; //dodge
            card_SeerValue_1 = .0082f * Configs._ACMConfigServer.Instance.runesStatMult; //ap
            card_SeerValue_2 = .0038f * Configs._ACMConfigServer.Instance.runesStatMult; //ucdr
            card_FerociousValue_1 = .0012f * Configs._ACMConfigServer.Instance.runesStatMult; //dmg
            card_FerociousValue_2 = .0028f * Configs._ACMConfigServer.Instance.runesStatMult; //as
            #endregion

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
            vanguardPassiveReflectAmount = .75f;
            vanguardSpearBaseDamage = 22;
            vanguardSwordBaseDamage = 24;
            vanguardShieldBaseDuration = 480; //8s
            vanguardShieldBaseDamageReduction = .16f;
            vanguardShieldDurationPerLevel = 15;
            vanguardUltimateBossExecute = .05f;
            vanguardShieldDamageReduction = 0;

            specVanguard_DefenseBase = .0025f;
            specVanguard_MeleeDamageBase = .002f;
            specVanguard_ShieldDamageReductionBase = .005f;
            specVanguard_SpearDamageBase = 8;
            specVanguard_UltCostBase = .003f;
            #endregion

            #region Blood Mage
            if (!hasBloodMage)
                bloodMageBloodEnchantment = false;
            hasBloodMage = false;
            //bloodMageHealthDrain = bloodMageBaseHealthDrain;
            bloodMageEnchantmentBaseManaCost = 1f;
            bloodMagePassiveChance = .15f;
            bloodMageBasePassiveWeaponDamageMult = .2f;
            bloodMageBaseDamageGain = .01f;
            bloodMageBaseUltRegen = .02f;
            bloodMageSiphonBaseDamage = 20;
            bloodMageSiphonHealMax = .15f;
            bloodMageDamageGain = .1f;
            bloodMageUltTicks = 8;
            bloodMageUltRegen = bloodMageBaseUltRegen;
            bloodMagePassiveHealing = false;
            bloodMagePassiveDamageLevel = .01f;
            #endregion

            #region Commander
            hasCommander = false;
            commanderUltActive = false;
            commanderUltDuration = 60 * 4;
            commanderBannerEndurance = .85f;
            commanderBannerDamage = .1f;
            commanderBannerRange = 200;
            commanderBannerDuration = 60 * 10;
            commanderBannerPersist = 60;
            commanderCryRange = 325;
            commanderCryDamage = commanderCryBaseDamage;
            commanderCryBonusDamage = .15f;
            commanderCryDuration = 60 * 4;
            commanderPassiveEndurance = .01f;
            bannerFollowsPlayer = false;

            specCommander_BannerRangeBase = 8;
            specCommander_BannerDamageReductionBase = .01f;
            specCommander_PassiveEnduranceBase = .001f;
            specCommander_WhipRangeBase = .02f;
            specCommander_MinionDamageBase = .004f;
            #endregion

            #region Scout
            hasScout = false;
            scoutPassiveSpeedBonus = .15f;
            scoutOtherJump = false;
            scoutColaDuration = 60 * 4;
            scoutColaDamageBonus = .25f;
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

            specScout_ColaDurationBase = 9; //.15s
            specScout_CooldownReductionBase = .01f;
            specScout_DodgeBase = .0015f;
            specScout_TrapDamageBase = 14;
            specScout_UltCostBase = .012f;
            #endregion

            #region Soulmancer
            hasSoulmancer = false;

            soulmancerSoulRipChance_Base = .2f;
            soulmancerSoulRipChance = 0;
            soulmancerSoulRipChance_PerLevel = .01f;
            soulmancerSoulRipDamage_Base = 15;
            soulmancerSoulRipDamage = 0;
            soulmancerSoulRipDamage_PerLevel = 3;

            soulmancerConsumeDuration_Base = 60 * 5;
            soulmancerConsumeDuration = 0;
            soulmancerConsumeDuration_PerLevel = 8;
            soulmancerConsumeHeal_Base = .0065f;
            soulmancerConsumeHeal = 0;

            soulmancerSoulShatterDamage_Base = 26;
            soulmancerSoulShatterDamage = 0;
            soulmancerShatterDamage_PerLevel = 9;
            soulmancerSoulShatterRange = 370;
            soulmancerSoulShatterCastTarget = Player.Center;

            soulmancerSacrificeHealthCost_Base = .0075f;
            soulmancerSacrificeHealthCost = 0;
            soulmancerSacrificeSoulCount_Base = 20;
            soulmancerSacrificeSoulCount = soulmancerSacrificeSoulCount_Base;

            specSoulmancer_AbilityPowerBase = .0125f;
            specSoulmancer_ConsumeDurationBase = 6;
            specSoulmancer_CooldownReductionBase = .01f;
            specSoulmancer_MagicAttackSpeedBase = .0035f;
            specSoulmancer_SoulRipChanceBase = .01f;
            #endregion

            #region Inventor
            hasInventor = false;

            inventorSentryFirerate = 60;
            inventorSentryRange = 360;
            inventorRangePerLevel = 10;
            inventorSentryDamage = 5 + (int)(Player.HeldItem.damage * .2f);
            inventorSentryDamagePerLevel = 2;

            inventorCogShotgunProjectiles = 5;
            inventorCogShotgunDamage = 0;
            inventorCogShotgunDamageBase = 5;
            inventorCogShotgunDamagePerLevel = 3;

            inventorOverclockDuration = 60 * 5;
            #endregion

            #region Crusader
            hasCrusader = false;

            crusaderEndurance = .97f;
            crusaderEnduranceBuff = .96f;

            crusaderHealing = .13f;

            crusaderHammerDamageBase = 21;
            crusaderHammerDamageLevel = 8;
            crusaderHammerDamage = 0;
            crusaderHammerVelocity = 25f;
            crusaderHammerVelocityMult = 1f;

            crusaderGuardianAngel = false;
            crusaderGuardianAngelDuration = 60 * 10;
            crusaderGuardianAngelEndurance = .6f; //40%
            crusaderGuardianAngelEnduranceLevel = .005f;
            #endregion

            #region Gambler
            gamblerPassiveMinDmg = .75f;
            gamblerPassiveMaxDmg = 1.25f;
            gamblerPassiveMinDmgPerLevel = .004f;

            gamblerDiceDamageBase = 6;
            gamblerDiceDamage = 0;
            gamblerDiceDamagePerLevel = 4;
            gamblerDiceCount = 3;
            gamblerDiceAghanimsHeal = false;

            gamblerLuckyStreak = false;
            gamblerPassiveBoostMaxDuration = 12 * 60;
            gamblerPassiveBoostDamage = .25f;
            gamblerPassiveBoostPerLevel = .005f;

            gamblerUlt = false;
            gamblerUltAttackSpeed = .2f;
            gamblerUltAbilityPower = 1f;
            gamblerUltAbilityPowerPerLevel = .05f;
            gamblerUltCooldownReduction = .5f;
            gamblerUltCooldownReductionPerLevel = .008f;
            gamblerUltDurationBase = 8 * 60;
            gamblerUltDuration = gamblerUltDurationBase;
            #endregion

            #region Plague
            plagueDebuffDuration = 8; //In seconds

            plaguePassiveCritBase = .04f;
            plaguePassiveCrit = plaguePassiveCritBase;
            plaguePassivePlaguedDamage = .01f;
            plaguePassivePlaguedPerLevel = .0025f;

            plaguePassivePlaguedDamageBase = .01f;
            plaguePassivePlaguedPerLevel = .0025f;
            plaguePassivePlaguedDamage = 0;

            plagueInsectDamageBase = 10;
            plagueInsectDamagePerLevel = 3;
            plagueInsectDamageTotal = 0;
            plagueInsectsBase = 1;

            plagueInfectionBurstBase = 13;
            plagueInfectionBurstPerLevel = 9;
            plagueInfectionBurstTotal = 0;
            plagueInfectionRange = 450;

            plagueDeadzoneDuration = 60 * 12;
            plagueDeadzoneBaseDamage = 5;
            plagueDeadzoneDamagePerLevel = 2;
            plagueDeadzoneDamage = 0;
            plagueDeadzoneRange = 275;
            plagueDeadzoneStrikeInterval = 15;
            #endregion

            #region Scholar
            
            #endregion

            compactHUD = Configs.ACMConfigClient.Instance.compactHUD;
            blinkingHUD = Configs.ACMConfigClient.Instance.blinkingHUD;
            base.ResetEffects();
        }

        #region MP Player Syncing
        //public override void clientClone(ModPlayer clientClone)
        //{
        //    ACMPlayer clone = clientClone as ACMPlayer;
        //
        //    clone.vanguardSkillPoints = vanguardSkillPoints;
        //    clone.bloodMageSkillPoints = bloodMageSkillPoints;
        //    clone.commanderSkillPoints = commanderSkillPoints;
        //    clone.scoutSkillPoints = scoutSkillPoints;
        //    clone.soulmancerSkillPoints = soulmancerSkillPoints;
        //    clone.crusaderSkillPoints = crusaderSkillPoints;
        //
        //    base.clientClone(clientClone);
        //}
        // /\ & \/
        //public override void SendClientChanges(ModPlayer clientPlayer)
        //{
        //    ACMPlayer clone = clientPlayer as ACMPlayer;
        //
        //    if (clone.vanguardSkillPoints != vanguardSkillPoints ||
        //        clone.bloodMageSkillPoints != bloodMageSkillPoints ||
        //        clone.commanderSkillPoints != commanderSkillPoints ||
        //        clone.scoutSkillPoints != scoutSkillPoints ||
        //        clone.soulmancerSkillPoints != soulmancerSkillPoints ||
        //        clone.crusaderSkillPoints != crusaderSkillPoints)
        //    {        
        //        var packet = Mod.GetPacket();
        //        packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncTalentPoints);
        //        packet.Write((byte)Player.whoAmI);
        //        packet.Write(equippedClass);
        //        packet.Send();
        //    }
        //}

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


        // Old attack speed, unused since tmodloader added their own player.GetAttackSpeed()
        //public override float UseTimeMultiplier(Item item)
        //{
        //    if(Player.HeldItem.pick > 0 || Player.HeldItem.axe > 0 || Player.HeldItem.hammer > 0)
        //        return 1f;
        //    else
        //        return 1f - (attackSpeed - 1f);
        //}

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            //mediumCoreDeath = false;
            
            return new[] {
            new Item(ItemType<Items.WhiteCloth>()),
            new Item(ItemType<Items.ClassBook>()),
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

            healthToRegen = 0;
            healthToRegenMedium = 0;
            healthToRegenSlow = 0;
            healthToRegenSnail = 0;
            healthToRegenSecond = 0;

            bloodMageBloodEnchantment = false;
            //bloodMageCurUltTicks = 0;

            accountantRatDamageAccumulated = 0;
            accountantRatTicks = 0;
            base.UpdateDead();
        }

        //public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        //{
        //    healValue *= 2;
        //    base.GetHealLife(item, quickHeal, ref healValue);
        //}

        public override bool? CanHitNPCWithItem(Item item, NPC target)
        {
            if(hasMajorsCare)
                if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.TownBunny)
                    return false;

            return base.CanHitNPCWithItem(item, target);
        }

        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            if (hasMajorsCare)
                if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.TownBunny)
                    return false;

            return base.CanHitNPCWithProj(proj, target);
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= GetInstance<Configs._ACMConfigServer>().enemyDamageMultiplier;

            //Dodge
            if (Main.rand.NextFloat() < dodgeChance)
                Player.NinjaDodge();

            if (hasNessie && nessieCooldown <= 0)
            {
                Player.NinjaDodge();
                nessieCooldown = nessieBaseCooldown;
                int nessieChosenText = Main.rand.Next(nessieProcText.Length);
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 140, Player.width, Player.height), Color.White, nessieProcText[nessieChosenText], true);
                SoundStyle hit = new SoundStyle($"{nameof(ApacchiisClassesMod2)}/Sounds/SoundEffects/NessieHit");
                SoundEngine.PlaySound(hit with
                {
                    Volume = 5f,
                }, Player.Center);
            }

            if (commanderBannerBuffDuration > 0)
                modifiers.FinalDamage *= commanderBannerEndurance;

            if (hasCrusader)
            {
                modifiers.FinalDamage *= crusaderEndurance;
                if (healthToRegen > 0 || healthToRegenMedium > 0 || healthToRegenSecond > 0 || healthToRegenSlow > 0 || healthToRegenSnail > 0)
                    modifiers.FinalDamage *= crusaderEnduranceBuff;
            }

            if (crusaderGuardianAngel)
                modifiers.FinalDamage *= crusaderGuardianAngelEndurance;

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.TrainingRapier>())
                modifiers.FinalDamage.Base -= 6;

            // This stays last in the bottom
            if (hasaccountantRat)
            {
                modifiers.FinalDamage *= 1.1f;
            }

            base.ModifyHitByNPC(npc, ref modifiers);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            //Dodge
            if (Main.rand.NextFloat() < dodgeChance)
                Player.NinjaDodge();

            if (hasNessie && nessieCooldown <= 0)
            {
                Player.NinjaDodge();
                nessieCooldown = nessieBaseCooldown;
                int nessieChosenText = Main.rand.Next(nessieProcText.Length);
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 140, Player.width, Player.height), Color.White, nessieProcText[nessieChosenText], true);
                SoundStyle hit = new SoundStyle($"{nameof(ApacchiisClassesMod2)}/Sounds/SoundEffects/NessieHit");
                SoundEngine.PlaySound(hit with
                {
                    Volume = 5f,
                }, Player.Center);
            }

            if (hasOldShield)
                modifiers.FinalDamage *= .86f;

            if (commanderBannerBuffDuration > 0)
                modifiers.FinalDamage *= commanderBannerEndurance;

            if (hasCrusader)
            {
                modifiers.FinalDamage *= crusaderEndurance;
                if (healthToRegen > 0 || healthToRegenMedium > 0 || healthToRegenSecond > 0 || healthToRegenSlow > 0 || healthToRegenSnail > 0)
                    modifiers.FinalDamage *= crusaderEnduranceBuff;
            }

            if (crusaderGuardianAngel)
                modifiers.FinalDamage *= crusaderGuardianAngelEndurance;

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.TrainingRapier>())
                modifiers.FinalDamage.Base -= 6;

            // This stays last in the bottom
            if (hasaccountantRat)
            {
                modifiers.FinalDamage *= 1.1f;
            }

            base.ModifyHitByProjectile(proj, ref modifiers);
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            hurtInfo.Damage = (int)(hurtInfo.Damage * GetInstance<Configs._ACMConfigServer>().enemyDamageMultiplier);

            InBattle();
            //if (ultCharge < ultChargeMax && !hasCactusRing)
            //    ultCharge = (int)(ultCharge * .96f);

            int hitDir;
            if (npc.position.X < Player.position.X)
                hitDir = -1;
            else
                hitDir = 1;

            if (hasVanguard && !npc.dontTakeDamage)
                Player.ApplyDamageToNPC(npc, (int)(Player.statDefense * vanguardPassiveReflectAmount), 0, hitDir, false);

            if (hasEldenRing)
                Player.immuneTime -= 15;

            if (hasNeterihsToken)
                Player.immuneTime += 60;
            if (hasSqueaker)
            {
                Player.immuneTime += 15;
                SoundEngine.PlaySound(SoundID.Critter, Player.Center);
            }

            if (hasChocolateBar)
            {
                chocolateBarTimer = 90;
                chocolateBarStoredHealth = (int)(hurtInfo.Damage * .11f);
            }

            if (hasFlanPudding)
            {
                if (flanPuddingTimer <= 0)
                {
                    int heal = (int)(Player.statLifeMax2 * .02f);
                    HealPlayer(1, 1, heal);
                    flanPuddingTimer = 120;
                }
            }

            // This stays last in the bottom
            if (hasaccountantRat)
            {
                accountantRatTicks = 20;
                Player.statLife += hurtInfo.Damage;
                accountantRatDamageAccumulated += hurtInfo.Damage;
                accountantRatDamageAccumulated -= 1;
            }

            totalDamageTaken += hurtInfo.Damage;

            base.OnHitByNPC(npc, hurtInfo);
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            hurtInfo.Damage = (int)(hurtInfo.Damage * GetInstance<Configs._ACMConfigServer>().enemyDamageMultiplier);

            InBattle();
            //if (ultCharge < ultChargeMax && !hasCactusRing)
            //    ultCharge = (int)(ultCharge * .96f);

            if (hasEldenRing)
                Player.immuneTime -= 15;

            if (hasNeterihsToken)
                Player.immuneTime += 60;
            if (hasSqueaker)
            {
                Player.immuneTime += 15;
                SoundEngine.PlaySound(SoundID.Critter, Player.Center);
            }

            if (hasChocolateBar)
            {
                chocolateBarTimer = 90;
                chocolateBarStoredHealth = (int)(hurtInfo.Damage * .11f);
            }

            if (hasFlanPudding)
            {
                if (flanPuddingTimer <= 0)
                {
                    int heal = (int)(Player.statLifeMax2 * .02f);
                    HealPlayer(1, 1, heal);
                    flanPuddingTimer = 120;
                }
            }

            // This stays last in the bottom
            if (hasaccountantRat)
            {
                accountantRatTicks = 20;
                Player.statLife += hurtInfo.Damage;
                accountantRatDamageAccumulated += hurtInfo.Damage;
                accountantRatDamageAccumulated -= 1;
            }

            totalDamageTaken += hurtInfo.Damage;

            base.OnHitByProjectile(proj, hurtInfo);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type != NPCID.TargetDummy)
                InBattle();

            if (Player.name == "Modding Man" || devTool)
                InBattle();

            if (target.life <= 0)
                enemiesKilled++;

            // Blood Gem
            if (bloodGemMeleeTimer <= 0 && hasBloodGem)
            {
                int heal = (int)(Player.statLifeMax2 * .02f);
                if (Player.statLife < Player.statLifeMax2)
                {
                    Player.statLife += heal;
                    Player.HealEffect(heal);
                    bloodGemMeleeTimer = 120;
                }
            }

            // Ley's Mushroom Heal
            if (Main.rand.NextFloat() < leysMushroomHealChance && hasLeysMushroom)
            {
                if (Main.hardMode) leysMushroomHeal = .04f;
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * leysMushroomHeal);
                Player.statLife += heal;
                Player.HealEffect(heal);
            }

            //Ley's Mushroom Buffs
            if (Main.rand.NextFloat() < leysMushroomBuffChance && hasLeysMushroom)
            {
                int duration = 60 * 3;
                if (Main.hardMode) duration = 60 * 4;

                int buff = Main.rand.Next(4);
                if (buff == 0)
                    Player.AddBuff(BuffType<Buffs.LeysDamage>(), duration);
                if (buff == 1)
                    Player.AddBuff(BuffType<Buffs.LeysCrit>(), duration);
                if (buff == 2)
                    Player.AddBuff(BuffType<Buffs.LeysEndurance>(), duration);
                if (buff == 3)
                    Player.AddBuff(BuffType<Buffs.LeysAttackSpeed>(), duration);
            }

            int damageDealtFinal;
            if (!target.friendly && target.type != NPCID.TargetDummy && !target.SpawnedFromStatue)
            {
                damageDealtFinal = damageDone;
                if (damageDealtFinal > target.life)
                    damageDealtFinal = target.life;
                damageDealt += damageDealtFinal;

                int dpsDealt = Player.getDPS();
                if (dpsDealt > highestDPS)
                    highestDPS = dpsDealt;

                int critDealt = 0;
                if (hit.Crit)
                    critDealt = damageDone;
                if (critDealt > highestCrit)
                    highestCrit = critDealt;
            }

            if (hasArcaneBlade && item.DamageType == DamageClass.Melee && Player.statMana < Player.statManaMax2 && arcaneBladeTimer >= 30)
            {
                int manaToRestore = (int)(damageDone * .08f);
                if (manaToRestore < 1)
                    manaToRestore = 1;
                if (manaToRestore > 20)
                    manaToRestore = 20;
                Player.statMana += manaToRestore;
                Player.ManaEffect(manaToRestore);
                arcaneBladeTimer = 0;
            }

            //Soulmancer
            if (hasSoulmancer && Player.HeldItem.DamageType == DamageClass.Magic)
            {
                if (Main.rand.NextFloat() < soulmancerSoulRipChance)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                    speed.Normalize();
                    speed *= 15f;
                    Projectile.NewProjectile(default, target.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragment>(), (int)(soulmancerSoulRipDamage * abilityPower), 0, Player.whoAmI);
                }
            }

            base.OnHitNPCWithItem(item, target, hit, damageDone);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type != NPCID.TargetDummy)
                InBattle();

            if (Player.name == "Modding Man" || devTool)
                InBattle();

            if (target.life <= 0)
                enemiesKilled++;

            // Blood Gem
            if (bloodGemProjectileTimer <= 0 && hasBloodGem)
            {
                int heal = (int)(Player.statLifeMax2 * .01f);
                if (Player.statLife < Player.statLifeMax2)
                {
                    Player.statLife += heal;
                    Player.HealEffect(heal);
                    bloodGemProjectileTimer = 240;
                }
            }

            // Ley's Mushroom Heal
            if (Main.rand.NextFloat() < leysMushroomHealChance && hasLeysMushroom)
            {
                if (Main.hardMode) leysMushroomHeal = .04f;
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * leysMushroomHeal);
                Player.statLife += heal;
                Player.HealEffect(heal);
            }

            //Ley's Mushroom Buffs
            if (Main.rand.NextFloat() < leysMushroomBuffChance && hasLeysMushroom)
            {
                int duration = 60 * 3;
                if (Main.hardMode) duration = 60 * 4;

                int buff = Main.rand.Next(4);
                if (buff == 0)
                    Player.AddBuff(BuffType<Buffs.LeysDamage>(), duration);
                if (buff == 1)
                    Player.AddBuff(BuffType<Buffs.LeysCrit>(), duration);
                if (buff == 2)
                    Player.AddBuff(BuffType<Buffs.LeysEndurance>(), duration);
                if (buff == 3)
                    Player.AddBuff(BuffType<Buffs.LeysAttackSpeed>(), duration);
            }

            //Wind's Road Minon Heal
            if (hasWindsRoar && _windsRoarCooldown <= 0f && proj.minion)
            {
                int heal = (int)(Player.statLifeMax2 * .01f * healingPower);
                HealPlayer(1, 1, heal);
                _windsRoarCooldown = 60 * 5f;
            }

            int damageDealtFinal;
            if (!target.friendly && target.type != NPCID.TargetDummy && !target.SpawnedFromStatue)
            {
                damageDealtFinal = damageDone;
                if (damageDealtFinal > target.life)
                    damageDealtFinal = target.life;
                damageDealt += damageDealtFinal;

                int dpsDealt = Player.getDPS();
                if (dpsDealt > highestDPS)
                    highestDPS = dpsDealt;

                int critDealt = 0;
                if (hit.Crit)
                    critDealt = damageDone;
                if (critDealt > highestCrit)
                    highestCrit = critDealt;
            }

            if (hasArcaneBlade && proj.DamageType == DamageClass.Melee && Player.statMana < Player.statManaMax2 && arcaneBladeTimer >= 30)
            {
                int manaToRestore = (int)(damageDone * .08f);
                if (manaToRestore < 1)
                    manaToRestore = 1;
                if (manaToRestore > 20)
                    manaToRestore = 20;
                Player.statMana += manaToRestore;
                Player.ManaEffect(manaToRestore);
                arcaneBladeTimer = 0;
            }

            //Blood Mage Aghs Heal
            if (proj.type == ProjectileType<Projectiles.BloodMage.Transfusion>() && hasAghanims || proj.type == ProjectileType<Projectiles.BloodMage.Transfusion>() && hasAghanimsShard)
            {
                int heal = (int)(damageDone * .1f * healingPower);
                HealPlayer(1, 3, heal);
            }

            //Gambler Aghs Dice Heal
            if (proj.type == ProjectileType<Projectiles.Gambler.Dice>() && hasAghanims || proj.type == ProjectileType<Projectiles.Gambler.Dice>() && hasAghanimsShard)
            {
                float percentHeal = Main.rand.NextFloat(.0025f, .005f);
                int heal = (int)(Player.statLifeMax2 * percentHeal * healingPower);
                if (heal < 1) heal = 1;
                if (Player.statLife < Player.statLifeMax2)
                {
                    Player.statLife += heal;
                    Player.HealEffect(heal);
                }
            }

            //Soulmancer
            if (hasSoulmancer && target.type != NPCID.TargetDummy)
            {
                if (Player.HeldItem.DamageType == DamageClass.Magic)
                {
                    if (Main.rand.NextFloat() < soulmancerSoulRipChance && proj.type != ProjectileType<Projectiles.Soulmancer.SoulFragment>() && proj.type != ProjectileType<Projectiles.Soulmancer.SoulShatter>())
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                        speed.Normalize();
                        speed *= 15f;
                        Projectile.NewProjectile(default, target.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragment>(), (int)(soulmancerSoulRipDamage * abilityPower), 0, Player.whoAmI);
                    }
                }

                if (proj.type == ProjectileType<Projectiles.Soulmancer.SoulFragment>() && soulmancerConsumeDuration_Cur > 0 && Player.statLife < Player.statLifeMax2)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                    speed.Normalize();
                    speed *= 18f;
                    Projectile.NewProjectile(default, target.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragmentAbsorb>(), 0, 0, Player.whoAmI);
                }
            }

            //Plague
            if (equippedClass == "Plague")
            {
                if(proj.type == ProjectileType<Projectiles.Plague.PlagueInsect>())
                {
                    target.AddBuff(BuffType<Buffs.Plague.PlagueInfection>(), 60 * plagueDebuffDuration);

                    int projCount = 0;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        if (Main.projectile[i].active && Main.projectile[i].type == ProjectileType<Projectiles.Plague.PlagueInsect>())
                            projCount++;
                    }

                    if (projCount < 20)
                    {
                        Projectile.NewProjectile(null, target.Center, new Vector2(0, -1), ProjectileType<Projectiles.Plague.PlagueInsect>(), (int)(plagueInsectDamageTotal * abilityPower), 0f, Player.whoAmI);
                        if (Main.rand.NextFloat() < .25f)
                        {
                            int p = Projectile.NewProjectile(null, target.Center, new Vector2(0, 1), ProjectileType<Projectiles.Plague.PlagueInsect>(), (int)(plagueInsectDamageTotal * abilityPower), 0f, Player.whoAmI);
                            Main.projectile[p].timeLeft = 60 * 8;
                        }
                    }
                }    
            }

            base.OnHitNPCWithProj(proj, target, hit, damageDone);
        }


        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            //Vanguard Execute
            if (target.boss && target.life <= target.lifeMax * vanguardUltimateBossExecute && proj.type == ProjectileType<Projectiles.Vanguard.VanguardUltimate>())
                modifiers.FinalDamage += target.lifeMax * 3;
            if (!target.boss && target.life <= target.lifeMax / 3 && proj.type == ProjectileType<Projectiles.Vanguard.VanguardUltimate>())
                modifiers.FinalDamage += target.lifeMax * 3;

            //Gambler Passive
            if (equippedClass == "Gambler")
            {
                //Gambler Ability 2
                if (gamblerLuckyStreak)
                {
                    gamblerPassiveMinDmg += gamblerPassiveBoostDamage;
                    gamblerPassiveMaxDmg += gamblerPassiveBoostDamage;
                }

                float mult = Main.rand.NextFloat(gamblerPassiveMinDmg, gamblerPassiveMaxDmg);
                if (gamblerPassiveFeedback)
                {
                    if (mult >= (gamblerPassiveMinDmg + gamblerPassiveMaxDmg) / 2)
                        CombatText.NewText(new Rectangle((int)Player.Center.X, (int)(Player.Center.Y) + 85, 1, 1), Color.CadetBlue, ">", true, true);
                    else
                        CombatText.NewText(new Rectangle((int)Player.Center.X, (int)(Player.Center.Y) + 85, 1, 1), Color.IndianRed, "<", true, true);
                }
                modifiers.FinalDamage *= mult;
            }

            //Plague passive Plagued damage bonus
            if(equippedClass == "Plague" && proj.minion)
            {
                if (target.HasBuff(BuffType<Buffs.Plague.PlagueInfection>()))
                {
                    modifiers.FinalDamage += plaguePassivePlaguedDamage;
                    minionCritChance *= 1.25f;
                }
            }

            //Minion Crits
            if (Main.rand.NextFloat() < minionCritChance && proj.minion)
                modifiers.SetCrit();

            //Plague A1 insect crit
            if (Main.rand.NextFloat() < minionCritChance && proj.type == ProjectileType<Projectiles.Plague.PlagueInsect>())
                modifiers.SetCrit();

            if (isUnstableConcoctionReady)
            {
                modifiers.FinalDamage *= 3;
                isUnstableConcoctionReady = false;
            }

            if (bloodMageBloodEnchantment && Player.HeldItem.DamageType == DamageClass.Magic)
            {
                modifiers.FinalDamage *= (bloodMageDamageGain + 1f);
                if (!bloodMageEnchantmentOnCooldown)
                {
                    bloodMageEnchantmentOnCooldown = true;
                    int heal = (int)(Player.HeldItem.mana * bloodMageEnchantmentBaseManaCost);

                    if(proj.type == 931 && target.boss) //Nightglow projectile ID
                        heal /= 5;
                    
                    Player.statLife += heal;
                    CombatText.NewText(new Rectangle((int)Player.Center.X + 32, (int)Player.Center.Y + 64, 1, 1), Color.Green, "" + heal);
                }
            }

            if (Main.rand.NextFloat() < bloodMagePassiveChance && Player.HeldItem.DamageType == DamageClass.Magic && hasBloodMage)
            {
                modifiers.FinalDamage.Base += (int)(Player.HeldItem.damage * bloodMageBasePassiveWeaponDamageMult * abilityPower);
                float velDirX = Main.rand.NextFloat(-5f, 5f);
                float velDirY = Main.rand.NextFloat(-5f, 5f);
                SoundEngine.PlaySound(SoundID.BloodZombie, target.Center);
                for (int x = 0; x < 15; x++)
                    Dust.NewDustDirect(target.position, 1, 1, DustID.Blood, velDirX, velDirY, 0, default, Main.rand.NextFloat(1f, 2f));

                if (bloodMagePassiveHealing)
                {
                    int healing = (int)(Player.statLifeMax2 * .0025f);
                    if (healing < 1) healing = 1;
                    Player.statLife += healing;
                    Player.HealEffect(healing);
                }
            }

            if (commanderBannerBuffDuration > 0)
                modifiers.FinalDamage += commanderBannerDamage;

            if (commanderUltActive)
                modifiers.SetCrit();

            if (hasScout && scoutColaCurDuration > 0)
                modifiers.FinalDamage += scoutColaDamageBonus;

            modifiers.CritDamage += critDamageMult - 1f;

            // Insect reduced dmg vs worms
            if (equippedClass == "Plague" && proj.type == ProjectileType<Projectiles.Plague.PlagueInsect>() && target.realLife != 0)
                modifiers.FinalDamage *= .02f;

            base.ModifyHitNPCWithProj(proj, target, ref modifiers);
        }

        public override void PlayerConnect()
        {
            GetInstance<ACM2ModSystem>()._HUD.SetState(null);
            GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
            base.PlayerConnect();
        }

        public override void PlayerDisconnect()
        {
            GetInstance<ACM2ModSystem>()._HUD.SetState(null);
            GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
            base.PlayerDisconnect();
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            //Gambler Passive
            if (equippedClass == "Gambler")
            {
                //Gambler Ability 2
                if (gamblerLuckyStreak)
                {
                    gamblerPassiveMinDmg += gamblerPassiveBoostDamage;
                    gamblerPassiveMaxDmg += gamblerPassiveBoostDamage;
                }

                float mult = Main.rand.NextFloat(gamblerPassiveMinDmg, gamblerPassiveMaxDmg);
                if (gamblerPassiveFeedback)
                {
                    if (mult >= (gamblerPassiveMinDmg + gamblerPassiveMaxDmg) / 2)
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)(target.position.Y + (target.height * 3f)), target.width / 2, target.height / 2), Color.CadetBlue, ">", false, true);
                    else
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)(target.position.Y + (target.height * 3f)), target.width / 2, target.height / 2), Color.IndianRed, "<", false, true);
                }
                modifiers.FinalDamage *= mult;
            }

            if (isUnstableConcoctionReady)
            {
                modifiers.FinalDamage *= 3;
                isUnstableConcoctionReady = false;
            }

            if (bloodMageBloodEnchantment && Player.HeldItem.DamageType == DamageClass.Magic)
            {
                modifiers.FinalDamage *= (bloodMageDamageGain + 1f);

                if (!bloodMageEnchantmentOnCooldown)
                {
                    int heal = (int)(Player.HeldItem.mana * bloodMageEnchantmentBaseManaCost);
                    Player.statLife += heal;
                    CombatText.NewText(new Rectangle((int)Player.Center.X + 32, (int)Player.Center.Y + 64, 1, 1), Color.Green, "" + (int)(Player.HeldItem.mana * bloodMageEnchantmentBaseManaCost));
                    bloodMageEnchantmentOnCooldown = true;
                }
            }

            if (Main.rand.NextFloat() < bloodMagePassiveChance && Player.HeldItem.DamageType == DamageClass.Magic && hasBloodMage)
            {
                modifiers.FinalDamage.Base += (int)(Player.HeldItem.damage * bloodMageBasePassiveWeaponDamageMult * abilityPower);
                float velDirX = Main.rand.NextFloat(-5f, 5f);
                float velDirY = Main.rand.NextFloat(-5f, 5f);
                SoundEngine.PlaySound(SoundID.BloodZombie, target.Center);
                for (int x = 0; x < 15; x++)
                    Dust.NewDustDirect(target.position, 1, 1, DustID.Blood, velDirX, velDirY, 0, default, Main.rand.NextFloat(1f, 2f));

                if (bloodMagePassiveHealing)
                {
                    int healing = (int)(Player.statLifeMax2 * .0025f);
                    if (healing < 1) healing = 1;
                    Player.statLife += healing;
                    Player.HealEffect(healing);
                }
            }

            if (commanderBannerBuffDuration > 0)
                modifiers.FinalDamage += commanderBannerDamage;

            if (commanderUltActive)
                modifiers.SetCrit();

            if (hasScout && scoutColaCurDuration > 0)
                modifiers.FinalDamage += scoutColaDamageBonus;

            modifiers.CritDamage += critDamageMult - 1f;

            base.ModifyHitNPCWithItem(item, target, ref modifiers);
        }

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            if (bloodMageBloodEnchantment)
            {
                Player.statLife -= (int)(Player.HeldItem.mana * bloodMageEnchantmentBaseManaCost);
                CombatText.NewText(new Rectangle((int)Player.Center.X - 32, (int)Player.Center.Y + 64, 1, 1), Color.Red, "" + (int)(Player.HeldItem.mana * bloodMageEnchantmentBaseManaCost));
            }

            base.OnConsumeMana(item, manaConsumed);
        }

        public override void PreUpdateBuffs()
        {
            if (Player.HeldItem.damage > 1 && Player.HeldItem.useTime > 0)
            {
                float shotsPerSecond = 60 / Player.HeldItem.useTime;
                float dps = shotsPerSecond * Player.HeldItem.damage;
                abilityPower += dps * Configs._ACMConfigServer.Instance.abilityPowerWeaponDPSMult / 100;
            }

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.SoulBurner>())
                soulmancerSoulRipChance += .25f;

            base.PreUpdateBuffs();
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (hasBrokenHeart && healValue >= 50 && item.type != ItemID.Mushroom)
                healValue += (int)(Player.statLifeMax2 * .07f);

            if (hasMushroomConcentrate && item.type == ItemID.Mushroom)
            {
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * .06f);
                healValue = 60 + heal;
            }

            if (hasBerserkersBrew && item.type != ItemID.Mushroom)
            {
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * .35f);
                healValue += heal;
            }

            base.GetHealLife(item, quickHeal, ref healValue);
        }

        public override void PreUpdate()
        {
            chocolateBarTimer--;
            inventorOverclockCurDuration--;
            flanPuddingTimer--;
            if (bloodGemProjectileTimer > 0)
                bloodGemProjectileTimer--;
            if (bloodGemMeleeTimer > 0)
                bloodGemMeleeTimer--;
            nessieCooldown--;
            majorsCareTimer--;
            _windsRoarCooldown--;

            if (hasMajorsCare)
            {
                if (majorsCareTimer <= 0f)
                {
                    Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.MajorsCareProj>(), 0, 0f, Player.whoAmI);
                    majorsCareTimer = 60 * 5;
                }
            }

            //if (Configs._ACMConfigServer.Instance.startWithRelic && !gotFreeRelic)
            //{
            //    Player.QuickSpawnItem(null, ItemType<Items.Relics.RandomRelic>(), 1);
            //    gotFreeRelic = true;
            //}

            if (levelUpText && Main.netMode != NetmodeID.Server && Configs.ACMConfigClient.Instance.announceLevelUp)
            {
                Main.NewText($"You have leveled up! You have new Skill Points an Rune Draws to spend!");
                levelUpText = false;
            }

            if (Player.statLife <= 0 && !Player.dead && bloodMageBloodEnchantment && hasBloodMage)
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " ran out of blood for their 'Blood Enchantment' ability!. Silly player!"), 1, 1);
            if (Player.statLife <= 0 && !Player.dead && soulmancerSacrificeSoulCount_Cur > 0 && hasSoulmancer)
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " has sacrificed their own soul for the cause!. Now that's dedication!"), 1, 1);

            int selectedRatText = Main.rand.Next(accountantRatDeathText.Length);
            if (Player.statLife <= 0 && accountantRatDamageAccumulated > 0)
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + accountantRatDeathText[selectedRatText]), 1, 1);

            arcaneBladeTimer++;

            // Old %hp based DoT
            //if (testRelicDamageAccumulated > 0 && testRelicTimer % 2 == 0)
            //{
            //    int damageToTake = (int)(Player.statLifeMax2 * .005f);
            //    if(damageToTake < 1)
            //        damageToTake = 1;
            //
            //    Player.statLife -= damageToTake;
            //    testRelicDamageAccumulated -= damageToTake;
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 140, Player.width, Player.height), Color.IndianRed, "" + testRelicDamageAccumulated);
            //}

            accountantRatTimer++;
            if (accountantRatTicks == 0)
                accountantRatDamageAccumulated = 0;
            if(accountantRatTimer % 15 == 0 && accountantRatTicks > 0)
            {
                if (accountantRatDamageAccumulated > 0)
                {
                    Player.statLife -= accountantRatDamageAccumulated / 20;
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 15, Player.width, Player.height), Color.IndianRed, "" + accountantRatDamageAccumulated / 20);
                    accountantRatTicks--;
                }
            }
               


            if (globalSingleSecondTimer == 0)
                if (healthToRegen > 0 || healthToRegenMedium > 0 || healthToRegenSlow > 0 || healthToRegenSnail > 0 || healthToRegenSecond > 0)
                    for (int x = 0; x < 5; x++)
                        Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 1.75f));

            if(healthToRegen > 0)
            {
                int healing = (int)(Player.statLifeMax2 * .005f);
                if (healing < 1) healing = 1;
                if (healing > healthToRegen) healing = healthToRegen;
                Player.statLife += healing;
                healthToRegen -= healing;
            }

            healthToRegenMediumTimer++;
            if(healthToRegenMediumTimer % 4 == 0)
            {
                if(healthToRegenMedium > 0)
                {
                    int healing = (int)(Player.statLifeMax2 * .005f);
                    if (healing < 1) healing = 1;
                    if (healing > healthToRegenMedium) healing = healthToRegenMedium;
                    Player.statLife += healing;
                    healthToRegenMedium -= healing;
                }    
            }

            healthToRegenSlowTimer++;
            if (healthToRegenSlowTimer % 8 == 0)
            {
                if (healthToRegenSlow > 0)
                {
                    int healing = (int)(Player.statLifeMax2 * .005f);
                    if (healing < 1) healing = 1;
                    if (healing > healthToRegenSlow) healing = healthToRegenSlow;
                    Player.statLife += healing;
                    healthToRegenSlow -= healing;
                }
            }

            healthToRegenSnailTimer++;
            if (healthToRegenSnailTimer % 20 == 0)
            {
                if (healthToRegenSnail > 0)
                {
                    int healing = (int)(Player.statLifeMax2 * .005f);
                    if (healing < 1) healing = 1;
                    if (healing > healthToRegenSnail) healing = healthToRegenSnail;
                    Player.statLife += healing;
                    healthToRegenSnail -= healing;
                }
            }

            healthToRegenSecondTimer++;
            if (healthToRegenSecondTimer % 60 == 0)
            {
                if (healthToRegenSecond > 0)
                {
                    int healing = (int)(Player.statLifeMax2 * .01f);
                    if (healing < 1) healing = 1;
                    if (healing > healthToRegenSecond) healing = healthToRegenSecond;
                    Player.statLife += healing;
                    healthToRegenSecond -= healing;
                }
            }

            resetHUD--;
            if (resetHUD <= 0)
                resetHUD = 600;
            if (globalSingleSecondTimer == 0)
                globalSingleSecondTimer = 60;
            globalSingleSecondTimer--;
            
            if (ability1Cooldown > 0)
                ability1Cooldown--;
            if(ability2Cooldown > 0)
                ability2Cooldown--;

            pSecHealthTimer--;
            inBattleTimer--;
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

            if (vanguardShieldCurrentDuration > 0)
            {
                vanguardShieldUp = true;
                vanguardShieldCurrentDuration--;
                Player.endurance += vanguardShieldDamageReduction;
            }
            else
            {
                vanguardShieldUp = false;
            }

            if (vanguardTalent_3 == "L")
                vanguardShieldBaseDamageReduction += .05f;
            else if (vanguardTalent_3 == "R")
                vanguardSpearBaseDamage += 18;
            if (vanguardTalent_4 == "R")
                vanguardShieldBaseDuration += 30;
            if (vanguardTalent_6 == "R" || vanguardTalent_6 == "B")
                vanguardPassiveReflectAmount += .35f;
            if (vanguardTalent_8 == "R")
                vanguardShieldBaseDuration += 48;
            if (vanguardTalent_8 == "L" || vanguardTalent_8 == "B")
                vanguardSwordBaseDamage += 28;
            else if (vanguardTalent_10 == "R" || vanguardTalent_10 == "B")
                vanguardUltimateBossExecute += .04f;

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

                if(vanguardShieldRegen)
                    vanguardShieldRegenTimer++;
                if(vanguardShieldRegenTimer == 60)
                {
                    vanguardShieldRegenTimer = 0;
                    Player.statLife += (int)(Player.statLifeMax2 * .012f * healingPower);
                    Player.HealEffect((int)(Player.statLifeMax2 * .012f * healingPower));
                    for (int x = 0; x < 3; x++)
                        Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                }
            }
            else
            {
                vanguardDustFlag = false;
                vanguardDustTimer = 5;
                vanguardDustLocations = 1;
            }

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

                if (scoutColaCurDuration > 0)
                {
                    var dustScout = Dust.NewDustDirect(Player.position, Player.width, Player.height, 63, Player.velocity.X * .5f, Player.velocity.Y * .5f, Scale: 1.5f);
                    dustScout.noGravity = true;
                    dustScout.noLight = true;

                    if(scoutColaCurDuration == 1)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(12f, 12f);
                            var dust = Dust.NewDustPerfect(Player.Center, 63, speed, Scale: 1.5f);
                            dust.noGravity = true;
                        }

                        CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Poof!", true);
                    }
                }
            }

            if (soulmancerConsumeDuration_Cur > 0)
                soulmancerConsumeDuration_Cur--;
            if(soulmancerConsumeDuration_Cur > 0)
            {
                // Circle Dust
                Vector2 origin = Player.Center;
                //origin.X += 8;
                //origin.X -= Player.width / 2;
                float radius = 75;

                int locations = 20;
                for (int i = 0; i < locations; i++)
                {
                    Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                    var dust = Dust.NewDustPerfect(position, 63, Vector2.Zero, 0, Color.Red, 2f);
                    dust.noGravity = true;
                    dust.noLight = true;
                }

                soulmancerSoulRipChance += 5;
            }

            if (soulmancerSacrificeTimer > 0)
                soulmancerSacrificeTimer--;

            if (soulmancerSacrificeTimer == 0 && soulmancerSacrificeSoulCount_Cur > 0)
            {
                soulmancerSacrificeSoulCount_Cur--;
                soulmancerSacrificeTimer = 3;

                Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                speed.Normalize();
                speed *= 15f;
                Projectile.NewProjectile(default, Player.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragment>(), (int)(soulmancerSoulRipDamage * abilityPower * 1.5f), 0, Player.whoAmI);

                SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, Player.position);

                int healthCost = (int)(Player.statLifeMax2 * soulmancerSacrificeHealthCost);
                if (healthCost < 1)
                    healthCost = 1;
                Player.statLife -= healthCost;
                Player.HealEffect(-healthCost);
            }

            #region Global Skill Points Spent
            if (equippedClass == "Blood Mage")
                spentSkillPointsGlobal = bloodMageSpentSkillPoints;
            if (equippedClass == "Commander")
                spentSkillPointsGlobal = commanderSpentSkillPoints;
            if (equippedClass == "Scout")
                spentSkillPointsGlobal = scoutSpentSkillPoints;
            if (equippedClass == "Vanguard")
                spentSkillPointsGlobal = vanguardSpentSkillPoints;
            if (equippedClass == "Soulmancer")
                spentSkillPointsGlobal = soulmancerSpentSkillPoints;
            #endregion

            base.PreUpdate();
        }

        public override void PostUpdateBuffs()
        {
            #region Cards Stats
            lifeMult += card_HealthyCount * card_HealthyValue;
            Player.endurance += card_ImpenetrableCount * card_ImpenetrableValue;
            Player.GetAttackSpeed(DamageClass.Generic) += card_NimbleHandsCount * card_NimbleHandsValue;
            Player.GetDamage(DamageClass.Generic) += card_CarryCount * card_CarryValue;
            critDamageMult += card_DeadeyeCount * card_DeadeyeValue;
            dodgeChance += card_SneakyCount * card_SneakyValue;
            abilityPower += card_PowerfulCount * card_PowerfulValue;
            cooldownReduction -= card_TimelessCount * card_TimelessValue;
            ultCooldownReduction -= card_MightyCount * card_MightyValue;
            healingPower += card_MendingCount * card_MendingValue;
            if(hasClass) classStatMultiplier += card_MagicalCount * card_MagicalValue;

            lifeMult += card_VeteranCount * card_VeteranValue_1;
            manaMult += card_VeteranCount * card_VeteranValue_2;

            lifeMult += card_FortifiedCount * card_FortifiedValue_1;
            Player.endurance += card_FortifiedCount * card_FortifiedValue_2;

            Player.GetDamage(DamageClass.Generic) += card_FerociousCount * card_FerociousValue_1;
            Player.GetAttackSpeed(DamageClass.Generic) += card_FerociousCount * card_FerociousValue_2;

            critDamageMult += card_ProwlerCount * card_ProwlerValue_2;
            Player.GetDamage(DamageClass.Generic) += card_ProwlerCount * card_ProwlerValue_1;

            critDamageMult += card_MischievousCount * card_MischievousValue_1;
            dodgeChance += card_MischievousCount * card_MischievousValue_2;

            abilityPower += card_MasterfulCount * card_MasterfulValue_1;
            healingPower += card_MasterfulCount * card_MasterfulValue_2;

            abilityPower += card_SeerCount * card_SeerValue_1;
            ultCooldownReduction -= card_SeerCount * card_SeerValue_2;

            healingPower += card_HealerCount * card_HealerValue_1;
            cooldownReduction -= card_HealerCount * card_HealerValue_2;

            cooldownReduction -= card_SparkOfGeniusCount * card_SparkOfGeniusValue_1;
            ultCooldownReduction -= card_SparkOfGeniusCount * card_SparkOfGeniusValue_2;
            #endregion

            base.PostUpdateBuffs();
        }


        // Class specific upgrades here work but do not show up on the class menu
        public override void UpdateEquips()
        {
            if (hasBloodMage)
            {
                if (bloodMageTalent_2 == "R")
                    bloodMageEnchantmentBaseManaCost -= .13f;
                if (bloodMageTalent_8 == "L")
                    bloodMageEnchantmentBaseManaCost -= .11f;
                if (bloodMageTalent_7 == "L")
                    bloodMagePassiveChance += .05f;
            }

            if (hasCrusader)
            {
                if (crusaderTalent_2 == "L")
                    crusaderEndurance += .01f;
                if (crusaderTalent_2 == "R")
                    crusaderHammerDamageBase += 18;
                if (crusaderTalent_5 == "L")
                    crusaderEndurance += .01f;
                if (crusaderTalent_5 == "R")
                    crusaderGuardianAngelDuration += 60;
                if (crusaderTalent_6 == "R")
                    crusaderGuardianAngelEndurance -= .05f;
                if (crusaderTalent_8 == "L")
                    crusaderHammerDamageBase += 45;
                if (crusaderTalent_9 == "R")
                {
                    crusaderHammerDamageBase += 24;
                    crusaderHammerVelocityMult += .15f;
                }
                if (crusaderTalent_10 == "R")
                    crusaderGuardianAngelDuration += 180;
                if (crusaderTalent_10 == "L")
                    crusaderHealing += .025f;
            }

            if (equippedClass == "Gambler")
            {
                if (gamblerTalent_1 == "L")
                {
                    gamblerPassiveMinDmg += .025f;
                    gamblerPassiveMaxDmg += .025f;
                }
                if (gamblerTalent_1 == "R")
                    gamblerDiceDamageBase += 8;

                if (gamblerTalent_3 == "L")
                    gamblerPassiveBoostMaxDuration += 60;

                if (gamblerTalent_4 == "L")
                    gamblerPassiveBoostDamage += .08f;

                if (gamblerTalent_5 == "L")
                    gamblerDiceCount += 2;

                if (gamblerTalent_6 == "L")
                {
                    gamblerPassiveMinDmg += .025f;
                    gamblerPassiveMaxDmg += .025f;
                }

                // gambler T_7L done in Buffs.LuckyStreak
                if (gamblerTalent_7 == "R")
                {
                    gamblerPassiveMinDmg += .05f;
                    gamblerPassiveMaxDmg += .05f;
                }

                if (gamblerTalent_8 == "L")
                    gamblerDiceDamageBase += 23;

                if (gamblerTalent_9 == "L")
                    gamblerUltDurationBase += 120;
                if (gamblerTalent_9 == "R")
                    gamblerPassiveBoostDamage += .1f;

                if (gamblerTalent_10 == "L")
                    gamblerPassiveBoostMaxDuration += 180;
                if (gamblerTalent_10 == "R")
                    gamblerUltAttackSpeed += .15f;
            }

            if (equippedClass == "Plague")
            {
                if (plagueTalent_1 == "L")
                    abilityPower += .04f;
                if (plagueTalent_1 == "R")
                    plaguePassivePlaguedDamageBase += .02f;
                //
                if (plagueTalent_2 == "L")
                    plagueInsectDamageBase += 10;
                if (plagueTalent_2 == "R")
                    plagueInfectionRange += 50;
                //
                if (plagueTalent_3 == "L")
                    minionCritChance += .01f;
                if (plagueTalent_3 == "R")
                    cooldownReduction -= .07f;
                //
                if (plagueTalent_4 == "R")
                    plagueInfectionBurstBase += 30;
                //
                if (plagueTalent_5 == "L")
                    cooldownReduction -= .05f;
                if (plagueTalent_5 == "R")
                    Player.GetDamage(DamageClass.Summon) += .02f;
                //
                if (plagueTalent_6 == "L")
                    minionCritChance += .01f;
                if (plagueTalent_6 == "R")
                    plagueInfectionRange += 50;
                //
                if (plagueTalent_7 == "R")
                    plaguePassivePlaguedDamageBase += .02f;
                //
                if (plagueTalent_8 == "L")
                    minionCritChance += .01f;
                if (plagueTalent_8 == "R")
                    lifeMult += .075f;
                //
                if (plagueTalent_9 == "L")
                    abilityPower += .15f;
                if (plagueTalent_9 == "R")
                    plagueDebuffDuration += 2; //In seconds
                //10th Right is done on the ability cast itself
            }

            base.UpdateEquips();
        }

        public override void PostUpdateEquips()
        {
            if (hasClass)
                if (hasScalingWarbanner)
                {
                    classStatMultiplier += .1f;
                    if (Main.hardMode)
                        classStatMultiplier += .08f;
                }

            //globalLevel = defeatedBosses.Count;

            #region Vanguard
            vanguardLevel = vanguardDefeatedBosses.Count;
            if (vanguardLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                vanguardLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasVanguard)
            {
                #region Vanguard Talents
                if (vanguardTalent_1 == "L" || vanguardTalent_1 == "B")
                    Player.statDefense += 2;
                else if (vanguardTalent_1 == "R" || vanguardTalent_1 == "B")
                    cooldownReduction -= .06f;

                if (vanguardTalent_2 == "L" || vanguardTalent_2 == "B")
                    lifeMult += .015f;
                else if (vanguardTalent_2 == "R" || vanguardTalent_2 == "B")
                    Player.GetDamage(DamageClass.Melee) += .01f;

                if (vanguardTalent_4 == "L" || vanguardTalent_4 == "B")
                    Player.statDefense += 4;

                if (vanguardTalent_5 == "L" || vanguardTalent_5 == "B")
                    lifeMult += .02f;
                else if (vanguardTalent_5 == "R" || vanguardTalent_5 == "B")
                    ultCooldownReduction -= .08f;

                if (vanguardTalent_7 == "L" || vanguardTalent_7 == "B")
                    Player.GetCritChance(DamageClass.Melee) += 2;
                else if (vanguardTalent_7 == "R" || vanguardTalent_7 == "B")
                    vanguardSpearHeal = true;

                if (vanguardTalent_9 == "L" || vanguardTalent_9 == "B")
                    vanguardShieldRegen = true;
                else if (vanguardTalent_9 == "R" || vanguardTalent_9 == "B")
                    cooldownReduction -= .17f;

                if (vanguardTalent_10 == "L" || vanguardTalent_10 == "B")
                    lifeMult += .08f;

                lifeMult += talentSinkVanguardLeft * talentSinkVanguardLeftValue;
                Player.GetDamage(DamageClass.Melee) += talentSinkVanguardRight * talentSinkVanguardRightValue;
                #endregion

                vanguardSpearDamage = vanguardSpearBaseDamage + 10 * vanguardLevel;
                vanguardShieldDuration = vanguardShieldBaseDuration + vanguardShieldDurationPerLevel * vanguardLevel;
                vanguardSwordDamage = vanguardSwordBaseDamage + 9 * vanguardLevel;
                vanguardShieldDamageReduction += vanguardShieldBaseDamageReduction;

                //Player.endurance += vanguardShieldDamageReduction;
            }
            #endregion

            #region Blood Mage
            bloodMageLevel = bloodMageDefeatedBosses.Count;
            if (bloodMageLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                bloodMageLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasBloodMage)
            {
                #region Blood Mage Talents
                if (bloodMageTalent_1 == "R" || bloodMageTalent_1 == "B")
                    bloodMageSiphonHealMax += .05f;
                if (bloodMageTalent_1 == "L" || bloodMageTalent_1 == "B")
                    cooldownReduction -= .04f;

                if (bloodMageTalent_2 == "L" || bloodMageTalent_2 == "B")
                    bloodMageBaseDamageGain += .02f;

                if (bloodMageTalent_3 == "L" || bloodMageTalent_3 == "B")
                    lifeMult += .05f;
                else if (bloodMageTalent_3 == "R" || bloodMageTalent_3 == "B")
                    cooldownReduction -= .1f;

                if (bloodMageTalent_4 == "L" || bloodMageTalent_4 == "B")
                    ultCooldownReduction -= .1f;
                else if (bloodMageTalent_4 == "R" || bloodMageTalent_4 == "B")
                    bloodMageBasePassiveWeaponDamageMult += .05f;

                if (bloodMageTalent_5 == "L" || bloodMageTalent_5 == "B")
                    bloodMageSiphonBaseDamage += 35;
                else if (bloodMageTalent_5 == "R" || bloodMageTalent_5 == "B")
                    ultCooldownReduction -= .04f;

                if (bloodMageTalent_6 == "L" || bloodMageTalent_6 == "B")
                    manaMult += .18f;
                else if (bloodMageTalent_6 == "R" || bloodMageTalent_6 == "B")
                    Player.GetCritChance(DamageClass.Magic) += 2;

                if (bloodMageTalent_7 == "R" || bloodMageTalent_7 == "B")
                    bloodMageBaseDamageGain += .03f;

                if (bloodMageTalent_8 == "R" || bloodMageTalent_8 == "B")
                    ultCooldownReduction -= .04f;

                if (bloodMageTalent_9 == "L" || bloodMageTalent_9 == "B")
                    bloodMagePassiveHealing = true;
                else if (bloodMageTalent_9 == "R" || bloodMageTalent_9 == "B")
                    bloodMageBaseUltRegen += .004f;

                if (bloodMageTalent_10 == "L" || bloodMageTalent_10 == "B")
                    cooldownReduction -= .12f;
                else if (bloodMageTalent_10 == "R" || bloodMageTalent_10 == "B")
                    bloodMageUltTicks += 3;

                abilityPower += talentSinkBloodMageLeft * talentSinkBloodMageLeftValue;
                healingPower += talentSinkBloodMageRight * talentSinkBloodMageRightValue;
                #endregion

                bloodMageUltRegen = bloodMageBaseUltRegen + .0008f * bloodMageLevel;
                bloodMageSiphonDamage = bloodMageSiphonBaseDamage + 6 * bloodMageLevel;
                bloodMageDamageGain += bloodMageBaseDamageGain * bloodMageLevel;
                bloodMageBasePassiveWeaponDamageMult += bloodMagePassiveDamageLevel * bloodMageLevel;
            }
            #endregion

            #region Commander
            commanderLevel = commanderDefeatedBosses.Count;
            if (commanderLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                commanderLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasCommander)
            {
                #region Commander Talents
                if (commanderTalent_1 == "L" || commanderTalent_1 == "B")
                    commanderBannerDamage += .03f;
                else if (commanderTalent_1 == "R" || commanderTalent_1 == "B")
                    Player.whipRangeMultiplier += .04f;

                if (commanderTalent_2 == "L" || commanderTalent_2 == "B")
                    commanderBannerRange = (int)(commanderBannerRange * 1.28f);
                else if (commanderTalent_2 == "R" || commanderTalent_2 == "B")
                    cooldownReduction -= .15f;

                if (commanderTalent_3 == "L" || commanderTalent_3 == "B")
                    commanderPassiveEndurance += .002f;
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
                    Player.whipRangeMultiplier += .08f;

                if (commanderTalent_9 == "L" || commanderTalent_9 == "B")
                    ultCooldownReduction -= .1f;
                else if (commanderTalent_9 == "R" || commanderTalent_9 == "B")
                    commanderPassiveEndurance += .002f;

                if (commanderTalent_10 == "L" || commanderTalent_10 == "B")
                    Player.whipRangeMultiplier += .05f;
                else if (commanderTalent_10 == "R" || commanderTalent_10 == "B")
                    commanderBannerEndurance -= .15f;

                cooldownReduction -= talentSinkCommanderLeft * talentSinkCommanderLeftValue;
                ultCooldownReduction -= talentSinkCommanderRight * talentSinkCommanderRightValue;
                #endregion


            Player.endurance += Player.maxMinions * commanderPassiveEndurance;
            commanderBannerDuration += 24 * commanderLevel;
            commanderUltDuration += 15 * commanderLevel;
            commanderCryDamage += commanderCryDamageLevel * commanderLevel;
            }
            #endregion

            #region Scout
            scoutLevel = scoutDefeatedBosses.Count;
            if (scoutLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                scoutLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasScout)
            {
                #region Scout Talents
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

                lifeMult += talentSinkScoutLeft * talentSinkScoutLeftValue;
                ultCooldownReduction -= talentSinkScoutRight * talentSinkScoutRightValue;
                #endregion

                #region Specs
                scoutColaDuration += (int)(specScout_ColaDuration * specScout_ColaDurationBase);
                cooldownReduction -= specScout_CooldownReduction * specScout_CooldownReductionBase;
                dodgeChance += specScout_Dodge * specScout_DodgeBase;
                scoutTrapBaseDamage += (int)(specScout_TrapDamage * specScout_TrapDamageBase);
                ultCooldownReduction -= specScout_UltCost * specScout_UltCostBase;
                #endregion

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

                scoutTrapDamage += scoutTrapDamageLevel * scoutLevel;
                scoutUltSpeed += scoutUltSpeedLevel * scoutLevel;
            }
            #endregion

            #region Soulmancer
            soulmancerLevel = soulmancerDefeatedBosses.Count;
            if (soulmancerLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                soulmancerLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasSoulmancer)
            {
                #region Soulmancer Talents
                if (soulmancerTalent_1 == "L")
                    soulmancerSoulRipChance_Base += .01f;
                if (soulmancerTalent_1 == "R")
                    abilityPower += .1f;

                if (soulmancerTalent_2 == "L")
                    cooldownReduction -= .06f;
                if (soulmancerTalent_2 == "R")
                    Player.GetCritChance(DamageClass.Magic) += 2;

                if (soulmancerTalent_3 == "L")
                    ultCooldownReduction -= .06f;
                if (soulmancerTalent_3 == "R")
                    soulmancerSoulRipChance_Base += .02f;

                if (soulmancerTalent_4 == "L")
                    soulmancerSoulRipDamage_Base += 11;
                if (soulmancerTalent_4 == "R")
                    soulmancerSoulShatterRange += 65;

                if (soulmancerTalent_5 == "L")
                    ultCooldownReduction -= .04f;
                if (soulmancerTalent_5 == "R")
                    soulmancerSoulShatterDamage_Base += 24;

                if (soulmancerTalent_6 == "L")
                    soulmancerConsumeDuration_Base += 60;
                if (soulmancerTalent_6 == "R")
                    soulmancerSoulRipDamage_Base += 12;

                if (soulmancerTalent_7 == "L")
                    soulmancerSoulRipChance_Base += .02f;
                if (soulmancerTalent_7 == "R")
                    Player.GetCritChance(DamageClass.Magic) += 2;

                if (soulmancerTalent_8 == "L")
                    soulmancerSoulShatterRange += 95;
                if (soulmancerTalent_8 == "R")
                    soulmancerSoulRipChance_Base += .03f;

                if (soulmancerTalent_9 == "L")
                    abilityPower += .16f;
                if (soulmancerTalent_9 == "R")
                    cooldownReduction -= .08f;

                if (soulmancerTalent_9 == "L")
                    soulmancerSoulRipChance_Base += .04f;
                if (soulmancerTalent_9 == "R")
                    soulmancerConsumeDuration_Base += 60;

                abilityPower += talentSinkSoulmancerLeft * talentSinkSoulmancerLeftValue;
                Player.GetDamage(DamageClass.Magic) += talentSinkSoulmancerRight * talentSinkSoulmancerRightValue;
                #endregion

                #region Specs
                soulmancerSoulRipChance_Base += specSoulmancer_SoulRipChance * specSoulmancer_SoulRipChanceBase;
                abilityPower += specSoulmancer_AbilityPower * specSoulmancer_AbilityPowerBase;
                Player.GetAttackSpeed(DamageClass.Magic) += specSoulmancer_MagicAttackSpeed * specSoulmancer_MagicAttackSpeedBase;
                cooldownReduction -= specSoulmancer_CooldownReduction * specSoulmancer_CooldownReductionBase;
                soulmancerConsumeDuration += specSoulmancer_ConsumeDuration * specSoulmancer_ConsumeDurationBase;
                #endregion

                soulmancerSoulRipDamage += soulmancerSoulRipDamage_Base + soulmancerSoulRipDamage_PerLevel * soulmancerLevel;
                soulmancerSoulRipChance += soulmancerSoulRipChance_Base + soulmancerSoulRipChance_PerLevel * soulmancerLevel;
                soulmancerConsumeHeal += soulmancerConsumeHeal_Base;
                soulmancerConsumeDuration += soulmancerConsumeDuration_Base + soulmancerConsumeDuration_PerLevel * soulmancerLevel;
                soulmancerSoulShatterDamage += soulmancerSoulShatterDamage_Base + soulmancerShatterDamage_PerLevel * soulmancerLevel;
                //soulmancerSacrificeSoulCount += soulmancerSacrificeSoulCount_Base;
                soulmancerSacrificeHealthCost += soulmancerSacrificeHealthCost_Base;
            }
            #endregion

            #region Crusader
            crusaderLevel = crusaderDefeatedBosses.Count;
            if (crusaderLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                crusaderLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasCrusader)
            {
                if (crusaderTalent_1 == "L")
                    cooldownReduction -= .02f;
                if (crusaderTalent_1 == "R")
                    ultCooldownReduction -= .03f;
                if (crusaderTalent_3 == "L")
                    crusaderHammerVelocityMult += .3f;
                if (crusaderTalent_3 == "R")
                    lifeMult += .02f;
                if (crusaderTalent_4 == "L")
                    Player.GetDamage(DamageClass.Melee) += .02f;
                if (crusaderTalent_4 == "R")
                    ultCooldownReduction -= .03f;
                if (crusaderTalent_6 == "L")
                    crusaderHammerVelocityMult += .35f;
                if (crusaderTalent_7 == "L")
                    Player.GetCritChance(DamageClass.Melee) += 2;
                if (crusaderTalent_7 == "R")
                    healingPower += .03f;

                if (crusaderTalent_8 == "R")
                    pSecHealthRegen += Player.statLifeMax2 * .001f;
                if (crusaderTalent_9 == "L")
                    ultCooldownReduction -= .04f;

                abilityPower += talentSinkCrusaderLeft * talentSinkCrusaderLeftValue;
                healingPower += talentSinkCrusaderRight * talentSinkCrusaderRightValue;

                crusaderHammerDamage = crusaderHammerDamageBase + crusaderHammerDamageLevel * crusaderLevel;
                crusaderGuardianAngelEndurance -= crusaderGuardianAngelEnduranceLevel * crusaderLevel;
            }

            
            #endregion

            #region Gambler
            gamblerLevel = gamblerDefeatedBosses.Count;
            if (gamblerLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                gamblerLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (equippedClass == "Gambler")
            {
                if (gamblerTalent_2 == "L")
                    dodgeChance += .02f;
                if (gamblerTalent_2 == "R")
                    ultCooldownReduction -= .08f;

                if (gamblerTalent_3 == "R")
                    abilityPower += .16f;

                if (gamblerTalent_4 == "R")
                    Player.GetCritChance(DamageClass.Ranged) += 2;

                if (gamblerTalent_5 == "R")
                    dodgeChance += .03f;
                
                if (gamblerTalent_6 == "R")
                    cooldownReduction -= .1f;

                if (gamblerTalent_8 == "R")
                    dodgeChance += .04f;

                if (gamblerUlt)
                {
                    Player.GetAttackSpeed(DamageClass.Generic) += gamblerUltAttackSpeed;
                    ability1cdr -= gamblerUltCooldownReduction;
                }

                cooldownReduction -= talentSinkGamblerLeft * talentSinkGamblerLeftValue;
                Player.GetDamage(DamageClass.Ranged) += talentSinkGamblerRight * talentSinkGamblerRightValue;

                gamblerPassiveMinDmg += gamblerPassiveMinDmgPerLevel * gamblerLevel;
                gamblerPassiveMaxDmg += gamblerPassiveMinDmgPerLevel * gamblerLevel;
                gamblerDiceDamage += gamblerDiceDamageBase + gamblerDiceDamagePerLevel * gamblerLevel;
                gamblerPassiveBoostDamage += gamblerPassiveBoostPerLevel * gamblerLevel;
                gamblerUltCooldownReduction += gamblerUltCooldownReductionPerLevel * gamblerLevel;
                gamblerUltAbilityPower += gamblerUltAbilityPowerPerLevel * gamblerLevel;
            }
            #endregion

            #region Plague
            plagueLevel = plagueDefeatedBosses.Count;
            if (plagueLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                plagueLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if(equippedClass == "Plague")
            {
                if (plagueTalent_4 == "L")
                    plagueDeadzoneRange += 50;
                if (plagueTalent_7 == "L")
                    plagueDeadzoneRange += 50;
                if (plagueTalent_10 == "L")
                    plagueDeadzoneStrikeInterval = 10;
                //10th Right is done on the ability cast itself

                //Sinks
                abilityPower += talentSinkPlagueLeft * talentSinkPlagueLeftValue;
                minionCritChance += talentSinkPlagueRight * talentSinkPlagueRightValue;

                minionCritChance += plaguePassiveCritBase;

                plaguePassivePlaguedDamage += plaguePassivePlaguedDamageBase;
                plaguePassivePlaguedDamage += plaguePassivePlaguedPerLevel * plagueLevel;

                plagueInsectDamageTotal += plagueInsectDamageBase;
                plagueInsectDamageTotal += plagueInsectDamagePerLevel * plagueLevel;

                plagueInfectionBurstTotal += plagueInfectionBurstBase;
                plagueInfectionBurstTotal += plagueInfectionBurstPerLevel * plagueLevel;

                plagueDeadzoneDamage += plagueDeadzoneBaseDamage;
                plagueDeadzoneDamage += plagueDeadzoneDamagePerLevel * plagueLevel;

                //Plague ult
                _plagueDeadzoneDurationCurrent--;
                if (_plagueDeadzoneDurationCurrent > 0)
                {
                    float q = (float)_plagueDeadzoneDurationCurrent / (float)plagueDeadzoneDuration;
                    // Circle Dust
                    Vector2 origin = Player.Center;
                    float radius = plagueDeadzoneRange;
                    Vector2 origin2 = Player.Center;
                    float radius2 = plagueDeadzoneRange * -q;

                    int locations = 48;
                    for (int j = 0; j < locations; j++)
                    {
                        //Range Circle
                        Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * j)) * radius;
                        if (!hasAghanims && !hasAghanimsShard)
                        {
                            if (Collision.CanHitLine(Player.Center, 1, 1, position, 1, 1))
                            {
                                var dust = Dust.NewDustPerfect(position, 61, Vector2.Zero, 0, Color.White, 1.5f);
                                dust.noGravity = true;
                                dust.noLight = true;
                                //dust.velocity = Player.velocity;
                            }
                        }
                        else
                        {
                            var dust = Dust.NewDustPerfect(position, 61, Vector2.Zero, 0, Color.White, 1.5f);
                            dust.noGravity = true;
                            dust.noLight = true;
                            //dust.velocity = Player.velocity;
                        }

                    }

                    for (int j = 0; j < 8; j++)
                    {
                        //Duration circle
                        Vector2 position2 = origin2 + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 8 * j)) * radius2;
                        var dust2 = Dust.NewDustPerfect(position2, 6, Vector2.Zero, 0, Color.White, 1f);
                        dust2.noGravity = true;
                        dust2.noLight = true;
                    }
                }
                if (_plagueDeadzoneDurationCurrent > 0 && globalSingleSecondTimer % plagueDeadzoneStrikeInterval == 0)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (Vector2.Distance(Player.Center, Main.npc[i].Center) <= plagueDeadzoneRange && !Main.npc[i].townNPC && !Main.npc[i].dontTakeDamage && Main.npc[i].type != NPCID.DD2Bartender && Main.npc[i].type != NPCID.DD2EterniaCrystal && Main.npc[i].type != NPCID.DD2LanePortal && !Main.npc[i].friendly)
                        {
                            int dmg = (int)(plagueDeadzoneDamage * abilityPower);
                            if (Main.npc[i].realLife > 0) dmg /= 4; //damage reduced by 75% vs worm enemies

                            if (!hasAghanims && !hasAghanimsShard)
                            {
                                if (Collision.CanHitLine(Player.Center, 1, 1, Main.npc[i].Center, 1, 1))
                                {
                                    Player.StrikeNPCDirect(Main.npc[i], new NPC.HitInfo
                                    {
                                        Damage = dmg
                                    });
                                }
                            }
                            else
                            {
                                Player.StrikeNPCDirect(Main.npc[i], new NPC.HitInfo
                                {
                                    Damage = dmg
                                });
                                Main.npc[i].AddBuff(BuffType<Buffs.Plague.PlagueInfection>(), 60 * plagueDebuffDuration / 5); //20% duration
                            }
                        }
                    }
                }
            }
            #endregion

            #region Inventor
            if (inventorOverclockCurDuration > 0)
                Player.GetAttackSpeed(DamageClass.Generic) += .25f;
            #endregion

            #region Ability Ready Sounds
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
            #endregion

            //Shard stats only
            if (hasAghanimsShard && !hasAghanims)
            {
                if (hasBloodMage)
                {
                    abilityPower += .15f;
                    cooldownReduction -= .05f;
                }

                if (hasCommander)
                {
                    ability1MaxCooldown -= 5;
                    commanderBannerRange += 25;
                    bannerFollowsPlayer = true;
                }

                if (hasVanguard)
                {
                    vanguardPassiveReflectAmount += .65f;
                    Player.endurance += .04f;
                }

                if (hasScout)
                    scoutUltInvDuration += 60;

                if (hasSoulmancer)
                {
                    soulmancerSoulShatterRange -= 175;
                    abilityPower += .06f;
                    soulmancerSoulShatterCastTarget = Main.MouseWorld;
                }

                if (hasCrusader)
                {
                    abilityPower += .1f;
                    cooldownReduction -= .1f;
                    healingPower += .1f;
                    ultCooldownReduction -= .1f;
                }

                if (equippedClass == "Gambler")
                {
                    gamblerDiceAghanimsHeal = true;
                    gamblerDiceDamageBase += 12;
                    gamblerPassiveBoostMaxDuration += 60;
                }

                if (equippedClass == "Plague")
                {
                    plagueDeadzoneRange += 100;
                    ultCooldownReduction -= .1f;
                }
            }

            if (hasDarkSign)
                lifeMult -= .15f;

            if (hasMajorsCare)
                lifeMult += .05f;

            if (hasPocketSlime)
                lifeMult -= .05f;

            if (hasPeanut)
                lifeMult -= .16f;

            if (hasOldBlood)
                defenseMult -= .1f;

            if(hasLuckyLeaf)
                lifeMult -= .15f;

            if (hasChaosAccelerant)
            {
                lifeMult -= .4f;
                manaMult -= .4f;
            }

            if (hasTearsOfLife)
                lifeMult -= .03f;

            if (hasSqueaker)
                lifeMult -= .8f;

            if (hasFlanPudding) //Chocolate Pudding
                lifeMult += .03f;

            if (hasPorcelainMask)
                defenseMult -= .25f;

            if (hasWindsRoar)
                lifeMult += .02f;

            if (hasScout)
                lifeMult -= scoutLevel * .0035f * _ACMConfigServer.Instance.classStatMultNegative;

            if(equippedClass == "Plague")
                lifeMult -= plagueLevel * .006f * _ACMConfigServer.Instance.classStatMultNegative;

            ultChargeMax = (int)(ultChargeMax * ultCooldownReduction);
            if (ultChargeMax < 60)
                ultChargeMax = 60;
            if (ultCharge > ultChargeMax)
                ultCharge = ultChargeMax;

            if (pSecHealthTimer == 0)
            {
                if (!Player.dead)
                {
                    if (pSecHealthRegen > 0 && pSecHealthRegen < 1) pSecHealthRegen = 1;
                    Player.statLife += (int)pSecHealthRegen;
                }

                if(hasManaBag)
                {
                    int manaToRegen = (int)(Player.statManaMax2 * .02f);
                    if(Player.statMana < Player.statManaMax2)
                        Player.statMana += manaToRegen;
                }

                if (bloodMageCurUltTicks > 0)
                {
                    int heal = (int)(Player.statLifeMax2 * bloodMageUltRegen * healingPower);
                    HealPlayer(1, 3, heal);
                    bloodMageCurUltTicks--;
                    //Main.NewText($"{bloodMageCurUltTicks}/{bloodMageUltTicks}");
                }
                pSecHealthTimer = 60;
            }

            if (cooldownReduction < 0f)
                cooldownReduction = 0f;

            if (hasScout && scoutColaCurDuration > 0)
                if (hasAghanims || hasAghanimsShard)
                    Player.GetCritChance(DamageClass.Ranged) += 15;

            if (hasSoulmancer)
            {
                if (hasAghanims || hasAghanimsShard)
                    soulmancerSoulShatterCastTarget = Main.MouseWorld;
                else
                    soulmancerSoulShatterCastTarget = Player.Center;
            }

            if (chocolateBarTimer == 0)
            {
                Player.statLife += (int)(chocolateBarStoredHealth + Player.statLifeMax2 * .025f);
                Player.HealEffect((int)(chocolateBarStoredHealth + Player.statLifeMax2 * .025f));
                chocolateBarStoredHealth = 0;

                for (int x = 0; x < 4; x++)
                    Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
            }

            if (globalSingleSecondTimer % 6 == 0 && _plagueInsectsToSpawn > 0) 
            {
                Vector2 PointToCursor = Main.MouseWorld - Player.position;
                PointToCursor.Normalize();
                Projectile.NewProjectile(null, Player.position, PointToCursor.RotatedByRandom(MathHelper.ToRadians(70f)), ProjectileType<Projectiles.Plague.PlagueInsect>(), (int)(plagueInsectDamageTotal * abilityPower), 0f, Player.whoAmI);
                _plagueInsectsToSpawn--;
                SoundEngine.PlaySound(SoundID.NPCDeath24, Player.Center);
            }

            //LAZER TARGETING NPC CLOSEST TO MOUSE, MAYBE GOOD FOR SOME ABILITIES IN THE FUTURE
            //if (globalSingleSecondTimer % 1 == 0 && lazerTaget != -1)
            //{
            //    if (Main.npc[lazerTaget].life > 0)
            //    {
            //        Main.npc[lazerTaget].SimpleStrikeNPC(1, 0, false, 0f);
            //        Player.statLife++;
            //        Player.HealEffect(1);
            //    }
            //    else
            //    lazerTaget = -1;
            //    Main.NewText(lazerTaget);
            //}

            base.PostUpdateEquips();
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            //LAZER TARGETING NPC CLOSEST TO MOUSE, MAYBE GOOD FOR SOME ABILITIES IN THE FUTURE
            //if(lazerTaget > -1)
            //    Utils.DrawLine(Main.spriteBatch, Player.Center, Main.npc[lazerTaget].Center, Color.LawnGreen, Color.Red, 3f);

            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }

        public override void PostUpdateMiscEffects()
        {
            Player.statLifeMax2 = (int)(Player.statLifeMax2 * lifeMult);
            Player.statDefense *= defenseMult;
            Player.statManaMax2 = (int)(Player.statManaMax2 * manaMult);

            base.PostUpdateMiscEffects();
        }

        public override void PostUpdate()
        {
            // RELIC LIST CODE WAS HERE ON 1.4.3, 1.4.4 BUGGED IT, PLACEHOLDER

            if (Main.netMode != NetmodeID.Server)
            {
                if(Player.whoAmI == Main.myPlayer)
                {
                    if (equippedClass != "" && GetInstance<ACM2ModSystem>()._HUD.CurrentState == null)
                        GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());

                    //if (resetHUD == 1 && equippedClass != "")
                    //{
                    //    GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                    //    GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
                    //}
                }
            }

            base.PostUpdate();
        }

        public override void OnRespawn()
        {
            if (Main.myPlayer == Player.whoAmI && Main.netMode != NetmodeID.Server)
            {
                GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
            }

            base.OnRespawn();
        }

        public override void UpdateLifeRegen()
        {
            //if(bloodMageBloodEnchantment)
            //    Player.lifeRegenTime = 0;
            //if (bloodMageBloodEnchantment && Player.lifeRegen > 0)
            //{
            //    Player.lifeRegen = 0;
            //    Player.lifeRegen -= 2;
            //}

            base.UpdateLifeRegen();
        }

        public override void UpdateBadLifeRegen()
        {
            //if (bloodMageBloodEnchantment)
            //    Player.lifeRegenTime = 0;
            //if (bloodMageBloodEnchantment && Player.lifeRegen > 0)
            //{
            //        Player.lifeRegen = 0;
            //        Player.lifeRegen -= 2;
            //}

            base.UpdateBadLifeRegen();
        }

        public override void OnEnterWorld()
        {
            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(new UI.ClassesMenu());
            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);

            if (!updatedRelicList)
            {
                for (int i = 0; i < ItemLoader.ItemCount; i++)
                {
                    Item newItem = new Item();
                    try
                    {
                        newItem.SetDefaults(i);
                    }
                    catch
                    {
                        newItem.netDefaults(i);
                    }

                    //ACMGlobalItem relicItem = newItem.GetGlobalItem<ACMGlobalItem>();
                    newItem.TryGetGlobalItem(out ACMGlobalItem relicItem);

                    if (relicItem != null)
                        if (relicItem.isRelic && !relicList.Contains(i) || relicItem.isCraftableRelic && !relicList.Contains(i))
                            relicList.Add(i);
                }
                updatedRelicList = true;
            }

            base.OnEnterWorld();
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                #region Press Key: Escape(Inventory) to close menus
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._ClassesMenu.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._RelicsUI.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._RelicsUI.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._BloodMageTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._BloodMageTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._CommanderTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._CommanderTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._ScoutTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._ScoutTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._VanguardTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._VanguardTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                #endregion

                if (ACM2.Menu.JustReleased)
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
            }

            if (Main.myPlayer == Player.whoAmI && !Player.dead)
            {
                if (ACM2.ClassAbility1.JustReleased && ability1Cooldown <= 0)
                {
                    Vector2 PointToCursor = Main.MouseWorld - Player.position;
                    PointToCursor.Normalize();

                    if (hasUnstableConcoction)
                        if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;


                    #region Relic Effects
                    if (ability1MaxCooldown > 1)
                    {
                        if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2)
                        {
                            int heal = (int)(Player.statLifeMax2 * .03f);
                            healthToRegenSlow += heal;
                            Player.HealEffect(heal);
                            for (int x = 0; x < 3; x++)
                                Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                        }
                    }
                    #endregion


                    switch (equippedClass)
                    {
                        case "Vanguard":
                            PointToCursor *= 21f;

                            if (vanguardSpearHeal && Player.statLife < Player.statLifeMax2)
                            {
                                Player.statLife += (int)(Player.statLifeMax2 * .026f * healingPower);
                                Player.HealEffect((int)(Player.statLifeMax2 * .026f * healingPower));
                                for (int x = 0; x < 3; x++)
                                    Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                            }


                            SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot);
                            Projectile.NewProjectile(null, new Vector2(Player.position.X + Player.width / 2, Player.position.Y), new Vector2(PointToCursor.X, PointToCursor.Y), ProjectileType<Projectiles.Vanguard.VanguardSpear>(), (int)(vanguardSpearDamage * abilityPower), 0, Player.whoAmI);

                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Spear Of Light!", true);
                            break;

                        case "Blood Mage":
                            PointToCursor.Normalize();
                            PointToCursor *= 18f;

                            SoundEngine.PlaySound(SoundID.Item21);
                            Projectile.NewProjectile(null, new Vector2(Player.position.X + Player.width / 2, Player.position.Y), new Vector2(PointToCursor.X, PointToCursor.Y), ProjectileType<Projectiles.BloodMage.Transfusion>(), (int)(bloodMageSiphonDamage * abilityPower), 0, Player.whoAmI);

                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Transfusion!", true);
                            break;

                        case "Commander":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Commander.WarBanner>(), 0, 0, Player.whoAmI);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "War Banner!", true);
                            if (Configs._ACMConfigServer.Instance.castingChatMessages && Main.netMode != NetmodeID.SinglePlayer)
                                SendMultiplayerSyncedChatMessage($"{Player.name} has used 'War Banner'!");
                            break;

                        case "Scout":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Hit-a-Soda!", true);
                            scoutColaCurDuration = scoutColaDuration;

                            SoundEngine.PlaySound(SoundID.Item3, Player.position);
                            break;

                        case "Soulmancer":
                            if (Player.statLife < Player.statLifeMax2 * .5f)
                            {
                                AddAbilityCooldown(1, ability1MaxCooldown);
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Consume!", true);
                                soulmancerConsumeDuration_Cur = soulmancerConsumeDuration;

                                SoundEngine.PlaySound(SoundID.DD2_WitherBeastAuraPulse, Player.position);
                            }
                            break;

                        case "Crusader":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Protection!", true);
                            if (Configs._ACMConfigServer.Instance.castingChatMessages && Main.netMode != NetmodeID.SinglePlayer)
                                SendMultiplayerSyncedChatMessage($"{Player.name} has used 'Protection'!");
                            int heal = (int)(Player.statLifeMax2 * crusaderHealing * healingPower);
                            HealPlayer(1, 5, heal);
                            SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact, Player.position);
                            break;

                        case "Inventor":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Deploy Sentry!", true);

                            for (int p = 0; p < Main.maxProjectiles; p++)
                            {
                                if (Player.ownedProjectileCounts[ProjectileType<Projectiles.Inventor.Sentry>()] > 0)
                                    if (Main.projectile[p].type == ProjectileType<Projectiles.Inventor.Sentry>() && Main.projectile[p].owner == Main.myPlayer)
                                        Main.projectile[p].Kill();
                            }
                            
                               
                            int xx = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
                            int yy = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
                            int bump = -32;
                            if (Player.gravDir == -1f)
                                yy = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
                            Player.FindSentryRestingSpot(ProjectileType<Projectiles.Inventor.Sentry>(), out xx, out yy, out bump);
                            Projectile.NewProjectile(null, xx, yy - 22, 0, 0, ProjectileType<Projectiles.Inventor.Sentry>(), 0, 0, Player.whoAmI);

                            SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, Player.position);
                            break;

                        case "Gambler":
                            PointToCursor.Normalize();
                            PointToCursor *= 14f;

                            AddAbilityCooldown(1, ability1MaxCooldown);

                            Main.NewText((int)(gamblerDiceDamage * abilityPower));

                            if(gamblerDiceCount >= 3)
                            {
                                Projectile.NewProjectile(null, Player.position, PointToCursor, ProjectileType<Projectiles.Gambler.Dice>(), (int)(gamblerDiceDamage * abilityPower), 0, Player.whoAmI);
                                Projectile.NewProjectile(null, Player.position, PointToCursor.RotatedBy(MathHelper.ToRadians(10f)), ProjectileType<Projectiles.Gambler.Dice>(), (int)(gamblerDiceDamage * abilityPower), 0, Player.whoAmI);
                                Projectile.NewProjectile(null, Player.position, PointToCursor.RotatedBy(MathHelper.ToRadians(-10f)), ProjectileType<Projectiles.Gambler.Dice>(), (int)(gamblerDiceDamage * abilityPower), 0, Player.whoAmI);
                            }
                            if (gamblerDiceCount >= 5)
                            {
                                Projectile.NewProjectile(null, Player.position, PointToCursor.RotatedBy(MathHelper.ToRadians(5f)), ProjectileType<Projectiles.Gambler.Dice>(), (int)(gamblerDiceDamage * abilityPower), 0, Player.whoAmI);
                                Projectile.NewProjectile(null, Player.position, PointToCursor.RotatedBy(MathHelper.ToRadians(-5f)), ProjectileType<Projectiles.Gambler.Dice>(), (int)(gamblerDiceDamage * abilityPower), 0, Player.whoAmI);
                            }

                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Roll the Dice!", true);
                            break;

                        case "Plague":
                            AddAbilityCooldown(1, ability1MaxCooldown);


                            _plagueInsectsToSpawn = plagueInsectsBase;

                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Infect!", true);
                            break;
                    }
                    a1Sound = false;
                }

                if (ACM2.ClassAbility2.JustReleased && ability2Cooldown <= 0)
                {
                    Vector2 PointToCursor = Main.MouseWorld - Player.position;
                    PointToCursor.Normalize();

                    if (hasUnstableConcoction && equippedClass != "Blood Mage")
                        if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;

                    #region Relic Effects
                    if (ability2MaxCooldown > 1)
                    {
                        if (hasScout)
                        {
                            if (Collision.CanHitLine(Player.Center, 1, 1, Main.MouseWorld, 1, 1))
                            {
                                if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2 /*&& equippedClass != "Blood Mage"*/)
                                {
                                    int heal = (int)(Player.statLifeMax2 * .03f);
                                    healthToRegenSlow += heal;
                                    Player.HealEffect(heal);
                                    for (int x = 0; x < 3; x++)
                                        Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                                }
                            }
                        }
                        else
                        {
                            if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2 /*&& equippedClass != "Blood Mage"*/)
                            {
                                int heal = (int)(Player.statLifeMax2 * .03f);
                                healthToRegenSlow += heal;
                                Player.HealEffect(heal);
                                for (int x = 0; x < 3; x++)
                                    Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                            }
                        }
                    }
                    #endregion

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
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment>(), 0, 0, Player.whoAmI);
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment2>(), 0, 0, Player.whoAmI);
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment3>(), 0, 0, Player.whoAmI);
                            }
                                
                            if (!bloodMageBloodEnchantment)
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Blood Enchantment: Off!", true);
                            else
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Blood Enchantment: On!", true);
                            break;

                        case "Commander":
                            Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Commander.BattleCry>(), 0, 0, Player.whoAmI);
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Battle Cry!", true);
                            break;

                        case "Scout":
                            if (Collision.CanHitLine(Player.Center, 1, 1, Main.MouseWorld, 1, 1))
                            {
                                AddAbilityCooldown(2, ability2MaxCooldown);
                                Projectile.NewProjectile(null, Main.MouseWorld, Vector2.Zero, ProjectileType<Projectiles.Scout.ScoutTrap>(), 0, 0, Player.whoAmI);
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Explosive Trap!", true);
                                SoundEngine.PlaySound(SoundID.Item37, Player.position);
                            }
                            break;

                        case "Soulmancer":
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Soul Shatter!", true);
                            if (hasAghanims || hasAghanimsShard)
                                Projectile.NewProjectile(null, Main.MouseWorld, Vector2.Zero, ProjectileType<Projectiles.Soulmancer.SoulShatter>(), 0, 0, Player.whoAmI);
                            else
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Soulmancer.SoulShatter>(), 0, 0, Player.whoAmI);
                            SoundEngine.PlaySound(SoundID.DD2_LightningBugZap, Player.position);
                            break;

                        case "Crusader":
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Hallowed Hammer!", true);
                            Projectile.NewProjectile(null, Player.position, PointToCursor * crusaderHammerVelocity * crusaderHammerVelocityMult, ProjectileID.PaladinsHammerFriendly, (int)(crusaderHammerDamage * abilityPower), 5f, Player.whoAmI);
                            SoundEngine.PlaySound(SoundID.DD2_DrakinShot, Player.position);
                            break;

                        case "Gambler":
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Lucky Streak!", true);
                            Player.AddBuff(BuffType<Buffs.Gambler.LuckyStreak>(), gamblerPassiveBoostMaxDuration);
                            break;

                        case "Inventor":
                            Vector2 vel = Main.MouseWorld - Player.Center;
                            vel.Normalize();
                            vel *= 28f;

                            for (int i = 0; i < inventorCogShotgunProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(vel.X, vel.Y).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-5f, 5f)));

                                if(inventorOverclockCurDuration > 0)
                                    Projectile.NewProjectile(null, Player.Center, perturbedSpeed, ProjectileType<Projectiles.Inventor.Cog>(), inventorSentryDamage * 2, 0, Player.whoAmI);
                                else
                                    Projectile.NewProjectile(null, Player.Center, perturbedSpeed, ProjectileType<Projectiles.Inventor.Cog>(), inventorSentryDamage, 0, Player.whoAmI);
                            }
                            SoundEngine.PlaySound(SoundID.Item36, Player.position);
                            break;

                        case "Plague":
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Infection Burst!", true);
                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                var target = Main.npc[i];
                                if(Vector2.Distance(Player.Center, target.Center) <= plagueInfectionRange)
                                {
                                    if (target.active && !target.dontTakeDamage && !target.friendly && !target.townNPC && target.HasBuff<Buffs.Plague.PlagueInfection>())
                                    {
                                        target.SimpleStrikeNPC((int)(plagueInfectionBurstTotal * abilityPower), 0);
                                        target.AddBuff(BuffType<Buffs.Plague.PlagueInfection>(), 60 * plagueDebuffDuration);

                                        //if(talent heal per enemy hit)
                                    }

                                    if (target.active && !target.dontTakeDamage && !target.friendly && !target.townNPC && !target.HasBuff<Buffs.Plague.PlagueInfection>())
                                        target.AddBuff(BuffType<Buffs.Plague.PlagueInfection>(), 60 * plagueDebuffDuration / 3); //33% duration

                                    if(plagueTalent_10 == "R")
                                    {
                                        HealPlayer(0, 2, (int)(Player.statLifeMax2 * .005f));
                                    }
                                }
                            }

                            // Circle Dust
                            Vector2 origin = Player.Center;
                            origin.X -= Player.width / 2;
                            float radius = plagueInfectionRange;

                            int locations = 40;
                            for (int i = 0; i < locations; i++)
                            {
                                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                                var dust = Dust.NewDustPerfect(position, 61, Vector2.Zero, 0, Color.White, 2f);
                                dust.noGravity = true;
                                dust.noLight = true;

                                Vector2 dvel = dust.position - Player.Center;
                                dvel.Normalize();
                                dvel *= 40f;
                                dust.velocity = -dvel;
                            }
                            SoundEngine.PlaySound(SoundID.DD2_BetsysWrathImpact, Player.position);
                            break;
                    }
                    a2Sound = false;

                    // LAZER TARGETING NPC CLOSEST TO MOUSE, MAYBE GOOD FOR SOME ABILITIES IN THE FUTURE
                    //float dist = 9999f;
                    //for (int i = 0; i < Main.maxNPCs; i++)
                    //{
                    //    if (Main.npc[i].active && !Main.npc[i].friendly && !Main.npc[i].dontTakeDamage)
                    //    {
                    //        Vector2 tCenter = Main.npc[i].Center;
                    //        Vector2 mCenter = Main.MouseWorld;
                    //        if (Vector2.Distance(tCenter, mCenter) < dist)
                    //        {
                    //            Vector2.Distance(ref tCenter, ref mCenter, out float d);
                    //            dist = d;
                    //            lazerTaget = i;
                    //            Main.NewText("New Target: " + i + $"with {dist}");
                    //        }
                    //    }
                    //}
                }

                if (ACM2.ClassAbility2.Current && ability2Cooldown <= 0)
                {
                    switch (equippedClass)
                    {
                        case "Plague":
                            // Circle Dust
                            Vector2 origin = Player.Center;
                            origin.X -= Player.width / 2;
                            float radius = plagueInfectionRange;

                            int locations = 40;
                            for (int i = 0; i < locations; i++)
                            {
                                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                                var dust = Dust.NewDustPerfect(position, 61, Vector2.Zero, 0, Color.White, 2f);
                                dust.noGravity = true;
                                dust.noLight = true;
                                dust.velocity = Player.velocity * .75f;
                            }
                            SoundEngine.PlaySound(SoundID.DD2_BetsysWrathShot, Player.position);
                            break;
                    }
                }

                if (ACM2.ClassAbilityUltimate.JustReleased)
                {
                    Vector2 PointToCursor = Main.MouseWorld - Player.position;
                    PointToCursor.Normalize();

                    if (ultCharge >= ultChargeMax)
                    {   
                        ultCharge -= ultChargeMax;

                        #region Relic Effects
                        if (hasUnstableConcoction)
                            if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;

                        if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2)
                        {
                            int heal = (int)(Player.statLifeMax2 * .075f);
                            healthToRegen += heal;
                            Player.HealEffect(heal);
                            for (int x = 0; x < 3; x++)
                                Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                        }
                        #endregion

                        switch (equippedClass)
                        {
                            case "Vanguard":
                                Projectile.NewProjectile(null, new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 1000), new Vector2(0f, 50f), ProjectileType<Projectiles.Vanguard.VanguardUltimate>(), (int)(vanguardSwordDamage * abilityPower), 0, Player.whoAmI);
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Sword Of Judgement!", true);
                                //NetworkText text;
                                //text = NetworkText.FromKey(player.name + " was healed for " + healAmount + " health", acmPlayer.player.name);
                                //NetMessage.(text, new Color(25, 225, 25));
                                break;

                            case "Blood Mage":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Regeneration!", true);
                                
                                SoundEngine.PlaySound(SoundID.Item4, Player.position);

                                if (Configs._ACMConfigServer.Instance.castingChatMessages && Main.netMode != NetmodeID.SinglePlayer)
                                    SendMultiplayerSyncedChatMessage($"{Player.name} has used 'Regeneration'!");

                                HealPlayer(1, 2, (int)(Player.statLifeMax2 * bloodMageUltRegen * bloodMageUltTicks * healingPower));

                                //bloodMageCurUltTicks = bloodMageUltTicks;
                                break;

                            case "Commander":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Inspire!", true);
                                if (Configs._ACMConfigServer.Instance.castingChatMessages && Main.netMode != NetmodeID.SinglePlayer)
                                    SendMultiplayerSyncedChatMessage($"{Player.name} has used 'Inspire'!");

                                //PlaySyncedSound(SoundID.Thunder.SoundPath, Player.position);

                                SoundEngine.PlaySound(SoundID.Thunder, Player.position);

                                ApplyBuffToAllPlayers(BuffType<Buffs.Commander.Inspire>(), commanderUltDuration);
                                break;

                            case "Scout":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Atomic-Slap (TM)!", true);
                                SoundEngine.PlaySound(SoundID.Item3, Player.position);

                                scoutUltCurDuration = scoutUltDuration;
                                scoutUltInvCurDuration = scoutUltInvDuration;
                                break;

                            case "Soulmancer":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Self Sacrifice!", true);
                                soulmancerSacrificeTimer = 3;
                                soulmancerSacrificeSoulCount_Cur = soulmancerSacrificeSoulCount_Base;
                                break;

                            case "Crusader":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Guardian Angel!", true);
                                if (Configs._ACMConfigServer.Instance.castingChatMessages && Main.netMode != NetmodeID.SinglePlayer)
                                    SendMultiplayerSyncedChatMessage($"{Player.name} has used 'Guardian Angel'!");

                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    Player.AddBuff(BuffType<Buffs.Crusader.GuardianAngel>(), crusaderGuardianAngelDuration);
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
                                            packet.Write(BuffType<Buffs.Crusader.GuardianAngel>());
                                            packet.Write(crusaderGuardianAngelDuration);
                                            packet.Send(-1, -1);
                                        }
                                    } 
                                }
                                SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, Player.position);
                                break;

                            case "Gambler":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Jackpot!", true);
                                Player.AddBuff(BuffType<Buffs.Gambler.Jackpot>(), gamblerUltDuration);
                                break;

                            case "Plague":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Deadzone!", true);
                                _plagueDeadzoneDurationCurrent = plagueDeadzoneDuration;
                                break;

                            case "Inventor":
                                inventorOverclockCurDuration = inventorOverclockDuration;
                                SoundEngine.PlaySound(SoundID.DD2_KoboldIgnite, Player.Center);
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
            tag.Add("totalDamageTaken", totalDamageTaken);

            tag.Add("gotFreeRelic", gotFreeRelic);
            tag.Add("hasAghanimsShard", hasAghanimsShard);

            #region Cards
            tag.Add("cardsPoints", cardsPoints);
            tag.Add("card_ProwlerCount", card_ProwlerCount);
            tag.Add("card_CarryCount", card_CarryCount);
            tag.Add("card_DeadeyeCount", card_DeadeyeCount);
            tag.Add("card_FortifiedCount", card_FortifiedCount);
            tag.Add("card_HealerCount", card_HealerCount);
            tag.Add("card_HealthyCount", card_HealthyCount);
            tag.Add("card_ImpenetrableCount", card_ImpenetrableCount);
            tag.Add("card_MagicalCount", card_MagicalCount);
            tag.Add("card_MasterfulCount", card_MasterfulCount);
            tag.Add("card_MendingCount", card_MendingCount);
            tag.Add("card_MightyCount", card_MightyCount);
            tag.Add("card_NimbleHandsCount", card_NimbleHandsCount);
            tag.Add("card_PowerfulCount", card_PowerfulCount);
            tag.Add("card_SneakyCount", card_SneakyCount);
            tag.Add("card_SparkOfGeniusCount", card_SparkOfGeniusCount);
            tag.Add("card_TimelessCount", card_TimelessCount);
            tag.Add("card_VeteranCount", card_VeteranCount);
            tag.Add("card_MischievousCount", card_MischievousCount);
            tag.Add("card_SeerCount", card_SeerCount);
            tag.Add("card_FerociousCount", card_FerociousCount);
            #endregion

            #region Vanguard
            tag.Add("vanguardDefeatedBosses", vanguardDefeatedBosses);
            tag.Add("vanguardSkillPoints", vanguardSkillPoints);
            tag.Add("vanguardSpentSkillPoints", vanguardSpentSkillPoints);

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

            tag.Add("specVanguard_Defense", specVanguard_Defense);
            tag.Add("specVanguard_MeleeDamage", specVanguard_MeleeDamage);
            tag.Add("specVanguard_ShieldDamageReduction", specVanguard_ShieldDamageReduction);
            tag.Add("specVanguard_SpearDamage", specVanguard_SpearDamage);
            tag.Add("specVanguard_UltCost", specVanguard_UltCost);

            tag.Add("talentSinkVanguardLeft", talentSinkVanguardLeft);
            tag.Add("talentSinkVanguardRight", talentSinkVanguardRight);
            #endregion

            #region Blood Mage
            tag.Add("bloodMageDefeatedBosses", bloodMageDefeatedBosses);
            tag.Add("bloodMageSkillPoints", bloodMageSkillPoints);
            tag.Add("bloodMageSpentSkillPoints", bloodMageSpentSkillPoints);

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

            tag.Add("specBloodMage_MaxHealth", specBloodMage_MaxHealth);
            tag.Add("specBloodMage_CooldownReduction", specBloodMage_CooldownReduction);
            tag.Add("specBloodMage_EnchantDamage", specBloodMage_EnchantDamage);
            tag.Add("specBloodMage_TransfusionDamage", specBloodMage_TransfusionDamage);
            tag.Add("specBloodMage_UltHeal", specBloodMage_UltHeal);

            tag.Add("talentSinkBloodMageLeft", talentSinkBloodMageLeft);
            tag.Add("talentSinkBloodMageRight", talentSinkBloodMageRight);
            #endregion

            #region Commander
            tag.Add("commanderDefeatedBosses", commanderDefeatedBosses);
            tag.Add("commanderSkillPoints", commanderSkillPoints);
            tag.Add("commanderSpentSkillPoints", commanderSpentSkillPoints);

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

            tag.Add("specCommander_BannerDamageReduction", specCommander_BannerDamageReduction);
            tag.Add("specCommander_BannerRange", specCommander_BannerRange);
            tag.Add("specCommander_MinionDamage", specCommander_MinionDamage);
            tag.Add("specCommander_PassiveEndurance", specCommander_PassiveEndurance);
            tag.Add("specCommander_WhipRange", specCommander_WhipRange);

            tag.Add("talentSinkCommanderLeft", talentSinkCommanderLeft);
            tag.Add("talentSinkCommanderRight", talentSinkCommanderRight);
            #endregion

            #region Scout
            tag.Add("scoutDefeatedBosses", scoutDefeatedBosses);
            tag.Add("scoutSkillPoints", scoutSkillPoints);
            tag.Add("scoutSpentSkillPoints", scoutSpentSkillPoints);
            
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

            tag.Add("specScout_ColaDuration", specScout_ColaDuration);
            tag.Add("specScout_CooldownReduction", specScout_CooldownReduction);
            tag.Add("specScout_Dodge", specScout_Dodge);
            tag.Add("specScout_TrapDamage", specScout_TrapDamage);
            tag.Add("specScout_UltCost", specScout_UltCost);

            tag.Add("specSoulmancer_AbilityPower", specSoulmancer_AbilityPower);
            tag.Add("specSoulmancer_ConsumeDuration", specSoulmancer_ConsumeDuration);
            tag.Add("specSoulmancer_CooldownReduction", specSoulmancer_CooldownReduction);
            tag.Add("specSoulmancer_MagicAttackSpeed", specSoulmancer_MagicAttackSpeed);
            tag.Add("specSoulmancer_SoulRipChance", specSoulmancer_SoulRipChance);

            tag.Add("talentSinkScoutLeft", talentSinkScoutLeft);
            tag.Add("talentSinkScoutRight", talentSinkScoutRight);
            #endregion

            #region Soulmancer
            tag.Add("soulmancerDefeatedBosses", soulmancerDefeatedBosses);
            tag.Add("soulmancerSkillPoints", soulmancerSkillPoints);
            tag.Add("soulmancerSpentSkillPoints", soulmancerSpentSkillPoints);

            tag.Add("soulmancerTalent_1", soulmancerTalent_1);
            tag.Add("soulmancerTalent_2", soulmancerTalent_2);
            tag.Add("soulmancerTalent_3", soulmancerTalent_3);
            tag.Add("soulmancerTalent_4", soulmancerTalent_4);
            tag.Add("soulmancerTalent_5", soulmancerTalent_5);
            tag.Add("soulmancerTalent_6", soulmancerTalent_6);
            tag.Add("soulmancerTalent_7", soulmancerTalent_7);
            tag.Add("soulmancerTalent_8", soulmancerTalent_8);
            tag.Add("soulmancerTalent_9", soulmancerTalent_9);
            tag.Add("soulmancerTalent_10", soulmancerTalent_10);

            tag.Add("talentSinkSoulmancerLeft", talentSinkSoulmancerLeft);
            tag.Add("talentSinkSoulmancerRight", talentSinkSoulmancerRight);
            #endregion

            #region Crusader
            tag.Add("crusaderDefeatedBosses", crusaderDefeatedBosses);
            tag.Add("crusaderSkillPoints", crusaderSkillPoints);
            tag.Add("crusaderSpentSkillPoints", crusaderSpentSkillPoints);

            tag.Add("crusaderTalent_1", crusaderTalent_1);
            tag.Add("crusaderTalent_2", crusaderTalent_2);
            tag.Add("crusaderTalent_3", crusaderTalent_3);
            tag.Add("crusaderTalent_4", crusaderTalent_4);
            tag.Add("crusaderTalent_5", crusaderTalent_5);
            tag.Add("crusaderTalent_6", crusaderTalent_6);
            tag.Add("crusaderTalent_7", crusaderTalent_7);
            tag.Add("crusaderTalent_8", crusaderTalent_8);
            tag.Add("crusaderTalent_9", crusaderTalent_9);
            tag.Add("crusaderTalent_10", crusaderTalent_10);

            tag.Add("talentSinkCrusaderLeft", talentSinkCrusaderLeft);
            tag.Add("talentSinkCrusaderRight", talentSinkCrusaderRight);
            #endregion

            #region Gambler
            tag.Add("gamblerDefeatedBosses", gamblerDefeatedBosses);
            tag.Add("gamblerSkillPoints", gamblerSkillPoints);
            tag.Add("gamblerSpentSkillPoints", gamblerSpentSkillPoints);

            tag.Add("gamblerTalent_1", gamblerTalent_1);
            tag.Add("gamblerTalent_2", gamblerTalent_2);
            tag.Add("gamblerTalent_3", gamblerTalent_3);
            tag.Add("gamblerTalent_4", gamblerTalent_4);
            tag.Add("gamblerTalent_5", gamblerTalent_5);
            tag.Add("gamblerTalent_6", gamblerTalent_6);
            tag.Add("gamblerTalent_7", gamblerTalent_7);
            tag.Add("gamblerTalent_8", gamblerTalent_8);
            tag.Add("gamblerTalent_9", gamblerTalent_9);
            tag.Add("gamblerTalent_10", gamblerTalent_10);

            tag.Add("talentSinkGamblerLeft", talentSinkGamblerLeft);
            tag.Add("talentSinkGamblerRight", talentSinkGamblerRight);
            #endregion

            #region Plague
            tag.Add("plagueDefeatedBosses", plagueDefeatedBosses);
            tag.Add("plagueSkillPoints", plagueSkillPoints);
            tag.Add("plagueSpentSkillPoints", plagueSpentSkillPoints);

            tag.Add("plagueTalent_1", plagueTalent_1);
            tag.Add("plagueTalent_2", plagueTalent_2);
            tag.Add("plagueTalent_3", plagueTalent_3);
            tag.Add("plagueTalent_4", plagueTalent_4);
            tag.Add("plagueTalent_5", plagueTalent_5);
            tag.Add("plagueTalent_6", plagueTalent_6);
            tag.Add("plagueTalent_7", plagueTalent_7);
            tag.Add("plagueTalent_8", plagueTalent_8);
            tag.Add("plagueTalent_9", plagueTalent_9);
            tag.Add("plagueTalent_10", plagueTalent_10);

            tag.Add("talentSinkPlagueLeft", talentSinkPlagueLeft);
            tag.Add("talentSinkPlagueRight", talentSinkPlagueRight);
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
            totalDamageTaken = tag.GetInt("totalDamageTaken");
            
            gotFreeRelic = tag.GetBool("gotFreeRelic");
            hasAghanimsShard = tag.GetBool("hasAghanimsShard");


            #region Cards
            cardsPoints = tag.GetInt("cardsPoints");
            card_ProwlerCount = tag.GetInt("card_ProwlerCount");
            card_CarryCount = tag.GetInt("card_CarryCount");
            card_DeadeyeCount = tag.GetInt("card_DeadeyeCount");
            card_FortifiedCount = tag.GetInt("card_FortifiedCount");
            card_HealerCount = tag.GetInt("card_HealerCount");
            card_HealthyCount = tag.GetInt("card_HealthyCount");
            card_ImpenetrableCount = tag.GetInt("card_ImpenetrableCount");
            card_MagicalCount = tag.GetInt("card_MagicalCount");
            card_MasterfulCount = tag.GetInt("card_MasterfulCount");
            card_MendingCount = tag.GetInt("card_MendingCount");
            card_MightyCount = tag.GetInt("card_MightyCount");
            card_NimbleHandsCount = tag.GetInt("card_NimbleHandsCount");
            card_PowerfulCount = tag.GetInt("card_PowerfulCount");
            card_SneakyCount = tag.GetInt("card_SneakyCount");
            card_SparkOfGeniusCount = tag.GetInt("card_SparkOfGeniusCount");
            card_TimelessCount = tag.GetInt("card_TimelessCount");
            card_VeteranCount = tag.GetInt("card_VeteranCount");
            card_MischievousCount = tag.GetInt("card_MischievousCount");
            card_SeerCount = tag.GetInt("card_SeerCount");
            card_FerociousCount = tag.GetInt("card_FerociousCount");
            #endregion

            #region Vanguard
            vanguardDefeatedBosses.AddRange(tag.GetList<string>("vanguardDefeatedBosses"));
            vanguardSkillPoints = tag.GetInt("vanguardSkillPoints");
            vanguardSpentSkillPoints = tag.GetInt("vanguardSpentSkillPoints");

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

            specVanguard_Defense = tag.GetInt("specVanguard_Defense");
            specVanguard_MeleeDamage = tag.GetInt("specVanguard_MeleeDamage");
            specVanguard_ShieldDamageReduction = tag.GetInt("specVanguard_ShieldDamageReduction");
            specVanguard_SpearDamage = tag.GetInt("specVanguard_SpearDamage");
            specVanguard_UltCost = tag.GetInt("specVanguard_UltCost");

            talentSinkVanguardLeft = tag.GetInt("talentSinkVanguardLeft");
            talentSinkVanguardRight = tag.GetInt("talentSinkVanguardRight");
            #endregion

            #region Blood Mage
            bloodMageDefeatedBosses.AddRange(tag.GetList<string>("bloodMageDefeatedBosses"));
            bloodMageSkillPoints = tag.GetInt("bloodMageSkillPoints");
            bloodMageSpentSkillPoints = tag.GetInt("bloodMageSpentSkillPoints");

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

            specBloodMage_MaxHealth = tag.GetInt("specBloodMage_MaxHealth");
            specBloodMage_CooldownReduction = tag.GetInt("specBloodMage_CooldownReduction");
            specBloodMage_EnchantDamage = tag.GetInt("specBloodMage_EnchantDamage");
            specBloodMage_TransfusionDamage = tag.GetInt("specBloodMage_TransfusionDamage");
            specBloodMage_UltHeal = tag.GetInt("specBloodMage_UltHeal");

            talentSinkBloodMageLeft = tag.GetInt("talentSinkBloodMageLeft");
            talentSinkBloodMageRight = tag.GetInt("talentSinkBloodMageRight");
            #endregion

            #region Commander
            commanderDefeatedBosses.AddRange(tag.GetList<string>("commanderDefeatedBosses"));
            commanderSkillPoints = tag.GetInt("commanderSkillPoints");
            commanderSpentSkillPoints = tag.GetInt("commanderSpentSkillPoints");

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

            specCommander_BannerDamageReduction = tag.GetInt("specCommander_BannerDamageReduction");
            specCommander_BannerRange = tag.GetInt("specCommander_BannerRange");
            specCommander_MinionDamage = tag.GetInt("specCommander_MinionDamage");
            specCommander_PassiveEndurance = tag.GetInt("specCommander_PassiveEndurance");
            specCommander_WhipRange = tag.GetInt("specCommander_WhipRange");

            talentSinkCommanderLeft = tag.GetInt("talentSinkCommanderLeft");
            talentSinkCommanderRight = tag.GetInt("talentSinkCommanderRight");
            #endregion

            #region Scout
            scoutDefeatedBosses.AddRange(tag.GetList<string>("scoutDefeatedBosses"));
            scoutSkillPoints = tag.GetInt("scoutSkillPoints");
            scoutSpentSkillPoints = tag.GetInt("scoutSpentSkillPoints");
            
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

            specScout_ColaDuration = tag.GetInt("specScout_ColaDuration");
            specScout_CooldownReduction = tag.GetInt("specScout_CooldownReduction");
            specScout_Dodge = tag.GetInt("specScout_Dodge");
            specScout_TrapDamage = tag.GetInt("specScout_TrapDamage");
            specScout_UltCost = tag.GetInt("specScout_UltCost");

            talentSinkScoutLeft = tag.GetInt("talentSinkScoutLeft");
            talentSinkScoutRight = tag.GetInt("talentSinkScoutRight");
            #endregion

            #region Soulmancer
            soulmancerDefeatedBosses.AddRange(tag.GetList<string>("soulmancerDefeatedBosses"));
            soulmancerSkillPoints = tag.GetInt("soulmancerSkillPoints");
            soulmancerSpentSkillPoints = tag.GetInt("soulmancerSpentSkillPoints");

            soulmancerTalent_1 = tag.GetString("soulmancerTalent_1");
            soulmancerTalent_2 = tag.GetString("soulmancerTalent_2");
            soulmancerTalent_3 = tag.GetString("soulmancerTalent_3");
            soulmancerTalent_4 = tag.GetString("soulmancerTalent_4");
            soulmancerTalent_5 = tag.GetString("soulmancerTalent_5");
            soulmancerTalent_6 = tag.GetString("soulmancerTalent_6");
            soulmancerTalent_7 = tag.GetString("soulmancerTalent_7");
            soulmancerTalent_8 = tag.GetString("soulmancerTalent_8");
            soulmancerTalent_9 = tag.GetString("soulmancerTalent_9");
            soulmancerTalent_10 = tag.GetString("soulmancerTalent_10");

            specSoulmancer_AbilityPower = tag.GetInt("specSoulmancer_AbilityPower");
            specSoulmancer_ConsumeDuration = tag.GetInt("specSoulmancer_ConsumeDuration");
            specSoulmancer_CooldownReduction = tag.GetInt("specSoulmancer_CooldownReduction");
            specSoulmancer_MagicAttackSpeed = tag.GetInt("specSoulmancer_MagicAttackSpeed");
            specSoulmancer_SoulRipChance = tag.GetInt("specSoulmancer_SoulRipChance");

            talentSinkSoulmancerLeft = tag.GetInt("talentSinkSoulmancerLeft");
            talentSinkSoulmancerRight = tag.GetInt("talentSinkSoulmancerRight");
            #endregion

            #region Crusader
            crusaderDefeatedBosses.AddRange(tag.GetList<string>("crusaderDefeatedBosses"));
            crusaderSkillPoints = tag.GetInt("crusaderSkillPoints");
            crusaderSpentSkillPoints = tag.GetInt("crusaderSpentSkillPoints");

            crusaderTalent_1 = tag.GetString("crusaderTalent_1");
            crusaderTalent_2 = tag.GetString("crusaderTalent_2");
            crusaderTalent_3 = tag.GetString("crusaderTalent_3");
            crusaderTalent_4 = tag.GetString("crusaderTalent_4");
            crusaderTalent_5 = tag.GetString("crusaderTalent_5");
            crusaderTalent_6 = tag.GetString("crusaderTalent_6");
            crusaderTalent_7 = tag.GetString("crusaderTalent_7");
            crusaderTalent_8 = tag.GetString("crusaderTalent_8");
            crusaderTalent_9 = tag.GetString("crusaderTalent_9");
            crusaderTalent_10 = tag.GetString("crusaderTalent_10");

            talentSinkCrusaderLeft = tag.GetInt("talentSinkCrusaderLeft");
            talentSinkCrusaderRight = tag.GetInt("talentSinkCrusaderRight");
            #endregion

            #region Gambler
            gamblerDefeatedBosses.AddRange(tag.GetList<string>("gamblerDefeatedBosses"));
            gamblerSkillPoints = tag.GetInt("gamblerSkillPoints");
            gamblerSpentSkillPoints = tag.GetInt("gamblerSpentSkillPoints");

            gamblerTalent_1 = tag.GetString("gamblerTalent_1");
            gamblerTalent_2 = tag.GetString("gamblerTalent_2");
            gamblerTalent_3 = tag.GetString("gamblerTalent_3");
            gamblerTalent_4 = tag.GetString("gamblerTalent_4");
            gamblerTalent_5 = tag.GetString("gamblerTalent_5");
            gamblerTalent_6 = tag.GetString("gamblerTalent_6");
            gamblerTalent_7 = tag.GetString("gamblerTalent_7");
            gamblerTalent_8 = tag.GetString("gamblerTalent_8");
            gamblerTalent_9 = tag.GetString("gamblerTalent_9");
            gamblerTalent_10 = tag.GetString("gamblerTalent_10");

            talentSinkGamblerLeft = tag.GetInt("talentSinkGamblerLeft");
            talentSinkGamblerRight = tag.GetInt("talentSinkGamblerRight");
            #endregion

            #region Plague
            plagueDefeatedBosses.AddRange(tag.GetList<string>("plagueDefeatedBosses"));
            plagueSkillPoints = tag.GetInt("plagueSkillPoints");
            plagueSpentSkillPoints = tag.GetInt("plagueSpentSkillPoints");

            plagueTalent_1 = tag.GetString("plagueTalent_1");
            plagueTalent_2 = tag.GetString("plagueTalent_2");
            plagueTalent_3 = tag.GetString("plagueTalent_3");
            plagueTalent_4 = tag.GetString("plagueTalent_4");
            plagueTalent_5 = tag.GetString("plagueTalent_5");
            plagueTalent_6 = tag.GetString("plagueTalent_6");
            plagueTalent_7 = tag.GetString("plagueTalent_7");
            plagueTalent_8 = tag.GetString("plagueTalent_8");
            plagueTalent_9 = tag.GetString("plagueTalent_9");
            plagueTalent_10 = tag.GetString("plagueTalent_10");

            talentSinkPlagueLeft = tag.GetInt("talentSinkPlagueLeft");
            talentSinkPlagueRight = tag.GetInt("talentSinkPlagueRight");
            #endregion

            base.LoadData(tag);
        }

        /// <summary>
        /// Add ability cooldowns. 'ability' corresponds to ability number (1/2).
        /// </summary>
        void AddAbilityCooldown(int ability, int timeInSeconds)
        {
            if (ability == 1)
                ability1Cooldown = (int)(60 * timeInSeconds * cooldownReduction * ability1cdr);
            if (ability == 2)
                ability2Cooldown = (int)(60 * timeInSeconds * cooldownReduction * ability2cdr);
        }

        /// <summary>
        /// Sets the Player in combat for 3s, increasing ult charge.
        /// </summary>
        public void InBattle()
        {
            inBattleTimer = outOfBattleTimeMax;
            outOfBattleTimer = inBattleTimeMax;
        }

        public void SendMultiplayerSyncedChatMessage(string msg, Color? color = null)
        {
            if (color == null) color = Color.White;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(msg, color);
            }
            else
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)ACMHandlePacketMessage.SendChatMessage);
                packet.Write(msg);
                packet.Write(color.Value.R);
                packet.Write(color.Value.G);
                packet.Write(color.Value.B);
                packet.Send(-1, -1);
            }
        }

        public void PlaySyncedSound(string soundPath, Vector2 pos)
        {
            if(Main.netMode == NetmodeID.SinglePlayer)
            {
                SoundEngine.PlaySound(new SoundStyle(soundPath), pos);
            }
            else
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)ACM2.ACMHandlePacketMessage.PlaySyncedSound);
                packet.Write(soundPath);
                packet.WriteVector2(pos);
                packet.Send(-1, -1);
            }
        }

        /// <summary>
        /// Call this to easily heal players based on arguments passed.
        /// <para>healAmount is automatically set to 1 if it is lower than 1.</para>
        /// </summary>
        /// <param name="healType"><para>Determines who to heal:</para>
        /// <para>0 = Heal self</para>
        /// <para>1 = Heal Everyone</para>
        /// <para>2 = Heal Lowest Health(WIP, DOES NOT WORK)</para></param>
        /// <param name="healSpeed">
        /// <para>Determines how fast the player is healed:</para> 
        /// <para>0 = Instant</para>
        /// <para>1 = Snail</para>
        /// <para>2 = Slow</para>
        /// <para>3 = Medium</para>
        /// <para>4 = Fast</para>
        /// <para>5 = Per Second</para></param>
        /// <param name="healAmount">The amount the player is healed for</param>
        public void HealPlayer(int healType, int healSpeed, int healAmount, bool? healEffect = true)
        {
            if (healAmount < 1)
                healAmount = 1;

            //Multiplayer
            if (Main.netMode == NetmodeID.MultiplayerClient) 
            {
                // Heal Self Only
                if (healType == 0)
                {
                    if ((bool)healEffect)
                        Player.HealEffect(healAmount);
                    switch (healSpeed)
                    {
                        case 0: // Instant
                            Player.statLife += healAmount;
                            break;

                        case 1: // Snail Speed
                            healthToRegenSnail += healAmount;
                            break;

                        case 2: // Slow Speed
                            healthToRegenSlow += healAmount;
                            break;

                        case 3: // Medium Speed
                            healthToRegenMedium += healAmount;
                            break;

                        case 4: // Fast Speed
                            healthToRegen += healAmount;
                            break;

                        case 5: // p/Second
                            healthToRegenSecond += healAmount;
                            break;
                    }
                }

                // Heal All Players
                if (healType == 1)
                {
                    switch (healSpeed)
                    {
                        case 0: // Instant
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayer);
                                    packet.Write((byte)i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 1: // Snail Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerSnail);
                                    packet.Write((byte)i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 2: // Slow Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerSlow);
                                    packet.Write((byte)i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 3: // Medium Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerMedium);
                                    packet.Write((byte)i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 4: // Fast Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerFast);
                                    packet.Write((byte)i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 5: // Fast Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerSecond);
                                    packet.Write((byte)i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;
                    }
                }

                // Heal Lowest Health
                if (healType == 2)
                {
                    switch (healSpeed)
                    {
                        case 0: // Instant
                            int curHP = 99999999;
                            int selectedPlayer = -1;
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    if(Main.player[i].statLife < curHP)
                                    {
                                        curHP = Main.player[i].statLife;
                                        selectedPlayer = i;
                                    }

                                    if(i == 255)
                                    {
                                        ModPacket packet = Mod.GetPacket();
                                        packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayer);
                                        packet.Write(selectedPlayer);
                                        packet.Write(healAmount);
                                        packet.Send(-1, -1);
                                    }
                                }
                            break;

                        case 1: // Snail Speed

                            break;

                        case 2: // Slow Speed

                            break;

                        case 3: // Medium Speed

                            break;

                        case 4: // Fast Speed

                            break;
                    }
                }
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)// Singleplayer
            {
                // Heal Self Only
                switch (healSpeed)
                {
                    case 0: // Instant
                        Player.statLife += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 1: // Snail Speed
                        healthToRegenSnail += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 2: // Slow Speed
                        healthToRegenSlow += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 3: // Medium Speed
                        healthToRegenMedium += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 4: // Fast Speed
                        healthToRegen += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 5: // p/Second
                        healthToRegenSecond += healAmount;
                        Player.HealEffect(healAmount);
                        break;
                }
            }
        }

        void ApplyBuffToAllPlayers(int buffType, int duration)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead)
                    {
                        ModPacket packet = Mod.GetPacket();
                        packet.Write((byte)ACM2.ACMHandlePacketMessage.BuffPlayer);
                        packet.Write((byte)i);
                        packet.Write(buffType);
                        packet.Write(duration);
                        packet.Send(-1, -1);
                    }
                }
            }

            if(Main.netMode == NetmodeID.SinglePlayer)
            {
                Player.AddBuff(buffType, duration);
            }
        }
    }
}