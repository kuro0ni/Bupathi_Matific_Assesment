using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParser
{
    T Parse<T>(string text);

    string Unparse<T>(T obj);
}
