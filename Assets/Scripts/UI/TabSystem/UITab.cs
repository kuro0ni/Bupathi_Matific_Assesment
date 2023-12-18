using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITab : MonoBehaviour
{
    private UITabBody TabBody;
    private GameObject TabGraphic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTabBody(UITabBody tabBody)
    {
        TabBody = tabBody;
    }

    public void OnTabClicked()
    {
        TabBody.Show();
    }

    public void AddNewItem(GameObject itemGO)
    {
        TabBody.AddNewItem(itemGO);
    }

    public void SetTabGraphic(GameObject tabGraphic)
    {
        TabGraphic = tabGraphic;
    }

    public GameObject GetTabGraphic()
    {
        return TabGraphic;
    }
}
