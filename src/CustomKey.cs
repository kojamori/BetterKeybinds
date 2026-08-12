using System;
using SFS.Input;
using UnityEngine;

namespace BetterKeybinds;

[Serializable]
public class CustomKey : KeybindingsPC.Key, I_Key
{
    public bool leftCtrl = false;
    public bool rightCtrl = false;
    public bool leftShift = false;
    public bool rightShift = false;
    public bool leftAlt = false;
    public bool rightAlt = false;

    public static CustomKey From(KeybindingsPC.Key key)
    {
        return new CustomKey(key.key)
        {
            leftCtrl = key.ctrl
        };
    }

    public static CustomKey Clone(CustomKey key)
    {
        return new CustomKey(key.key, key.leftCtrl, key.rightCtrl, key.leftAlt, key.rightAlt, key.leftShift, key.rightShift, key.ctrl);
    }
    
    public static implicit operator CustomKey(KeyCode key)
    {
        return new CustomKey(key);
    }

    public static CustomKey Ctrl(KeyCode key)
    {
        return new CustomKey(key)
        {
            ctrl = true,
            leftCtrl = true
        };
    }
    
    public CustomKey(KeyCode key, bool leftCtrl = false, bool rightCtrl = false, bool leftAlt = false, bool rightAlt = false, bool leftShift = false, bool rightShift = false, bool ctrl = false)
    {
        this.key = key;
        this.ctrl = leftCtrl || rightCtrl;
        this.leftCtrl = leftCtrl;
        this.rightCtrl = rightCtrl;
        this.leftAlt = leftAlt;
        this.rightAlt = rightAlt;
        this.leftShift = leftShift;
        this.rightShift = rightShift;
    }

    public CustomKey(KeyCode key)
    {
        this.key = key;
    }

    bool I_Key.IsKeyDown()
    {
        if (!Input.GetKeyDown(key))
            return false;

        bool leftCtrlDown = Input.GetKey(KeyCode.LeftControl);
        bool rightCtrlDown = Input.GetKey(KeyCode.RightControl);
        bool leftShiftDown = Input.GetKey(KeyCode.LeftShift);
        bool rightShiftDown = Input.GetKey(KeyCode.RightShift);
        bool leftAltDown = Input.GetKey(KeyCode.LeftAlt);
        bool rightAltDown = Input.GetKey(KeyCode.RightAlt);

        if (key != KeyCode.LeftControl &&
            leftCtrl != leftCtrlDown)
            return false;

        if (key != KeyCode.RightControl &&
            rightCtrl != rightCtrlDown)
            return false;

        if (key != KeyCode.LeftShift &&
            leftShift != leftShiftDown)
            return false;

        if (key != KeyCode.RightShift &&
            rightShift != rightShiftDown)
            return false;

        if (key != KeyCode.LeftAlt &&
            leftAlt != leftAltDown)
            return false;

        if (key != KeyCode.RightAlt &&
            rightAlt != rightAltDown)
            return false;

        return true;
    }

    bool I_Key.IsKeyStay()
    {
        return Input.GetKey(key);
    }

    bool I_Key.IsKeyUp()
    {
        return Input.GetKeyUp(key);
    }
}