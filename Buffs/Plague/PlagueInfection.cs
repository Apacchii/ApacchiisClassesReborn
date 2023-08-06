using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs.Plague
{
    public class PlagueInfection : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.buffImmune[ModContent.BuffType<PlagueInfection>()] = false;
            npc.GetGlobalNPC<ACMGlobalNPC>().plagueInfection = true;
            base.Update(npc, ref buffIndex);
        }
    }
}