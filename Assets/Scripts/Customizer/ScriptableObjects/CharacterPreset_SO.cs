using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPreset_", menuName = "Scriptable Objects/Character Customization/CharacterPreset")]
public class CharacterPreset_SO : ScriptableObject
{
    private string PresetId = System.Guid.NewGuid().ToString();
    [SerializeField]
    private string Name;
    [SerializeField]
    private Dictionary<int,Sprite> Cosmetics;
}
