using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LootSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public int amount;
    [SerializeField] Image image;
    [SerializeField] Inventory inventory;
    [SerializeField] Text stackText;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] LootPanel lootPanel;
    public event Action<LootSlot> OnPointerEnterEvent;
    public event Action<LootSlot> OnPointerExitEvent;

    private void OnValidate()
    {
        inventory = FindObjectOfType<Inventory>();
        stackText = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        if (item == null)
        {
            image.color = new Color(0, 0, 0, 0);
            stackText.enabled = false;
        }
        else
        {
            image.color = Color.white;
            image.sprite = item.Sprite;
            if (amount > 1)
            {
                stackText.enabled = true;
                stackText.text = amount.ToString();
            }
            else
            {
                stackText.enabled = false;
            }
        }
    }

    //Poprawic dla niestackujacych sie itemow, wytyczne.txt
    //Dodac podobne do BuyButton
    public void GetItem()
    {
        if (inventory.IsFull())
        {
            return;
        }
        if (item != null)
        {
            for (int i = 0; i < lootPanel.chest.lootItems.Count; i++)
            {
                if (lootPanel.chest.lootItems[i].item == item)
                {
                    if(item != null && item.MaximumStacks > 1)
                    {
                        amount--;
                        lootPanel.chest.lootItems[i].amount--;
                        Debug.Log("A: " + amount);
                        for (int j = 0; j < inventory.itemSlots.Length; j++)
                        {
                            if (inventory.itemSlots[j].Item == null)
                            {
                                inventory.itemSlots[j].Item = item;
                                inventory.itemSlots[j].Amount = 1;
                                break;
                            }
                        }
                        if (amount == 0)
                        {
                            lootPanel.chest.lootItems[i] = new Pair();
                            item = null;
                        }
                        break;
                    }
                    else if(item != null && item.MaximumStacks == 1)
                    {
                        amount--;
                        lootPanel.chest.lootItems[i].amount--;                        
                        for (int j = 0; j < inventory.itemSlots.Length; j++)
                        {
                            if (inventory.itemSlots[j].Item == null)
                            {
                                inventory.itemSlots[j].Item = item;
                                inventory.itemSlots[j].Amount = 1;
                                break;
                            }
                        }
                        if (amount == 0)
                        {
                            lootPanel.chest.lootItems[i] = new Pair();
                            item = null;
                        }
                    }
                }              
            }
        }
    }

    public void TakeAllFromSlot()
    {
        int pom = amount;
        for (int i = 0; i < pom; i++)
        {
            GetItem();            
        }
        if(amount == 0)
            item = null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }
}
