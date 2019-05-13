using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] public Image image;
    [SerializeField] public Item item;
    [SerializeField] Button button;
    [SerializeField] Inventory inventory;
    [SerializeField] Shop shop;
    [SerializeField] GameObject shopPanel;
    [SerializeField] ItemTooltip shopTooltip;
    public int stackSize;
    [SerializeField] public Text stackText;

    public event Action<BuyButton> OnPointerEnterEvent;
    public event Action<BuyButton> OnPointerExitEvent;
    
    
    private void Awake() {
        shop = FindObjectOfType<Shop>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
        stackText = GetComponentInChildren<Text>();
        inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (item == null)
        {
            image.color = new Color(0, 0, 0, 0);
            stackText.enabled = false;
        }
        else
        {
            image.color = Color.white;
            image.sprite = item.Sprite;
            if(stackSize > 1)
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
    //nie stackuje
    public void Buy()
    {
        if (inventory.IsFull())
        {
            return;
        }
        if (item != null && item.BuyValue <= inventory.money)
        {
            if (inventory.IsFull())
            {
                return;
            }
            if (item != null)
            {
                inventory.money -= item.BuyValue;
                for (int i = 0; i < shop.sellItems.Count; i++)
                {
                    if (shop.sellItems[i].item == item)
                    {
                        if (item != null && item.MaximumStacks > 1)
                        {
                            stackSize--;
                            shop.sellItems[i].amount--;
                            Debug.Log("A: " + shop.sellItems[i].amount);
                            for (int j = 0; j < inventory.itemSlots.Length; j++)
                            {
                                if (inventory.itemSlots[j].Item == null)
                                {
                                    inventory.itemSlots[j].Item = item;
                                    inventory.itemSlots[j].Amount = 1;
                                    break;
                                }
                            }
                            if (stackSize <= 0)
                            {
                                shop.sellItems[i] = new Pair();
                                item = null;
                            }
                            return;
                        }
                        else if (item != null && item.MaximumStacks == 1)
                        {
                            shop.sellItems[i].amount--;
                            stackSize--;
                            for (int j = 0; j < inventory.itemSlots.Length; j++)
                            {
                                if (inventory.itemSlots[j].Item == null)
                                {
                                    inventory.itemSlots[j].Item = item;
                                    inventory.itemSlots[j].Amount = 1;
                                    break;
                                }
                            }
                            if (stackSize == 0)
                            {
                                shop.sellItems[i] = new Pair();
                                item = null;
                            }
                            return;
                        }
                    }
                }

            }
        }       
        Debug.Log("Zakupiono");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }
}
