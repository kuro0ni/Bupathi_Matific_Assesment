using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICosmeticTabItem : MonoBehaviour
{
    [SerializeField]
    public CosmeticItem CosmeticItemData;

    [Header("UI Elements")]
    public Button ItemBtn;

    public Image ItemIcon;

    public Image PriceLabel;
    public TMP_Text PriceText;

    public Image LevelLabel;
    public TMP_Text LevelText;

    public Image LevelLockGraphic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateItem(CosmeticItem itemData, CosmeticItem_SO itemSO, UserData userData, UnityEvent<UserData> refreshItemEvent)
    {
        CosmeticItemData = itemData;

        ItemBtn.onClick.RemoveAllListeners();

        ItemIcon.sprite = itemSO.Icon;

        itemData.SetItemStateByUserData(userData);

        SetGraphicsByState(itemData, userData);

        PriceText.text = itemData.Price.ToString();
        LevelText.text = $"Lvl.{itemData.MinLevel}";

        refreshItemEvent.AddListener(RefreshItem);
    }

    private void SetGraphicsByState(CosmeticItem itemData, UserData userData)
    {
        switch (itemData.State)
        {
            case CosmeticItemState.AVAILABLE:
                PriceLabel.gameObject.SetActive(false);
                LevelLabel.gameObject.SetActive(false);
                LevelLockGraphic.gameObject.SetActive(false);

                ItemBtn.interactable = true;
                break;

            case CosmeticItemState.PURCHASABLE:
                PriceLabel.gameObject.SetActive(true);
                LevelLabel.gameObject.SetActive(false);
                LevelLockGraphic.gameObject.SetActive(false);

                ItemBtn.interactable = true;

                if (userData.Coins < itemData.Price)
                {
                    ItemBtn.interactable = false;
                }

                break;

            case CosmeticItemState.LOCKED:
                PriceLabel.gameObject.SetActive(false);
                LevelLabel.gameObject.SetActive(true);
                LevelLockGraphic.gameObject.SetActive(true);

                ItemBtn.interactable = false;
                break;

            default:
                break;
        }
    }

    public void RefreshItem(UserData data)
    {
        SetGraphicsByState(CosmeticItemData, data);
    }
}
