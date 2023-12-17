using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticComponent : MonoBehaviour, ICosmeticComponent
{
    [SerializeField] 
    private CosmeticType_SO CosmeticType;
    [SerializeField]
    private SpriteRenderer CosmeticRenderer;

    private int SelectedItemId;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RenderItem(int itemId)
    {
        Debug.Log($"Rendering item {itemId}");
        SelectedItemId = itemId;
        CosmeticRenderer.sprite = CosmeticType.GetItem(itemId).Sprite;
    }

    public CosmeticType_SO GetCosmeticType()
    {
        return CosmeticType;
    }

    public void ClearRender()
    {
        CosmeticRenderer.sprite = null;
    }
}
