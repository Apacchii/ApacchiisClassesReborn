using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ApacchiisClassesMod2.Configs
{
    //[LabelKey("Server Config")]
    public class _ACMConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static _ACMConfigServer Instance;

        #region General
        [Header("General")]

        //[DefaultValue(false)]
        //[Label("Global levels")]
        //[Tooltip("If true, all classes will share the same level, if false, each class will have their own level")]
        //public bool configGlobalLevels { get; set; }

        [Slider]
        [DefaultValue(100)]
        [Increment(5)]
        [Range(10, 200)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.MaxClassLevel.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.MaxClassLevel.Tooltip")]
        public int maxClassLevel { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ExtraRelicSlot.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ExtraRelicSlot.Tooltip")]
        public bool doubleRelics { get; set; }

        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ChatMessageSupportAbility.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ChatMessageSupportAbility.Tooltip")]
        [DefaultValue(true)]
        public bool castingChatMessages { get; set; }

        //[DefaultValue(true)]
        //[Label("Class-Specific Weapons")]
        //[Tooltip("If true, whenever you craft a class you'll receive a weak weapon specific to that class.\n[Default: On]")]
        //public bool classWeaponsEnabled { get; set; }

        //[DefaultValue(false)]
        //[Label("Start with a random relic")]
        //[Tooltip("If true, characters will start with a random relic item.\nWorks even after a character has been created\n[Default: Off]")]
        //public bool startWithRelic { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.HiddenAccStats.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.HiddenAccStats.Tooltip")]
        public bool configHidden { get; set; }
        #endregion

        #region Balance
        [Header("Balance")]

        [Slider]
        [DefaultValue(1.05f)]
        [Increment(.05f)]
        [Range(1f, 5f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.EnemyDamageMultiplier.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.EnemyDamageMultiplier.Tooltip")]
        public float enemyDamageMultiplier { get; set; }

        [Slider]
        [DefaultValue(1.05f)]
        [Increment(.01f)]
        [Range(1f, 5f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.EnemyNonBossHealthMultiplier.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.EnemyNonBossHealthMultiplier.Tooltip")]
        public float enemyHealthMultiplier { get; set; }

        [Slider]
        [DefaultValue(1.05f)]
        [Increment(.05f)]
        [Range(1f, 5f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.BossHealthMultiplier.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.BossHealthMultiplier.Tooltip")]
        public float bossHealthMultiplier { get; set; }

        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ClassStatsMultiplier.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ClassStatsMultiplier.Tooltip")]
        //[DrawTicks]
        [DefaultValue(1)]
        [Increment(.05f)]
        [Range(.1f, 3f)]
        [Slider]
        [SliderColor(255, 255, 255)]
        public float classStatMult { get; set; }

        [LabelKey("$Mods.ApacchiisClassesMod2.Config.RuneStatsMultiplier.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.RuneStatsMultiplier.Tooltip")]
        //[DrawTicks]
        [DefaultValue(1)]
        [Increment(.05f)]
        [Range(.1f, 3f)]
        [Slider]
        [SliderColor(255, 255, 255)]
        public float runesStatMult { get; set; }

        [LabelKey("$Mods.ApacchiisClassesMod2.Config.APWeaponDPS.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.APWeaponDPS.Tooltip")]
        [DefaultValue(.05f)]
        [Increment(.01f)]
        [Range(.01f, .5f)]
        [Slider]
        [SliderColor(255, 255, 255)]
        public float abilityPowerWeaponDPSMult { get; set; }

        #endregion

        #region Compatibility
        [Header("Mod.Compatibility")]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.CalamityScaling.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.CalamityScaling.Tooltip")]
        [DefaultValue(false)]
        public bool calamityScaling { get; set; }
        #endregion
    }
}
