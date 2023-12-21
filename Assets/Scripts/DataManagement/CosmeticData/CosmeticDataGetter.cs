using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CosmeticDataGetter : ICosmeticDataGetter, IGameService
{
    IGameDataStorage GameDataStorage;

    private CosmeticData Data;

    public CosmeticDataGetter(IGameDataStorage gameDataStorage)
    {
        GameDataStorage = gameDataStorage;

        try
        {
            Data = gameDataStorage.Download<CosmeticData>();
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError("Could not find Cosmetic data file in the local drive.");
        }
        
    }

    public CosmeticData GetData()
    {
        return Data;
    }

    public CosmeticItem GetItemDataById(int itemId)
    {
        foreach (CosmeticItem item in Data.Items)
        {
            if (item.ItemId == itemId)
            {
                return item;
            }
        }

        return null; 
    }

    public void SetData<T>(T data)
    {
        GameDataStorage.Upload(data);
    }
}
