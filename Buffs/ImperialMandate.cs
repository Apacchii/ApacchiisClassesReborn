using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ApacchiisClassesMod2.Buffs
{
    public class ImperialMandate : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<ACMGlobalNPC>().imperialMandate = true;
            base.Update(npc, ref buffIndex);
        }
    }
}