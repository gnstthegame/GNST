using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Klasa zarządzająca interfejsem sklepu
/// </summary>
public class ShopManager : MonoBehaviour
{
    //przyciski odpowiadające za kupno
    [SerializeField] public BuyButton[] buyButtons;
    //przyciski odpowiadające za sprzedaż
    [SerializeField] SellButton[] sellButtons;
    //obiekt pod którym przyciski znajdują się w hierarchii
    [SerializeField] Transform buttonsParent;

    //handlarz, z którym aktualnie wchodzimy w interakcję
    public Shop shop;
    //ekwipunek postaci
    [SerializeField] Inventory inventory;
    //obiekt pozwalający na wyświetlanie panelu
    [SerializeField] public CanvasGroup canvasGroup;
    //panel z informacjami o przedmiocie
    [SerializeField] public ItemTooltip itemTooltip;

    //zdarzenie gdy kursor myszy znajdzie się na przycisku zakupu
    public event Action<BuyButton> OnBuyPointerEnterEvent;
    //zdarzenie gdy kursor myszy opuści pole przycisku zakupu
    public event Action<BuyButton> OnBuyPointerExitEvent;
    //zdarzenie gdy kursor myszy znajdzie się na przycisku sprzedaży
    public event Action<SellButton> OnSellPointerEnterEvent;
    //zdarzenie gdy kursor myszy opuści pole przycisku sprzedaży
    public event Action<SellButton> OnSellPointerExitEvent;

    /// <summary>
    /// Metoda odnajdująca obiekty w hierarchii projektu
    /// </summary>
    private void Awake()
    {
        buyButtons = buttonsParent.GetComponentsInChildren<BuyButton>();
        sellButtons = buttonsParent.GetComponentsInChildren<SellButton>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    /// <summary>
    /// Metoda przypisuje zdarzenia do każdego przycisku, oraz ustawia UI okna handlu
    /// </summary>
    void Start()
    {
        shop = FindObjectOfType<Shop>();
        if (shop == null)
        {
            return;
        }

        for (int i = 0; i < buyButtons.Length; i++)
        {
            buyButtons[i].OnPointerEnterEvent += ShowTooltip;
            buyButtons[i].OnPointerExitEvent += HideTooltip;

            if (shop.sellItems[i] != null)
            {
                buyButtons[i].item = shop.sellItems[i].item;
                buyButtons[i].stackSize = shop.sellItems[i].amount;
                //Debug.Log(buyButtons[i].item.ID);
            }
            else
            {
                buyButtons[i].item = null;
                buyButtons[i].stackSize = 0;
            }
        }

        for (int i = 0; i < sellButtons.Length; i++)
        {
            sellButtons[i].OnPointerEnterEvent += ShowTooltip;
            sellButtons[i].OnPointerExitEvent += HideTooltip;
            if (inventory.itemSlots[i].Item != null)
            {
                sellButtons[i].item = inventory.itemSlots[i].Item;
                sellButtons[i].stackSize = inventory.itemSlots[i].Amount;
            }
            else
            {
                sellButtons[i].item = null;
                sellButtons[i].stackSize = 0;
            }
        }

    }
    /// <summary>
    /// Metoda pokazuje panel sklepu
    /// </summary>
    public void Show()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    /// <summary>
    /// Metoda ukrywa panel sklepu
    /// </summary>
    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void ToggleShow()
    {
        GetComponent<CanvasGroup>().alpha = 1 - GetComponent<CanvasGroup>().alpha;
        GetComponent<CanvasGroup>().blocksRaycasts = !GetComponent<CanvasGroup>().blocksRaycasts;
    }

    /// <summary>
    /// Metoda uaktualniająca UI
    /// </summary>
    void Update()
    {
        if (shop == null)
        {
            return;
        }
        int i = 0;
        for (i = 0; i < buyButtons.Length; i++)
        {
            if (shop.sellItems[i] != null)
            {
                buyButtons[i].item = shop.sellItems[i].item;
                buyButtons[i].stackSize = shop.sellItems[i].amount;
            }
            else
            {
                buyButtons[i].item = null;
                buyButtons[i].stackSize = 0;
            }
        }


        for (i = 0; i < sellButtons.Length; i++)
        {
            if (inventory.itemSlots[i].Item != null)
            {
                sellButtons[i].item = inventory.itemSlots[i].Item;
                sellButtons[i].stackSize = inventory.itemSlots[i].Amount;
            }
            else
            {
                sellButtons[i].item = null;
                sellButtons[i].stackSize = 0;
            }
        }
    }

    /// <summary>
    /// Metoda pokazująca panel z informacjami o przedmiocie do zakupu
    /// </summary>
    /// <param name="buyButton">Slot z przedmiotem do zakupu</param>
    private void ShowTooltip(BuyButton buyButton)
    {
        if (buyButton.item != null && buyButton.item is EquippableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((EquippableItem)buyButton.item);
        }
        else if (buyButton.item != null && buyButton.item is UsableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((UsableItem)buyButton.item);
        }
    }

    /// <summary>
    /// Metoda ukrywająca panel z informacjami o przedmiocie do zakupu
    /// </summary>
    /// <param name="buyButton">Slot z przedmiotem</param>
    private void HideTooltip(BuyButton buyButton)
    {
        itemTooltip.HideTooltip();
    }

    /// <summary>
    /// Metoda pokazująca panel z informacjami o przedmiocie do sprzedaży
    /// </summary>
    /// <param name="sellButton">Slot z przedmiotem do sprzedaży</param>
    private void ShowTooltip(SellButton sellButton)
    {
        if (sellButton.item != null && sellButton.item is EquippableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((EquippableItem)sellButton.item);
        }
        else if (sellButton.item != null && sellButton.item is UsableItem && GetComponent<CanvasGroup>().alpha == 1)
        {
            itemTooltip.ShowTooltip((UsableItem)sellButton.item);
        }
    }

    /// <summary>
    /// Metoda ukrywająca panel z informacjami o przedmiocie do sprzedaży
    /// </summary>
    /// <param name="sellButton">Slot z przedmiotem</param>
    private void HideTooltip(SellButton sellButton)
    {
        itemTooltip.HideTooltip();
    }
}
