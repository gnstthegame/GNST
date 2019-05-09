using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour {
    public Image image;
    public Item item;
    //public Sprite empty;
    public Inventory inventory;
    Button button;
    public ShopManager shopManager;

    // Use this for initialization
    void Start() {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
        shopManager = FindObjectOfType<ShopManager>();
    }

    // Update is called once per frame
    void Update() {
        //if (item == null)
        //{
        //    image.sprite = empty;
        //}
        //else
        //{
        //    image.sprite = item.Icon;
        //}
    }

    public void Buy()
    {
        if(inventory.IsFull() || item == null || inventory.money <= item.BuyValue)
        {
            Debug.Log("Nie można kupić");
            return;
        }
        else
        {
            inventory.AddItem(item);
            inventory.money -= item.BuyValue;
            shopManager.itemsToSell.Remove(item);
            item = null;
        }
        
        
    }
}
