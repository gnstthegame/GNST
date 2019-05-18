using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LootSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public int amount;
    [SerializeField] Image image;
    [SerializeField] Inventory inventory;
    [SerializeField] Text stackText;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] LootPanel lootPanel;
    public event Action<LootSlot> OnPointerEnterEvent;
    public event Action<LootSlot> OnPointerExitEvent;

    private void Awake() {
        lootPanel = FindObjectOfType<LootPanel>();
        inventory = FindObjectOfType<Inventory>();
        stackText = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (item == null) {
            image.color = new Color(0, 0, 0, 0);
            stackText.enabled = false;
        } else {
            image.color = Color.white;
            image.sprite = item.Sprite;
            if (amount > 1) {
                stackText.enabled = true;
                stackText.text = amount.ToString();
            } else {
                stackText.enabled = false;
            }
        }
    }

    //Poprawic dla niestackujacych sie itemow, wytyczne.txt
    //AD Uproszczenie
    public void GetItem() {
        if (inventory.IsFull()) {
            return;
        }
        for (int i = 0; i < lootPanel.chest.lootItems.Count; i++) {
            if (lootPanel.chest.lootItems[i].item == item) {
                if (item != null && item.MaximumStacks > 0) {
                    inventory.AddItem(item);
                    amount--;
                    lootPanel.chest.lootItems[i].amount--;
                    if (lootPanel.chest.lootItems[i].amount == 0) {
                        lootPanel.chest.lootItems[i] = new Pair();
                        item = null;
                    }
                    break;
                }
            }
        }
    }

    public void TakeAllFromSlot() {
        int pom = amount;
        for (int i = 0; i < pom; i++) {
            GetItem();
        }
        if (amount == 0)
            item = null;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }
}
