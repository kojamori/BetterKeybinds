using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SFS.Input;
using UnityEngine;

namespace BetterKeybinds;

public static class Utils
{
    public static string GetDisplayName(CustomKey key)
    {
        string modifiers = "";

        if (key.leftCtrl)
            modifiers += "Left Ctrl + ";

        if (key.rightCtrl)
            modifiers += "Right Ctrl + ";

        if (key.leftShift)
            modifiers += "Left Shift + ";

        if (key.rightShift)
            modifiers += "Right Shift + ";

        if (key.leftAlt)
            modifiers += "Left Alt + ";

        if (key.rightAlt)
            modifiers += "Right Alt + ";

        return modifiers + GetKeyName(key.key);
    }

    private static string GetKeyName(KeyCode key)
    {
        return key switch
        {
            KeyCode.Return => "Enter",
            KeyCode.Comma => ">",
            KeyCode.Period => "<",

            KeyCode.LeftBracket => "[",
            KeyCode.RightBracket => "]",

            KeyCode.KeypadEnter => "Enter",

            KeyCode.UpArrow => "Up",
            KeyCode.DownArrow => "Down",
            KeyCode.RightArrow => "Right",
            KeyCode.LeftArrow => "Left",

            KeyCode.RightShift => "Right Shift",
            KeyCode.LeftShift => "Left Shift",
            KeyCode.RightControl => "Right Ctrl",
            KeyCode.LeftControl => "Left Ctrl",

            _ => key.ToString()
        };
    }
}