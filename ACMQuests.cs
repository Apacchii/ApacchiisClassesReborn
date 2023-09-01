using ApacchiisClassesMod2.Configs;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace ApacchiisClassesMod2
{
	public class ACMQuests : ModPlayer
    {
        public int questsCompleted;

        public string chosenQuest = "";
        public string questName;
        public string questDesc;
        bool _canCompleteQuest = false;

        string _worldEvil; // "Crimson"/"Corruption"
        public int chosenQuestReward = -573; //Random number. Unused, for now

        float _questDifficultyMultiplier = 1f;
        float _questRewardMultiplier = 1f;

        int _qLumberjackToCompleteBase = 70;
        int _qMasonrySuppliesToCompleteBase = 150;
        int _qMinerSuppliesToCompleteBase = 6;
        int _qHallowedSuppliesToCompleteBase = 4;

        public int qSlayerCount;
        public int qSlayerCountToComplete;
        int _qSlayerCountToCompleteBase = 150;

        public int qUnicornHunterCount;
        public int qUnicornHunterToComplete;
        int _qUnicornHunterToCompleteBase = 1;

        public int qBossSlayerCount;
        public int qBossSlayerToComplete;
        int _qBossSlayerToCompleteBase = 1;

        public int qCrimsonHunterCount;
        public int qCrimsonHunterToComplete;
        int _qCrimsonHunterToCompleteBase = 80;

        int _qMinerSuppliesBarSelected; //0 = copp/tin, 1 = iron/lead, 2 = silv/tung, 3 = gold/plat
        int _qCritterCollectorSelected;
        int _qTurnInSelected;
        int _qJewelryRequestSelected;
        int _qJewelryRequestToCompleteBase = 2;
        int _qPlantsVsDeadSelected;
        int _qPlantsVsDeadToCompleteBase = 2;
        int _qBuildingMaterialsWoodToCompleteBase = 60;
        int _qBuildingMaterialsWoodSelected;
        int _qBuildingMaterialsSecondBlockToCompleteBase = 80;
        int _qBuildingMaterialsSecondBlockSelected;
        int _qBuildingMaterialsTorchesToCompleteBase = 30;

        public int qBountyHunterCount;
        int _qBountyHunterSelected;

        private string[] _preHardmodeQuests =
        {
            "Slayer", //Defeat enemies
            "Lumberjack", //Turn in wood
            "Masonry Supplies", //Turn in stone
            "Miner Supplies", //Turn in randomly chosen ore bars"
            "Jewelry Request", //Turn in a random gem
            "Critter Collector", //Turn in a random critter
            "Plants vs Undead", //Turn in random plants
        };

        private string[] _hardmodeQuests =
        {
            "Unicorn Hunter", //Defeat unicorn
            "Boss Slayer", //Defeat any boss
            "Hallowed Supplies", //Turn in hallowed bars
            "Turn In", //Turn in a random item
            "Building Materials", //Turn in two types of blocks and torches
            "Evil Hunter", //Defeat enemies while in an evil biome
            //"Bounty Hunter", //Defeat a rare enemy
        };

        public List<string> questList = new List<string>
        {
        };

        public List<int> qBountyHunter = new List<int>
        {
            NPCID.Tim,
            NPCID.UndeadMiner,
            NPCID.Mimic,
            NPCID.RuneWizard,
            NPCID.Nymph,
            NPCID.Moth,
            NPCID.IceGolem,
            NPCID.Paladin,
            NPCID.BigMimicHallow,
            NPCID.BigMimicJungle,
            NPCID.Medusa,
            NPCID.SandElemental,
            NPCID.IceMimic,
            NPCID.DoctorBones,
        };

        public List<int> qBuildingMaterialsWood = new List<int>
        {
            ItemID.Wood,
            ItemID.RichMahogany,
            ItemID.Pearlwood,
            ItemID.BorealWood,
            ItemID.PalmWood,
        };
        public List<int> qBuildingMaterialsSecondBlock = new List<int>
        {
            ItemID.GrayBrick,
            ItemID.Granite,
            ItemID.Marble,
            ItemID.RedBrick,
            ItemID.SnowBrick,
        };

        public List<int> qTurnInItemList = new List<int>
        {
            ItemID.LifeCrystal,
            ItemID.ManaCrystal,
            ItemID.ZombieArm,
            ItemID.Shackle,
            ItemID.Lens,
            ItemID.WoodenCrate,
            ItemID.BorealWoodSword,
            ItemID.BorealWoodBow,
            ItemID.AbigailsFlower,
            ItemID.HealingPotion,
            ItemID.IronskinPotion,
            ItemID.RegenerationPotion,
            ItemID.RecallPotion,
            ItemID.Harpoon,
        };

        public List<int> qJewelryRequestList = new List<int>
        {
            ItemID.Amethyst,
            ItemID.Topaz,
            ItemID.Sapphire,
            ItemID.Emerald,
            ItemID.Ruby,
            ItemID.Amber,
            ItemID.Diamond,
        };

        //Ammo request quest ? Maybe ask for bullets and arrows at the same time ?
        //public List<int> AMMOLIST_BULLET/ARROW = new List<int>
        //{
        //};

        public List<int> qPlantsVsUndeadList = new List<int>
        {
            ItemID.Daybloom,
            ItemID.Blinkroot,
            ItemID.Deathweed,
            ItemID.Fireblossom,
            ItemID.Shiverthorn,
            ItemID.Moonglow,
            ItemID.Waterleaf,
            ItemID.Mushroom,
            ItemID.GlowingMushroom,
        };

        public List<int> qCritterCollectorList = new List<int>
        {
            ItemID.Bunny,
            ItemID.Bird,
            ItemID.BlueJay,
            ItemID.BlueMacaw,
            ItemID.Buggy,
            ItemID.Cardinal,
            ItemID.Duck,
            ItemID.MallardDuck,
            ItemID.Firefly,
            ItemID.Frog,
            ItemID.Goldfish,
            ItemID.Grasshopper,
            ItemID.GrayCockatiel,
            ItemID.Grubby,
            ItemID.LadyBug,
            ItemID.Maggot,
            ItemID.Mouse,
            ItemID.Owl,
            ItemID.Penguin,
            ItemID.Rat,
            ItemID.ScarletMacaw,
            ItemID.Scorpion,
            ItemID.BlackScorpion,
            ItemID.Seagull,
            ItemID.Turtle,
            ItemID.Sluggy,
            ItemID.Snail,
            ItemID.Stinkbug,
            ItemID.Toucan,
            ItemID.Worm,
            ItemID.YellowCockatiel,
            ItemID.JuliaButterfly,
            ItemID.MonarchButterfly,
            ItemID.PurpleEmperorButterfly,
            ItemID.RedAdmiralButterfly,
            ItemID.SulphurButterfly,
            ItemID.TreeNymphButterfly,
            ItemID.UlyssesButterfly,
            ItemID.ZebraSwallowtailButterfly,
        };

        //Unused, for now
        //private int[] _rewardList =
        //{
        //    ItemType<Items.LostRuneFragment>(),
        //};

        public override void PreUpdate()
        {
            if (Main.time == 0 && Main.dayTime)
            {
                ResetQuests();
                SelectNewQuest();
                if(ACMConfigClient.Instance.questChatMessage)
                    Main.NewText($"A new daily quest is now available: [{chosenQuest}]");
            }

            //Remove the text from the HUD in order to "hide" it
            if(chosenQuest == "")
            {
                questName = "";
                questDesc = "";
            }

            //Kill enemies
            if (chosenQuest == "Slayer")
            {
                int obj = (int)(_qSlayerCountToCompleteBase * _questDifficultyMultiplier);
                qSlayerCountToComplete = obj;

                questName = chosenQuest;
                questDesc = $"Defeat {qSlayerCount}/{qSlayerCountToComplete} enemies.";
            }

            //Turn in wood
            if(chosenQuest == "Lumberjack")
            {
                int obj = (int)(_qLumberjackToCompleteBase * _questDifficultyMultiplier);
                int woodAmount = 0;

                for(int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.Wood)
                        woodAmount += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {woodAmount}/{obj} Wood[i:{ItemID.Wood}].";
            }

            //Turn in stone
            if (chosenQuest == "Masonry Supplies")
            {
                int obj = (int)(_qMasonrySuppliesToCompleteBase * _questDifficultyMultiplier);
                int stoneAmount = 0;

                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.StoneBlock)
                        stoneAmount += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {stoneAmount}/{obj} Stone Blocks[i:{ItemID.StoneBlock}].";
            }

            //Turn in stone
            if (chosenQuest == "Miner Supplies")
            {
                int obj = (int)(_qMinerSuppliesToCompleteBase * _questDifficultyMultiplier);
                int ingotAmount = 0;
                
                if(_qMinerSuppliesBarSelected == 0)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.CopperBar || Player.inventory[i].type == ItemID.TinBar)
                            ingotAmount += Player.inventory[i].stack;
                    questDesc = $"Turn in {ingotAmount}/{obj} Copper[i:{ItemID.CopperBar}]/Tin[i:{ItemID.TinBar}] Bars.";
                }

                if (_qMinerSuppliesBarSelected == 1)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.IronBar || Player.inventory[i].type == ItemID.LeadBar)
                            ingotAmount += Player.inventory[i].stack;
                    questDesc = $"Turn in {ingotAmount}/{obj} Iron[i:{ItemID.IronBar}]/Lead[i:{ItemID.LeadBar}] Bars.";
                }

                if (_qMinerSuppliesBarSelected == 2)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.SilverBar || Player.inventory[i].type == ItemID.TungstenBar)
                            ingotAmount += Player.inventory[i].stack;
                    questDesc = $"Turn in {ingotAmount}/{obj} Silver[i:{ItemID.SilverBar}]/Tungsten[i:{ItemID.TungstenBar}] Bars.";
                }

                if (_qMinerSuppliesBarSelected == 3)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.GoldBar || Player.inventory[i].type == ItemID.PlatinumBar)
                            ingotAmount += Player.inventory[i].stack;
                    questDesc = $"Turn in {ingotAmount}/{obj} Gold[i:{ItemID.GoldBar}]/Platinum[i:{ItemID.PlatinumBar}] Bars.";
                }

                questName = chosenQuest;
            }

            //Defeat unicorn
            if (chosenQuest == "Unicorn Hunter")
            {
                int obj = (int)(_qUnicornHunterToCompleteBase * _questDifficultyMultiplier);
                qUnicornHunterToComplete = obj;

                questName = chosenQuest;
                questDesc = $"Defeat {qUnicornHunterCount}/{qUnicornHunterToComplete} Unicorn/s.";
            }

            //Defeat any boss
            if (chosenQuest == "Boss Slayer")
            {
                int obj = (int)(_qBossSlayerToCompleteBase * _questDifficultyMultiplier);
                qBossSlayerToComplete = obj;

                questName = chosenQuest;
                questDesc = $"Defeat any boss {qBossSlayerCount}/{qBossSlayerToComplete}.";
            }

            //Turn in Hallowed Bars
            if (chosenQuest == "Hallowed Supplies")
            {
                int obj = (int)(_qHallowedSuppliesToCompleteBase * _questDifficultyMultiplier);
                int barsAmount = 0;

                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.HallowedBar)
                        barsAmount += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {barsAmount}/{obj}[i:{ItemID.HallowedBar}].";
            }

            if(chosenQuest == "Critter Collector")
            {
                //Get the selected critter's item
                Item critter = new Item();
                try
                {
                    critter.SetDefaults(qCritterCollectorList[_qCritterCollectorSelected]);
                }
                catch
                {
                    critter.netDefaults(qCritterCollectorList[_qCritterCollectorSelected]);
                }

                int crittersInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == critter.type)
                        crittersInInv += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {crittersInInv}/1 {critter.Name}[i:{critter.type}].";
            }

            if (chosenQuest == "Turn In")
            {
                //Get the selected item
                Item itemToTurnIn = new Item();
                try
                {
                    itemToTurnIn.SetDefaults(qTurnInItemList[_qTurnInSelected]);
                }
                catch
                {
                    itemToTurnIn.netDefaults(qTurnInItemList[_qTurnInSelected]);
                }

                int itemToTurnInInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == itemToTurnIn.type)
                        itemToTurnInInInv += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {itemToTurnInInInv}/1 {itemToTurnIn.Name}[i:{itemToTurnIn.type}].";
            }

            if (chosenQuest == "Jewelry Request")
            {
                //Get the selected gem
                Item gemToTurnIn = new Item();
                try
                {
                    gemToTurnIn.SetDefaults(qJewelryRequestList[_qJewelryRequestSelected]);
                }
                catch
                {
                    gemToTurnIn.netDefaults(qJewelryRequestList[_qJewelryRequestSelected]);
                }

                int obj = (int)(_qJewelryRequestToCompleteBase * _questDifficultyMultiplier);
                int gemsToTurnInInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == gemToTurnIn.type)
                        gemsToTurnInInInv += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {gemsToTurnInInInv}/{obj} {gemToTurnIn.Name}[i:{gemToTurnIn.type}].";
            }

            if (chosenQuest == "Plants vs Undead")
            {
                //Get the selected plant's item
                Item plantToTurnIn = new Item();
                try
                {
                    plantToTurnIn.SetDefaults(qPlantsVsUndeadList[_qPlantsVsDeadSelected]);
                }
                catch
                {
                    plantToTurnIn.netDefaults(qPlantsVsUndeadList[_qPlantsVsDeadSelected]);
                }

                int obj = (int)(_qPlantsVsDeadToCompleteBase * _questDifficultyMultiplier);
                int plantsToTurnInInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == plantToTurnIn.type)
                        plantsToTurnInInInv += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {plantsToTurnInInInv}/{obj} {plantToTurnIn.Name}[i:{plantToTurnIn.type}].";
            }

            //Defeat enemeies inside an evil biome
            if (chosenQuest == "Evil Hunter")
            {
                int obj = (int)(_qCrimsonHunterToCompleteBase * _questDifficultyMultiplier);
                qCrimsonHunterToComplete = obj;

                questName = $"{_worldEvil} Hunter";
                questDesc = $"Defeat {qCrimsonHunterCount}/{qCrimsonHunterToComplete} enemies while in a {_worldEvil} biome.";
            }

            if (chosenQuest == "Building Materials")
            {
                //Get the selected plant's item
                Item woodToTurnIn = new Item();
                try
                {
                    woodToTurnIn.SetDefaults(qBuildingMaterialsWood[_qBuildingMaterialsWoodSelected]);
                }
                catch
                {
                    woodToTurnIn.netDefaults(qBuildingMaterialsWood[_qBuildingMaterialsWoodSelected]);
                }

                Item secondBlockToTurnIn = new Item();
                try
                {
                    secondBlockToTurnIn.SetDefaults(qBuildingMaterialsSecondBlock[_qBuildingMaterialsSecondBlockSelected]);
                }
                catch
                {
                    secondBlockToTurnIn.netDefaults(qBuildingMaterialsSecondBlock[_qBuildingMaterialsSecondBlockSelected]);
                }

                int objWood = (int)(_qBuildingMaterialsWoodToCompleteBase * _questDifficultyMultiplier);
                int woodInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == woodToTurnIn.type)
                        woodInInv += Player.inventory[i].stack;

                int objSecondBlock = (int)(_qBuildingMaterialsSecondBlockToCompleteBase * _questDifficultyMultiplier);
                int secondBlockInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == secondBlockToTurnIn.type)
                        secondBlockInInv += Player.inventory[i].stack;

                int objTorches = (int)(_qBuildingMaterialsTorchesToCompleteBase * _questDifficultyMultiplier);
                int torchesInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.Torch)
                        torchesInInv += Player.inventory[i].stack;

                questName = chosenQuest;
                questDesc = $"Turn in {woodInInv}/{objWood} {woodToTurnIn.Name}[i:{woodToTurnIn.type}], {secondBlockInInv}/{objSecondBlock} {secondBlockToTurnIn.Name}/s[i:{secondBlockToTurnIn.type}] and {torchesInInv}/{objTorches} Torches[i:{ItemID.Torch}].";
            }

            //Defeat a rare enemy
            if (chosenQuest == "Bounty Hunter")
            {
                NPC enemyToDefeat = new NPC();
                enemyToDefeat.SetDefaults(qBountyHunter[_qBountyHunterSelected]);

                int obj = 1;

                questName = chosenQuest;
                questDesc = $"Defeat {qBountyHunterCount}/{obj} {enemyToDefeat.TypeName}.";
            }

            base.PreUpdate();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //Slayer
            if (chosenQuest == "Slayer" && target.life <= 0 && !target.friendly && !target.SpawnedFromStatue && !target.CountsAsACritter)
                qSlayerCount++;

            //Unicorn Hunter
            if (chosenQuest == "Unicorn Hunter" && target.life <= 0 && target.type == NPCID.Unicorn && !target.friendly && !target.SpawnedFromStatue)
                qUnicornHunterCount++;

            //Boss Slayer
            if (chosenQuest == "Boss Slayer" && target.life <= 0 && target.boss)
                qBossSlayerCount++;

            //Crimson Hunter
            if (chosenQuest == "Evil Hunter" && target.life <= 0 && !target.friendly && !target.SpawnedFromStatue && !target.CountsAsACritter)
                if(Player.ZoneCrimson || Player.ZoneCorrupt)
                    qBossSlayerCount++;

            base.OnHitNPC(target, hit, damageDone);
        }

        //For some reason OnHitNPC() is working for both projectiles and melee hits so keep this commented for now in case it's just a tmod bug
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ////Slayer
            //if (chosenQuest == "Slayer" && target.life <= 0 && !target.friendly && !target.SpawnedFromStatue && !target.CountsAsACritter)
            //    qSlayerCount++;
            //
            ////Unicorn Hunter
            //if (chosenQuest == "Unicorn Hunter" && target.life <= 0 && target.type == NPCID.Unicorn)
            //    qUnicornHunterCount++;
            //
            ////Boss Slayer
            //if (chosenQuest == "Boss Slayer" && target.life <= 0 && target.boss)
            //    qBossSlayerCount++;

            base.OnHitNPCWithProj(proj, target, hit, damageDone);
        }

        /// <summary>
        /// Removes the player's current quest and resets all quest variables
        /// </summary>
        private void ResetQuests()
        {
            //Add pre-hardmode and hardmode quests to the available quest list accordingly
            questList.Clear();
            foreach (string Quest in _preHardmodeQuests)
                questList.Add(Quest);
            if (Main.hardMode)
                foreach (string hmQuest in _hardmodeQuests)
                    questList.Add(hmQuest);

            //Remove the quest the player already has
            chosenQuest = "";
            chosenQuestReward = -573;
            _questDifficultyMultiplier = 1f;
            _questRewardMultiplier = 1f;
            if (Main.hardMode)
                _questDifficultyMultiplier += .25f;
            _canCompleteQuest = true;

            qSlayerCount = 0;
            qUnicornHunterCount = 0;
            qBossSlayerCount = 0;
            qCrimsonHunterCount = 0;
            qBountyHunterCount = 0;

            if (WorldGen.crimson)
                _worldEvil = "Crimson";
            else
                _worldEvil = "Corruption";
        }

        /// <summary>
        /// Selects a new quest from the available quest list, difficulty affects quest rewards too
        /// </summary>
        public void SelectNewQuest()
        {
            ResetQuests();
            //If the difficulty is not the default one, add more rewards
            if (_questDifficultyMultiplier > 1f)
                _questRewardMultiplier += (_questDifficultyMultiplier - 1f) / 2;

            //Add random difficulty for variety and some rewards according to said difficulty
            float difficultyMult = Main.rand.NextFloat(0f, _questDifficultyMultiplier * 1f);
            _questDifficultyMultiplier += difficultyMult;
            _questRewardMultiplier += difficultyMult / 2;

            //Select the quest
            int questCount = 0;
            foreach(string Quest in questList)
                questCount++;
            int selectedQuest = Main.rand.Next(0, questCount);
            chosenQuest = questList[selectedQuest];

            //Select a random ingot as the quest's objective
            if(chosenQuest == "Miner Supplies")
                _qMinerSuppliesBarSelected = Main.rand.Next(4);
            //Pick a random critter from the list
            if(chosenQuest == "Critter Collector")
                _qCritterCollectorSelected = Main.rand.Next(0, qCritterCollectorList.Count);
            //Pick a random critter from the list
            if (chosenQuest == "Turn In")
                _qTurnInSelected = Main.rand.Next(0, qTurnInItemList.Count);
            //Pick a random gem from the list
            if (chosenQuest == "Jewelry Request")
                _qJewelryRequestSelected = Main.rand.Next(0, qJewelryRequestList.Count);
            //Pick a random plant from the list
            if (chosenQuest == "Plants vs Undead")
                _qPlantsVsDeadSelected = Main.rand.Next(0, qPlantsVsUndeadList.Count);
            //Pick Building Material blocks
            if (chosenQuest == "Building Materials")
            {
                _qBuildingMaterialsWoodSelected = Main.rand.Next(0, qBuildingMaterialsWood.Count);
                _qBuildingMaterialsSecondBlockSelected = Main.rand.Next(0, qBuildingMaterialsSecondBlock.Count);
            }
            //Pick a random enemy from the list
            if (chosenQuest == "Bounty Hunter")
                _qBountyHunterSelected = Main.rand.Next(0, qBountyHunter.Count);
        }

        private void GiveRewards()
        {
            //Always give the player some gold
            int _goldReward = 010000; //Always grants at least 1 Gold
            int _townNPCCount = 0;

            //Count how many Town NPCs there are and add 10 more silver coins per npc to the quest's gold reward
            for (int i = 0; i < Main.maxNPCs; i++)
                if (Main.npc[i].townNPC && Main.npc[i].active)
                    _townNPCCount++;
            _goldReward += 0800 * _townNPCCount;
            _goldReward = (int)(_goldReward * _questRewardMultiplier);

            //Calculate coins to give
            int gold = _goldReward / 10000;
            _goldReward -= gold * 10000;
            int silver = _goldReward / 100;
            _goldReward -= silver * 100;
            int copper = _goldReward;

            //Add money gained from quests to the Classes Menu
            var classesMenu = GetInstance<UI.ClassesMenu>();
            //classesMenu.goldGainedFromQuests += gold + (silver / 100) + (copper / 1000);

            //Give gold coins
            Player.QuickSpawnItem(null, ItemID.GoldCoin, gold);
            //Give silver coins
            if (silver > 0)
                Player.QuickSpawnItem(null, ItemID.SilverCoin, silver);
            //Give bronze coins
            if (copper > 0)
                Player.QuickSpawnItem(null, ItemID.CopperCoin, copper);

            Main.NewText($"Quest Completed: {chosenQuest}!");
            Main.NewText($"Rewards: {gold} Gold, {silver} Silver, {copper} Copper");

            int secondRewardChosen = Main.rand.Next(3);
            if(secondRewardChosen == 0) //2 rune frags
            {
                Player.QuickSpawnItem(null, ItemType<Items.LostRuneFragment>(), (int)(2 * _questRewardMultiplier));
                Main.NewText($"{(int)(2 * _questRewardMultiplier)} Lost Rune Fragments");
            }
            else if (secondRewardChosen == 1) //2 relic frags
            {
                Player.QuickSpawnItem(null, ItemType<Items.RelicFragment>(), (int)(2 * _questRewardMultiplier));
                Main.NewText($"{(int)(2 * _questRewardMultiplier)} Relic Fragments");
            }
            else if (secondRewardChosen == 2) //1 of each frag
            {
                Player.QuickSpawnItem(null, ItemType<Items.LostRuneFragment>(), (int)(1 * _questRewardMultiplier));
                Player.QuickSpawnItem(null, ItemType<Items.RelicFragment>(), (int)(1 * _questRewardMultiplier));
                Main.NewText($"{(int)(1 * _questRewardMultiplier)} Relic Fragments");
                Main.NewText($"{(int)(1 * _questRewardMultiplier)} Lost Rune Fragments");
            }
        }

        /// <summary>
        /// Completes the player's current quest and gives them one of the rewards from the rewards list
        /// </summary>
        public void CompleteQuest()
        {
            //Defeat 100 enemies
            if (chosenQuest == "Slayer" && qSlayerCount >= qSlayerCountToComplete)
            {
                GiveRewards();
                ResetQuests();
            }

            if(chosenQuest == "Lumberjack")
            {
                int obj = (int)(_qLumberjackToCompleteBase * _questDifficultyMultiplier);
                int woodAmount = 0;

                //How much wood we have
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.Wood)
                        woodAmount += Player.inventory[i].stack;

                //If we have enough, complete the quest
                if(woodAmount >= obj)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == ItemID.Wood && Player.inventory[i].stack >= obj)
                        {
                            Player.inventory[i].stack -= obj;
                            GiveRewards();
                            ResetQuests();
                            break;
                        }
                    }
                }
            }

            if (chosenQuest == "Miner Supplies")
            {
                int obj = (int)(_qMinerSuppliesToCompleteBase * _questDifficultyMultiplier);
                int ingotAmount = 0;

                //How much of each bar we have
                if (_qMinerSuppliesBarSelected == 0)
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.CopperBar || Player.inventory[i].type == ItemID.TinBar)
                            ingotAmount += Player.inventory[i].stack;

                if (_qMinerSuppliesBarSelected == 1)
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.IronBar || Player.inventory[i].type == ItemID.LeadBar)
                            ingotAmount += Player.inventory[i].stack;

                if (_qMinerSuppliesBarSelected == 2)
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.SilverBar || Player.inventory[i].type == ItemID.TungstenBar)
                            ingotAmount += Player.inventory[i].stack;

                if (_qMinerSuppliesBarSelected == 3)
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                        if (Player.inventory[i].type == ItemID.GoldBar || Player.inventory[i].type == ItemID.PlatinumBar)
                            ingotAmount += Player.inventory[i].stack;

                //If we have enough, complete the quest
                if (ingotAmount >= obj)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if(_qMinerSuppliesBarSelected == 0)
                        {
                            if (Player.inventory[i].type == ItemID.CopperBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                            if (Player.inventory[i].type == ItemID.TinBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                        }

                        if (_qMinerSuppliesBarSelected == 1)
                        {
                            if (Player.inventory[i].type == ItemID.IronBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                            if (Player.inventory[i].type == ItemID.LeadBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                        }

                        if (_qMinerSuppliesBarSelected == 2)
                        {
                            if (Player.inventory[i].type == ItemID.SilverBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                            if (Player.inventory[i].type == ItemID.TungstenBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                        }

                        if (_qMinerSuppliesBarSelected == 3)
                        {
                            if (Player.inventory[i].type == ItemID.GoldBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                            if (Player.inventory[i].type == ItemID.PlatinumBar && Player.inventory[i].stack >= obj)
                            {
                                Player.inventory[i].stack -= obj;
                                GiveRewards();
                                ResetQuests();
                            }
                        }
                    }
                }
            }

            if (chosenQuest == "Masonry Supplies")
            {
                int obj = (int)(_qMasonrySuppliesToCompleteBase * _questDifficultyMultiplier);
                int stoneAmount = 0;

                //How many ingots we have
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.StoneBlock)
                        stoneAmount += Player.inventory[i].stack;

                //If we have enough, complete the quest
                if (stoneAmount >= obj)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == ItemID.StoneBlock && Player.inventory[i].stack >= obj)
                        {
                            Player.inventory[i].stack -= obj;
                            GiveRewards();
                            ResetQuests();
                            break;
                        }
                    }
                }
            }

            //Unicorn Hunter
            if (chosenQuest == "Unicorn Hunter" && qUnicornHunterCount >= qUnicornHunterToComplete)
            {
                GiveRewards();
                ResetQuests();
            }

            //Boss Slayer
            if (chosenQuest == "Boss Slayer" && qBossSlayerCount >= qBossSlayerToComplete)
            {
                GiveRewards();
                ResetQuests();
            }

            if (chosenQuest == "Hallowed Supplies")
            {
                int obj = (int)(_qHallowedSuppliesToCompleteBase * _questDifficultyMultiplier);
                int barAmount = 0;

                //How many ingots we have
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.HallowedBar)
                        barAmount += Player.inventory[i].stack;

                //If we have enough, complete the quest
                if (barAmount >= obj)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == ItemID.HallowedBar && Player.inventory[i].stack >= obj)
                        {
                            Player.inventory[i].stack -= obj;
                            GiveRewards();
                            ResetQuests();
                            break;
                        }
                    }
                }
            }

            if (chosenQuest == "Critter Collector")
            {
                //Get the selected critter's item
                Item critter = new Item();
                try
                {
                    critter.SetDefaults(qCritterCollectorList[_qCritterCollectorSelected]);
                }
                catch
                {
                    critter.netDefaults(qCritterCollectorList[_qCritterCollectorSelected]);
                }
                int crittersInInv = 0;

                //Do we have the critter
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == critter.type)
                        crittersInInv += Player.inventory[i].stack;

                //If we have it, complete the quest
                if (crittersInInv >= 1)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == critter.type && Player.inventory[i].stack >= 1)
                        {
                            Player.inventory[i].stack -= 1;
                            GiveRewards();
                            ResetQuests();
                            break;
                        }
                    }
                }
            }

            if (chosenQuest == "Turn In")
            {
                //Get the selected item
                Item itemToTurnIn = new Item();
                try
                {
                    itemToTurnIn.SetDefaults(qTurnInItemList[_qTurnInSelected]);
                }
                catch
                {
                    itemToTurnIn.netDefaults(qTurnInItemList[_qTurnInSelected]);
                }
                int ItemToTurnInInInv = 0;

                //Do we have the item
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == itemToTurnIn.type)
                        ItemToTurnInInInv += Player.inventory[i].stack;

                //If we have it, complete the quest
                if (ItemToTurnInInInv >= 1)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == itemToTurnIn.type && Player.inventory[i].stack >= 1)
                        {
                            Player.inventory[i].stack -= 1;
                            GiveRewards();
                            ResetQuests();
                            break;
                        }
                    }
                }
            }

            if (chosenQuest == "Jewelry Request")
            {
                //Get the selected gem
                Item gemToTurnIn = new Item();
                try
                {
                    gemToTurnIn.SetDefaults(qJewelryRequestList[_qJewelryRequestSelected]);
                }
                catch
                {
                    gemToTurnIn.netDefaults(qJewelryRequestList[_qJewelryRequestSelected]);
                }
                int obj = (int)(_qJewelryRequestToCompleteBase * _questDifficultyMultiplier);
                int GemsToTurnInInInv = 0;

                //Do we have the gem
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == gemToTurnIn.type)
                        GemsToTurnInInInv += Player.inventory[i].stack;

                //If we have it, complete the quest
                if (GemsToTurnInInInv >= obj)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == gemToTurnIn.type && Player.inventory[i].stack >= obj)
                        {
                            Player.inventory[i].stack -= obj;
                            GiveRewards();
                            ResetQuests();
                            break;
                        }
                    }
                }
            }

            //Crimson Hunter
            if (chosenQuest == "Evil Hunter" && qCrimsonHunterCount >= qCrimsonHunterToComplete)
            {
                GiveRewards();
                ResetQuests();
            }

            if (chosenQuest == "Building Materials")
            {
                //Wood
                Item woodToTurnIn = new Item();
                try
                {
                    woodToTurnIn.SetDefaults(qBuildingMaterialsWood[_qBuildingMaterialsWoodSelected]);
                }
                catch
                {
                    woodToTurnIn.netDefaults(qBuildingMaterialsWood[_qBuildingMaterialsWoodSelected]);
                }
                int objWood = (int)(_qBuildingMaterialsWoodToCompleteBase * _questDifficultyMultiplier);
                int woodInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == woodToTurnIn.type)
                        woodInInv += Player.inventory[i].stack;

                //Second Block
                Item secondBlockToTurnIn = new Item();
                try
                {
                    secondBlockToTurnIn.SetDefaults(qBuildingMaterialsSecondBlock[_qBuildingMaterialsSecondBlockSelected]);
                }
                catch
                {
                    secondBlockToTurnIn.netDefaults(qBuildingMaterialsSecondBlock[_qBuildingMaterialsSecondBlockSelected]);
                }
                int objSecondBlock = (int)(_qBuildingMaterialsSecondBlockToCompleteBase * _questDifficultyMultiplier);
                int secondBlockInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == secondBlockToTurnIn.type)
                        secondBlockInInv += Player.inventory[i].stack;

                //Torches
                int objTorches = (int)(_qBuildingMaterialsTorchesToCompleteBase * _questDifficultyMultiplier);
                int torchesInInv = 0;
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == ItemID.Torch)
                        torchesInInv += Player.inventory[i].stack;

                //If we have everything, complete the quest
                if (woodInInv >= objWood && secondBlockInInv >= objSecondBlock && torchesInInv >= objTorches)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == woodToTurnIn.type && Player.inventory[i].stack >= objWood)
                            Player.inventory[i].stack -= objWood;
                        if (Player.inventory[i].type == secondBlockToTurnIn.type && Player.inventory[i].stack >= objSecondBlock)
                            Player.inventory[i].stack -= objSecondBlock;
                        if (Player.inventory[i].type == ItemID.Torch && Player.inventory[i].stack >= objTorches)
                            Player.inventory[i].stack -= objTorches;
                    }
                    //Find a way to give rewards only if the above checks are true, possibly exploitable but works for now, maybe {X++ >> if X == INT >> REWARDS}
                    GiveRewards();
                    ResetQuests();
                }
            }

            //Crimson Hunter
            if (chosenQuest == "Bounty Hunter" && qBountyHunterCount >= 1)
            {
                GiveRewards();
                ResetQuests();
            }

            if (chosenQuest == "Plants vs Undead")
            {
                //Get the selected critter's item
                Item plant = new Item();
                try
                {
                    plant.SetDefaults(qPlantsVsUndeadList[_qPlantsVsDeadSelected]);
                }
                catch
                {
                    plant.netDefaults(qPlantsVsUndeadList[_qPlantsVsDeadSelected]);
                }
                int plantsInInv = 0;
                int plantsObj = (int)(_qPlantsVsDeadToCompleteBase * _questDifficultyMultiplier);

                //Do we have the critter
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    if (Player.inventory[i].type == plant.type)
                        plantsInInv += Player.inventory[i].stack;

                //If we have it, complete the quest
                if (plantsInInv >= plantsObj)
                {
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (Player.inventory[i].type == plant.type && Player.inventory[i].stack >= plantsObj)
                        {
                            Player.inventory[i].stack -= plantsObj;
                            GiveRewards();
                            ResetQuests();
                            break;
                        }
                    }
                }
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("questsCompleted", questsCompleted);
            tag.Add("chosenQuest", chosenQuest);
            tag.Add("_worldEvil", _worldEvil);
            tag.Add("_questDifficultyMultiplier", _questDifficultyMultiplier);
            tag.Add("_canCompleteQuest", _canCompleteQuest);

            //Slayer
            tag.Add("qSlayerCount", qSlayerCount);
            tag.Add("qSlayerCountToComplete", qSlayerCountToComplete);

            //Unicorn Hunter
            tag.Add("qUnicornHunterCount", qUnicornHunterCount);
            tag.Add("qUnicornHunterToComplete", qUnicornHunterToComplete);

            //Miner Supplies
            tag.Add("_qMinerSuppliesBarSelected", _qMinerSuppliesBarSelected);

            //Boss Slayer
            tag.Add("qBossSlayerCount", qBossSlayerCount);
            tag.Add("qBossSlayerToComplete", qBossSlayerToComplete);

            //Critter Collector
            tag.Add("_qCritterCollectorSelected", _qCritterCollectorSelected);

            //Plants vs Undead
            tag.Add("_qPlantsVsDeadSelected", _qPlantsVsDeadSelected);

            //Evil Hunter
            tag.Add("qCrimsonHunterCount", qCrimsonHunterCount);
            tag.Add("qCrimsonHunterToComplete", qCrimsonHunterToComplete);

            //Bounty Hunter
            tag.Add("_qBountyHunterSelected", _qBountyHunterSelected);

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            questsCompleted = tag.GetInt("questsCompleted");
            chosenQuest = tag.GetString("chosenQuest");
            _worldEvil = tag.GetString("_worldEvil");
            _questDifficultyMultiplier = tag.GetFloat("_questDifficultyMultiplier");
            _canCompleteQuest = tag.GetBool("_canCompleteQuest");

            //Slayer
            qSlayerCount = tag.GetInt("qSlayerCount");
            qSlayerCountToComplete = tag.GetInt("qSlayerCountToComplete");

            //Unicorn Hunter
            qUnicornHunterCount = tag.GetInt("qUnicornHunterCount");
            qUnicornHunterToComplete = tag.GetInt("qUnicornHunterToComplete");

            //Miner Supplies
            _qMinerSuppliesBarSelected = tag.GetInt("_qMinerSuppliesBarSelected");

            //Boss Slayer
            qBossSlayerCount = tag.GetInt("qBossSlayerCount");
            qBossSlayerToComplete = tag.GetInt("qBossSlayerToComplete");

            //Critter Collector
            _qCritterCollectorSelected = tag.GetInt("_qCritterCollectorSelected");

            //Plants vs Undead
            _qPlantsVsDeadSelected = tag.GetInt("_qPlantsVsDeadSelected");

            //Evil Hunter
            qCrimsonHunterCount = tag.GetInt("qCrimsonHunterCount");
            qCrimsonHunterToComplete = tag.GetInt("qCrimsonHunterToComplete");

            //Bounty Hunter
            _qBountyHunterSelected = tag.GetInt("_qBountyHunterSelected");

            base.LoadData(tag);
        }
    }
}