using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour, IGameService
{
    public List<AudioClipData_SO> AudioClips;

    public void PlayAudio(AudioClipType clipType, AudioSource speaker)
    {
        AudioClipData_SO clip = GetClip(clipType);

        if (clip == null) return;

        if (clip.Looping)
        {
            speaker.clip = clip.AudioClip;
            speaker.Play();
        }
        else
        {
            speaker.PlayOneShot(clip.AudioClip);
        }
    }

    private AudioClipData_SO GetClip(AudioClipType clipType)
    {
        for (int i = 0; i < AudioClips.Count; i++)
        {
            if (AudioClips[i].ClipType == clipType)
            {
                return AudioClips[i];
            }
        }

        return null;
    }
}