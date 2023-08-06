using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ApacchiisClassesMod2.Items.Accessories

{
    public class DevCooldownAcc : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("DevCooldownAcc");
            // Tooltip.SetDefault("[Dev Item]\n'Used by the developer to test class abilities'\nReduces ability cooldowns by 98%\nUltimate charges 10x faster");
        }
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = -12;
        }

		
		public override void UpdateAccessory (Player player, bool hideVisual)
		{

            if(player.name == "Modding" || player.name == "Modding Man")
                player.statLifeMax2 += 1000;

            player.GetModPlayer<ACMPlayer>().cooldownReduction -= .98f;
            player.GetModPlayer<ACMPlayer>().devTool = true;
        }

        //public override bool CanEquipAccessory(Player player, int slot, bool modded)
        //{
        //    if (player.name == "Modding" || player.name == "Modding Man")
        //        return true;
        //    else
        //        return false;
        //}
    }
}

