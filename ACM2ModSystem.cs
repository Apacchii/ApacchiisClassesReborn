using System.IO;
using System;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ApacchiisClassesMod2.UI;
using ApacchiisClassesMod2.UI.HUD;
using ApacchiisClassesMod2.UI.Specializations;
using Terraria.UI;
using System.Collections.Generic;

namespace ApacchiisClassesMod2
{
    public class ACM2ModSystem : ModSystem
    {
        Player Player = Main.player[Main.myPlayer];

        public static ModKeybind ClassAbility1;
        public static ModKeybind ClassAbility2;
        public static ModKeybind ClassAbilityUltimate;
        public static ModKeybind Menu;

        
        private GameTime _lastUpdateUiGameTime;

        internal ClassesMenu ClassesMenu;
        internal UserInterface _ClassesMenu;

        internal HUD HUD;
        internal UserInterface _HUD;

        internal VanguardTalents VanguardTalents;
        internal UserInterface _VanguardTalents;

        internal BloodMageTalents BloodMageTalents;
        internal UserInterface _BloodMageTalents;
        internal BloodMageSpecs BloodMageSpecs;
        internal UserInterface _BloodMageSpecs;

        internal CommanderTalents CommanderTalents;
        internal UserInterface _CommanderTalents;

        internal ScoutTalents ScoutTalents;
        internal UserInterface _ScoutTalents;

        //public ACM2()
        //{
        //    Properties = new ModProperties()
        //    {
        //        Autoload = true,
        //        AutoloadGores = true,
        //        AutoloadSounds = true
        //    };
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

                VanguardTalents = new VanguardTalents();
                _VanguardTalents = new UserInterface();

                BloodMageTalents = new BloodMageTalents();
                _BloodMageTalents = new UserInterface();
                BloodMageSpecs = new BloodMageSpecs();
                _BloodMageSpecs = new UserInterface();

                CommanderTalents = new CommanderTalents();
                _CommanderTalents = new UserInterface();

                ScoutTalents = new ScoutTalents();
                _ScoutTalents = new UserInterface();
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

            if (_VanguardTalents?.CurrentState != null)
                _VanguardTalents.Update(gameTime);

            if (_BloodMageTalents?.CurrentState != null)
                _BloodMageTalents.Update(gameTime);
            if (_BloodMageSpecs?.CurrentState != null)
                _BloodMageSpecs.Update(gameTime);

            if (_CommanderTalents?.CurrentState != null)
                _CommanderTalents.Update(gameTime);

            if (_ScoutTalents?.CurrentState != null)
                _ScoutTalents.Update(gameTime);

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
                #endregion

                #region Skills
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Skills",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _BloodMageSpecs?.CurrentState != null)
                            _BloodMageSpecs.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                        return true;
                    },
                       InterfaceScaleType.UI));
                #endregion
            }
            base.ModifyInterfaceLayers(layers);
        }
    }
}