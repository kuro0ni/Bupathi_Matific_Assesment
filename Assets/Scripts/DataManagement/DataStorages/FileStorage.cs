using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileStorage : IGameDataStorage
{
    IFileReader FileReader;
    public FileStorage(IFileReader fileReader) 
    {
        FileReader = fileReader;
    }

    public T Download<T>()
    {
        T data = FileReader.ReadFile<T>();
        return data;
    }

    public void Upload<T>(T data)
    {
        FileReader.WriteFile(data);
    }
}

public enum GameData
{
    USER_DATA = 0,
    COSMETIC_DATA = 1
}