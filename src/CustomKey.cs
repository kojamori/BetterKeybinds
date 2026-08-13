using System;
using SFS.Input;
using System.Collections.Generic;
using UnityEngine;

namespace BetterKeybinds;

[Serializable]
public class CustomKey : KeybindingsPC.Key, I_Key
{
    #region Modifier Key Fields

    public bool leftCtrl = false;
    public bool rightCtrl = false;
    public bool leftShift = false;
    public bool rightShift = false;
    public bool leftAlt = false;
    public bool rightAlt = false;

    #endregion
    
    #region Constructors
    public CustomKey(KeyCode key)
    {
        this.key = key;
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
    
    public static CustomKey From(KeybindingsPC.Key key)
    {
        return new CustomKey(key.key)
        {
            leftCtrl = key.ctrl
        };
    }
    
    public static implicit operator CustomKey(KeyCode key)
    {
        return new CustomKey(key);
    }
    #endregion

    #region Static Factory Methods
    public static CustomKey Clone(CustomKey key)
    {
        return new CustomKey(key.key, key.leftCtrl, key.rightCtrl, key.leftAlt, key.rightAlt, key.leftShift, key.rightShift, key.ctrl);
    }

    public static CustomKey LCtrl(KeyCode key)
    {
        return new CustomKey(key)
        {
            ctrl = true,
            leftCtrl = true
        };
    }
    
    public static CustomKey RCtrl(KeyCode key)
    {
        return new CustomKey(key)
        {
            ctrl = true,
            rightCtrl = true
        };
    }
    
    public static CustomKey LShift(KeyCode key)
    {
        return new CustomKey(key)
        {
            leftShift = true
        };
    }
    
    public static CustomKey RShift(KeyCode key)
    {
        return new CustomKey(key)
        {
            rightShift = true
        };
    }
    
    public static CustomKey LAlt(KeyCode key)
    {
        return new CustomKey(key)
        {
            rightShift = true
        };
    }
    
    public static CustomKey RAlt(KeyCode key)
    {
        return new CustomKey(key)
        {
            rightShift = true
        };
    }
    #endregion
    
    #region I_Key Implementation
    bool I_Key.IsKeyDown()
    {
        if (LockedKeys.Contains(this.key)) return false;
        
        if (LockedCodes.Contains(this.key) && !ExcludedKeys.Contains(this))
        {
            return false;
        }
        
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

    #endregion

    #region Key Locking
    public static HashSet<KeyCode> LockedCodes = new();
    public static HashSet<CustomKey> LockedKeys = new();

    public static HashSet<CustomKey> ExcludedKeys = new();
    public static void LockKeyCode(CustomKey excludedKey)
    {
        LockedCodes.Add(excludedKey.key);
        ExcludedKeys.Add(excludedKey);
    }
    
    public static void LockKeyCode(KeyCode keyCode)
    {
        LockedCodes.Add(keyCode);
    }
    
    public static void LockKey(CustomKey target)
    {
        LockedKeys.Add(target);
    }
    
    public static void UnlockKeyCode(KeyCode keyCode)
    {
        LockedCodes.Remove(keyCode);
        ExcludedKeys.RemoveWhere(k => k.key == keyCode);
    }

    public static void UnlockKeyCode(KeyCode keyCode, CustomKey owner)
    {
        ExcludedKeys.Remove(owner);
    }

    #endregion
    
}