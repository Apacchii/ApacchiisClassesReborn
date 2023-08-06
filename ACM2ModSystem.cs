using System.IO;
using System;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ApacchiisClassesMod2.UI;
using ApacchiisClassesMod2.UI.HUD;
using ApacchiisClassesMod2.UI.Other;
using ApacchiisClassesMod2.UI.Specializations;
using Terraria.UI;
using System.Collections.Generic;

namespace ApacchiisClassesMod2
{
    public class ACM2ModSystem : ModSystem
    {
        public static ModKeybind ClassAbility1;
        public static ModKeybind ClassAbility2;
        public static ModKeybind ClassAbilityUltimate;
        public static ModKeybind Menu;

        
        private GameTime _lastUpdateUiGameTime;

        internal ClassesMenu ClassesMenu;
        internal UserInterface _ClassesMenu;

        internal HUD HUD;
        internal UserInterface _HUD;

        internal RelicsUI RelicsUI;
        internal UserInterface _RelicsUI;

        internal VanguardTalents VanguardTalents;
        internal UserInterface _VanguardTalents;

        internal BloodMageTalents BloodMageTalents;
        internal UserInterface _BloodMageTalents;

        internal CommanderTalents CommanderTalents;
        internal UserInterface _CommanderTalents;

        internal ScoutTalents ScoutTalents;
        internal UserInterface _ScoutTalents;

        internal SoulmancerTalents SoulmancerTalents;
        internal UserInterface _SoulmancerTalents;

        internal SoulmancerTalents CrusaderTalents;
        internal UserInterface _CrusaderTalents;

        internal GamblerTalents GamblerTalents;
        internal UserInterface _GamblerTalents;

        internal PlagueTalents PlagueTalents;
        internal UserInterface _PlagueTalents;

        internal GeneralCards Cards;
        internal UserInterface _Cards;
        internal MyDeck MyDeck;
        internal UserInterface _MyDeck;

        //Achievements Mod
        //public override void PostSetupContent()
        //{
        //    //Try when 'Achievements Mod' releases on 1.4.4 - NOT WORKING
        //    if (ModLoader.TryGetMod("TMLAchievements", out Mod achievsMod))
        //        achievsMod.Call("AddAchievement",
        //        this, //Mod name reference
        //        "NitehrisTreasury", //Name, no spaces ?
        //        Terraria.Achievements.AchievementCategory.Collector,
        //        "ApacchiisClassesMod2/Achievements/test",
        //        null, //Image border, null = none
        //        false, //Show progress bar
        //        false, //Show icon in inventory
        //        40f, //Order for icon in inventory, higher is later, 36f is Champion Of Terraria
        //        new string[] { "Craft_" + ModContent.ItemType<Items.Relics.NiterihsJewelryBox>() });
        //    base.PostSetupContent();
        //}

        public override void Load()
        {
            ClassAbility1 = KeybindLoader.RegisterKeybind(Mod, "Class Ability: 1", "Q");
            ClassAbility2 = KeybindLoader.RegisterKeybind(Mod, "Class Ability: 2", "C");
            ClassAbilityUltimate = KeybindLoader.RegisterKeybind(Mod, "Class Ability: Ultimate", "V");
            Menu = KeybindLoader.RegisterKeybind(Mod, "Menu", "N");

            if (!Main.dedServ)
            {
                ClassesMenu = new ClassesMenu();
                _ClassesMenu = new UserInterface();

                HUD = new HUD();
                _HUD = new UserInterface();

                RelicsUI = new RelicsUI();
                _RelicsUI = new UserInterface();

                VanguardTalents = new VanguardTalents();
                _VanguardTalents = new UserInterface();

                BloodMageTalents = new BloodMageTalents();
                _BloodMageTalents = new UserInterface();

                CommanderTalents = new CommanderTalents();
                _CommanderTalents = new UserInterface();

                ScoutTalents = new ScoutTalents();
                _ScoutTalents = new UserInterface();

                SoulmancerTalents = new SoulmancerTalents();
                _SoulmancerTalents = new UserInterface();

                CrusaderTalents = new SoulmancerTalents();
                _CrusaderTalents = new UserInterface();

                GamblerTalents = new GamblerTalents();
                _GamblerTalents = new UserInterface();

                PlagueTalents = new PlagueTalents();
                _PlagueTalents = new UserInterface();

                Cards = new GeneralCards();
                _Cards = new UserInterface();
                MyDeck = new MyDeck();
                _MyDeck =new UserInterface();
            }

            base.Load();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;

            if (_ClassesMenu?.CurrentState != null)
                _ClassesMenu.Update(gameTime);
            if (_HUD?.CurrentState != null)
                _HUD.Update(gameTime);
            if (_Cards?.CurrentState != null)
                _Cards.Update(gameTime);
            if (_MyDeck?.CurrentState != null)
                _MyDeck?.Update(gameTime);

            if (_RelicsUI?.CurrentState != null)
                _RelicsUI.Update(gameTime);
            if (_VanguardTalents?.CurrentState != null)
                _VanguardTalents.Update(gameTime);
            if (_BloodMageTalents?.CurrentState != null)
                _BloodMageTalents.Update(gameTime);
            if (_CommanderTalents?.CurrentState != null)
                _CommanderTalents.Update(gameTime);
            if (_ScoutTalents?.CurrentState != null)
                _ScoutTalents.Update(gameTime);
            if (_SoulmancerTalents?.CurrentState != null)
                _SoulmancerTalents.Update(gameTime);
            if (_CrusaderTalents?.CurrentState != null)
                _CrusaderTalents.Update(gameTime);
            if (_GamblerTalents?.CurrentState != null)
                _GamblerTalents.Update(gameTime);
            if (_PlagueTalents?.CurrentState != null)
                _PlagueTalents.Update(gameTime);

            base.UpdateUI(gameTime);
        }
        
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        
            if (mouseTextIndex != -1)
            {
                #region ClassMenu
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: ClassesMenu",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _ClassesMenu?.CurrentState != null)
                        {
                            _ClassesMenu.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: RelicsUI",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _RelicsUI?.CurrentState != null)
                        {
                            _RelicsUI.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
                #endregion

                #region HUD
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: HUD",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _HUD?.CurrentState != null)
                        {
                            _HUD.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
                #endregion

                #region Talents
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _VanguardTalents?.CurrentState != null)
                        {
                            _VanguardTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _BloodMageTalents?.CurrentState != null)
                        {
                            _BloodMageTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _CommanderTalents?.CurrentState != null)
                        {
                            _CommanderTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _ScoutTalents?.CurrentState != null)
                        {
                            _ScoutTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _SoulmancerTalents?.CurrentState != null)
                        {
                            _SoulmancerTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _CrusaderTalents?.CurrentState != null)
                        {
                            _CrusaderTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _GamblerTalents?.CurrentState != null)
                        {
                            _GamblerTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _PlagueTalents?.CurrentState != null)
                        {
                            _PlagueTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
                #endregion

                #region Cards
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                   "ApacchiisClassesMod2: Cards",
                   delegate
                   {
                       if (_lastUpdateUiGameTime != null && _Cards?.CurrentState != null)
                           _Cards.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                       return true;
                   },
                      InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                   "ApacchiisClassesMod2: My Deck",
                   delegate
                   {
                       if (_lastUpdateUiGameTime != null && _MyDeck?.CurrentState != null)
                           _MyDeck.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                       return true;
                   },
                      InterfaceScaleType.UI));
                #endregion
            }
            base.ModifyInterfaceLayers(layers);
        }
    }
}