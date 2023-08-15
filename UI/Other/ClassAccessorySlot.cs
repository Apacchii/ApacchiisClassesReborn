using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.UI.Other
{
	public class ClasAccessorySlot : ModAccessorySlot
	{
        public override string FunctionalTexture => "ApacchiisClassesMod2/Assets/Class";
        public override string VanityTexture => "ApacchiisClassesMod2/Assets/Relic";
		public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back7"; //7
		public override string VanityBackgroundTexture => "Terraria/Images/Inventory_Back11";
		public override bool DrawVanitySlot => true;
		public override bool DrawDyeSlot => false;

		bool hasUpdatedAccessory = false; //Disable and enable the class accessory slot to prevent a bug where 
                                          //players need to re-equip the class banner in order for it to level up in multiplayer

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			if (context == AccessorySlotType.FunctionalSlot && checkItem.GetGlobalItem<ACMGlobalItem>().isClass)
				return true;
			
			if (context == AccessorySlotType.VanitySlot && checkItem.GetGlobalItem<ACMGlobalItem>().isRelic || context == AccessorySlotType.VanitySlot && checkItem.GetGlobalItem<ACMGlobalItem>().isCraftableRelic)
				return true;

			//if (context == AccessorySlotType.DyeSlot && checkItem.GetGlobalItem<ACMGlobalItem>().isRelic)
			//	return true;

			return false;
		}

		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			if (item.GetGlobalItem<ACMGlobalItem>().isClass)
				return true;

			return false;
		}

        public override bool IsEnabled()
        {
            if (!hasUpdatedAccessory)
			{
                hasUpdatedAccessory = true;
				return false;
			}
			else
			{ return true; }
        }

        public override void OnMouseHover(AccessorySlotType context)
		{
			switch (context)
			{
				case AccessorySlotType.FunctionalSlot:
					Main.hoverItemName = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.ClassPrefix")}";
					break;
				case AccessorySlotType.VanitySlot:
					Main.hoverItemName = $"Relic";
					break;
				//case AccessorySlotType.DyeSlot:
				//	Main.hoverItemName = $"Relic";
				//	break;
			}
		}
	}
}