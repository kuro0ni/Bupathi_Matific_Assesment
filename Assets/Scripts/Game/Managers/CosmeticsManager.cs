using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticsManager : MonoBehaviour
{
    public List<CosmeticItem_SO> CosmeticItems;

    public UnityEvent<CosmeticData> OnCosmeticDataLoaded;
    public void BuildCosmeticsDatabase()
    {
        Debug.Log("Loading cosmetics database");

        ICosmeticDataGetter cosmeticDataGetter = (ICosmeticDataGetter)ServiceLocator.Current.Get(Service.COSMETIC_DATA_GETTER);
        CosmeticData cosmeticData = cosmeticDataGetter.GetData();

        if (cosmeticData == null)
        {
            Debug.Log("No cosmetics data found, building database from scriptable objects");

            cosmeticData = new CosmeticData();
            cosmeticData.Items = new List<CosmeticItem>();

            foreach (CosmeticItem_SO itemSO in CosmeticItems)
            {
                CosmeticItem cosmeticItem = new CosmeticItem();
                cosmeticItem.ItemId = itemSO.ItemId;
                cosmeticItem.Price  = itemSO.Price;
                cosmeticItem.MinLevel = itemSO.MinLevel;
                cosmeticItem.State = itemSO.State;
                
                cosmeticData.Items.Add(cosmeticItem);
            }

            Debug.Log("Saving cosmetics data");

            cosmeticDataGetter.SetData(cosmeticData);
        }

        OnCosmeticDataLoaded.Invoke(cosmeticData);
    }


}
