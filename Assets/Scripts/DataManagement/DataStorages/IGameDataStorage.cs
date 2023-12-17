using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameDataStorage
{
    void Upload<T>(T data);
    T Download<T>();
}
