using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Klasa obsługująca zakup przedmiotu u handlarza
/// </summary>
public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //obrazek przedmiotu
    [SerializeField] public Image image;
    //sprzedawany przedmiot
    [SerializeField] public Item item;
    //przycisk z metodą sprzedaży
    [SerializeField] Button button;
    //ekwipunek
    [SerializeField] Inventory inventory;
    //obiekt sklepu
    [SerializeField] Shop shop;
    //panel sklepu
    [SerializeField] GameObject shopPanel;
    //panel z informacjami o przedmiocie
    [SerializeField] ItemTooltip shopTooltip;
    //ilość przedmiotu w pojedynczym slocie
    public int stackSize;
    //obiekt wyświetlający ilość przedmiotu
    [SerializeField] public Text stackText;

    //zdarzenie wywoływane po najechaniu kursorem myszki na obiekt
    public event Action<BuyButton> OnPointerEnterEvent;
    //zdarzenie wywoływane po opuszczeniu obiektu przez kursor
    public event Action<BuyButton> OnPointerExitEvent;

    /// <summary>
    /// Metoda odnajdująca w hierarchii projektu podstawowe komponenty
    /// </summary>
    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
        stackText = GetComponentInChildren<Text>();
        inventory = FindObjectOfType<Inventory>();
    }

    /// <summary>
    /// Metoda uaktualniająca UI
    /// </summary>
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
    /// <summary>
    /// Metoda wywoływana po kliknięciu, jeżeli jest taka możliwość, następuje zakup przedmiotu
    /// </summary>
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

    /// <summary>
    /// Zdarzenie obsługujące najechanie kursorem na obiekt
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }

    /// <summary>
    /// Zdarzenie obsługujące po opuszczeniu kursora z obiektu
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }
}
