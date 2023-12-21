using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabBody : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void AddNewItem(GameObject itemGO)
    {
        itemGO.transform.SetParent(transform);
    }
}
