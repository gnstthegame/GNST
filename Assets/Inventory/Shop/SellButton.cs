using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Klasa obsługująca sprzedaż przedmiotów u handlarza
/// </summary>
public class SellButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //obrazek przedmiotu
    [SerializeField] public Image image;
    //sprzedawany przedmiot
    [SerializeField] public Item item;
    //przycisk z metodą kupna
    [SerializeField] Button button;
    //ekwipunek
    [SerializeField] Inventory inventory;
    //obiekt sklepu
    [SerializeField] Shop shop;
    //panel z informacjami o przedmiocie
    [SerializeField] ItemTooltip shopTooltip;
    //panel sklepu
    [SerializeField] GameObject shopPanel;
    //obiekt wyświetlający ilość przedmiotu
    [SerializeField] public Text stackText;
    [SerializeField] BuyButton[] buyButtons;
    //obiekt zarządzający sklepem
    [SerializeField] ShopManager shopManager;
    //ilość przedmiotu w pojedynczym slocie
    public int stackSize;

    //zdarzenie wywoływane po najechaniu kursorem myszki na obiekt
    public event Action<SellButton> OnPointerEnterEvent;
    //zdarzenie wywoływane po opuszczeniu obiektu przez kursor
    public event Action<SellButton> OnPointerExitEvent;


    /// <summary>
    /// Metoda odnajdująca w hierarchii projektu podstawowe komponenty
    /// </summary>
    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Sell);
        stackText = GetComponentInChildren<Text>();
        inventory = FindObjectOfType<Inventory>();
        buyButtons = FindObjectsOfType<BuyButton>();
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
    /// Metoda wywoływana po kliknięciu, po jej wywołaniu następuje sprzedaż przedmiotu
    /// </summary>
    public void Sell()
    {
        if (item != null)
        {
            //inventory.RemoveItem(item.ID);

            for (int i = 0; i < shop.sellItems.Count; i++)
            {
                if (inventory.itemSlots[i].Item == item && inventory.itemSlots[i].Amount < item.MaximumStacks)
                {
                    inventory.itemSlots[i].Amount--;
                    stackSize--;
                    inventory.money += item.SellValue;
                    for (int j = 0; j < shop.sellItems.Count; j++)
                    {
                        if (shop.sellItems[j].item == item)
                        {
                            shop.sellItems[j].amount++;
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
                else if (inventory.itemSlots[i].Item == item && inventory.itemSlots[i].Amount == item.MaximumStacks)
                {
                    inventory.RemoveItem(item);
                    stackSize--;
                    inventory.money += item.SellValue;
                    for (int j = 0; j < shop.sellItems.Count; j++)
                    {
                        if (shop.sellItems[j].item == null)
                        {
                            shop.sellItems[i].item = item;
                            shop.sellItems[j].amount = 1;
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
            }
        }
        Debug.Log("Sprzedano");
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
