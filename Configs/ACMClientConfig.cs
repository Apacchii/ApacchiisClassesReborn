using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ApacchiisClassesMod2.Configs
{
    //[LabelKey("ClientConfig")]
    public class ACMConfigClient : ModConfig
    {

        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static ACMConfigClient Instance;


        //General HUD
        [Header("HUD")]

        [DefaultValue("Center")]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.HUDStyle.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.HUDStyle.Tooltip")]
        [DrawTicks]
        [OptionStrings(new string[] { "Left", "Center", "Right" })]
        public string hudStyle { get; set; }

        [DefaultValue(.04f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ReworkedHudHorizontalPosition.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ReworkedHudHorizontalPosition.Tooltip")]
        [Slider]
        [SliderColor(255, 255, 255)]
        [Increment(.01f)]
        [Range(0f, 1f)]
        public float reworkedHudHPos { get; set; }

        [DefaultValue(.94f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ReworkedHudVerticalPosition.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ReworkedHudVerticalPosition.Tooltip")]
        [Slider]
        [SliderColor(255, 255, 255)]
        [Increment(.01f)]
        [Range(0f, 1f)]
        public float reworkedHudVPos { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.CompactHUD.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.CompactHUD.Tooltip")]
        public bool compactHUD { get; set; }

        [DefaultValue(true)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.BlinkingHUD.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.BlinkingHUD.Tooltip")]
        public bool blinkingHUD { get; set; }

        //Quest HUD
        [Header("QuestsHUD")]

        [DefaultValue(true)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ShowQuestHUD.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ShowQuestHUD.Tooltip")]
        public bool showQuestHUD { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.NewQuestChatMessage.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.NewQuestChatMessage.Tooltip")]
        public bool questChatMessage { get; set; }

        [DefaultValue(700)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.QuestDescTextWidth.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.QuestDescTextWidth.Tooltip")]
        [Slider]
        [SliderColor(255, 255, 255)]
        [Increment(50f)]
        [Range(300f, 1000f)]
        public float questDescTextWidth { get; set; }

        //Team HUD
        [Header("TeamHUD")]

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.TeamHUD.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.TeamHUD.Tooltip")]
        public bool teamHUD { get; set; }

        [DefaultValue(.8f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.TeamHUDPlacementHorizontal.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.TeamHUDPlacementHorizontal.Tooltip")]
        [Slider]
        [SliderColor(255, 255, 255)]
        [Increment(.01f)]
        [Range(0f, 1f)]
        public float teamHUDPlacementHorizontal { get; set; }

        [DefaultValue(.04f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.TeamHUDPlacementVertical.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.TeamHUDPlacementVertical.Tooltip")]
        [Slider]
        [SliderColor(255, 255, 255)]
        [Increment(.01f)]
        [Range(0f, 1f)]
        public float teamHUDPlacementVertical { get; set; }

        //Effects & Feedback
        [Header("Effects/Feeback")]

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.SmallerAbilityVFX.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.SmallerAbilityVFX.Tooltip")]
        public bool smallVFX { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ZoomEffects.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ZoomEffects.Tooltip")]
        public bool ZoomEffects { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.AnnounceLevelUp.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.AnnounceLevelUp.Tooltip")]
        public bool announceLevelUp { get; set; }

        [DefaultValue(true)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ScreenShake.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ScreenShake.Tooltip")]
        public bool screenShake { get; set; }

        [DefaultValue(1f)]
        [Slider]
        [SliderColor(255, 255, 255)]
        [Increment(.1f)]
        [Range(.1f, 1f)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ScreenShakeIntensity.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ScreenShakeIntensity.Tooltip")]
        public float screenShakeIntensity { get; set; }

        [DefaultValue(false)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.BloodMagePassiveSFX.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.BloodMagePassiveSFX.Tooltip")]
        public bool bloodMagePassiveSFX { get; set; }

        [DefaultValue(true)]
        [LabelKey("$Mods.ApacchiisClassesMod2.Config.ApothecaryHelpPanel.Label")]
        [TooltipKey("$Mods.ApacchiisClassesMod2.Config.ApothecaryHelpPanel.Tooltip")]
        public bool ApothecaryHelpPanel { get; set; }
    }
}