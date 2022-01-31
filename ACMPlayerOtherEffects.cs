using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ApacchiisClassesMod2
{
    public class ACMPlayerOtherEffects : ModPlayer
	{
        #region Relics
        public bool hasRelic;

        public bool hasBrokenHeart;
        public bool hasBloodGem;
        public bool hasLeysMushroom;
        public bool hasStrangeMushroom;
        public bool hasMushroomConcentrate;
        public bool hasBerserkersBrew;
        #endregion

        int bloodGemProjectileTimer = 0;
        int bloodGemMeleeTimer = 0;

        float leysMushroomBuffChance = .1f;
        float leysMushroomHealChance = .04f;
        float leysMushroomHeal = .03f;

        public override void ResetEffects()
        {
            hasRelic = false;

            hasBrokenHeart = false;
            hasBloodGem = false;
            hasLeysMushroom = false;
            hasStrangeMushroom = false;
            hasMushroomConcentrate = false;
            hasBerserkersBrew = false;

            base.ResetEffects();
        }

        public override void PreUpdate()
        {
            if (bloodGemProjectileTimer > 0)
                bloodGemProjectileTimer--;

            if (bloodGemMeleeTimer > 0)
                bloodGemMeleeTimer--;
            base.PreUpdate();
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (hasBrokenHeart && healValue >= 50 && item.type != ItemID.Mushroom)
                healValue += (int)(Player.statLifeMax2 * .07f);

            if (hasMushroomConcentrate && item.type == ItemID.Mushroom)
            {
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * .06f);
                healValue = 60 + heal;
            }

            if (hasBerserkersBrew && item.type != ItemID.Mushroom)
            {
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * .35f);
                healValue += heal;
            }

            base.GetHealLife(item, quickHeal, ref healValue);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            // Blood Gem
            if(bloodGemMeleeTimer <= 0 && hasBloodGem)
            {
                int heal = (int)(Player.statLifeMax2 * .02f);
                if(Player.statLife < Player.statLifeMax2)
                {
                    Player.statLife += heal;
                    Player.HealEffect(heal);
                    bloodGemMeleeTimer = 120;
                }  
            }

            // Ley's Mushroom Heal
            if(Main.rand.NextFloat() < leysMushroomHealChance && hasLeysMushroom)
            {
                if (Main.hardMode) leysMushroomHeal = .04f;
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * leysMushroomHeal);
                Player.statLife += heal;
                Player.HealEffect(heal);
            }

            //Ley's Mushroom Buffs
            if (Main.rand.NextFloat() < leysMushroomBuffChance && hasLeysMushroom)
            {
                int duration = 60 * 3;
                if (Main.hardMode) duration = 60 * 4;

                int buff = Main.rand.Next(3);
                if (buff == 0)
                    Player.AddBuff(BuffType<Buffs.LeysDamage>(), duration);
                if (buff == 1)
                    Player.AddBuff(BuffType<Buffs.LeysCrit>(), duration);
                if (buff == 2)
                    Player.AddBuff(BuffType<Buffs.LeysEndurance>(), duration);
            }
            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            base.OnHitByProjectile(proj, damage, crit);
        }

        // Modify hit by collision
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            damage = (int)(damage * GetInstance<ACMConfigServer>().enemyDamageMultiplier);

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.TrainingRapier>())
                    damage -= 6;

            base.ModifyHitByNPC(npc, ref damage, ref crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            // Blood Gem
            if (bloodGemProjectileTimer <= 0 && hasBloodGem)
            {
                int heal = (int)(Player.statLifeMax2 * .01f);
                if (Player.statLife < Player.statLifeMax2)
                {
                    Player.statLife += heal;
                    Player.HealEffect(heal);
                    bloodGemProjectileTimer = 240;
                }
            }

            // Ley's Mushroom Heal
            if (Main.rand.NextFloat() < leysMushroomHealChance && hasLeysMushroom)
            {
                if (Main.hardMode) leysMushroomHeal = .05f;
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * leysMushroomHeal);
                Player.statLife += heal;
                Player.HealEffect(heal);
            }

            //Ley's Mushroom Buffs
            if (Main.rand.NextFloat() < leysMushroomBuffChance && hasLeysMushroom)
            {
                int duration = 60 * 3;
                if (Main.hardMode) duration = 60 * 4;

                int buff = Main.rand.Next(3);
                if (buff == 0)
                    Player.AddBuff(BuffType<Buffs.LeysDamage>(), duration);
                if(buff == 1)
                    Player.AddBuff(BuffType<Buffs.LeysCrit>(), duration);
                if (buff == 2)
                    Player.AddBuff(BuffType<Buffs.LeysEndurance>(), duration);
            }
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }

        // Modify hit by projectile
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            damage = (int)(damage * GetInstance<ACMConfigServer>().enemyDamageMultiplier);

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.TrainingRapier>())
                    damage -= 14;
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {

            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.FadingDagger>() && proj.type == ProjectileType<Projectiles.Weapons.FadingDagger>() && crit)
                damage += 4;
            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.MasteredFadingDagger>() && proj.type == ProjectileType<Projectiles.Weapons.FadingDagger>() && crit)
                damage += 10;
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}