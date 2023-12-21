#if (UNITY_EDITOR)
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CosmesticItemPreviewRenderer : MonoBehaviour
{
    public ICosmeticComponent [] CosmeticComponents;

    public GameObject Character;

    [Expandable]
    public CosmeticItem_SO RenderItem;
    public GameObject FocusedRenderer;

    void Start()
    {
        
    }

    [Button("Apply Cosmetic")]
    public void ApplyCosmetic()
    {
        if (RenderItem == null) return;

        LoadCosmeticComponents();

        ICosmeticComponent cosmeticComp = GetRelatedCosmeticComponent(RenderItem);

        FocusedRenderer = cosmeticComp.GetRendererGameObject();
        FocusedRenderer.transform.localPosition = Vector3.zero;

        cosmeticComp.RenderItem(RenderItem.ItemId);
       
        UnityEditor.Selection.activeGameObject = FocusedRenderer;
    }

    private ICosmeticComponent GetRelatedCosmeticComponent(CosmeticItem_SO cosmeticItemSO)
    {
        for (int i = 0; i < CosmeticComponents.Length; ++i) 
        {
            CosmeticItem_SO itemSO = CosmeticComponents[i].GetCosmeticType().GetItem(cosmeticItemSO.ItemId);

            if (itemSO != null)
            {
                return CosmeticComponents[i];
            }
        }

        return null;
    }

    private void LoadCosmeticComponents()
    {
        if (CosmeticComponents != null) return;

        CosmeticComponents = Character.GetComponentsInChildren<ICosmeticComponent>();
    }

    [Button("Record Cosmetic Position Offset")]
    private void RecordCosmeticItemPositionOffset()
    {
        RenderItem.PositionOffset = FocusedRenderer.transform.localPosition;
        EditorUtility.SetDirty(RenderItem);
    }

}
#endif
