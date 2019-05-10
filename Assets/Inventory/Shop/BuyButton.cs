using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] public Image image;
    [SerializeField] public Item item;
    [SerializeField] Button button;
    [SerializeField] Inventory inventory;
    [SerializeField] Shop shop;
    [SerializeField] GameObject shopPanel;
    [SerializeField] ItemTooltip shopTooltip;
    public int stackSize;
    [SerializeField] public Text stackText;

    public event Action<BuyButton> OnPointerEnterEvent;
    public event Action<BuyButton> OnPointerExitEvent;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
	}

    private void OnValidate()
    {
        stackText = GetComponentInChildren<Text>();
    }

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
            if(stackSize > 1)
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
    //nie stackuje
    public void Buy()
    {
        if(item != null && item.BuyValue <= inventory.money)
        {
            for (int i = 0; i < shop.sellItems.Count - 1; i++)
            {
                if (shop.sellItems[i] == item)
                {
                    stackSize--;
                    inventory.AddItem(item);
                    inventory.money -= item.BuyValue;
                    if (stackSize == 0)
                    {
                        shop.sellItems[i] = null;
                        item = null;
                    }
                    break;
                }
            }            
        }
        Debug.Log("Zakupiono");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }
}
