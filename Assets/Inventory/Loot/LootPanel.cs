using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Pair
{
    public Item item;
    public int amount;

    public Pair()
    {
        item = null;
        amount = 0;
    }

    public Pair(Item it, int am)
    {
        item = it;
        amount = am;
    }
}

public class LootPanel : MonoBehaviour {

    [SerializeField] Inventory inventory;
    LootSlot[] lootSlots;
    [SerializeField] GameObject lootSlotsParent;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] public Chest chest;

    public event Action<LootSlot> OnPointerEnterEvent;
    public event Action<LootSlot> OnPointerExitEvent;

    private void Awake()
    {
        lootSlots = lootSlotsParent.GetComponentsInChildren<LootSlot>();
    }

    public void Show()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < lootSlots.Length; i++) {
            lootSlots[i].OnPointerEnterEvent += ShowTooltip;
            lootSlots[i].OnPointerExitEvent += HideTooltip;
        }
        if (chest != null) {
            for (int i = 0; i < chest.lootItems.Count; i++) {
                if (chest.lootItems[i].item != null) {
                    lootSlots[i].item = chest.lootItems[i].item;
                    lootSlots[i].amount = chest.lootItems[i].amount;
                } else {
                    lootSlots[i].item = null;
                    lootSlots[i].amount = 0;
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(chest != null)
        {
            bool empty = true;
            for (int i = 0; i < chest.lootItems.Count; i++)
            {
                if (chest.lootItems[i].item != null)
                {
                    empty = false;
                    lootSlots[i].item = chest.lootItems[i].item;
                    lootSlots[i].amount = chest.lootItems[i].amount;
                }
                else
                {
                    lootSlots[i].item = null;
                    lootSlots[i].amount = 0;
                }
            }
            if (empty) {
                chest.Destr();
            }
        }
        
	}

    public void TakeAllItems()
    {
        foreach(LootSlot slot in lootSlots)
        {
            if(slot.amount == 1)
                slot.GetItem();
            else if(slot.amount > 1)
            {
                slot.TakeAllFromSlot();
            }
                
        }
    }

    private void ShowTooltip(LootSlot lootSlot)
    {
        if (lootSlot.item != null && lootSlot.item is EquippableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((EquippableItem)lootSlot.item);
        }
        else if (lootSlot.item != null && lootSlot.item is UsableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((UsableItem)lootSlot.item);
        }
    }

    private void HideTooltip(LootSlot buyButton)
    {
        itemTooltip.HideTooltip();
    }
}
