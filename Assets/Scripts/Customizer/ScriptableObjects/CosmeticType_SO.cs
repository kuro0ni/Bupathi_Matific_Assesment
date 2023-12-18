using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticType_", menuName = "Scriptable Objects/Character Customization/CosmeticType")]
public class CosmeticType_SO : ScriptableObject
{
    [SerializeField]
    private int TypeId;
    [SerializeField] 
    private string TypeName;
    [SerializeField]
    private List<CosmeticItem_SO> Items;

    public CosmeticItem_SO GetItem(int itemId)
    {
        foreach (CosmeticItem_SO item in Items)
        {
            if (item.ItemId == itemId)
            {
                return item;
            }
        }

        return null;
    }

    public int GetTypeId() 
    { 
        return TypeId; 
    }

    public string GetTypeName()
    {
        return TypeName;
    }

    public List<CosmeticItem_SO> GetCosmeticItems()
    {
        return Items;
    }
}
