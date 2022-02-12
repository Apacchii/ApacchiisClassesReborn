using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ApacchiisClassesMod2
{
    [Label("Server Config")]
    public class ACMConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static ACMConfigServer Instance;

        [Header("General")]

        //[DefaultValue(false)]
        //[Label("Global levels")]
        //[Tooltip("If true, all classes will share the same level, if false, each class will have their own level")]
        //public bool configGlobalLevels { get; set; }

        [Slider]
        [DefaultValue(100)]
        [Increment(5)]
        [Range(10, 100)]
        [Label("Max Class Level")]
        [Tooltip("Changes the maximum level all classes can reach\n[Default: 100]")]
        public int maxClassLevel { get; set; }

        [Slider]
        [DefaultValue(1.05f)]
        [Increment(.05f)]
        [Range(1f, 1.25f)]
        [Label("Enemy Damage Multiplier")]
        [Tooltip("Increases how much damage enemies deal to the player\n[Default: 1.05x]")]

        public float enemyDamageMultiplier { get; set; }

        [DefaultValue(true)]
        [Label("Class-Specific Weapons")]
        [Tooltip("If true, whenever you craft a class you'll receive a weak weapon specific to that class.\n[Default: On]")]
        public bool classWeaponsEnabled { get; set; }

        [DefaultValue(false)]
        [Label("Hidden Accessory Disables Stats")]
        [Tooltip("If true, all classes wont grant you stats when the accessory slot's visual is hidden\n[Default: Off]")]
        public bool configHidden { get; set; }

        [DefaultValue(false)]
        [Label("Double Talents")]
        [Tooltip("Changes if you are able to allocate points on both rows of the talent tree from level 10 onwards\n[Default: Off]")]
        public bool doubleTalents { get; set; }

        [Label("Class Stats Multiplier")]
        [Tooltip("Changes how many stats your class gives you per level\nDecreasing this will make the class give you less stats but will reduce the mod's impact on balance/difficulty")]
        //[DrawTicks]
        [DefaultValue(1)]
        [Increment(.05f)]
        [Range(.5f, 1.25f)]
        [Slider]
        [SliderColor(255, 255, 255)]
        public float classStatMult { get; set; }
    }
}
