using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Buffs.Inventor
{
    public class InventorPassive : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("On-The-Go Maintenance");
            // Description.SetDefault("Increasing both your and your turret's attack speed");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += .1f;
            player.GetModPlayer<ACMPlayer>().inventorSentryFirerate = (int)(player.GetModPlayer<ACMPlayer>().inventorSentryFirerate * .9f);
        }
    }
}