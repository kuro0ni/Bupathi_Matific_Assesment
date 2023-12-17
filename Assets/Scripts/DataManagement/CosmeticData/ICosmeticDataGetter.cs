using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICosmeticDataGetter
{
    CosmeticData GetData();
    void SetData<T>(T data);
}
