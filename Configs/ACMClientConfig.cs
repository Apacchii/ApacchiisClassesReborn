using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ApacchiisClassesMod2.Configs
{
    //[LabelKey("ClientConfig")]
    public class ACMConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static ACMConfigClient Instance;

        [Header("HUD")]

        [DefaultValue(true)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.CompactHUD.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.CompactHUD.Tooltip")]
        public bool compactHUD { get; set; }

        [DefaultValue(true)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.BlinkingHUD.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.BlinkingHUD.Tooltip")]
        public bool blinkingHUD { get; set; }

        [Header("Effects/Feeback")]

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.SmallerAbilityVFX.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.SmallerAbilityVFS.Tooltip")]
        public bool smallVFX { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.AnnounceLevelUp.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.AnnounceLevelUp.Tooltip")]
        public bool announceLevelUp { get; set; }
    }
}