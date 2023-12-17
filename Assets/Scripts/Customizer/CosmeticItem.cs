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
}

public enum CosmeticItemState
{
    AVAILABLE = 0,
    PURCHASABLE = 1,
    LOCKED = 2
}