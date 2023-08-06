using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Items.Relics;

namespace ApacchiisClassesMod2
{
	public class GlobalVanillaItemChanges : GlobalItem
	{
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

        public override void SetDefaults(Item item)
        {
            //if (item.healLife > 0)
            //    item.healLife *= 2;
            base.SetDefaults(item);
        }

        /// <summary>
        /// Adds a new line after the specified line of a vanilla item. UNUSED
        /// </summary>
        void UpdateItemText(int itemToUpdate, int lineToUpdate, string text, List<TooltipLine> tooltips, Item item)
        {
                if (item.type == itemToUpdate)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == $"Tooltip{lineToUpdate}")
                        line.Text += $"\n{text}";
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            #region Vanilla
            if (item.type == ItemID.RangerEmblem || item.type == ItemID.SorcererEmblem || item.type == ItemID.SummonerEmblem || item.type == ItemID.WarriorEmblem)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n3% increased ability power";

            if (item.type == ItemID.EyeoftheGolem)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n8% increased ability power";
            if (item.type == ItemID.AvengerEmblem)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n10% increased ability power";

            if (item.type == ItemID.DestroyerEmblem)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n10% increased ability power\n4% increased cooldown reduction";

            if (item.type == ItemID.CobaltShield)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n3% decreased ultimate cost";

            if (item.type == ItemID.ObsidianShield)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n2% increased cooldown reduction\n4% decreased ultimate cost";

            if (item.type == ItemID.CrossNecklace)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n3% increased cooldown reduction";

            if (item.type == ItemID.StarVeil)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n5% increased cooldown reduction";

            if (item.type == ItemID.RifleScope)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n6% increased cooldown reduction";

            if (item.type == ItemID.SniperScope)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n8% increased ability power\n7% increased cooldown reduction";

            if (item.type == ItemID.ReconScope)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n10% increased ability power\n8% increased cooldown reduction";

            if (item.type == ItemID.AnkletoftheWind)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% decreased ultimate cost";

            if (item.type == ItemID.Aglet)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% increased ability power";

            if (item.type == ItemID.HermesBoots)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% increased cooldown reduction";
            if (item.type == ItemID.RocketBoots)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% increased cooldown reduction";
            if (item.type == ItemID.SpectreBoots)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n2% increased cooldown reduction";
            if (item.type == ItemID.LightningBoots)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n4% increased cooldown reduction\n3% increased ability power\n3% decreased ultimate cost";
            if (item.type == ItemID.FrostsparkBoots)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n4% increased cooldown reduction\n4% increased ability power\n3% decreased ultimate cost";
            if (item.type == ItemID.TerrasparkBoots)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip4")
                        line.Text += "\n5% increased cooldown reduction\n5% increased ability power\n5% decreased ultimate cost";

            if (item.type == ItemID.FairyBoots)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip2")
                        line.Text += "\n2% increased cooldown reduction";

            if(item.wingSlot > -1)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n2% decreased ultimate cost";

            if (item.type == ItemID.MedicatedBandage)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n2% increased ability power";

            if (item.type == ItemID.ArmorBracing)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n2% increased ability power";

            if (item.type == ItemID.ThePlan)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n2% increased ability power";

            if (item.type == ItemID.CountercurseMantra)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n2% increased ability power";

            if (item.type == ItemID.Blindfold)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n2% increased ability power";

            if (item.type == ItemID.AnkhCharm)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n10% increased ability power";

            if (item.type == ItemID.AnkhShield)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n12% increased ability power\n3% increased cooldown reduction\n5% decreased ultimate cost";

            if (item.type == ItemID.NaturesGift)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% increased cooldown reduction";

            if (item.type == ItemID.ManaFlower)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n2% increased cooldown reduction";

            if (item.type == ItemID.ArcaneFlower)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip2")
                        line.Text += "\n3% increased cooldown reduction";

            if (item.type == ItemID.BandofRegeneration)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% decreased ultimate cost";

            if (item.type == ItemID.BandofStarpower)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% increased ability power";

            if (item.type == ItemID.ManaRegenerationBand)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n1% increased ability power\n1% decreased ultimate cost";

            if (item.type == ItemID.PhilosophersStone)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% increased cooldown reduction";

            if (item.type == ItemID.CharmofMyths)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                        line.Text += "\n1% increased cooldown reduction\n1% decreased ultimate cost";

            if (item.type == ItemID.MagicCuffs)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Text += "\n2% increased ability power\n2% decreased ultimate cost";

            if (item.type == ItemID.CelestialCuffs)
                foreach (TooltipLine line in tooltips)
                    if (line.Mod == "Terraria" && line.Name == "Tooltip2")
                        line.Text += "\n3% increased ability power\n3% decreased ultimate cost";
            #endregion

            base.ModifyTooltips(item, tooltips);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            if (item.type == ItemID.RangerEmblem || item.type == ItemID.SorcererEmblem || item.type == ItemID.SummonerEmblem || item.type == ItemID.WarriorEmblem)
                acmPlayer.abilityPower += .03f;

            if (item.type == ItemID.BandofRegeneration)
                acmPlayer.ultCooldownReduction -= .01f;

            if (item.type == ItemID.BandofStarpower)
                acmPlayer.abilityPower += .01f;

            if (item.type == ItemID.ManaRegenerationBand)
            {
                acmPlayer.ultCooldownReduction -= .01f;
                acmPlayer.abilityPower += .01f;
            }

            if (item.type == ItemID.PhilosophersStone)
                acmPlayer.cooldownReduction -= .01f;

            if (item.type == ItemID.CharmofMyths)
            {
                acmPlayer.ultCooldownReduction -= .01f;
                acmPlayer.cooldownReduction -= .01f;
            }

            if (item.type == ItemID.MagicCuffs)
            {
                acmPlayer.ultCooldownReduction -= .02f;
                acmPlayer.abilityPower += .02f;
            }

            if (item.type == ItemID.CelestialCuffs)
            {
                acmPlayer.ultCooldownReduction -= .03f;
                acmPlayer.abilityPower += .03f;
            }

            if (item.type == ItemID.NaturesGift)
                acmPlayer.cooldownReduction -= .01f;

            if (item.type == ItemID.ManaFlower)
                acmPlayer.cooldownReduction -= .02f;

            if (item.type == ItemID.ArcaneFlower)
                acmPlayer.cooldownReduction -= .03f;

            if (item.type == ItemID.MedicatedBandage)
                acmPlayer.abilityPower += .02f;
            if (item.type == ItemID.ArmorBracing)
                acmPlayer.abilityPower += .02f;
            if (item.type == ItemID.ThePlan)
                acmPlayer.abilityPower += .02f;
            if (item.type == ItemID.CountercurseMantra)
                acmPlayer.abilityPower += .02f;
            if (item.type == ItemID.Blindfold)
                acmPlayer.abilityPower += .02f;
            if (item.type == ItemID.AnkhCharm)
                acmPlayer.abilityPower += .1f;
            if (item.type == ItemID.AnkhShield)
            {
                acmPlayer.abilityPower += .12f;
                acmPlayer.cooldownReduction -= .03f;
                acmPlayer.ultCooldownReduction -= .05f;
            }

            if (item.type == ItemID.EyeoftheGolem)
                acmPlayer.abilityPower += .08f;
            if (item.type == ItemID.AvengerEmblem)
                acmPlayer.abilityPower += .1f;

            if (item.type == ItemID.DestroyerEmblem)
            {
                acmPlayer.abilityPower += .1f;
                acmPlayer.cooldownReduction -= .04f;
            }

            if (item.type == ItemID.CobaltShield)
                acmPlayer.ultCooldownReduction -= .04f;

            if (item.type == ItemID.ObsidianShield)
            {
                acmPlayer.ultCooldownReduction -= .04f;
                acmPlayer.cooldownReduction -= .02f;
            }
                
            if (item.type == ItemID.CrossNecklace)
                acmPlayer.cooldownReduction -= .05f;

            if (item.type == ItemID.StarVeil)
                acmPlayer.cooldownReduction -= .04f;

            if (item.type == ItemID.SniperScope)
                acmPlayer.cooldownReduction -= .06f;

            if (item.type == ItemID.SniperScope)
            {
                acmPlayer.cooldownReduction -= .07f;
                acmPlayer.abilityPower += .08f;
            }

            if (item.type == ItemID.ReconScope)
            {
                acmPlayer.cooldownReduction -= .08f;
                acmPlayer.abilityPower += .1f;
            }

            if (item.type == ItemID.AnkletoftheWind)
                acmPlayer.ultCooldownReduction -= .01f;

            if (item.type == ItemID.HermesBoots)
                acmPlayer.cooldownReduction -= .01f;
            if (item.type == ItemID.RocketBoots)
                acmPlayer.cooldownReduction -= .01f;
            if (item.type == ItemID.SpectreBoots)
                acmPlayer.cooldownReduction -= .02f;
            if (item.type == ItemID.LightningBoots)
            {
                acmPlayer.cooldownReduction -= .04f;
                acmPlayer.abilityPower += .03f;
                acmPlayer.ultCooldownReduction -= .02f;
            }
            if (item.type == ItemID.FrostsparkBoots)
            {
                acmPlayer.cooldownReduction -= .04f;
                acmPlayer.abilityPower += .04f;
                acmPlayer.ultCooldownReduction -= .02f;
            }
            if (item.type == ItemID.TerrasparkBoots)
            {
                acmPlayer.cooldownReduction -= .05f;
                acmPlayer.abilityPower += .05f;
                acmPlayer.ultCooldownReduction -= .05f;
            }

            if (item.type == ItemID.FairyBoots)
                acmPlayer.cooldownReduction -= .02f;

            if (item.wingSlot > -1)
                acmPlayer.ultCooldownReduction -= .02f;

            base.UpdateAccessory(item, player, hideVisual);
        }

        //public override void GetHealLife(Item item, Player player, bool quickHeal, ref int healValue)
        //{
        //    healValue *= 2;
        //    base.GetHealLife(item, player, quickHeal, ref healValue);
        //}
    }
}