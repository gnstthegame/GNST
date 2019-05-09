using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour, IItemContainter {
    //przedmioty startowe
    [SerializeField] Item[] startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] public ItemSlot[] itemSlots;
    public Text moneyText;
    public int money;

=======
using System;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour {
    [SerializeField]
    List<Item> startingItems;
    [SerializeField]
    Transform itemsParent;
    [SerializeField]
    public ItemSlot[] itemSlots;
    public int money;


>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;
<<<<<<< HEAD

    private void Start()
    {
        //dodanie obslugi zdarzenia do kazdego slotu
        for (int i = 0; i < itemSlots.Length; i++)
=======
    

    private void Start()
    {
        for(int i = 0; i < itemSlots.Length; i++)
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
<<<<<<< HEAD
        SetStartingItems();
    }

    private void Update()
    {
        moneyText.text = "Plusz: " + money.ToString();
    }
=======

        SetStartingItems();
    }



>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d

    private void OnValidate()
    {
        if(itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }
<<<<<<< HEAD
=======

>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
        SetStartingItems();
    }

    private void SetStartingItems()
    {
        int i = 0;
<<<<<<< HEAD
        for(; i < startingItems.Length && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i].GetCopy();
            itemSlots[i].Amount = 1;
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
=======
        for(; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = Instantiate(startingItems[i]);
        }

        for(; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
        }
    }

    public bool AddItem(Item item)
    {
<<<<<<< HEAD
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null || itemSlots[i].CanAddStack(itemSlots[i].Item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
=======
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
                return true;
            }
        }
        return false;
    }

<<<<<<< HEAD
=======
    //to chyba przez to sie dubluje
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
<<<<<<< HEAD
                itemSlots[i].Amount--;
                if (itemSlots[i].Amount == 0)
                {
                    itemSlots[i].Item = null;
                }
=======
                itemSlots[i].Item = null;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
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
<<<<<<< HEAD

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
=======
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
}
