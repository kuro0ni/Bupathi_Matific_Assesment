using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnLevelInitialized;

    public AudioController AudioController;
    public AudioSource BackgroundMusicSpeaker;
      
    void Awake()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        InitializeAudioController();

        OnLevelInitialized.Invoke();
    }

    private void InitializeAudioController()
    {
        ServiceLocator.Current.Register(AudioController, Service.AUDIO_CONTROLLER);

        AudioController.PlayAudio(AudioClipType.BACKGROUND_MUSIC, BackgroundMusicSpeaker);
    }
}
