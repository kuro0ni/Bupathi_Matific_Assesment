using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
