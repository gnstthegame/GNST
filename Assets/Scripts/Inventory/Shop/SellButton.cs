using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellButton : MonoBehaviour {
    [SerializeField] public Image image;
    [SerializeField] public Item item;
    [SerializeField] Button button;
    [SerializeField] Inventory inventory;
    [SerializeField] Shop shop;
    [SerializeField] ItemTooltip shopTooltip;
    [SerializeField] GameObject shopPanel;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Sell);
    }

    private void Update()
    {
        if(item == null)
        {
            image.color = new Color(0, 0, 0, 0);
        }
        else
        {
            image.color = Color.white;
            image.sprite = item.Sprite;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (item != null && shopPanel.activeInHierarchy)
            {
                if (item is EquippableItem)
                    shopTooltip.ShowTooltip((EquippableItem)item);
                else if (item is UsableItem)
                    shopTooltip.ShowTooltip((UsableItem)item);
            }
        }
        else
        {
            shopTooltip.HideTooltip();
        }
    }

    public void Sell()
    {
        if(item != null)
        {
            inventory.RemoveItem(item.ID);
            for (int i = 0; i < shop.sellItems.Count - 1; i++)
            {
                if (shop.sellItems[i] == null)
                {
                    shop.sellItems[i] = item;
                    break;
                }
            }
            inventory.money += item.SellValue;
            item = null;
        }
        Debug.Log("Sprzedano");
    }
}
