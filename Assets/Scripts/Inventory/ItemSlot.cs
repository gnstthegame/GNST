using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] Item item;
    [SerializeField] ItemTooltip tooltip;
    public Image image;

    //zdarzenie ktore sprawdza czy kliknieto prawy przycisk myszy 
    public event Action<Item> OnRightClickEvent;


    public Item Item
    {
        get { return item; }
        set
        {
            item = value;
            if(item == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = item.Sprite;
                image.enabled = true;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }

        if(tooltip == null)
        {
            tooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    //Klikniecie w slot
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(Item != null && OnRightClickEvent != null)
            {
                //trigger eventu informacja dla klasy inventorymanager
                OnRightClickEvent(Item);
            }
        }
    }

    //Wyswietlanie okien z informacjami o przedmiocie
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item is EquippableItem)
        {
            tooltip.ShowTooltip((EquippableItem)Item);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
