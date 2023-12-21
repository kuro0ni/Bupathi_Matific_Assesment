using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticsManager : MonoBehaviour, IGameService
{
    public List<CosmeticItem_SO> CosmeticItems;

    public UnityEvent<CosmeticData> OnCosmeticDataLoaded;

    private void Awake()
    {
        ServiceLocator.Current.Register(this, Service.COSMETIC_MANAGER);
    }

    /// <summary>
    /// Load cosmetic data from any avaialble source of storage and if there are no data use the scriptable objects to build the initial database
    /// </summary>
    public void LoadCosmeticsDatabase()
    {
        ICosmeticDataGetter cosmeticDataGetter = ServiceLocator.Current.Get<ICosmeticDataGetter>(Service.COSMETIC_DATA_GETTER);
        CosmeticData cosmeticData = cosmeticDataGetter.GetData();

        if (cosmeticData == null)
        {
            cosmeticData = new CosmeticData();
            cosmeticData.Items = new List<CosmeticItem>();

            BuildCosmeticsDatabase(cosmeticData, cosmeticDataGetter);
        }

        OnCosmeticDataLoaded.Invoke(cosmeticData);
    }

    /// <summary>
    /// Build cosmetic database using scriptable object data
    /// </summary>
    /// <param name="cosmeticData"></param>
    /// <param name="cosmeticDataGetter"></param>
    private void BuildCosmeticsDatabase(CosmeticData cosmeticData, ICosmeticDataGetter cosmeticDataGetter)
    {     
        foreach (CosmeticItem_SO itemSO in CosmeticItems)
        {
            CosmeticItem cosmeticItem = new CosmeticItem();
            cosmeticItem.ItemId = itemSO.ItemId;
            cosmeticItem.Price = itemSO.Price;
            cosmeticItem.MinLevel = itemSO.MinLevel;
            cosmeticItem.State = itemSO.State;

            cosmeticData.Items.Add(cosmeticItem);
        }

        Debug.Log("Saving cosmetics data");

        cosmeticDataGetter.SetData(cosmeticData);
    }

    public CosmeticItem_SO GetCosmeticSOByID(int itemId)
    {
        foreach (CosmeticItem_SO item in CosmeticItems)
        {
            if (item.ItemId == itemId)
            {
                return item;
            }
        }

        return null;
    }
}
