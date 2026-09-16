using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace ApacchiisClassesMod2
{
    public class GlobalNPCChanges : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add(new Item(ItemID.Mushroom)
                {
                    shopCustomPrice = 001000,
                });
            }
                
            base.ModifyShop(shop);
        }
    }
}