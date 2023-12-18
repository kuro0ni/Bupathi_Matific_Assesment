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
    private CosmeticItem CosmeticItemData;

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

    public void PopulateItem(CosmeticItem itemData, CosmeticItem_SO itemSO, UnityAction OnItemSelected)
    {
        ItemBtn.onClick.AddListener(OnItemSelected);

        ItemIcon.sprite = itemSO.Icon;
        
        switch (itemData.State)
        {
            case CosmeticItemState.AVAILABLE:
                PriceLabel.gameObject.SetActive(false);
                LevelLabel.gameObject.SetActive(false);

                ItemBtn.interactable = true;
                break;
            case CosmeticItemState.PURCHASABLE:
                PriceLabel.gameObject.SetActive(true);
                LevelLabel.gameObject.SetActive(false);

                break;
            case CosmeticItemState.LOCKED:
                PriceLabel.gameObject.SetActive(false);
                LevelLabel.gameObject.SetActive(true);

                ItemBtn.interactable = false;
                break;
            default:
                break;
        }
    }


}
