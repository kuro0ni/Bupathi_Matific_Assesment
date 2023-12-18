using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CharacterCustomizer : MonoBehaviour
{
    [SerializeField]
    private ICosmeticComponent [] CosmeticComponentList;
    [Expandable]
    [SerializeField]
    private CharacterPreset_SO CharacterPreset;
    [SerializeField]
    private GameObject CustomizerComponents;
    // Start is called before the first frame update
    void Start() 
    {
    
    }
    
    public void ApplyCosmetic(CosmeticItem item)
    {
        foreach (ICosmeticComponent component in CosmeticComponentList)
        {
            if (component.GetCosmeticType().GetTypeId() == item.TypeId)
            {
                CharacterPreset.UpdatePreset(item);
                component.RenderItem(item.ItemId);
            }
        }
    }

    [Button("Random Cosmetic")]
    public void ApplyCosmeticOnEditor()
    {
        if (CosmeticComponentList == null) 
        {
            CosmeticComponentList = GetComponentsInChildren<ICosmeticComponent>();
        }

        CosmeticItem item = new CosmeticItem();
        item.ItemId = Random.Range(0, 10);
        item.TypeId = Random.Range(1, 4);

        ApplyCosmetic(item);
    }

    [Button("Apply Preset")]
    public void ApplyPresetOnEditor()
    {
        if (CosmeticComponentList == null)
        {
            CosmeticComponentList = GetComponentsInChildren<ICosmeticComponent>();
        }

        List<CosmeticItem> cosmetics = CharacterPreset.GetCosmetics();

        foreach (CosmeticItem item in cosmetics)
        {
            ApplyCosmetic(item);
        }   
    }

    [Button("Reset Cosmetics")]
    public void ResetCosmetics()
    {
        if (CosmeticComponentList == null)
        {
            CosmeticComponentList = GetComponentsInChildren<ICosmeticComponent>();
        }

        foreach (ICosmeticComponent component in CosmeticComponentList)
        {
            component.ClearRender();
        }
    }

    public ICosmeticComponent [] GetCharacterCosmeticComponents()
    {
        if (CosmeticComponentList == null)
        {
            CosmeticComponentList = CustomizerComponents.transform.GetComponentsInChildren<ICosmeticComponent>();
        }

        return CosmeticComponentList;
    }
}
