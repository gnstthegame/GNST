using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] public Image image;
    [SerializeField] public Item item;
    [SerializeField] Button button;
    [SerializeField] Inventory inventory;
    [SerializeField] Shop shop;
    [SerializeField] ItemTooltip shopTooltip;
    [SerializeField] GameObject shopPanel;
    [SerializeField] public Text stackText;
    [SerializeField] BuyButton[] buyButtons;
    [SerializeField] ShopManager shopManager;
    public int stackSize;

    public event Action<SellButton> OnPointerEnterEvent;
    public event Action<SellButton> OnPointerExitEvent;

    private void OnValidate() {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Sell);
        stackText = GetComponentInChildren<Text>();
        inventory = FindObjectOfType<Inventory>();
    }
    private void Awake() {
        shop = FindObjectOfType<Shop>();
    }
    private void Update()
    {
        if(item == null)
        {
            image.color = new Color(0, 0, 0, 0);
            stackText.enabled = false;
        }
        else
        {
            image.color = Color.white;
            image.sprite = item.Sprite;
            if (stackSize > 1)
            {
                stackText.enabled = true;
                stackText.text = stackSize.ToString();
            }
            else
            {
                stackText.enabled = false;
            }
        }
    }
    //cos nie tak ze stack size
    public void Sell()
    {
        if(item != null)
        {
            inventory.RemoveItem(item.ID);
            inventory.money += item.SellValue;

            for (int i = 0; i < buyButtons.Length; i++)
            {
                if(buyButtons[i].item == item)
                {
                    stackSize--;
                    if (stackSize == 0)
                        item = null;
                    buyButtons[i].stackSize++;
                    return;
                }
            }

            for (int i = 0; i < shop.sellItems.Count; i++)
            {
                if (shop.sellItems[i] == null)
                {
                    stackSize--;
                    shop.sellItems[i] = item;
                    if (stackSize == 0)
                        item = null;
                    return;
                }
            }
        }
        Debug.Log("Sprzedano");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }
}
