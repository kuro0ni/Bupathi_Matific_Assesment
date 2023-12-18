using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabController : MonoBehaviour
{
    public GameObject TabBar;
    public GameObject TabBodyContainer;

    public GameObject TabPrefab;
    public GameObject TabBodyPrefab;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UITab AddNewTab(GameObject tabGraphicPrefab)
    {
        GameObject tabGO = Instantiate(TabPrefab, TabBar.transform);
        GameObject tabGraphicGO = Instantiate(tabGraphicPrefab, tabGO.transform);

        UITab tab = tabGO.GetComponent<UITab>();
        tab.SetTabGraphic(tabGraphicGO);

        AddNewTabBody(tab);

        return tab;
    }

    private void AddNewTabBody(UITab tab)
    {
        GameObject tabBodyGO = Instantiate(TabBodyPrefab, TabBodyContainer.transform);
        UITabBody tabBody = tabBodyGO.GetComponent<UITabBody>();

        tab.SetTabBody(tabBody);
    }
}
