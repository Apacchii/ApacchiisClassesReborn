using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Buffs.Apothecary
{
    public class ApothecaryMaxHealthMix : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ACMPlayer>().lifeMult += .1f;
        }
    }
}