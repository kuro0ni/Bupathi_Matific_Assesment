using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITabController : MonoBehaviour
{
    public GameObject TabBar;
    public GameObject TabBodyContainer;

    public GameObject TabPrefab;
    public GameObject TabBodyPrefab;

    private List<UITab> TabList;
    private int ActiveTab = 0;

    public UnityEvent<UITab, UITab> OnTabSelected;
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

        if (TabList == null) { TabList = new List<UITab>(); }

        TabList.Add(tab);

        int tabIndex = TabList.Count - 1;
        tab.TabBtn.onClick.AddListener(delegate { SetActiveTab(tabIndex); });

        return tab;
    }

    private void AddNewTabBody(UITab tab)
    {
        GameObject tabBodyGO = Instantiate(TabBodyPrefab, TabBodyContainer.transform);
        UITabBody tabBody = tabBodyGO.GetComponent<UITabBody>();

        tab.SetTabBody(tabBody);
    }

    public void SetActiveTab(int tabIndex)
    {
        int prevTab = ActiveTab;
        TabList[prevTab].TabUnSelected();

        ActiveTab = tabIndex;
        TabList[ActiveTab].TabSelected();

        OnTabSelected.Invoke(TabList[prevTab], TabList[ActiveTab]);
    }

    public List<UITab> GetTabList()
    {
        return TabList;
    }

    public UITab GetActiveTab()
    {
        return TabList[ActiveTab];
    }
}
