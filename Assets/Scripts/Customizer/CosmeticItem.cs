using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class CosmeticItem
{
    [System.NonSerialized]
    public int TypeId;
    public int ItemId;
    public int Price;
    public int MinLevel;
    public CosmeticItemState State;
 
    /// <summary>
    /// Change this item object's state from the given user data
    /// </summary>
    /// <param name="userData"></param>
    public void SetItemStateByUserData(UserData userData)
    {
        if (State == CosmeticItemState.AVAILABLE) return;

        if (userData.Level < MinLevel)
        {
            State = CosmeticItemState.LOCKED;
        }
        else
        {
            if (Price <= 0)
            {
                State = CosmeticItemState.AVAILABLE;
            }
            else
            {
                State = CosmeticItemState.PURCHASABLE;
            }           
        }

    }

    public void SetItemState(CosmeticItemState state)
    {
        State = state;
    }
}

public enum CosmeticItemState
{
    AVAILABLE = 0,
    PURCHASABLE = 1,
    LOCKED = 2
}