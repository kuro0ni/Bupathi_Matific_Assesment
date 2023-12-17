using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONFileReader : IFileReader
{
    private string Path;
    public JSONFileReader(string path)
    {
        Path = path;
    }

    public T ReadFile<T>()
    {
        StreamReader reader = new StreamReader(Path);

        string fileStr = reader.ReadToEnd();

        T data = JsonUtility.FromJson<T>(fileStr);

        return data;
    }

    public void WriteFile<T>(T data)
    {
        StreamWriter writer = new StreamWriter(Path);

        string dataStr = JsonUtility.ToJson(data);

        writer.WriteAsync(dataStr);
    }
}
