using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour {
    public Image image;
    public Item item;
    //public Sprite empty;
    public Inventory inventory;
    Button button;
    public ShopManager shopManager;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Sell);
        shopManager = FindObjectOfType<ShopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (item == null)
        //{
        //    image.sprite = empty;
        //}
        //else
        //{
        //    image.sprite = item.Icon;
        //}
    }
    //do poprawy
    public void Sell()
    {
        if(item != null)
        {
            inventory.money += item.SellValue;
            inventory.RemoveItem(item.ID);
            int i = 0;
            while(i < shopManager.itemsToSell.Count)
            {
                if(shopManager.itemsToSell[i] == null)
                {
                    shopManager.itemsToSell[i] = item;
                    break;
                }
                i++;
            }
            item = null;
        }
    }
}
