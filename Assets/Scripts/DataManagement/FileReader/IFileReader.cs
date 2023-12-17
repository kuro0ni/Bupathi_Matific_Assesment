using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFileReader
{
    T ReadFile<T>();
    void WriteFile<T>(T data);
}
