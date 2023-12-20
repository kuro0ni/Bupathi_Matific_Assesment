using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICosmeticDataGetter
{
    CosmeticData GetData();
    CosmeticItem GetItemDataById(int itemId);

    void SetData<T>(T data);
}
