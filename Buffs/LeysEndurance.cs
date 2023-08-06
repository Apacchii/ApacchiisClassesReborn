using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs
{
    public class LeysEndurance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ley's Mushroom Endurance");
            // Description.SetDefault("Decreasing damage taken");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(Main.hardMode)
                player.endurance += .06f;
            else
                player.endurance += .04f;
        }
    }
}