using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONParser : IParser
{
    public T Parse<T>(string text)
    {
        return JsonUtility.FromJson<T>(text);
    }

    public string Unparse<T>(T obj)
    {
        return JsonUtility.ToJson(obj);
    }
}
