using System;

using HarmonyLib;
using SFS.Input;
using UnityEngine;

namespace BetterKeybinds;

// Patch Keybinder so it can listen to new modifiers
[HarmonyPatch(typeof(KeyBinder), nameof(KeyBinder.ProcessInput))]
public static class KeyBinder_ProcessInput_Patch
{
    private static bool IsModifier(KeyCode key)
    {
        return key is
            KeyCode.LeftControl or
            KeyCode.RightControl or
            KeyCode.LeftShift or
            KeyCode.RightShift or
            KeyCode.LeftAlt or
            KeyCode.RightAlt;
    }

    public static bool Prefix(
        ref KeybindingsPC.Key ___key,
        Action ___apply,
        TMPro.TMP_Text ___text)
    {
        if (___key is not CustomKey key)
            return true;

        if (!Input.anyKeyDown)
            return false;

        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(keyCode))
                continue;

            key.key = keyCode;

            key.leftCtrl = Input.GetKey(KeyCode.LeftControl);
            key.rightCtrl = Input.GetKey(KeyCode.RightControl);

            key.leftShift = Input.GetKey(KeyCode.LeftShift);
            key.rightShift = Input.GetKey(KeyCode.RightShift);

            key.leftAlt = Input.GetKey(KeyCode.LeftAlt);
            key.rightAlt = Input.GetKey(KeyCode.RightAlt);

            // for backwards compatibility
            key.ctrl = key.leftCtrl || key.rightCtrl;

            // The primary key isn't an additional modifier.
            switch (keyCode)
            {
                case KeyCode.LeftControl:
                    key.leftCtrl = false;
                    break;

                case KeyCode.RightControl:
                    key.rightCtrl = false;
                    break;

                case KeyCode.LeftShift:
                    key.leftShift = false;
                    break;

                case KeyCode.RightShift:
                    key.rightShift = false;
                    break;

                case KeyCode.LeftAlt:
                    key.leftAlt = false;
                    break;

                case KeyCode.RightAlt:
                    key.rightAlt = false;
                    break;
            }

            ___apply();
                
            ___text.text = Utils.GetDisplayName(key);
            
            ScreenManager.main.CloseCurrent();

            return false;
        }

        return false;
    }
}

/*[HarmonyPatch(typeof(KeyBinder), "GetDisplayName")]
public static class KeyBinder_GetDisplayName
{
    public static bool Prefix(
        KeybindingsPC.Key k,
        ref string __result)
    {
        if (k is CustomKey customKey)
        {
            __result = Utils.GetDisplayName(customKey);
            return false;
        }

        return true;
    }
}*/

// get patched display name
[HarmonyPatch(typeof(KeyBinder), nameof(KeyBinder.Initialize))]
public static class KeyBinder_Initialize_Patch
{
    public static void Postfix(
        KeybindingsPC.Key key,
        TMPro.TMP_Text ___text)
    {
        if (key is CustomKey customKey)
            ___text.text = Utils.GetDisplayName(customKey);
    }
}

// dont let settings get nuked by reset to default
[HarmonyPatch(typeof(KeyBinder), nameof(KeyBinder.ResetToDefault))]
public static class KeyBinder_ResetToDefault_Patch
{
    public static bool Prefix(
        ref KeybindingsPC.Key ___key,
        KeybindingsPC.Key ___defaultKey,
        Action ___apply,
        TMPro.TMP_Text ___text)
    {
        if (___key is not CustomKey key)
            return true;

        if (___defaultKey is CustomKey defaultKey)
        {
            key.key = defaultKey.key;

            key.ctrl = defaultKey.ctrl;

            key.leftCtrl = defaultKey.leftCtrl;
            key.rightCtrl = defaultKey.rightCtrl;

            key.leftShift = defaultKey.leftShift;
            key.rightShift = defaultKey.rightShift;

            key.leftAlt = defaultKey.leftAlt;
            key.rightAlt = defaultKey.rightAlt;
        }
        else
        {
            // Vanilla default.
            key.key = ___defaultKey.key;
            key.ctrl = ___defaultKey.ctrl;
            key.leftCtrl = ___defaultKey.ctrl;
            key.rightCtrl = false;

            key.leftShift = false;
            key.rightShift = false;
            key.leftAlt = false;
            key.rightAlt = false;
        }

        ___apply();
        ___text.text = Utils.GetDisplayName(key);
        
        return false;
    }
}