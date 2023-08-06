using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs
{
    public class LeysAttackSpeed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ley's Mushroom Attack Speed");
            // Description.SetDefault("Increasing attack speed");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(Main.hardMode)
                player.GetAttackSpeed(DamageClass.Generic) += .08f;
            else
                player.GetAttackSpeed(DamageClass.Generic) += .05f;
        }
    }
}