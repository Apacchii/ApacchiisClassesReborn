using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Items.Relics;
using static Terraria.ModLoader.ModContent;
using Terraria.Utilities;

namespace ApacchiisClassesMod2
{
	public class ACMGlobalItem : GlobalItem
	{
        Player Player = Main.player[Main.myPlayer];

        public bool isClass = false;
        public bool isRelic = false;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }

        public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
        {
            // Classes and Relics cannot have prefixes at all
            if (pre == -1)
                if (isClass || isRelic)
                    return false;
            if (pre == -2)
                if (isClass || isRelic)
                    return false;
            if (pre == -3)
                if (isClass || isRelic)
                    return false;

            return base.PrefixChance(item, pre, rand);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Code for removing "Social" descriptions and allow Relics to be used in vanity slots while displaying their text normally
            #region Relics
            if (item.type == ItemType<CursedCandle>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<CursedCandle>().desc;
            }

            if (item.type == ItemType<VoidMirror>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<VoidMirror>().desc;
            }

            if (item.type == ItemType<BrokenHeart>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<BrokenHeart>().desc;
            }

            if (item.type == ItemType<BleedingMoonStone>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<BleedingMoonStone>().desc;
            }

            if (item.type == ItemType<BloodGem>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<BloodGem>().desc;
            }

            if (item.type == ItemType<AghanimsScepter>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<AghanimsScepter>().desc;
            }

            if (item.type == ItemType<NiterihsRing>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<NiterihsRing>().desc;
            }

            if (item.type == ItemType<NiterihsNecklace>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<NiterihsNecklace>().desc;
            }

            if (item.type == ItemType<NiterihsBracelet>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<NiterihsBracelet>().desc;
            }

            if (item.type == ItemType<NiterihsEarring>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<NiterihsEarring>().desc;
            }

            if (item.type == ItemType<UnstableConcoction>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<UnstableConcoction>().desc;
            }

            if (item.type == ItemType<StrangeMushroom>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<StrangeMushroom>().desc;
            }

            if (item.type == ItemType<MushroomConcentrate>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<MushroomConcentrate>().desc;
            }

            if (item.type == ItemType<LeysMushroom>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<LeysMushroom>().desc;
            }

            if (item.type == ItemType<TearsOfLife>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<TearsOfLife>().desc;
            }

            if (item.type == ItemType<OldBelt>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<OldBelt>().desc;
            }

            if (item.type == ItemType<Croissant>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<Croissant>().desc;
            }

            if (item.type == ItemType<StrangeGem>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<StrangeGem>().desc;
            }

            if (item.type == ItemType<NiterihsLuckyToken>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "SocialDesc")
                        line.text = GetInstance<NiterihsLuckyToken>().desc;
            }
            #endregion
        }
    }
}