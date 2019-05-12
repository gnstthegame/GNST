using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    [SerializeField] public BuyButton[] buyButtons;
    [SerializeField] SellButton[] sellButtons;
    [SerializeField] Transform buttonsParent;

    public Shop shop;
    [SerializeField] Inventory inventory;
    [SerializeField] public CanvasGroup canvasGroup;
    [SerializeField] public ItemTooltip itemTooltip;

    public event Action<BuyButton> OnBuyPointerEnterEvent;
    public event Action<BuyButton> OnBuyPointerExitEvent;
    public event Action<SellButton> OnSellPointerEnterEvent;
    public event Action<SellButton> OnSellPointerExitEvent;

    private void OnValidate()
    {
        buyButtons = buttonsParent.GetComponentsInChildren<BuyButton>();
        sellButtons = buttonsParent.GetComponentsInChildren<SellButton>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Use this for initialization
    void Start () {
        shop = FindObjectOfType<Shop>();
        if (shop == null) {
            return;
        }
        for (int i = 0; i < shop.sellItems.Count; i++)
        {
            if(shop.sellItems[i] != null)
                shop.sellItems[i] = shop.sellItems[i];
        }

        for (int i = 0; i < buyButtons.Length - 1; i++)
        {
            buyButtons[i].OnPointerEnterEvent += ShowTooltip;
            buyButtons[i].OnPointerExitEvent += HideTooltip;
            
            if (shop.sellItems[i] != null)
            {
                buyButtons[i].item = shop.sellItems[i];
                buyButtons[i].stackSize = 1;
                //Debug.Log(buyButtons[i].item.ID);
            }
            else
            {
                buyButtons[i].item = null;
                buyButtons[i].stackSize = 0;
            }
        }

        for (int i = 0; i < sellButtons.Length - 1; i++)
        {
            sellButtons[i].OnPointerEnterEvent += ShowTooltip;
            sellButtons[i].OnPointerExitEvent += HideTooltip;
            if(inventory.itemSlots[i].Item != null)
            {
                sellButtons[i].item = inventory.itemSlots[i].Item;
                sellButtons[i].stackSize = inventory.itemSlots[i].Amount;
            }
            else
            {
                sellButtons[i].item = null;
            }
        }
        
    }

    public void Show() {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void Hide() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void ToggleShow() {
        GetComponent<CanvasGroup>().alpha = 1- GetComponent<CanvasGroup>().alpha;
        GetComponent<CanvasGroup>().blocksRaycasts = !GetComponent<CanvasGroup>().blocksRaycasts;
    }
    // Update is called once per frame
    void Update () {
        if (shop == null) {
            return;
        }
        int i = 0;
        for (i = 0; i < buyButtons.Length - 1; i++)
        {
            if (shop.sellItems[i] != null)
            {
                buyButtons[i].item = shop.sellItems[i];
                buyButtons[i].stackSize = 1;
            }
            else
            {
                buyButtons[i].item = null;
                buyButtons[i].stackSize = 0;
            }
        }


        for (i = 0; i < sellButtons.Length - 1; i++)
        {
            if (inventory.itemSlots[i].Item != null)
            {
                sellButtons[i].item = inventory.itemSlots[i].Item;
                sellButtons[i].stackSize = inventory.itemSlots[i].Amount;
            }
            else
            {
                sellButtons[i].item = null;
            }
        }
    }

    private void ShowTooltip(BuyButton buyButton)
    {
        if (buyButton.item != null && buyButton.item is EquippableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((EquippableItem)buyButton.item);
        }
        else if (buyButton.item != null && buyButton.item is UsableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((UsableItem)buyButton.item);
        }
    }

    private void HideTooltip(BuyButton buyButton)
    {
        itemTooltip.HideTooltip();
    }

    private void ShowTooltip(SellButton sellButton)
    {
        if (sellButton.item != null && sellButton.item is EquippableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((EquippableItem)sellButton.item);
        }
        else if (sellButton.item != null && sellButton.item is UsableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((UsableItem)sellButton.item);
        }
    }

    private void HideTooltip(SellButton sellButton)
    {
        itemTooltip.HideTooltip();
    }
}
