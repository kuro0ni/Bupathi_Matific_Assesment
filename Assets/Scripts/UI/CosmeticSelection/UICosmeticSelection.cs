using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UICosmeticSelection : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private UITabController TabController;
    [SerializeField]
    private CharacterCustomizer CharacterCustomizer;

    [Header("Tab System Graphics")]
    [SerializeField]
    private GameObject TabGraphicPrefab;
    [SerializeField]
    private GameObject TabBodyItemGraphicPrefab;

    [Header("Audio")]
    public AudioSource ButtonSpeaker;
    public AudioSource CustomizerSpeaker;

   [HideInInspector]
    public UnityEvent<UserData> RefreshItemsEvent;
    [SerializeField]
    private List<CosmeticUITabData> CosmeticTabData;

    private CosmeticData CosmeticData;

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// Use Cosmetic and User data to populate the cosmetic selection UI
    /// </summary>
    /// <param name="data"></param>
    public void PopulateUI(CosmeticData data)
    {
        CosmeticData = data;

        ToggleSpeakers(false);

        ICosmeticComponent[] cosmeticComponents = CharacterCustomizer.GetCharacterCosmeticComponents();

        IUserDataGetter userDataGetter = ServiceLocator.Current.Get<IUserDataGetter>(Service.USER_DATA_GETTER);
        UserData userData = userDataGetter.GetData();

        CharacterPreset activePreset = GetActiveCharacterPresetData(userData, CharacterCustomizer.GetActivePreset().GetPresetData().PresetId);
        List<UICosmeticTabItem> presetActivatedTabItems = new List<UICosmeticTabItem>();

        foreach (ICosmeticComponent cosmeticComp in cosmeticComponents)
        {
            PopulateTabContent(cosmeticComp, data, userData, activePreset, presetActivatedTabItems);
        }

        SelectActivePresetItems(presetActivatedTabItems);

        TabController.OnTabSelected.AddListener(OnNewTabSelected);
        TabController.SetActiveTab(0);

        OnPopulationUIFinished();
    }

    private void OnPopulationUIFinished()
    {
        ToggleSpeakers(true);
    }

    /// <summary>
    /// Loop through available cosmetic components within the Character object in the scene to create new tabs and related tab content
    /// </summary>
    /// <param name="cosmeticComp"></param>
    /// <param name="data"></param>
    /// <param name="userData"></param>
    /// <param name="activePreset"></param>
    /// <param name="presetActivatedTabItems"></param>
    private void PopulateTabContent(ICosmeticComponent cosmeticComp, CosmeticData data, UserData userData, CharacterPreset activePreset, List<UICosmeticTabItem> presetActivatedTabItems)
    {
        UITab tab = PopulateNewTab(cosmeticComp);
        GetTabData(tab);

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

            if (IsAnActivePresetItem(activePreset, cosmeticTabItem))
            {
                presetActivatedTabItems.Add(cosmeticTabItem);
            }

            tab.TabUnSelected();
        }
    }

    /// <summary>
    /// Check if a a certain cosmetic item in a tab is also used in the active preset on the character
    /// </summary>
    /// <param name="activePreset"></param>
    /// <param name="cosmeticTabItem"></param>
    /// <returns></returns>
    private bool IsAnActivePresetItem(CharacterPreset activePreset, UICosmeticTabItem cosmeticTabItem)
    {
        if (activePreset == null) return false;

        foreach (int itemId in activePreset.Cosmetics)
        {
            if (itemId == cosmeticTabItem.CosmeticItemData.ItemId)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Loop through the given tab items that are in the active character preset and highlight them by invoking the onClick event on the items' buttons. Mimicking an item selection.
    /// </summary>
    /// <param name="presetActivatedTabItems"></param>
    private void SelectActivePresetItems(List<UICosmeticTabItem> presetActivatedTabItems)
    {
        for (int i = 0; i < presetActivatedTabItems.Count; i++)
        {
            TabController.SetActiveTab(i);
            presetActivatedTabItems[i].ItemBtn.onClick.Invoke();
        }
    }

    /// <summary>
    /// Add new tab and assign the tab name with the particular cosmetic component type
    /// </summary>
    /// <param name="cosmeticComp"></param>
    /// <returns></returns>
    private UITab PopulateNewTab(ICosmeticComponent cosmeticComp)
    {
        UITab tab = TabController.AddNewTab(TabGraphicPrefab);
        GameObject tabGraphicGO = tab.GetTabGraphic();

        tabGraphicGO.transform.GetChild(1).GetComponent<TMP_Text>().text = cosmeticComp.GetCosmeticType().GetTypeName();

        return tab;
    }

    /// <summary>
    /// Tab item's button's onClick callback which returns the particular item that was clicked by the user.
    /// Checks the selected item's state and perform the related operation. If purchasable, process the puchase
    /// </summary>
    /// <param name="cosmeticTabItem"></param>
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

        ServiceLocator.Current.Get<AudioController>(Service.AUDIO_CONTROLLER).PlayAudio(AudioClipType.COSMETIC_APPLY, CustomizerSpeaker);
    }

    /// <summary>
    /// Store state of each tab with information like currently active/ previously active item of each tab
    /// </summary>
    /// <param name="cosmeticTabItem"></param>
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

        tabData.ActiveCosmeticItem = cosmeticTabItem;
    }

    /// <summary>
    /// Handle the purchasing process of an comsetic item
    /// </summary>
    /// <param name="cosmeticTabItem"></param>
    /// <returns></returns>
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

            ServiceLocator.Current.Get<AudioController>(Service.AUDIO_CONTROLLER).PlayAudio(AudioClipType.COSMETIC_PURCHASE, ButtonSpeaker);
        }

        return isPurchaseSuccessfull;
    }

    /// <summary>
    /// Onlclick callback method of Tab buttons' which handles selection animations of the buttons
    /// </summary>
    /// <param name="oldTab"></param>
    /// <param name="newTab"></param>
    private void OnNewTabSelected(UITab oldTab, UITab newTab)
    {
        ServiceLocator.Current.Get<AudioController>(Service.AUDIO_CONTROLLER).PlayAudio(AudioClipType.BUTTON_CLICK_1, ButtonSpeaker);
        PlayNewTabSelectionAnimation(oldTab, newTab);
    }

    private void PlayCosmeticTabItemSelectionAnimation(UICosmeticTabItem cosmeticTabItem)
    {
        CosmeticUITabData tabData = GetTabData(TabController.GetActiveTab());

        if (tabData.PreviousCosmeticItem != null)
        {
            if (tabData.ActiveCosmeticItem != null)
            {
                //tabData.ActiveCosmeticItemAnim?.cancel(tabData.ActiveCosmeticItem.ItemBtn.gameObject);
                if (tabData.ActiveCosmeticItemAnim != null)
                {
                    LeanTween.cancel(tabData.ActiveCosmeticItemAnim.id);
                }
            }

            LeanTween.scale(tabData.PreviousCosmeticItem.ItemBtn.gameObject, Vector3.one, 0.25f).setEaseOutQuad();
        }

        if (tabData.ActiveCosmeticItem != null)
        {
            tabData.ActiveCosmeticItemAnim = LeanTween.scale(tabData.ActiveCosmeticItem.ItemBtn.gameObject, tabData.ActiveCosmeticItem.ItemBtn.transform.localScale * 1.2f, 0.4f).setLoopPingPong().setEaseInQuad();
        }
    }

    private void PlayNewTabSelectionAnimation(UITab oldTab, UITab newTab)
    {
        LeanTween.scale(oldTab.TabBtn.gameObject, /*oldTab.TabBtn.transform.localScale * 0.833f*/Vector3.one, 0.2f).setEaseInOutQuad();
        LeanTween.scale(newTab.TabBtn.gameObject, newTab.TabBtn.transform.localScale * 1.2f, 0.2f).setEaseInOutQuad();
    }

    /// <summary>
    /// Get the tab's UI related state data given the tab object. This method lazy loads the CosmeticUITabData when a request is made and if the data is not avaialble
    /// </summary>
    /// <param name="tab"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Loop through the saved character presets in user data to find which preset is currently active in the Character Customizer
    /// </summary>
    /// <param name="userData"></param>
    /// <param name="activePresetId"></param>
    /// <returns></returns>
    private CharacterPreset GetActiveCharacterPresetData(UserData userData, string activePresetId)
    {
        foreach (CharacterPreset preset in userData.MyCharacters)
        {
            if (preset.PresetId == activePresetId)
            {
                return preset;
            }
        }

        return null;
    }

    private void ToggleSpeakers(bool state)
    {
        CustomizerSpeaker.gameObject.SetActive(state);
        ButtonSpeaker.gameObject.SetActive(state);
    }
}
