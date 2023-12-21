using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticComponent : MonoBehaviour, ICosmeticComponent
{
    [SerializeField] 
    private CosmeticType_SO CosmeticType;
    [SerializeField]
    private SpriteRenderer CosmeticRenderer;

    public void RenderItem(int itemId)
    {
        CosmeticRenderer.sprite = CosmeticType.GetItem(itemId).Sprite;
        CosmeticRenderer.transform.localPosition = CosmeticType.GetItem(itemId).PositionOffset;
    }

    public CosmeticType_SO GetCosmeticType()
    {
        return CosmeticType;
    }

    public void ClearRender()
    {
        CosmeticRenderer.sprite = null;
    }

    public GameObject GetRendererGameObject()
    {
        return CosmeticRenderer.gameObject;
    }
}
