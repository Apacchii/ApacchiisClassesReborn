using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs
{
    public class SacrificeDot2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.buffImmune[ModContent.BuffType<SacrificeDot2>()] = false;
            npc.GetGlobalNPC<ACMGlobalNPC>().sacrifice2 = true;
            base.Update(npc, ref buffIndex);
        }
    }
}