using System;
using HarmonyLib;
using ModLoader;
using System.Collections.Generic;
using ModLoader.Helpers;
using SFS.UI;

namespace BetterKeybinds
{
    public class Main : Mod
    {
        public static Main Instance { get; private set; }
        public Main()
        {
            Instance = this;
        }

        public override string ModNameID => "BetterKeybinds";
        public override string DisplayName => "Better Keybinds";
        public override string Author => "kojamori";
        public override string MinimumGameVersionNecessary => "1.6.00.16";
        public override string ModVersion => "1.0.0";
        public override string Description => "A mod that adds the left and right Alt and left and right Shift keys to keybinds.";
        public override string IconLink => "";

        public override Dictionary<string, string> Dependencies => new Dictionary<string, string>();

        private Harmony _patcher;

        public override void Early_Load()
        {
            _patcher = new Harmony(Instance.ModNameID);
            _patcher.PatchAll();
        }

        public override void Load()
        {
        }

    }
}
