using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler {
<<<<<<< HEAD

    private Item _item;
    [SerializeField] Image image;
    [SerializeField] Text amountText;
    Vector2 originalPosition;

    //zdarzenie ktore sprawdza czy kliknieto prawy przycisk myszy 
=======
    [SerializeField]
    Image image;

>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

<<<<<<< HEAD
=======
    Vector2 originalPosition;


    Item _item;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d

    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
<<<<<<< HEAD
            if(_item == null)
=======

            if (_item == null)
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
            {
                image.color = disabledColor;
            }
            else
            {
<<<<<<< HEAD
                image.sprite = _item.Sprite;
=======
                image.sprite = _item.Icon;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
                image.color = normalColor;
            }
        }
    }

<<<<<<< HEAD
    private int _amount;
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

    protected virtual void OnValidate()
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

=======
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


>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

<<<<<<< HEAD
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
=======
    /// <summary>
    /// gdy skrypt jest wczytany lub cos sie zmieni
    /// </summary>
    protected virtual void OnValidate()
    {
        if(image == null)
            image = GetComponent<Image>();
    }

>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
<<<<<<< HEAD
        }        
=======
        }       
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
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

<<<<<<< HEAD
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
=======
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
<<<<<<< HEAD
        if (OnDropEvent != null)
=======
        if(OnDropEvent != null)
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
        {
            OnDropEvent(this);
        }
    }
}
