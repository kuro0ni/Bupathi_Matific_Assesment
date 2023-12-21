using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class CosmeticData
{
    public List<CosmeticItem> Items;

    public CosmeticItem GetItem(int itemId)
    {
        foreach (CosmeticItem item in Items)
        {
            if (item.ItemId == itemId)
            {
                return item;
            }
        }

        return null;
    }
}
