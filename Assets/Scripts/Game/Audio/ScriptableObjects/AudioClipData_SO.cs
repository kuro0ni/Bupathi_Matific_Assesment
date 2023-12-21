using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClip_", menuName = "Scriptable Objects/Audio/AudioClip")]
public class AudioClipData_SO : ScriptableObject 
{
    public AudioClip AudioClip;
    public AudioClipType ClipType;
    public bool Looping = false;
}

public enum AudioClipType
{
    BACKGROUND_MUSIC,
    BUTTON_CLICK_1,
    COSMETIC_PURCHASE,
    COSMETIC_APPLY
}
