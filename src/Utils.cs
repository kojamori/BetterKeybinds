using System.Reflection;
using HarmonyLib;
using SFS.Input;
using UnityEngine;

namespace BetterKeybinds;

public static class Utils
{
    public static string GetDisplayName(CustomKey key)
    {
        Debug.Log("getting custom name");
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
    
    public static void RefreshDisplayNames()
    {
        FieldInfo elementsField = AccessTools.Field(
            typeof(KeybindingsPC),
            "elements"
        );

        if (elementsField?.GetValue(KeybindingsPC.main) is not
            System.Collections.Generic.List<KeyBinder> elements)
            return;

        foreach (KeyBinder binder in elements)
        {
            FieldInfo keyField = AccessTools.Field(
                typeof(KeyBinder),
                "key"
            );

            FieldInfo textField = AccessTools.Field(
                typeof(KeyBinder),
                "text"
            );

            if (keyField?.GetValue(binder) is not KeybindingsPC.Key key)
                continue;

            if (textField?.GetValue(binder) is not TMPro.TMP_Text text)
                continue;

            if (key is CustomKey customKey)
            {
                text.text = GetDisplayName(customKey);
            }
            else
            {
                string displayName = Traverse
                    .Create(typeof(KeyBinder))
                    .Method("GetDisplayName", key)
                    .GetValue<string>();

                text.text = displayName;
            }
                
        }
    }
}