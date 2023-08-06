using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Items.Relics;
using static Terraria.ModLoader.ModContent;
using Terraria.Utilities;
using Terraria.ModLoader.IO;
using System.Security.Principal;

namespace ApacchiisClassesMod2
{
	public class ACMGlobalItem : GlobalItem
	{
        public bool isClass = false;
        public bool isRelic = false;
        public bool isCraftableRelic = false;
        public bool isMaterialRelic = false;

        public string desc;

        #region Obsolete
        //ItemType<AghanimsScepter>(),
        //ItemType<BerserkersBrew>(),
        //ItemType<BleedingMoonStone>(),
        //ItemType<BloodGem>(),
        //ItemType<BrokenHeart>(),
        //ItemType<Croissant>(),
        //ItemType<CursedCandle>(),
        //ItemType<LeysMushroom>(),
        //ItemType<MushroomConcentrate>(),
        //ItemType<NiterihsBracelet>(),
        //ItemType<NiterihsEarring>(),
        //ItemType<NiterihsLuckyToken>(),
        //ItemType<NiterihsNecklace>(),
        //ItemType<NiterihsRing>(),
        //ItemType<OldBelt>(),
        //ItemType<StrangeGem>(),
        //ItemType<StrangeMushroom>(),
        //ItemType<TearsOfLife>(),
        //ItemType<UnstableConcoction>(),
        //ItemType<VoidMirror>()
        #endregion

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
                if (isClass || isRelic || isCraftableRelic)
                    return false;
            if (pre == -2)
                if (isClass || isRelic || isCraftableRelic)
                    return false;
            if (pre == -3)
                if (isClass || isRelic || isCraftableRelic)
                    return false;

            return base.PrefixChance(item, pre, rand);
        }

        public override bool? UseItem(Item item, Player player)
        {
            Player Player = Main.player[Main.myPlayer];
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();

            if (acmPlayer.bloodMageBloodEnchantment && player.HeldItem.DamageType == DamageClass.Magic)
                acmPlayer.bloodMageEnchantmentOnCooldown = false;
            return base.UseItem(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Code for removing "Social" descriptions and allow Relics to be used in vanity slots while displaying their text normally
            if (isRelic || isCraftableRelic) // == ItemType<NiterihsNecklace>()
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social" || x.Name == "SocialDesc");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                            line.Text = "";

                //This one replaces the "Material" tooltip for one saying "Material Relic"
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Material")
                        line.Text = "[c/a16ce6:[Material Relic][c/a16ce6:]]";
            }

            #region Relics
            /*
            if (item.type == ItemType<CursedCandle>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = "";
            }
            
            if (item.type == ItemType<VoidMirror>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = "";
            }
            
            if (item.type == ItemType<BrokenHeart>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = "";
            }
            
            if (item.type == ItemType<BleedingMoonStone>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = "";
            }
            
            if (item.type == ItemType<BloodGem>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = "";
            }
            
            if (item.type == ItemType<AghanimsScepter>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = "";
            }
            
            if (item.type == ItemType<NiterihsRing>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = "";
            }
            
            if (item.type == ItemType<NiterihsBracelet>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<NiterihsBracelet>().desc;
            }
            
            if (item.type == ItemType<NiterihsEarring>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<NiterihsEarring>().desc;
            }
            
            if (item.type == ItemType<UnstableConcoction>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<UnstableConcoction>().desc;
            }
            
            if (item.type == ItemType<StrangeMushroom>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<StrangeMushroom>().desc;
            }
            
            if (item.type == ItemType<MushroomConcentrate>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<MushroomConcentrate>().desc;
            }
            
            if (item.type == ItemType<LeysMushroom>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<LeysMushroom>().desc;
            }
            
            if (item.type == ItemType<TearsOfLife>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<TearsOfLife>().desc;
            }
            
            if (item.type == ItemType<OldBelt>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<OldBelt>().desc;
            }
            
            if (item.type == ItemType<Croissant>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<Croissant>().desc;
            }
            
            if (item.type == ItemType<StrangeGem>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<StrangeGem>().desc;
            }
            
            if (item.type == ItemType<NiterihsLuckyToken>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<NiterihsLuckyToken>().desc;
            }
            
            if (item.type == ItemType<BerserkersBrew>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<BerserkersBrew>().desc;
            }
            
            if (item.type == ItemType<SleepingBabySqueaker>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<SleepingBabySqueaker>().desc;
            }
            
            if (item.type == ItemType<OldShield>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<OldShield>().desc;
            }
            
            if (item.type == ItemType<ChocolatePudding>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<ChocolatePudding>().desc;
            }
            
            if (item.type == ItemType<LuckyLeaf>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<LuckyLeaf>().desc;
            }
            
            if (item.type == ItemType<WornGloves>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<WornGloves>().desc;
            }
            
            if (item.type == ItemType<ArcaneBlade>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<ArcaneBlade>().desc;
            }
            
            if (item.type == ItemType<ManaBag>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<ManaBag>().desc;
            }
            
            if (item.type == ItemType<Hourglass>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<Hourglass>().desc;
            }
            
            if (item.type == ItemType<Stopwatch>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<Stopwatch>().desc;
            }
            
            if (item.type == ItemType<DarkSign>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<DarkSign>().desc;
            }
            
            if (item.type == ItemType<OldBlood>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<OldBlood>().desc;
            }
            
            if (item.type == ItemType<PocketSlime>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<PocketSlime>().desc;
            }
            
            if (item.type == ItemType<Nessie>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<Nessie>().desc;
            }
            
            if (item.type == ItemType<TheosCollar>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<TheosCollar>().desc;
            }
            
            if (item.type == ItemType<ChocolateBar>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<ChocolateBar>().desc;
            }

            if (item.type == ItemType<AccountantRat>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<AccountantRat>().desc;
            }

            if (item.type == ItemType<ChaosAccelerant>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<ChaosAccelerant>().desc;
            }

            if (item.type == ItemType<EldritchInoculation>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<EldritchInoculation>().desc;
            }

            if (item.type == ItemType<ScalingWarbanner>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<ScalingWarbanner>().desc;
            }

            if (item.type == ItemType<MinionsRightsContract>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<MinionsRightsContract>().desc;
            }

            if (item.type == ItemType<Peanut>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<Peanut>().desc;
            }

            if (item.type == ItemType<RapidFirecannon>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<RapidFirecannon>().desc;
            }

            if (item.type == ItemType<TornWhipPart>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<TornWhipPart>().desc;
            }

            if (item.type == ItemType<DerelictCactusRing>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<DerelictCactusRing>().desc;
            }

            if (item.type == ItemType<ManaInhibitor>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<ManaInhibitor>().desc;
            }

            if (item.type == ItemType<PorcelainMask>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<PorcelainMask>().desc;
            }

            if (item.type == ItemType<MajorsCare>())
            {
                tooltips.RemoveAll(x => x.Name == "Terraria" || x.Name == "Social");
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "SocialDesc")
                        line.Text = GetInstance<MajorsCare>().desc;
            }
            */
            #endregion
        }
    }
}