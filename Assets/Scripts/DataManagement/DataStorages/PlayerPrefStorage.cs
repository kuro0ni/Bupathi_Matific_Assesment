using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefStorage : IGameDataStorage
{
    string Key;
    IParser Parser;
    PlayerPrefType PrefType;

    public PlayerPrefStorage(string key, PlayerPrefType prefType, IParser parser)
    {
        Key = key;
        PrefType = prefType;
        Parser = parser;
    }

    public PlayerPrefStorage(string key)
    {
        Key = key;
    }

    public T Download<T>()
    {
        switch (PrefType)
        {
            case PlayerPrefType.STRING:
                return Parser.Parse<T>(PlayerPrefs.GetString(Key));

            case PlayerPrefType.INT:
                return (T)Convert.ChangeType(PlayerPrefs.GetInt(Key), typeof(T));

            case PlayerPrefType.FLOAT:
                return (T)Convert.ChangeType(PlayerPrefs.GetFloat(Key), typeof(T));

            default:
                break;
        }

        return default(T);
    }

    public void Upload<T>(T data)
    {
        switch (PrefType)
        {
            case PlayerPrefType.STRING:
                PlayerPrefs.SetString(Key, Parser.Unparse(data));
                break;

            case PlayerPrefType.INT:
                PlayerPrefs.SetInt(Key, (int)Convert.ChangeType(data, typeof(int)));
                break;

            case PlayerPrefType.FLOAT:
                PlayerPrefs.SetFloat(Key, (float)Convert.ChangeType(data, typeof(float)));
                break;

            default:
                break;
        }
    }
}

public enum PlayerPrefType
{
    STRING,
    INT,
    FLOAT

}