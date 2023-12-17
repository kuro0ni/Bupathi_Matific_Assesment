using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnLevelInitialized;
    void Awake()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        OnLevelInitialized.Invoke();
    }
}
