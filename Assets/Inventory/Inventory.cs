using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Inventory : MonoBehaviour, IItemContainter {
<<<<<<< Updated upstream
    //przedmioty startowe
    [SerializeField] Item[] startingItems;
    //przedmioty startowe(domyślnie znajdą się w ekwipunku)
    [SerializeField] public Item[] startingItems;
    //obiekt przechowujący wszystkie sloty ekwipunku
    [SerializeField] Transform itemsParent;
    //tablica pól panelu ekwipunku
    [SerializeField] public ItemSlot[] itemSlots;
    //obiekt tekstu wyświetlający aktualną ilość pieniędzy
    public Text moneyText;
    //aktualna ilość pieniędzy
    public int money;

    //zdarzenie obsługujące wyświetlanie informacji o przedmiocie po najechaniu kursorem
    public event Action<ItemSlot> OnPointerEnterEvent;
    //zdarzenie chowające panel z informacjami po przesunięciu kursora znad obiektu
    public event Action<ItemSlot> OnPointerExitEvent;
    //zdarzenie obsługujące kliknięcie na obiekt
    public event Action<ItemSlot> OnRightClickEvent;
    //zdarzenie obsługujące początek przeciągania w drag and drop w panelu ekwipunku
    public event Action<ItemSlot> OnBeginDragEvent;
    //zdarzenie obsługujące zakończenie procesu przeciągania obiektu
    public event Action<ItemSlot> OnEndDragEvent;
    //zdarzenie obsługujące sam proces pomiędzy rozpoczęciem, a zakończeniem procesu przeciągania
    public event Action<ItemSlot> OnDragEvent;
    //zdarzenie obsługujące koniec procesu drag and drop
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
        //dodanie przedmiotow z listy startowej do ekwipunku
        SetStartingItems();
    }

    /// <summary>
    /// Odświeżanie ilości pieniędzy w panelu
    /// </summary>
    private void Update()
    {
        moneyText.text = "Plusz: " + money.ToString();
    }
    

    /// <summary>
    /// Wyszukanie obiektu zawierającego sloty ekwipunku w hierarchii projektu, gdy go nie ustawiono.
    /// Ustawienie przedmiotów startowych na tak odnalezionym obiekcie.
    /// </summary>
    private void Awake()
    {
        if(itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }
    }
    
    /// <summary>
    /// Metoda dodająca do ekwipunku przedmioty z listy startowej, oraz ich ilość.
    /// </summary>
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

    
    /// <summary>
    /// Metoda dodająca przedmiot do ekwipunku
    /// </summary>
    /// <param name="item">Przedmiot do dodania</param>
    /// <returns>Prawda, gdy udało się dodać, fałsz w przeciwnym wypadku.</returns>
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

    /// <summary>
    /// Metoda dodająca konkretną ilość przedmiotu do ekwipunku
    /// </summary>
    /// <param name="item">Przedmiot do dodania</param>
    /// <param name="stack">Ilość</param>
    /// <returns>Prawda, gdy udano się dodać, fałsz w przeciwnym wypadku.</returns>
    public bool AddItem(Item item, int stack) {
        for (int j = 0; j < stack; j++) {
            if (AddItem(item) == false) {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Metoda usuwająca przedmiot z ekwipunku
    /// </summary>
    /// <param name="itemID">Identyfikator przedmiotu.</param>
    /// <returns>Usuwany przedmiot, gdy udało się usunąć, NULL w przeciwnym wypadku.</returns>
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

    /// <summary>
    /// Metoda sprawdzająca, czy wszystkie sloty w ekwipunku zawierają przedmiot.
    /// </summary>
    /// <returns>Prawda, gdy wszystkie sloty zawierają przedmiot, fałsz gdy którykolwiek z nich jest pusty.</returns>
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

    /// <summary>
    /// Metoda zwracająca informację o ilości wystąpień danego przedmiotu w ekwipunku.
    /// </summary>
    /// <param name="itemID">Identyfikator przedmiotu</param>
    /// <returns>Ilość wystąpień przedmiotu w ekwipunku</returns>
    public int ItemCount(string itemID)
    {
        int amount = 0;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item.ID == itemID)
            {
                amount += itemSlots[i].Amount;
            }
        }
        return amount;
    }
}
