using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPreset_", menuName = "Scriptable Objects/Character Customization/CharacterPreset")]
public class CharacterPreset_SO : ScriptableObject
{
    [ReadOnly]
    [SerializeField]
    private string PresetId = System.Guid.NewGuid().ToString();
    [SerializeField]
    private string Name;
    [SerializeField]
    [OnValueChanged("OnBodyCosmeticChanged")]
    private CosmeticType_SO BodyCosmetic;
    [ReadOnly]
    [SerializeField]
    private List<CosmeticItem> Cosmetics;

    [ExecuteAlways]
    public void UpdatePreset(CosmeticItem item)
    {
        bool isNewItem = true;

        for (int i = 0; i < Cosmetics.Count; i++)
        {
            if (Cosmetics[i].TypeId == item.TypeId)
            {
                //Cosmetics[i].ItemId = item.ItemId;
                Cosmetics[i] = item;
                isNewItem = false;
                return;
            }
        }

        if (isNewItem)
        {
            Cosmetics.Add(item); 
        }
    }
    
    void OnBodyCosmeticChanged()
    {
        if (BodyCosmetic == null) return;

        CosmeticItem item = new CosmeticItem();
        item.TypeId = BodyCosmetic.GetTypeId();
        item.ItemId = 0;

        UpdatePreset(item);
    }

    public List<CosmeticItem> GetCosmetics() 
    { 
        return Cosmetics; 
    }

    public void ResetCosmetics()
    {
        Cosmetics.Clear();
    }

}
