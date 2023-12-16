using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFileReader
{
    T ReadFile<T>(string path);
    void WriteFile<T>(string path, T data);
}
