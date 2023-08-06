using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Buffs.Gambler
{
    public class Jackpot : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Jackpot");
            /* Description.SetDefault("Your class' banner crit loss is now inverted, granting you positive crit chance instead\n" +
                                   "Attack speed increased\n" +
                                   "Cooldown reduction increased"); */
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ACMPlayer>().gamblerUlt = true;
        }
    }
}