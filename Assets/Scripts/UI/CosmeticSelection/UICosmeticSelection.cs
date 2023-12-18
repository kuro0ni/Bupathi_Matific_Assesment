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

        IUserStatManager userStatManager = (IUserStatManager)ServiceLocator.Current.Get(Service.USER_STAT_MANAGER);
        UserData userData = userStatManager.GetUserData();

        foreach (ICosmeticComponent cosmeticComp in cosmeticComponents)
        {
            UITab tab = PopulateNewTab(cosmeticComp);
         
            foreach (CosmeticItem_SO itemSO in cosmeticComp.GetCosmeticType().GetCosmeticItems())
            {
                GameObject cosmeticItemGO = Instantiate(TabBodyItemGraphicPrefab);
                UICosmeticTabItem cosmeticTabItem = cosmeticItemGO.GetComponent<UICosmeticTabItem>();

                CosmeticItem item = data.GetItem(itemSO.ItemId);
                item.TypeId = cosmeticComp.GetCosmeticType().GetTypeId();

                cosmeticTabItem.PopulateItem(item, itemSO, userData);
                cosmeticTabItem.ItemBtn.onClick.AddListener(delegate { OnCosmeticItemSelected(cosmeticTabItem); } );

                tab.AddNewItem(cosmeticItemGO);
            }

            tab.TabUnSelected();
          
        }

       TabController.SetActiveTab(0);
    }

    private UITab PopulateNewTab(ICosmeticComponent cosmeticComp)
    {
        UITab tab = TabController.AddNewTab(TabGraphicPrefab);
        GameObject tabGraphicGO = tab.GetTabGraphic();

        tabGraphicGO.transform.GetChild(1).GetComponent<TMP_Text>().text = cosmeticComp.GetCosmeticType().GetTypeName();

        return tab;
    }

    public void OnCosmeticItemSelected(UICosmeticTabItem cosmeticTabItem)
    {
        CharacterCustomizer.ApplyCosmetic(cosmeticTabItem.CosmeticItemData);
    }


}
