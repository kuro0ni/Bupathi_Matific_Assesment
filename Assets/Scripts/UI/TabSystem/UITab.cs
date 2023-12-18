using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITab : MonoBehaviour
{
    private UITabBody TabBody;
    private GameObject TabGraphic;
    public Button TabBtn;
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

    public void TabSelected()
    {
        TabBody.Show();
    }

    public void TabUnSelected()
    {
        TabBody.Hide();
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
