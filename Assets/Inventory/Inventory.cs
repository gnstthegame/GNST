using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Inventory : MonoBehaviour, IItemContainter {
    //przedmioty startowe
    [SerializeField] public Item[] startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] public ItemSlot[] itemSlots;
    public Text moneyText;
    public int money;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        //dodanie obslugi zdarzenia do kazdego slotu
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
        SetStartingItems();
    }

    private void Update()
    {
        moneyText.text = "Plusz: " + money.ToString();
    }

    private void Awake()
    {
        if(itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }
    }

    public void SetStartingItems()
    {
        foreach(Item i in startingItems) {
            AddItem(i);
        }
        //int i = 0;
        //for(; i < startingItems.Length && i < itemSlots.Length; i++)
        //{
        //    itemSlots[i].Item = startingItems[i].GetCopy();
        //    itemSlots[i].Amount = 1;
        //}

        //for (; i < itemSlots.Length; i++)
        //{
        //    itemSlots[i].Item = null;
        //    itemSlots[i].Amount = 0;
        //}
    }

    public bool AddItem(Item item) {
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == null || itemSlots[i].CanAddStack(item)) {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }
    public bool AddItem(Item item, int stack) {
        for (int j = 0; j < stack; j++) {
            if (AddItem(item) == false) {
                return false;
            }
        }
        return true;
    }

    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                itemSlots[i].Amount--;
                if (itemSlots[i].Amount == 0)
                {
                    itemSlots[i].Item = null;
                }
                return item;
            }
        }
        return null;
    }

    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Amount--;
                if(itemSlots[i].Amount == 0)
                {
                    itemSlots[i].Item = null;
                }            
                return true;
            }
        }
        return false;
    }

    public int ItemCount(string itemID)
    {
        int amount = 0;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item.ID == itemID)
            {
                amount++;
            }
        }
        return amount;
    }
}
