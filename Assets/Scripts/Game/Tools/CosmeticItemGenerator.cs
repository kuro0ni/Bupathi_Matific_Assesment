#if (UNITY_EDITOR)
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CosmeticItemGenerator : MonoBehaviour
{
    public List<Sprite> Items;
    public string Path = "Assets/ScriptableObjects/Customizer/CosmeticItems/";

    [Button("Generate Items")]
    void GenerateItemAssets()
    {
        int itemId = GetFileCountInFolder(Path);

        for (int i = 0; i < Items.Count; i++)
        {
            CreateScriptableObject(Items[i], itemId);
            itemId++;
        }
    }

    void CreateScriptableObject(Sprite itemSprite, int itemId)
    {
        CosmeticItem_SO asset = ScriptableObject.CreateInstance<CosmeticItem_SO>();
        asset.Sprite = itemSprite;
        asset.ItemId = itemId;
        AssetDatabase.CreateAsset(asset, $"{Path}{itemSprite.name}.asset");
        AssetDatabase.SaveAssets();
    }

    int GetFileCountInFolder(string folderPath)
    {
        string[] assetPaths = AssetDatabase.FindAssets("t:ScriptableObject", new[] { folderPath }); //

        return assetPaths.Length;
    }
}
#endif