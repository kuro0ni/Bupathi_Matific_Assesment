using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONFileReader : IFileReader
{
    public T ReadFile<T>(string path)
    {
        StreamReader reader = new StreamReader(path);

        string fileStr = reader.ReadToEnd();

        T data = JsonUtility.FromJson<T>(fileStr);

        return data;
    }

    public void WriteFile<T>(string path, T data)
    {
        StreamWriter writer = new StreamWriter(path);

        string dataStr = JsonUtility.ToJson(data);

        writer.WriteAsync(dataStr);
    }
}
