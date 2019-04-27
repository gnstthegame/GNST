using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    [SerializeField] BuyButton[] buyButtons;
    [SerializeField] SellButton[] sellButtons;
    [SerializeField] Transform buttonsParent;

    public Shop shop;
    [SerializeField] Inventory inventory;
    [SerializeField] public CanvasGroup canvasGroup;
    [SerializeField] public ItemTooltip itemTooltip;

    private void OnValidate()
    {
        buyButtons = buttonsParent.GetComponentsInChildren<BuyButton>();
        sellButtons = buttonsParent.GetComponentsInChildren<SellButton>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Use this for initialization
    void Start () {
        Debug.Log(shop.sellItems.Count);
        Debug.Log(sellButtons.Length);

        Debug.Log(sellButtons[17].item);

        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (shop.sellItems[i] != null)
            {
                buyButtons[i].item = shop.sellItems[i];
            }      
        }

        for (int i = 0; i < sellButtons.Length; i++)
        {
            if(inventory.itemSlots[i].Item != null)
            {
                sellButtons[i].item = inventory.itemSlots[i].Item;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        int i = 0;
        for (; i < buyButtons.Length; i++)
        {
            if (shop.sellItems[i] != null)
            {
                buyButtons[i].item = shop.sellItems[i];
            }
        }

        i = 0;
        for (; i < sellButtons.Length; i++)
        {
            if (inventory.itemSlots[i].Item != null)
            {
                sellButtons[i].item = inventory.itemSlots[i].Item;
            }
        }
    }
}
