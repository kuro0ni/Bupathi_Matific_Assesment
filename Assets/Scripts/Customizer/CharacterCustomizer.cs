using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class CharacterCustomizer : MonoBehaviour
{
    [SerializeField]
    private ICosmeticComponent[] CosmeticComponentList;
    [Expandable]
    [SerializeField]
    private CharacterPreset_SO CharacterPresetSO;
    [SerializeField]
    private GameObject CustomizerComponents;
    // Start is called before the first frame update
    void Start()
    {
        PlayInAnimation();
        Init();       
    }

    public void Init()
    {
        CharacterPresetSO.ResetCosmetics();
        CharacterPresetSO.LoadPreset();
        ApplyPreset();
    }

    public void ApplyCosmetic(CosmeticItem item)
    {
        foreach (ICosmeticComponent component in CosmeticComponentList)
        {
            if (component.GetCosmeticType().GetTypeId() == item.TypeId)
            {
                CharacterPresetSO.UpdatePresetCosmetics(item);
                component.RenderItem(item.ItemId);
            }
        }

        CharacterPresetSO.SavePreset();
    }

    public void ApplyPreset()
    {
        if (CosmeticComponentList == null)
        {
            CosmeticComponentList = GetComponentsInChildren<ICosmeticComponent>();
        }

        List<CosmeticItem> cosmetics = CharacterPresetSO.GetCosmetics();

        for (int i = 0; i < cosmetics.Count; i++)
        {
            ApplyCosmetic(cosmetics[i]);
        }
    }

    [Button("Reset Cosmetics")]
    public void ResetCosmetics()
    {
        if (CosmeticComponentList == null)
        {
            CosmeticComponentList = CustomizerComponents.GetComponentsInChildren<ICosmeticComponent>();
        }

        foreach (ICosmeticComponent component in CosmeticComponentList)
        {
            component.ClearRender();
        }
    }

    public ICosmeticComponent[] GetCharacterCosmeticComponents()
    {
        if (CosmeticComponentList == null)
        {
            CosmeticComponentList = CustomizerComponents.transform.GetComponentsInChildren<ICosmeticComponent>();
        }

        return CosmeticComponentList;
    }


    public CharacterPreset_SO GetActivePreset()
    {
        return CharacterPresetSO;
    }

    private void PlayInAnimation()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, -7.5f, transform.localPosition.z);
        LeanTween.moveLocalY(gameObject, -0.86f, 1.5f).setEaseOutCubic().setDelay(1);
    }
}
