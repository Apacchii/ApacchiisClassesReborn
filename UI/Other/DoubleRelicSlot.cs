using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;

namespace ApacchiisClassesMod2.UI.Other
{
	public class DoubleRelicSlot : ModAccessorySlot
	{
		public override bool IsEnabled() => _ACMConfigServer.Instance.doubleRelics;
        public override bool IsHidden() => !_ACMConfigServer.Instance.doubleRelics;
		public override bool DrawFunctionalSlot => false;
		public override bool DrawVanitySlot => _ACMConfigServer.Instance.doubleRelics;
		public override bool DrawDyeSlot => false;
		public override string VanityTexture => "ApacchiisClassesMod2/Assets/Relic";
		public override string VanityBackgroundTexture => "Terraria/Images/Inventory_Back11"; //7

		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			if (context == AccessorySlotType.VanitySlot && checkItem.GetGlobalItem<ACMGlobalItem>().isRelic)
				return true;

			return false;
		}

		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			return false;
		}

        public override void OnMouseHover(AccessorySlotType context)
		{
			switch (context)
			{
				case AccessorySlotType.VanitySlot:
					Main.hoverItemName = $"Relic";
					break;
			}
		}
	}
}