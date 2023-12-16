using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticDataGetter : ICosmeticDataGetter
{
    private CosmeticData Data;

    public CosmeticDataGetter()
    {

    }

    public CosmeticData GetData()
    {
        return new CosmeticData();
    }
}
