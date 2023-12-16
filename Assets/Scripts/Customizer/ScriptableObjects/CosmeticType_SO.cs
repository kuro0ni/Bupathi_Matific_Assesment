using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticType_", menuName = "Scriptable Objects/Character Customization/CosmeticType")]
public class CosmeticType_SO : ScriptableObject
{
    [SerializeField]
    private int TypeId;
    [SerializeField]
    private List<Sprite> Sprites;

    public Sprite GetItem(int itemId)
    {
        return Sprites[itemId];
    }

    public int GetTypeId() 
    { 
        return TypeId; 
    }
}
