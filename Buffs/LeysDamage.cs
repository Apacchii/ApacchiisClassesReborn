using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs
{
    public class LeysDamage : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ley's Mushroom Damage");
            // Description.SetDefault("Increasing damage dealt");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(Main.hardMode)
                player.GetDamage(DamageClass.Generic) += .05f;
            else
                player.GetDamage(DamageClass.Generic) += .04f;
        }
    }
}