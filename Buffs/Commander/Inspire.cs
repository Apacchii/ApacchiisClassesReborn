using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs.Commander
{
    public class Inspire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[Inspire]");
            // Description.SetDefault("Every attack is a critical strike");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ACMPlayer>().commanderUltActive = true;
        }
    }
}