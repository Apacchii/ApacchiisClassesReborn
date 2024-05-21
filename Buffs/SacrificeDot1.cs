using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs
{
    public class SacrificeDot1 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.buffImmune[ModContent.BuffType<SacrificeDot1>()] = false;
            npc.GetGlobalNPC<ACMGlobalNPC>().sacrifice1 = true;
            base.Update(npc, ref buffIndex);
        }
    }
}