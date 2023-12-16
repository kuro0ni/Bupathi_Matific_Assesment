using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICosmeticComponent
{
    void RenderItem(int itemId);
    CosmeticType_SO GetCosmeticType();

    void ClearRender();
}
