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

    //private LTDescr ActiveCosmeticItemAnim;

    [SerializeField]
    private List<CosmeticUITabData> CosmeticTabData;
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

        ICosmeticComponent[] cosmeticComponents = CharacterCustomizer.GetCharacterCosmeticComponents();

        IUserDataGetter userStatManager = ServiceLocator.Current.Get<IUserDataGetter>(Service.USER_DATA_GETTER);
        UserData userData = userStatManager.GetData();

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
                cosmeticTabItem.ItemBtn.onClick.AddListener(delegate { OnCosmeticItemSelected(cosmeticTabItem); });

                tab.AddNewItem(cosmeticTabItemGO);
            }

            tab.TabUnSelected();

        }

        TabController.OnTabSelected.AddListener(OnNewTabSelected);
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
        CosmeticUITabData tabData = GetTabData(TabController.GetActiveTab());

        if (cosmeticTabItem == tabData.ActiveCosmeticItem) return;

        if (cosmeticTabItem.CosmeticItemData.State == CosmeticItemState.PURCHASABLE)
        {
            bool isPurchaseSuccessfull = PurchaseCosmeticItem(cosmeticTabItem);

            if (!isPurchaseSuccessfull) return;
        }

        CacheActiveCosmeticItem(cosmeticTabItem);

        PlayCosmeticTabItemSelectionAnimation(cosmeticTabItem);       

        CharacterCustomizer.ApplyCosmetic(cosmeticTabItem.CosmeticItemData);
    }

    private void CacheActiveCosmeticItem(UICosmeticTabItem cosmeticTabItem)
    {
        CosmeticUITabData tabData = GetTabData(TabController.GetActiveTab());

        if (tabData.PreviousCosmeticItem == null)
        {
            tabData.PreviousCosmeticItem = cosmeticTabItem;
        }
        else
        {
            tabData.PreviousCosmeticItem = tabData.ActiveCosmeticItem;
        }

        //if (PrevCosmeticTabItem != null)
        //{
        //    PrevCosmeticTabItem = ActiveCosmeticTabItem;
        //}


        tabData.ActiveCosmeticItem = cosmeticTabItem;
    }

    private bool PurchaseCosmeticItem(UICosmeticTabItem cosmeticTabItem)
    {
        bool isPurchaseSuccessfull = false;

        IUserDataGetter userDataGetter = ServiceLocator.Current.Get<IUserDataGetter>(Service.USER_DATA_GETTER);
        ICosmeticDataGetter cosmeticDataGetter = ServiceLocator.Current.Get<ICosmeticDataGetter>(Service.COSMETIC_DATA_GETTER);

        UserData userData = userDataGetter.GetData();

        if (userData.Coins >= cosmeticTabItem.CosmeticItemData.Price)
        {
            userData.Coins -= cosmeticTabItem.CosmeticItemData.Price;
            cosmeticTabItem.CosmeticItemData.SetItemState(CosmeticItemState.AVAILABLE);

            RefreshItemsEvent.Invoke(userData);

            userDataGetter.SetData(userData);

            cosmeticDataGetter.SetData(CosmeticData);

            isPurchaseSuccessfull = true;
        }

        return isPurchaseSuccessfull;
    }

    private void OnNewTabSelected(UITab oldTab, UITab newTab)
    {
        PlayNewTabSelectionAnimation(oldTab, newTab);
    }

    private void PlayCosmeticTabItemSelectionAnimation(UICosmeticTabItem cosmeticTabItem)
    {
        CosmeticUITabData tabData = GetTabData(TabController.GetActiveTab());

        if (tabData.PreviousCosmeticItem != null)
        {
            if (tabData.ActiveCosmeticItem != null)
            {
                tabData.ActiveCosmeticItemAnim?.reset();
            }

            LeanTween.scale(tabData.PreviousCosmeticItem.ItemBtn.gameObject, tabData.PreviousCosmeticItem.ItemBtn.transform.localScale * 0.833f, 0.25f).setEaseOutQuad();
        }

        if (tabData.ActiveCosmeticItem != null)
        {
            tabData.ActiveCosmeticItemAnim = LeanTween.scale(tabData.ActiveCosmeticItem.ItemBtn.gameObject, tabData.ActiveCosmeticItem.ItemBtn.transform.localScale * 1.2f, 0.4f).setLoopPingPong().setEaseInQuad();
        }
    }

    private void PlayNewTabSelectionAnimation(UITab oldTab, UITab newTab)
    {
        LeanTween.scale(oldTab.TabBtn.gameObject, oldTab.TabBtn.transform.localScale * 0.833f, 0.2f).setEaseInOutQuad();
        LeanTween.scale(newTab.TabBtn.gameObject, newTab.TabBtn.transform.localScale * 1.2f, 0.2f).setEaseInOutQuad();
    }

    private CosmeticUITabData GetTabData(UITab tab)
    {
        if (CosmeticTabData == null)
        {
            CosmeticTabData = new List<CosmeticUITabData>();
        }

        foreach (CosmeticUITabData item in CosmeticTabData)
        {
            if (item.Tab == tab)
            {
                return item;
            }
        }

        CosmeticUITabData tabData = new CosmeticUITabData();
        tabData.Tab = tab;
        CosmeticTabData.Add(tabData);

        return tabData;
    }
}
