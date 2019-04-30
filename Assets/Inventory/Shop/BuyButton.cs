using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuyButton : MonoBehaviour {
    [SerializeField] public Image image;
    [SerializeField] public Item item;
    [SerializeField] Button button;
    [SerializeField] Inventory inventory;
    [SerializeField] Shop shop;
    [SerializeField] ItemTooltip shopTooltip;
    [SerializeField] GameObject shopPanel;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
	}

    private void Update()
    {
        if (item == null)
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
            if(item != null && shopPanel.activeInHierarchy)
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

    public void Buy()
    {
        if(item != null && item.BuyValue <= inventory.money && shop.sellItems.Count > 0)
        {
            for (int i = 0; i < shop.sellItems.Count - 1; i++)
            {
                if (shop.sellItems[i] == item)
                {
                    inventory.AddItem(item);
                    shop.sellItems[i] = null;
                    inventory.money -= item.BuyValue;
                    item = null;
                    break;
                }
            }            
        }
        Debug.Log("Zakupiono");
    }
}
