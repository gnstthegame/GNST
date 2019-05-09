using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    [SerializeField] BuyButton[] buyButtons;
    [SerializeField] SellButton[] sellButtons;
    [SerializeField] Transform buttonsParent;
    [SerializeField] Inventory inventory;
    public List<Item> itemsToSell;
    public Text money;
    public Sprite empty;

    private void OnValidate()
    {
        buyButtons = buttonsParent.GetComponentsInChildren<BuyButton>();
        sellButtons = buttonsParent.GetComponentsInChildren<SellButton>();
        
    }
    // Use this for initialization
    void Start () {
        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (itemsToSell[i] != null)
            {
                buyButtons[i].image.sprite = itemsToSell[i].Icon;
                buyButtons[i].item = itemsToSell[i];
            }
            else if (itemsToSell[i] == null)
            {
                buyButtons[i].GetComponent<Image>().sprite = empty;
                buyButtons[i].item = null;
            }
        }
        money.text = "Plusz: " + inventory.money.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        ShopUpdate();
    }

    private void ShopUpdate()
    {
        for (int i = 0; i < inventory.itemSlots.Length; i++)
        {
            if (inventory.itemSlots[i].Item != null)
            {
                sellButtons[i].image.sprite = inventory.itemSlots[i].Item.Icon;
                sellButtons[i].item = inventory.itemSlots[i].Item;
            }
            else if (inventory.itemSlots[i].Item == null)
            {
                sellButtons[i].image.sprite = empty;
                sellButtons[i].item = null;
            }
            if (i == inventory.itemSlots.Length)
            {
                i = 0;
            }
        }
        //przy zalozeniu ze ilosc buyButtons jest rowna rozmiarowi listy itemsToSell ale tak jest
        for (int i = 0; i < itemsToSell.Count; i++)
        {
            if (itemsToSell[i] != null)
            {
                buyButtons[i].image.sprite = itemsToSell[i].Icon;
                buyButtons[i].item = itemsToSell[i];                
            }
            else if (itemsToSell[i] == null)
            {
                buyButtons[i].GetComponent<Image>().sprite = empty;
                buyButtons[i].item = null;
            }
            if(i == buyButtons.Length)
            {
                i = 0;
            }
        }
        money.text = "Plusz: " + inventory.money.ToString();
    }
}
