using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticItem_", menuName = "Scriptable Objects/Character Customization/CosmeticItem")]
public class CosmeticItem_SO : ScriptableObject
{
    public int ItemId;
    public int Price;
    public int MinLevel;
    public CosmeticItemState State;
    public Sprite Sprite;
}
