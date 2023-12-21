using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathsConfig
{
    public static readonly string COSMETICS_PATH = Application.persistentDataPath + "\\CosmeticData.json";
    public static readonly string USER_DATA_PATH = Application.persistentDataPath + "\\UserData.json";

    public static readonly string COSMETICS_PREF_KEY = "CosmeticData";
    public static readonly string USER_DATA_PREF_KEY = "UserData";
}
