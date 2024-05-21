using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class Sacrifice : ModItem
	{
        public string desc = "" +
                             "[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, Hili!][c/e796e8:]]";
        
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Quest;

            Item.GetGlobalItem<ACMGlobalItem>().isRelic = true;
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc;
        }


        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasSacrifice = true;

            base.UpdateVanity(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
                if (line.Mod == "Terraria" && line.Name == "Equipable")
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableRelic")}";

            TooltipLine description0 = new TooltipLine(Mod, "RelicDescription", "Halves your max minions and grants you bonuses based on minion slots sacrificed:\n(Minion slots are rounded down. Only removes a max of 4 slots)");
            TooltipLine description1 = new TooltipLine(Mod, "RelicDescription", "");
            TooltipLine description2 = new TooltipLine(Mod, "RelicDescription", "");
            TooltipLine description3 = new TooltipLine(Mod, "RelicDescription", "");
            TooltipLine description4 = new TooltipLine(Mod, "RelicDescription", "");
            TooltipLine dono = new TooltipLine(Mod, "RelicDescription", "[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, SROSirFDrake!][c/e796e8:]]");


            var modplayer = Main.player[Main.myPlayer].GetModPlayer<ACMPlayer>();
            if (modplayer.minionSlotsSacrificed >= 1)
                description1.Text = "[c/419e3c:- 1 Slot: Whip damage increased by 10%]";
            else
                description1.Text = "[c/d63838:- 1 Slot: Whip damage increased by 10%]";

            if (modplayer.minionSlotsSacrificed >= 2)
                description2.Text = "[c/419e3c:- 2 Slots: Whips now deal 10 damage per second for 3 seconds]";
            else
                description2.Text = "[c/d63838:- 2 Slots: Whips now deal 10 damage per second for 3 seconds]";

            if (modplayer.minionSlotsSacrificed >= 3)
                description3.Text = "[c/419e3c:- 3 Slots: Whip damage increased by an additional 10%]";
            else
                description3.Text = "[c/d63838:- 3 Slots: Whip damage increased by an additional 10%]";

            if (modplayer.minionSlotsSacrificed >= 4)
                description4.Text = "[c/419e3c:- 4 Slots: Whips now deal an additional 10 damage per second for 3 seconds]";
            else
                description4.Text = "[c/d63838:- 4 Slots: Whips now deal an additional 10 damage per second for 3 seconds]";

            tooltips.Add(description0);
            tooltips.Add(description1);
            tooltips.Add(description2);
            tooltips.Add(description3);
            tooltips.Add(description4);
            tooltips.Add(dono);

            base.ModifyTooltips(tooltips);
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (!modded)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

