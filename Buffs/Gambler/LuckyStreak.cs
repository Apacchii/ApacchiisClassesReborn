using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Buffs.Gambler
{
    public class LuckyStreak : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lucky Streak");
            // Description.SetDefault("Increasing your passive's minimum and maximum damage deatl");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ACMPlayer>().gamblerLuckyStreak = true;

            if (player.GetModPlayer<ACMPlayer>().gamblerTalent_7 == "L")
                player.GetModPlayer<ACMPlayer>().gamblerLuckyStreakDodge = true;
        }
    }
}