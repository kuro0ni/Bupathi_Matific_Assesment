using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICosmeticSelection : MonoBehaviour
{
    [SerializeField]
    private UITabController TabController;
    [SerializeField]
    private CharacterCustomizer CharacterCustomizer;

    [Header("Tab System Graphics")]
    [SerializeField]
    private GameObject TabGraphicPrefab;
    [SerializeField]
    private GameObject TabBodyItemGraphicPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateUI(CosmeticData data)
    {
        ICosmeticComponent [] cosmeticComponents = CharacterCustomizer.GetCharacterCosmeticComponents();

        foreach (ICosmeticComponent cosmeticComp in cosmeticComponents)
        {
            UITab tab = PopulateNewTab(cosmeticComp);

            GameObject cosmeticItemGO = Instantiate(TabBodyItemGraphicPrefab);
            UICosmeticTabItem cosmeticItem = cosmeticItemGO.GetComponent<UICosmeticTabItem>();

            foreach (CosmeticItem_SO itemSO in cosmeticComp.GetCosmeticType().GetCosmeticItems())
            {
                CosmeticItem item = data.GetItem(itemSO.ItemId);
                //cosmeticItem.PopulateItem(item, OnCosmeticItemSelected);
            }
        
            tab.AddNewItem(cosmeticItemGO);
        }        
    }

    private UITab PopulateNewTab(ICosmeticComponent cosmeticComp)
    {
        UITab tab = TabController.AddNewTab(TabGraphicPrefab);
        GameObject tabGraphicGO = tab.GetTabGraphic();

        tabGraphicGO.transform.GetChild(1).GetComponent<TMP_Text>().text = cosmeticComp.GetCosmeticType().GetTypeName();

        return tab;
    }

    public void OnCosmeticItemSelected()
    {

    }
}
