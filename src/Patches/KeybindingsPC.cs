using HarmonyLib;
using SFS.Input;
using SFS.Parsers.Json;
using UnityEngine;

namespace BetterKeybinds;

[HarmonyPatch(typeof(KeybindingsPC), "OnLoad")]
public static class KeybindingsPC_OnLoad_Patch
{
    public static void Postfix()
    {
        LoadCustomKeys();
    }

    private static void LoadCustomKeys()
    {
        IFile path = FileLocations.GetSettingsPath("Keybindings");



        CustomKeyData data = null;

        if (!path.Exists())
        {
            data = new CustomKeyData();
        }
        
        try
        {
            data = JsonWrapper.FromJson<CustomKeyData>(path.ReadText());
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogException(ex);
        }

        // use default values if error
        data = data ?? new CustomKeyData();

        KeybindingsPC.keys.Close_Menu = data.Close_Menu;
        KeybindingsPC.keys.SaveLoad = data.SaveLoad;
        KeybindingsPC.keys.Select_All = data.Select_All;
        KeybindingsPC.keys.CopyPaste = data.CopyPaste;
        KeybindingsPC.keys.Duplicate = data.Duplicate;
        KeybindingsPC.keys.Delete = data.Delete;

        KeybindingsPC.keys.Rotate_Part = data.Rotate_Part;
        KeybindingsPC.keys.Flip_Part = data.Flip_Part;

        KeybindingsPC.keys.Undo = data.Undo;
        KeybindingsPC.keys.Redo = data.Redo;

        KeybindingsPC.keys.Toggle_Ignition = data.Toggle_Ignition;
        KeybindingsPC.keys.Throttle = data.Throttle;
        KeybindingsPC.keys.MinMax_Throttle = data.MinMax_Throttle;

        KeybindingsPC.keys.Toggle_RCS = data.Toggle_RCS;
        KeybindingsPC.keys.Turn_Rocket = data.Turn_Rocket;
        KeybindingsPC.keys.Move_Rocket_Using_RCS = data.Move_Rocket_Using_RCS;

        KeybindingsPC.keys.Activate_Stage = data.Activate_Stage;
        KeybindingsPC.keys.Toggle_Map = data.Toggle_Map;
        KeybindingsPC.keys.Timewarp = data.Timewarp;
        KeybindingsPC.keys.Switch_Rocket = data.Switch_Rocket;
        KeybindingsPC.keys.Toggle_Console = data.Toggle_Console;
    }

    private sealed class CustomKeyData
    {
        public CustomKey Close_Menu = KeyCode.Escape;

        public CustomKey[] SaveLoad =
        {
            KeyCode.F5,
            KeyCode.F9
        };

        public CustomKey Select_All = CustomKey.Ctrl(KeyCode.A);

        public CustomKey[] CopyPaste =
        {
            CustomKey.Ctrl(KeyCode.C),
            CustomKey.Ctrl(KeyCode.V)
        };

        public CustomKey Duplicate = CustomKey.Ctrl(KeyCode.D);
        public CustomKey Delete = KeyCode.Delete;

        public CustomKey[] Rotate_Part =
        {
            KeyCode.Q,
            KeyCode.E
        };

        public CustomKey[] Flip_Part =
        {
            KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D
        };

        public CustomKey Undo = CustomKey.Ctrl(KeyCode.Z);
        public CustomKey Redo = CustomKey.Ctrl(KeyCode.Y);

        public CustomKey Toggle_Ignition = KeyCode.Space;

        public CustomKey[] Throttle =
        {
            KeyCode.LeftControl,
            KeyCode.LeftShift
        };

        public CustomKey[] MinMax_Throttle =
        {
            KeyCode.X,
            KeyCode.Z
        };

        public CustomKey Toggle_RCS = KeyCode.R;

        public CustomKey[] Turn_Rocket =
        {
            KeyCode.Q,
            KeyCode.E
        };

        public CustomKey[] Move_Rocket_Using_RCS =
        {
            KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D
        };

        public CustomKey Activate_Stage = KeyCode.Return;
        public CustomKey Toggle_Map = KeyCode.M;

        public CustomKey[] Timewarp =
        {
            KeyCode.Comma,
            KeyCode.Period
        };

        public CustomKey[] Switch_Rocket =
        {
            KeyCode.LeftBracket,
            KeyCode.RightBracket
        };

        public CustomKey Toggle_Console = KeyCode.F1;
    }
}