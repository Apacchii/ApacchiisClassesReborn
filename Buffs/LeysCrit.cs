using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs
{
    public class LeysCrit : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ley's Mushroom Critical Strikes");
            Description.SetDefault("Increasing critial strike chance");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(Main.hardMode)
                player.GetCritChance(DamageClass.Generic) += 5;
            else
                player.GetCritChance(DamageClass.Generic) += 7;
        }
    }
}