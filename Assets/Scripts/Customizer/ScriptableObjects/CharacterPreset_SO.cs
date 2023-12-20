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

    public CharacterPreset GetPresetData()
    {
        CharacterPreset preset = new CharacterPreset();
        preset.PresetId = PresetId;
        preset.BodyCosmetic = BodyCosmetic.GetTypeId();
        preset.Cosmetics = new List<int>();

        foreach (CosmeticItem item in Cosmetics)
        {
            preset.Cosmetics.Add(item.ItemId);
        }

        return preset;
    }

    public void LoadPreset()
    {
        IUserDataGetter userDataGetter = ServiceLocator.Current.Get<IUserDataGetter>(Service.USER_DATA_GETTER);

        UserData userData = userDataGetter.GetData();

        for (int i = 0; i < userData.MyCharacters.Count; i++)
        {
            if (userData.MyCharacters[i].PresetId == PresetId)
            {
                PopulatePreset(userData.MyCharacters[i]);
            }
        }
    }

    private void PopulatePreset(CharacterPreset presetData)
    {
        Name = presetData.CharacterName;
        ICosmeticDataGetter cosmeticDataGetter = ServiceLocator.Current.Get<ICosmeticDataGetter>(Service.COSMETIC_DATA_GETTER);

        foreach (int itemId in presetData.Cosmetics)
        {
            CosmeticItem item = cosmeticDataGetter.GetItemDataById(itemId);
            UpdatePreset(item);
        }       
    }

    public void SavePreset()
    {
        if (!Application.isPlaying) return;

        CharacterPreset preset = GetPresetData();

        IUserDataGetter userDataGetter = ServiceLocator.Current.Get<IUserDataGetter>(Service.USER_DATA_GETTER);

        UserData userData = userDataGetter.GetData();

        bool isNewCharacterPreset = true;

        for (int i = 0; i < userData.MyCharacters.Count; i++)
        {
            if (userData.MyCharacters[i].PresetId == preset.PresetId)
            {
                userData.MyCharacters[i] = preset;
                isNewCharacterPreset = false;
                break;
            }
        }

        if (isNewCharacterPreset)
        {
            userData.MyCharacters.Add(preset);
        }

        userDataGetter.SetData(userData);
    }

}
