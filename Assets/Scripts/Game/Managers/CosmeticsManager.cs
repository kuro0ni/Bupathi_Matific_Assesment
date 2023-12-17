using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsManager : MonoBehaviour
{
    public List<CosmeticItem_SO> CosmeticItems;

    public void BuildCosmeticsDatabase()
    {
        ICosmeticDataGetter cosmeticDataGetter = (ICosmeticDataGetter)ServiceLocator.Current.Get(Service.COSMETIC_DATA_GETTER);
        CosmeticData cosmeticData = cosmeticDataGetter.GetData();

        if (cosmeticData == null)
        {
            cosmeticData = new CosmeticData();

            foreach (CosmeticItem_SO itemSO in CosmeticItems)
            {
                CosmeticItem cosmeticItem = new CosmeticItem();
                //cosmeticItem.
            }
        }
    }
}
