using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs.Commander
{
    public class WarBanner : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Commander's War Banner]");
            Description.SetDefault("Increased damage dealt\nReduced damage taken");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ACMPlayer>().commanderBannerBuffDuration = 1;
        }
    }
}