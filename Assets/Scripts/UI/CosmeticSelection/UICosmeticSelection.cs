using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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

    private CosmeticData CosmeticData;

    public UnityEvent<UserData> RefreshItemsEvent;
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
        CosmeticData = data;

        ICosmeticComponent [] cosmeticComponents = CharacterCustomizer.GetCharacterCosmeticComponents();

        IUserStatManager userStatManager = (IUserStatManager)ServiceLocator.Current.Get(Service.USER_STAT_MANAGER);
        UserData userData = userStatManager.GetUserData();

        foreach (ICosmeticComponent cosmeticComp in cosmeticComponents)
        {
            UITab tab = PopulateNewTab(cosmeticComp);
         
            foreach (CosmeticItem_SO itemSO in cosmeticComp.GetCosmeticType().GetCosmeticItems())
            {
                GameObject cosmeticTabItemGO = Instantiate(TabBodyItemGraphicPrefab);
                cosmeticTabItemGO.name = itemSO.Sprite.name;

                UICosmeticTabItem cosmeticTabItem = cosmeticTabItemGO.GetComponent<UICosmeticTabItem>();

                CosmeticItem item = data.GetItem(itemSO.ItemId);
                item.TypeId = cosmeticComp.GetCosmeticType().GetTypeId();

                cosmeticTabItem.PopulateItem(item, itemSO, userData, RefreshItemsEvent);
                cosmeticTabItem.ItemBtn.onClick.AddListener(delegate { OnCosmeticItemSelected(cosmeticTabItem); } );

                tab.AddNewItem(cosmeticTabItemGO);
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
        if (cosmeticTabItem.CosmeticItemData.State == CosmeticItemState.PURCHASABLE)
        {
            bool isPurchaseSuccessfull = PurchaseCosmeticItem(cosmeticTabItem);

            if (!isPurchaseSuccessfull) return;
        }

        CharacterCustomizer.ApplyCosmetic(cosmeticTabItem.CosmeticItemData);
    }

    private bool PurchaseCosmeticItem(UICosmeticTabItem cosmeticTabItem)
    {
        bool isPurchaseSuccessfull = false;

        IUserStatManager userStatManager = (IUserStatManager)ServiceLocator.Current.Get(Service.USER_STAT_MANAGER);
        ICosmeticDataGetter cosmeticDataGetter = (ICosmeticDataGetter)ServiceLocator.Current.Get(Service.COSMETIC_DATA_GETTER);

        UserData userData = userStatManager.GetUserData();

        if (userData.Coins >= cosmeticTabItem.CosmeticItemData.Price)
        {
            userData.Coins -= cosmeticTabItem.CosmeticItemData.Price;
            cosmeticTabItem.CosmeticItemData.SetItemState(CosmeticItemState.AVAILABLE);

            RefreshItemsEvent.Invoke(userData);

            userStatManager.SetUserData(userData);

            cosmeticDataGetter.SetData(CosmeticData);

            isPurchaseSuccessfull = true;
        }

        return isPurchaseSuccessfull;
    }
}
