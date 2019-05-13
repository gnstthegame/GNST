using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler {

    private Item _item;
    public int _amount;
    [SerializeField] Image image;
    [SerializeField] Text amountText;
    //Vector2 originalPosition;

    //zdarzenie ktore sprawdza czy kliknieto prawy przycisk myszy 
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);


    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if(_item == null)
            {
                image.color = disabledColor;
            }
            else
            {
                image.sprite = _item.Sprite;
                image.color = normalColor;
            }
        }
    }

    public int Amount {
        get { return _amount; }
        set{
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0) Item = null;

            if (amountText != null)
            {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }            
        }
    }

    protected virtual void Awake()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }

        if(amountText == null)
        {
            amountText = GetComponentInChildren<Text>();
        }
    }

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public bool CanAddStack(Item item, int amount = 1)
    {
        return (Item != null && Item.ID == item.ID && Amount + amount <= item.MaximumStacks);
    }

    //Klikniecie w slot
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }

    //Wyswietlanie okien z informacjami o przedmiocie
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
}
